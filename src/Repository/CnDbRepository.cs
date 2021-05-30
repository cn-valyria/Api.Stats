using System;
using System.Collections.Generic;
using System.Text;
using Valyria.Models;

namespace Repository
{
    public class CnDbRepository : ICnDbRepository
    {
        public INationDataHandler Nations { get; }
        public IAllianceDataHelper Alliances { get; }

        public CnDbRepository(
            INationDataHandler nationDataHandler,
            IAllianceDataHelper allianceDataHandler)
        {
            Nations = nationDataHandler;
            Alliances = allianceDataHandler;
        }
    }
}
