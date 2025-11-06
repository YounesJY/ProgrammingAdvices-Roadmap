// CalcuatorProject.cpp : This file contains the 'main' function. Program execution begins and ends there.

#include <iostream>
#include <iomanip>
//#include "../MyInputLibrary.h";
#include "../MyOthersStuffLibrary.h";

//using namespace input;
using namespace others;
using namespace std;

class Calculator {
private:
	enum operations { addition = 1, subtraction = 2, multiplication = 3, division = 4, canceling = 5, clearing = 6, exitCalculator = 7 };

	enum operations _operationToPerform = (enum operations)0;
	int _lastReceivedValue = 0;
	int _oldResult = 0;
	int _currentResult = 0;

	bool _isZero(float numberToCheck) { return (numberToCheck == 0); }

	string _getLastPerformedOperationName() {
		string operaionsNames[] = { "", "Adding", "Subtarcting", "Multiplying by", "Dividing by" , "Cancelling last operation" , "Clearing" };

		if (_operationToPerform < 1 || _operationToPerform > 6)
			return "";

		return operaionsNames[_operationToPerform];
	}


	void _clear() {
		_operationToPerform = operations::clearing;
		_lastReceivedValue = 0;
		_oldResult = _currentResult = 0;
	}

	void _add() {
		_operationToPerform = operations::addition;
		_oldResult = _currentResult;
		_currentResult += _lastReceivedValue;
	}

	void _subtract() {
		_operationToPerform = operations::subtraction;
		_oldResult = _currentResult;
		_currentResult -= _lastReceivedValue;
	}

	void _multiply() {
		_operationToPerform = operations::multiplication;
		_oldResult = _currentResult;
		_currentResult *= _lastReceivedValue;
	}

	void _divide() {
		_lastReceivedValue = (_isZero(_lastReceivedValue) ? 1 : _lastReceivedValue);

		_operationToPerform = operations::division;
		_oldResult = _currentResult;
		_currentResult /= _lastReceivedValue;
	}

	void _cancelLastPerformedOperation() {
		_operationToPerform = operations::canceling;
		_lastReceivedValue = 0;
		_currentResult = _oldResult;
	}

	void _printResult() {
		cout << "----------------------------------" << endl;
		cout << "-- Result after [" << _getLastPerformedOperationName() << " " << _lastReceivedValue << "] is: [" << _currentResult << "]" << endl;
		cout << "----------------------------------" << endl;
	}


	void _goBackToTheMainMenu() {
		cout << " -> Press any key to go back to the main menu... ";
		system("pause>0");
		runCalculatorApp();
	}

	void _showCalculatorMainMenu() {
		cout << "======================================================" << endl;
		cout << "==              Calculator Main Menu                ==" << endl;
		cout << "======================================================" << endl;
		cout << "==\t [1] Add                                    ==" << endl;
		cout << "==\t [2] Subtract                               ==" << endl;
		cout << "==\t [3] Multiply                               ==" << endl;
		cout << "==\t [4] Divide                                 ==" << endl;
		cout << "==\t [5] Cancel last performed operation        ==" << endl;
		cout << "==\t [6] Clear calcualtor                       ==" << endl;
		cout << "==\t [7] Exit                                   ==" << endl;
		cout << "======================================================" << endl;
	}

	enum operations _askForOperationToPerforme(string messageToDisplay, short from, short to) {
		cout << "  - What operation do you want execute ?" << endl;
		return (enum operations)readPositiveInteger(from, to, messageToDisplay);
	}

	void _executeOperation() {
		system("cls");

		if ((_operationToPerform != operations::canceling) && (_operationToPerform != operations::clearing) && (_operationToPerform != operations::exitCalculator))
			_lastReceivedValue = readInteger(" -> What number to perfrom [" + _getLastPerformedOperationName() + " operation] with ? ");

		switch (_operationToPerform)
		{
		case Calculator::addition:
			_add();
			break;
		case Calculator::subtraction:
			_subtract();
			break;
		case Calculator::multiplication:
			_multiply();
			break;
		case Calculator::division:
			_divide();
			break;
		case Calculator::canceling:
			_cancelLastPerformedOperation();
			break;
		case Calculator::clearing:
			_clear();
			break;
		case Calculator::exitCalculator:
			exit(0);
		default:
			exit(0);
		}

		_printResult();
		_goBackToTheMainMenu();
	}


public:

	void runCalculatorApp() {
		system("cls");
		_showCalculatorMainMenu();
		_operationToPerform = _askForOperationToPerforme(" -> Please enter your choice [1 to 7] ? ", 1, 7);
		_executeOperation();
	}
};

int main()
{
	Calculator calc;

	calc.runCalculatorApp();


}
