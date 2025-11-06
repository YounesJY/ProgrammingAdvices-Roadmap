#include <iostream>
#include "../../HeaderFiles/InputValidate.h"

using namespace std;

int main() {
    cout << InputValidate::isNumberBetween(5, 1, 10) << endl;
    cout << InputValidate::isNumberBetween(5.5, 1.3, 10.8) << endl;

    cout << InputValidate::isDateBetween(Date(),
        Date(8, 12, 2022),
        Date(31, 12, 2022)) << endl;

    cout << InputValidate::isDateBetween(Date(),
        Date(31, 12, 2022),
        Date(8, 12, 2022)) << endl;

    cout << "\nPlease Enter a Number:\n";
    int x = InputValidate::readIntNumber("Invalid Number, Enter again:\n");
    cout << "x=" << x;

    cout << "\nPlease Enter a Number between 1 and 5:\n";
    int y = InputValidate::readIntNumberBetween(1, 5, "Number is not within range, enter again:\n");
    cout << "y=" << y;

    cout << "\nPlease Enter a Double Number:\n";
    double a = InputValidate::readDoubleNumber("Invalid Number, Enter again:\n");
    cout << "a=" << a;

    cout << "\nPlease Enter a Double Number between 1 and 5:\n";
    double b = InputValidate::readDoubleNumberBetween(1, 5, "Number is not within range, enter again:\n");
    cout << "b=" << b;

    cout << endl << InputValidate::isValidDate(Date(35, 12, 2022)) << endl;

    system("pause>0");

    return 0;
}