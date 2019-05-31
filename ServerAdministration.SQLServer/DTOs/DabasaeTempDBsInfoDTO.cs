namespace ServerAdministration.SQLServer.DTOs
{
    public class DabasaeTempDBsInfoDTO
    {
        public string Name { get; set; }
        public string LogicalName { get; set; }
        public string FileType { get; set; }
        public string PhysicalFilePath { get; set; }
        public string State { get; set; }
        public float SizeInMB { get; set; }
        public float SizeInGB => SizeInMB / 1024;
        public string MaxSize { get; set; }
    }
}
