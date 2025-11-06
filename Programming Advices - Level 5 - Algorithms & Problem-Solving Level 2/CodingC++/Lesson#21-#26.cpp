#include<iostream>
using namespace std;

int main()
{
    // --> Level 3 #21 - #26
    /*
        string Name = "Mohammed Abu-Hadhoud", City = "Amman", Country = "Jordan";
        unsigned short int Age = 44;
        float MonthlySalary = 5000, YearlySalary = MonthlySalary * 12;
        char Gender = 'M';
        bool Married = true;

        cout << "************************************** " << endl;
        cout << "- Name           : " << Name << endl;
        cout << "- Age            : " << Age << endl;
        cout << "- Coutry         : " << Country << endl;
        cout << "- City           : " << City << endl;
        cout << "- Monthly Salary : " << MonthlySalary << endl;
        cout << "- Yearly Salary  : " << YearlySalary << endl;
        cout << "- Gender         : " << Gender << endl;
        cout << "- Married        : " << Married << endl;
        cout << "************************************** \n\n";


    	cout << " \n ======================================== \n" << endl;


        int number1 = 10, number2 = 20, number3 = 30;
        cout << number1 << " +" << endl;
        cout << number2 << " +" << endl;
        cout << number3 << endl;
        cout << "__________________________\n" << endl;
        cout << "Totale = " << number1 + number2 + number3 << endl << endl;


        cout << " \n ======================================== \n" << endl;


        unsigned short int CurrentAge = 25;
        cout << "- After 5 years you will be " << CurrentAge + 5 << " years old \n\n";
    */


    cout << " \n ======================================== \n" << endl;


    /*
        string userName, userCourty, userCity;
        unsigned short int userAge;
        float MonthlySalary;
        char Gender;
        bool isMarried;

        cout << "--> Hi user please enter: \n";
        cout << "- Your name: ";
        cin >> userName;
        cout << "- Age: ";
        cin >> userAge;
        cout << "- Country: ";
        cin >> userCourty;
        cout << "- City: ";
        cin >> userCity;
        cout << "- Monthly Salary: ";
        cin >> MonthlySalary;
        cout << "- Gender: (M / F): ";
        cin >> Gender;
        cout << "- Are married ? ( Yes ==> 1 / No ==> 0):  ";
        cin >> isMarried;

        cout << "************************************** " << endl;
        cout << "- Name           : " << userName << endl;
        cout << "- Age            : " << userAge << endl;
        cout << "- Coutry         : " << userCourty << endl;
        cout << "- City           : " << userCity << endl;
        cout << "- Monthly Salary : " << MonthlySalary << endl;
        cout << "- Yearly Salary  : " << MonthlySalary * 12 << endl;
        cout << "- Gender         : " << Gender << endl;
        cout << "- Married        : " << isMarried << endl;
        cout << "************************************** \n\n";



        cout << " \n ======================================== \n" << endl;


        int number1, number2, number3;

        cout << "--> Enter: \n";
        cout << "- Number1: ";
        cin >> number1;
        cout << "- Number2: ";
        cin >> number2;
        cout << "- Number3: ";
        cin >> number3;

        cout << number1 << " +" << endl;
        cout << number2 << " +" << endl;
        cout << number3 << endl;
        cout << "__________________________\n" << endl;
        cout << "Totale = " << number1 + number2 + number3 << endl << endl;


        cout << " \n ======================================== \n" << endl;


        unsigned short int userAge;
        cout << "--> Please enter your age: ";
        cin >> userAge;
        cout << "  - After 5 years you will be " << userAge + 5 << " years old. \n\n"; q
    */

    int number1, number2;

    cout << "-- Please enter the first number: ";
    cin >> number1;
    cout << "-- Please enter the second number: ";
    cin >> number2;

    cout << "> " << number1 <<  " + " << number2 << " = " << number1 + number2 << endl;
    cout << "> " << number1 <<  " - " << number2 << " = " << number1 - number2 << endl;
    cout << "> " << number1 <<  " * " << number2 << " = " << number1 * number2 << endl;
    cout << "> " << number1 <<  " / " << number2 << " = " << number1 / number2 << endl;
	cout << "> " << number1 <<  " % " << number2 << " = " << number1 % number2 << endl;

    return 0;
}