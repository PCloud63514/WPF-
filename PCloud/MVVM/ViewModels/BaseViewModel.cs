using MVVM.Common;
using MVVM.Models;
using System;

namespace MVVM.ViewModels
{
    public abstract class BaseViewModel:Notifier, IDisposable
    {
        public BaseModel BaseModel { get; }
        public BaseViewModel(BaseModel baseModel)
        {
            BaseModel = baseModel;
            Initalize();
        }

        protected abstract void Initalize();

        #region IDisposable Support
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {

                Console.WriteLine("Dispose");
                BaseModel.Dispose(disposing);
            }
        }

        void IDisposable.Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
