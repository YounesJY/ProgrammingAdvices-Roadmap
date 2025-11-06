#include<iostream>
using namespace std;

enum enCountry {Egypt = 1, Morocco = 2, Algeria = 3};
enum enOperations { Sum = 1, Subtraction = 2, Multiplication  = 3, Division = 4};
enum enDays { Monday = 1, Thesday = 2, Wednesday = 3, Thursday = 4, Friday = 5, Saturday = 6, Sunday = 7};
enum enMonth { January = 1, February = 2, March = 3, April = 4, Mai = 5, June = 6, July = 7, August = 8, September = 9, October = 10, November = 11, December = 12};


//-------------------------------------//
//-------------------------------------//

void readCountryNumber(short int &countryChoice) {

    cout << "--> Please enter the number of your Country:" << endl;
    cout << "  - [1] Egypt"   << endl;
    cout << "  - [2] Morocco" << endl;
    cout << "  - [3] Algeria" << endl;
    cout << "  > Your choice ? ";
    cin >> countryChoice;

}

string getCountry(short int countryChoice) {

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

void readNumbers(float &number1, float &number2) {

    cout << " -> Please enter the first number: ";
    cin >> number1;
    cout << " -> Please enter the second number: ";
    cin >> number2;

}

void chooseOperation(short int &Operation) {

    cout << " -> Please choose the operation to perform: " << endl;
    cout << "  - [1] -> [+]" << endl;
    cout << "  - [2] -> [-]" << endl;
    cout << "  - [3] -> [*]" << endl;
    cout << "  - [4] -> [/]" << endl;

    cout << " -> Your choice number ? ";
    cin >> Operation;

}

float getResult(float number1, float number2, short int Operation) {

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


void readDayNumber(short int &dayNumber) {

    cout << "--> Please enter the day number (1 - 7): " << endl;
    cin >> dayNumber;

}

string getDay(short int dayNumber) {

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

//-------------------------------------//
//-------------------------------------//


void readMonthNumber(unsigned short int &monthNumber) {

    cout << "--> Please enter the month number (1 - 12): " << endl;
    cin >> monthNumber;

}

string getMonth(unsigned short int monthNumber) {

    enMonth Month = (enMonth) monthNumber;

    switch (Month) {

    case(enMonth::January):
        return ( "January" );

    case(enMonth::February):
        return ( "February" );

    case(enMonth::March):
        return ( "March" );

    case(enMonth::April):
        return ( "April" );

    case(enMonth::Mai):
        return ( "Mai" );

    case(enMonth::June):
        return ( "June" );

    case(enMonth::July):
        return ( "July" );

    case(enMonth::August):
        return ( "August" );

    case(enMonth::September):
        return ( "September" );

    case(enMonth::October):
        return ( "October" );

    case(enMonth::November):
        return ( "November" );

    case(enMonth::December):
        return ( "December" );

    default:
        return ( "× Invalid Month" );
    }

}

void printMonth(unsigned short int monthNumber, string Month) {

    cout << "\n|-------------------------------------|\n" << endl;
    cout << " -> Month N.[" << monthNumber << "]: [ " << Month << " ]" << endl;
    cout << "\n|-------------------------------------|\n" << endl;

}


int main() {

    //--> Lesson #46
    /*
    //------------------------------------------------------------------------------//

        //--> Country example
        short int countryChoice;

        cout << "|~~~~~~~~~~~~~ Choose Country ~~~~~~~~~~~~|" << endl;
        cout << "\n===========================================\n" << endl;
        readCountryNumber(countryChoice);

        cout << "\n|-------------------------------------|\n" << endl;
        cout << " -> Your country is: [ " << getCountry(countryChoice) << " ]" << endl;
        cout << "\n|-------------------------------------|\n" << endl;

        cout << "\n===========================================\n" << endl;


    //------------------------------------------------------------------------------//

        //--> Problem #36 Simple Calculator
        float number1, number2;
        short int Operation;

        cout << "|~~~~~~~~~~~ Simple Calculator ~~~~~~~~~~~|" << endl;
        cout << "\n===========================================\n" << endl;
        readNumbers(number1, number2);
        chooseOperation(Operation);
        cout << "\n|-------------------------------------|\n" << endl;
        cout << " -> The result is: [ " << getResult(number1, number2, Operation) << " ]" << endl;
        cout << "\n|-------------------------------------|\n" << endl;
        cout << "\n===========================================\n" << endl;


    //------------------------------------------------------------------------------//

        //--> Problem #43 Days Of Week
        short int dayNumber;

        cout << "|~~~~~~~~~~~~~ Days Of Week ~~~~~~~~~~~~~|" << endl;
        cout << "\n===========================================\n" << endl;
        readDayNumber(dayNumber);
        cout << "\n|-------------------------------------|\n" << endl;
        cout << " -> Day N.[" << dayNumber << "]: [ " << getDay(dayNumber) << " ]" << endl;
        cout << "\n|-------------------------------------|\n" << endl;
        cout << "\n===========================================\n" << endl;


    //------------------------------------------------------------------------------//

        //--> Problem #45 Year Months
        unsigned short int monthNumber;

        cout << "\n===========================================\n" << endl;
        readMonthNumber(monthNumber);
        printMonth(monthNumber, getMonth(monthNumber));
        cout << "\n===========================================\n" << endl;
    //------------------------------------------------------------------------------//
    */
    return 0;
}

