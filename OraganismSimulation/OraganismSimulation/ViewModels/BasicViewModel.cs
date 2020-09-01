using System;
using MVVM.Models;
using MVVM.ViewModels;
using OraganismSimulation.Core;

namespace OraganismSimulation.ViewModels
{
    /// <summary>
    /// MainWindowViewModel을 제외한 모든 자식 ViewModel
    /// </summary>
    public class BasicViewModel : BaseViewModel
    {
        public BasicViewModel(BaseModel baseModel) : base(baseModel)
        {

        }

        protected override void Initalize()
        {
            GlobalInstance.Instance.Broadcaster.Register(Properties.Resources.Broadcast_Model_Dispose, ViewModelDispose);
        }

        private void ViewModelDispose(object sender, object e)
        {
            Dispose(true);
        }
    }
}
