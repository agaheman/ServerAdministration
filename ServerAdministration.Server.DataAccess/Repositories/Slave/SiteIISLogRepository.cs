﻿using ServerAdministration.Server.DataAccess.DbContexts;
using ServerAdministration.Server.Entities;

namespace ServerAdministration.Server.DataAccess.Repositories.Slave
{
    public class SiteIISLogRepository : RepositorySlave<SiteIISLog>
    {
        public SiteIISLogRepository(SlaveDbContext dbContext) : base(dbContext)
        {
        }
    }
}
