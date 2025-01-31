﻿//----------------------------------------------------------------------------- 
// <copyright file="ProfileSimpleArrayStore.cs" company="KEYENCE">
//	 Copyright (c) 2019 KEYENCE CORPORATION. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------- 

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace LJX8_DllSampleAll.Data
{
	/// <summary>
	/// Store simple array data both profile and luminance.
	/// </summary>
	public class ProfileSimpleArrayStore
	{
		[DllImport("kernel32.dll", SetLastError = false)]
		private static extern void CopyMemory(IntPtr destination, IntPtr source, UIntPtr length);

		#region constant
		private const int BatchFinalizeFlagBitCount = 16;
		#endregion

		#region Field
		/// <summary>Object for exclusive control</summary>
		private readonly object _syncObject = new object();

		private uint _count;
		private uint _notify;

		/// <summary>Profile data (simple array)</summary>
		public readonly List<ushort> _profileData = new List<ushort>();
		/// <summary>Luminance data (simple array)</summary>
		public readonly List<ushort> _luminanceData = new List<ushort>();
		#endregion

		#region Property
		/// <summary>Profile data size</summary>
		public int DataWidth { get; set; }
		/// <summary>The value indicating whether luminance data output is enable or not</summary>
		public bool IsLuminanceEnable { private get; set; }
		/// <summary>Latest batch number</summary>
		public int BatchNo { get; private set; }

		/// <summary>Stored profile count</summary>
		public uint Count
		{
			get
			{
				lock (_syncObject)
				{
					return _count;
				}
			}
			set
			{
				lock (_syncObject)
				{
					_count = value;
				}
			}
		}

		/// <summary>Callback function notification parameter (for high-speed communication)</summary>
		public uint Notify
		{
			get
			{
				lock (_syncObject)
				{
					uint value = _notify;
					_notify = 0;
					return value;
				}
			}
			set
			{
				lock (_syncObject)
				{
					if ((uint)(value & (0x1 << BatchFinalizeFlagBitCount)) != 0)
					{
						BatchNo++;
					}

					_notify |= value;
				}
			}
		}
		#endregion

		#region Method
		public bool AddReceivedData(IntPtr profileBuffer, IntPtr luminanceBuffer, uint count)
		{
			lock (_syncObject)
			{
				if (DataWidth <= 0)
				{
					return Count >= Define.BufferFullCount;
				}

				uint copyCount = Math.Min((uint)Define.BufferFullCount - Count, count);
				if (copyCount == 0) return Count >= Define.BufferFullCount;

				Count += copyCount;
				int bufferSize = DataWidth * (int)copyCount;
				ushort[] buffer = new ushort[bufferSize];
				CopyUshort(profileBuffer, buffer, bufferSize);
				_profileData.AddRange(buffer);

				if (IsLuminanceEnable)
				{
					CopyUshort(luminanceBuffer, buffer, bufferSize);
					_luminanceData.AddRange(buffer);
				}

				return Count >= Define.BufferFullCount;
			}
		}
		public void Clear()
		{
			lock (_syncObject)
			{
				BatchNo = 0;
				Count = 0;
				_notify = 0;
				DataWidth = 0;
				IsLuminanceEnable = false;
				_profileData.Clear();
				_luminanceData.Clear();
			}
		}

        public byte[] GetImageByte(int index, int count)
        {
            lock (_syncObject)
            {
                if (DataWidth <= 0 || _profileData == null)
                {
                    return new byte[1];
                }

                int profileHeight = _profileData.Count() / DataWidth;
                if (count <= 0 || index > profileHeight || index + count > profileHeight)
                {
                    return new byte[1];
                }

                byte[] profile = _profileData.Skip(index * DataWidth).Take(count * DataWidth).SelectMany(BitConverter.GetBytes).ToArray();

                return profile;
            }
        }

        public bool SaveDataAsImages(string filePath, int index, int count)
        {
            lock (_syncObject)
            {
                if (string.IsNullOrEmpty(filePath)) return false;

                if (DataWidth <= 0 || _profileData == null)
                {
                    return false;
                }

                int profileHeight = _profileData.Count() / DataWidth;
                if (count <= 0 || index > profileHeight || index + count > profileHeight)
                {
                    return false;
                }

                string pathBase = Path.Combine(Path.GetDirectoryName(filePath), Path.GetFileNameWithoutExtension(filePath));

                SaveTiff(index, count, pathBase);

                return true;
            }
        }

        private void SaveTiff(int index, int count, string pathBase)
        {
            const string heightSuffix = "_height.tif";
            const string luminanceSuffix = "_luminance.tif";

            lock (_syncObject)
            {
                SaveTiffCore(pathBase + heightSuffix, _profileData, index, count);
            }
        }

        private void SaveTiffCore(string filePath, IEnumerable<ushort> data, int index, int count)
        {
            byte[] profile = data.Skip(index * DataWidth).Take(count * DataWidth).SelectMany(BitConverter.GetBytes).ToArray();

            int profileHeight = profile.Count() / 2 / DataWidth;
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                WriteTiffHeader(stream, (uint)DataWidth, (uint)profileHeight);

                writer.Write(profile);
            }
        }

        private static void WriteTiffHeader(Stream stream, uint width, uint height)
        {
            // <header(8)> + <tag count(2)> + <tag(12)>*12 + <next IFD(4)> + <resolution unit(8)>*2
            const uint stripOffset = 174;

            stream.Position = 0;
            // Header (little endian)
            stream.Write(new byte[] { 0x49, 0x49, 0x2A, 0x00, 0x08, 0x00, 0x00, 0x00 }, 0, 8);

            // Tag count
            stream.Write(new byte[] { 0x0C, 0x00 }, 0, 2);

            // Image Width
            WriteTiffTag(stream, 0x0100, 3, 1, width);

            // Image Length
            WriteTiffTag(stream, 0x0101, 3, 1, height);

            // Bits per sample
            WriteTiffTag(stream, 0x0102, 3, 1, 16);

            // Compression (no compression)
            WriteTiffTag(stream, 0x0103, 3, 1, 1);

            // Photometric interpretation (white mode & monochrome)
            WriteTiffTag(stream, 0x0106, 3, 1, 1);

            // Strip offsets
            WriteTiffTag(stream, 0x0111, 3, 1, stripOffset);

            // Rows per strip
            WriteTiffTag(stream, 0x0116, 3, 1, height);

            // strip byte counts
            WriteTiffTag(stream, 0x0117, 4, 1, width * height * (uint)2);

            // X resolusion address
            WriteTiffTag(stream, 0x011A, 5, 1, stripOffset - 16);

            // Y resolusion address
            WriteTiffTag(stream, 0x011B, 5, 1, stripOffset - 8);

            // Resolusion unit (inch)
            WriteTiffTag(stream, 0x0128, 3, 1, 2);

            // Color map (not use color map)
            WriteTiffTag(stream, 0x0140, 3, 1, 0);

            // Next IFD
            stream.Write(BitConverter.GetBytes((int)0), 0, 4);

            // X resolusion and Y resolusion
            stream.Write(BitConverter.GetBytes((int)96), 0, 4);
            stream.Write(BitConverter.GetBytes((int)1), 0, 4);
            stream.Write(BitConverter.GetBytes((int)96), 0, 4);
            stream.Write(BitConverter.GetBytes((int)1), 0, 4);
        }

        private static void WriteTiffTag(Stream stream, ushort kind, ushort dataType, uint dataSize, uint data)
        {
            List<byte> list = new List<byte>();
            list.AddRange(BitConverter.GetBytes(kind));
            list.AddRange(BitConverter.GetBytes(dataType));
            list.AddRange(BitConverter.GetBytes(dataSize));
            list.AddRange(BitConverter.GetBytes(data));
            byte[] tag = list.ToArray();
            stream.Write(tag, 0, tag.Length);
        }

        private static void CopyUshort(IntPtr source, ushort[] destination, int length)
		{
			using (PinnedObject pin = new PinnedObject(destination))
			{
				int copyLength = Marshal.SizeOf(typeof(ushort)) * length;
				CopyMemory(pin.Pointer, source, (UIntPtr)copyLength);
			}
		}
		#endregion
	}
}
