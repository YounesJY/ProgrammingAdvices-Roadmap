using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Calculator
{
    private enum operations { none = 0, addition = 1, subtraction = 2, multiplication = 3, division = 4, clear };

    private static operations _operationToPerform = operations.clear;
    private static float _lastReceivedValue = 0;
    private static float _currentResult = 0;

    public static bool _isZero(float numberToCheck)
    {
        return (numberToCheck == 0);
    }

    private static string _getLastPerformedOperationName()
    {
        string[] operaionsNames = { "none", "Adding", "Subtarcting", "Multiplying by", "Dividing by", "Clearing" };

        if ((byte)_operationToPerform < 1 || (byte)_operationToPerform > 6)
            return operaionsNames[0];

        return operaionsNames[(byte)_operationToPerform];
    }


    public Calculator()
    {
        Calculator._operationToPerform = operations.none;
        Calculator._currentResult = 0;
    }


    public void clear()
    {
        _operationToPerform = operations.clear;
    }

    public void add(float value)
    {
        _operationToPerform = operations.addition;
        _lastReceivedValue = value;
        _currentResult += value;
    }

    public void subtract(float value)
    {
        _operationToPerform = operations.subtraction;
        _lastReceivedValue = value;
        _currentResult -= value;
    }

    public void multiply(float value)
    {
        _operationToPerform = operations.multiplication;
        _lastReceivedValue = value;
        _currentResult *= value;
    }

    public void divide(float value)
    {
        value = (_isZero(value) ? 1 : value);

        _operationToPerform = operations.division;
        _lastReceivedValue = value;
        _currentResult /= value;
    }


    public void printResult()
    {
        Console.WriteLine("----------------------------------");
        if (_operationToPerform == operations.none)
        {
            Console.WriteLine("-- [No operation has done yet]");

        }
        else
            Console.WriteLine($"-- Result after [{_getLastPerformedOperationName()}] [{_lastReceivedValue}] is: [{_currentResult}]");
        Console.WriteLine("----------------------------------");
    }
}



namespace CalcuatorProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Calculator calculator = new Calculator();

            calculator.printResult();

            calculator.clear();
            calculator.printResult();

            calculator.add(12);
            calculator.printResult();

            calculator.subtract(19);
            calculator.printResult();

            calculator.multiply(-3);
            calculator.printResult();

            calculator.divide(2);
            calculator.printResult();


            calculator.add(100);
            calculator.printResult();

            calculator.divide(0);
            calculator.printResult();


        }
    }
}
