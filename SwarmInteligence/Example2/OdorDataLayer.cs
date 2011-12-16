using SwarmIntelligence.Core;

namespace Example2
{
    public class OdorDataLayer<TKey> : DataLayer<TKey, OdorData>
    {
        private OdorData _odorData;

        public OdorDataLayer(double odor)
        {
            _odorData = new OdorData(odor);
        }

        public OdorDataLayer()
        {
            _odorData = new OdorData(0);
        }

        public override OdorData Get(TKey key)
        {
            return _odorData;
        }

        public override void Set(TKey key, OdorData data)
        {
            _odorData = data;
        }
    }
}
