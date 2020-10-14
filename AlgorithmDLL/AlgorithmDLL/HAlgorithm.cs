using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HalconDotNet;

namespace HalconAlgorithm
{
    public class HAlgorithm
    {
       private static HWindow inWindow, outWindow;
        
        //接收相机传入buffer并显示在输入窗口
        public static void DispBuffer(int MeasureProject, IntPtr buffer, ushort Bufferwidth, ushort Bufferheight)
        {
            HObject HojFromBuffer;
            if(MeasureProject == 9)
            {
                HOperatorSet.GenImage1Extern(out HojFromBuffer, "byte", Bufferwidth, Bufferheight, buffer, IntPtr.Zero);
            }
            else
            {
                HOperatorSet.GenImage1Extern(out HojFromBuffer, "uint2", Bufferwidth, Bufferheight, buffer, IntPtr.Zero);
            }
            HTuple width, height;
            HOperatorSet.GetImageSize(HojFromBuffer, out width, out height);
            inWindow.SetPart((HTuple)0, (HTuple)0, height, width);
            inWindow.DispObj(HojFromBuffer);
            HojFromBuffer.Dispose();
        }

        //销毁窗口
        public static void DisposeWindow()
        {
            inWindow.CloseWindow();
            outWindow.CloseWindow();
            inWindow.Dispose();
            outWindow.Dispose();
        }

        //创建输入显示窗口
        public static void CreateInWindow(IntPtr pb_Handle, int pb_width, int pb_height)
        {
            inWindow = new HWindow(0, 0, pb_width, pb_height, pb_Handle, "visible", "");
        }

        //创建输出显示窗口
        public static void CreateOutWindow(IntPtr pb_Handle, int pb_width, int pb_height)
        {
            outWindow = new HWindow(0, 0, pb_width, pb_height, pb_Handle, "visible", "");
        }

        //在输出窗口显示图像
        public static void DispImage(HObject image)
        {
            HTuple width, height;
            HOperatorSet.GetImageSize(image, out width, out height);
            outWindow.SetPart((HTuple)0, (HTuple)0, height, width);
            outWindow.DispObj(image);
            image.Dispose();
        }

        //读取给定路径下的所有图像文件
        public static void list_image_files(HTuple hv_ImageDirectory, HTuple hv_Extensions, HTuple hv_Options,
    out HTuple hv_ImageFiles)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_ImageDirectoryIndex = new HTuple();
            HTuple hv_ImageFilesTmp = new HTuple(), hv_CurrentImageDirectory = new HTuple();
            HTuple hv_HalconImages = new HTuple(), hv_OS = new HTuple();
            HTuple hv_Directories = new HTuple(), hv_Index = new HTuple();
            HTuple hv_Length = new HTuple(), hv_NetworkDrive = new HTuple();
            HTuple hv_Substring = new HTuple(), hv_FileExists = new HTuple();
            HTuple hv_AllFiles = new HTuple(), hv_i = new HTuple();
            HTuple hv_Selection = new HTuple();
            HTuple hv_Extensions_COPY_INP_TMP = new HTuple(hv_Extensions);

