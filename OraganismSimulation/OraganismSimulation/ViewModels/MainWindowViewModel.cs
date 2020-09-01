using GalaSoft.MvvmLight.Command;
using MVVM.ViewModels;
using OraganismSimulation.Core;
using OraganismSimulation.Models;
using System.Windows.Input;

namespace OraganismSimulation.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Field
        private bool _isRun = false;
        private int _unitNum;
        private int _maxStep;
        private string _planetBtnText = "Start";
        public string PlanetBtnText
        {
            get
            {
                return _planetBtnText;
            }
            set
            {
                _planetBtnText = value;
                OnPropertyChanged(nameof(PlanetBtnText));
            }
        }
        public int MaxStep
        {
            get
            {
                return _maxStep;
            }
            set
            {
                _maxStep = value;
                OnPropertyChanged(nameof(MaxStep));
            }
        }
        public int UnitNum
        {
            get
            {
                return _unitNum;
            }
            set
            {
                _unitNum = value;
                OnPropertyChanged(nameof(UnitNum));
            }
        }
        #endregion
        #region Command
        private ICommand _planetStartBtnClickCommand;
        public ICommand PlanetStartBtnClickCommand
        {
            get
            {
                if(_planetStartBtnClickCommand is null)
                {
                    _planetStartBtnClickCommand = new RelayCommand(PlanetStartBtnClick);
                }
                return _planetStartBtnClickCommand;
            }
        }
        #endregion
        #region CommandMethod
        private void PlanetStartBtnClick()
        {
            if (_isRun)
            {
                _isRun = false;
                PlanetBtnText = "Start";
                GlobalInstance.Instance.Broadcaster.Broadcast(Properties.Resources.Broadcast_PlanetStop, this, null);
            }
            else
            {
                _isRun = true;
                PlanetBtnText = "Stop";
                GlobalInstance.Instance.Broadcaster.Broadcast(Properties.Resources.Broadcast_PlanetStart, this, null);
            }
        }
        #endregion
        public MainWindowViewModel():base(new MainWindowModel())
        {

        }

        protected override void Initalize()
        {
            
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            GlobalInstance.Instance.Broadcaster.Broadcast(Properties.Resources.Broadcast_ViewModel_Dispose, this, null); 
        }
    }
}
