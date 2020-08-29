using System;
using MVVM.ViewModels;
using OraganismSimulation.Core;
using OraganismSimulation.Models;

namespace OraganismSimulation.ViewModels
{
    public class PlanetViewModel : BaseViewModel
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
            //throw new NotImplementedException();
        }
    }
}
