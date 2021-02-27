using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.Utilities.FileHelpers
{
    public class FileHelper
    {
        public static string Add(FileUpload objectFile, string path)
        {
            string photoName = string.Empty;
            string photoExtension = string.Empty;


            if (objectFile.files.Length > 0)
            {
                photoExtension = Path.GetExtension(objectFile.files.FileName);
                if (photoExtension.ToLower() == ".jpg" || photoExtension.ToLower() == ".png")
                {
                    photoName = Guid.NewGuid() + photoExtension;


                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    using (FileStream fileStream = System.IO.File.Create(path + photoName))
                    {
                        objectFile.files.CopyTo(fileStream);
                        fileStream.Flush();

                    }

                }


            }

            return photoName;
        }
    }
}
