using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorNotCom.Models
{
    class CalculatorModel
    {
        string result;

        public CalculatorModel(string firstOperand, string secondOperand, string operation)
        {
            ValidateOperand(firstOperand);
            ValidateOperand(secondOperand);
            ValidateOperation(operation);

            FirstOperand = firstOperand;
            SecondOperand = secondOperand;
            Operation = operation;
            result = string.Empty;
        }

        public CalculatorModel()
        {
            FirstOperand = string.Empty;
            SecondOperand = string.Empty;
            Operation = string.Empty;
            result = string.Empty;
        }

        public string FirstOperand { get; set; }
        public string SecondOperand { get; set; }
        public string Operation { get; set; }
        public string Result { get { return result; } }

        public void CalculateResult()
        {
            ValidateData();

            try
            {
                switch (Operation)
                {
                    case ("+"):
                        result = (Convert.ToDouble(FirstOperand) + Convert.ToDouble(SecondOperand)).ToString();
                        break;

                    case ("-"):
                        result = (Convert.ToDouble(FirstOperand) - Convert.ToDouble(SecondOperand)).ToString();
                        break;

                    case ("*"):
                        result = (Convert.ToDouble(FirstOperand) * Convert.ToDouble(SecondOperand)).ToString();
                        break;

                    case ("/"):
                        if (Convert.ToDouble(SecondOperand) == 0) throw new Exception("Division by zero");
                        result = (Convert.ToDouble(FirstOperand) / Convert.ToDouble(SecondOperand)).ToString();
                        break;
                }
            }
            catch (Exception)
            {
                result = "Error during calculation";
                throw;
            }
        }

        void ValidateData()
        {
            switch (Operation)
            {
                case "/":
                case "*":
                case "-":
                case "+":
                    ValidateOperand(FirstOperand);
                    ValidateOperand(SecondOperand);
                    break;
                default:
                    result = "Invalid operation: " + Operation;
                    throw new ArgumentException("Invalid Operation: " + Operation);
            }
        }
        private void ValidateOperation(string operation)
        {
            switch (operation)
            {
                case "+":
                case "-":
                case "/":
                case "*":
                    break;
                default:
                    result = "Invalid operation: " + operation;
                    throw new ArgumentException("Invalid Operation: " + operation);
            }
        }

        private void ValidateOperand(string operand)
        {
            try
            {
                Convert.ToDouble(operand);
            }
            catch (Exception)
            {
                result = "Invalid operand: " + operand;
                throw;
            }
        }
    }
}
