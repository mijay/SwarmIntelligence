using SwarmIntelligence2.Core;
using SwarmIntelligence2.Core.Coordinates;
using SwarmIntelligence2.GeneralImplementation;
using SwarmIntelligence2.TwoDimensional;

namespace Test2.BasicTest
{
    public class Test: TestBase
    {
        #region Setup/Teardown

        public override void SetUp()
        {
            base.SetUp();
            RangeValidator2D.Register();
            size = new Range<Coordinates2D>(new Coordinates2D(-4, -3), new Coordinates2D(12, 5));
            map = new DictionaryMap<Coordinates2D, NoDataBackground>(size);
            background = new DelegateBackground<Coordinates2D, NoDataBackground>(size, delegate { return new NoDataBackground(); });
        }

        #endregion

        private Range<Coordinates2D> size;
        private Map<Coordinates2D, NoDataBackground> map;
        private Background<Coordinates2D, NoDataBackground> background;
    }
}