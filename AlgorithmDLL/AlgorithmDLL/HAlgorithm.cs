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
               out double DistanceX1, out double DistanceY1, string path)
        {
            Radius = -1;
            PositionDegree = -1;
            RunTime = -1;
            DistanceX1 = -1;
            DistanceY1 = -1;
            HObject image;
            HOperatorSet.GenEmptyObj(out image);
            image.Dispose();
            HTuple hv_ImageFiles = new HTuple(), hv_Index = new HTuple();
            hv_ImageFiles.Dispose(); hv_Index.Dispose();
            bool MeasureIsSucced = true;
            
            list_image_files(path, "bmp", new HTuple(), out hv_ImageFiles);
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
                        Measure_18(image);
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
                    else
                    {
                        insertLine3D(lv, RunTime);
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
    out double Radius, out double PositionDegree, out double RunTime, out double DistanceX1, out double DistanceY1)
        {
            Radius = -1;
            PositionDegree = -1;
            RunTime = -1;
            DistanceX1 = -1;
            DistanceY1 = -1;
            HObject image;
            HOperatorSet.GenImage1Extern(out image, "byte", BufferWidth, BufferHeight, buffer, IntPtr.Zero);
            bool MeasureISucced = true;

            switch (MeasureProject)
            {
                case 9:
                    MeasureISucced = Measure_9(image, out Radius, out PositionDegree, out RunTime, out DistanceX1, out DistanceY1);
                    break;
                case 18:
                    MeasureISucced = Measure_18(image);
                    break;
                default:
                    break;
            }
            if(MeasureISucced == true)
            {
                insertLine2D(lv, RunTime, Radius, PositionDegree, DistanceX1, DistanceY1);
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
        //    public static bool Measure_9(HObject ho_Image, out double CirclrRadius, out double PositionDegree, out double RunTime,
        //        out double DistanceX1, out double DistanceY1)
        //    {
        //        CirclrRadius = -1;
        //        PositionDegree = -1;
        //        RunTime = -1;
        //        DistanceX1 = -1;
        //        DistanceY1 = -1;
        //        // Local iconic variables 
        //        HObject ho_BigCircle;
        //        // Local control variables 
        //        HTuple hv_Width = new HTuple(), hv_Height = new HTuple();
        //        HTuple hv_StartTime = new HTuple(), hv_MaxCircleRow = new HTuple();
        //        HTuple hv_MaxCircleColumn = new HTuple(), hv_MaxCircleRadius = new HTuple();
        //        HTuple hv_MinCircleRow = new HTuple(), hv_MinCircleColumn = new HTuple();
        //        HTuple hv_MinCircleRadius = new HTuple(), hv_TwoCirclePhi = new HTuple();
        //        HTuple hv_LeftTopRowEdge = new HTuple(), hv_LeftTopColumnEdge = new HTuple();
        //        HTuple hv_RightButtomRowEdge = new HTuple(), hv_RightButtomColumnEdge = new HTuple();
        //        HTuple hv_LeftButtomRowEdge = new HTuple(), hv_LeftButtomColumnEdge = new HTuple();
        //        HTuple hv_RightTopRowEdge = new HTuple(), hv_RightTopColumnEdge = new HTuple();
        //        HTuple hv_FitCircleCenterRow = new HTuple(), hv_FitCircleCenterCol = new HTuple();
        //        HTuple hv_FitCircleCenterRadius = new HTuple(), hv_ButtomRowEdge1 = new HTuple();
        //        HTuple hv_ButtomColumnEdge1 = new HTuple(), hv_ButtomRowEdge2 = new HTuple();
        //        HTuple hv_ButtomColumnEdge2 = new HTuple(), hv_RightRowEdge1 = new HTuple();
        //        HTuple hv_RightColumnEdge1 = new HTuple(), hv_RightRowEdge2 = new HTuple();
        //        HTuple hv_RightColumnEdge2 = new HTuple(), hv_X1 = new HTuple();
        //        HTuple hv_Y1 = new HTuple(), hv_StopTime = new HTuple();
        //        HTuple hv_runtime = new HTuple(), hv_CircleRadius = new HTuple();
        //        HTuple hv_PositionDegree = new HTuple();
        //        // Initialize local and output iconic variables 
        //        HOperatorSet.GenEmptyObj(out ho_BigCircle);
        //        hv_Width.Dispose(); hv_Height.Dispose();
        //        HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
        //        hv_StartTime.Dispose();
        //        HOperatorSet.CountSeconds(out hv_StartTime);
        //        hv_MaxCircleRow.Dispose(); hv_MaxCircleColumn.Dispose(); hv_MaxCircleRadius.Dispose(); hv_MinCircleRow.Dispose(); hv_MinCircleColumn.Dispose(); hv_MinCircleRadius.Dispose(); hv_TwoCirclePhi.Dispose();
        //        try
        //        {
        //            gen_TwoCircle_info(ho_Image, out hv_MaxCircleRow, out hv_MaxCircleColumn, out hv_MaxCircleRadius,
        //out hv_MinCircleRow, out hv_MinCircleColumn, out hv_MinCircleRadius, out hv_TwoCirclePhi);
        //            using (HDevDisposeHelper dh = new HDevDisposeHelper())
        //            {
        //                ho_BigCircle.Dispose(); hv_LeftTopRowEdge.Dispose(); hv_LeftTopColumnEdge.Dispose(); hv_RightButtomRowEdge.Dispose(); hv_RightButtomColumnEdge.Dispose(); hv_LeftButtomRowEdge.Dispose(); hv_LeftButtomColumnEdge.Dispose(); hv_RightTopRowEdge.Dispose(); hv_RightTopColumnEdge.Dispose(); hv_FitCircleCenterRow.Dispose(); hv_FitCircleCenterCol.Dispose(); hv_FitCircleCenterRadius.Dispose();
        //                gen_Edge_Circle(ho_Image, out ho_BigCircle, hv_MaxCircleRow, hv_MaxCircleColumn,
        //                    hv_TwoCirclePhi, hv_Width, hv_Height, (new HTuple(45)).TupleRad(), hv_MaxCircleRadius,
        //                    out hv_LeftTopRowEdge, out hv_LeftTopColumnEdge, out hv_RightButtomRowEdge,
        //                    out hv_RightButtomColumnEdge, out hv_LeftButtomRowEdge, out hv_LeftButtomColumnEdge,
        //                    out hv_RightTopRowEdge, out hv_RightTopColumnEdge, out hv_FitCircleCenterRow,
        //                    out hv_FitCircleCenterCol, out hv_FitCircleCenterRadius);
        //            }
        //            //定位底部测量矩形
        //            using (HDevDisposeHelper dh = new HDevDisposeHelper())
        //            {
        //                hv_ButtomRowEdge1.Dispose(); hv_ButtomColumnEdge1.Dispose(); hv_ButtomRowEdge2.Dispose(); hv_ButtomColumnEdge2.Dispose();
        //                gen_buttom_edge(ho_Image, hv_MaxCircleRow, hv_MaxCircleColumn, (hv_MaxCircleRadius * 3) / 2,
        //                    (hv_MaxCircleRadius * 3) / 4, hv_TwoCirclePhi, hv_Width, hv_Height, (new HTuple(90)).TupleRad()
        //                    , (new HTuple(90)).TupleRad(), hv_MinCircleColumn, hv_MaxCircleColumn, out hv_ButtomRowEdge1,
        //                    out hv_ButtomColumnEdge1, out hv_ButtomRowEdge2, out hv_ButtomColumnEdge2);
        //            }
        //            //定位右侧测量矩形
        //            using (HDevDisposeHelper dh = new HDevDisposeHelper())
        //            {
        //                hv_RightRowEdge1.Dispose(); hv_RightColumnEdge1.Dispose(); hv_RightRowEdge2.Dispose(); hv_RightColumnEdge2.Dispose();
        //                gen_right_edge(ho_Image, hv_MinCircleRow, hv_MinCircleColumn, (hv_MinCircleRadius * 3) / 4,
        //                    (hv_MinCircleRadius * 6) / 5, hv_MaxCircleColumn, hv_TwoCirclePhi, hv_Width,
        //                    hv_Height, (new HTuple(180)).TupleRad(), 0, out hv_RightRowEdge1, out hv_RightColumnEdge1,
        //                    out hv_RightRowEdge2, out hv_RightColumnEdge2);
        //            }
        //            //计算X1
        //            hv_X1.Dispose();
        //            HOperatorSet.DistancePl(hv_FitCircleCenterRow, hv_FitCircleCenterCol, hv_RightRowEdge1,
        //                hv_RightColumnEdge1, hv_RightRowEdge2, hv_RightColumnEdge2, out hv_X1);
        //            //计算Y1
        //            hv_Y1.Dispose();
        //            HOperatorSet.DistancePl(hv_FitCircleCenterRow, hv_FitCircleCenterCol, hv_ButtomRowEdge1,
        //                hv_ButtomColumnEdge1, hv_ButtomRowEdge2, hv_ButtomColumnEdge2, out hv_Y1);
        //            //计算运行时间
        //            hv_StopTime.Dispose();
        //            HOperatorSet.CountSeconds(out hv_StopTime);
        //            hv_runtime.Dispose();
        //            using (HDevDisposeHelper dh = new HDevDisposeHelper())
        //            {
        //                hv_runtime = (hv_StopTime - hv_StartTime) * 1000;
        //                RunTime = hv_runtime;
        //            }
        //            //输出结果
        //            hv_CircleRadius.Dispose();
        //            hv_CircleRadius = new HTuple(hv_FitCircleCenterRadius);
        //            CirclrRadius = hv_CircleRadius * 2 * 0.009443;
        //            hv_PositionDegree.Dispose();
        //            using (HDevDisposeHelper dh = new HDevDisposeHelper())
        //            {
        //                hv_PositionDegree = 2 * (((((((hv_X1 * 0.009443) - 19.605)).TuplePow(
        //                    2)) + ((((hv_Y1 * 0.009443) - 6.788)).TuplePow(2)))).TupleSqrt());
        //            }
        //            PositionDegree = hv_PositionDegree;
        //            DistanceX1 = hv_X1 * 0.009443;
        //            DistanceY1 = hv_Y1 * 0.009443;

        //            DispImage(ho_Image);
        //            outWindow.SetColor("red");
        //            outWindow.SetLineWidth(2);
        //            outWindow.SetDraw("margin");
        //            outWindow.DispCircle(hv_FitCircleCenterRow, hv_FitCircleCenterCol, hv_FitCircleCenterRadius);
        //            outWindow.DispLine(hv_ButtomRowEdge1, hv_ButtomColumnEdge1, hv_ButtomRowEdge2, hv_ButtomColumnEdge2);
        //            outWindow.DispLine(hv_RightRowEdge1, hv_RightColumnEdge1, hv_RightRowEdge2, hv_RightColumnEdge2);

        //            double RightCenterRow = (hv_RightRowEdge1 + hv_RightRowEdge2) / 2;
        //            double RightCenterCol = (hv_RightColumnEdge1 + hv_RightColumnEdge2) / 2;
        //            double ButtomCenterRow = (hv_ButtomRowEdge1 + hv_ButtomRowEdge2) / 2;
        //            double ButtomCenterCol = (hv_ButtomColumnEdge1 + hv_ButtomColumnEdge2) / 2;
        //            outWindow.DispArrow(hv_FitCircleCenterRow, hv_FitCircleCenterCol, (HTuple)RightCenterRow, (HTuple)RightCenterCol, (HTuple)6);
        //            outWindow.DispArrow(hv_FitCircleCenterRow, hv_FitCircleCenterCol, (HTuple)ButtomCenterRow, (HTuple)ButtomCenterCol, (HTuple)6);

        //        }
        //        catch (Exception ex)
        //        {
        //            //MessageBox.Show("请正确放置工件");
        //            return false;
        //        }

        //        ho_Image.Dispose();
        //        ho_BigCircle.Dispose();

        //        hv_Width.Dispose();
        //        hv_Height.Dispose();
        //        hv_StartTime.Dispose();
        //        hv_MaxCircleRow.Dispose();
        //        hv_MaxCircleColumn.Dispose();
        //        hv_MaxCircleRadius.Dispose();
        //        hv_MinCircleRow.Dispose();
        //        hv_MinCircleColumn.Dispose();
        //        hv_MinCircleRadius.Dispose();
        //        hv_TwoCirclePhi.Dispose();
        //        hv_LeftTopRowEdge.Dispose();
        //        hv_LeftTopColumnEdge.Dispose();
        //        hv_RightButtomRowEdge.Dispose();
        //        hv_RightButtomColumnEdge.Dispose();
        //        hv_LeftButtomRowEdge.Dispose();
        //        hv_LeftButtomColumnEdge.Dispose();
        //        hv_RightTopRowEdge.Dispose();
        //        hv_RightTopColumnEdge.Dispose();
        //        hv_FitCircleCenterRow.Dispose();
        //        hv_FitCircleCenterCol.Dispose();
        //        hv_FitCircleCenterRadius.Dispose();
        //        hv_ButtomRowEdge1.Dispose();
        //        hv_ButtomColumnEdge1.Dispose();
        //        hv_ButtomRowEdge2.Dispose();
        //        hv_ButtomColumnEdge2.Dispose();
        //        hv_RightRowEdge1.Dispose();
        //        hv_RightColumnEdge1.Dispose();
        //        hv_RightRowEdge2.Dispose();
        //        hv_RightColumnEdge2.Dispose();
        //        hv_X1.Dispose();
        //        hv_Y1.Dispose();
        //        hv_StopTime.Dispose();
        //        hv_runtime.Dispose();
        //        hv_CircleRadius.Dispose();
        //        hv_PositionDegree.Dispose();
        //        return true;
        //    }


        public static void gen_pixel2real_distance(HTuple hv_PixclRealDis, out HTuple hv_p1_XOffest, out HTuple hv_p1_YOffest, 
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
          

        public static bool Measure_9(HObject ho_Image, out double CircleRadius, out double PositionDegree, out double RunTime,
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
            gen_pixel2real_distance(hv_PixclRealDis, out hv_p1_XOffest, out hv_p1_YOffest,out hv_p2_XOffest, out hv_p2_YOffest, 
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
                    CircleRadius = (hv_EdgeCircleCenterRadius * 2) * hv_PixclRealDis;
                }
                hv_PositionDegree.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    PositionDegree = 2 * (((((((hv_X1 * hv_PixclRealDis) - 19.605)).TuplePow(
                        2)) + ((((hv_Y1 * hv_PixclRealDis) - 6.788)).TuplePow(2)))).TupleSqrt());
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
                if (PositionDegree < 0 || PositionDegree > 0.6 || CircleRadius < 9.40 || CircleRadius > 9.50)
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
                }
            }
            catch (Exception ex)
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

        public static bool Measure_18(HObject ho_Image)
        {
            return true;
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

            ColumnHeader C6 = new ColumnHeader();
            C6.Text = "CurrentTime";
            C6.Width = 250;
            lv.Columns.Add(C6);
        }

        //向ListView中插入一行数据
        public static void insertLine3D(ListView lv, double RunTime)
        {
            ListViewItem items = new ListViewItem(RunTime.ToString("f5"));
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
