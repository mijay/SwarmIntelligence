namespace SwarmIntelligence.GeneralImplementation.Background
{
    public class EmptyData
    {
        static EmptyData()
        {
            Instance = new EmptyData();
        }

        public static EmptyData Instance { get; private set; }
    }
}