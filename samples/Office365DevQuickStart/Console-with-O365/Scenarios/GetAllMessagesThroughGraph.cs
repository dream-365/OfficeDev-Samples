namespace Console_with_O365.Scenarios
{
    class GetAllMessagesThroughGraph : Scenario
    {
        public override void Run()
        {
            var tmgr = new UserTokenManagement();

            var token = tmgr.AcquireToken(Settings.ResourceUrlOfGraph);

            var api = new Graph.GraphMailAPI(token);

            var result = api.GetMessages().Result;
        }
    }
}
