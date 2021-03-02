using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchool.Utility
{
    public class FileService
    {
        public enum FileType
        {
            Document, Image, ImageOrPdf, Video
        }

        public static async Task<string> SaveDoc(IFormFile file, string folderName, FileType fileType)
        {
            try
            {
                string ext = Path.GetExtension(file.FileName);
                if (fileType == FileType.Document)
                {
                    if (!CheckIfFileIsADoc(ext.ToLower()))
                        throw new Exception();
                }
                else if (fileType == FileType.ImageOrPdf)
                {
                    if (!CheckIfFileIsImageOrPdf(ext.ToLower()))
                        throw new Exception();
                }

                string uniqueFileName = "";
                string uploadsFolder = "wwwroot/Files/" + folderName;
                Directory.CreateDirectory(uploadsFolder);
                uniqueFileName = Path.GetFileNameWithoutExtension(file.FileName) + "_" + Guid.NewGuid().ToString() + ext;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                return filePath;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static void DeleteFile(string path)
        {
            //string path = Request.MapPath(filePath);
            if (File.Exists(path))
            {
                try
                {
                    File.Delete(path);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

        }

        public static bool CheckIfFileIsVideo(string extension)
        {
            extension = extension.ToLower();
            if (extension == ".mp4" || extension == ".mkv")
                return true;

            return false;
        }


        public static bool CheckIfFileIsImage(string extension)
        {
            extension = extension.ToLower();
            if (extension == ".png" || extension == ".jpg" || extension == ".jpeg")
                return true;

            return false;
        }

        public static bool CheckIfFileIsImageOrPdf(string extension)
        {
            if (extension == ".pdf" || extension == ".png" || extension == ".jpg" || extension == ".jpeg")
                return true;

            return false;
        }

        public static bool CheckIfFileIsADoc(string extension)
        {
            if (extension == ".doc" || extension == ".docx" || extension == ".pdf" || extension == ".txt" || extension == ".xls"
                || extension == ".xlsx" || extension == ".ppt" || extension == ".pptx")
                return true;

            return false;
        }

        private static bool CheckFileSize(IFormFile file)
        {
            if (file.Length > (4 * 1000 * 1024))
                return false;

            return true;

        }

    }
}
