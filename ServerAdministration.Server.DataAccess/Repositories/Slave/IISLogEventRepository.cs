using Microsoft.EntityFrameworkCore;
using ServerAdministration.Server.DataAccess.DbContexts;
using ServerAdministration.Server.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerAdministration.Server.DataAccess.Repositories.Slave
{
    public class IISLogEventRepository : RepositorySlave<IISLogEvent>
    {
        public IISLogEventRepository(SlaveDbContext dbContext) : base(dbContext)
        {
        }
    }
}
