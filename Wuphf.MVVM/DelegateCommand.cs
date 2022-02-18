using System;
using System.Windows.Input;

namespace Wuphf.MVVM
{
    public class DelegateCommand : ICommand
    {
        private Func<bool> _canExecute;
        private Action _method;
        public event EventHandler CanExecuteChanged;
        public DelegateCommand(Action method)
        : this(method, new Func<bool>(() => true))
        {
            _method = method;
        }
        public DelegateCommand(Action method, Func<bool> canExecute)
        {
            _method = method;
            _canExecute = canExecute;
        }
        protected void OnCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged.Invoke(this, EventArgs.Empty);
            }
        }
        public bool CanExecute(object o)
        {
            if (_canExecute == null)
            {
                return true;
            }
            return _canExecute();
        }
        public void Execute(object o)
        {
            _method.Invoke();
        }
    }
    public class DelegateCommand<T> : ICommand
    {
        private Func<T, bool> _canExecute;
        private Action<T> _method;
        public event EventHandler CanExecuteChanged;
        public DelegateCommand(Action<T> method)
        : this(method, new Func<T, bool>((s) => true))
        {
            _method = method;
        }
        public DelegateCommand(Action<T> method, Func<T, bool> canExecute)
        {
            _method = method;
            _canExecute = canExecute;
        }
        protected void OnCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged.Invoke(this, EventArgs.Empty);
            }
        }
        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
            {
                return true;
            }
            T parm = default;
            if (parameter != null)
            {
                parm = (T)parameter;
            }
            return _canExecute(parm);
        }
        public void Execute(object parameter)
        {
            if (_method == null)
            {
                return;
            }
            T parm = default;
            if (parameter != null)
            {
                parm = (T)parameter;
            }
            _method.Invoke(parm);
        }
    }
}
