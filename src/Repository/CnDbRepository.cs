namespace Repository
{
    public class CnDbRepository : ICnDbRepository
    {
        public INationDataHandler Nations { get; }
        public IAllianceDataHelper Alliances { get; }
        public IWarDataHandler Wars { get; }
        public IAidDataHandler Aid { get; }

        public CnDbRepository(
            INationDataHandler nationDataHandler,
            IAllianceDataHelper allianceDataHandler,
            IWarDataHandler warDataHandler,
            IAidDataHandler aidDataHandler)
        {
            Nations = nationDataHandler;
            Alliances = allianceDataHandler;
            Wars = warDataHandler;
            Aid = aidDataHandler;
        }
    }
}
