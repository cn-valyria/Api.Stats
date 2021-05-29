using System;
using System.Collections.Generic;
using System.Text;
using Valyria.Models;

namespace Repository
{
    public class CnDbRepository : ICnDbRepository
    {
        public IDataHandler<Nation> Nations { get; }

        public CnDbRepository(
            IDataHandler<Nation> nationDataHandler)
        {
            Nations = nationDataHandler;
        }
    }
}
