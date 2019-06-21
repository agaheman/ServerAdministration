using System;

namespace ServerAdministration.Server.Entities
{
    public class Insurance:IEntity
    {
        public Insurance()
        {
            IsActive = true;
        }
        public byte InsuranceId { get; set; }
        public string ServerUrl { get; set; }
        public string Version { get; set; }
        public bool IsActive { get; set; }

        public DateTime? LastDataGatheringDateTime { get; set; }

        public InsurancesEnum InsuranceEnum
        {
            get
            {
                return (InsurancesEnum)InsuranceId;
            }
        }
    }
}
 