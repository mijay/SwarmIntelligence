namespace SILibrary.General.Background
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