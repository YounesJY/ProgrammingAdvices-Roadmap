#pragma once
#include <iostream>

using namespace std;

enum choice { yes = 1, no = 2 };

namespace input {

    double readNumber(string message = "") {
                                                 double number;
        cout << message;
        cin >> number;

        while (cin.fail()) {
            cin.clear();
            cin.ignore(std::numeric_limits < std::streamsize>::max(), '\n');
            cout << "  Invalid Number!" << endl;
            cout << "  - Try again: \n" << endl;

            cout << message;
            cin >> number;
        }

        return number;
    }

    double readDouble(string messageToDisplay = "") {
        return readNumber(messageToDisplay);
    }

    int readInteger(string messageToDisplay = "") {
        return floor(readNumber(messageToDisplay));
    }

    int readPositiveInteger(string message = "") {
        int number;
        do {
            number = readInteger(message);
            if (number < 1) {
                cout << " !! Invalid value [" << number << "] !" << endl;
                cout << "  - Try again: \n" << endl;
            }
        } while (number < 1);
        return number;
    }

    int readPositiveInteger(int from, int to, string messageToDisplay = "") {
        int number;
        do {
            number = readPositiveInteger(messageToDisplay);
            if (number < from || number > to) {
                cout << " !! Invalid value [" << number << "] !" << endl;
                cout << "  - Try again: \n" << endl;
            }
        } while (number < from || number > to);
        return number;
    }

    enum choice getChoice(string messageToDisplay = "") {

        cout << endl;
        cout << messageToDisplay;
        cout << "  > [1] Yes" << endl;
        cout << "  > [2] No" << endl;
        cout << " => Your choice ? ";
       return (enum choice) readPositiveInteger(1, 2);

    }
}

