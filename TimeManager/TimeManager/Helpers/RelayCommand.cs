using System;
using System.Windows.Input;

namespace TimeManager.Helpers
{
    #region Classes

    /// <summary>
    /// A command whose sole purpose is to relay its functionality 
    /// to other objects by invoking delegates. 
    /// The default return value for the CanExecute method is 'true'.
    /// <see cref="RaiseCanExecuteChanged"/> needs to be called whenever
    /// <see cref="CanExecute"/> is expected to return a different value.
    /// </summary>
    public class RelayCommand : ICommand
    {
        #region Constants

        private readonly Func<bool> m_canExecuteFunc;

        private readonly Action m_executeAction;

        #endregion

        #region Events and Delegates

        /// <summary>
        /// Raised when RaiseCanExecuteChanged is called.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new command that can always executeAction.
        /// </summary>
        /// <param name="executeAction">The execution logic.</param>
        public RelayCommand(Action executeAction)
            : this(executeAction, null)
        {
        }

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="executeAction">The execution logic.</param>
        /// <param name="canExecuteFunc">The execution status logic.</param>
        public RelayCommand(Action executeAction, Func<bool> canExecuteFunc)
        {
            if (executeAction == null)
                throw new ArgumentNullException("executeAction");
            m_executeAction = executeAction;
            m_canExecuteFunc = canExecuteFunc;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Determines whether this <see cref="RelayCommand"/> can executeAction in its current state.
        /// </summary>
        /// <param name="parameter">
        /// Data used by the command. If the command does not require data to be passed, this object can be set to null.
        /// </param>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        public bool CanExecute(object parameter)
        {
            return m_canExecuteFunc == null || m_canExecuteFunc();
        }

        /// <summary>
        /// Executes the <see cref="RelayCommand"/> on the current command target.
        /// </summary>
        /// <param name="parameter">
        /// Data used by the command. If the command does not require data to be passed, this object can be set to null.
        /// </param>
        public void Execute(object parameter)
        {
            m_executeAction();
        }

        /// <summary>
        /// Method used to raise the <see cref="CanExecuteChanged"/> event
        /// to indicate that the return value of the <see cref="CanExecute"/>
        /// method has changed.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        #endregion
    }

    /// <summary>
    /// A command whose sole purpose is to relay its functionality 
    /// to other objects by invoking delegates. 
    /// The default return value for the CanExecute method is 'true'.
    /// <see cref="RaiseCanExecuteChanged"/> needs to be called whenever
    /// <see cref="CanExecute"/> is expected to return a different value.
    /// </summary>
    public class RelayCommand<T> : ICommand
    {
        #region Constants

        private readonly Predicate<T> m_canExecuteFunc;

        private readonly Action<T> m_executeAction;

        #endregion

        #region Events and Delegates

        /// <summary>
        /// Raised when RaiseCanExecuteChanged is called.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new command that can always executeAction.
        /// </summary>
        /// <param name="executeAction">The execution logic.</param>
        public RelayCommand(Action<T> executeAction)
            : this(executeAction, null)
        {
        }

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="executeAction">The execution logic.</param>
        /// <param name="canExecuteFunc">The execution status logic.</param>
        public RelayCommand(Action<T> executeAction, Predicate<T> canExecuteFunc)
        {
            if (executeAction == null)
                throw new ArgumentNullException("executeAction");
            m_executeAction = executeAction;
            m_canExecuteFunc = canExecuteFunc;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Determines whether this <see cref="RelayCommand"/> can executeAction in its current state.
        /// </summary>
        /// <param name="parameter">
        /// Data used by the command. If the command does not require data to be passed, this object can be set to null.
        /// </param>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        public bool CanExecute(object parameter)
        {
            return m_canExecuteFunc == null || m_canExecuteFunc((T)parameter);
        }

        /// <summary>
        /// Executes the <see cref="RelayCommand"/> on the current command target.
        /// </summary>
        /// <param name="parameter">
        /// Data used by the command. If the command does not require data to be passed, this object can be set to null.
        /// </param>
        public void Execute(object parameter)
        {
            m_executeAction((T)parameter);
        }

        /// <summary>
        /// Method used to raise the <see cref="CanExecuteChanged"/> event
        /// to indicate that the return value of the <see cref="CanExecute"/>
        /// method has changed.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        #endregion
    }

    #endregion
}

