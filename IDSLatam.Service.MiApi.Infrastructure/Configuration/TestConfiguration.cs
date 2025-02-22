using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IDSLatam.Service.MiApi.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IDSLatam.Service.MiApi.Infrastructure.Configuration
{
    public class TestConfiguration
    {
         public TestConfiguration(EntityTypeBuilder<Test> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(x => x.Key);
        }
    }
}