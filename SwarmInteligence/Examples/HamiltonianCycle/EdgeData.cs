namespace HamiltonianCycle
{
	public class EdgeData
	{
		public double Odor { get; set; }
		public double Weight { get; private set; }

		public EdgeData(double weight, double odor)
		{
			Weight = weight;
			Odor = odor;
		}
	}
}