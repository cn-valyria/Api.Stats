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
        IDataHandler<Nation> Nations { get; }
    }
}
