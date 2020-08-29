using System;

namespace MVVM.Models
{
    public abstract class BaseModel : IDisposable
    {
        public BaseModel()
        {
            Initalize();
        }

        public abstract void Initalize();

        #region IDisposable Support
        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
        }

        void IDisposable.Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
