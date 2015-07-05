// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using System.Windows.Input;

namespace SyntaxHighlighting
{
	public class DelegateCommand : ICommand
	{
		Action<object> execute;
		Func<object, bool> canExecute;
		
		public DelegateCommand(Action<object> execute)
			: this(execute, null)
		{
		}
		
		public DelegateCommand(Action<object> execute, Func<object, bool> canExecute)
		{
			this.execute = execute;
			this.canExecute = canExecute;
		}
		
		public event EventHandler CanExecuteChanged {
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}
		
		public void Execute(object parameter)
		{
			execute(parameter);
		}
		
		public bool CanExecute(object parameter)
		{
			if (canExecute != null) {
				return canExecute(parameter);
			}
			return true;
		}
	}
}
