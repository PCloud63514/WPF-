using MVVM.Common;

namespace OraganismSimulation.Core
{
    public class GlobalInstance
    {
        public static GlobalInstance Instance { get; } = new GlobalInstance();
        public Broadcaster Broadcaster { get; } = new Broadcaster();
    }
}
