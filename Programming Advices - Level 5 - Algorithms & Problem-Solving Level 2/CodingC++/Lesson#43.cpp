#include<iostream>
using namespace std;



//------------------------------------// // #33 Grades

void ReadGrade(float &Grade) {

    cout << "--> Please enter your grade: ";
    cin >> Grade;

}

char GradeImplementation(float Grade) {

    if (Grade > 89) {
        return ( 'A' );
    }
    else if (Grade > 79) {
        return ( 'B' );
    }
    else if (Grade > 69) {
        return ( 'C' );
    }
    else if (Grade > 59) {
        return ( 'D' );
    }
    else if (Grade > 49) {
        return ( 'E' );
    }
    else {
        return ( 'F' );
    }
    0جظ.

}

//------------------------------------// // #34 Total sales

void ReadTotalSales(unsigned int &TotalSales) {

    cout << "--> Please enter the total sales: ";
    cin >> TotalSales;

}


float GetTotalCommission(unsigned int TotalSales) {

    if (TotalSales >= 1000000) {
        return (TotalSales * 0.01);
    }
    else if (TotalSales >= 500000) {
        return (TotalSales * 0.02);
    }
    else if (TotalSales >= 100000) {
        return (TotalSales * 0.03);
    }
    else if (TotalSales >= 50000) {
        return (TotalSales * 0.05);
    }
    else {
        return 0;
    }

}

//------------------------------------// // #36 Simple Calculator

void ReadNumbers(float &number1, float &number2) {

    cout << "--> Please enter the first number: ";
    cin >> number1;
    cout << "--> Please enter the second number: ";
    cin >> number2;

}

void ChoseOperation(char &Operation) {

    cout << "\n -> Please chose the operation to perform:" << endl;
    cout << "  ( [ + ] | [ - ] | [ * ] | [ / ] )" << endl << endl;
    cout << "   ";
    cin >> Operation;

}

float GetResult(float number1, float number2, char Operation) {

    if (Operation == '+') {
        return (number1 + number2);
    }
    else if (Operation == '-') {
        return (number1 - number2);
    }
    else if (Operation == '*') {
        return (number1 * number2);
    }
    else if (Operation == '/') {
        return (number1 / number2);
    }
    else {
        cout << "\n  × Wrong Operation [ " << Operation << " ]" << endl;
        return 1;
    }

}

//------------------------------------// // #44 Days Of Week

void ReadDayNumber(short int &dayNumber) {

    cout << "--> Please enter the day number (1 - 7): " << endl;
    cin >> dayNumber;

}

string CheckDayNumber(short int dayNumber) {

    if (dayNumber < 1 || dayNumber > 7) {
        return ( "× Wrong Day Number" );
    }
    else if (dayNumber == 1 ) {
        return ( "Monday" );
    }
    else if (dayNumber == 2 ) {
        return ( "Tuesday" );
    }
    else if (dayNumber == 3 ) {
        return ( "Wednesday" );
    }
    else if (dayNumber == 4 ) {
        return ( "Thursday" );
    }
    else if (dayNumber == 5 ) {
        return ( "Friday" );
    }
    else if (dayNumber == 6 ) {
        return ( "Saturday" );
    }
    else {
        return ( "Sunday" );
    }

}

//------------------------------------// // #45 Year Months

void ReadMonthNumber(short int &monthNumber) {

    cout << "--> Please enter the month number (1 - 12): " << endl;
    cin >> monthNumber;

}

string CheckMonthNumber(short int monthNumber) {

    if (monthNumber < 1 || monthNumber > 12) {
        return ( "× Wrong Month Number" );
    }
    else if (monthNumber == 1 ) {
        return ( "January" );
    }
    else if (monthNumber == 2 ) {
        return ( "February" );
    }
    else if (monthNumber == 3 ) {
        return ( "March" );
    }
    else if (monthNumber == 4 ) {
        return ( "April" );
    }
    else if (monthNumber == 5 ) {
        return ( "Mai" );
    }
    else if (monthNumber == 6 ) {
        return ( "June" );
    }
    else if (monthNumber == 7 ) {
        return ( "July" );
    }
    else if (monthNumber == 8 ) {
        return ( "August" );
    }
    else if (monthNumber == 9 ) {
        return ( "September" );
    }
    else if (monthNumber == 10 ) {
        return ( "October" );
    }
    else if (monthNumber == 11 ) {
        return ( "November" );
    }
    else {
        return ( "December" );
    }

}


int main()
{

    ////--> Lesson #43
    /*
        //--> Problem #33 Grades [ A B C D E F]
        float Grade;

        cout << "->>>>>>>>>>>>>>> Get Grade <<<<<<<<<<<<<<<-" << endl;
        cout << "\n===========================================\n" << endl;
        ReadGrade(Grade);
        cout << "\n|----------------------|\n" << endl;
        cout << " -> Your grade is: [ " << GradeImplementation(Grade) << " ]"  << endl;
        cout << "\n|----------------------|\n" << endl;
        cout << "\n===========================================\n" << endl;

    //------------------------------------------------------------------------------//

        //--> Problem #34 Commission percentage
        unsigned int TotalSales;

        cout << "->>>>>>>>>>>>>>> Get Grade <<<<<<<<<<<<<<<-" << endl;
        cout << "\n===========================================\n" << endl;
        ReadTotalSales(TotalSales);
        cout << "\n|-------------------------------------|\n" << endl;
        cout << " -> The total commission is: [ " << GetTotalCommission(TotalSales) << " ]"  << endl;
        cout << "\n|-------------------------------------|\n" << endl;
        cout << "\n===========================================\n" << endl;

    //------------------------------------------------------------------------------//

        //--> Problem #36 Simple Calculator

        float number1, number2;
        char Operation;

        cout << "->>>>>>>>>>> Simple Calculator <<<<<<<<<<<-" << endl;
        cout << "\n===========================================\n" << endl;

        ReadNumbers(number1, number2);
        ChoseOperation(Operation);

        cout << "\n|-------------------------------------|\n" << endl;
        cout << " -> The Result is: [ " << GetResult(number1, number2, Operation) << " ]" << endl;
        cout << "\n|-------------------------------------|\n" << endl;
        cout << "\n===========================================\n" << endl;

    //------------------------------------------------------------------------------//

        //--> Problem #44 Days of week

        short int dayNumber;

        cout << "->>>>>>>>> Days Of The Week <<<<<<<<<<-" << endl;
        cout << "\n===========================================\n" << endl;
        ReadDayNumber(dayNumber);

        cout << "\n|-----------------------------------------|\n" << endl;
        cout << " -> Day N.[ " << dayNumber << " ] : [ " << CheckDayNumber(dayNumber) << " ]" << endl;
        cout << "\n|-----------------------------------------|\n" << endl;
        cout << "\n===========================================\n" << endl;


    //------------------------------------------------------------------------------//

        //--> Problem #45 Year months

        short int monthNumber;

        cout << "->>>>>>>>>>>>> Year Months <<<<<<<<<<<<<<<-" << endl;
        cout << "\n===========================================\n" << endl;
        ReadMonthNumber(monthNumber);

        cout << "\n|-----------------------------------------|\n" << endl;
        cout << " -> Day N.[ " << monthNumber << " ] : [ " << CheckMonthNumber(monthNumber) << " ]" << endl;
        cout << "\n|-----------------------------------------|\n" << endl;
        cout << "\n===========================================\n" << endl;
    */

    return 0;
}

