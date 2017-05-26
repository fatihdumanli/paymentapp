using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PaymentApp.Windows.Services
{
    public class Command : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action m_execute;
        private Func<bool> m_canExecute;


        public Command(Action execute, Func<bool> canExecute = null)
        {
            if (execute == null)
                throw new ArgumentNullException();
            m_execute = execute;
            m_canExecute = canExecute;
        }


        public bool CanExecute(object parameter)
        {
            if (m_canExecute == null)
                return true;

            return m_canExecute();

        }

        public void Execute(object parameter)
        {
            if (!CanExecute(parameter))
                return;

            try
            {
                m_execute();
            }
            catch (Exception ex)
            {
                Debugger.Break();
                throw;
            }
        }
    }

    public class Command<T> : System.Windows.Input.ICommand
    {
        private readonly Action<T> _execute;
        private readonly Func<T, bool> _canExecute;
        public event EventHandler CanExecuteChanged;

        public Command(Action<T> execute, Func<T, bool> canexecute = null)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            _execute = execute;
            _canExecute = canexecute ?? (e => true);
        }

        [DebuggerStepThrough]
        public bool CanExecute(object p)
        {
            try
            {
                var _Value = (T)Convert.ChangeType(p, typeof(T));
                return _canExecute == null ? true : _canExecute(_Value);
            }
            catch { return false; }
        }

        public void Execute(object p)
        {
            if (!CanExecute(p))
                return;
            var _Value = (T)Convert.ChangeType(p, typeof(T));
            _execute(_Value);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
