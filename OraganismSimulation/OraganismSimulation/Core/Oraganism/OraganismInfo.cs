namespace OraganismSimulation.Oraganism.Core
{
    /// <summary>
    /// Oraganism의 정보
    /// </summary>
    public class OraganismInfo
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Value { get; set; }

        public OraganismInfo(double x, double y, double width, double height, double value)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
            this.Value = value;
        }
    }
}
