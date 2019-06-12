using Common.Utilities;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ServerAdministration.WindowOs
{
    public static class FileAndFolder
    {
        public static long DirSize(DirectoryInfo dir)
        {
            return dir.GetFiles().Sum(fi => fi.Length) +
                   dir.GetDirectories().Sum(di => DirSize(di));
        }

        public static List<DriveInfoViewModel> GetDrivesInfo()
        {
            return DriveInfo.GetDrives().Select(
                d => new DriveInfoViewModel()
                {
                    Name = d.Name,
                    VolumeLabel = d.VolumeLabel,
                    AvailableFreeSpaceByte = d.AvailableFreeSpace,
                    TotalSizeByte = d.TotalSize,
                    DriveType = d.DriveType.ToString(),
                    DriveFormat = d.DriveFormat

                }).ToList();

        }

        public static DriveInfoViewModel GetDriveInfo(string path)
        {
            var driveInfo = DriveInfo.GetDrives()
                  .First(d => d.IsReady && d.RootDirectory.ToString() == Path.GetPathRoot(path));

            if (driveInfo == null)
                throw new DriveNotFoundException();

            return new DriveInfoViewModel()
            {
                Name = driveInfo.Name,
                VolumeLabel = driveInfo.VolumeLabel,
                AvailableFreeSpaceByte = driveInfo.AvailableFreeSpace,
                TotalSizeByte = driveInfo.TotalSize,
                DriveType = driveInfo.DriveType.ToString(),
                DriveFormat = driveInfo.DriveFormat
            };
        }

    }

}
