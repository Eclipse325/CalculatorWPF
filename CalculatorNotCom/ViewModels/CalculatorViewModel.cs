using CalculatorNotCom.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorNotCom.ViewModels
{
    class CalculatorViewModel : ViewModelBase
    {
        Models.CalculatorModel calculator;
        private bool newDisplayRequired = false;
        private string display;

        private RelayCommand digitButtonClick;
        private RelayCommand signButtonClick;
        private RelayCommand equalButtonClick;

        public CalculatorViewModel()
        {
            calculator = new Models.CalculatorModel();
            display = "0";
            FirstOperand = string.Empty;
            SecondOperand = string.Empty;
            Operation = string.Empty;
        }

        public RelayCommand DigitButtonClick
        {
            get
            {
                return digitButtonClick ??
                  (digitButtonClick = new RelayCommand(obj =>
                  {
                      DisplayButtonClick(Convert.ToString(obj));
                  }));
            }
        }

        public RelayCommand SignButtonClick
        {
            get
            {
                return signButtonClick ??
                  (signButtonClick = new RelayCommand(obj =>
                  {
                      OperationButtonClick(Convert.ToString(obj));

                  }));
            }
        }

        public RelayCommand EqualButtonClick
        {
            get
            {
                return equalButtonClick ??
                  (equalButtonClick = new RelayCommand(obj =>
                  {
                      EqualButtonClickMethod();
                  }));
            }
        }

        void DisplayButtonClick(string sign)
        {
            switch (sign)
            {
                case "C":
                    Display = "0";
                    FirstOperand = string.Empty;
                    SecondOperand = string.Empty;
                    Operation = string.Empty;
                    break;
                case "Del":
                    if (display.Length > 1)
                        Display = display.Substring(0, display.Length - 1);
                    else Display = "0";
                    break;
                case ",":
                    if (newDisplayRequired)
                    {
                        Display = "0,";
                    }
                    else
                    {
                        if (!display.Contains(","))
                        {
                            Display = display + ",";
                        }
                    }
                    break;
                default:
                    if (display == "0" || newDisplayRequired)
                            Display = sign;

                    else
                        Display = display + sign;
                    break;
            }
            newDisplayRequired = false;
        }

        public void OperationButtonClick(string operation)
        {
                if (FirstOperand == string.Empty || FirstOperand == "0")
                {
                    FirstOperand = display;
                    Operation = operation;
                }
                else
                {
                    if (SecondOperand == string.Empty) SecondOperand = display;
                        Operation = operation;
                    try { calculator.CalculateResult(); }
                    catch(Exception ex) 
                        { 
                            Display = string.Empty;  
                            Display = ex.Message; 
                            FirstOperand = string.Empty; 
                            newDisplayRequired = true; 
                            return; 
                         }


                    Display = Result;
                    FirstOperand = display;
                }
                newDisplayRequired = true;
        }

        public void EqualButtonClickMethod()
        {
            if (SecondOperand == string.Empty) SecondOperand = display;
           
                try { calculator.CalculateResult(); }
                catch (Exception ex)
                {
                    Display = string.Empty;
                    Display = ex.Message;
                    FirstOperand = string.Empty;
                    newDisplayRequired = true;
                    return;
                }
                Display = Result;
                calculator.FirstOperand = display;
            newDisplayRequired = true;
        }

        public string FirstOperand
        {
            get { return calculator.FirstOperand; }
            set { calculator.FirstOperand = value;  }
        }

        public string SecondOperand
        {
            get { return calculator.SecondOperand; }
            set { 
                    calculator.SecondOperand = value;
                }
        }

        public string Operation
        {
            get { return calculator.Operation; }
            set { calculator.Operation = value; }
        }

        public string Result
        {
            get { return calculator.Result; }
        }

        public string Display
        {
            get { return display; }
            set
            {
                display = value;
                OnPropertyChanged("Display");
            }
        }


    }
}