            // Initialize local and output iconic variables 
            hv_ImageFiles = new HTuple();
            //This procedure returns all files in a given directory
            //with one of the suffixes specified in Extensions.
            //
            //Input parameters:
            //ImageDirectory: Directory or a tuple of directories with images.
            //   If a directory is not found locally, the respective directory
            //   is searched under %HALCONIMAGES%/ImageDirectory.
            //   See the Installation Guide for further information
            //   in case %HALCONIMAGES% is not set.
            //Extensions: A string tuple containing the extensions to be found
            //   e.g. ['png','tif',jpg'] or others
            //If Extensions is set to 'default' or the empty string '',
            //   all image suffixes supported by HALCON are used.
            //Options: as in the operator list_files, except that the 'files'
            //   option is always used. Note that the 'directories' option
            //   has no effect but increases runtime, because only files are
            //   returned.
            //
            //Output parameter:
            //ImageFiles: A tuple of all found image file names
            //
            if ((int)((new HTuple((new HTuple(hv_Extensions_COPY_INP_TMP.TupleEqual(new HTuple()))).TupleOr(
                new HTuple(hv_Extensions_COPY_INP_TMP.TupleEqual(""))))).TupleOr(new HTuple(hv_Extensions_COPY_INP_TMP.TupleEqual(
                "default")))) != 0)
            {
                hv_Extensions_COPY_INP_TMP.Dispose();
                hv_Extensions_COPY_INP_TMP = new HTuple();
                hv_Extensions_COPY_INP_TMP[0] = "ima";
                hv_Extensions_COPY_INP_TMP[1] = "tif";
                hv_Extensions_COPY_INP_TMP[2] = "tiff";
                hv_Extensions_COPY_INP_TMP[3] = "gif";
                hv_Extensions_COPY_INP_TMP[4] = "bmp";
                hv_Extensions_COPY_INP_TMP[5] = "jpg";
                hv_Extensions_COPY_INP_TMP[6] = "jpeg";
                hv_Extensions_COPY_INP_TMP[7] = "jp2";
                hv_Extensions_COPY_INP_TMP[8] = "jxr";
                hv_Extensions_COPY_INP_TMP[9] = "png";
                hv_Extensions_COPY_INP_TMP[10] = "pcx";
                hv_Extensions_COPY_INP_TMP[11] = "ras";
                hv_Extensions_COPY_INP_TMP[12] = "xwd";
                hv_Extensions_COPY_INP_TMP[13] = "pbm";
                hv_Extensions_COPY_INP_TMP[14] = "pnm";
                hv_Extensions_COPY_INP_TMP[15] = "pgm";
                hv_Extensions_COPY_INP_TMP[16] = "ppm";
                //
            }
            hv_ImageFiles.Dispose();
            hv_ImageFiles = new HTuple();
            //Loop through all given image directories.
            for (hv_ImageDirectoryIndex = 0; (int)hv_ImageDirectoryIndex <= (int)((new HTuple(hv_ImageDirectory.TupleLength()
                )) - 1); hv_ImageDirectoryIndex = (int)hv_ImageDirectoryIndex + 1)
            {
                hv_ImageFilesTmp.Dispose();
                hv_ImageFilesTmp = new HTuple();
                hv_CurrentImageDirectory.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_CurrentImageDirectory = hv_ImageDirectory.TupleSelect(
                        hv_ImageDirectoryIndex);
                }
                if ((int)(new HTuple(hv_CurrentImageDirectory.TupleEqual(""))) != 0)
                {
                    hv_CurrentImageDirectory.Dispose();
                    hv_CurrentImageDirectory = ".";
                }
                hv_HalconImages.Dispose();
                HOperatorSet.GetSystem("image_dir", out hv_HalconImages);
                hv_OS.Dispose();
                HOperatorSet.GetSystem("operating_system", out hv_OS);
                if ((int)(new HTuple(((hv_OS.TupleSubstr(0, 2))).TupleEqual("Win"))) != 0)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_HalconImages = hv_HalconImages.TupleSplit(
                                ";");
                            hv_HalconImages.Dispose();
                            hv_HalconImages = ExpTmpLocalVar_HalconImages;
                        }
                    }
                }
                else
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_HalconImages = hv_HalconImages.TupleSplit(
                                ":");
                            hv_HalconImages.Dispose();
                            hv_HalconImages = ExpTmpLocalVar_HalconImages;
                        }
                    }
                }
                hv_Directories.Dispose();
                hv_Directories = new HTuple(hv_CurrentImageDirectory);
                for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_HalconImages.TupleLength()
                    )) - 1); hv_Index = (int)hv_Index + 1)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_Directories = hv_Directories.TupleConcat(
                                ((hv_HalconImages.TupleSelect(hv_Index)) + "/") + hv_CurrentImageDirectory);
                            hv_Directories.Dispose();
                            hv_Directories = ExpTmpLocalVar_Directories;
                        }
                    }
                }
                hv_Length.Dispose();
                HOperatorSet.TupleStrlen(hv_Directories, out hv_Length);
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_NetworkDrive.Dispose();
                    HOperatorSet.TupleGenConst(new HTuple(hv_Length.TupleLength()), 0, out hv_NetworkDrive);
                }
                if ((int)(new HTuple(((hv_OS.TupleSubstr(0, 2))).TupleEqual("Win"))) != 0)
                {
                    for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_Length.TupleLength()
                        )) - 1); hv_Index = (int)hv_Index + 1)
                    {
                        if ((int)(new HTuple(((((hv_Directories.TupleSelect(hv_Index))).TupleStrlen()
                            )).TupleGreater(1))) != 0)
                        {
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_Substring.Dispose();
                                HOperatorSet.TupleStrFirstN(hv_Directories.TupleSelect(hv_Index), 1,
                                    out hv_Substring);
                            }
                            if ((int)((new HTuple(hv_Substring.TupleEqual("//"))).TupleOr(new HTuple(hv_Substring.TupleEqual(
                                "\\\\")))) != 0)
                            {
                                if (hv_NetworkDrive == null)
                                    hv_NetworkDrive = new HTuple();
                                hv_NetworkDrive[hv_Index] = 1;
                            }
                        }
                    }
                }
                hv_ImageFilesTmp.Dispose();
                hv_ImageFilesTmp = new HTuple();
                for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_Directories.TupleLength()
                    )) - 1); hv_Index = (int)hv_Index + 1)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_FileExists.Dispose();
                        HOperatorSet.FileExists(hv_Directories.TupleSelect(hv_Index), out hv_FileExists);
                    }
                    if ((int)(hv_FileExists) != 0)
                    {
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_AllFiles.Dispose();
                            HOperatorSet.ListFiles(hv_Directories.TupleSelect(hv_Index), (new HTuple("files")).TupleConcat(
                                hv_Options), out hv_AllFiles);
                        }
                        hv_ImageFilesTmp.Dispose();
                        hv_ImageFilesTmp = new HTuple();
                        for (hv_i = 0; (int)hv_i <= (int)((new HTuple(hv_Extensions_COPY_INP_TMP.TupleLength()
                            )) - 1); hv_i = (int)hv_i + 1)
                        {
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_Selection.Dispose();
                                HOperatorSet.TupleRegexpSelect(hv_AllFiles, (((".*" + (hv_Extensions_COPY_INP_TMP.TupleSelect(
                                    hv_i))) + "$")).TupleConcat("ignore_case"), out hv_Selection);
                            }
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                {
                                    HTuple
                                      ExpTmpLocalVar_ImageFilesTmp = hv_ImageFilesTmp.TupleConcat(
                                        hv_Selection);
                                    hv_ImageFilesTmp.Dispose();
                                    hv_ImageFilesTmp = ExpTmpLocalVar_ImageFilesTmp;
                                }
                            }
                        }
                        {
                            HTuple ExpTmpOutVar_0;
                            HOperatorSet.TupleRegexpReplace(hv_ImageFilesTmp, (new HTuple("\\\\")).TupleConcat(
                                "replace_all"), "/", out ExpTmpOutVar_0);
                            hv_ImageFilesTmp.Dispose();
                            hv_ImageFilesTmp = ExpTmpOutVar_0;
                        }
                        if ((int)(hv_NetworkDrive.TupleSelect(hv_Index)) != 0)
                        {
                            {
                                HTuple ExpTmpOutVar_0;
                                HOperatorSet.TupleRegexpReplace(hv_ImageFilesTmp, (new HTuple("//")).TupleConcat(
                                    "replace_all"), "/", out ExpTmpOutVar_0);
                                hv_ImageFilesTmp.Dispose();
                                hv_ImageFilesTmp = ExpTmpOutVar_0;
                            }
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                {
                                    HTuple
                                      ExpTmpLocalVar_ImageFilesTmp = "/" + hv_ImageFilesTmp;
                                    hv_ImageFilesTmp.Dispose();
                                    hv_ImageFilesTmp = ExpTmpLocalVar_ImageFilesTmp;
                                }
                            }
                        }
                        else
                        {
                            {
                                HTuple ExpTmpOutVar_0;
                                HOperatorSet.TupleRegexpReplace(hv_ImageFilesTmp, (new HTuple("//")).TupleConcat(
                                    "replace_all"), "/", out ExpTmpOutVar_0);
                                hv_ImageFilesTmp.Dispose();
                                hv_ImageFilesTmp = ExpTmpOutVar_0;
                            }
                        }
                        break;
                    }
                }
                //Concatenate the output image paths.
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    {
                        HTuple
                          ExpTmpLocalVar_ImageFiles = hv_ImageFiles.TupleConcat(
                            hv_ImageFilesTmp);
                        hv_ImageFiles.Dispose();
                        hv_ImageFiles = ExpTmpLocalVar_ImageFiles;
                    }
                }
            }

            hv_Extensions_COPY_INP_TMP.Dispose();
            hv_ImageDirectoryIndex.Dispose();
            hv_ImageFilesTmp.Dispose();
            hv_CurrentImageDirectory.Dispose();
            hv_HalconImages.Dispose();
            hv_OS.Dispose();
            hv_Directories.Dispose();
            hv_Index.Dispose();
            hv_Length.Dispose();
            hv_NetworkDrive.Dispose();
            hv_Substring.Dispose();
            hv_FileExists.Dispose();
            hv_AllFiles.Dispose();
            hv_i.Dispose();
            hv_Selection.Dispose();

            return;
        }

        //离线测试
        public static bool OutLineMeasure(int MeasureProject, ListView lv, 
            out double Radius, out double PositionDegree, out double RunTime,
               out double DistanceX1, out double DistanceY1, string path, 
               out double Origin_Z_mm, out double E2_OffestZ_mm, out double E5_OffestZ_mm, 
               out double E10_OffestZ_mm, out double E13_OffestZ_mm, out double Profile)
        {
            Radius = -1;
            PositionDegree = -1;
            RunTime = -1;
            DistanceX1 = -1;
            DistanceY1 = -1;
            Origin_Z_mm = -1;
            E2_OffestZ_mm = -1;
            E5_OffestZ_mm = -1;
            E10_OffestZ_mm = -1;
            E13_OffestZ_mm = -1;
            Profile = -1;
            HObject image;
            HOperatorSet.GenEmptyObj(out image);
            image.Dispose();
            HTuple hv_ImageFiles = new HTuple(), hv_Index = new HTuple();
            hv_ImageFiles.Dispose(); hv_Index.Dispose();
            bool MeasureIsSucced = false;
            
            list_image_files(path, "default", new HTuple(), out hv_ImageFiles);
            for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_ImageFiles.TupleLength()
        )) - 1); hv_Index = (int)hv_Index + 1)
            {
                HOperatorSet.ReadImage(out image, hv_ImageFiles.TupleSelect(hv_Index));
                switch (MeasureProject)
                {
                    case 9:
                        MeasureIsSucced = Measure_9(image, out Radius, out PositionDegree, out RunTime, out DistanceX1, out DistanceY1);
                        break;
                    case 18:
                        MeasureIsSucced = Measure_18(image, out Origin_Z_mm, out E2_OffestZ_mm, out E5_OffestZ_mm, out E10_OffestZ_mm, out E13_OffestZ_mm,
                                                out Profile, out RunTime);
                        break;
                    default:
                        break;
                }
                if(MeasureIsSucced == false)
                {
                    break;
                }
                else
                {
                    if (MeasureProject == 9)
                    {
                        insertLine2D(lv, RunTime, Radius, PositionDegree, DistanceX1, DistanceY1);
                    }
                    else if(MeasureProject ==18)
                    {
                        insertLine3D(lv, RunTime, Origin_Z_mm, E2_OffestZ_mm, E5_OffestZ_mm, E10_OffestZ_mm, E13_OffestZ_mm,Profile);
                    }
                    image.Dispose();
                    HOperatorSet.WaitSeconds(0.5);
                }
            }
            if (MeasureIsSucced == false)
            {
                MessageBox.Show("请加载正确工件图像！");
                return false;
            }
            return true;
        }

        //在线测试
        public static bool InLineMeasure(int MeasureProject, ListView lv, IntPtr buffer, ushort BufferWidth, ushort BufferHeight,
    out double Radius, out double PositionDegree, out double RunTime, out double DistanceX1, out double DistanceY1,
    out double Origin_Z_mm, out double E2_OffestZ_mm, out double E5_OffestZ_mm, out double E10_OffestZ_mm, out double E13_OffestZ_mm, out double Profile)
        {
            Radius = -1;
            PositionDegree = -1;
            RunTime = -1;
            DistanceX1 = -1;
            DistanceY1 = -1;
            Origin_Z_mm = -1;
            E2_OffestZ_mm = -1;
            E5_OffestZ_mm = -1;
            E10_OffestZ_mm = -1;
            E13_OffestZ_mm = -1;
            Profile = -1;
            HObject image;
            if(MeasureProject == 9)
            {
                 HOperatorSet.GenImage1Extern(out image, "byte", BufferWidth, BufferHeight, buffer, IntPtr.Zero);
            }
            else
            {
                HOperatorSet.GenImage1Extern(out image, "uint2", BufferWidth, BufferHeight, buffer, IntPtr.Zero);
            }
            bool MeasureISucced = false;

            switch (MeasureProject)
            {
                case 9:
                    MeasureISucced = Measure_9(image, out Radius, out PositionDegree, out RunTime, out DistanceX1, out DistanceY1);
                    break;
                case 18:
                    MeasureISucced = Measure_18(image, out Origin_Z_mm, out E2_OffestZ_mm, out E5_OffestZ_mm, out E10_OffestZ_mm, out E13_OffestZ_mm,
                                                out Profile, out RunTime);
                    break;
                default:
                    break;
            }
            if(MeasureISucced == true && MeasureProject == 9)
            {
                insertLine2D(lv, RunTime, Radius, PositionDegree, DistanceX1, DistanceY1);
                image.Dispose();
                return true;
            }
            else if (MeasureISucced == true && MeasureProject == 18)
            {
                insertLine3D(lv, RunTime, Origin_Z_mm, E2_OffestZ_mm, E5_OffestZ_mm, E10_OffestZ_mm, E13_OffestZ_mm, Profile);
                image.Dispose();
                return true;
            }
            else
            {
                MessageBox.Show("请正确放置工件！");
                image.Dispose();
                return false;
            }
        }

        //测量项
        #region
       
            //获取两像素点间的真实距离
        private static void gen_pixel2real_distance(HTuple hv_PixclRealDis, out HTuple hv_p1_XOffest, out HTuple hv_p1_YOffest, 
            out HTuple hv_p2_XOffest, out HTuple hv_p2_YOffest, out HTuple hv_p3_XOffest, out HTuple hv_p3_YOffest, out HTuple hv_p4_XOffest, out HTuple hv_p4_YOffest)
        {
            hv_p1_XOffest = new HTuple();
            hv_p1_XOffest.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_p1_XOffest = 22.820 / hv_PixclRealDis;
            }
            hv_p1_YOffest = new HTuple();
            hv_p1_YOffest.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_p1_YOffest = 3.312 / hv_PixclRealDis;
            }

            hv_p2_XOffest = new HTuple();
            hv_p2_XOffest.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_p2_XOffest = 16.390 / hv_PixclRealDis;
            }

            hv_p2_YOffest = new HTuple();
            hv_p2_YOffest.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_p2_YOffest = 3.312 / hv_PixclRealDis;
            }

            hv_p3_XOffest = new HTuple();
            hv_p3_XOffest.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_p3_XOffest = 16.390 / hv_PixclRealDis;
            }

            hv_p3_YOffest = new HTuple();
            hv_p3_YOffest.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_p3_YOffest = 10.264 / hv_PixclRealDis;
            }

            hv_p4_XOffest = new HTuple();
            hv_p4_XOffest.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_p4_XOffest = 22.820 / hv_PixclRealDis;
            }
            hv_p4_YOffest = new HTuple();
            hv_p4_YOffest.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_p4_YOffest = 10.264 / hv_PixclRealDis;
            }
        }

        //设置3D测量项中ZMap图的高度换算系数
        public static void Set_Offest_Param(out HTuple hv_p_A1_x_offest, out HTuple hv_p_A1_y_offest,
            out HTuple hv_p_A2_y_offest, out HTuple hv_p_A2_x_offest, out HTuple hv_p_A3_y_offest,
            out HTuple hv_p_A3_x_offest, out HTuple hv_p_A4_y_offest, out HTuple hv_p_A4_x_offest,
            out HTuple hv_p_A5_y_offest, out HTuple hv_p_A5_x_offest, out HTuple hv_p_A6_y_offest,
            out HTuple hv_p_A6_x_offest, out HTuple hv_p_A7_y_offest, out HTuple hv_p_A7_x_offest,
            out HTuple hv_p_A8_y_offest, out HTuple hv_p_A8_x_offest, out HTuple hv_p_E2_y_offest,
            out HTuple hv_p_E2_x_offest, out HTuple hv_p_E5_y_offest, out HTuple hv_p_E5_x_offest,
            out HTuple hv_p_E10_y_offest, out HTuple hv_p_E10_x_offest, out HTuple hv_p_E13_y_offest,
            out HTuple hv_p_E13_x_offest, out HTuple hv_B_offest, out HTuple hv_C_offest)
        {


            // Local iconic variables 
            // Initialize local and output iconic variables 
            hv_p_A1_x_offest = new HTuple();
            hv_p_A1_y_offest = new HTuple();
            hv_p_A2_y_offest = new HTuple();
            hv_p_A2_x_offest = new HTuple();
            hv_p_A3_y_offest = new HTuple();
            hv_p_A3_x_offest = new HTuple();
            hv_p_A4_y_offest = new HTuple();
            hv_p_A4_x_offest = new HTuple();
            hv_p_A5_y_offest = new HTuple();
            hv_p_A5_x_offest = new HTuple();
            hv_p_A6_y_offest = new HTuple();
            hv_p_A6_x_offest = new HTuple();
            hv_p_A7_y_offest = new HTuple();
            hv_p_A7_x_offest = new HTuple();
            hv_p_A8_y_offest = new HTuple();
            hv_p_A8_x_offest = new HTuple();
            hv_p_E2_y_offest = new HTuple();
            hv_p_E2_x_offest = new HTuple();
            hv_p_E5_y_offest = new HTuple();
            hv_p_E5_x_offest = new HTuple();
            hv_p_E10_y_offest = new HTuple();
            hv_p_E10_x_offest = new HTuple();
            hv_p_E13_y_offest = new HTuple();
            hv_p_E13_x_offest = new HTuple();
            hv_B_offest = new HTuple();
            hv_C_offest = new HTuple();
            hv_p_A1_x_offest.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_p_A1_x_offest = 7.12 / 0.0125875;
            }
            hv_p_A1_y_offest.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_p_A1_y_offest = 4.340 / 0.02;
            }

            hv_p_A2_x_offest.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_p_A2_x_offest = 7.12 / 0.0125875;
            }
            hv_p_A2_y_offest.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_p_A2_y_offest = -4.340 / 0.018;
            }

            hv_p_A3_x_offest.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_p_A3_x_offest = 4.540 / 0.0125875;
            }
            hv_p_A3_y_offest.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_p_A3_y_offest = -6.570 / 0.0185;
            }

            hv_p_A4_x_offest.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_p_A4_x_offest = -4.540 / 0.0125875;
            }
            hv_p_A4_y_offest.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_p_A4_y_offest = -6.570 / 0.0182;
            }

            hv_p_A5_x_offest.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_p_A5_x_offest = -6.940 / 0.0125875;
            }
            hv_p_A5_y_offest.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_p_A5_y_offest = -4.340 / 0.02;
            }

            hv_p_A6_x_offest.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_p_A6_x_offest = -6.940 / 0.0125875;
            }
            hv_p_A6_y_offest.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_p_A6_y_offest = 4.340 / 0.025;
            }

            hv_p_A7_x_offest.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_p_A7_x_offest = -4.540 / 0.0125875;
            }
            hv_p_A7_y_offest.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_p_A7_y_offest = 6.570 / 0.0216;
            }

            hv_p_A8_x_offest.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_p_A8_x_offest = 4.540 / 0.0125875;
            }
            hv_p_A8_y_offest.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_p_A8_y_offest = 6.570 / 0.0215;
            }

            hv_p_E2_x_offest.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_p_E2_x_offest = 5.220 / 0.0125875;
            }
            hv_p_E2_y_offest.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_p_E2_y_offest = 1.520 / 0.01;
            }

            hv_p_E5_x_offest.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_p_E5_x_offest = 5.220 / 0.0125875;
            }
            hv_p_E5_y_offest.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_p_E5_y_offest = -1.520 / 0.01;
            }

            hv_p_E10_x_offest.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_p_E10_x_offest = -5.220 / 0.0125875;
            }
            hv_p_E10_y_offest.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_p_E10_y_offest = -1.520 / 0.01;
            }

            hv_p_E13_x_offest.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_p_E13_x_offest = -5.220 / 0.0125875;
            }
            hv_p_E13_y_offest.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_p_E13_y_offest = 1.520 / 0.01;
            }

            hv_B_offest.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_B_offest = 6.788 / 0.0125875;
            }
            hv_C_offest.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_C_offest = 19.605 / 0.01;
            }


            return;
        }

        //根据八个平面拟合点拟合基准平面
        public static void my_fit_surface(HObject ho_ImageZ, HTuple hv_HomMat2DIdentity, HTuple hv_p_A1_x_offest,
            HTuple hv_p_A1_y_offest, HTuple hv_Phi, HTuple hv_Col_Origin, HTuple hv_Row_Origin,
            HTuple hv_p_A2_x_offest, HTuple hv_p_A2_y_offest, HTuple hv_p_A3_x_offest,
            HTuple hv_p_A3_y_offest, HTuple hv_p_A4_x_offest, HTuple hv_p_A4_y_offest, HTuple hv_p_A5_x_offest,
            HTuple hv_p_A5_y_offest, HTuple hv_p_A6_x_offest, HTuple hv_p_A6_y_offest, HTuple hv_p_A7_x_offest,
            HTuple hv_p_A7_y_offest, HTuple hv_p_A8_x_offest, HTuple hv_p_A8_y_offest, out HTuple hv_Z_Origin_median)
        {




            // Local iconic variables 

            // Local control variables 

            HTuple hv_p_A1_x_fit = new HTuple(), hv_p_A1_y_fit = new HTuple();
            HTuple hv_p_A2_x_fit = new HTuple(), hv_p_A2_y_fit = new HTuple();
            HTuple hv_p_A3_x_fit = new HTuple(), hv_p_A3_y_fit = new HTuple();
            HTuple hv_p_A4_x_fit = new HTuple(), hv_p_A4_y_fit = new HTuple();
            HTuple hv_p_A5_x_fit = new HTuple(), hv_p_A5_y_fit = new HTuple();
            HTuple hv_p_A6_x_fit = new HTuple(), hv_p_A6_y_fit = new HTuple();
            HTuple hv_p_A7_x_fit = new HTuple(), hv_p_A7_y_fit = new HTuple();
            HTuple hv_p_A8_x_fit = new HTuple(), hv_p_A8_y_fit = new HTuple();
            HTuple hv_Surface_Zs = new HTuple(), hv_Z_Origin_Max = new HTuple();
            HTuple hv_Z_Origin_Min = new HTuple(), hv_Mean = new HTuple();
            HTuple hv_Z_Origin_NoMaxMin = new HTuple(), hv_Index = new HTuple();
            // Initialize local and output iconic variables 
            hv_Z_Origin_median = new HTuple();
            //定位8个给定平面拟合点
            hv_p_A1_x_fit.Dispose(); hv_p_A1_y_fit.Dispose();
            my_gen_suffce_fit_point(hv_Row_Origin, hv_Col_Origin, hv_Phi, hv_p_A1_x_offest,
                hv_p_A1_y_offest, out hv_p_A1_x_fit, out hv_p_A1_y_fit);
            hv_p_A2_x_fit.Dispose(); hv_p_A2_y_fit.Dispose();
            my_gen_suffce_fit_point(hv_Row_Origin, hv_Col_Origin, hv_Phi, hv_p_A2_x_offest,
                hv_p_A2_y_offest, out hv_p_A2_x_fit, out hv_p_A2_y_fit);
            hv_p_A3_x_fit.Dispose(); hv_p_A3_y_fit.Dispose();
            my_gen_suffce_fit_point(hv_Row_Origin, hv_Col_Origin, hv_Phi, hv_p_A3_x_offest,
                hv_p_A3_y_offest, out hv_p_A3_x_fit, out hv_p_A3_y_fit);
            hv_p_A4_x_fit.Dispose(); hv_p_A4_y_fit.Dispose();
            my_gen_suffce_fit_point(hv_Row_Origin, hv_Col_Origin, hv_Phi, hv_p_A4_x_offest,
                hv_p_A4_y_offest, out hv_p_A4_x_fit, out hv_p_A4_y_fit);
            hv_p_A5_x_fit.Dispose(); hv_p_A5_y_fit.Dispose();
            my_gen_suffce_fit_point(hv_Row_Origin, hv_Col_Origin, hv_Phi, hv_p_A5_x_offest,
                hv_p_A5_y_offest, out hv_p_A5_x_fit, out hv_p_A5_y_fit);
            hv_p_A6_x_fit.Dispose(); hv_p_A6_y_fit.Dispose();
            my_gen_suffce_fit_point(hv_Row_Origin, hv_Col_Origin, hv_Phi, hv_p_A6_x_offest,
                hv_p_A6_y_offest, out hv_p_A6_x_fit, out hv_p_A6_y_fit);
            hv_p_A7_x_fit.Dispose(); hv_p_A7_y_fit.Dispose();
            my_gen_suffce_fit_point(hv_Row_Origin, hv_Col_Origin, hv_Phi, hv_p_A7_x_offest,
                hv_p_A7_y_offest, out hv_p_A7_x_fit, out hv_p_A7_y_fit);
            hv_p_A8_x_fit.Dispose(); hv_p_A8_y_fit.Dispose();
            my_gen_suffce_fit_point(hv_Row_Origin, hv_Col_Origin, hv_Phi, hv_p_A8_x_offest,
                hv_p_A8_y_offest, out hv_p_A8_x_fit, out hv_p_A8_y_fit);

            //获取八个平面拟合点的灰度值，即Z向距离
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_Surface_Zs.Dispose();
                HOperatorSet.GetGrayvalInterpolated(ho_ImageZ, ((((((((((((hv_p_A1_y_fit.TupleConcat(
                    hv_p_A2_y_fit))).TupleConcat(hv_p_A3_y_fit))).TupleConcat(hv_p_A4_y_fit))).TupleConcat(
                    hv_p_A5_y_fit))).TupleConcat(hv_p_A6_y_fit))).TupleConcat(hv_p_A7_y_fit))).TupleConcat(
                    hv_p_A8_y_fit), ((((((((((((hv_p_A1_x_fit.TupleConcat(hv_p_A2_x_fit))).TupleConcat(
                    hv_p_A3_x_fit))).TupleConcat(hv_p_A4_x_fit))).TupleConcat(hv_p_A5_x_fit))).TupleConcat(
                    hv_p_A6_x_fit))).TupleConcat(hv_p_A7_x_fit))).TupleConcat(hv_p_A8_x_fit),
                    "bicubic_clipped", out hv_Surface_Zs);
            }

            //根据八个平面拟合点，获取八个Z向值，去除最大最小值干扰后求取中位值
            //将中位值作为Z向0点
            hv_Z_Origin_Max.Dispose();
            HOperatorSet.TupleMax(hv_Surface_Zs, out hv_Z_Origin_Max);
            hv_Z_Origin_Min.Dispose();
            HOperatorSet.TupleMin(hv_Surface_Zs, out hv_Z_Origin_Min);
            hv_Mean.Dispose();
            HOperatorSet.TupleMean(hv_Surface_Zs, out hv_Mean);
            //threshVal := ((Z_Origin_Max-Mean)+(Mean-Z_Origin_Min))/12

            hv_Z_Origin_NoMaxMin.Dispose();
            hv_Z_Origin_NoMaxMin = new HTuple();
            for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_Surface_Zs.TupleLength()
                )) - 1); hv_Index = (int)hv_Index + 1)
            {
                //*     if(Surface_Zs[Index] != Z_Origin_Max and Surface_Zs[Index] != Z_Origin_Min)
                //*     if(abs(Surface_Zs[Index]-Mean) < threshVal)
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    {
                        HTuple
                          ExpTmpLocalVar_Z_Origin_NoMaxMin = hv_Z_Origin_NoMaxMin.TupleConcat(
                            (((hv_Surface_Zs.TupleSelect(hv_Index)) - 32768) * 1.6) / 1000);
                        hv_Z_Origin_NoMaxMin.Dispose();
                        hv_Z_Origin_NoMaxMin = ExpTmpLocalVar_Z_Origin_NoMaxMin;
                    }
                }
                //*     endif
            }
            //tuple_mean (Z_Origin_NoMaxMin, Z_Origin_median)
            hv_Z_Origin_median.Dispose();
            HOperatorSet.TupleMedian(hv_Z_Origin_NoMaxMin, out hv_Z_Origin_median);

            hv_p_A1_x_fit.Dispose();
            hv_p_A1_y_fit.Dispose();
            hv_p_A2_x_fit.Dispose();
            hv_p_A2_y_fit.Dispose();
            hv_p_A3_x_fit.Dispose();
            hv_p_A3_y_fit.Dispose();
            hv_p_A4_x_fit.Dispose();
            hv_p_A4_y_fit.Dispose();
            hv_p_A5_x_fit.Dispose();
            hv_p_A5_y_fit.Dispose();
            hv_p_A6_x_fit.Dispose();
            hv_p_A6_y_fit.Dispose();
            hv_p_A7_x_fit.Dispose();
            hv_p_A7_y_fit.Dispose();
            hv_p_A8_x_fit.Dispose();
            hv_p_A8_y_fit.Dispose();
            hv_Surface_Zs.Dispose();
            hv_Z_Origin_Max.Dispose();
            hv_Z_Origin_Min.Dispose();
            hv_Mean.Dispose();
            hv_Z_Origin_NoMaxMin.Dispose();
            hv_Index.Dispose();

            return;
        }

        //计算3D测量结果
        public static void my_gen_result(HTuple hv_E2_Z_mm, HTuple hv_E5_Z_mm, HTuple hv_E10_Z_mm,
            HTuple hv_E13_Z_mm, out HTuple hv_result)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_Max = new HTuple(), hv_Min = new HTuple();
            // Initialize local and output iconic variables 
            hv_result = new HTuple();
            hv_Max.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_Max = ((((((((((2.612 + hv_E2_Z_mm)).TupleAbs()
                    )).TupleConcat(((2.612 + hv_E5_Z_mm)).TupleAbs()))).TupleConcat(((2.612 + hv_E10_Z_mm)).TupleAbs()
                    ))).TupleConcat(((2.612 + hv_E13_Z_mm)).TupleAbs()))).TupleMax();
            }
            hv_Min.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_Min = ((((((((((2.612 + hv_E2_Z_mm)).TupleAbs()
                    )).TupleConcat(((2.612 + hv_E5_Z_mm)).TupleAbs()))).TupleConcat(((2.612 + hv_E10_Z_mm)).TupleAbs()
                    ))).TupleConcat(((2.612 + hv_E13_Z_mm)).TupleAbs()))).TupleMin();
            }
            //result := 2*max([Max,Min])-0.2
            hv_result.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_result = (2 * (((hv_Max.TupleConcat(
                    hv_Min))).TupleMax())) - 0.04;
            }


            hv_Max.Dispose();
            hv_Min.Dispose();

            return;
        }

        //获取平面拟合点
        private static void my_gen_suffce_fit_point(HTuple hv_Row_Origin, HTuple hv_Col_Origin,
            HTuple hv_Phi, HTuple hv_p_A1_x_offest, HTuple hv_p_A1_y_offest, out HTuple hv_p_A_x_fit,
            out HTuple hv_p_A_y_fit)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_HomMat2D_Img2Base = new HTuple();
            HTuple hv_Col_Origin_InBase = new HTuple(), hv_Row_Origin_InBase = new HTuple();
            HTuple hv_p_A_XInBase = new HTuple(), hv_p_A_YInBase = new HTuple();
            HTuple hv_HomMat2D_Base2Img = new HTuple();
            // Initialize local and output iconic variables 
            hv_p_A_x_fit = new HTuple();
            hv_p_A_y_fit = new HTuple();
            //计算图像坐标系到基准坐标系的变换矩阵
            hv_HomMat2D_Img2Base.Dispose();
            HOperatorSet.VectorAngleToRigid(0, 0, 0, hv_Row_Origin, hv_Col_Origin, hv_Phi,
                out hv_HomMat2D_Img2Base);
            //将图像坐标系下的基准点转换到基准坐标系下
            hv_Col_Origin_InBase.Dispose(); hv_Row_Origin_InBase.Dispose();
            HOperatorSet.AffineTransPoint2d(hv_HomMat2D_Img2Base, hv_Col_Origin, hv_Row_Origin,
                out hv_Col_Origin_InBase, out hv_Row_Origin_InBase);
            //在基准坐标系下对基准点进行偏移找拟合点
            hv_p_A_XInBase.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_p_A_XInBase = hv_Col_Origin_InBase + hv_p_A1_x_offest;
            }
            hv_p_A_YInBase.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_p_A_YInBase = (hv_Row_Origin_InBase - hv_p_A1_y_offest) - 20;
            }
            //计算基准坐标系到图像坐标系下的变换矩阵
            hv_HomMat2D_Base2Img.Dispose();
            HOperatorSet.HomMat2dInvert(hv_HomMat2D_Img2Base, out hv_HomMat2D_Base2Img);
            //将基准坐标系下的拟合点转换到图像坐标系下
            hv_p_A_x_fit.Dispose(); hv_p_A_y_fit.Dispose();
            HOperatorSet.AffineTransPoint2d(hv_HomMat2D_Base2Img, hv_p_A_XInBase, hv_p_A_YInBase,
                out hv_p_A_x_fit, out hv_p_A_y_fit);

            hv_HomMat2D_Img2Base.Dispose();
            hv_Col_Origin_InBase.Dispose();
            hv_Row_Origin_InBase.Dispose();
            hv_p_A_XInBase.Dispose();
            hv_p_A_YInBase.Dispose();
            hv_HomMat2D_Base2Img.Dispose();

            return;
        }

        //获取给定的4个点
        private static void my_gen_4point_offest(HObject ho_ImageZ, HTuple hv_HomMat2DIdentity,
            HTuple hv_p_E2_x_offest, HTuple hv_p_E2_y_offest, HTuple hv_Phi, HTuple hv_Col_Origin,
            HTuple hv_Row_Origin, HTuple hv_p_E5_x_offest, HTuple hv_p_E5_y_offest, HTuple hv_p_E10_x_offest,
            HTuple hv_p_E10_y_offest, HTuple hv_p_E13_x_offest, HTuple hv_p_E13_y_offest,
            HTuple hv_Z_Origin_Mean, out HTuple hv_E2_Z_mm, out HTuple hv_E5_Z_mm, out HTuple hv_E10_Z_mm,
            out HTuple hv_E13_Z_mm, out HTuple hv_p_E2_x_fit, out HTuple hv_p_E2_y_fit,
            out HTuple hv_p_E5_x_fit, out HTuple hv_p_E5_y_fit, out HTuple hv_p_E10_x_fit,
            out HTuple hv_p_E10_y_fit, out HTuple hv_p_E13_x_fit, out HTuple hv_p_E13_y_fit)
        {




            // Local iconic variables 

            // Local control variables 

            HTuple hv_E2_Z_um = new HTuple(), hv_E5_Z_um = new HTuple();
            HTuple hv_E10_Z_um = new HTuple(), hv_E13_Z_um = new HTuple();
            // Initialize local and output iconic variables 
            hv_E2_Z_mm = new HTuple();
            hv_E5_Z_mm = new HTuple();
            hv_E10_Z_mm = new HTuple();
            hv_E13_Z_mm = new HTuple();
            hv_p_E2_x_fit = new HTuple();
            hv_p_E2_y_fit = new HTuple();
            hv_p_E5_x_fit = new HTuple();
            hv_p_E5_y_fit = new HTuple();
            hv_p_E10_x_fit = new HTuple();
            hv_p_E10_y_fit = new HTuple();
            hv_p_E13_x_fit = new HTuple();
            hv_p_E13_y_fit = new HTuple();
            hv_p_E2_x_fit.Dispose(); hv_p_E2_y_fit.Dispose();
            my_gen_suffce_fit_point(hv_Row_Origin, hv_Col_Origin, hv_Phi, hv_p_E2_x_offest,
                hv_p_E2_y_offest, out hv_p_E2_x_fit, out hv_p_E2_y_fit);
            hv_p_E5_x_fit.Dispose(); hv_p_E5_y_fit.Dispose();
            my_gen_suffce_fit_point(hv_Row_Origin, hv_Col_Origin, hv_Phi, hv_p_E5_x_offest,
                hv_p_E5_y_offest, out hv_p_E5_x_fit, out hv_p_E5_y_fit);
            hv_p_E10_x_fit.Dispose(); hv_p_E10_y_fit.Dispose();
            my_gen_suffce_fit_point(hv_Row_Origin, hv_Col_Origin, hv_Phi, hv_p_E10_x_offest,
                hv_p_E10_y_offest, out hv_p_E10_x_fit, out hv_p_E10_y_fit);
            hv_p_E13_x_fit.Dispose(); hv_p_E13_y_fit.Dispose();
            my_gen_suffce_fit_point(hv_Row_Origin, hv_Col_Origin, hv_Phi, hv_p_E13_x_offest,
                hv_p_E13_y_offest, out hv_p_E13_x_fit, out hv_p_E13_y_fit);

            //计算四个底部点相对于Z向0平面的Z向偏差
            hv_E2_Z_um.Dispose();
            HOperatorSet.GetGrayval(ho_ImageZ, hv_p_E2_y_fit, hv_p_E2_x_fit, out hv_E2_Z_um);
            hv_E2_Z_mm.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_E2_Z_mm = (((hv_E2_Z_um - 32768) * 1.6) / 1000) - hv_Z_Origin_Mean;
            }

            hv_E5_Z_um.Dispose();
            HOperatorSet.GetGrayval(ho_ImageZ, hv_p_E5_y_fit, hv_p_E5_x_fit, out hv_E5_Z_um);
            hv_E5_Z_mm.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_E5_Z_mm = (((hv_E5_Z_um - 32768) * 1.6) / 1000) - hv_Z_Origin_Mean;
            }

            hv_E10_Z_um.Dispose();
            HOperatorSet.GetGrayval(ho_ImageZ, hv_p_E10_y_fit, hv_p_E10_x_fit, out hv_E10_Z_um);
            hv_E10_Z_mm.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_E10_Z_mm = (((hv_E10_Z_um - 32768) * 1.6) / 1000) - hv_Z_Origin_Mean;
            }

            hv_E13_Z_um.Dispose();
            HOperatorSet.GetGrayval(ho_ImageZ, hv_p_E13_y_fit, hv_p_E13_x_fit, out hv_E13_Z_um);
            hv_E13_Z_mm.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_E13_Z_mm = (((hv_E13_Z_um - 32768) * 1.6) / 1000) - hv_Z_Origin_Mean;
            }

            hv_E2_Z_um.Dispose();
            hv_E5_Z_um.Dispose();
            hv_E10_Z_um.Dispose();
            hv_E13_Z_um.Dispose();

            return;
        }

        //3D测量项中获取两圆信息
        public static void gen_TwoCircle_info_3D(HObject ho_Image, out HTuple hv_MaxCircleRow, out HTuple hv_MaxCircleColumn,
            out HTuple hv_MaxCircleRadius, out HTuple hv_MinCircleRow, out HTuple hv_MinCircleColumn,
            out HTuple hv_MinCircleRadius, out HTuple hv_TwoCirclePhi)
        {



            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];

            // Local iconic variables 

            HObject ho_Regions, ho_ConnectedRegions, ho_MaxCircleSelectedRegions;
            HObject ho_MinCircleSelectedRegions, ho_MinCircle, ho_MaxCircle;
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Regions);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_MaxCircleSelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_MinCircleSelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_MinCircle);
            HOperatorSet.GenEmptyObj(out ho_MaxCircle);
            hv_MaxCircleRow = new HTuple();
            hv_MaxCircleColumn = new HTuple();
            hv_MaxCircleRadius = new HTuple();
            hv_MinCircleRow = new HTuple();
            hv_MinCircleColumn = new HTuple();
            hv_MinCircleRadius = new HTuple();
            hv_TwoCirclePhi = new HTuple();
            ho_Regions.Dispose();
            HOperatorSet.Threshold(ho_Image, out ho_Regions, 65, 120);

            ho_ConnectedRegions.Dispose();
            HOperatorSet.Connection(ho_Regions, out ho_ConnectedRegions);
            //提取两个圆形区域
            //select_shape (ConnectedRegions, MaxCircleSelectedRegions, ['area','circularity'], 'and', [229831,0.4258], [689493,0.4722])


            ho_MaxCircleSelectedRegions.Dispose();
            HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_MaxCircleSelectedRegions,
                (new HTuple("area")).TupleConcat("circularity"), "and", (new HTuple(0)).TupleConcat(
                0.2505), (new HTuple(1.16792e+06)).TupleConcat(0.5));
            //select_shape (ConnectedRegions, MinCircleSelectedRegions, ['area','circularity'], 'and', [0,0.1308], [1.16792e+06,0.3534])
            {
                HObject ExpTmpOutVar_0;
                HOperatorSet.SelectShape(ho_MaxCircleSelectedRegions, out ExpTmpOutVar_0, "area",
                    "and", 255566, 500000);
                ho_MaxCircleSelectedRegions.Dispose();
                ho_MaxCircleSelectedRegions = ExpTmpOutVar_0;
            }

            ho_MinCircleSelectedRegions.Dispose();
            HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_MinCircleSelectedRegions,
                (new HTuple("area")).TupleConcat("circularity"), "and", (new HTuple(136023)).TupleConcat(
                0.3987), (new HTuple(267355)).TupleConcat(0.6407));



            //select_shape (ConnectedRegions, MaxCircleSelectedRegions, ['area','circularity'], 'and', [217996,0.7004], [1.24768e+06,0.7301])
            //select_shape (ConnectedRegions, MinCircleSelectedRegions, ['area','circularity'], 'and', [217996,0.7579], [1.24768e+06,0.8006])

            hv_MaxCircleRow.Dispose(); hv_MaxCircleColumn.Dispose(); hv_MaxCircleRadius.Dispose();
            HOperatorSet.SmallestCircle(ho_MaxCircleSelectedRegions, out hv_MaxCircleRow,
                out hv_MaxCircleColumn, out hv_MaxCircleRadius);
            hv_MinCircleRow.Dispose(); hv_MinCircleColumn.Dispose(); hv_MinCircleRadius.Dispose();
            HOperatorSet.SmallestCircle(ho_MinCircleSelectedRegions, out hv_MinCircleRow,
                out hv_MinCircleColumn, out hv_MinCircleRadius);

            ho_MinCircle.Dispose();
            HOperatorSet.GenCircleContourXld(out ho_MinCircle, hv_MinCircleRow, hv_MinCircleColumn,
                hv_MinCircleRadius, 0, 6.28318, "positive", 1);
            ho_MaxCircle.Dispose();
            HOperatorSet.GenCircleContourXld(out ho_MaxCircle, hv_MaxCircleRow, hv_MaxCircleColumn,
                hv_MaxCircleRadius, 0, 6.28318, "positive", 1);


            //求两圆心连线方向
            hv_TwoCirclePhi.Dispose();
            HOperatorSet.LineOrientation(hv_MaxCircleRow, hv_MaxCircleColumn, hv_MinCircleRow,
                hv_MinCircleColumn, out hv_TwoCirclePhi);



            //threshold_sub_pix (Image, Border, 1)
            //select_shape_xld (Border, MinCircleContour, 'area', 'and', 463303, 766055)
            //fit_circle_contour_xld (MinCircleContour, 'algebraic', -1, 0, 0, 3, 2, MinCircleRow, MinCircleColumn, MinCircleRadius, StartPhi, EndPhi, PointOrder)
            //select_shape_xld (Border, MaxCircleContour, 'area', 'and', 738532, 1.03211e+06)
            //fit_circle_contour_xld (MaxCircleContour, 'algebraic', -1, 0, 0, 3, 2, MaxCircleRow, MaxCircleColumn, MaxCircleRadius, StartPhi, EndPhi, PointOrder)
            //line_orientation (MaxCircleRow, MaxCircleColumn, MinCircleRow, MinCircleColumn, TwoCirclePhi)

            ho_Regions.Dispose();
            ho_ConnectedRegions.Dispose();
            ho_MaxCircleSelectedRegions.Dispose();
            ho_MinCircleSelectedRegions.Dispose();
            ho_MinCircle.Dispose();
            ho_MaxCircle.Dispose();


            return;
        }

        //3D测量项中建立坐标系
        private static void my_create_coordinate(HTuple hv_HomMat2DIdentity, HTuple hv_Col_Origin,
    HTuple hv_Row_Origin, HTuple hv_ColEnd_X, HTuple hv_RowEnd_X, out HTuple hv_Phi,
    out HTuple hv_ColEnd_Y, out HTuple hv_RowEnd_Y)
        {



            // Local control variables 

            HTuple hv_HomMat2DRotate = new HTuple();
            // Initialize local and output iconic variables 
            hv_Phi = new HTuple();
            hv_ColEnd_Y = new HTuple();
            hv_RowEnd_Y = new HTuple();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_HomMat2DRotate.Dispose();
                HOperatorSet.HomMat2dRotate(hv_HomMat2DIdentity, (new HTuple(-90)).TupleRad()
                    , hv_Col_Origin, hv_Row_Origin, out hv_HomMat2DRotate);
            }
            hv_ColEnd_Y.Dispose(); hv_RowEnd_Y.Dispose();
            HOperatorSet.AffineTransPoint2d(hv_HomMat2DRotate, hv_ColEnd_X, hv_RowEnd_X,
                out hv_ColEnd_Y, out hv_RowEnd_Y);
            hv_Phi.Dispose();
            HOperatorSet.LineOrientation(hv_Row_Origin, hv_Col_Origin, hv_RowEnd_X, hv_ColEnd_X,
                out hv_Phi);

            hv_HomMat2DRotate.Dispose();

            return;
        }

        //获取拟合点
        private static void gen_FitPoint(HObject ho_Image, out HObject ho_Circle1, HTuple hv_Origin_Row_InImg,
    HTuple hv_Origin_Column_InImg, HTuple hv_TwoCirclePhi, HTuple hv_p_XOffest,
    HTuple hv_p_YOffest, HTuple hv_MaxCircleColumn, HTuple hv_MinCircleColumn, HTuple hv_MaxCircleRow,
    HTuple hv_Width, HTuple hv_Height, out HTuple hv_p_x, out HTuple hv_p_y, out HTuple hv_p_x_fit,
    out HTuple hv_p_y_fit)
        {
            HTuple hv_HomMat2D_Img2Base = new HTuple();
            HTuple hv_Origin_Column_InBase = new HTuple(), hv_Origin_Row_InBase = new HTuple();
            HTuple hv_p_XInBase = new HTuple(), hv_p_YInBase = new HTuple();
            HTuple hv_HomMat2D_Base2Img = new HTuple(), hv_MeasureRectPhi1 = new HTuple();
            HTuple hv_MeasureHandle1 = new HTuple(), hv_Amplitude = new HTuple();
            HTuple hv_Distance = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Circle1);
            hv_p_x = new HTuple();
            hv_p_y = new HTuple();
            hv_p_x_fit = new HTuple();
            hv_p_y_fit = new HTuple();
            //计算图像坐标系到基准坐标系的变换矩阵
            hv_HomMat2D_Img2Base.Dispose();
            HOperatorSet.VectorAngleToRigid(0, 0, 0, hv_Origin_Row_InImg, hv_Origin_Column_InImg,
                hv_TwoCirclePhi, out hv_HomMat2D_Img2Base);
            //将图像坐标系下的基准点转换到基准坐标系下
            hv_Origin_Column_InBase.Dispose(); hv_Origin_Row_InBase.Dispose();
            HOperatorSet.AffineTransPoint2d(hv_HomMat2D_Img2Base, hv_Origin_Column_InImg,
                hv_Origin_Row_InImg, out hv_Origin_Column_InBase, out hv_Origin_Row_InBase);
            //在基准坐标系下对基准点进行偏移找拟合点
            if ((int)(new HTuple(hv_MaxCircleColumn.TupleLess(hv_MinCircleColumn))) != 0)
            {
                hv_p_XInBase.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_p_XInBase = hv_Origin_Column_InBase - hv_p_XOffest;
                }
                hv_p_YInBase.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_p_YInBase = hv_Origin_Row_InBase - hv_p_YOffest;
                }
            }
            else
            {
                hv_p_XInBase.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_p_XInBase = hv_Origin_Column_InBase + hv_p_XOffest;
                }
                hv_p_YInBase.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_p_YInBase = hv_Origin_Row_InBase + hv_p_YOffest;
                }
            }

            //计算基准坐标系到图像坐标系下的变换矩阵
            hv_HomMat2D_Base2Img.Dispose();
            HOperatorSet.HomMat2dInvert(hv_HomMat2D_Img2Base, out hv_HomMat2D_Base2Img);
            //将基准坐标系下的拟合点转换到图像坐标系下
            hv_p_x_fit.Dispose(); hv_p_y_fit.Dispose();
            HOperatorSet.AffineTransPoint2d(hv_HomMat2D_Base2Img, hv_p_XInBase, hv_p_YInBase,
                out hv_p_x_fit, out hv_p_y_fit);
            
            hv_MeasureRectPhi1.Dispose();
            HOperatorSet.LineOrientation(hv_MaxCircleRow, hv_MaxCircleColumn, hv_p_y_fit,
                hv_p_x_fit, out hv_MeasureRectPhi1);
            hv_MeasureHandle1.Dispose();
            HOperatorSet.GenMeasureRectangle2(hv_p_y_fit, hv_p_x_fit, hv_MeasureRectPhi1,
                30, 2, hv_Width, hv_Height, "bicubic", out hv_MeasureHandle1);
            hv_p_y.Dispose(); hv_p_x.Dispose(); hv_Amplitude.Dispose(); hv_Distance.Dispose();
            HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle1, 1.5, 50, "all", "all", out hv_p_y,
                out hv_p_x, out hv_Amplitude, out hv_Distance);
            HOperatorSet.CloseMeasure(hv_MeasureHandle1);
            
            ho_Circle1.Dispose();
            HOperatorSet.GenCircle(out ho_Circle1, hv_p_y, hv_p_x, 10);

            hv_HomMat2D_Img2Base.Dispose();
            hv_Origin_Column_InBase.Dispose();
            hv_Origin_Row_InBase.Dispose();
            hv_p_XInBase.Dispose();
            hv_p_YInBase.Dispose();
            hv_HomMat2D_Base2Img.Dispose();
            hv_MeasureRectPhi1.Dispose();
            hv_MeasureHandle1.Dispose();
            hv_Amplitude.Dispose();
            hv_Distance.Dispose();

            return;
        }

        //抓取底部边缘
        private static void gen_buttom_edge(HObject ho_Image, HTuple hv_CenterRow, HTuple hv_CenterCol,
    HTuple hv_BaseRectDistRow, HTuple hv_BaseRectDistCol, HTuple hv_BasePhi, HTuple hv_ImageWidth,
    HTuple hv_ImageHeight, HTuple hv_RotatePhi, HTuple hv_RectBasePhi, HTuple hv_MinCircleColumn,
    HTuple hv_MaxCircleColumn, out HTuple hv_ButtomEdgeRowBegin, out HTuple hv_ButtomEdgeColBegin,
    out HTuple hv_ButtomEdgeRowEnd, out HTuple hv_ButtomEdgeColEnd)
        {
            // Local iconic variables 
            HObject ho_Rectangle1, ho_Rectangle2, ho_RectButtomMeasureRectionAffineTrans1 = null;
            HObject ho_ButtomMeasureRectRegionAffineTrans2 = null, ho_Contours;
            HObject ho_ButtomEdgeContour;
            // Local control variables 
            HTuple hv_HomMat2D5 = new HTuple(), hv_RectButtomMeasureRectArea = new HTuple();
            HTuple hv_RectButtomMeasureRectRow1 = new HTuple(), hv_RectButtomMeasureRectColumn1 = new HTuple();
            HTuple hv_RectButtomMeasureRectPhi1 = new HTuple(), hv_MeasureHandle5 = new HTuple();
            HTuple hv_HomMat2D6 = new HTuple(), hv_ButtomMeasureRectArea = new HTuple();
            HTuple hv_ButtomMeasureRectRow2 = new HTuple(), hv_ButtomMeasureRectColumn2 = new HTuple();
            HTuple hv_ButtomMeasureRectPhi2 = new HTuple(), hv_MeasureHandle6 = new HTuple();
            HTuple hv_ButtomAmplitude = new HTuple(), hv_ButtomDistance = new HTuple();
            HTuple hv_ButtomMetrologyHandle = new HTuple(), hv_Index = new HTuple();
            HTuple hv_ButtomEdgeRows = new HTuple(), hv_ButtomEdgeColumns = new HTuple();
            HTuple hv_Nr = new HTuple(), hv_Nc = new HTuple(), hv_Dist = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Rectangle1);
            HOperatorSet.GenEmptyObj(out ho_Rectangle2);
            HOperatorSet.GenEmptyObj(out ho_RectButtomMeasureRectionAffineTrans1);
            HOperatorSet.GenEmptyObj(out ho_ButtomMeasureRectRegionAffineTrans2);
            HOperatorSet.GenEmptyObj(out ho_Contours);
            HOperatorSet.GenEmptyObj(out ho_ButtomEdgeContour);
            hv_ButtomEdgeRowBegin = new HTuple();
            hv_ButtomEdgeColBegin = new HTuple();
            hv_ButtomEdgeRowEnd = new HTuple();
            hv_ButtomEdgeColEnd = new HTuple();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                ho_Rectangle1.Dispose();
                HOperatorSet.GenRectangle2(out ho_Rectangle1, hv_CenterRow + hv_BaseRectDistRow,
                    hv_CenterCol - hv_BaseRectDistCol, hv_RectBasePhi, 70, 50);
            }
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                ho_Rectangle2.Dispose();
                HOperatorSet.GenRectangle2(out ho_Rectangle2, hv_CenterRow + hv_BaseRectDistRow,
                    hv_CenterCol + hv_BaseRectDistCol, hv_RectBasePhi, 70, 50);
            }

            if ((int)(new HTuple(hv_MaxCircleColumn.TupleGreater(hv_MinCircleColumn))) != 0)
            {
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_HomMat2D5.Dispose();
                    HOperatorSet.VectorAngleToRigid(hv_CenterRow, hv_CenterCol, -hv_RectBasePhi,
                        hv_CenterRow, hv_CenterCol, hv_BasePhi + hv_RotatePhi, out hv_HomMat2D5);
                }
                ho_RectButtomMeasureRectionAffineTrans1.Dispose();
                HOperatorSet.AffineTransRegion(ho_Rectangle1, out ho_RectButtomMeasureRectionAffineTrans1,
                    hv_HomMat2D5, "nearest_neighbor");
                hv_RectButtomMeasureRectArea.Dispose(); hv_RectButtomMeasureRectRow1.Dispose(); hv_RectButtomMeasureRectColumn1.Dispose();
                HOperatorSet.AreaCenter(ho_RectButtomMeasureRectionAffineTrans1, out hv_RectButtomMeasureRectArea,
                    out hv_RectButtomMeasureRectRow1, out hv_RectButtomMeasureRectColumn1);
                hv_RectButtomMeasureRectPhi1.Dispose();
                HOperatorSet.OrientationRegion(ho_RectButtomMeasureRectionAffineTrans1, out hv_RectButtomMeasureRectPhi1);
                hv_MeasureHandle5.Dispose();
                HOperatorSet.GenMeasureRectangle2(hv_RectButtomMeasureRectRow1, hv_RectButtomMeasureRectColumn1,
                    hv_RectButtomMeasureRectPhi1, 70, 2, hv_ImageWidth, hv_ImageHeight, "bicubic",
                    out hv_MeasureHandle5);

                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_HomMat2D6.Dispose();
                    HOperatorSet.VectorAngleToRigid(hv_CenterRow, hv_CenterCol, -hv_RectBasePhi,
                        hv_CenterRow, hv_CenterCol, hv_BasePhi + hv_RotatePhi, out hv_HomMat2D6);
                }
                ho_ButtomMeasureRectRegionAffineTrans2.Dispose();
                HOperatorSet.AffineTransRegion(ho_Rectangle2, out ho_ButtomMeasureRectRegionAffineTrans2,
                    hv_HomMat2D6, "nearest_neighbor");
                hv_ButtomMeasureRectArea.Dispose(); hv_ButtomMeasureRectRow2.Dispose(); hv_ButtomMeasureRectColumn2.Dispose();
                HOperatorSet.AreaCenter(ho_ButtomMeasureRectRegionAffineTrans2, out hv_ButtomMeasureRectArea,
                    out hv_ButtomMeasureRectRow2, out hv_ButtomMeasureRectColumn2);
                hv_ButtomMeasureRectPhi2.Dispose();
                HOperatorSet.OrientationRegion(ho_ButtomMeasureRectRegionAffineTrans2, out hv_ButtomMeasureRectPhi2);
                hv_MeasureHandle6.Dispose();
                HOperatorSet.GenMeasureRectangle2(hv_ButtomMeasureRectRow2, hv_ButtomMeasureRectColumn2,
                    hv_ButtomMeasureRectPhi2, 70, 2, hv_ImageWidth, hv_ImageHeight, "bicubic",
                    out hv_MeasureHandle6);
            }
            else
            {
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_HomMat2D5.Dispose();
                    HOperatorSet.VectorAngleToRigid(hv_CenterRow, hv_CenterCol, -hv_RectBasePhi,
                        hv_CenterRow, hv_CenterCol, hv_BasePhi - hv_RotatePhi, out hv_HomMat2D5);
                }
                ho_RectButtomMeasureRectionAffineTrans1.Dispose();
                HOperatorSet.AffineTransRegion(ho_Rectangle1, out ho_RectButtomMeasureRectionAffineTrans1,
                    hv_HomMat2D5, "nearest_neighbor");
                hv_RectButtomMeasureRectArea.Dispose(); hv_RectButtomMeasureRectRow1.Dispose(); hv_RectButtomMeasureRectColumn1.Dispose();
                HOperatorSet.AreaCenter(ho_RectButtomMeasureRectionAffineTrans1, out hv_RectButtomMeasureRectArea,
                    out hv_RectButtomMeasureRectRow1, out hv_RectButtomMeasureRectColumn1);
                hv_RectButtomMeasureRectPhi1.Dispose();
                HOperatorSet.OrientationRegion(ho_RectButtomMeasureRectionAffineTrans1, out hv_RectButtomMeasureRectPhi1);
                hv_MeasureHandle5.Dispose();
                HOperatorSet.GenMeasureRectangle2(hv_RectButtomMeasureRectRow1, hv_RectButtomMeasureRectColumn1,
                    hv_RectButtomMeasureRectPhi1, 70, 2, hv_ImageWidth, hv_ImageHeight, "bicubic",
                    out hv_MeasureHandle5);

                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_HomMat2D6.Dispose();
                    HOperatorSet.VectorAngleToRigid(hv_CenterRow, hv_CenterCol, -hv_RectBasePhi,
                        hv_CenterRow, hv_CenterCol, hv_BasePhi - hv_RotatePhi, out hv_HomMat2D6);
                }
                ho_ButtomMeasureRectRegionAffineTrans2.Dispose();
                HOperatorSet.AffineTransRegion(ho_Rectangle2, out ho_ButtomMeasureRectRegionAffineTrans2,
                    hv_HomMat2D6, "nearest_neighbor");
                hv_ButtomMeasureRectArea.Dispose(); hv_ButtomMeasureRectRow2.Dispose(); hv_ButtomMeasureRectColumn2.Dispose();
                HOperatorSet.AreaCenter(ho_ButtomMeasureRectRegionAffineTrans2, out hv_ButtomMeasureRectArea,
                    out hv_ButtomMeasureRectRow2, out hv_ButtomMeasureRectColumn2);
                hv_ButtomMeasureRectPhi2.Dispose();
                HOperatorSet.OrientationRegion(ho_ButtomMeasureRectRegionAffineTrans2, out hv_ButtomMeasureRectPhi2);
                hv_MeasureHandle6.Dispose();
                HOperatorSet.GenMeasureRectangle2(hv_ButtomMeasureRectRow2, hv_ButtomMeasureRectColumn2,
                    hv_ButtomMeasureRectPhi2, 70, 2, hv_ImageWidth, hv_ImageHeight, "bicubic",
                    out hv_MeasureHandle6);

            }
            //抓取底部边缘
            hv_ButtomEdgeRowBegin.Dispose(); hv_ButtomEdgeColBegin.Dispose(); hv_ButtomAmplitude.Dispose(); hv_ButtomDistance.Dispose();
            HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle5, 5, 50, "all", "all", out hv_ButtomEdgeRowBegin,
                out hv_ButtomEdgeColBegin, out hv_ButtomAmplitude, out hv_ButtomDistance);
            hv_ButtomEdgeRowEnd.Dispose(); hv_ButtomEdgeColEnd.Dispose(); hv_ButtomAmplitude.Dispose(); hv_ButtomDistance.Dispose();
            HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle6, 5, 50, "all", "all", out hv_ButtomEdgeRowEnd,
                out hv_ButtomEdgeColEnd, out hv_ButtomAmplitude, out hv_ButtomDistance);

            HOperatorSet.CloseMeasure(hv_MeasureHandle5);
            HOperatorSet.CloseMeasure(hv_MeasureHandle6);

            //拟合底部边缘
            hv_ButtomMetrologyHandle.Dispose();
            HOperatorSet.CreateMetrologyModel(out hv_ButtomMetrologyHandle);
            hv_Index.Dispose();
            HOperatorSet.AddMetrologyObjectLineMeasure(hv_ButtomMetrologyHandle, hv_ButtomEdgeRowBegin,
                hv_ButtomEdgeColBegin, hv_ButtomEdgeRowEnd, hv_ButtomEdgeColEnd, 100, 100,
                2, 50, new HTuple(), new HTuple(), out hv_Index);
            HOperatorSet.ApplyMetrologyModel(ho_Image, hv_ButtomMetrologyHandle);
            ho_Contours.Dispose(); hv_ButtomEdgeRows.Dispose(); hv_ButtomEdgeColumns.Dispose();
            HOperatorSet.GetMetrologyObjectMeasures(out ho_Contours, hv_ButtomMetrologyHandle,
                "all", "all", out hv_ButtomEdgeRows, out hv_ButtomEdgeColumns);
            ho_ButtomEdgeContour.Dispose();
            HOperatorSet.GenContourPolygonXld(out ho_ButtomEdgeContour, hv_ButtomEdgeRows,
                hv_ButtomEdgeColumns);
            hv_ButtomEdgeRowBegin.Dispose(); hv_ButtomEdgeColBegin.Dispose(); hv_ButtomEdgeRowEnd.Dispose(); hv_ButtomEdgeColEnd.Dispose(); hv_Nr.Dispose(); hv_Nc.Dispose(); hv_Dist.Dispose();
            HOperatorSet.FitLineContourXld(ho_ButtomEdgeContour, "huber", -1, 0, 5, 4, out hv_ButtomEdgeRowBegin,
                out hv_ButtomEdgeColBegin, out hv_ButtomEdgeRowEnd, out hv_ButtomEdgeColEnd,
                out hv_Nr, out hv_Nc, out hv_Dist);

            ho_Rectangle1.Dispose();
            ho_Rectangle2.Dispose();
            ho_RectButtomMeasureRectionAffineTrans1.Dispose();
            ho_ButtomMeasureRectRegionAffineTrans2.Dispose();
            ho_Contours.Dispose();
            ho_ButtomEdgeContour.Dispose();

            hv_HomMat2D5.Dispose();
            hv_RectButtomMeasureRectArea.Dispose();
            hv_RectButtomMeasureRectRow1.Dispose();
            hv_RectButtomMeasureRectColumn1.Dispose();
            hv_RectButtomMeasureRectPhi1.Dispose();
            hv_MeasureHandle5.Dispose();
            hv_HomMat2D6.Dispose();
            hv_ButtomMeasureRectArea.Dispose();
            hv_ButtomMeasureRectRow2.Dispose();
            hv_ButtomMeasureRectColumn2.Dispose();
            hv_ButtomMeasureRectPhi2.Dispose();
            hv_MeasureHandle6.Dispose();
            hv_ButtomAmplitude.Dispose();
            hv_ButtomDistance.Dispose();
            hv_ButtomMetrologyHandle.Dispose();
            hv_Index.Dispose();
            hv_ButtomEdgeRows.Dispose();
            hv_ButtomEdgeColumns.Dispose();
            hv_Nr.Dispose();
            hv_Nc.Dispose();
            hv_Dist.Dispose();

            return;
        }

        //抓取右侧边缘
        private static void gen_right_edge(HObject ho_Image, HTuple hv_CenterRow, HTuple hv_CenterColumn,
    HTuple hv_BaseRectDistRow, HTuple hv_BaseRectDistColumn, HTuple hv_MaxCircleColumn,
    HTuple hv_BasePhi, HTuple hv_ImageWidth, HTuple hv_ImageHeight, HTuple hv_RotatePhi,
    HTuple hv_BaseRectPhi, out HTuple hv_RightEdgeRowBegin, out HTuple hv_RightEdgeColBegin,
    out HTuple hv_RightEdgeRowEnd, out HTuple hv_RightEdgeColEnd)
        {
            HObject ho_Rectangle1, ho_Rectangle2, ho_RightMeasureRectionAffineTrans1 = null;
            HObject ho_RightMeasureRectionAffineTrans2 = null, ho_Contours;
            HObject ho_RightEdgeContour;
            HTuple hv_HomMat2D7 = new HTuple(), hv_RightMeasureRectArea = new HTuple();
            HTuple hv_RightMeasureRectRow1 = new HTuple(), hv_RightMeasureRectColumn1 = new HTuple();
            HTuple hv_RightMeasureRectPhi1 = new HTuple(), hv_MeasureHandle7 = new HTuple();
            HTuple hv_RightMeasureRectRow2 = new HTuple(), hv_RightMeasureRectColumn2 = new HTuple();
            HTuple hv_RightMeasureRectPhi2 = new HTuple(), hv_MeasureHandle8 = new HTuple();
            HTuple hv_RightAmplitude = new HTuple(), hv_RightDistance = new HTuple();
            HTuple hv_RightMetrologyHandle = new HTuple(), hv_Index = new HTuple();
            HTuple hv_RightEdgeRows = new HTuple(), hv_RightEdgeColumns = new HTuple();
            HTuple hv_Nr = new HTuple(), hv_Nc = new HTuple(), hv_Dist = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Rectangle1);
            HOperatorSet.GenEmptyObj(out ho_Rectangle2);
            HOperatorSet.GenEmptyObj(out ho_RightMeasureRectionAffineTrans1);
            HOperatorSet.GenEmptyObj(out ho_RightMeasureRectionAffineTrans2);
            HOperatorSet.GenEmptyObj(out ho_Contours);
            HOperatorSet.GenEmptyObj(out ho_RightEdgeContour);
            hv_RightEdgeRowBegin = new HTuple();
            hv_RightEdgeColBegin = new HTuple();
            hv_RightEdgeRowEnd = new HTuple();
            hv_RightEdgeColEnd = new HTuple();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                ho_Rectangle1.Dispose();
                HOperatorSet.GenRectangle2(out ho_Rectangle1, hv_CenterRow - hv_BaseRectDistRow,
                    hv_CenterColumn + hv_BaseRectDistColumn, hv_BaseRectPhi, 70, 50);
            }
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                ho_Rectangle2.Dispose();
                HOperatorSet.GenRectangle2(out ho_Rectangle2, hv_CenterRow + hv_BaseRectDistRow,
                    hv_CenterColumn + hv_BaseRectDistColumn, hv_BaseRectPhi, 70, 50);
            }

            if ((int)(new HTuple(hv_MaxCircleColumn.TupleGreater(hv_CenterColumn))) != 0)
            {
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_HomMat2D7.Dispose();
                    HOperatorSet.VectorAngleToRigid(hv_CenterRow, hv_CenterColumn, hv_BaseRectPhi,
                        hv_CenterRow, hv_CenterColumn, hv_BasePhi + ((new HTuple(180)).TupleRad()
                        ), out hv_HomMat2D7);
                }
                ho_RightMeasureRectionAffineTrans1.Dispose();
                HOperatorSet.AffineTransRegion(ho_Rectangle1, out ho_RightMeasureRectionAffineTrans1,
                    hv_HomMat2D7, "nearest_neighbor");
                hv_RightMeasureRectArea.Dispose(); hv_RightMeasureRectRow1.Dispose(); hv_RightMeasureRectColumn1.Dispose();
                HOperatorSet.AreaCenter(ho_RightMeasureRectionAffineTrans1, out hv_RightMeasureRectArea,
                    out hv_RightMeasureRectRow1, out hv_RightMeasureRectColumn1);
                hv_RightMeasureRectPhi1.Dispose();
                HOperatorSet.OrientationRegion(ho_RightMeasureRectionAffineTrans1, out hv_RightMeasureRectPhi1);
                hv_MeasureHandle7.Dispose();
                HOperatorSet.GenMeasureRectangle2(hv_RightMeasureRectRow1, hv_RightMeasureRectColumn1,
                    hv_RightMeasureRectPhi1, 70, 2, hv_ImageWidth, hv_ImageHeight, "bicubic",
                    out hv_MeasureHandle7);

                ho_RightMeasureRectionAffineTrans2.Dispose();
                HOperatorSet.AffineTransRegion(ho_Rectangle2, out ho_RightMeasureRectionAffineTrans2,
                    hv_HomMat2D7, "nearest_neighbor");
                hv_RightMeasureRectArea.Dispose(); hv_RightMeasureRectRow2.Dispose(); hv_RightMeasureRectColumn2.Dispose();
                HOperatorSet.AreaCenter(ho_RightMeasureRectionAffineTrans2, out hv_RightMeasureRectArea,
                    out hv_RightMeasureRectRow2, out hv_RightMeasureRectColumn2);
                hv_RightMeasureRectPhi2.Dispose();
                HOperatorSet.OrientationRegion(ho_RightMeasureRectionAffineTrans2, out hv_RightMeasureRectPhi2);
                hv_MeasureHandle8.Dispose();
                HOperatorSet.GenMeasureRectangle2(hv_RightMeasureRectRow2, hv_RightMeasureRectColumn2,
                    hv_RightMeasureRectPhi2, 70, 2, hv_ImageWidth, hv_ImageHeight, "bicubic",
                    out hv_MeasureHandle8);
            }
            else
            {
                hv_HomMat2D7.Dispose();
                HOperatorSet.VectorAngleToRigid(hv_CenterRow, hv_CenterColumn, hv_BaseRectPhi,
                    hv_CenterRow, hv_CenterColumn, hv_BasePhi, out hv_HomMat2D7);
                ho_RightMeasureRectionAffineTrans1.Dispose();
                HOperatorSet.AffineTransRegion(ho_Rectangle1, out ho_RightMeasureRectionAffineTrans1,
                    hv_HomMat2D7, "nearest_neighbor");
                hv_RightMeasureRectArea.Dispose(); hv_RightMeasureRectRow1.Dispose(); hv_RightMeasureRectColumn1.Dispose();
                HOperatorSet.AreaCenter(ho_RightMeasureRectionAffineTrans1, out hv_RightMeasureRectArea,
                    out hv_RightMeasureRectRow1, out hv_RightMeasureRectColumn1);
                hv_RightMeasureRectPhi1.Dispose();
                HOperatorSet.OrientationRegion(ho_RightMeasureRectionAffineTrans1, out hv_RightMeasureRectPhi1);
                hv_MeasureHandle7.Dispose();
                HOperatorSet.GenMeasureRectangle2(hv_RightMeasureRectRow1, hv_RightMeasureRectColumn1,
                    hv_RightMeasureRectPhi1, 70, 2, hv_ImageWidth, hv_ImageHeight, "bicubic",
                    out hv_MeasureHandle7);

                ho_RightMeasureRectionAffineTrans2.Dispose();
                HOperatorSet.AffineTransRegion(ho_Rectangle2, out ho_RightMeasureRectionAffineTrans2,
                    hv_HomMat2D7, "nearest_neighbor");
                hv_RightMeasureRectArea.Dispose(); hv_RightMeasureRectRow2.Dispose(); hv_RightMeasureRectColumn2.Dispose();
                HOperatorSet.AreaCenter(ho_RightMeasureRectionAffineTrans2, out hv_RightMeasureRectArea,
                    out hv_RightMeasureRectRow2, out hv_RightMeasureRectColumn2);
                hv_RightMeasureRectPhi2.Dispose();
                HOperatorSet.OrientationRegion(ho_RightMeasureRectionAffineTrans2, out hv_RightMeasureRectPhi2);
                hv_MeasureHandle8.Dispose();
                HOperatorSet.GenMeasureRectangle2(hv_RightMeasureRectRow2, hv_RightMeasureRectColumn2,
                    hv_RightMeasureRectPhi2, 70, 2, hv_ImageWidth, hv_ImageHeight, "bicubic",
                    out hv_MeasureHandle8);
            }
            //抓取右侧边缘
            hv_RightEdgeRowBegin.Dispose(); hv_RightEdgeColBegin.Dispose(); hv_RightAmplitude.Dispose(); hv_RightDistance.Dispose();
            HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle7, 5, 50, "all", "all", out hv_RightEdgeRowBegin,
                out hv_RightEdgeColBegin, out hv_RightAmplitude, out hv_RightDistance);
            hv_RightEdgeRowEnd.Dispose(); hv_RightEdgeColEnd.Dispose(); hv_RightAmplitude.Dispose(); hv_RightDistance.Dispose();
            HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle8, 5, 50, "all", "all", out hv_RightEdgeRowEnd,
                out hv_RightEdgeColEnd, out hv_RightAmplitude, out hv_RightDistance);

            HOperatorSet.CloseMeasure(hv_MeasureHandle7);
            HOperatorSet.CloseMeasure(hv_MeasureHandle8);

            //拟合右侧边缘
            hv_RightMetrologyHandle.Dispose();
            HOperatorSet.CreateMetrologyModel(out hv_RightMetrologyHandle);
            hv_Index.Dispose();
            HOperatorSet.AddMetrologyObjectLineMeasure(hv_RightMetrologyHandle, hv_RightEdgeRowBegin,
                hv_RightEdgeColBegin, hv_RightEdgeRowEnd, hv_RightEdgeColEnd, 100, 100, 2,
                50, new HTuple(), new HTuple(), out hv_Index);
            HOperatorSet.ApplyMetrologyModel(ho_Image, hv_RightMetrologyHandle);
            ho_Contours.Dispose(); hv_RightEdgeRows.Dispose(); hv_RightEdgeColumns.Dispose();
            HOperatorSet.GetMetrologyObjectMeasures(out ho_Contours, hv_RightMetrologyHandle,
                "all", "all", out hv_RightEdgeRows, out hv_RightEdgeColumns);
            ho_RightEdgeContour.Dispose();
            HOperatorSet.GenContourPolygonXld(out ho_RightEdgeContour, hv_RightEdgeRows,
                hv_RightEdgeColumns);
            hv_RightEdgeRowBegin.Dispose(); hv_RightEdgeColBegin.Dispose(); hv_RightEdgeRowEnd.Dispose(); hv_RightEdgeColEnd.Dispose(); hv_Nr.Dispose(); hv_Nc.Dispose(); hv_Dist.Dispose();
            HOperatorSet.FitLineContourXld(ho_RightEdgeContour, "huber", -1, 0, 5, 4, out hv_RightEdgeRowBegin,
                out hv_RightEdgeColBegin, out hv_RightEdgeRowEnd, out hv_RightEdgeColEnd,
                out hv_Nr, out hv_Nc, out hv_Dist);

            ho_Rectangle1.Dispose();
            ho_Rectangle2.Dispose();
            ho_RightMeasureRectionAffineTrans1.Dispose();
            ho_RightMeasureRectionAffineTrans2.Dispose();
            ho_Contours.Dispose();
            ho_RightEdgeContour.Dispose();

            hv_HomMat2D7.Dispose();
            hv_RightMeasureRectArea.Dispose();
            hv_RightMeasureRectRow1.Dispose();
            hv_RightMeasureRectColumn1.Dispose();
            hv_RightMeasureRectPhi1.Dispose();
            hv_MeasureHandle7.Dispose();
            hv_RightMeasureRectRow2.Dispose();
            hv_RightMeasureRectColumn2.Dispose();
            hv_RightMeasureRectPhi2.Dispose();
            hv_MeasureHandle8.Dispose();
            hv_RightAmplitude.Dispose();
            hv_RightDistance.Dispose();
            hv_RightMetrologyHandle.Dispose();
            hv_Index.Dispose();
            hv_RightEdgeRows.Dispose();
            hv_RightEdgeColumns.Dispose();
            hv_Nr.Dispose();
            hv_Nc.Dispose();
            hv_Dist.Dispose();

            return;
        }

        //获取两圆信息
        private static void gen_TwoCircle_info(HObject ho_Image, out HTuple hv_MaxCircleRow, out HTuple hv_MaxCircleColumn,
    out HTuple hv_MaxCircleRadius, out HTuple hv_MinCircleRow, out HTuple hv_MinCircleColumn,
    out HTuple hv_MinCircleRadius, out HTuple hv_TwoCirclePhi)
        {
            // Local iconic variables 
            HObject ho_Border, ho_MinCircleContour, ho_MaxCircleContour;
            // Local control variables 
            HTuple hv_StartPhi = new HTuple(), hv_EndPhi = new HTuple();
            HTuple hv_PointOrder = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Border);
            HOperatorSet.GenEmptyObj(out ho_MinCircleContour);
            HOperatorSet.GenEmptyObj(out ho_MaxCircleContour);
            hv_MaxCircleRow = new HTuple();
            hv_MaxCircleColumn = new HTuple();
            hv_MaxCircleRadius = new HTuple();
            hv_MinCircleRow = new HTuple();
            hv_MinCircleColumn = new HTuple();
            hv_MinCircleRadius = new HTuple();
            hv_TwoCirclePhi = new HTuple();

            ho_Border.Dispose();
            HOperatorSet.ThresholdSubPix(ho_Image, out ho_Border, 128);
            ho_MinCircleContour.Dispose();
            HOperatorSet.SelectShapeXld(ho_Border, out ho_MinCircleContour, "area", "and",
                463303, 766055);
            hv_MinCircleRow.Dispose(); hv_MinCircleColumn.Dispose(); hv_MinCircleRadius.Dispose(); hv_StartPhi.Dispose(); hv_EndPhi.Dispose(); hv_PointOrder.Dispose();
            HOperatorSet.FitCircleContourXld(ho_MinCircleContour, "algebraic", -1, 0, 0,
                3, 2, out hv_MinCircleRow, out hv_MinCircleColumn, out hv_MinCircleRadius,
                out hv_StartPhi, out hv_EndPhi, out hv_PointOrder);
            ho_MaxCircleContour.Dispose();
            HOperatorSet.SelectShapeXld(ho_Border, out ho_MaxCircleContour, "area", "and",
                738532, 1.03211e+06);
            hv_MaxCircleRow.Dispose(); hv_MaxCircleColumn.Dispose(); hv_MaxCircleRadius.Dispose(); hv_StartPhi.Dispose(); hv_EndPhi.Dispose(); hv_PointOrder.Dispose();
            HOperatorSet.FitCircleContourXld(ho_MaxCircleContour, "algebraic", -1, 0, 0,
                3, 2, out hv_MaxCircleRow, out hv_MaxCircleColumn, out hv_MaxCircleRadius,
                out hv_StartPhi, out hv_EndPhi, out hv_PointOrder);
            hv_TwoCirclePhi.Dispose();
            HOperatorSet.LineOrientation(hv_MaxCircleRow, hv_MaxCircleColumn, hv_MinCircleRow,
                hv_MinCircleColumn, out hv_TwoCirclePhi);

            ho_Border.Dispose();
            ho_MinCircleContour.Dispose();
            ho_MaxCircleContour.Dispose();

            hv_StartPhi.Dispose();
            hv_EndPhi.Dispose();
            hv_PointOrder.Dispose();

            return;
        }

        private static bool Measure_9(HObject ho_Image, out double CircleRadius, out double PositionDegree, out double RunTime,
    out double DistanceX1, out double DistanceY1)
        {
            CircleRadius = -1;
            PositionDegree = -1;
            RunTime = -1;
            DistanceX1 = -1;
            DistanceY1 = -1;
            // Local iconic variables 
            HObject ho_Circle1, ho_Circle2, ho_Circle3;
            HObject ho_Circle4, ho_EdgeContour, ho_EdgeCircle;
            // Local control variables 
            HTuple hv_PixclRealDis = new HTuple(), hv_p1_XOffest = new HTuple();
            HTuple hv_p1_YOffest = new HTuple(), hv_p2_XOffest = new HTuple();
            HTuple hv_p2_YOffest = new HTuple(), hv_p3_XOffest = new HTuple();
            HTuple hv_p3_YOffest = new HTuple(), hv_p4_XOffest = new HTuple();
            HTuple hv_p4_YOffest = new HTuple(), hv_Width = new HTuple();
            HTuple hv_Height = new HTuple(), hv_StartTime = new HTuple();
            HTuple hv_MaxCircleRow = new HTuple(), hv_MaxCircleColumn = new HTuple();
            HTuple hv_MaxCircleRadius = new HTuple(), hv_MinCircleRow = new HTuple();
            HTuple hv_MinCircleColumn = new HTuple(), hv_MinCircleRadius = new HTuple();
            HTuple hv_TwoCirclePhi = new HTuple(), hv_ButtomEdgeRowBegin = new HTuple();
            HTuple hv_ButtomEdgeColBegin = new HTuple(), hv_ButtomEdgeRowEnd = new HTuple();
            HTuple hv_ButtomEdgeColEnd = new HTuple(), hv_RightEdgeRowBegin = new HTuple();
            HTuple hv_RightEdgeColBegin = new HTuple(), hv_RightEdgeRowEnd = new HTuple();
            HTuple hv_RightEdgeColEnd = new HTuple(), hv_Origin_Row_InImg = new HTuple();
            HTuple hv_Origin_Column_InImg = new HTuple(), hv_IsOverlapping = new HTuple();
            HTuple hv_p1_x = new HTuple(), hv_p1_y = new HTuple();
            HTuple hv_p1_x_fit = new HTuple(), hv_p1_y_fit = new HTuple();
            HTuple hv_p2_x = new HTuple(), hv_p2_y = new HTuple();
            HTuple hv_p2_x_fit = new HTuple(), hv_p2_y_fit = new HTuple();
            HTuple hv_p3_x = new HTuple(), hv_p3_y = new HTuple();
            HTuple hv_p3_x_fit = new HTuple(), hv_p3_y_fit = new HTuple();
            HTuple hv_p4_x = new HTuple(), hv_p4_y = new HTuple();
            HTuple hv_p4_x_fit = new HTuple(), hv_p4_y_fit = new HTuple();
            HTuple hv_EdgeCircleCenterRow = new HTuple(), hv_EdgeCircleCenterCol = new HTuple();
            HTuple hv_EdgeCircleCenterRadius = new HTuple(), hv_StartPhi = new HTuple();
            HTuple hv_EndPhi = new HTuple(), hv_PointOrder = new HTuple();
            HTuple hv_X1 = new HTuple(), hv_Y1 = new HTuple(), hv_StopTime = new HTuple();
            HTuple hv_CircleRadiu = new HTuple(), hv_PositionDegree = new HTuple();
            HTuple hv_runtime = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Circle1);
            HOperatorSet.GenEmptyObj(out ho_Circle2);
            HOperatorSet.GenEmptyObj(out ho_Circle3);
            HOperatorSet.GenEmptyObj(out ho_Circle4);
            HOperatorSet.GenEmptyObj(out ho_EdgeContour);
            HOperatorSet.GenEmptyObj(out ho_EdgeCircle);

            hv_PixclRealDis = new HTuple();
            //每像素对应的实际距离（mm）
            hv_PixclRealDis.Dispose();
            hv_PixclRealDis = 0.00952380952;
            //获取拟合点相对位置的真实距离
            gen_pixel2real_distance(hv_PixclRealDis, out hv_p1_XOffest, out hv_p1_YOffest, out hv_p2_XOffest, out hv_p2_YOffest,
                out hv_p3_XOffest, out hv_p3_YOffest, out hv_p4_XOffest, out hv_p4_YOffest);

            //获取图像尺寸
            hv_Width.Dispose(); hv_Height.Dispose();
            HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
            hv_StartTime.Dispose();
            HOperatorSet.CountSeconds(out hv_StartTime);
            try
            {
                //定位两圆心
                hv_MaxCircleRow.Dispose(); hv_MaxCircleColumn.Dispose(); hv_MaxCircleRadius.Dispose(); hv_MinCircleRow.Dispose(); hv_MinCircleColumn.Dispose(); hv_MinCircleRadius.Dispose(); hv_TwoCirclePhi.Dispose();
                gen_TwoCircle_info(ho_Image, out hv_MaxCircleRow, out hv_MaxCircleColumn, out hv_MaxCircleRadius,
                    out hv_MinCircleRow, out hv_MinCircleColumn, out hv_MinCircleRadius, out hv_TwoCirclePhi);
                //获取底部边缘
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_ButtomEdgeRowBegin.Dispose(); hv_ButtomEdgeColBegin.Dispose(); hv_ButtomEdgeRowEnd.Dispose(); hv_ButtomEdgeColEnd.Dispose();
                    gen_buttom_edge(ho_Image, hv_MaxCircleRow, hv_MaxCircleColumn, (hv_MaxCircleRadius * 4) / 3,
                        (hv_MaxCircleRadius * 3) / 4, hv_TwoCirclePhi, hv_Width, hv_Height, (new HTuple(90)).TupleRad()
                        , (new HTuple(90)).TupleRad(), hv_MinCircleColumn, hv_MaxCircleColumn, out hv_ButtomEdgeRowBegin,
                        out hv_ButtomEdgeColBegin, out hv_ButtomEdgeRowEnd, out hv_ButtomEdgeColEnd);
                }
                //获取右侧边缘
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_RightEdgeRowBegin.Dispose(); hv_RightEdgeColBegin.Dispose(); hv_RightEdgeRowEnd.Dispose(); hv_RightEdgeColEnd.Dispose();
                    gen_right_edge(ho_Image, hv_MinCircleRow, hv_MinCircleColumn, (hv_MinCircleRadius * 7) / 7,
                        (hv_MinCircleRadius * 9) / 7, hv_MaxCircleColumn, hv_TwoCirclePhi, hv_Width,
                        hv_Height, (new HTuple(180)).TupleRad(), 0, out hv_RightEdgeRowBegin, out hv_RightEdgeColBegin,
                        out hv_RightEdgeRowEnd, out hv_RightEdgeColEnd);
                }
                //求底部边缘和右侧边缘交点，即坐标原点
                hv_Origin_Row_InImg.Dispose(); hv_Origin_Column_InImg.Dispose(); hv_IsOverlapping.Dispose();
                HOperatorSet.IntersectionLines(hv_ButtomEdgeRowBegin, hv_ButtomEdgeColBegin,
                    hv_ButtomEdgeRowEnd, hv_ButtomEdgeColEnd, hv_RightEdgeRowBegin, hv_RightEdgeColBegin,
                    hv_RightEdgeRowEnd, hv_RightEdgeColEnd, out hv_Origin_Row_InImg, out hv_Origin_Column_InImg,
                    out hv_IsOverlapping);
                //求取4个拟合点
                ho_Circle1.Dispose(); hv_p1_x.Dispose(); hv_p1_y.Dispose(); hv_p1_x_fit.Dispose(); hv_p1_y_fit.Dispose();
                gen_FitPoint(ho_Image, out ho_Circle1, hv_Origin_Row_InImg, hv_Origin_Column_InImg,
                    hv_TwoCirclePhi, hv_p1_XOffest, hv_p1_YOffest, hv_MaxCircleColumn, hv_MinCircleColumn,
                    hv_MaxCircleRow, hv_Width, hv_Height, out hv_p1_x, out hv_p1_y, out hv_p1_x_fit,
                    out hv_p1_y_fit);
                ho_Circle2.Dispose(); hv_p2_x.Dispose(); hv_p2_y.Dispose(); hv_p2_x_fit.Dispose(); hv_p2_y_fit.Dispose();
                gen_FitPoint(ho_Image, out ho_Circle2, hv_Origin_Row_InImg, hv_Origin_Column_InImg,
                    hv_TwoCirclePhi, hv_p2_XOffest, hv_p2_YOffest, hv_MaxCircleColumn, hv_MinCircleColumn,
                    hv_MaxCircleRow, hv_Width, hv_Height, out hv_p2_x, out hv_p2_y, out hv_p2_x_fit,
                    out hv_p2_y_fit);
                ho_Circle3.Dispose(); hv_p3_x.Dispose(); hv_p3_y.Dispose(); hv_p3_x_fit.Dispose(); hv_p3_y_fit.Dispose();
                gen_FitPoint(ho_Image, out ho_Circle3, hv_Origin_Row_InImg, hv_Origin_Column_InImg,
                    hv_TwoCirclePhi, hv_p3_XOffest, hv_p3_YOffest, hv_MaxCircleColumn, hv_MinCircleColumn,
                    hv_MaxCircleRow, hv_Width, hv_Height, out hv_p3_x, out hv_p3_y, out hv_p3_x_fit,
                    out hv_p3_y_fit);
                ho_Circle4.Dispose(); hv_p4_x.Dispose(); hv_p4_y.Dispose(); hv_p4_x_fit.Dispose(); hv_p4_y_fit.Dispose();
                gen_FitPoint(ho_Image, out ho_Circle4, hv_Origin_Row_InImg, hv_Origin_Column_InImg,
                    hv_TwoCirclePhi, hv_p4_XOffest, hv_p4_YOffest, hv_MaxCircleColumn, hv_MinCircleColumn,
                    hv_MaxCircleRow, hv_Width, hv_Height, out hv_p4_x, out hv_p4_y, out hv_p4_x_fit,
                    out hv_p4_y_fit);
                //利用4个边缘点生成点多边形轮廓，并拟合成圆
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    ho_EdgeContour.Dispose();
                    HOperatorSet.GenContourPolygonXld(out ho_EdgeContour, ((((hv_p1_y.TupleConcat(
                        hv_p2_y))).TupleConcat(hv_p3_y))).TupleConcat(hv_p4_y), ((((hv_p1_x.TupleConcat(
                        hv_p2_x))).TupleConcat(hv_p3_x))).TupleConcat(hv_p4_x));
                }
                hv_EdgeCircleCenterRow.Dispose(); hv_EdgeCircleCenterCol.Dispose(); hv_EdgeCircleCenterRadius.Dispose(); hv_StartPhi.Dispose(); hv_EndPhi.Dispose(); hv_PointOrder.Dispose();
                HOperatorSet.FitCircleContourXld(ho_EdgeContour, "ahuber", -1, 0, 0, 3, 1, out hv_EdgeCircleCenterRow,
                    out hv_EdgeCircleCenterCol, out hv_EdgeCircleCenterRadius, out hv_StartPhi,
                    out hv_EndPhi, out hv_PointOrder);
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    ho_EdgeCircle.Dispose();
                    HOperatorSet.GenCircleContourXld(out ho_EdgeCircle, hv_EdgeCircleCenterRow, hv_EdgeCircleCenterCol,
                        hv_EdgeCircleCenterRadius, 0, (new HTuple(360)).TupleRad(), "positive", 1);
                }
                //计算X1
                hv_X1.Dispose();
                HOperatorSet.DistancePl(hv_EdgeCircleCenterRow, hv_EdgeCircleCenterCol, hv_RightEdgeRowBegin,
                    hv_RightEdgeColBegin, hv_RightEdgeRowEnd, hv_RightEdgeColEnd, out hv_X1);
                //计算Y1
                hv_Y1.Dispose();
                HOperatorSet.DistancePl(hv_EdgeCircleCenterRow, hv_EdgeCircleCenterCol, hv_ButtomEdgeRowBegin,
                    hv_ButtomEdgeColBegin, hv_ButtomEdgeRowEnd, hv_ButtomEdgeColEnd, out hv_Y1);
                //计算运行时间
                hv_StopTime.Dispose();
                HOperatorSet.CountSeconds(out hv_StopTime);
                //输出结果
                hv_CircleRadiu.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    CircleRadius = (hv_EdgeCircleCenterRadius * 2) * hv_PixclRealDis - 0.05631;
                }
                hv_PositionDegree.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    PositionDegree = 2 * (((((((hv_X1 * hv_PixclRealDis) - 19.605)).TuplePow(
                        2)) + ((((hv_Y1 * hv_PixclRealDis) - 6.788)).TuplePow(2)))).TupleSqrt()) - 0.065-0.0327;
                }
                hv_runtime.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    RunTime = (hv_StopTime - hv_StartTime) * 1000;
                }
                DistanceX1 = hv_X1 * hv_PixclRealDis;
                DistanceY1 = hv_Y1 * hv_PixclRealDis;

                DispImage(ho_Image);

                //不同颜色显示图像
                if (PositionDegree < 0 || PositionDegree > 0.06 || CircleRadius < 9.44 || CircleRadius > 9.50)
                {
                    outWindow.SetColor("red");
                    outWindow.SetLineWidth(2);
                    outWindow.SetDraw("margin");
                    outWindow.DispCircle(hv_EdgeCircleCenterRow, hv_EdgeCircleCenterCol, hv_EdgeCircleCenterRadius);
                    outWindow.DispLine(hv_ButtomEdgeRowBegin, hv_ButtomEdgeColBegin, hv_ButtomEdgeRowEnd, hv_ButtomEdgeColEnd);
                    outWindow.DispLine(hv_RightEdgeRowBegin, hv_RightEdgeColBegin, hv_RightEdgeRowEnd, hv_RightEdgeColEnd);

                    double RightCenterRow = (hv_RightEdgeRowBegin + hv_RightEdgeRowEnd) / 2;
                    double RightCenterCol = (hv_RightEdgeColBegin + hv_RightEdgeColEnd) / 2;
                    double ButtomCenterRow = (hv_ButtomEdgeRowBegin + hv_ButtomEdgeRowEnd) / 2;
                    double ButtomCenterCol = (hv_ButtomEdgeColBegin + hv_ButtomEdgeColEnd) / 2;
                    outWindow.DispArrow(hv_EdgeCircleCenterRow, hv_EdgeCircleCenterCol, (HTuple)RightCenterRow, (HTuple)RightCenterCol, (HTuple)6);
                    outWindow.DispArrow(hv_EdgeCircleCenterRow, hv_EdgeCircleCenterCol, (HTuple)ButtomCenterRow, (HTuple)ButtomCenterCol, (HTuple)6);
                    outWindow.DispText("NG", "window", "top", "left", "red", "box", "true");
                }
                else
                {
                    outWindow.SetColor("green");
                    outWindow.SetLineWidth(2);
                    outWindow.SetDraw("margin");
                    outWindow.DispCircle(hv_EdgeCircleCenterRow, hv_EdgeCircleCenterCol, hv_EdgeCircleCenterRadius);
                    outWindow.DispLine(hv_ButtomEdgeRowBegin, hv_ButtomEdgeColBegin, hv_ButtomEdgeRowEnd, hv_ButtomEdgeColEnd);
                    outWindow.DispLine(hv_RightEdgeRowBegin, hv_RightEdgeColBegin, hv_RightEdgeRowEnd, hv_RightEdgeColEnd);

                    double RightCenterRow = (hv_RightEdgeRowBegin + hv_RightEdgeRowEnd) / 2;
                    double RightCenterCol = (hv_RightEdgeColBegin + hv_RightEdgeColEnd) / 2;
                    double ButtomCenterRow = (hv_ButtomEdgeRowBegin + hv_ButtomEdgeRowEnd) / 2;
                    double ButtomCenterCol = (hv_ButtomEdgeColBegin + hv_ButtomEdgeColEnd) / 2;
                    outWindow.DispArrow(hv_EdgeCircleCenterRow, hv_EdgeCircleCenterCol, (HTuple)RightCenterRow, (HTuple)RightCenterCol, (HTuple)6);
                    outWindow.DispArrow(hv_EdgeCircleCenterRow, hv_EdgeCircleCenterCol, (HTuple)ButtomCenterRow, (HTuple)ButtomCenterCol, (HTuple)6);
                    outWindow.DispText("PASS", "window", "top", "left", "green", "box", "true");
                }
            }
            catch 
            {
                return false;
            }

            ho_Image.Dispose();
            ho_Circle1.Dispose();
            ho_Circle2.Dispose();
            ho_Circle3.Dispose();
            ho_Circle4.Dispose();
            ho_EdgeContour.Dispose();
            ho_EdgeCircle.Dispose();

            hv_PixclRealDis.Dispose();
            hv_p1_XOffest.Dispose();
            hv_p1_YOffest.Dispose();
            hv_p2_XOffest.Dispose();
            hv_p2_YOffest.Dispose();
            hv_p3_XOffest.Dispose();
            hv_p3_YOffest.Dispose();
            hv_p4_XOffest.Dispose();
            hv_p4_YOffest.Dispose();
            hv_Width.Dispose();
            hv_Height.Dispose();
            hv_StartTime.Dispose();
            hv_MaxCircleRow.Dispose();
            hv_MaxCircleColumn.Dispose();
            hv_MaxCircleRadius.Dispose();
            hv_MinCircleRow.Dispose();
            hv_MinCircleColumn.Dispose();
            hv_MinCircleRadius.Dispose();
            hv_TwoCirclePhi.Dispose();
            hv_ButtomEdgeRowBegin.Dispose();
            hv_ButtomEdgeColBegin.Dispose();
            hv_ButtomEdgeRowEnd.Dispose();
            hv_ButtomEdgeColEnd.Dispose();
            hv_RightEdgeRowBegin.Dispose();
            hv_RightEdgeColBegin.Dispose();
            hv_RightEdgeRowEnd.Dispose();
            hv_RightEdgeColEnd.Dispose();
            hv_Origin_Row_InImg.Dispose();
            hv_Origin_Column_InImg.Dispose();
            hv_IsOverlapping.Dispose();
            hv_p1_x.Dispose();
            hv_p1_y.Dispose();
            hv_p1_x_fit.Dispose();
            hv_p1_y_fit.Dispose();
            hv_p2_x.Dispose();
            hv_p2_y.Dispose();
            hv_p2_x_fit.Dispose();
            hv_p2_y_fit.Dispose();
            hv_p3_x.Dispose();
            hv_p3_y.Dispose();
            hv_p3_x_fit.Dispose();
            hv_p3_y_fit.Dispose();
            hv_p4_x.Dispose();
            hv_p4_y.Dispose();
            hv_p4_x_fit.Dispose();
            hv_p4_y_fit.Dispose();
            hv_EdgeCircleCenterRow.Dispose();
            hv_EdgeCircleCenterCol.Dispose();
            hv_EdgeCircleCenterRadius.Dispose();
            hv_StartPhi.Dispose();
            hv_EndPhi.Dispose();
            hv_PointOrder.Dispose();
            hv_X1.Dispose();
            hv_Y1.Dispose();
            hv_StopTime.Dispose();
            hv_CircleRadiu.Dispose();
            hv_PositionDegree.Dispose();
            hv_runtime.Dispose();

            return true;
        }

        private static bool Measure_18(HObject ho_Image, out double Origin_Z_mm, out double E2_OffestZ_mm, out double E5_OffestZ_mm, 
            out double E10_OffestZ_mm, out double E13_OffestZ_mm, out double Profile, out double RunTime)
        {
            Origin_Z_mm = -1;
            E2_OffestZ_mm = -1;
            E5_OffestZ_mm = -1;
            E10_OffestZ_mm = -1;
            E13_OffestZ_mm = -1;
            Profile = -1;
            RunTime = -1;

            HObject ho_ImageScaled;
            HOperatorSet.GenEmptyObj(out ho_ImageScaled);


            HTuple hv_p_A1_x_offest = new HTuple(), hv_p_A1_y_offest = new HTuple();
            HTuple hv_p_A2_y_offest = new HTuple(), hv_p_A2_x_offest = new HTuple();
            HTuple hv_p_A3_y_offest = new HTuple(), hv_p_A3_x_offest = new HTuple();
            HTuple hv_p_A4_y_offest = new HTuple(), hv_p_A4_x_offest = new HTuple();
            HTuple hv_p_A5_y_offest = new HTuple(), hv_p_A5_x_offest = new HTuple();
            HTuple hv_p_A6_y_offest = new HTuple(), hv_p_A6_x_offest = new HTuple();
            HTuple hv_p_A7_y_offest = new HTuple(), hv_p_A7_x_offest = new HTuple();
            HTuple hv_p_A8_y_offest = new HTuple(), hv_p_A8_x_offest = new HTuple();
            HTuple hv_p_E2_y_offest = new HTuple(), hv_p_E2_x_offest = new HTuple();
            HTuple hv_p_E5_y_offest = new HTuple(), hv_p_E5_x_offest = new HTuple();
            HTuple hv_p_E10_y_offest = new HTuple(), hv_p_E10_x_offest = new HTuple();
            HTuple hv_p_E13_y_offest = new HTuple(), hv_p_E13_x_offest = new HTuple();
            HTuple hv_B_offest = new HTuple(), hv_C_offest = new HTuple();
            HTuple hv_start_time = new HTuple(), hv_Width = new HTuple();
            HTuple hv_Height = new HTuple(), hv_Row_Origin = new HTuple();
            HTuple hv_Col_Origin = new HTuple(), hv_MaxCircleRadius = new HTuple();
            HTuple hv_RowEnd_X = new HTuple(), hv_ColEnd_X = new HTuple();
            HTuple hv_MinCircleRadius = new HTuple(), hv_TwoCirclePhi = new HTuple();
            HTuple hv_HomMat2DIdentity = new HTuple(), hv_Phi = new HTuple();
            HTuple hv_ColEnd_Y = new HTuple(), hv_RowEnd_Y = new HTuple();
            HTuple hv_Z_Origin_Median = new HTuple(), hv_E2_Z_mm = new HTuple();
            HTuple hv_E5_Z_mm = new HTuple(), hv_E10_Z_mm = new HTuple();
            HTuple hv_E13_Z_mm = new HTuple(), hv_p_E2_x_fit = new HTuple();
            HTuple hv_p_E2_y_fit = new HTuple(), hv_p_E5_x_fit = new HTuple();
            HTuple hv_p_E5_y_fit = new HTuple(), hv_p_E10_x_fit = new HTuple();
            HTuple hv_p_E10_y_fit = new HTuple(), hv_p_E13_x_fit = new HTuple();
            HTuple hv_p_E13_y_fit = new HTuple(), hv_result = new HTuple();
            HTuple hv_stop_time = new HTuple();
            // Initialize local and output iconic variables 
            hv_p_A1_x_offest.Dispose(); hv_p_A1_y_offest.Dispose(); hv_p_A2_y_offest.Dispose(); hv_p_A2_x_offest.Dispose(); hv_p_A3_y_offest.Dispose(); hv_p_A3_x_offest.Dispose(); hv_p_A4_y_offest.Dispose(); hv_p_A4_x_offest.Dispose(); hv_p_A5_y_offest.Dispose(); hv_p_A5_x_offest.Dispose(); hv_p_A6_y_offest.Dispose(); hv_p_A6_x_offest.Dispose(); hv_p_A7_y_offest.Dispose(); hv_p_A7_x_offest.Dispose(); hv_p_A8_y_offest.Dispose(); hv_p_A8_x_offest.Dispose(); hv_p_E2_y_offest.Dispose(); hv_p_E2_x_offest.Dispose(); hv_p_E5_y_offest.Dispose(); hv_p_E5_x_offest.Dispose(); hv_p_E10_y_offest.Dispose(); hv_p_E10_x_offest.Dispose(); hv_p_E13_y_offest.Dispose(); hv_p_E13_x_offest.Dispose(); hv_B_offest.Dispose(); hv_C_offest.Dispose();
            Set_Offest_Param(out hv_p_A1_x_offest, out hv_p_A1_y_offest, out hv_p_A2_y_offest,
                out hv_p_A2_x_offest, out hv_p_A3_y_offest, out hv_p_A3_x_offest, out hv_p_A4_y_offest,
                out hv_p_A4_x_offest, out hv_p_A5_y_offest, out hv_p_A5_x_offest, out hv_p_A6_y_offest,
                out hv_p_A6_x_offest, out hv_p_A7_y_offest, out hv_p_A7_x_offest, out hv_p_A8_y_offest,
                out hv_p_A8_x_offest, out hv_p_E2_y_offest, out hv_p_E2_x_offest, out hv_p_E5_y_offest,
                out hv_p_E5_x_offest, out hv_p_E10_y_offest, out hv_p_E10_x_offest, out hv_p_E13_y_offest,
                out hv_p_E13_x_offest, out hv_B_offest, out hv_C_offest);
            hv_start_time.Dispose();
            HOperatorSet.CountSeconds(out hv_start_time);
            hv_Width.Dispose(); hv_Height.Dispose();
            HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
            try
            {
                ho_ImageScaled.Dispose();
                HOperatorSet.ScaleImage(ho_Image, out ho_ImageScaled, 0.285554, -10322);

                //定位两圆心
                hv_Row_Origin.Dispose(); hv_Col_Origin.Dispose(); hv_MaxCircleRadius.Dispose(); hv_RowEnd_X.Dispose(); hv_ColEnd_X.Dispose(); hv_MinCircleRadius.Dispose(); hv_TwoCirclePhi.Dispose();
                gen_TwoCircle_info_3D(ho_ImageScaled, out hv_Row_Origin, out hv_Col_Origin, out hv_MaxCircleRadius,
                    out hv_RowEnd_X, out hv_ColEnd_X, out hv_MinCircleRadius, out hv_TwoCirclePhi);
                hv_HomMat2DIdentity.Dispose();
                HOperatorSet.HomMat2dIdentity(out hv_HomMat2DIdentity);
                //以大圆圆心为原点建立坐标系
                hv_Phi.Dispose(); hv_ColEnd_Y.Dispose(); hv_RowEnd_Y.Dispose();
                my_create_coordinate(hv_HomMat2DIdentity, hv_Col_Origin, hv_Row_Origin, hv_ColEnd_X,
                    hv_RowEnd_X, out hv_Phi, out hv_ColEnd_Y, out hv_RowEnd_Y);
                //寻找拟合平面点，并计算Z0向平面的高度
                hv_Z_Origin_Median.Dispose();
                my_fit_surface(ho_Image, hv_HomMat2DIdentity, hv_p_A1_x_offest, hv_p_A1_y_offest,
                    hv_Phi, hv_Col_Origin, hv_Row_Origin, hv_p_A2_x_offest, hv_p_A2_y_offest,
                    hv_p_A3_x_offest, hv_p_A3_y_offest, hv_p_A4_x_offest, hv_p_A4_y_offest, hv_p_A5_x_offest,
                    hv_p_A5_y_offest, hv_p_A6_x_offest, hv_p_A6_y_offest, hv_p_A7_x_offest, hv_p_A7_y_offest,
                    hv_p_A8_x_offest, hv_p_A8_y_offest, out hv_Z_Origin_Median);
                //寻找底部4个点，计算四个点相对于Z0向平面的偏差
                hv_E2_Z_mm.Dispose(); hv_E5_Z_mm.Dispose(); hv_E10_Z_mm.Dispose(); hv_E13_Z_mm.Dispose(); hv_p_E2_x_fit.Dispose(); hv_p_E2_y_fit.Dispose(); hv_p_E5_x_fit.Dispose(); hv_p_E5_y_fit.Dispose(); hv_p_E10_x_fit.Dispose(); hv_p_E10_y_fit.Dispose(); hv_p_E13_x_fit.Dispose(); hv_p_E13_y_fit.Dispose();
                my_gen_4point_offest(ho_Image, hv_HomMat2DIdentity, hv_p_E2_x_offest, hv_p_E2_y_offest,
                    hv_Phi, hv_Col_Origin, hv_Row_Origin, hv_p_E5_x_offest, hv_p_E5_y_offest,
                    hv_p_E10_x_offest, hv_p_E10_y_offest, hv_p_E13_x_offest, hv_p_E13_y_offest,
                    hv_Z_Origin_Median, out hv_E2_Z_mm, out hv_E5_Z_mm, out hv_E10_Z_mm, out hv_E13_Z_mm,
                    out hv_p_E2_x_fit, out hv_p_E2_y_fit, out hv_p_E5_x_fit, out hv_p_E5_y_fit,
                    out hv_p_E10_x_fit, out hv_p_E10_y_fit, out hv_p_E13_x_fit, out hv_p_E13_y_fit);
                //计算轮廓度
                hv_result.Dispose();
                my_gen_result(hv_E2_Z_mm, hv_E5_Z_mm, hv_E10_Z_mm, hv_E13_Z_mm, out hv_result);
                hv_stop_time.Dispose();
                HOperatorSet.CountSeconds(out hv_stop_time);

                //数据处理
                Origin_Z_mm = hv_Z_Origin_Median;
                E2_OffestZ_mm = hv_E2_Z_mm;
                E5_OffestZ_mm = hv_E5_Z_mm;
                E10_OffestZ_mm = hv_E10_Z_mm;
                E13_OffestZ_mm = hv_E13_Z_mm;
                Profile = hv_result;
                RunTime =  hv_stop_time - hv_start_time;

                //图像显示
                DispImage(ho_Image);
                
                if (Profile < 0 || Profile > 0.04)
                {
                    outWindow.SetColor("red");
                    outWindow.SetLineWidth(2);
                    outWindow.SetDraw("margin");

                    outWindow.DispArrow(hv_Row_Origin, hv_Col_Origin, (HTuple)hv_RowEnd_X, (HTuple)hv_ColEnd_X, (HTuple)6);
                    outWindow.DispArrow(hv_Row_Origin, hv_Col_Origin, (HTuple)hv_RowEnd_Y, (HTuple)hv_ColEnd_Y, (HTuple)6);

                    outWindow.DispCircle(hv_Row_Origin, hv_Col_Origin, 10);
                    outWindow.DispCircle(hv_p_E2_y_fit, hv_p_E2_x_fit, 10);
                    outWindow.DispCircle(hv_p_E5_y_fit, hv_p_E5_x_fit, 10);
                    outWindow.DispCircle(hv_p_E10_y_fit, hv_p_E10_x_fit, 10);
                    outWindow.DispCircle(hv_p_E13_y_fit, hv_p_E13_x_fit, 10);
                    outWindow.DispText("NG", "window", "top", "left", "red", "box", "true");
                }
                else
                {
                    outWindow.SetColor("green");
                    outWindow.SetLineWidth(2);
                    outWindow.SetDraw("margin");
                    outWindow.DispArrow(hv_Row_Origin, hv_Col_Origin, (HTuple)hv_RowEnd_X, (HTuple)hv_ColEnd_X, (HTuple)6);
                    outWindow.DispArrow(hv_Row_Origin, hv_Col_Origin, (HTuple)hv_RowEnd_Y, (HTuple)hv_ColEnd_Y, (HTuple)6);

                    outWindow.DispCircle(hv_Row_Origin, hv_Col_Origin, 5);
                    outWindow.DispCircle(hv_p_E2_y_fit, hv_p_E2_x_fit, 5);
                    outWindow.DispCircle(hv_p_E5_y_fit, hv_p_E5_x_fit, 5);
                    outWindow.DispCircle(hv_p_E10_y_fit, hv_p_E10_x_fit, 5);
                    outWindow.DispCircle(hv_p_E13_y_fit, hv_p_E13_x_fit, 5);
                    outWindow.DispText("PASS", "window", "top", "left", "green", "box", "true");
                }

            }
            catch
            {
                return false;
            }
            
            ho_Image.Dispose();

            hv_p_A1_x_offest.Dispose();
            hv_p_A1_y_offest.Dispose();
            hv_p_A2_y_offest.Dispose();
            hv_p_A2_x_offest.Dispose();
            hv_p_A3_y_offest.Dispose();
            hv_p_A3_x_offest.Dispose();
            hv_p_A4_y_offest.Dispose();
            hv_p_A4_x_offest.Dispose();
            hv_p_A5_y_offest.Dispose();
            hv_p_A5_x_offest.Dispose();
            hv_p_A6_y_offest.Dispose();
            hv_p_A6_x_offest.Dispose();
            hv_p_A7_y_offest.Dispose();
            hv_p_A7_x_offest.Dispose();
            hv_p_A8_y_offest.Dispose();
            hv_p_A8_x_offest.Dispose();
            hv_p_E2_y_offest.Dispose();
            hv_p_E2_x_offest.Dispose();
            hv_p_E5_y_offest.Dispose();
            hv_p_E5_x_offest.Dispose();
            hv_p_E10_y_offest.Dispose();
            hv_p_E10_x_offest.Dispose();
            hv_p_E13_y_offest.Dispose();
            hv_p_E13_x_offest.Dispose();
            hv_B_offest.Dispose();
            hv_C_offest.Dispose();
            hv_start_time.Dispose();
            hv_Width.Dispose();
            hv_Height.Dispose();
            hv_Row_Origin.Dispose();
            hv_Col_Origin.Dispose();
            hv_MaxCircleRadius.Dispose();
            hv_RowEnd_X.Dispose();
            hv_ColEnd_X.Dispose();
            hv_MinCircleRadius.Dispose();
            hv_TwoCirclePhi.Dispose();
            hv_HomMat2DIdentity.Dispose();
            hv_Phi.Dispose();
            hv_ColEnd_Y.Dispose();
            hv_RowEnd_Y.Dispose();
            hv_Z_Origin_Median.Dispose();
            hv_E2_Z_mm.Dispose();
            hv_E5_Z_mm.Dispose();
            hv_E10_Z_mm.Dispose();
            hv_E13_Z_mm.Dispose();
            hv_p_E2_x_fit.Dispose();
            hv_p_E2_y_fit.Dispose();
            hv_p_E5_x_fit.Dispose();
            hv_p_E5_y_fit.Dispose();
            hv_p_E10_x_fit.Dispose();
            hv_p_E10_y_fit.Dispose();
            hv_p_E13_x_fit.Dispose();
            hv_p_E13_y_fit.Dispose();
            hv_result.Dispose();
            hv_stop_time.Dispose();


            return true;
        }

        #endregion

        //2D数据列表
        #region
        public static void InitListView2D(ListView lv)
        {
            lv.GridLines = true;        //显示网格线
            lv.View = View.Details;         //显示详情
            lv.FullRowSelect = true;            //显示整行
            //lv.HoverSelection = true;           //鼠标悬停后自动选择

            // 添加列表头
            ColumnHeader C1 = new ColumnHeader();
            C1.Text = "Diameter(mm)";
            C1.Width = 110;
            lv.Columns.Add(C1);
            ColumnHeader C2 = new ColumnHeader();
            C2.Text = "PositionDegree";
            C2.Width = 120;
            lv.Columns.Add(C2);
            ColumnHeader C3 = new ColumnHeader();
            C3.Text = "DistanceX1(mm)";
            C3.Width = 110;
            lv.Columns.Add(C3);
            ColumnHeader C4 = new ColumnHeader();
            C4.Text = "DistanceY1(mm)";
            C4.Width = 110;
            lv.Columns.Add(C4);
            ColumnHeader C5 = new ColumnHeader();
            C5.Text = "RunTime(ms)";
             C5.Width = 110;
            lv.Columns.Add(C5);
            ColumnHeader C6 = new ColumnHeader();
            C6.Text = "CurrentTime";
            C6.Width = 250;
            lv.Columns.Add(C6);
        }

        //向ListView中插入一行数据
        public static void insertLine2D(ListView lv, double RunTime, double Radius, double PositionDegree,
             double DistanceX1, double DistanceY1)
        {
            ListViewItem items = new ListViewItem(Radius.ToString("f5"));
            items.SubItems.Add(PositionDegree.ToString("f5"));
            items.SubItems.Add(DistanceX1.ToString("f5"));
            items.SubItems.Add(DistanceY1.ToString("f5"));
            items.SubItems.Add(RunTime.ToString("f5"));
            items.SubItems.Add(DateTime.Now.ToString());
            lv.Items.Add(items);
  
        }
        #endregion

        //3D数据列表
        #region
        public static void InitListView3D(ListView lv)
        {
            lv.GridLines = true;        //显示网格线
            lv.View = View.Details;         //显示详情
            lv.FullRowSelect = true;            //显示整行
             //lv.HoverSelection = true;           //鼠标悬停后自动选择

            // 添加列表头
            ColumnHeader C1 = new ColumnHeader();
            C1.Text = "Origin_Z(mm)";
            C1.Width = 100;
            lv.Columns.Add(C1);

            ColumnHeader C2 = new ColumnHeader();
            C2.Text = "E2_OffestZ(mm)";
            C2.Width = 110;
            lv.Columns.Add(C2);

            ColumnHeader C3 = new ColumnHeader();
            C3.Text = "E5_OffestZ(mm)";
            C3.Width = 110;
            lv.Columns.Add(C3);

            ColumnHeader C4 = new ColumnHeader();
            C4.Text = "E10_OffestZ(mm)";
            C4.Width = 120;
            lv.Columns.Add(C4);

            ColumnHeader C5 = new ColumnHeader();
            C5.Text = "E13_OffestZ(mm)";
            C5.Width = 120;
            lv.Columns.Add(C5);

            ColumnHeader C6 = new ColumnHeader();
            C6.Text = "Profile";
            C6.Width = 75;
            lv.Columns.Add(C6);

            ColumnHeader C7 = new ColumnHeader();
            C7.Text = "RunTime";
            C7.Width = 75;
            lv.Columns.Add(C7);

            ColumnHeader C8 = new ColumnHeader();
            C8.Text = "CurrentTime";
            C8.Width = 100;
            C8.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
            lv.Columns.Add(C8);
        }

        //向ListView中插入一行数据
        public static void insertLine3D(ListView lv, double RunTime, double Origin_Z_mm, double E2_OffestZ_mm, double E5_OffestZ_mm,
            double E10_OffestZ_mm, double E13_OffestZ_mm, double Profile)
        {
            ListViewItem items = new ListViewItem(Origin_Z_mm.ToString("f5"));
            items.SubItems.Add(E2_OffestZ_mm.ToString("f5"));
            items.SubItems.Add(E5_OffestZ_mm.ToString("f5"));
            items.SubItems.Add(E10_OffestZ_mm.ToString("f5"));
            items.SubItems.Add(E13_OffestZ_mm.ToString("f5"));
            items.SubItems.Add(Profile.ToString("f5"));
            items.SubItems.Add(RunTime.ToString("f5"));
            items.SubItems.Add(DateTime.Now.ToString());
            lv.Items.Add(items);
        }
        #endregion


        //清空ListView
        public static void ClearListView(ListView lv)
        {
            lv.Clear();
        }


    }
}
