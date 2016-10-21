using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using POWERPOINT = Microsoft.Office.Interop.PowerPoint;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using Microsoft.Office.Core;

namespace PointApp.Helper
{
    public class PPTDeal
    {
        private EncoderParameters ep;
        private ImageCodecInfo ici;
        private string CurrentFile = string.Empty;
        private int currentPage;
        private int pageCount;

        #region==基本的参数信息
        POWERPOINT.ApplicationClass objApp = null;
        POWERPOINT.Presentation objPresSet = null;
        POWERPOINT.SlideShowSettings objSSS;

        #endregion

        public PPTDeal()
        {
            ep = new EncoderParameters();
            ep.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 60L);
            ici = null;
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.MimeType == "image/jpeg")
                {
                    ici = codec;
                }
            }
        }

        public int CurrentPage
        {
            get { return currentPage; }
            //get { return this.objPresSet.SlideShowWindow.View.Slide.SlideIndex; }
        }

        public int PageCount
        {
            get { return pageCount; }
        }

        /// <summary>
        /// 打开PPT文档并播放显示。
        /// </summary>
        /// <param name="filePath">PPT文件路径</param>
        public void PPTOpen(string filePath)
        {
            try
            {
                CurrentFile = filePath;
                Thread.Sleep(100);
                //防止连续打开多个PPT程序.
                if (this.objApp != null)
                {
                    this.objApp.Quit();
                    this.objApp = null;
                }
                Thread.Sleep(100);

                objApp = new POWERPOINT.ApplicationClass();
                //以非只读方式打开,方便操作结束后保存.
                objPresSet = objApp.Presentations.Open(filePath, MsoTriState.msoCTrue, MsoTriState.msoCTrue, MsoTriState.msoFalse);
                //Prevent Office Assistant from displaying alert messages:
                //bAssistantOn = objApp.Assistant.On;
                //objApp.Assistant.On = false;
                objSSS = this.objPresSet.SlideShowSettings;
                objSSS.Run();
                currentPage = 1;
                pageCount = objPresSet.Slides.Count;
                //this.objPresSet.SlideShowWindow.View.GotoSlide(2, OFFICECORE.MsoTriState.msoFalse);
            }
            catch (Exception)
            {
                //this.objApp.Quit();
                //Thread.Sleep(1000);
                //PPTOpen(filePath);
            }
        }

        /// <summary>
        /// PPT换页
        /// </summary>
        /// <param name="file"></param>
        /// <param name="page"></param>
        public void SetPPTPage(string file, int page)
        {
            if (file != CurrentFile)
            {
                PPTOpen(file);
            }
            try
            {
                this.objPresSet.SlideShowWindow.View.GotoSlide(page, MsoTriState.msoFalse);
            }
            catch
            {

            }
        }

        /// <summary>
        /// 关闭PPT
        /// </summary>
        public void PPTClose()
        {
            CurrentFile = string.Empty;
            if (objApp != null)
            {
                this.objApp.Quit();
                this.objApp = null;
            }
        }

        public int SetImagesByPPT(string file, int imageWidth, int imageHeight)
        {
            string[] list = file.Split('\\');
            string pptName = list[list.Length - 1];
            string folderName = pptName.Substring(0, pptName.LastIndexOf("."));

            string pptImageFolderPath = Application.StartupPath + "\\PPTImages\\" + folderName;
            if (!Directory.Exists(pptImageFolderPath))
            {
                Directory.CreateDirectory(pptImageFolderPath);
            }
            //生成图片
            int count = GetPptPic(file, pptImageFolderPath, imageWidth, imageHeight);
            return count;
        }

        /// <summary>
        /// 切取PPT为图片
        /// </summary>
        private int GetPptPic(string pptPath, string imagePath, int imageWidth, int imageHeight)
        {
            int count = 0;
            try
            {
                POWERPOINT.ApplicationClass ac = new POWERPOINT.ApplicationClass();
                POWERPOINT.Presentation p = ac.Presentations.Open(pptPath, MsoTriState.msoCTrue, MsoTriState.msoCTrue, MsoTriState.msoFalse);
                count = p.Slides.Count; ;
                DeleteInDir(imagePath);
                p.SaveAs(imagePath, POWERPOINT.PpSaveAsFileType.ppSaveAsJPG, MsoTriState.msoTrue);
                RenameImage(imagePath, imageWidth, imageHeight);
                p.Close();
            }
            catch
            {

            }
            return count;
        }

        /// <summary>
        /// 删除文件夹中的所有文件
        /// </summary>
        /// <param name="szDirPath"></param>
        private void DeleteInDir(string szDirPath)
        {
            if (szDirPath.Trim() == "" || !Directory.Exists(szDirPath))
                return;
            DirectoryInfo dirInfo = new DirectoryInfo(szDirPath);

            FileInfo[] fileInfos = dirInfo.GetFiles();
            if (fileInfos != null && fileInfos.Length > 0)
            {
                foreach (FileInfo fileInfo in fileInfos)
                {
                    File.Delete(fileInfo.FullName); //删除文件
                }
            }
            DirectoryInfo[] dirInfos = dirInfo.GetDirectories();
            if (dirInfos != null && dirInfos.Length > 0)
            {
                foreach (DirectoryInfo childDirInfo in dirInfos)
                {
                    DeleteInDir(childDirInfo.ToString()); //递归
                }
            }
        }

        //完整图片
        private void RenameImage(string szDirPath, int imageWidth, int imageHeight)
        {
            if (szDirPath.Trim() == "" || !Directory.Exists(szDirPath))
            {
                return;
            }
            DirectoryInfo dirInfo = new DirectoryInfo(szDirPath);
            FileInfo[] fileInfos = dirInfo.GetFiles("*.jpg");
            int clearCount = 0;
            for (int i = 0; i < fileInfos.Length; i++)
            {
                if (i == 0)
                {
                    clearCount = fileInfos[i].Name.Length - 5;
                }
                //Console.WriteLine(fileInfos[i].Name.Substring(clearCount, fileInfos[i].Name.Length - clearCount).ToLower());

                GetThumbnail(fileInfos[i].FullName, fileInfos[i].DirectoryName + "//" + fileInfos[i].Name.Substring(clearCount, fileInfos[i].Name.Length - clearCount).ToLower(), imageHeight, imageWidth);
                File.Delete(fileInfos[i].FullName); //删除文件
            }
        }

        /// <SUMMARY>
        /// 图片无损缩放
        /// </SUMMARY>
        /// <PARAM name="sourceFile">图片源路径</PARAM>
        /// <PARAM name="destFile">缩放后图片输出路径</PARAM>
        /// <PARAM name="destHeight">缩放后图片高度</PARAM>
        /// <PARAM name="destWidth">缩放后图片宽度</PARAM>
        /// <RETURNS></RETURNS>
        public void GetThumbnail(string sourceFile, string destFile, int destHeight, int destWidth)
        {
            try
            {
                Bitmap b = new Bitmap(destWidth, destHeight);
                Image sourceImage = Image.FromFile(sourceFile);
                Graphics g = Graphics.FromImage(b);

                // 插值算法的质量
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(sourceImage, new Rectangle(0, 0, destWidth, destHeight), new Rectangle(0, 0, sourceImage.Width, sourceImage.Height), GraphicsUnit.Pixel);
                g.Dispose();
                b.Save(destFile, ici, ep);
                sourceImage.Dispose();
                b.Dispose();
            }
            catch
            {
                //return null;
            }
        }

        //获取置顶ppt的页数
        public int GetPageCountByPPT(string pptPath)
        {
            POWERPOINT.ApplicationClass ac = new POWERPOINT.ApplicationClass();
            POWERPOINT.Presentation p = ac.Presentations.Open(pptPath, MsoTriState.msoCTrue, MsoTriState.msoCTrue, MsoTriState.msoFalse);
            return p.Slides.Count;
        }

        internal void NextPage()
        {
            if (currentPage < pageCount)
            {
                try
                {
                    this.objPresSet.SlideShowWindow.View.GotoSlide(currentPage + 1, MsoTriState.msoFalse);
                    currentPage += 1;
                }
                catch
                {

                }
            }

        }

        internal void PrePage()
        {
            if (currentPage > 1)
            {
                try
                {
                    this.objPresSet.SlideShowWindow.View.GotoSlide(currentPage - 1, MsoTriState.msoFalse);
                    currentPage -= 1;
                }
                catch
                {

                }
            }
        }

        public void SetPPTPage(int page)
        {
            if (page <=this.pageCount)
            {
                try
                {
                    this.objPresSet.SlideShowWindow.View.GotoSlide(page, MsoTriState.msoFalse);
                    currentPage = page;
                }
                catch
                {

                }
            }
        }
    }
}
