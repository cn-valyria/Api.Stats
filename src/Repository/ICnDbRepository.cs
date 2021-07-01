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

        /// <summary>
        /// A handler that exposes access methods to all war data currently known to our CN data repository
        /// </summary>
        IWarDataHandler Wars { get; }

        /// <summary>
        /// A handler that exposes access methods to all war data currently known to our CN data repository
        /// </summary>
        IAidDataHandler Aid { get; }
    }
}
