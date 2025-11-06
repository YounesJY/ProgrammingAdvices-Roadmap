#include<iostream>
using namespace std;

enum enCountry {Egypt = 1, Morocco = 2, Algeria = 3};
enum enOperations { Sum = 1, Subtraction = 2, Multiplication  = 3, Division = 4};
enum enDays { Monday = 1, Thesday = 2, Wednesday = 3, Thursday = 4, Friday = 5, Saturday = 6, Sunday = 7};

void ReadCountryNumber(short int &countryChoice) {

    cout << "--> Please enter the number of your Country:" << endl;
    cout << "  - [1] Egypt"   << endl;
    cout << "  - [2] Morocco" << endl;
    cout << "  - [3] Algeria" << endl;
    cout << "  > Your choice ? ";
    cin >> countryChoice;

}

string GetCountry(short int countryChoice) {

    enCountry Country = (enCountry) countryChoice;

    if (Country == enCountry::Egypt) {
        return ( "Egypt" );
    }
    else if (Country == enCountry::Morocco) {
        return ( "Morocco" );
    }
    else if (Country == enCountry::Algeria) {
        return ( "Algeria" );
    }
    else {
        return ( "Invalid County" );
    }

}

//-------------------------------------//
//-------------------------------------//

void ReadNumbers(float &number1, float &number2) {

    cout << " -> Please enter the first number: ";
    cin >> number1;
    cout << " -> Please enter the second number: ";
    cin >> number2;

}

void ChooseOperation(short int &Operation) {

    cout << " -> Please choose the operation to perform: " << endl;
    cout << "  - [1] -> [+]" << endl;
    cout << "  - [2] -> [-]" << endl;
    cout << "  - [3] -> [*]" << endl;
    cout << "  - [4] -> [/]" << endl;

    cout << " -> Your choice number ? ";
    cin >> Operation;

}

float GetResult(float number1, float number2, short int Operation) {

    enOperations operationChoice = (enOperations) Operation;

    switch (operationChoice) {

    case (enOperations::Sum):
        return (number1 + number2);

    case (enOperations::Subtraction):
        return (number1 - number2);

    case (enOperations::Multiplication):
        return (number1 * number2);

    case (enOperations::Division):
        return (number1 / number2);

    default:
        cout << "\n× Invalid operation number [ " << Operation << " ]" << endl;
        return 1;

    }

}



//-------------------------------------//
//-------------------------------------//


void ReadDayNumber(short int &dayNumber) {

    cout << "--> Please enter the day number (1 - 7): " << endl;
    cin >> dayNumber;

}

string GetDay(short int dayNumber) {

    enDays Day = (enDays) dayNumber;

    switch (Day) {

    case(enDays::Monday):
        return ( "Monday" );

    case(enDays::Thesday):
        return ( "Thesday" );

    case(enDays::Wednesday):
        return ( "Wednesday" );

    case(enDays::Thursday):
        return ( "Thursday" );

    case(enDays::Friday):
        return ( "Friday" );

    case(enDays::Saturday):
        return ( "Saturday" );

    case(enDays::Sunday):
        return ( "Sunday" );

    default:
        return ( "× Invalid Day" );

    }

}

int main() {

	//--> Lesson #46
    /*
    short int countryChoice;

        cout << "|~~~~~~~~~~~~~ Choose Country ~~~~~~~~~~~~|" << endl;
        cout << "\n===========================================\n" << endl;
    	ReadCountryNumber(countryChoice);

        cout << "\n|-------------------------------------|\n" << endl;
        cout << " -> Your country is: [ " << GetCountry(countryChoice) << " ]" << endl;
        cout << "\n|-------------------------------------|\n" << endl;

        cout << "\n===========================================\n" << endl;




    //--> Problem #36 Simple Calculator
    float number1, number2;
    short int Operation;

    cout << "|~~~~~~~~~~~ Simple Calculator ~~~~~~~~~~~|" << endl;
    cout << "\n===========================================\n" << endl;
    ReadNumbers(number1, number2);
    ChooseOperation(Operation);
    cout << "\n|-------------------------------------|\n" << endl;
    cout << " -> The result is: [ " << GetResult(number1, number2, Operation) << " ]" << endl;
    cout << "\n|-------------------------------------|\n" << endl;
    cout << "\n===========================================\n" << endl;

    */

    //--> Problem #43 Days Of Week

    short int dayNumber;

    cout << "|~~~~~~~~~~~~~ Days Of Week ~~~~~~~~~~~~~|" << endl;
    cout << "\n===========================================\n" << endl;
    ReadDayNumber(dayNumber);
    cout << "\n|-------------------------------------|\n" << endl;
    cout << " -> Day N.[" << dayNumber << "]: [ " << GetDay(dayNumber) << " ]" << endl;
    cout << "\n|-------------------------------------|\n" << endl;
    cout << "\n===========================================\n" << endl;



    return 0;
}

    