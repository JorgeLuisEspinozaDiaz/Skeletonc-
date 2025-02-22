using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IDSLatam.Common.Core.Base;

namespace IDSLatam.Service.MiApi.Core.Entities
{
    public class Test : EntityBase
    {

        public Test()
        {
            
        }
        public Guid Key { set; get; }
        public string Nombre { set; get; }
    }
}