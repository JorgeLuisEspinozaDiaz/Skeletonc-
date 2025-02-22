using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IDSLatam.Service.MiApi.Application.Repositories;
using IDSLatam.Service.MiApi.Core.Entities;

namespace IDSLatam.Service.MiApi.Infrastructure.Repositories
{
    public class TestRepository: RepositoryBase<Test>, ITest
    {
         public TestRepository(BaseDbContext dbContext): base(dbContext)
        {
        }
    }
}