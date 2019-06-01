using System;
using System.Collections.Generic;
using System.IO;
using Ionic.Zip;

namespace Common
{
    public static class ZipHelper
    {
        public static string ZipLogFiles(List<string> logFilesPaths, string outputFolderPath = @"ArchivedIISLogs", string corporateName = "test")
        {
            string restulPath;
            using (ZipFile zip = new ZipFile())
            {
                zip.AddFiles(logFilesPaths, "");
                string zipFileName = corporateName + "-Hourly [" + DateTime.Now.ToString("yyyyMMddH") + "].zip";
                restulPath = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), outputFolderPath, zipFileName));
                zip.Save(restulPath);
            }
            return restulPath;
        }
    }
}
