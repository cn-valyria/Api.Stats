using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Valyria.Models;

namespace Repository
{
    public interface IAllianceDataHelper : IDataHandler<Alliance>
    {
        /// <summary>
        /// Get the alliance linked to a specific nation ID, or null if the nation does not exist or does not have an alliance
        /// </summary>
        Task<Alliance?> GetByNation(int nationId);
    }
}
