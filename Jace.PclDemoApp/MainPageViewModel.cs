using Jace.Tokenizer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Jace.PclDemoApp
{
	public class MainPageViewModel : INotifyPropertyChanged
	{
		private TokenReader _reader = new TokenReader();

		public bool IsActive
		{
			set
			{
				RaisePropertyChanged("Formula");
			}
		}

		private static string _formula;

		public string Formula
		{
			get { return _formula; }
			set
			{
				if (_formula != value)
				{
					_formula = value;

					this.Variables = _reader.Read(_formula)
						.Where(w => w.TokenType == TokenType.Text)
						.Distinct(new TokenComparer())
						.Select(s => new Variable { Name = (string)s.Value })
						.ToList();

					this.Result = 0;
					RaisePropertyChanged("Formula");
				}
			}
		}

		private static ObservableCollection<Variable> _variables = new ObservableCollection<Variable>();

		public IList<Variable> Variables
		{
			get { return _variables; }
			set
			{
				if (_variables != value)
				{
					_variables.Clear();

					foreach (var item in value)
					{
						_variables.Add(item);
					}

					RaisePropertyChanged("Variables");
				}
			}
		}

		private double _result;

		public double Result
		{
			get { return _result; }
			set
			{
				if (_result != value)
				{
					_result = value;
					RaisePropertyChanged("Result");
				}
			}
		}

		public ICommand CalculateCommand { get { return new DelegateCommand(ExecuteCalculate); } }

		private void ExecuteCalculate()
		{
			CalculationEngine engine = new CalculationEngine();
			var formula = engine.Build(this.Formula);
			this.Result = formula(this.Variables.ToDictionary(k => k.Name, v => v.Value));
		}

		#region INotifyPropertyChanged

		public event PropertyChangedEventHandler PropertyChanged;
		private void RaisePropertyChanged(string propertyName)
		{
			var handler = this.PropertyChanged;
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion


		private class DelegateCommand : ICommand
		{
			private Action _executeMethod;
			public DelegateCommand(Action executeMethod)
			{
				_executeMethod = executeMethod;
			}

			public bool CanExecute(object parameter)
			{
				return true;
			}

			public event EventHandler CanExecuteChanged;

			public void Execute(object parameter)
			{
				_executeMethod.Invoke();
			}

		}
		
	}
}