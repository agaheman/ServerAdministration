namespace ServerAdministration.SQLServer.DTOs
{
    public class DatabasesInfoDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float SizeInMB { get; set; }
        public float SizeInGB => SizeInMB / 1024;
        public DBStatus DBStatus { get; set; }
        public string Owner { get; set; }
        public string Created { get; set; }

    }

    public class DBStatus
    {
        public string Status { get; set; }
        public string Recovery { get; set; }
        public int Version { get; set; }
        public string Colation { get; set; }
        public int CompabilityLevel { get; set; }

    }

}
