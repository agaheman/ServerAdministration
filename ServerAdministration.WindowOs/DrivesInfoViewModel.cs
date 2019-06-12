using Common.Utilities;

namespace ServerAdministration.WindowOs
{
    public class DriveInfoViewModel
    {
        public string Name;
        public string VolumeLabel;
        public string TotalSize => DigitalStorage.ByteToHumanReadableSize(TotalSizeByte);
        public long TotalSizeByte;

        public string AvailableFreeSpace => DigitalStorage.ByteToHumanReadableSize(AvailableFreeSpaceByte);
        public long AvailableFreeSpaceByte;

        public string DriveFormat;
        public string DriveType;
    }
}
