using System;
using System.Collections.Generic;
using System.Text;
using Valyria.Models;

namespace Repository
{
    public interface ICnDbRepository
    {
        /// <summary>
        /// A handler that exposes access methods to all nation data currently known to our CN data repository
        /// </summary>
        INationDataHandler Nations { get; }

        /// <summary>
        /// A handler that exposes access methods to all bulk alliance data currently known to our CN data repository
        /// </summary>
        IAllianceDataHelper Alliances { get; }
    }
}
