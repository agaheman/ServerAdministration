using Microsoft.EntityFrameworkCore;
using ServerAdministration.Server.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerAdministration.Server.DataAccess.Repositories
{
    public class SiteIISLogRepository : Repository<SiteIISLog>
    {
        public SiteIISLogRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
