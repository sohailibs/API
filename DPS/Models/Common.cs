using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace DPS.Models
{
    public class Common
    {
        public void ExeceptionHandleWithMethodName(Exception ex, string MethodName)
        {
            string startupPath = System.IO.Path.GetFullPath(@"..\..\");

            string h = Environment.CurrentDirectory;

            //image.Save(HostingEnvironment.MapPath("~" + "\\ErrorLog.txt"));

            string FileFath = HostingEnvironment.MapPath("~" + "/ErrorLog.txt");

            if (!File.Exists(FileFath))
            {
                File.Create(FileFath);
            }
            string errorLogPath = @FileFath;
            File.AppendAllText(errorLogPath, Environment.NewLine + Environment.NewLine + Environment.NewLine + GetCurrentSaudiTime() + Environment.NewLine + "Message: " + ex.Message + Environment.NewLine + "StackTrace: " + ex.StackTrace + Environment.NewLine + ex.InnerException);
        }

        public DateTime GetCurrentSaudiTime()
        {
            TimeZoneInfo TimeZone = TimeZoneInfo.FindSystemTimeZoneById("Arab Standard Time");
            DateTime dateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZone);
            return dateTime;
        }

        

        public string CreateFolderPathByFolderNameWithCurrentDate(string IdentificationNo, string FolderName, string Type)
        {
            string RootPath = HostingEnvironment.MapPath("~");
            string Path = null;
            IdentificationNo = IdentificationNo.Replace('/', '-');
            DateTime CurrentDateTime = GetCurrentSaudiTime();
            try
            {
                if (Type == "Programe")
                {
                    if (!Directory.Exists(RootPath + "Files/" + FolderName + "/" + CurrentDateTime.ToString("MM-dd-yyyy")))
                    {
                        Directory.CreateDirectory(RootPath + "Files/" + FolderName + "/" + CurrentDateTime.ToString("MM-dd-yyyy"));
                        Path = "Files/" + FolderName + "/" + CurrentDateTime.ToString("MM-dd-yyyy");
                    }
                    else
                    {
                        Path = RootPath + "Files/" + FolderName + "/" + CurrentDateTime.ToString("MM-dd-yyyy");
                    }

                    if (!Directory.Exists(Path + "/" + IdentificationNo))
                    {
                        Directory.CreateDirectory(RootPath + "Files/" + FolderName + "/" + CurrentDateTime.ToString("MM-dd-yyyy") + "/" + IdentificationNo);
                        Path = "Files/" + FolderName + "/" + CurrentDateTime.ToString("MM-dd-yyyy") + "/" + IdentificationNo;
                    }
                    else
                    {
                        Path = "Files/" + FolderName + "/" + CurrentDateTime.ToString("MM-dd-yyyy") + "/" + IdentificationNo;
                    }

                }
                if (Type == "Investment")
                {
                    if (!Directory.Exists(RootPath + "Files/" + FolderName + "/" + CurrentDateTime.ToString("MM-dd-yyyy")))
                    {
                        Directory.CreateDirectory(RootPath + "Files/" + FolderName + "/" + CurrentDateTime.ToString("MM-dd-yyyy"));
                        Path = "Files/" + FolderName + "/" + CurrentDateTime.ToString("MM-dd-yyyy");
                    }
                    else
                    {
                        Path = RootPath + "Files/" + FolderName + "/" + CurrentDateTime.ToString("MM-dd-yyyy");
                    }

                    if (!Directory.Exists(Path + "/" + IdentificationNo))
                    {
                        Directory.CreateDirectory(RootPath + "Files/" + FolderName + "/" + CurrentDateTime.ToString("MM-dd-yyyy") + "/" + IdentificationNo);
                        Path = "Files/" + FolderName + "/" + CurrentDateTime.ToString("MM-dd-yyyy") + "/" + IdentificationNo;
                    }
                    else
                    {
                        Path = "Files/" + FolderName + "/" + CurrentDateTime.ToString("MM-dd-yyyy") + "/" + IdentificationNo;
                    }
                }

            }
            catch (Exception ex)
            {
                ExeceptionHandleWithMethodName(ex, "CommonDb GetFolderPathByFolderNameWithCurrentDate");
            }


            return Path;
        }

    }
}