using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.Utilities.FileHelpers
{
    public class FileHelper
    {
        public static string Add(IFormFile objectFile, string path)
        {
            string photoName = string.Empty;
            string photoExtension = string.Empty;


            if (objectFile.Length > 0)
            {
                photoExtension = Path.GetExtension(objectFile.FileName);
                if (photoExtension.ToLower() == ".jpg" || photoExtension.ToLower() == ".png")
                {
                    photoName = Guid.NewGuid() + photoExtension;


                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    using (FileStream fileStream = System.IO.File.Create(path + photoName))
                    {
                        objectFile.CopyTo(fileStream);
                        fileStream.Flush();

                    }

                }


            }

            return photoName;
        }
        public static string Update(IFormFile files,string path)
        {
            string photoName = string.Empty;
            string photoExtension = string.Empty;


            if (files.Length > 0)
            {
                photoExtension = Path.GetExtension(files.FileName);
                if (photoExtension.ToLower() == ".jpg" || photoExtension.ToLower() == ".png")
                {
                    photoName = Guid.NewGuid() + photoExtension;


                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    using (FileStream fileStream = System.IO.File.Create(path + photoName))
                    {
                        files.CopyTo(fileStream);
                        fileStream.Flush();

                    }

                }
          

            }

            return photoName;
        }
    }
}
