using MVVM.ViewModels;
using OraganismSimulation.Models;

namespace OraganismSimulation.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Field
        private int _unitNum;
        private int _maxStep;
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
        public MainWindowViewModel():base(new MainWindowModel())
        {

        }

        protected override void Initalize()
        {

        }
    }
}
