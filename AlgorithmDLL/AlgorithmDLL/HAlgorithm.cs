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
        
        public static void DispBuffer(IntPtr buffer, ushort Bufferwidth, ushort Bufferheight)
        {
            HObject HojFromBuffer;
            HOperatorSet.GenImage1Extern(out HojFromBuffer, "byte", Bufferwidth, Bufferheight, buffer, IntPtr.Zero);
            HTuple width, height;
            HOperatorSet.GetImageSize(HojFromBuffer, out width, out height);
            inWindow.SetPart((HTuple)0, (HTuple)0, height, width);
            inWindow.DispObj(HojFromBuffer);
            HojFromBuffer.Dispose();
        }

        public static void DisposeWindow(PictureBox pb_in, PictureBox pb_out)
        {
            CreateInWindow(pb_in.Handle, pb_in.Width, pb_in.Height);
            CreateOutWindow(pb_out.Handle, pb_out.Width, pb_out.Height);
            inWindow.Dispose();
            outWindow.Dispose();
        }

        public static void CreateInWindow(IntPtr pb_Handle, int pb_width, int pb_height)
        {
            inWindow = new HWindow(0, 0, pb_width, pb_height, pb_Handle, "visible", "");
        }

        public static void CreateOutWindow(IntPtr pb_Handle, int pb_width, int pb_height)
        {
            outWindow = new HWindow(0, 0, pb_width, pb_height, pb_Handle, "visible", "");
        }

        public static void DispImageFile(string path)
        {
            HObject image;
            HOperatorSet.GenEmptyObj(out image);
            HTuple hv_ImageFiles = new HTuple(), hv_Index = new HTuple();
            hv_ImageFiles.Dispose(); hv_Index.Dispose();
            list_image_files(path, "bmp", new HTuple(), out hv_ImageFiles);
            for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_ImageFiles.TupleLength()
        )) - 1); hv_Index = (int)hv_Index + 1)
            {
                image.Dispose();
                HOperatorSet.ReadImage(out image, hv_ImageFiles.TupleSelect(hv_Index)); HTuple width, height;
                HOperatorSet.GetImageSize(image, out width, out height);
                inWindow.SetPart((HTuple)0, (HTuple)0, height, width);
                inWindow.DispObj(image);
                HOperatorSet.WaitSeconds(0.5);
            }

                

            image.Dispose();
        }

        public static void DispImage(HObject image)
        {
            HTuple width, height;
            HOperatorSet.GetImageSize(image, out width, out height);
            outWindow.SetPart((HTuple)0, (HTuple)0, height, width);
            outWindow.DispObj(image);
            image.Dispose();
        }

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

        public static void OutLineMeasure(int MeasureProject, ListView lv, 
            out double Radius, out double PositionDegree, out double RunTime,
            out double DistanceL1L2, out double DistanceX1, out double DistanceY1, string path)
        {
            Radius = -1;
            PositionDegree = -1;
            RunTime = -1;
            DistanceL1L2 = -1;
            DistanceX1 = -1;
            DistanceY1 = -1;
            HObject image;
            HOperatorSet.GenEmptyObj(out image);
            image.Dispose();
            HTuple hv_ImageFiles = new HTuple(), hv_Index = new HTuple();
            hv_ImageFiles.Dispose(); hv_Index.Dispose();
            
            list_image_files(path, "bmp", new HTuple(), out hv_ImageFiles);
            for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_ImageFiles.TupleLength()
        )) - 1); hv_Index = (int)hv_Index + 1)
            {
                HOperatorSet.ReadImage(out image, hv_ImageFiles.TupleSelect(hv_Index));
                switch (MeasureProject)
                {
                    case 9:
                        Measure_9(image, out Radius, out PositionDegree, out RunTime, out DistanceX1, out DistanceY1);
                        break;
                    case 10:
                        Measure_10(image, out DistanceL1L2, out DistanceX1, out RunTime);
                        break;
                    case 33:
                        Measure_33(image, out Radius, out PositionDegree, out DistanceX1, out DistanceY1, out RunTime);
                        break;
                    case 34:
                        Measure_34(image, out RunTime, out DistanceL1L2, out DistanceY1);
                        break;
                    case 46:
                        Measure_46(image, out RunTime, out DistanceY1);
                        break;
                    default:
                        break;
                }
                insertLine(lv, RunTime, Radius, PositionDegree, DistanceL1L2, DistanceX1, DistanceY1);
                HOperatorSet.WaitSeconds(0.5);
            }

            image.Dispose();

        }

        public static void InLineMeasure(int MeasureProject, ListView lv, IntPtr buffer, ushort BufferWidth, ushort BufferHeight,
    out double Radius, out double PositionDegree, out double RunTime,
    out double DistanceL1L2, out double DistanceX1, out double DistanceY1)
        {
            Radius = -1;
            PositionDegree = -1;
            RunTime = -1;
            DistanceL1L2 = -1;
            DistanceX1 = -1;
            DistanceY1 = -1;
            HObject image;
            HOperatorSet.GenImage1Extern(out image, "byte", BufferWidth, BufferHeight, buffer, IntPtr.Zero);

            switch (MeasureProject)
            {
                case 9:
                    Measure_9(image, out Radius, out PositionDegree, out RunTime, out DistanceX1, out DistanceY1);
                    break;
                case 10:
                    Measure_10(image, out DistanceL1L2, out DistanceX1, out RunTime);
                    break;
                case 33:
                    Measure_33(image, out Radius, out PositionDegree, out DistanceX1, out DistanceY1, out RunTime);
                    break;
                case 34:
                    Measure_34(image, out RunTime, out DistanceL1L2, out DistanceY1);
                    break;
                case 46:
                    Measure_46(image, out RunTime, out DistanceY1);
                    break;
                default:
                    break;
            }
            insertLine(lv, RunTime, Radius, PositionDegree, DistanceL1L2, DistanceX1, DistanceY1);

            image.Dispose();

        }

        public static void Measure_9(HObject ho_Image, out double CirclrRadius, out double PositionDegree, out double RunTime,
            out double DistanceX1, out double DistanceY1)
        {
            // Local iconic variables 
            HObject ho_BigCircle;
            // Local control variables 
            HTuple hv_Width = new HTuple(), hv_Height = new HTuple();
            HTuple hv_StartTime = new HTuple(), hv_MaxCircleRow = new HTuple();
            HTuple hv_MaxCircleColumn = new HTuple(), hv_MaxCircleRadius = new HTuple();
            HTuple hv_MinCircleRow = new HTuple(), hv_MinCircleColumn = new HTuple();
            HTuple hv_MinCircleRadius = new HTuple(), hv_TwoCirclePhi = new HTuple();
            HTuple hv_LeftTopRowEdge = new HTuple(), hv_LeftTopColumnEdge = new HTuple();
            HTuple hv_RightButtomRowEdge = new HTuple(), hv_RightButtomColumnEdge = new HTuple();
            HTuple hv_LeftButtomRowEdge = new HTuple(), hv_LeftButtomColumnEdge = new HTuple();
            HTuple hv_RightTopRowEdge = new HTuple(), hv_RightTopColumnEdge = new HTuple();
            HTuple hv_FitCircleCenterRow = new HTuple(), hv_FitCircleCenterCol = new HTuple();
            HTuple hv_FitCircleCenterRadius = new HTuple(), hv_ButtomRowEdge1 = new HTuple();
            HTuple hv_ButtomColumnEdge1 = new HTuple(), hv_ButtomRowEdge2 = new HTuple();
            HTuple hv_ButtomColumnEdge2 = new HTuple(), hv_RightRowEdge1 = new HTuple();
            HTuple hv_RightColumnEdge1 = new HTuple(), hv_RightRowEdge2 = new HTuple();
            HTuple hv_RightColumnEdge2 = new HTuple(), hv_X1 = new HTuple();
            HTuple hv_Y1 = new HTuple(), hv_StopTime = new HTuple();
            HTuple hv_runtime = new HTuple(), hv_CircleRadius = new HTuple();
            HTuple hv_PositionDegree = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_BigCircle);
            hv_Width.Dispose(); hv_Height.Dispose();
            HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
            hv_StartTime.Dispose();
            HOperatorSet.CountSeconds(out hv_StartTime);
            hv_MaxCircleRow.Dispose(); hv_MaxCircleColumn.Dispose(); hv_MaxCircleRadius.Dispose(); hv_MinCircleRow.Dispose(); hv_MinCircleColumn.Dispose(); hv_MinCircleRadius.Dispose(); hv_TwoCirclePhi.Dispose();
            gen_TwoCircle_info(ho_Image, out hv_MaxCircleRow, out hv_MaxCircleColumn, out hv_MaxCircleRadius,
                out hv_MinCircleRow, out hv_MinCircleColumn, out hv_MinCircleRadius, out hv_TwoCirclePhi);
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                ho_BigCircle.Dispose(); hv_LeftTopRowEdge.Dispose(); hv_LeftTopColumnEdge.Dispose(); hv_RightButtomRowEdge.Dispose(); hv_RightButtomColumnEdge.Dispose(); hv_LeftButtomRowEdge.Dispose(); hv_LeftButtomColumnEdge.Dispose(); hv_RightTopRowEdge.Dispose(); hv_RightTopColumnEdge.Dispose(); hv_FitCircleCenterRow.Dispose(); hv_FitCircleCenterCol.Dispose(); hv_FitCircleCenterRadius.Dispose();
                gen_Edge_Circle(ho_Image, out ho_BigCircle, hv_MaxCircleRow, hv_MaxCircleColumn,
                    hv_TwoCirclePhi, hv_Width, hv_Height, (new HTuple(45)).TupleRad(), hv_MaxCircleRadius,
                    out hv_LeftTopRowEdge, out hv_LeftTopColumnEdge, out hv_RightButtomRowEdge,
                    out hv_RightButtomColumnEdge, out hv_LeftButtomRowEdge, out hv_LeftButtomColumnEdge,
                    out hv_RightTopRowEdge, out hv_RightTopColumnEdge, out hv_FitCircleCenterRow,
                    out hv_FitCircleCenterCol, out hv_FitCircleCenterRadius);
            }
            //定位底部测量矩形
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_ButtomRowEdge1.Dispose(); hv_ButtomColumnEdge1.Dispose(); hv_ButtomRowEdge2.Dispose(); hv_ButtomColumnEdge2.Dispose();
                gen_buttom_edge(ho_Image, hv_MaxCircleRow, hv_MaxCircleColumn, (hv_MaxCircleRadius * 3) / 2,
                    (hv_MaxCircleRadius * 3) / 4, hv_TwoCirclePhi, hv_Width, hv_Height, (new HTuple(90)).TupleRad()
                    , (new HTuple(90)).TupleRad(), hv_MinCircleColumn, hv_MaxCircleColumn, out hv_ButtomRowEdge1,
                    out hv_ButtomColumnEdge1, out hv_ButtomRowEdge2, out hv_ButtomColumnEdge2);
            }
            //定位右侧测量矩形
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_RightRowEdge1.Dispose(); hv_RightColumnEdge1.Dispose(); hv_RightRowEdge2.Dispose(); hv_RightColumnEdge2.Dispose();
                gen_right_edge(ho_Image, hv_MinCircleRow, hv_MinCircleColumn, (hv_MinCircleRadius * 3) / 4,
                    (hv_MinCircleRadius * 6) / 5, hv_MaxCircleColumn, hv_TwoCirclePhi, hv_Width,
                    hv_Height, (new HTuple(180)).TupleRad(), 0, out hv_RightRowEdge1, out hv_RightColumnEdge1,
                    out hv_RightRowEdge2, out hv_RightColumnEdge2);
            }
            //计算X1
            hv_X1.Dispose();
            HOperatorSet.DistancePl(hv_FitCircleCenterRow, hv_FitCircleCenterCol, hv_RightRowEdge1,
                hv_RightColumnEdge1, hv_RightRowEdge2, hv_RightColumnEdge2, out hv_X1);
            //计算Y1
            hv_Y1.Dispose();
            HOperatorSet.DistancePl(hv_FitCircleCenterRow, hv_FitCircleCenterCol, hv_ButtomRowEdge1,
                hv_ButtomColumnEdge1, hv_ButtomRowEdge2, hv_ButtomColumnEdge2, out hv_Y1);
            //计算运行时间
            hv_StopTime.Dispose();
            HOperatorSet.CountSeconds(out hv_StopTime);
            hv_runtime.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_runtime = (hv_StopTime - hv_StartTime) * 1000;
                RunTime = hv_runtime;
            }
            //输出结果
            hv_CircleRadius.Dispose();
            hv_CircleRadius = new HTuple(hv_FitCircleCenterRadius);
            CirclrRadius = hv_CircleRadius*2;
            hv_PositionDegree.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_PositionDegree = 2 * ((((((hv_X1 - 19.605)).TuplePow(
                    2)) + (((hv_Y1 - 6.788)).TuplePow(2)))).TupleSqrt());
            }
            PositionDegree = hv_PositionDegree;
            DistanceX1 = hv_X1;
            DistanceY1 = hv_Y1;

            DispImage(ho_Image);
            outWindow.SetColor("red");
            outWindow.SetLineWidth(2);
            outWindow.SetDraw("margin");
            outWindow.DispCircle(hv_FitCircleCenterRow, hv_FitCircleCenterCol, hv_FitCircleCenterRadius);
            outWindow.DispLine(hv_ButtomRowEdge1, hv_ButtomColumnEdge1, hv_ButtomRowEdge2, hv_ButtomColumnEdge2);
            outWindow.DispLine(hv_RightRowEdge1, hv_RightColumnEdge1, hv_RightRowEdge2, hv_RightColumnEdge2);

            ho_Image.Dispose();
            ho_BigCircle.Dispose();

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
            hv_LeftTopRowEdge.Dispose();
            hv_LeftTopColumnEdge.Dispose();
            hv_RightButtomRowEdge.Dispose();
            hv_RightButtomColumnEdge.Dispose();
            hv_LeftButtomRowEdge.Dispose();
            hv_LeftButtomColumnEdge.Dispose();
            hv_RightTopRowEdge.Dispose();
            hv_RightTopColumnEdge.Dispose();
            hv_FitCircleCenterRow.Dispose();
            hv_FitCircleCenterCol.Dispose();
            hv_FitCircleCenterRadius.Dispose();
            hv_ButtomRowEdge1.Dispose();
            hv_ButtomColumnEdge1.Dispose();
            hv_ButtomRowEdge2.Dispose();
            hv_ButtomColumnEdge2.Dispose();
            hv_RightRowEdge1.Dispose();
            hv_RightColumnEdge1.Dispose();
            hv_RightRowEdge2.Dispose();
            hv_RightColumnEdge2.Dispose();
            hv_X1.Dispose();
            hv_Y1.Dispose();
            hv_StopTime.Dispose();
            hv_runtime.Dispose();
            hv_CircleRadius.Dispose();
            hv_PositionDegree.Dispose();
        }

        public static void Measure_10(HObject ho_Image, out double DistanceL1L2, out double DistanceX1, out double RunTime)
        {
            DistanceL1L2 = -1;
            DistanceX1 = -1;
            RunTime = -1;
            // Local control variables 
            HTuple hv_Width = new HTuple(), hv_Height = new HTuple();
            HTuple hv_StartTime = new HTuple(), hv_MaxCircleRow = new HTuple();
            HTuple hv_MaxCircleColumn = new HTuple(), hv_MaxCircleRadius = new HTuple();
            HTuple hv_MinCircleRow = new HTuple(), hv_MinCircleColumn = new HTuple();
            HTuple hv_MinCircleRadius = new HTuple(), hv_TwoCirclePhi = new HTuple();
            HTuple hv_RightCircleRowEdge1 = new HTuple(), hv_RightCircleColumnEdge1 = new HTuple();
            HTuple hv_LeftCircleRowEdge1 = new HTuple(), hv_LeftCircleColumnEdge1 = new HTuple();
            HTuple hv_RightCircleRowEdge2 = new HTuple(), hv_RightCircleColumnEdge2 = new HTuple();
            HTuple hv_LeftCircleRowEdge2 = new HTuple(), hv_LeftCircleColumnEdge2 = new HTuple();
            HTuple hv_RightCenterRow = new HTuple(), hv_RightCenterCol = new HTuple();
            HTuple hv_LeftCenterRow = new HTuple(), hv_LeftCenterCol = new HTuple();
            HTuple hv_L1_L2 = new HTuple(), hv_RightRowEdge1 = new HTuple();
            HTuple hv_RightColumnEdge1 = new HTuple(), hv_RightRowEdge2 = new HTuple();
            HTuple hv_RightColumnEdge2 = new HTuple(), hv_L3CenterRow = new HTuple();
            HTuple hv_L3CenterCol = new HTuple(), hv_L3StartRow = new HTuple();
            HTuple hv_L3StartCol = new HTuple(), hv_L3EndRow = new HTuple();
            HTuple hv_L3EndCol = new HTuple(), hv_X1 = new HTuple();
            HTuple hv_StopTime = new HTuple(), hv_runtime = new HTuple();
            HTuple hv_distanceL1L2 = new HTuple(), hv_distanceX1 = new HTuple();
            // Initialize local and output iconic variables 
            hv_Width.Dispose(); hv_Height.Dispose();
            HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
            hv_StartTime.Dispose();
            HOperatorSet.CountSeconds(out hv_StartTime);
            hv_MaxCircleRow.Dispose(); hv_MaxCircleColumn.Dispose(); hv_MaxCircleRadius.Dispose(); hv_MinCircleRow.Dispose(); hv_MinCircleColumn.Dispose(); hv_MinCircleRadius.Dispose(); hv_TwoCirclePhi.Dispose();
            gen_TwoCircle_info(ho_Image, out hv_MaxCircleRow, out hv_MaxCircleColumn, out hv_MaxCircleRadius,
                out hv_MinCircleRow, out hv_MinCircleColumn, out hv_MinCircleRadius, out hv_TwoCirclePhi);
            hv_RightCircleRowEdge1.Dispose(); hv_RightCircleColumnEdge1.Dispose(); hv_LeftCircleRowEdge1.Dispose(); hv_LeftCircleColumnEdge1.Dispose(); hv_RightCircleRowEdge2.Dispose(); hv_RightCircleColumnEdge2.Dispose(); hv_LeftCircleRowEdge2.Dispose(); hv_LeftCircleColumnEdge2.Dispose();
            gen_L1L2_Measure10(ho_Image, hv_MinCircleRow, hv_MinCircleColumn, hv_MinCircleRadius,
                hv_TwoCirclePhi, hv_Width, hv_Height, out hv_RightCircleRowEdge1, out hv_RightCircleColumnEdge1,
                out hv_LeftCircleRowEdge1, out hv_LeftCircleColumnEdge1, out hv_RightCircleRowEdge2,
                out hv_RightCircleColumnEdge2, out hv_LeftCircleRowEdge2, out hv_LeftCircleColumnEdge2);
            //分别计算L1、L2的中心坐标
            hv_RightCenterRow.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_RightCenterRow = (hv_RightCircleRowEdge1 + hv_RightCircleRowEdge2) / 2;
            }
            hv_RightCenterCol.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_RightCenterCol = (hv_RightCircleColumnEdge1 + hv_RightCircleColumnEdge2) / 2;
            }
            hv_LeftCenterRow.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_LeftCenterRow = (hv_LeftCircleRowEdge1 + hv_LeftCircleRowEdge2) / 2;
            }
            hv_LeftCenterCol.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_LeftCenterCol = (hv_LeftCircleColumnEdge1 + hv_LeftCircleColumnEdge2) / 2;
            }
            //显示并计算L1、L2之间距离
            hv_L1_L2.Dispose();
            HOperatorSet.DistancePp(hv_RightCenterRow, hv_RightCenterCol, hv_LeftCenterRow,
                hv_LeftCenterCol, out hv_L1_L2);
            //定位右侧测量矩形
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_RightRowEdge1.Dispose(); hv_RightColumnEdge1.Dispose(); hv_RightRowEdge2.Dispose(); hv_RightColumnEdge2.Dispose();
                gen_right_edge(ho_Image, hv_MinCircleRow, hv_MinCircleColumn, (hv_MinCircleRadius * 3) / 4,
                    (hv_MinCircleRadius * 5) / 4, hv_MaxCircleColumn, hv_TwoCirclePhi, hv_Width,
                    hv_Height, (new HTuple(180)).TupleRad(), 0, out hv_RightRowEdge1, out hv_RightColumnEdge1,
                    out hv_RightRowEdge2, out hv_RightColumnEdge2);
            }
            //计算L3中心坐标和起点、终点坐标
            hv_L3CenterRow.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_L3CenterRow = (hv_RightCenterRow + hv_LeftCenterRow) / 2;
            }
            hv_L3CenterCol.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_L3CenterCol = (hv_RightCenterCol + hv_LeftCenterCol) / 2;
            }
            hv_L3StartRow.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_L3StartRow = (hv_RightCircleRowEdge1 + hv_LeftCircleRowEdge2) / 2;
            }
            hv_L3StartCol.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_L3StartCol = (hv_RightCircleColumnEdge1 + hv_LeftCircleColumnEdge2) / 2;
            }
            hv_L3EndRow.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_L3EndRow = (hv_RightCircleRowEdge2 + hv_LeftCircleRowEdge1) / 2;
            }
            hv_L3EndCol.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_L3EndCol = (hv_RightCircleColumnEdge2 + hv_LeftCircleColumnEdge1) / 2;
            }
            //计算X1
            hv_X1.Dispose();
            HOperatorSet.DistancePl(hv_L3CenterRow, hv_L3CenterCol, hv_RightRowEdge1, hv_RightColumnEdge1,
                hv_RightRowEdge2, hv_RightColumnEdge2, out hv_X1);
            //计算运行时间
            hv_StopTime.Dispose();
            HOperatorSet.CountSeconds(out hv_StopTime);
            hv_runtime.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_runtime = (hv_StopTime - hv_StartTime) * 1000;
                RunTime = hv_runtime;
            }
            //输出结果
            hv_distanceL1L2.Dispose();
            hv_distanceL1L2 = new HTuple(hv_L1_L2);
            DistanceL1L2 = hv_distanceL1L2;
            hv_distanceX1.Dispose();
            hv_distanceX1 = new HTuple(hv_X1);
            DistanceX1 = hv_distanceX1;

            //显示
            DispImage(ho_Image);
            outWindow.SetColor("red");
            outWindow.SetLineWidth(2);
            outWindow.DispLine(hv_RightCenterRow, hv_RightCenterCol, hv_LeftCenterRow, hv_LeftCenterCol);
            outWindow.DispLine(hv_L3StartRow, hv_L3StartCol, hv_L3EndRow, hv_L3EndCol);

            ho_Image.Dispose();
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
            hv_RightCircleRowEdge1.Dispose();
            hv_RightCircleColumnEdge1.Dispose();
            hv_LeftCircleRowEdge1.Dispose();
            hv_LeftCircleColumnEdge1.Dispose();
            hv_RightCircleRowEdge2.Dispose();
            hv_RightCircleColumnEdge2.Dispose();
            hv_LeftCircleRowEdge2.Dispose();
            hv_LeftCircleColumnEdge2.Dispose();
            hv_RightCenterRow.Dispose();
            hv_RightCenterCol.Dispose();
            hv_LeftCenterRow.Dispose();
            hv_LeftCenterCol.Dispose();
            hv_L1_L2.Dispose();
            hv_RightRowEdge1.Dispose();
            hv_RightColumnEdge1.Dispose();
            hv_RightRowEdge2.Dispose();
            hv_RightColumnEdge2.Dispose();
            hv_L3CenterRow.Dispose();
            hv_L3CenterCol.Dispose();
            hv_L3StartRow.Dispose();
            hv_L3StartCol.Dispose();
            hv_L3EndRow.Dispose();
            hv_L3EndCol.Dispose();
            hv_X1.Dispose();
            hv_StopTime.Dispose();
            hv_runtime.Dispose();
            hv_distanceL1L2.Dispose();
            hv_distanceX1.Dispose();
        }

        public static void Measure_33(HObject ho_Image, out double Radius, out double PositionDegree, 
            out double DistanceX1, out double DistanceY1, out double RunTime)
        {
            // Local iconic variables 
            HObject ho_SmallCircle;
            // Local control variables 
            HTuple hv_Width = new HTuple(), hv_Height = new HTuple();
            HTuple hv_StartTime = new HTuple(), hv_MaxCircleRow = new HTuple();
            HTuple hv_MaxCircleColumn = new HTuple(), hv_MaxCircleRadius = new HTuple();
            HTuple hv_MinCircleRow = new HTuple(), hv_MinCircleColumn = new HTuple();
            HTuple hv_MinCircleRadius = new HTuple(), hv_TwoCirclePhi = new HTuple();
            HTuple hv_LeftTopRowEdge = new HTuple(), hv_LeftTopColumnEdge = new HTuple();
            HTuple hv_RightButtomRowEdge = new HTuple(), hv_RightButtomColumnEdge = new HTuple();
            HTuple hv_LeftButtomRowEdge = new HTuple(), hv_LeftButtomColumnEdge = new HTuple();
            HTuple hv_RightTopRowEdge = new HTuple(), hv_RightTopColumnEdge = new HTuple();
            HTuple hv_FitCircleCenterRow = new HTuple(), hv_FitCircleCenterCol = new HTuple();
            HTuple hv_FitCircleCenterRadius = new HTuple(), hv_ButtomRowEdge1 = new HTuple();
            HTuple hv_ButtomColumnEdge1 = new HTuple(), hv_ButtomRowEdge2 = new HTuple();
            HTuple hv_ButtomColumnEdge2 = new HTuple(), hv_RightRowEdge1 = new HTuple();
            HTuple hv_RightColumnEdge1 = new HTuple(), hv_RightRowEdge2 = new HTuple();
            HTuple hv_RightColumnEdge2 = new HTuple(), hv_X1 = new HTuple();
            HTuple hv_Y1 = new HTuple(), hv_position_degree = new HTuple();
            HTuple hv_StopTime = new HTuple(), hv_CircleRadius = new HTuple();
            HTuple hv_PositionDegree = new HTuple(), hv_RunTime = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_SmallCircle);
            hv_Width.Dispose(); hv_Height.Dispose();
            HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.SetDraw(HDevWindowStack.GetActive(), "margin");
            }
            hv_StartTime.Dispose();
            HOperatorSet.CountSeconds(out hv_StartTime);
            hv_MaxCircleRow.Dispose(); hv_MaxCircleColumn.Dispose(); hv_MaxCircleRadius.Dispose(); hv_MinCircleRow.Dispose(); hv_MinCircleColumn.Dispose(); hv_MinCircleRadius.Dispose(); hv_TwoCirclePhi.Dispose();
            gen_TwoCircle_info(ho_Image, out hv_MaxCircleRow, out hv_MaxCircleColumn, out hv_MaxCircleRadius,
                out hv_MinCircleRow, out hv_MinCircleColumn, out hv_MinCircleRadius, out hv_TwoCirclePhi);
            //生成基准矩形
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                ho_SmallCircle.Dispose(); hv_LeftTopRowEdge.Dispose(); hv_LeftTopColumnEdge.Dispose(); hv_RightButtomRowEdge.Dispose(); hv_RightButtomColumnEdge.Dispose(); hv_LeftButtomRowEdge.Dispose(); hv_LeftButtomColumnEdge.Dispose(); hv_RightTopRowEdge.Dispose(); hv_RightTopColumnEdge.Dispose(); hv_FitCircleCenterRow.Dispose(); hv_FitCircleCenterCol.Dispose(); hv_FitCircleCenterRadius.Dispose();
                gen_Edge_Circle(ho_Image, out ho_SmallCircle, hv_MinCircleRow, hv_MinCircleColumn,
                    hv_TwoCirclePhi, hv_Width, hv_Height, (new HTuple(45)).TupleRad(), hv_MinCircleRadius,
                    out hv_LeftTopRowEdge, out hv_LeftTopColumnEdge, out hv_RightButtomRowEdge,
                    out hv_RightButtomColumnEdge, out hv_LeftButtomRowEdge, out hv_LeftButtomColumnEdge,
                    out hv_RightTopRowEdge, out hv_RightTopColumnEdge, out hv_FitCircleCenterRow,
                    out hv_FitCircleCenterCol, out hv_FitCircleCenterRadius);
            }
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_SmallCircle, HDevWindowStack.GetActive());
            }
            //定位底部测量矩形
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_ButtomRowEdge1.Dispose(); hv_ButtomColumnEdge1.Dispose(); hv_ButtomRowEdge2.Dispose(); hv_ButtomColumnEdge2.Dispose();
                gen_buttom_edge(ho_Image, hv_MaxCircleRow, hv_MaxCircleColumn, (hv_MaxCircleRadius * 3) / 2,
                    (hv_MaxCircleRadius * 3) / 4, hv_TwoCirclePhi, hv_Width, hv_Height, (new HTuple(90)).TupleRad()
                    , (new HTuple(90)).TupleRad(), hv_MinCircleColumn, hv_MaxCircleColumn, out hv_ButtomRowEdge1,
                    out hv_ButtomColumnEdge1, out hv_ButtomRowEdge2, out hv_ButtomColumnEdge2);
            }

            //定位右侧测量矩形
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_RightRowEdge1.Dispose(); hv_RightColumnEdge1.Dispose(); hv_RightRowEdge2.Dispose(); hv_RightColumnEdge2.Dispose();
                gen_right_edge(ho_Image, hv_MinCircleRow, hv_MinCircleColumn, (hv_MinCircleRadius * 3) / 4,
                    (hv_MinCircleRadius * 5) / 4, hv_MaxCircleColumn, hv_TwoCirclePhi, hv_Width,
                    hv_Height, (new HTuple(180)).TupleRad(), (new HTuple(0)).TupleRad(), out hv_RightRowEdge1,
                    out hv_RightColumnEdge1, out hv_RightRowEdge2, out hv_RightColumnEdge2);
            }

            //计算X1
            hv_X1.Dispose();
            HOperatorSet.DistancePl(hv_FitCircleCenterRow, hv_FitCircleCenterCol, hv_RightRowEdge1,
                hv_RightColumnEdge1, hv_RightRowEdge2, hv_RightColumnEdge2, out hv_X1);
            //计算Y1
            hv_Y1.Dispose();
            HOperatorSet.DistancePl(hv_FitCircleCenterRow, hv_FitCircleCenterCol, hv_ButtomRowEdge1,
                hv_ButtomColumnEdge1, hv_ButtomRowEdge2, hv_ButtomColumnEdge2, out hv_Y1);
            //计算基准度
            hv_position_degree.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_position_degree = 2 * ((((((5.325 - hv_X1)).TuplePow(
                    2)) + (((6.788 - hv_Y1)).TuplePow(2)))).TupleSqrt());
            }
            //计算运行时间
            hv_StopTime.Dispose();
            HOperatorSet.CountSeconds(out hv_StopTime);
            //输出结果
            hv_CircleRadius.Dispose();
            hv_CircleRadius = new HTuple(hv_FitCircleCenterRadius);
            Radius = hv_CircleRadius*2;
            hv_PositionDegree.Dispose();
            hv_PositionDegree = new HTuple(hv_position_degree);
            PositionDegree = hv_PositionDegree;
            hv_RunTime.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_RunTime = (hv_StopTime - hv_StartTime) * 1000;
                RunTime = hv_RunTime;
            }
            DistanceX1 = hv_X1;
            DistanceY1 = hv_Y1;

            DispImage(ho_Image);
            outWindow.SetColor("red");
            outWindow.SetLineWidth(2);
            outWindow.SetDraw("margin");
            outWindow.DispCircle(hv_FitCircleCenterRow, hv_FitCircleCenterCol, hv_FitCircleCenterRadius);
            outWindow.DispLine(hv_ButtomRowEdge1, hv_ButtomColumnEdge1, hv_ButtomRowEdge2, hv_ButtomColumnEdge2);
            outWindow.DispLine(hv_RightRowEdge1, hv_RightColumnEdge1, hv_RightRowEdge2, hv_RightColumnEdge2);


            ho_Image.Dispose();
            ho_SmallCircle.Dispose();

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
            hv_LeftTopRowEdge.Dispose();
            hv_LeftTopColumnEdge.Dispose();
            hv_RightButtomRowEdge.Dispose();
            hv_RightButtomColumnEdge.Dispose();
            hv_LeftButtomRowEdge.Dispose();
            hv_LeftButtomColumnEdge.Dispose();
            hv_RightTopRowEdge.Dispose();
            hv_RightTopColumnEdge.Dispose();
            hv_FitCircleCenterRow.Dispose();
            hv_FitCircleCenterCol.Dispose();
            hv_FitCircleCenterRadius.Dispose();
            hv_ButtomRowEdge1.Dispose();
            hv_ButtomColumnEdge1.Dispose();
            hv_ButtomRowEdge2.Dispose();
            hv_ButtomColumnEdge2.Dispose();
            hv_RightRowEdge1.Dispose();
            hv_RightColumnEdge1.Dispose();
            hv_RightRowEdge2.Dispose();
            hv_RightColumnEdge2.Dispose();
            hv_X1.Dispose();
            hv_Y1.Dispose();
            hv_position_degree.Dispose();
            hv_StopTime.Dispose();
            hv_CircleRadius.Dispose();
            hv_PositionDegree.Dispose();
            hv_RunTime.Dispose();
        }

        public static void Measure_34(HObject ho_Image, out double RunTime, out double DistanceL1L2, out double DistanceY1)
        {
            // Local control variables 
            HTuple hv_Width = new HTuple(), hv_Height = new HTuple();
            HTuple hv_StartTime = new HTuple(), hv_MaxCircleRow = new HTuple();
            HTuple hv_MaxCircleColumn = new HTuple(), hv_MaxCircleRadius = new HTuple();
            HTuple hv_MinCircleRow = new HTuple(), hv_MinCircleColumn = new HTuple();
            HTuple hv_MinCircleRadius = new HTuple(), hv_TwoCirclePhi = new HTuple();
            HTuple hv_RightCircleRowEdge1 = new HTuple(), hv_RightCircleColumnEdge1 = new HTuple();
            HTuple hv_LeftCircleRowEdge1 = new HTuple(), hv_LeftCircleColumnEdge1 = new HTuple();
            HTuple hv_RightCircleRowEdge2 = new HTuple(), hv_RightCircleColumnEdge2 = new HTuple();
            HTuple hv_LeftCircleRowEdge2 = new HTuple(), hv_LeftCircleColumnEdge2 = new HTuple();
            HTuple hv_RightCenterRow = new HTuple(), hv_RightCenterCol = new HTuple();
            HTuple hv_LeftCenterRow = new HTuple(), hv_LeftCenterCol = new HTuple();
            HTuple hv_L1_L2 = new HTuple(), hv_ButtomRowEdge1 = new HTuple();
            HTuple hv_ButtomColumnEdge1 = new HTuple(), hv_ButtomRowEdge2 = new HTuple();
            HTuple hv_ButtomColumnEdge2 = new HTuple(), hv_L3CenterRow = new HTuple();
            HTuple hv_L3CenterCol = new HTuple(), hv_L3StartRow = new HTuple();
            HTuple hv_L3StartCol = new HTuple(), hv_L3EndRow = new HTuple();
            HTuple hv_L3EndCol = new HTuple(), hv_Y1 = new HTuple();
            HTuple hv_StopTime = new HTuple(), hv_runtime = new HTuple();
            HTuple hv_distanceY1 = new HTuple(), hv_distanceL1L2 = new HTuple();
            // Initialize local and output iconic variables 
            hv_Width.Dispose(); hv_Height.Dispose();
            HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
            hv_StartTime.Dispose();
            HOperatorSet.CountSeconds(out hv_StartTime);
            hv_MaxCircleRow.Dispose(); hv_MaxCircleColumn.Dispose(); hv_MaxCircleRadius.Dispose(); hv_MinCircleRow.Dispose(); hv_MinCircleColumn.Dispose(); hv_MinCircleRadius.Dispose(); hv_TwoCirclePhi.Dispose();
            gen_TwoCircle_info(ho_Image, out hv_MaxCircleRow, out hv_MaxCircleColumn, out hv_MaxCircleRadius,
                out hv_MinCircleRow, out hv_MinCircleColumn, out hv_MinCircleRadius, out hv_TwoCirclePhi);
            hv_RightCircleRowEdge1.Dispose(); hv_RightCircleColumnEdge1.Dispose(); hv_LeftCircleRowEdge1.Dispose(); hv_LeftCircleColumnEdge1.Dispose(); hv_RightCircleRowEdge2.Dispose(); hv_RightCircleColumnEdge2.Dispose(); hv_LeftCircleRowEdge2.Dispose(); hv_LeftCircleColumnEdge2.Dispose();
            gen_L1L2_Measure34(ho_Image, hv_MinCircleRow, hv_MinCircleColumn, hv_MinCircleRadius,
                hv_TwoCirclePhi, hv_Width, hv_Height, out hv_RightCircleRowEdge1, out hv_RightCircleColumnEdge1,
                out hv_LeftCircleRowEdge1, out hv_LeftCircleColumnEdge1, out hv_RightCircleRowEdge2,
                out hv_RightCircleColumnEdge2, out hv_LeftCircleRowEdge2, out hv_LeftCircleColumnEdge2);
            //分别计算L1、L2的中心坐标
            hv_RightCenterRow.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_RightCenterRow = (hv_RightCircleRowEdge1 + hv_RightCircleRowEdge2) / 2;
            }
            hv_RightCenterCol.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_RightCenterCol = (hv_RightCircleColumnEdge1 + hv_RightCircleColumnEdge2) / 2;
            }
            hv_LeftCenterRow.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_LeftCenterRow = (hv_LeftCircleRowEdge1 + hv_LeftCircleRowEdge2) / 2;
            }
            hv_LeftCenterCol.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_LeftCenterCol = (hv_LeftCircleColumnEdge1 + hv_LeftCircleColumnEdge2) / 2;
            }
            //计算L1、L2之间距离
            hv_L1_L2.Dispose();
            HOperatorSet.DistancePp(hv_RightCenterRow, hv_RightCenterCol, hv_LeftCenterRow,
                hv_LeftCenterCol, out hv_L1_L2);
            //定位底部测量矩形
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_ButtomRowEdge1.Dispose(); hv_ButtomColumnEdge1.Dispose(); hv_ButtomRowEdge2.Dispose(); hv_ButtomColumnEdge2.Dispose();
                gen_buttom_edge(ho_Image, hv_MaxCircleRow, hv_MaxCircleColumn, (hv_MaxCircleRadius * 3) / 2,
                    (hv_MaxCircleRadius * 3) / 4, hv_TwoCirclePhi, hv_Width, hv_Height, (new HTuple(90)).TupleRad()
                    , (new HTuple(90)).TupleRad(), hv_MinCircleColumn, hv_MaxCircleColumn, out hv_ButtomRowEdge1,
                    out hv_ButtomColumnEdge1, out hv_ButtomRowEdge2, out hv_ButtomColumnEdge2);
            }
            //计算L3中心坐标和起点、终点坐标
            hv_L3CenterRow.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_L3CenterRow = (hv_RightCenterRow + hv_LeftCenterRow) / 2;
            }
            hv_L3CenterCol.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_L3CenterCol = (hv_RightCenterCol + hv_LeftCenterCol) / 2;
            }
            hv_L3StartRow.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_L3StartRow = (hv_RightCircleRowEdge1 + hv_LeftCircleRowEdge2) / 2;
            }
            hv_L3StartCol.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_L3StartCol = (hv_RightCircleColumnEdge1 + hv_LeftCircleColumnEdge2) / 2;
            }
            hv_L3EndRow.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_L3EndRow = (hv_RightCircleRowEdge2 + hv_LeftCircleRowEdge1) / 2;
            }
            hv_L3EndCol.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_L3EndCol = (hv_RightCircleColumnEdge2 + hv_LeftCircleColumnEdge1) / 2;
            }
            //计算X1
            hv_Y1.Dispose();
            HOperatorSet.DistancePl(hv_L3CenterRow, hv_L3CenterCol, hv_ButtomRowEdge1, hv_ButtomColumnEdge1,
                hv_ButtomRowEdge2, hv_ButtomColumnEdge2, out hv_Y1);
            //计算运行时间
            hv_StopTime.Dispose();
            HOperatorSet.CountSeconds(out hv_StopTime);
            //输出结果
            hv_runtime.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_runtime = (hv_StopTime - hv_StartTime) * 1000;
                RunTime = hv_runtime;
            }
            hv_distanceY1.Dispose();
            hv_distanceY1 = new HTuple(hv_Y1);
            DistanceY1 = hv_distanceY1;
            hv_distanceL1L2.Dispose();
            hv_distanceL1L2 = new HTuple(hv_L1_L2);
            DistanceL1L2 = hv_distanceL1L2;

            DispImage(ho_Image);
            outWindow.SetColor("red");
            outWindow.SetLineWidth(2);
            outWindow.DispLine(hv_RightCenterRow, hv_RightCenterCol, hv_LeftCenterRow, hv_LeftCenterCol);
            outWindow.DispLine(hv_L3StartRow, hv_L3StartCol, hv_L3EndRow, hv_L3EndCol);
            outWindow.DispLine(hv_ButtomRowEdge1, hv_ButtomColumnEdge1, hv_ButtomRowEdge2, hv_ButtomColumnEdge2);

            ho_Image.Dispose();

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
            hv_RightCircleRowEdge1.Dispose();
            hv_RightCircleColumnEdge1.Dispose();
            hv_LeftCircleRowEdge1.Dispose();
            hv_LeftCircleColumnEdge1.Dispose();
            hv_RightCircleRowEdge2.Dispose();
            hv_RightCircleColumnEdge2.Dispose();
            hv_LeftCircleRowEdge2.Dispose();
            hv_LeftCircleColumnEdge2.Dispose();
            hv_RightCenterRow.Dispose();
            hv_RightCenterCol.Dispose();
            hv_LeftCenterRow.Dispose();
            hv_LeftCenterCol.Dispose();
            hv_L1_L2.Dispose();
            hv_ButtomRowEdge1.Dispose();
            hv_ButtomColumnEdge1.Dispose();
            hv_ButtomRowEdge2.Dispose();
            hv_ButtomColumnEdge2.Dispose();
            hv_L3CenterRow.Dispose();
            hv_L3CenterCol.Dispose();
            hv_L3StartRow.Dispose();
            hv_L3StartCol.Dispose();
            hv_L3EndRow.Dispose();
            hv_L3EndCol.Dispose();
            hv_Y1.Dispose();
            hv_StopTime.Dispose();
            hv_runtime.Dispose();
            hv_distanceY1.Dispose();
            hv_distanceL1L2.Dispose();
        }

        public static void Measure_46(HObject ho_Image, out double RunTime, out double DistanceY)
        {
            // Local control variables 
            HTuple hv_Width = new HTuple(), hv_Height = new HTuple();
            HTuple hv_StartTime = new HTuple(), hv_MaxCircleRow = new HTuple();
            HTuple hv_MaxCircleColumn = new HTuple(), hv_MaxCircleRadius = new HTuple();
            HTuple hv_MinCircleRow = new HTuple(), hv_MinCircleColumn = new HTuple();
            HTuple hv_MinCircleRadius = new HTuple(), hv_TwoCirclePhi = new HTuple();
            HTuple hv_P1Row = new HTuple(), hv_P1Col = new HTuple();
            HTuple hv_P2Row = new HTuple(), hv_P2Col = new HTuple();
            HTuple hv_P3Row = new HTuple(), hv_P3Col = new HTuple();
            HTuple hv_ButtomRowEdge1 = new HTuple(), hv_ButtomColumnEdge1 = new HTuple();
            HTuple hv_ButtomRowEdge2 = new HTuple(), hv_ButtomColumnEdge2 = new HTuple();
            HTuple hv_Y1 = new HTuple(), hv_Y2 = new HTuple(), hv_Y3 = new HTuple();
            HTuple hv_StopTime = new HTuple(), hv_runtime = new HTuple();
            HTuple hv_distanceY = new HTuple();
            // Initialize local and output iconic variables 
             hv_Width.Dispose(); hv_Height.Dispose();
            HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
            hv_StartTime.Dispose();
            HOperatorSet.CountSeconds(out hv_StartTime);
            hv_MaxCircleRow.Dispose(); hv_MaxCircleColumn.Dispose(); hv_MaxCircleRadius.Dispose(); hv_MinCircleRow.Dispose(); hv_MinCircleColumn.Dispose(); hv_MinCircleRadius.Dispose(); hv_TwoCirclePhi.Dispose();
            gen_TwoCircle_info(ho_Image, out hv_MaxCircleRow, out hv_MaxCircleColumn, out hv_MaxCircleRadius,
                out hv_MinCircleRow, out hv_MinCircleColumn, out hv_MinCircleRadius, out hv_TwoCirclePhi);
            hv_P1Row.Dispose(); hv_P1Col.Dispose(); hv_P2Row.Dispose(); hv_P2Col.Dispose(); hv_P3Row.Dispose(); hv_P3Col.Dispose();
            gen_p1p2p3_Measure46(ho_Image, hv_MinCircleRow, hv_MinCircleColumn, hv_MinCircleRadius,
                hv_MaxCircleColumn, hv_TwoCirclePhi, hv_Width, hv_Height, out hv_P1Row, out hv_P1Col,
                out hv_P2Row, out hv_P2Col, out hv_P3Row, out hv_P3Col);
            //定位底部测量矩形
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_ButtomRowEdge1.Dispose(); hv_ButtomColumnEdge1.Dispose(); hv_ButtomRowEdge2.Dispose(); hv_ButtomColumnEdge2.Dispose();
                gen_buttom_edge(ho_Image, hv_MaxCircleRow, hv_MaxCircleColumn, (hv_MaxCircleRadius * 3) / 2,
                    (hv_MaxCircleRadius * 3) / 4, hv_TwoCirclePhi, hv_Width, hv_Height, (new HTuple(90)).TupleRad()
                    , (new HTuple(90)).TupleRad(), hv_MinCircleColumn, hv_MaxCircleColumn, out hv_ButtomRowEdge1,
                    out hv_ButtomColumnEdge1, out hv_ButtomRowEdge2, out hv_ButtomColumnEdge2);
            }
            //计算三点分别到底部边缘距离
            hv_Y1.Dispose();
            HOperatorSet.DistancePl(hv_P1Row, hv_P1Col, hv_ButtomRowEdge1, hv_ButtomColumnEdge1,
                hv_ButtomRowEdge2, hv_ButtomColumnEdge2, out hv_Y1);
            hv_Y2.Dispose();
            HOperatorSet.DistancePl(hv_P2Row, hv_P2Col, hv_ButtomRowEdge1, hv_ButtomColumnEdge1,
                hv_ButtomRowEdge2, hv_ButtomColumnEdge2, out hv_Y2);
            hv_Y3.Dispose();
            HOperatorSet.DistancePl(hv_P3Row, hv_P3Col, hv_ButtomRowEdge1, hv_ButtomColumnEdge1,
                hv_ButtomRowEdge2, hv_ButtomColumnEdge2, out hv_Y3);
            //计算运行时间
            hv_StopTime.Dispose();
            HOperatorSet.CountSeconds(out hv_StopTime);
            //输出结果
            hv_runtime.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_runtime = (hv_StopTime - hv_StartTime) * 1000;
                RunTime = hv_runtime;
            }
            hv_distanceY.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_distanceY = ((hv_Y1 + hv_Y2) + hv_Y3) / 3;
                DistanceY = hv_distanceY;
            }

            DispImage(ho_Image);
            outWindow.SetColor("red");
            outWindow.SetLineWidth(2);
            outWindow.DispLine(hv_ButtomRowEdge1, hv_ButtomColumnEdge1, hv_ButtomRowEdge2, hv_ButtomColumnEdge2);
            outWindow.DispCircle(hv_P1Row, hv_P1Col, 20);
            outWindow.DispCircle(hv_P2Row, hv_P2Col, 20);
            outWindow.DispCircle(hv_P3Row, hv_P3Col, 20);

            ho_Image.Dispose();

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
            hv_P1Row.Dispose();
            hv_P1Col.Dispose();
            hv_P2Row.Dispose();
            hv_P2Col.Dispose();
            hv_P3Row.Dispose();
            hv_P3Col.Dispose();
            hv_ButtomRowEdge1.Dispose();
            hv_ButtomColumnEdge1.Dispose();
            hv_ButtomRowEdge2.Dispose();
            hv_ButtomColumnEdge2.Dispose();
            hv_Y1.Dispose();
            hv_Y2.Dispose();
            hv_Y3.Dispose();
            hv_StopTime.Dispose();
            hv_runtime.Dispose();
            hv_distanceY.Dispose();
        }

        private static void gen_buttom_edge(HObject ho_Image, HTuple hv_CenterRow, HTuple hv_CenterCol,
    HTuple hv_BaseRectDistRow, HTuple hv_BaseRectDistCol, HTuple hv_BasePhi, HTuple hv_ImageWidth,
    HTuple hv_ImageHeight, HTuple hv_RotatePhi, HTuple hv_RectBasePhi, HTuple hv_MinCircleColumn,
    HTuple hv_MaxCircleColumn, out HTuple hv_ButtomRowEdge1, out HTuple hv_ButtomColumnEdge1,
    out HTuple hv_ButtomRowEdge2, out HTuple hv_ButtomColumnEdge2)
        {




            // Local iconic variables 

            HObject ho_Rectangle1, ho_Rectangle2, ho_RectButtomMeasureRectionAffineTrans1 = null;
            HObject ho_ButtomMeasureRectRegionAffineTrans2 = null;

            // Local control variables 

            HTuple hv_HomMat2D5 = new HTuple(), hv_RectButtomMeasureRectArea = new HTuple();
            HTuple hv_RectButtomMeasureRectRow1 = new HTuple(), hv_RectButtomMeasureRectColumn1 = new HTuple();
            HTuple hv_RectButtomMeasureRectPhi1 = new HTuple(), hv_MeasureHandle5 = new HTuple();
            HTuple hv_HomMat2D6 = new HTuple(), hv_ButtomMeasureRectArea = new HTuple();
            HTuple hv_ButtomMeasureRectRow2 = new HTuple(), hv_ButtomMeasureRectColumn2 = new HTuple();
            HTuple hv_ButtomMeasureRectPhi2 = new HTuple(), hv_MeasureHandle6 = new HTuple();
            HTuple hv_ButtomAmplitude = new HTuple(), hv_ButtomDistance = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Rectangle1);
            HOperatorSet.GenEmptyObj(out ho_Rectangle2);
            HOperatorSet.GenEmptyObj(out ho_RectButtomMeasureRectionAffineTrans1);
            HOperatorSet.GenEmptyObj(out ho_ButtomMeasureRectRegionAffineTrans2);
            hv_ButtomRowEdge1 = new HTuple();
            hv_ButtomColumnEdge1 = new HTuple();
            hv_ButtomRowEdge2 = new HTuple();
            hv_ButtomColumnEdge2 = new HTuple();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                ho_Rectangle1.Dispose();
                HOperatorSet.GenRectangle2(out ho_Rectangle1, hv_CenterRow + hv_BaseRectDistRow,
                    hv_CenterCol - hv_BaseRectDistCol, hv_RectBasePhi, 100, 50);
            }
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                ho_Rectangle2.Dispose();
                HOperatorSet.GenRectangle2(out ho_Rectangle2, hv_CenterRow + hv_BaseRectDistRow,
                    hv_CenterCol + hv_BaseRectDistCol, hv_RectBasePhi, 100, 50);
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
                    hv_RectButtomMeasureRectPhi1, 100, 50, hv_ImageWidth, hv_ImageHeight, "bicubic",
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
                    hv_ButtomMeasureRectPhi2, 100, 50, hv_ImageWidth, hv_ImageHeight, "bicubic",
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
                    hv_RectButtomMeasureRectPhi1, 100, 50, hv_ImageWidth, hv_ImageHeight, "bicubic",
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
                    hv_ButtomMeasureRectPhi2, 100, 50, hv_ImageWidth, hv_ImageHeight, "bicubic",
                    out hv_MeasureHandle6);
            }
            //抓取底部边缘
            hv_ButtomRowEdge1.Dispose(); hv_ButtomColumnEdge1.Dispose(); hv_ButtomAmplitude.Dispose(); hv_ButtomDistance.Dispose();
            HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle5, 1, 30, "all", "all", out hv_ButtomRowEdge1,
                out hv_ButtomColumnEdge1, out hv_ButtomAmplitude, out hv_ButtomDistance);
            hv_ButtomRowEdge2.Dispose(); hv_ButtomColumnEdge2.Dispose(); hv_ButtomAmplitude.Dispose(); hv_ButtomDistance.Dispose();
            HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle6, 1, 30, "all", "all", out hv_ButtomRowEdge2,
                out hv_ButtomColumnEdge2, out hv_ButtomAmplitude, out hv_ButtomDistance);
            HOperatorSet.CloseMeasure(hv_MeasureHandle5);
            HOperatorSet.CloseMeasure(hv_MeasureHandle6);
            ho_Rectangle1.Dispose();
            ho_Rectangle2.Dispose();
            ho_RectButtomMeasureRectionAffineTrans1.Dispose();
            ho_ButtomMeasureRectRegionAffineTrans2.Dispose();

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

            return;
        }

        private static void gen_Edge_Circle(HObject ho_Image, out HObject ho_Circle, HTuple hv_CenterRow,
            HTuple hv_CenterCol, HTuple hv_BasePhi, HTuple hv_ImageWidth, HTuple hv_ImageHeight,
            HTuple hv_RotatePhi, HTuple hv_BaseRectDist, out HTuple hv_LeftTopRowEdge, out HTuple hv_LeftTopColumnEdge,
            out HTuple hv_RightButtomRowEdge, out HTuple hv_RightButtomColumnEdge, out HTuple hv_LeftButtomRowEdge,
            out HTuple hv_LeftButtomColumnEdge, out HTuple hv_RightTopRowEdge, out HTuple hv_RightTopColumnEdge,
            out HTuple hv_FitCircleCenterRow, out HTuple hv_FitCircleCenterCol, out HTuple hv_FitCircleCenterRadius)
        {
            // Local iconic variables 
            HObject ho_Rect, ho_RightTopRectAffineTrans;
            HObject ho_LeftTopRectAffineTrans, ho_LeftButtomRectAffineTrans;
            HObject ho_RightButtomRectAffineTrans, ho_BigCircleContour;
            // Local control variables 
            HTuple hv_HomMat2D1 = new HTuple(), hv_RightTopRectArea = new HTuple();
            HTuple hv_RightTopRectRow = new HTuple(), hv_RightTopRectColumn = new HTuple();
            HTuple hv_RightTopRectPhi = new HTuple(), hv_MeasureHandle1 = new HTuple();
            HTuple hv_HomMat2D2 = new HTuple(), hv_LeftTopRectArea = new HTuple();
            HTuple hv_LeftTopRectRow = new HTuple(), hv_LeftTopRectColumn = new HTuple();
            HTuple hv_LeftTopRectPhi = new HTuple(), hv_MeasureHandle2 = new HTuple();
            HTuple hv_HomMat2D3 = new HTuple(), hv_LeftButtomRectArea = new HTuple();
            HTuple hv_LeftButtomRectRow = new HTuple(), hv_LeftButtomRectColumn = new HTuple();
            HTuple hv_LeftButtomRectPhi = new HTuple(), hv_MeasureHandle3 = new HTuple();
            HTuple hv_HomMat2D4 = new HTuple(), hv_RightButtomRectArea = new HTuple();
            HTuple hv_RightButtomRectRow = new HTuple(), hv_RightButtomRectColumn = new HTuple();
            HTuple hv_RightButtomRectPhi = new HTuple(), hv_MeasureHandle4 = new HTuple();
            HTuple hv_LeftTopAmplitude = new HTuple(), hv_LeftTopDistance = new HTuple();
            HTuple hv_RightButtomAmplitude = new HTuple(), hv_RightButtomDistance = new HTuple();
            HTuple hv_LeftButtomAmplitude = new HTuple(), hv_LeftButtomDistance = new HTuple();
            HTuple hv_RightTopAmplitude = new HTuple(), hv_RightTopDistance = new HTuple();
            HTuple hv_FitCircleCenterStartPhi = new HTuple(), hv_FitCircleCenterEndPhi = new HTuple();
            HTuple hv_FitCircleCenterPointOrder = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Circle);
            HOperatorSet.GenEmptyObj(out ho_Rect);
            HOperatorSet.GenEmptyObj(out ho_RightTopRectAffineTrans);
            HOperatorSet.GenEmptyObj(out ho_LeftTopRectAffineTrans);
            HOperatorSet.GenEmptyObj(out ho_LeftButtomRectAffineTrans);
            HOperatorSet.GenEmptyObj(out ho_RightButtomRectAffineTrans);
            HOperatorSet.GenEmptyObj(out ho_BigCircleContour);
            hv_LeftTopRowEdge = new HTuple();
            hv_LeftTopColumnEdge = new HTuple();
            hv_RightButtomRowEdge = new HTuple();
            hv_RightButtomColumnEdge = new HTuple();
            hv_LeftButtomRowEdge = new HTuple();
            hv_LeftButtomColumnEdge = new HTuple();
            hv_RightTopRowEdge = new HTuple();
            hv_RightTopColumnEdge = new HTuple();
            hv_FitCircleCenterRow = new HTuple();
            hv_FitCircleCenterCol = new HTuple();
            hv_FitCircleCenterRadius = new HTuple();
            //生成基准矩形
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                ho_Rect.Dispose();
                HOperatorSet.GenRectangle2(out ho_Rect, hv_CenterRow, hv_CenterCol + hv_BaseRectDist,
                    0, 100, 50);
            }
            //生成测量矩形
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_HomMat2D1.Dispose();
                HOperatorSet.VectorAngleToRigid(hv_CenterRow, hv_CenterCol, 0, hv_CenterRow,
                    hv_CenterCol, hv_BasePhi + hv_RotatePhi, out hv_HomMat2D1);
            }
            ho_RightTopRectAffineTrans.Dispose();
            HOperatorSet.AffineTransRegion(ho_Rect, out ho_RightTopRectAffineTrans, hv_HomMat2D1,
                "nearest_neighbor");
            hv_RightTopRectArea.Dispose(); hv_RightTopRectRow.Dispose(); hv_RightTopRectColumn.Dispose();
            HOperatorSet.AreaCenter(ho_RightTopRectAffineTrans, out hv_RightTopRectArea,
                out hv_RightTopRectRow, out hv_RightTopRectColumn);
            hv_RightTopRectPhi.Dispose();
            HOperatorSet.OrientationRegion(ho_RightTopRectAffineTrans, out hv_RightTopRectPhi);
            hv_MeasureHandle1.Dispose();
            HOperatorSet.GenMeasureRectangle2(hv_RightTopRectRow, hv_RightTopRectColumn,
                hv_RightTopRectPhi, 100, 50, hv_ImageWidth, hv_ImageHeight, "bicubic", out hv_MeasureHandle1);

            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_HomMat2D2.Dispose();
                HOperatorSet.VectorAngleToRigid(hv_CenterRow, hv_CenterCol, 0, hv_CenterRow,
                    hv_CenterCol, (hv_BasePhi + hv_RotatePhi) + ((new HTuple(90)).TupleRad()), out hv_HomMat2D2);
            }
            ho_LeftTopRectAffineTrans.Dispose();
            HOperatorSet.AffineTransRegion(ho_Rect, out ho_LeftTopRectAffineTrans, hv_HomMat2D2,
                "nearest_neighbor");
            hv_LeftTopRectArea.Dispose(); hv_LeftTopRectRow.Dispose(); hv_LeftTopRectColumn.Dispose();
            HOperatorSet.AreaCenter(ho_LeftTopRectAffineTrans, out hv_LeftTopRectArea, out hv_LeftTopRectRow,
                out hv_LeftTopRectColumn);
            hv_LeftTopRectPhi.Dispose();
            HOperatorSet.OrientationRegion(ho_LeftTopRectAffineTrans, out hv_LeftTopRectPhi);
            hv_MeasureHandle2.Dispose();
            HOperatorSet.GenMeasureRectangle2(hv_LeftTopRectRow, hv_LeftTopRectColumn, hv_LeftTopRectPhi,
                100, 50, hv_ImageWidth, hv_ImageHeight, "bicubic", out hv_MeasureHandle2);

            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_HomMat2D3.Dispose();
                HOperatorSet.VectorAngleToRigid(hv_CenterRow, hv_CenterCol, 0, hv_CenterRow,
                    hv_CenterCol, (hv_BasePhi + hv_RotatePhi) + ((new HTuple(180)).TupleRad()), out hv_HomMat2D3);
            }
            ho_LeftButtomRectAffineTrans.Dispose();
            HOperatorSet.AffineTransRegion(ho_Rect, out ho_LeftButtomRectAffineTrans, hv_HomMat2D3,
                "nearest_neighbor");
            hv_LeftButtomRectArea.Dispose(); hv_LeftButtomRectRow.Dispose(); hv_LeftButtomRectColumn.Dispose();
            HOperatorSet.AreaCenter(ho_LeftButtomRectAffineTrans, out hv_LeftButtomRectArea,
                out hv_LeftButtomRectRow, out hv_LeftButtomRectColumn);
            hv_LeftButtomRectPhi.Dispose();
            HOperatorSet.OrientationRegion(ho_LeftButtomRectAffineTrans, out hv_LeftButtomRectPhi);
            hv_MeasureHandle3.Dispose();
            HOperatorSet.GenMeasureRectangle2(hv_LeftButtomRectRow, hv_LeftButtomRectColumn,
                hv_LeftButtomRectPhi, 100, 50, hv_ImageWidth, hv_ImageHeight, "bicubic",
                out hv_MeasureHandle3);

            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_HomMat2D4.Dispose();
                HOperatorSet.VectorAngleToRigid(hv_CenterRow, hv_CenterCol, 0, hv_CenterRow,
                    hv_CenterCol, (hv_BasePhi + hv_RotatePhi) + ((new HTuple(270)).TupleRad()), out hv_HomMat2D4);
            }
            ho_RightButtomRectAffineTrans.Dispose();
            HOperatorSet.AffineTransRegion(ho_Rect, out ho_RightButtomRectAffineTrans, hv_HomMat2D4,
                "nearest_neighbor");
            hv_RightButtomRectArea.Dispose(); hv_RightButtomRectRow.Dispose(); hv_RightButtomRectColumn.Dispose();
            HOperatorSet.AreaCenter(ho_RightButtomRectAffineTrans, out hv_RightButtomRectArea,
                out hv_RightButtomRectRow, out hv_RightButtomRectColumn);
            hv_RightButtomRectPhi.Dispose();
            HOperatorSet.OrientationRegion(ho_RightButtomRectAffineTrans, out hv_RightButtomRectPhi);
            hv_MeasureHandle4.Dispose();
            HOperatorSet.GenMeasureRectangle2(hv_RightButtomRectRow, hv_RightButtomRectColumn,
                hv_RightButtomRectPhi, 100, 50, hv_ImageWidth, hv_ImageHeight, "bicubic",
                out hv_MeasureHandle4);
            //抓取大圆四个边缘点
            hv_LeftTopRowEdge.Dispose(); hv_LeftTopColumnEdge.Dispose(); hv_LeftTopAmplitude.Dispose(); hv_LeftTopDistance.Dispose();
            HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle2, 1, 60, "all", "all", out hv_LeftTopRowEdge,
                out hv_LeftTopColumnEdge, out hv_LeftTopAmplitude, out hv_LeftTopDistance);
            hv_RightButtomRowEdge.Dispose(); hv_RightButtomColumnEdge.Dispose(); hv_RightButtomAmplitude.Dispose(); hv_RightButtomDistance.Dispose();
            HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle4, 1, 60, "all", "all", out hv_RightButtomRowEdge,
                out hv_RightButtomColumnEdge, out hv_RightButtomAmplitude, out hv_RightButtomDistance);
            hv_LeftButtomRowEdge.Dispose(); hv_LeftButtomColumnEdge.Dispose(); hv_LeftButtomAmplitude.Dispose(); hv_LeftButtomDistance.Dispose();
            HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle3, 1, 60, "all", "all", out hv_LeftButtomRowEdge,
                out hv_LeftButtomColumnEdge, out hv_LeftButtomAmplitude, out hv_LeftButtomDistance);
            hv_RightTopRowEdge.Dispose(); hv_RightTopColumnEdge.Dispose(); hv_RightTopAmplitude.Dispose(); hv_RightTopDistance.Dispose();
            HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle1, 1, 60, "all", "all", out hv_RightTopRowEdge,
                out hv_RightTopColumnEdge, out hv_RightTopAmplitude, out hv_RightTopDistance);
            HOperatorSet.CloseMeasure(hv_MeasureHandle1);
            HOperatorSet.CloseMeasure(hv_MeasureHandle2);
            HOperatorSet.CloseMeasure(hv_MeasureHandle3);
            HOperatorSet.CloseMeasure(hv_MeasureHandle4);
            //计算大圆直径
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                ho_BigCircleContour.Dispose();
                HOperatorSet.GenContourPolygonXld(out ho_BigCircleContour, ((((hv_LeftTopRowEdge.TupleConcat(
                    hv_RightButtomRowEdge))).TupleConcat(hv_LeftButtomRowEdge))).TupleConcat(
                    hv_RightTopRowEdge), ((((hv_LeftTopColumnEdge.TupleConcat(hv_RightButtomColumnEdge))).TupleConcat(
                    hv_LeftButtomColumnEdge))).TupleConcat(hv_RightTopColumnEdge));
            }
            hv_FitCircleCenterRow.Dispose(); hv_FitCircleCenterCol.Dispose(); hv_FitCircleCenterRadius.Dispose(); hv_FitCircleCenterStartPhi.Dispose(); hv_FitCircleCenterEndPhi.Dispose(); hv_FitCircleCenterPointOrder.Dispose();
            HOperatorSet.FitCircleContourXld(ho_BigCircleContour, "geometric", -1, 0, 0,
                5, 2, out hv_FitCircleCenterRow, out hv_FitCircleCenterCol, out hv_FitCircleCenterRadius,
                out hv_FitCircleCenterStartPhi, out hv_FitCircleCenterEndPhi, out hv_FitCircleCenterPointOrder);
            ho_Circle.Dispose();
            HOperatorSet.GenCircle(out ho_Circle, hv_FitCircleCenterRow, hv_FitCircleCenterCol,
                hv_FitCircleCenterRadius);
            ho_Rect.Dispose();
            ho_RightTopRectAffineTrans.Dispose();
            ho_LeftTopRectAffineTrans.Dispose();
            ho_LeftButtomRectAffineTrans.Dispose();
            ho_RightButtomRectAffineTrans.Dispose();
            ho_BigCircleContour.Dispose();

            hv_HomMat2D1.Dispose();
            hv_RightTopRectArea.Dispose();
            hv_RightTopRectRow.Dispose();
            hv_RightTopRectColumn.Dispose();
            hv_RightTopRectPhi.Dispose();
            hv_MeasureHandle1.Dispose();
            hv_HomMat2D2.Dispose();
            hv_LeftTopRectArea.Dispose();
            hv_LeftTopRectRow.Dispose();
            hv_LeftTopRectColumn.Dispose();
            hv_LeftTopRectPhi.Dispose();
            hv_MeasureHandle2.Dispose();
            hv_HomMat2D3.Dispose();
            hv_LeftButtomRectArea.Dispose();
            hv_LeftButtomRectRow.Dispose();
            hv_LeftButtomRectColumn.Dispose();
            hv_LeftButtomRectPhi.Dispose();
            hv_MeasureHandle3.Dispose();
            hv_HomMat2D4.Dispose();
            hv_RightButtomRectArea.Dispose();
            hv_RightButtomRectRow.Dispose();
            hv_RightButtomRectColumn.Dispose();
            hv_RightButtomRectPhi.Dispose();
            hv_MeasureHandle4.Dispose();
            hv_LeftTopAmplitude.Dispose();
            hv_LeftTopDistance.Dispose();
            hv_RightButtomAmplitude.Dispose();
            hv_RightButtomDistance.Dispose();
            hv_LeftButtomAmplitude.Dispose();
            hv_LeftButtomDistance.Dispose();
            hv_RightTopAmplitude.Dispose();
            hv_RightTopDistance.Dispose();
            hv_FitCircleCenterStartPhi.Dispose();
            hv_FitCircleCenterEndPhi.Dispose();
            hv_FitCircleCenterPointOrder.Dispose();

            return;
        }

        private static void gen_right_edge(HObject ho_Image, HTuple hv_CenterRow, HTuple hv_CenterColumn,
            HTuple hv_BaseRectDistRow, HTuple hv_BaseRectDistColumn, HTuple hv_MaxCircleColumn,
            HTuple hv_BasePhi, HTuple hv_ImageWidth, HTuple hv_ImageHeight, HTuple hv_RotatePhi,
            HTuple hv_BaseRectPhi, out HTuple hv_RightRowEdge1, out HTuple hv_RightColumnEdge1,
            out HTuple hv_RightRowEdge2, out HTuple hv_RightColumnEdge2)
        {
            // Local iconic variables 
            HObject ho_Rectangle3, ho_Rectangle4, ho_RightMeasureRectionAffineTrans1 = null;
            HObject ho_RightMeasureRectionAffineTrans2 = null;
            // Local control variables 
            HTuple hv_HomMat2D7 = new HTuple(), hv_RightMeasureRectArea = new HTuple();
            HTuple hv_RightMeasureRectRow1 = new HTuple(), hv_RightMeasureRectColumn1 = new HTuple();
            HTuple hv_RightMeasureRectPhi1 = new HTuple(), hv_MeasureHandle7 = new HTuple();
            HTuple hv_RightMeasureRectRow2 = new HTuple(), hv_RightMeasureRectColumn2 = new HTuple();
            HTuple hv_RightMeasureRectPhi2 = new HTuple(), hv_MeasureHandle8 = new HTuple();
            HTuple hv_RightAmplitude = new HTuple(), hv_RightDistance = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Rectangle3);
            HOperatorSet.GenEmptyObj(out ho_Rectangle4);
            HOperatorSet.GenEmptyObj(out ho_RightMeasureRectionAffineTrans1);
            HOperatorSet.GenEmptyObj(out ho_RightMeasureRectionAffineTrans2);
            hv_RightRowEdge1 = new HTuple();
            hv_RightColumnEdge1 = new HTuple();
            hv_RightRowEdge2 = new HTuple();
            hv_RightColumnEdge2 = new HTuple();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                ho_Rectangle3.Dispose();
                HOperatorSet.GenRectangle2(out ho_Rectangle3, hv_CenterRow - hv_BaseRectDistRow,
                    hv_CenterColumn + hv_BaseRectDistColumn, hv_BaseRectPhi, 100, 50);
            }
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                ho_Rectangle4.Dispose();
                HOperatorSet.GenRectangle2(out ho_Rectangle4, hv_CenterRow + hv_BaseRectDistRow,
                    hv_CenterColumn + hv_BaseRectDistColumn, hv_BaseRectPhi, 100, 50);
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
                HOperatorSet.AffineTransRegion(ho_Rectangle3, out ho_RightMeasureRectionAffineTrans1,
                    hv_HomMat2D7, "nearest_neighbor");
                hv_RightMeasureRectArea.Dispose(); hv_RightMeasureRectRow1.Dispose(); hv_RightMeasureRectColumn1.Dispose();
                HOperatorSet.AreaCenter(ho_RightMeasureRectionAffineTrans1, out hv_RightMeasureRectArea,
                    out hv_RightMeasureRectRow1, out hv_RightMeasureRectColumn1);
                hv_RightMeasureRectPhi1.Dispose();
                HOperatorSet.OrientationRegion(ho_RightMeasureRectionAffineTrans1, out hv_RightMeasureRectPhi1);
                hv_MeasureHandle7.Dispose();
                HOperatorSet.GenMeasureRectangle2(hv_RightMeasureRectRow1, hv_RightMeasureRectColumn1,
                    hv_RightMeasureRectPhi1, 100, 50, hv_ImageWidth, hv_ImageHeight, "bicubic",
                    out hv_MeasureHandle7);

                ho_RightMeasureRectionAffineTrans2.Dispose();
                HOperatorSet.AffineTransRegion(ho_Rectangle4, out ho_RightMeasureRectionAffineTrans2,
                    hv_HomMat2D7, "nearest_neighbor");
                hv_RightMeasureRectArea.Dispose(); hv_RightMeasureRectRow2.Dispose(); hv_RightMeasureRectColumn2.Dispose();
                HOperatorSet.AreaCenter(ho_RightMeasureRectionAffineTrans2, out hv_RightMeasureRectArea,
                    out hv_RightMeasureRectRow2, out hv_RightMeasureRectColumn2);
                hv_RightMeasureRectPhi2.Dispose();
                HOperatorSet.OrientationRegion(ho_RightMeasureRectionAffineTrans2, out hv_RightMeasureRectPhi2);
                hv_MeasureHandle8.Dispose();
                HOperatorSet.GenMeasureRectangle2(hv_RightMeasureRectRow2, hv_RightMeasureRectColumn2,
                    hv_RightMeasureRectPhi2, 100, 50, hv_ImageWidth, hv_ImageHeight, "bicubic",
                    out hv_MeasureHandle8);
            }
            else
            {
                hv_HomMat2D7.Dispose();
                HOperatorSet.VectorAngleToRigid(hv_CenterRow, hv_CenterColumn, hv_BaseRectPhi,
                    hv_CenterRow, hv_CenterColumn, hv_BasePhi, out hv_HomMat2D7);
                ho_RightMeasureRectionAffineTrans1.Dispose();
                HOperatorSet.AffineTransRegion(ho_Rectangle3, out ho_RightMeasureRectionAffineTrans1,
                    hv_HomMat2D7, "nearest_neighbor");
                hv_RightMeasureRectArea.Dispose(); hv_RightMeasureRectRow1.Dispose(); hv_RightMeasureRectColumn1.Dispose();
                HOperatorSet.AreaCenter(ho_RightMeasureRectionAffineTrans1, out hv_RightMeasureRectArea,
                    out hv_RightMeasureRectRow1, out hv_RightMeasureRectColumn1);
                hv_RightMeasureRectPhi1.Dispose();
                HOperatorSet.OrientationRegion(ho_RightMeasureRectionAffineTrans1, out hv_RightMeasureRectPhi1);
                hv_MeasureHandle7.Dispose();
                HOperatorSet.GenMeasureRectangle2(hv_RightMeasureRectRow1, hv_RightMeasureRectColumn1,
                    hv_RightMeasureRectPhi1, 100, 50, hv_ImageWidth, hv_ImageHeight, "bicubic",
                    out hv_MeasureHandle7);

                ho_RightMeasureRectionAffineTrans2.Dispose();
                HOperatorSet.AffineTransRegion(ho_Rectangle4, out ho_RightMeasureRectionAffineTrans2,
                    hv_HomMat2D7, "nearest_neighbor");
                hv_RightMeasureRectArea.Dispose(); hv_RightMeasureRectRow2.Dispose(); hv_RightMeasureRectColumn2.Dispose();
                HOperatorSet.AreaCenter(ho_RightMeasureRectionAffineTrans2, out hv_RightMeasureRectArea,
                    out hv_RightMeasureRectRow2, out hv_RightMeasureRectColumn2);
                hv_RightMeasureRectPhi2.Dispose();
                HOperatorSet.OrientationRegion(ho_RightMeasureRectionAffineTrans2, out hv_RightMeasureRectPhi2);
                hv_MeasureHandle8.Dispose();
                HOperatorSet.GenMeasureRectangle2(hv_RightMeasureRectRow2, hv_RightMeasureRectColumn2,
                    hv_RightMeasureRectPhi2, 100, 50, hv_ImageWidth, hv_ImageHeight, "bicubic",
                    out hv_MeasureHandle8);
            }
            //抓取右侧边缘
            hv_RightRowEdge1.Dispose(); hv_RightColumnEdge1.Dispose(); hv_RightAmplitude.Dispose(); hv_RightDistance.Dispose();
            HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle7, 1, 30, "all", "all", out hv_RightRowEdge1,
                out hv_RightColumnEdge1, out hv_RightAmplitude, out hv_RightDistance);
            hv_RightRowEdge2.Dispose(); hv_RightColumnEdge2.Dispose(); hv_RightAmplitude.Dispose(); hv_RightDistance.Dispose();
            HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle8, 1, 30, "all", "all", out hv_RightRowEdge2,
                out hv_RightColumnEdge2, out hv_RightAmplitude, out hv_RightDistance);
            HOperatorSet.CloseMeasure(hv_MeasureHandle7);
            HOperatorSet.CloseMeasure(hv_MeasureHandle8);
            ho_Rectangle3.Dispose();
            ho_Rectangle4.Dispose();
            ho_RightMeasureRectionAffineTrans1.Dispose();
            ho_RightMeasureRectionAffineTrans2.Dispose();

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

            return;
        }

        private static void gen_TwoCircle_info(HObject ho_Image, out HTuple hv_MaxCircleRow, out HTuple hv_MaxCircleColumn,
            out HTuple hv_MaxCircleRadius, out HTuple hv_MinCircleRow, out HTuple hv_MinCircleColumn,
            out HTuple hv_MinCircleRadius, out HTuple hv_TwoCirclePhi)
        {
            // Local iconic variables 
            HObject ho_Regions, ho_ConnectedRegions, ho_MinCircleSelectedRegions;
            HObject ho_MaxCircleSelectedRegions;
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Regions);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_MinCircleSelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_MaxCircleSelectedRegions);
            hv_MaxCircleRow = new HTuple();
            hv_MaxCircleColumn = new HTuple();
            hv_MaxCircleRadius = new HTuple();
            hv_MinCircleRow = new HTuple();
            hv_MinCircleColumn = new HTuple();
            hv_MinCircleRadius = new HTuple();
            hv_TwoCirclePhi = new HTuple();
            ho_Regions.Dispose();
            HOperatorSet.FastThreshold(ho_Image, out ho_Regions, 128, 255, 20);
            ho_ConnectedRegions.Dispose();
            HOperatorSet.Connection(ho_Regions, out ho_ConnectedRegions);
            //提取两个圆形区域
            ho_MinCircleSelectedRegions.Dispose();
            HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_MinCircleSelectedRegions,
                (new HTuple("area")).TupleConcat("circularity"), "and", (new HTuple(727440)).TupleConcat(
                0.8407), (new HTuple(1.22468e+06)).TupleConcat(1));
            ho_MaxCircleSelectedRegions.Dispose();
            HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_MaxCircleSelectedRegions,
                (new HTuple("area")).TupleConcat("circularity"), "and", (new HTuple(1.15101e+06)).TupleConcat(
                0.6473), (new HTuple(1.68508e+06)).TupleConcat(0.837));
            hv_MaxCircleRow.Dispose(); hv_MaxCircleColumn.Dispose(); hv_MaxCircleRadius.Dispose();
            HOperatorSet.InnerCircle(ho_MaxCircleSelectedRegions, out hv_MaxCircleRow, out hv_MaxCircleColumn,
                out hv_MaxCircleRadius);
            hv_MinCircleRow.Dispose(); hv_MinCircleColumn.Dispose(); hv_MinCircleRadius.Dispose();
            HOperatorSet.InnerCircle(ho_MinCircleSelectedRegions, out hv_MinCircleRow, out hv_MinCircleColumn,
                out hv_MinCircleRadius);
            //求两圆心连线方向
            hv_TwoCirclePhi.Dispose();
            HOperatorSet.LineOrientation(hv_MaxCircleRow, hv_MaxCircleColumn, hv_MinCircleRow,
                hv_MinCircleColumn, out hv_TwoCirclePhi);
            ho_Regions.Dispose();
            ho_ConnectedRegions.Dispose();
            ho_MinCircleSelectedRegions.Dispose();
            ho_MaxCircleSelectedRegions.Dispose();


            return;
        }

        private static void gen_L1L2_Measure10(HObject ho_Image, HTuple hv_CenterRow, HTuple hv_CenterColumn,
    HTuple hv_MinCircleRadius, HTuple hv_BasePhi, HTuple hv_Width, HTuple hv_Height,
    out HTuple hv_RightCircleRowEdge1, out HTuple hv_RightCircleColumnEdge1, out HTuple hv_LeftCircleRowEdge1,
    out HTuple hv_LeftCircleColumnEdge1, out HTuple hv_RightCircleRowEdge2, out HTuple hv_RightCircleColumnEdge2,
    out HTuple hv_LeftCircleRowEdge2, out HTuple hv_LeftCircleColumnEdge2)
        {
            // Local iconic variables 
            HObject ho_Rect1, ho_RightRect1, ho_LeftRect1;
            HObject ho_Rect2, ho_RightRect2, ho_LeftRect2;
            // Local control variables 
            HTuple hv_HomMat2D1 = new HTuple(), hv_RightRect1Area = new HTuple();
            HTuple hv_RightRect1Row = new HTuple(), hv_RightRect1Column = new HTuple();
            HTuple hv_RightRect1Phi = new HTuple(), hv_MeasureHandle1 = new HTuple();
            HTuple hv_HomMat2D2 = new HTuple(), hv_LeftRect1Area = new HTuple();
            HTuple hv_LeftRect1Row = new HTuple(), hv_LeftRect1Column = new HTuple();
            HTuple hv_LeftRect1Phi = new HTuple(), hv_MeasureHandle3 = new HTuple();
            HTuple hv_RightRect2Area = new HTuple(), hv_RightRect2Row = new HTuple();
            HTuple hv_RightRect2Column = new HTuple(), hv_RightRect2Phi = new HTuple();
            HTuple hv_MeasureHandle2 = new HTuple(), hv_LeftRect2Area = new HTuple();
            HTuple hv_LeftRect2Row = new HTuple(), hv_LeftRect2Column = new HTuple();
            HTuple hv_LeftRect2Phi = new HTuple(), hv_MeasureHandle4 = new HTuple();
            HTuple hv_Amplitude = new HTuple(), hv_Distance = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Rect1);
            HOperatorSet.GenEmptyObj(out ho_RightRect1);
            HOperatorSet.GenEmptyObj(out ho_LeftRect1);
            HOperatorSet.GenEmptyObj(out ho_Rect2);
            HOperatorSet.GenEmptyObj(out ho_RightRect2);
            HOperatorSet.GenEmptyObj(out ho_LeftRect2);
            hv_RightCircleRowEdge1 = new HTuple();
            hv_RightCircleColumnEdge1 = new HTuple();
            hv_LeftCircleRowEdge1 = new HTuple();
            hv_LeftCircleColumnEdge1 = new HTuple();
            hv_RightCircleRowEdge2 = new HTuple();
            hv_RightCircleColumnEdge2 = new HTuple();
            hv_LeftCircleRowEdge2 = new HTuple();
            hv_LeftCircleColumnEdge2 = new HTuple();
            try
            {
                //生成基准矩形
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    ho_Rect1.Dispose();
                    HOperatorSet.GenRectangle2(out ho_Rect1, hv_CenterRow - 100, hv_CenterColumn + hv_MinCircleRadius,
                        0, 100, 20);
                }
                //生成测量矩形
                hv_HomMat2D1.Dispose();
                HOperatorSet.VectorAngleToRigid(hv_CenterRow, hv_CenterColumn, 0, hv_CenterRow,
                    hv_CenterColumn, hv_BasePhi, out hv_HomMat2D1);
                ho_RightRect1.Dispose();
                HOperatorSet.AffineTransRegion(ho_Rect1, out ho_RightRect1, hv_HomMat2D1, "nearest_neighbor");
                hv_RightRect1Area.Dispose(); hv_RightRect1Row.Dispose(); hv_RightRect1Column.Dispose();
                HOperatorSet.AreaCenter(ho_RightRect1, out hv_RightRect1Area, out hv_RightRect1Row,
                    out hv_RightRect1Column);
                hv_RightRect1Phi.Dispose();
                HOperatorSet.OrientationRegion(ho_RightRect1, out hv_RightRect1Phi);
                hv_MeasureHandle1.Dispose();
                HOperatorSet.GenMeasureRectangle2(hv_RightRect1Row, hv_RightRect1Column, hv_RightRect1Phi,
                    100, 50, hv_Width, hv_Height, "bicubic", out hv_MeasureHandle1);

                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_HomMat2D2.Dispose();
                    HOperatorSet.VectorAngleToRigid(hv_CenterRow, hv_CenterColumn, 0, hv_CenterRow,
                        hv_CenterColumn, hv_BasePhi + ((new HTuple(180)).TupleRad()), out hv_HomMat2D2);
                }
                ho_LeftRect1.Dispose();
                HOperatorSet.AffineTransRegion(ho_Rect1, out ho_LeftRect1, hv_HomMat2D2, "nearest_neighbor");
                hv_LeftRect1Area.Dispose(); hv_LeftRect1Row.Dispose(); hv_LeftRect1Column.Dispose();
                HOperatorSet.AreaCenter(ho_LeftRect1, out hv_LeftRect1Area, out hv_LeftRect1Row,
                    out hv_LeftRect1Column);
                hv_LeftRect1Phi.Dispose();
                HOperatorSet.OrientationRegion(ho_LeftRect1, out hv_LeftRect1Phi);
                hv_MeasureHandle3.Dispose();
                HOperatorSet.GenMeasureRectangle2(hv_LeftRect1Row, hv_LeftRect1Column, hv_LeftRect1Phi,
                    100, 50, hv_Width, hv_Height, "bicubic", out hv_MeasureHandle3);
                //生成基准矩形
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    ho_Rect2.Dispose();
                    HOperatorSet.GenRectangle2(out ho_Rect2, hv_CenterRow + 100, hv_CenterColumn + hv_MinCircleRadius,
                        0, 100, 20);
                }
                //生成测量矩形
                ho_RightRect2.Dispose();
                HOperatorSet.AffineTransRegion(ho_Rect2, out ho_RightRect2, hv_HomMat2D1, "nearest_neighbor");
                hv_RightRect2Area.Dispose(); hv_RightRect2Row.Dispose(); hv_RightRect2Column.Dispose();
                HOperatorSet.AreaCenter(ho_RightRect2, out hv_RightRect2Area, out hv_RightRect2Row,
                    out hv_RightRect2Column);
                hv_RightRect2Phi.Dispose();
                HOperatorSet.OrientationRegion(ho_RightRect2, out hv_RightRect2Phi);
                hv_MeasureHandle2.Dispose();
                HOperatorSet.GenMeasureRectangle2(hv_RightRect2Row, hv_RightRect2Column, hv_RightRect2Phi,
                    100, 50, hv_Width, hv_Height, "bicubic", out hv_MeasureHandle2);

                ho_LeftRect2.Dispose();
                HOperatorSet.AffineTransRegion(ho_Rect2, out ho_LeftRect2, hv_HomMat2D2, "nearest_neighbor");
                hv_LeftRect2Area.Dispose(); hv_LeftRect2Row.Dispose(); hv_LeftRect2Column.Dispose();
                HOperatorSet.AreaCenter(ho_LeftRect2, out hv_LeftRect2Area, out hv_LeftRect2Row,
                    out hv_LeftRect2Column);
                hv_LeftRect2Phi.Dispose();
                HOperatorSet.OrientationRegion(ho_LeftRect2, out hv_LeftRect2Phi);
                hv_MeasureHandle4.Dispose();
                HOperatorSet.GenMeasureRectangle2(hv_LeftRect2Row, hv_LeftRect2Column, hv_LeftRect2Phi,
                    100, 50, hv_Width, hv_Height, "bicubic", out hv_MeasureHandle4);
                //抓取边缘点
                hv_RightCircleRowEdge1.Dispose(); hv_RightCircleColumnEdge1.Dispose(); hv_Amplitude.Dispose(); hv_Distance.Dispose();
                HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle1, 1, 30, "all", "all", out hv_RightCircleRowEdge1,
                    out hv_RightCircleColumnEdge1, out hv_Amplitude, out hv_Distance);
                hv_LeftCircleRowEdge1.Dispose(); hv_LeftCircleColumnEdge1.Dispose(); hv_Amplitude.Dispose(); hv_Distance.Dispose();
                HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle3, 1, 30, "all", "all", out hv_LeftCircleRowEdge1,
                    out hv_LeftCircleColumnEdge1, out hv_Amplitude, out hv_Distance);
                hv_RightCircleRowEdge2.Dispose(); hv_RightCircleColumnEdge2.Dispose(); hv_Amplitude.Dispose(); hv_Distance.Dispose();
                HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle2, 1, 30, "all", "all", out hv_RightCircleRowEdge2,
                    out hv_RightCircleColumnEdge2, out hv_Amplitude, out hv_Distance);
                hv_LeftCircleRowEdge2.Dispose(); hv_LeftCircleColumnEdge2.Dispose(); hv_Amplitude.Dispose(); hv_Distance.Dispose();
                HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle4, 1, 30, "all", "all", out hv_LeftCircleRowEdge2,
                    out hv_LeftCircleColumnEdge2, out hv_Amplitude, out hv_Distance);
                ho_Rect1.Dispose();
                ho_RightRect1.Dispose();
                ho_LeftRect1.Dispose();
                ho_Rect2.Dispose();
                ho_RightRect2.Dispose();
                ho_LeftRect2.Dispose();

                hv_HomMat2D1.Dispose();
                hv_RightRect1Area.Dispose();
                hv_RightRect1Row.Dispose();
                hv_RightRect1Column.Dispose();
                hv_RightRect1Phi.Dispose();
                hv_MeasureHandle1.Dispose();
                hv_HomMat2D2.Dispose();
                hv_LeftRect1Area.Dispose();
                hv_LeftRect1Row.Dispose();
                hv_LeftRect1Column.Dispose();
                hv_LeftRect1Phi.Dispose();
                hv_MeasureHandle3.Dispose();
                hv_RightRect2Area.Dispose();
                hv_RightRect2Row.Dispose();
                hv_RightRect2Column.Dispose();
                hv_RightRect2Phi.Dispose();
                hv_MeasureHandle2.Dispose();
                hv_LeftRect2Area.Dispose();
                hv_LeftRect2Row.Dispose();
                hv_LeftRect2Column.Dispose();
                hv_LeftRect2Phi.Dispose();
                hv_MeasureHandle4.Dispose();
                hv_Amplitude.Dispose();
                hv_Distance.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {
                ho_Rect1.Dispose();
                ho_RightRect1.Dispose();
                ho_LeftRect1.Dispose();
                ho_Rect2.Dispose();
                ho_RightRect2.Dispose();
                ho_LeftRect2.Dispose();

                hv_HomMat2D1.Dispose();
                hv_RightRect1Area.Dispose();
                hv_RightRect1Row.Dispose();
                hv_RightRect1Column.Dispose();
                hv_RightRect1Phi.Dispose();
                hv_MeasureHandle1.Dispose();
                hv_HomMat2D2.Dispose();
                hv_LeftRect1Area.Dispose();
                hv_LeftRect1Row.Dispose();
                hv_LeftRect1Column.Dispose();
                hv_LeftRect1Phi.Dispose();
                hv_MeasureHandle3.Dispose();
                hv_RightRect2Area.Dispose();
                hv_RightRect2Row.Dispose();
                hv_RightRect2Column.Dispose();
                hv_RightRect2Phi.Dispose();
                hv_MeasureHandle2.Dispose();
                hv_LeftRect2Area.Dispose();
                hv_LeftRect2Row.Dispose();
                hv_LeftRect2Column.Dispose();
                hv_LeftRect2Phi.Dispose();
                hv_MeasureHandle4.Dispose();
                hv_Amplitude.Dispose();
                hv_Distance.Dispose();

                throw HDevExpDefaultException;
            }
        }

        private static void gen_L1L2_Measure34(HObject ho_Image, HTuple hv_MinCircleRow, HTuple hv_MinCircleColumn,
    HTuple hv_MinCircleRadius, HTuple hv_TwoCirclePhi, HTuple hv_Width, HTuple hv_Height,
    out HTuple hv_RightCircleRowEdge1, out HTuple hv_RightCircleColumnEdge1, out HTuple hv_LeftCircleRowEdge1,
    out HTuple hv_LeftCircleColumnEdge1, out HTuple hv_RightCircleRowEdge2, out HTuple hv_RightCircleColumnEdge2,
    out HTuple hv_LeftCircleRowEdge2, out HTuple hv_LeftCircleColumnEdge2)
        {
            // Local iconic variables 
            HObject ho_Rect1, ho_RightRect1, ho_LeftRect1;
            HObject ho_Rect2, ho_RightRect2, ho_LeftRect2;
            // Local control variables 
            HTuple hv_HomMat2D1 = new HTuple(), hv_RightRect1Area = new HTuple();
            HTuple hv_RightRect1Row = new HTuple(), hv_RightRect1Column = new HTuple();
            HTuple hv_RightRect1Phi = new HTuple(), hv_MeasureHandle1 = new HTuple();
            HTuple hv_HomMat2D2 = new HTuple(), hv_LeftRect1Area = new HTuple();
            HTuple hv_LeftRect1Row = new HTuple(), hv_LeftRect1Column = new HTuple();
            HTuple hv_LeftRect1Phi = new HTuple(), hv_MeasureHandle3 = new HTuple();
            HTuple hv_RightRect2Area = new HTuple(), hv_RightRect2Row = new HTuple();
            HTuple hv_RightRect2Column = new HTuple(), hv_RightRect2Phi = new HTuple();
            HTuple hv_MeasureHandle2 = new HTuple(), hv_LeftRect2Area = new HTuple();
            HTuple hv_LeftRect2Row = new HTuple(), hv_LeftRect2Column = new HTuple();
            HTuple hv_LeftRect2Phi = new HTuple(), hv_MeasureHandle4 = new HTuple();
            HTuple hv_Amplitude = new HTuple(), hv_Distance = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Rect1);
            HOperatorSet.GenEmptyObj(out ho_RightRect1);
            HOperatorSet.GenEmptyObj(out ho_LeftRect1);
            HOperatorSet.GenEmptyObj(out ho_Rect2);
            HOperatorSet.GenEmptyObj(out ho_RightRect2);
            HOperatorSet.GenEmptyObj(out ho_LeftRect2);
            hv_RightCircleRowEdge1 = new HTuple();
            hv_RightCircleColumnEdge1 = new HTuple();
            hv_LeftCircleRowEdge1 = new HTuple();
            hv_LeftCircleColumnEdge1 = new HTuple();
            hv_RightCircleRowEdge2 = new HTuple();
            hv_RightCircleColumnEdge2 = new HTuple();
            hv_LeftCircleRowEdge2 = new HTuple();
            hv_LeftCircleColumnEdge2 = new HTuple();
            //生成基准矩形
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                ho_Rect1.Dispose();
                HOperatorSet.GenRectangle2(out ho_Rect1, hv_MinCircleRow - 100, hv_MinCircleColumn + hv_MinCircleRadius,
                    0, 100, 20);
            }
            //生成测量矩形
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_HomMat2D1.Dispose();
                HOperatorSet.VectorAngleToRigid(hv_MinCircleRow, hv_MinCircleColumn, 0, hv_MinCircleRow,
                    hv_MinCircleColumn, hv_TwoCirclePhi + ((new HTuple(90)).TupleRad()), out hv_HomMat2D1);
            }
            ho_RightRect1.Dispose();
            HOperatorSet.AffineTransRegion(ho_Rect1, out ho_RightRect1, hv_HomMat2D1, "nearest_neighbor");
            hv_RightRect1Area.Dispose(); hv_RightRect1Row.Dispose(); hv_RightRect1Column.Dispose();
            HOperatorSet.AreaCenter(ho_RightRect1, out hv_RightRect1Area, out hv_RightRect1Row,
                out hv_RightRect1Column);
            hv_RightRect1Phi.Dispose();
            HOperatorSet.OrientationRegion(ho_RightRect1, out hv_RightRect1Phi);
            hv_MeasureHandle1.Dispose();
            HOperatorSet.GenMeasureRectangle2(hv_RightRect1Row, hv_RightRect1Column, hv_RightRect1Phi,
                100, 50, hv_Width, hv_Height, "bicubic", out hv_MeasureHandle1);

            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_HomMat2D2.Dispose();
                HOperatorSet.VectorAngleToRigid(hv_MinCircleRow, hv_MinCircleColumn, 0, hv_MinCircleRow,
                    hv_MinCircleColumn, hv_TwoCirclePhi - ((new HTuple(90)).TupleRad()), out hv_HomMat2D2);
            }
            ho_LeftRect1.Dispose();
            HOperatorSet.AffineTransRegion(ho_Rect1, out ho_LeftRect1, hv_HomMat2D2, "nearest_neighbor");
            hv_LeftRect1Area.Dispose(); hv_LeftRect1Row.Dispose(); hv_LeftRect1Column.Dispose();
            HOperatorSet.AreaCenter(ho_LeftRect1, out hv_LeftRect1Area, out hv_LeftRect1Row,
                out hv_LeftRect1Column);
            hv_LeftRect1Phi.Dispose();
            HOperatorSet.OrientationRegion(ho_LeftRect1, out hv_LeftRect1Phi);
            hv_MeasureHandle3.Dispose();
            HOperatorSet.GenMeasureRectangle2(hv_LeftRect1Row, hv_LeftRect1Column, hv_LeftRect1Phi,
                100, 50, hv_Width, hv_Height, "bicubic", out hv_MeasureHandle3);
            //生成基准矩形
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                ho_Rect2.Dispose();
                HOperatorSet.GenRectangle2(out ho_Rect2, hv_MinCircleRow + 100, hv_MinCircleColumn + hv_MinCircleRadius,
                    0, 100, 20);
            }
            //生成测量矩形
            ho_RightRect2.Dispose();
            HOperatorSet.AffineTransRegion(ho_Rect2, out ho_RightRect2, hv_HomMat2D1, "nearest_neighbor");
            hv_RightRect2Area.Dispose(); hv_RightRect2Row.Dispose(); hv_RightRect2Column.Dispose();
            HOperatorSet.AreaCenter(ho_RightRect2, out hv_RightRect2Area, out hv_RightRect2Row,
                out hv_RightRect2Column);
            hv_RightRect2Phi.Dispose();
            HOperatorSet.OrientationRegion(ho_RightRect2, out hv_RightRect2Phi);
            hv_MeasureHandle2.Dispose();
            HOperatorSet.GenMeasureRectangle2(hv_RightRect2Row, hv_RightRect2Column, hv_RightRect2Phi,
                100, 50, hv_Width, hv_Height, "bicubic", out hv_MeasureHandle2);

            ho_LeftRect2.Dispose();
            HOperatorSet.AffineTransRegion(ho_Rect2, out ho_LeftRect2, hv_HomMat2D2, "nearest_neighbor");
            hv_LeftRect2Area.Dispose(); hv_LeftRect2Row.Dispose(); hv_LeftRect2Column.Dispose();
            HOperatorSet.AreaCenter(ho_LeftRect2, out hv_LeftRect2Area, out hv_LeftRect2Row,
                out hv_LeftRect2Column);
            hv_LeftRect2Phi.Dispose();
            HOperatorSet.OrientationRegion(ho_LeftRect2, out hv_LeftRect2Phi);
            hv_MeasureHandle4.Dispose();
            HOperatorSet.GenMeasureRectangle2(hv_LeftRect2Row, hv_LeftRect2Column, hv_LeftRect2Phi,
                100, 50, hv_Width, hv_Height, "bicubic", out hv_MeasureHandle4);
            //抓取边缘点
            hv_RightCircleRowEdge1.Dispose(); hv_RightCircleColumnEdge1.Dispose(); hv_Amplitude.Dispose(); hv_Distance.Dispose();
            HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle1, 1, 30, "all", "all", out hv_RightCircleRowEdge1,
                out hv_RightCircleColumnEdge1, out hv_Amplitude, out hv_Distance);
            hv_LeftCircleRowEdge1.Dispose(); hv_LeftCircleColumnEdge1.Dispose(); hv_Amplitude.Dispose(); hv_Distance.Dispose();
            HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle3, 1, 30, "all", "all", out hv_LeftCircleRowEdge1,
                out hv_LeftCircleColumnEdge1, out hv_Amplitude, out hv_Distance);
            hv_RightCircleRowEdge2.Dispose(); hv_RightCircleColumnEdge2.Dispose(); hv_Amplitude.Dispose(); hv_Distance.Dispose();
            HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle2, 1, 30, "all", "all", out hv_RightCircleRowEdge2,
                out hv_RightCircleColumnEdge2, out hv_Amplitude, out hv_Distance);
            hv_LeftCircleRowEdge2.Dispose(); hv_LeftCircleColumnEdge2.Dispose(); hv_Amplitude.Dispose(); hv_Distance.Dispose();
            HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle4, 1, 30, "all", "all", out hv_LeftCircleRowEdge2,
                out hv_LeftCircleColumnEdge2, out hv_Amplitude, out hv_Distance);
            ho_Rect1.Dispose();
            ho_RightRect1.Dispose();
            ho_LeftRect1.Dispose();
            ho_Rect2.Dispose();
            ho_RightRect2.Dispose();
            ho_LeftRect2.Dispose();

            hv_HomMat2D1.Dispose();
            hv_RightRect1Area.Dispose();
            hv_RightRect1Row.Dispose();
            hv_RightRect1Column.Dispose();
            hv_RightRect1Phi.Dispose();
            hv_MeasureHandle1.Dispose();
            hv_HomMat2D2.Dispose();
            hv_LeftRect1Area.Dispose();
            hv_LeftRect1Row.Dispose();
            hv_LeftRect1Column.Dispose();
            hv_LeftRect1Phi.Dispose();
            hv_MeasureHandle3.Dispose();
            hv_RightRect2Area.Dispose();
            hv_RightRect2Row.Dispose();
            hv_RightRect2Column.Dispose();
            hv_RightRect2Phi.Dispose();
            hv_MeasureHandle2.Dispose();
            hv_LeftRect2Area.Dispose();
            hv_LeftRect2Row.Dispose();
            hv_LeftRect2Column.Dispose();
            hv_LeftRect2Phi.Dispose();
            hv_MeasureHandle4.Dispose();
            hv_Amplitude.Dispose();
            hv_Distance.Dispose();

            return;
        }

        private static void gen_p1p2p3_Measure46(HObject ho_Image, HTuple hv_MinCircleRow, HTuple hv_MinCircleColumn,
    HTuple hv_MinCircleRadius, HTuple hv_MaxCircleColumn, HTuple hv_TwoCirclePhi,
    HTuple hv_Width, HTuple hv_Height, out HTuple hv_P1Row, out HTuple hv_P1Col,
    out HTuple hv_P2Row, out HTuple hv_P2Col, out HTuple hv_P3Row, out HTuple hv_P3Col)
        {
            // Local iconic variables 
            HObject ho_Rect1, ho_Rect2, ho_Rect3, ho_P1Rect = null;
            HObject ho_P2Rect = null, ho_P3Rect = null;
            // Local control variables 
            HTuple hv_HomMat2D = new HTuple(), hv_P1RectArea = new HTuple();
            HTuple hv_P1RectRow = new HTuple(), hv_P1RectColumn = new HTuple();
            HTuple hv_P1RectPhi = new HTuple(), hv_MeasureHandle1 = new HTuple();
            HTuple hv_P2RectArea = new HTuple(), hv_P2RectRow = new HTuple();
            HTuple hv_P2RectColumn = new HTuple(), hv_P2RectPhi = new HTuple();
            HTuple hv_MeasureHandle2 = new HTuple(), hv_P3RectArea = new HTuple();
            HTuple hv_P3RectRow = new HTuple(), hv_P3RectColumn = new HTuple();
            HTuple hv_P3RectPhi = new HTuple(), hv_MeasureHandle3 = new HTuple();
            HTuple hv_Amplitude = new HTuple(), hv_Distance = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Rect1);
            HOperatorSet.GenEmptyObj(out ho_Rect2);
            HOperatorSet.GenEmptyObj(out ho_Rect3);
            HOperatorSet.GenEmptyObj(out ho_P1Rect);
            HOperatorSet.GenEmptyObj(out ho_P2Rect);
            HOperatorSet.GenEmptyObj(out ho_P3Rect);
            hv_P1Row = new HTuple();
            hv_P1Col = new HTuple();
            hv_P2Row = new HTuple();
            hv_P2Col = new HTuple();
            hv_P3Row = new HTuple();
            hv_P3Col = new HTuple();
            //生成基准矩形
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                ho_Rect1.Dispose();
                HOperatorSet.GenRectangle2(out ho_Rect1, hv_MinCircleRow, hv_MinCircleColumn + ((hv_MinCircleRadius * 5) / 4),
                    0, 100, 20);
            }
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                ho_Rect2.Dispose();
                HOperatorSet.GenRectangle2(out ho_Rect2, hv_MinCircleRow + ((hv_MinCircleRadius * 4) / 5),
                    hv_MinCircleColumn + ((hv_MinCircleRadius * 5) / 4), 0, 100, 20);
            }
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                ho_Rect3.Dispose();
                HOperatorSet.GenRectangle2(out ho_Rect3, hv_MinCircleRow - ((hv_MinCircleRadius * 4) / 5),
                    hv_MinCircleColumn + ((hv_MinCircleRadius * 5) / 4), 0, 100, 20);
            }

            if ((int)(new HTuple(hv_MaxCircleColumn.TupleLess(hv_MinCircleColumn))) != 0)
            {
                //生成测量矩形
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_HomMat2D.Dispose();
                    HOperatorSet.VectorAngleToRigid(hv_MinCircleRow, hv_MinCircleColumn, 0, hv_MinCircleRow,
                        hv_MinCircleColumn, hv_TwoCirclePhi - ((new HTuple(90)).TupleRad()), out hv_HomMat2D);
                }
                ho_P1Rect.Dispose();
                HOperatorSet.AffineTransRegion(ho_Rect1, out ho_P1Rect, hv_HomMat2D, "nearest_neighbor");
                hv_P1RectArea.Dispose(); hv_P1RectRow.Dispose(); hv_P1RectColumn.Dispose();
                HOperatorSet.AreaCenter(ho_P1Rect, out hv_P1RectArea, out hv_P1RectRow, out hv_P1RectColumn);
                hv_P1RectPhi.Dispose();
                HOperatorSet.OrientationRegion(ho_P1Rect, out hv_P1RectPhi);
                hv_MeasureHandle1.Dispose();
                HOperatorSet.GenMeasureRectangle2(hv_P1RectRow, hv_P1RectColumn, hv_P1RectPhi,
                    100, 20, hv_Width, hv_Height, "bicubic", out hv_MeasureHandle1);

                ho_P2Rect.Dispose();
                HOperatorSet.AffineTransRegion(ho_Rect2, out ho_P2Rect, hv_HomMat2D, "nearest_neighbor");
                hv_P2RectArea.Dispose(); hv_P2RectRow.Dispose(); hv_P2RectColumn.Dispose();
                HOperatorSet.AreaCenter(ho_P2Rect, out hv_P2RectArea, out hv_P2RectRow, out hv_P2RectColumn);
                hv_P2RectPhi.Dispose();
                HOperatorSet.OrientationRegion(ho_P2Rect, out hv_P2RectPhi);
                hv_MeasureHandle2.Dispose();
                HOperatorSet.GenMeasureRectangle2(hv_P2RectRow, hv_P2RectColumn, hv_P2RectPhi,
                    100, 20, hv_Width, hv_Height, "bicubic", out hv_MeasureHandle2);

                ho_P3Rect.Dispose();
                HOperatorSet.AffineTransRegion(ho_Rect3, out ho_P3Rect, hv_HomMat2D, "nearest_neighbor");
                hv_P3RectArea.Dispose(); hv_P3RectRow.Dispose(); hv_P3RectColumn.Dispose();
                HOperatorSet.AreaCenter(ho_P3Rect, out hv_P3RectArea, out hv_P3RectRow, out hv_P3RectColumn);
                hv_P3RectPhi.Dispose();
                HOperatorSet.OrientationRegion(ho_P3Rect, out hv_P3RectPhi);
                hv_MeasureHandle3.Dispose();
                HOperatorSet.GenMeasureRectangle2(hv_P3RectRow, hv_P3RectColumn, hv_P3RectPhi,
                    100, 20, hv_Width, hv_Height, "bicubic", out hv_MeasureHandle3);
                //定位P1、P2、P3
                hv_P1Row.Dispose(); hv_P1Col.Dispose(); hv_Amplitude.Dispose(); hv_Distance.Dispose();
                HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle1, 1, 30, "all", "all", out hv_P1Row,
                    out hv_P1Col, out hv_Amplitude, out hv_Distance);
                hv_P2Row.Dispose(); hv_P2Col.Dispose(); hv_Amplitude.Dispose(); hv_Distance.Dispose();
                HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle2, 1, 30, "all", "all", out hv_P2Row,
                    out hv_P2Col, out hv_Amplitude, out hv_Distance);
                hv_P3Row.Dispose(); hv_P3Col.Dispose(); hv_Amplitude.Dispose(); hv_Distance.Dispose();
                HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle3, 1, 30, "all", "all", out hv_P3Row,
                    out hv_P3Col, out hv_Amplitude, out hv_Distance);
            }
            else
            {
                //生成测量矩形
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_HomMat2D.Dispose();
                    HOperatorSet.VectorAngleToRigid(hv_MinCircleRow, hv_MinCircleColumn, 0, hv_MinCircleRow,
                        hv_MinCircleColumn, hv_TwoCirclePhi + ((new HTuple(90)).TupleRad()), out hv_HomMat2D);
                }
                ho_P1Rect.Dispose();
                HOperatorSet.AffineTransRegion(ho_Rect1, out ho_P1Rect, hv_HomMat2D, "nearest_neighbor");
                hv_P1RectArea.Dispose(); hv_P1RectRow.Dispose(); hv_P1RectColumn.Dispose();
                HOperatorSet.AreaCenter(ho_P1Rect, out hv_P1RectArea, out hv_P1RectRow, out hv_P1RectColumn);
                hv_P1RectPhi.Dispose();
                HOperatorSet.OrientationRegion(ho_P1Rect, out hv_P1RectPhi);
                hv_MeasureHandle1.Dispose();
                HOperatorSet.GenMeasureRectangle2(hv_P1RectRow, hv_P1RectColumn, hv_P1RectPhi,
                    100, 20, hv_Width, hv_Height, "bicubic", out hv_MeasureHandle1);

                ho_P2Rect.Dispose();
                HOperatorSet.AffineTransRegion(ho_Rect2, out ho_P2Rect, hv_HomMat2D, "nearest_neighbor");
                hv_P2RectArea.Dispose(); hv_P2RectRow.Dispose(); hv_P2RectColumn.Dispose();
                HOperatorSet.AreaCenter(ho_P2Rect, out hv_P2RectArea, out hv_P2RectRow, out hv_P2RectColumn);
                hv_P2RectPhi.Dispose();
                HOperatorSet.OrientationRegion(ho_P2Rect, out hv_P2RectPhi);
                hv_MeasureHandle2.Dispose();
                HOperatorSet.GenMeasureRectangle2(hv_P2RectRow, hv_P2RectColumn, hv_P2RectPhi,
                    100, 20, hv_Width, hv_Height, "bicubic", out hv_MeasureHandle2);

                ho_P3Rect.Dispose();
                HOperatorSet.AffineTransRegion(ho_Rect3, out ho_P3Rect, hv_HomMat2D, "nearest_neighbor");
                hv_P3RectArea.Dispose(); hv_P3RectRow.Dispose(); hv_P3RectColumn.Dispose();
                HOperatorSet.AreaCenter(ho_P3Rect, out hv_P3RectArea, out hv_P3RectRow, out hv_P3RectColumn);
                hv_P3RectPhi.Dispose();
                HOperatorSet.OrientationRegion(ho_P3Rect, out hv_P3RectPhi);
                hv_MeasureHandle3.Dispose();
                HOperatorSet.GenMeasureRectangle2(hv_P3RectRow, hv_P3RectColumn, hv_P3RectPhi,
                    100, 20, hv_Width, hv_Height, "bicubic", out hv_MeasureHandle3);
                //定位P1、P2、P3
                hv_P1Row.Dispose(); hv_P1Col.Dispose(); hv_Amplitude.Dispose(); hv_Distance.Dispose();
                HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle1, 1, 30, "all", "all", out hv_P1Row,
                    out hv_P1Col, out hv_Amplitude, out hv_Distance);
                hv_P2Row.Dispose(); hv_P2Col.Dispose(); hv_Amplitude.Dispose(); hv_Distance.Dispose();
                HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle2, 1, 30, "all", "all", out hv_P2Row,
                    out hv_P2Col, out hv_Amplitude, out hv_Distance);
                hv_P3Row.Dispose(); hv_P3Col.Dispose(); hv_Amplitude.Dispose(); hv_Distance.Dispose();
                HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle3, 1, 30, "all", "all", out hv_P3Row,
                    out hv_P3Col, out hv_Amplitude, out hv_Distance);
            }
            ho_Rect1.Dispose();
            ho_Rect2.Dispose();
            ho_Rect3.Dispose();
            ho_P1Rect.Dispose();
            ho_P2Rect.Dispose();
            ho_P3Rect.Dispose();

            hv_HomMat2D.Dispose();
            hv_P1RectArea.Dispose();
            hv_P1RectRow.Dispose();
            hv_P1RectColumn.Dispose();
            hv_P1RectPhi.Dispose();
            hv_MeasureHandle1.Dispose();
            hv_P2RectArea.Dispose();
            hv_P2RectRow.Dispose();
            hv_P2RectColumn.Dispose();
            hv_P2RectPhi.Dispose();
            hv_MeasureHandle2.Dispose();
            hv_P3RectArea.Dispose();
            hv_P3RectRow.Dispose();
            hv_P3RectColumn.Dispose();
            hv_P3RectPhi.Dispose();
            hv_MeasureHandle3.Dispose();
            hv_Amplitude.Dispose();
            hv_Distance.Dispose();

            return;
        }

        public static void InitListView(ListView lv)
        {
            lv.GridLines = true;        //显示网格线
            lv.View = View.Details;         //显示详情
            lv.FullRowSelect = true;            //显示整行
            //lv.HoverSelection = true;           //鼠标悬停后自动选择


            // 添加列表头
            ColumnHeader C1 = new ColumnHeader();
            C1.Text = "Diameter";
            C1.Width = 110;
            lv.Columns.Add(C1);
            ColumnHeader C2 = new ColumnHeader();
            C2.Text = "PositionDegree";
            C2.Width = 120;
            lv.Columns.Add(C2);
            ColumnHeader C3 = new ColumnHeader();
            C3.Text = "DistanceX1";
            C3.Width = 110;
            lv.Columns.Add(C3);
            ColumnHeader C4 = new ColumnHeader();
            C4.Text = "DistanceY1";
            C4.Width = 110;
            lv.Columns.Add(C4);
            ColumnHeader C5 = new ColumnHeader();
            C5.Text = "DistanceL1L2";
            C5.Width = 110;
            lv.Columns.Add(C5);
            ColumnHeader C6 = new ColumnHeader();
            C6.Text = "RunTime";
             C6.Width = 110;
            lv.Columns.Add(C6);
        }

        public static void insertLine(ListView lv, double RunTime, double Radius, double PositionDegree,
             double DistanceL1L2,  double DistanceX1, double DistanceY1)
        {
            ListViewItem items = new ListViewItem(Radius.ToString("f5"));
            items.SubItems.Add(PositionDegree.ToString("f5"));
            items.SubItems.Add(DistanceX1.ToString("f5"));
            items.SubItems.Add(DistanceY1.ToString("f5"));
            items.SubItems.Add(DistanceL1L2.ToString("f5"));
            items.SubItems.Add(RunTime.ToString("f5"));
            lv.Items.Add(items);
  
        }

        public static void ClearListView(ListView lv)
        {
            lv.Clear();
        }
    }
}
