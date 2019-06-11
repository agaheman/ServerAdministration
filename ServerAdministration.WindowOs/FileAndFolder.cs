using Common.Utilities;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ServerAdministration.WindowOs
{
    public class FileAndFolder
    {
        public List<DrivesInfoViewModel> GetDrivesInfo()
        {
            return DriveInfo.GetDrives().Select(
                d => new DrivesInfoViewModel()
                {
                    Name = d.Name,
                    VolumeLabel = d.VolumeLabel,
                    AvailableFreeSpace = DigitalStorage.ByteToHumanReadableSize(d.AvailableFreeSpace),
                    TotalSize = DigitalStorage.ByteToHumanReadableSize(d.TotalSize),
                    DriveType = d.DriveType.ToString(),
                    DriveFormat = d.DriveFormat

                }).ToList();

        }


        public class DrivesInfoViewModel
        {
            public string Name;
            public string VolumeLabel;
            public string TotalSize;
            public string AvailableFreeSpace;
            public string DriveFormat;
            public string DriveType;
        }

    }

}
