namespace Repository
{
    public class CnDbRepository : ICnDbRepository
    {
        public INationDataHandler Nations { get; }
        public IAllianceDataHelper Alliances { get; }
        public IWarDataHandler Wars { get; }

        public CnDbRepository(
            INationDataHandler nationDataHandler,
            IAllianceDataHelper allianceDataHandler,
            IWarDataHandler warDataHandler)
        {
            Nations = nationDataHandler;
            Alliances = allianceDataHandler;
            Wars = warDataHandler;
        }
    }
}
