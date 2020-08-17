using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Automation;
using System.Windows.Input;

namespace ProcessInvokeCapture.ViewModels
{
    class MainWindowViewModel:BaseViewModel
    {
        #region Parameter
        private AutomationEventHandler MyEventHandler;
        private ObservableCollection<WrapProcess> _processCollection;
        public ObservableCollection<WrapProcess> ProcessCollection
        {
            get
            {
                if(_processCollection is null)
                {
                    _processCollection = new ObservableCollection<WrapProcess>();
                }
                return _processCollection;
            }
        }
        private ObservableCollection<WrapInvoke> _invokeElementCollection;
        public ObservableCollection<WrapInvoke> InvokeElementCollection
        {
            get
            {
                if(_invokeElementCollection is null)
                {
                    _invokeElementCollection = new ObservableCollection<WrapInvoke>();
                }
                return _invokeElementCollection;
            }
        }
        private ObservableCollection<string> _historyCollection;
        public ObservableCollection<string> HistoryCollection
        {
            get
            {
                if(_historyCollection is null)
                {
                    _historyCollection = new ObservableCollection<string>();
                }
                return _historyCollection;
            }
        }
        #endregion
        #region Command
        private ICommand _processListRefreshCommand;
        public ICommand ProcessListRefreshCommand
        {
            get
            {
                if(_processListRefreshCommand is null)
                {
                    _processListRefreshCommand = new RelayCommand(ProcessListRefresh);
                }
                return _processListRefreshCommand;
            }
        }
        private ICommand _processListSelectionChangedCommand;
        public ICommand ProcessListSelectionChangedCommand
        {
            get
            {
                if(_processListSelectionChangedCommand is null)
                {
                    _processListSelectionChangedCommand = new RelayCommand<object>(ProcessListSelectionChanged);
                }
                return _processListSelectionChangedCommand;
            }
        }
        private ICommand _invokeElementListSelectionChangedCommand;
        public ICommand InvokeElementListSelectionChangedCommand
        {
            get
            {
                if(_invokeElementListSelectionChangedCommand is null)
                {
                    _invokeElementListSelectionChangedCommand = new RelayCommand<object>(InvokeElementListSelectionChanged);
                }
                return _invokeElementListSelectionChangedCommand;
            }
        }
        #endregion
        #region CommandMethod
        private void ProcessListRefresh()
        {
            ProcessCollection.Clear();
            InvokeElementCollection.Clear();
            HistoryCollection.Clear();

            Process[] process = Process.GetProcesses();
            foreach (Process p in process)
            {
                if (p.MainWindowHandle != IntPtr.Zero)
                {
                    ProcessCollection.Add(new WrapProcess(p));
                }
            }
        }

        private void ProcessListSelectionChanged(object obj)
        {
            InvokeElementCollection.Clear();

            WrapInvoke wrapInvoke = GetWrapAE((WrapProcess)obj);
            if(wrapInvoke == null)
            {
                return;
            }
            Automation.AddAutomationEventHandler(
                InvokePattern.InvokedEvent,
                wrapInvoke.AE,
                TreeScope.Subtree,
                MyEventHandler = new AutomationEventHandler(OnUIAutomationEvent));
            Condition cond = new PropertyCondition(
                AutomationElement.IsInvokePatternAvailableProperty, true);
            AutomationElementCollection aec = wrapInvoke.AE.FindAll(TreeScope.Subtree, cond);

            foreach(AutomationElement ae in aec)
            {
                InvokeElementCollection.Add(new WrapInvoke(ae));
            }
        }
        private void InvokeElementListSelectionChanged(object obj)
        {
            WrapInvoke wrapInvoke = obj as WrapInvoke;
            wrapInvoke.Invoke();
        }
        #endregion

        public override void Initialize()
        {
            Process[] process = Process.GetProcesses();
            foreach (Process p in process)
            {
                if(p.MainWindowHandle != IntPtr.Zero)
                {
                    ProcessCollection.Add(new WrapProcess(p));
                }
            }
        }

        private WrapInvoke GetWrapAE(WrapProcess wrapProcess)
        {
            AutomationElement ae = wrapProcess.RootElement;
            return new WrapInvoke(ae);
        }

        private void OnUIAutomationEvent(object src, AutomationEventArgs e)
        {
            AutomationElement ae = src as AutomationElement;
            AddEvent(ae);
        }

        delegate void MyDele(AutomationElement ae);
        private void AddEvent(AutomationElement ae)
        {
            if (App.Current.Dispatcher.CheckAccess())
            {
                object[] objs = new object[1] { ae };
                App.Current.Dispatcher.Invoke(new MyDele(AddEvent), objs);
            }
            else
            {
                HistoryCollection.Add(ae.Current.Name + " 발생");
            }
        }
    }

    public class WrapInvoke
    {
        public AutomationElement AE { get; private set; }
        public string Name
        {
            get
            {
                return AE.Current.Name;
            }
        }
        public string ControlType
        {
            get
            {
                return AE.Current.LocalizedControlType;
            }
        }

        public WrapInvoke(AutomationElement ae)
        {
            AE = ae;
        }

        public void Invoke()
        {
            try
            {
                InvokePattern inv_pat = AE.GetCurrentPattern(InvokePattern.Pattern) as InvokePattern;
                inv_pat.Invoke();
            }
            catch
            {
            }
        }

        public override string ToString()
        {
            return ControlType + ":" + Name;
        }
    }

    public class WrapProcess
    {
        public Process Process;
        
        public string Title
        {
            get
            {
                return Process.ProcessName;
            }
        }
        /// <summary>
        /// MainWindow a Mapping
        /// </summary>
        public AutomationElement RootElement
        {
            get
            {
                if(Process.MainWindowHandle == IntPtr.Zero)
                {
                    return null;
                }
                return AutomationElement.FromHandle(Process.MainWindowHandle);
            }
        }
        public WrapProcess(Process process)
        {
            Process = process;
        }
        public override string ToString()
        {
            return Title;
        }
    }

    public class BaseViewModel : INotifyPropertyChanged, IDisposable
    {

        public BaseViewModel()
        {
            Initialize();
        }
        public virtual void Initialize()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {

            }
        }
       
        public void Dispose()
        {
            Dispose(true);
        }
    }
}
