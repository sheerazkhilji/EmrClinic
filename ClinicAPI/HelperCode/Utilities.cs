using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ClinicAPI.HelperCode
{
    public class Utilities
    {

        public static AttachmentType GetMimeType(string value)
        {
            if (String.IsNullOrEmpty(value))
                return new AttachmentType
                {
                    FriendlyName = "Unknown",
                    MimeType = "application/octet-stream",
                    Extension = ""
                };

            var data = value.Substring(0, 5);

            switch (data.ToUpper())
            {
                case "IVBOR":
                    return new AttachmentType
                    {
                        FriendlyName = "Photo",
                        MimeType = "image/png",
                        Extension = ".png"
                    };
                case "/9J/4":
                    return new AttachmentType
                    {
                        FriendlyName = "Photo",
                        MimeType = "image/jpg",
                        Extension = ".jpg"
                    };

                case "AAAAF":
                    return new AttachmentType
                    {
                        FriendlyName = "Video",
                        MimeType = "video/mp4",
                        Extension = ".mp4"
                    };
                case "JVBER":
                    return new AttachmentType
                    {
                        FriendlyName = "word",
                        MimeType = "application/",
                        Extension = ".pdf"
                    };

                case "UESDB":

                    return new AttachmentType
                    {
                        FriendlyName = "Documentss",
                        MimeType = "application/msword",
                        Extension = ".docx"

                    };

                default:
                    return new AttachmentType
                    {
                        FriendlyName = "Unknown",
                        MimeType = string.Empty,
                        Extension = ""
                    };
            }
        }

        public static string AppendTimeStamp(string fileName)
        {
            return string.Concat(Path.GetFileNameWithoutExtension(fileName), DateTime.Now.ToString("yyyyMMddHHmmssfff"), Path.GetExtension(fileName));
        }
        public static string AppendTimeStampForFiles(int TaskAssignId, string fileName)
        {
            string guid = Guid.NewGuid().ToString("N").Substring(0, 16);
            return string.Concat(
                TaskAssignId.ToString(),
                "-",
                Path.GetFileNameWithoutExtension(fileName),
                DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                guid,
                Path.GetExtension(fileName)
                );

        }

        public class AttachmentType
        {
            public string MimeType { get; set; }
            public string FriendlyName { get; set; }
            public string Extension { get; set; }
        }

    }
}