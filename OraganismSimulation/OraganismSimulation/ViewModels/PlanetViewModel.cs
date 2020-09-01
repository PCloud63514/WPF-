using OraganismSimulation.Core;
using OraganismSimulation.Models;

namespace OraganismSimulation.ViewModels
{
    public class PlanetViewModel : BasicViewModel
    {
        #region Field
        private string _text;
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                OnPropertyChanged(nameof(Text));
            }
        }
        #endregion
        public PlanetViewModel() : base(new PlanetModel())
        {
            Text = "Planet";
            GlobalInstance.Instance.Broadcaster.Register(Properties.Resources.Broadcast_PlanetStart, PlanetStart);
            GlobalInstance.Instance.Broadcaster.Register(Properties.Resources.Broadcast_PlanetStop, PlanetStop);
        }

        private void PlanetStop(object sender, object e)
        {
            Text = "PlanetStop";
        }

        private void PlanetStart(object sender, object e)
        {
            Text = "PlanetStart";
        }

        protected override void Initalize()
        {
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
