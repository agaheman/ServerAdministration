using Microsoft.EntityFrameworkCore;
using ServerAdministration.Server.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerAdministration.Server.DataAccess.Repositories
{
    public class IISLogEventRepository : Repository<IISLogEvent>
    {
        public IISLogEventRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
