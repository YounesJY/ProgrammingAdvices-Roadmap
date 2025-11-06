#include<iostream>
#include<string.h>

using namespace std;

struct Address {
    string Country;
    string City;
};

struct Salary {
    float monthlySalary;
    float yearlySalary;
};

struct User {
    string fullName;
    unsigned short int Age;
    string phoneNumber;
    Address userAddress;
    Salary userSalary;
    char Gender;
    bool isMarried;
};

enum enGender {Male, Female};
enum enMaritalStatus {Single, Married};

int main()
{

    // --> Video #27 - 28 - 29 - 30 Structures Enums Typecasting Strings
/*
    User newUser;

    cout << "-----------+ User Informations +----------" << endl;

    cout << "\n$=========================================$\n" << endl;
    cout << "--> Please enter:" << endl;

    cout << "  - Your full name: ";
    getline(cin, newUser.fullName);

    cout << "  - Your age: ";
    cin >> newUser.Age;

    cout << "  - Phone number: ";
    cin >> newUser.phoneNumber;

    cout << "  - Country: ";
    cin >> newUser.userAddress.Country;
    cout << "  - City: ";
    cin >> newUser.userAddress.City;

    cout << "  - Monthly salary: ";
    cin >> newUser.userSalary.monthlySalary;
    newUser.userSalary.yearlySalary = newUser.userSalary.monthlySalary * 12;

    cout << "  - Gender ( M / F): ";
    cin >> newUser.Gender;

    cout << "  - Married ? ( Yes -> 1 / No -> 0): ";
    cin >> newUser.isMarried;


    cout << "\n$**********************$\n" << endl;
    cout << " => User Name     : " << newUser.fullName 				<< endl;
    cout << " => Age           : " << newUser.Age 			 		<< endl;
    cout << " => Phone Number  : " << newUser.phoneNumber 			 << endl;
    cout << " => Country       : " << newUser.userAddress.Country  	<< endl;
    cout << " => City          : " << newUser.userAddress.City		 << endl;
    cout << " => Monthly Salary: " << newUser.userSalary.monthlySalary << endl;
    cout << " => Yearly Salary : " << newUser.userSalary.yearlySalary  << endl;
    cout << " => Gender        : " << newUser.Gender 			      << endl;
    cout << " => Married       : " << newUser.isMarried 			   << endl;
    cout << "\n$**********************$\n" << endl;
    cout << "\n$=========================================$\n" << endl;
    
    //-------------------------------------------------------------------------//
 
	int number1 = 20, num;
    float number2 = 55.23;
    double number3 = 33.5;
    string string1 = "43.22";
     
	cout << "-----------+ Type Covertion +----------" << endl;   
    
	cout << "\n$=========================================$\n" << endl;
    cout << "--> String  \"" << string1 << "\" to:" << endl;
    cout << "  - Double : " << stod(string1) << endl;
    cout << "  - Float  : " << stof(string1) << endl;
    cout << "  - Integer: " << stoi(string1) << endl;
    
    cout << "\n$**********************$\n" << endl;
    
    cout << "--> Integer [ " << number1 << " ] to string [ " << to_string(number1) << " ]" << endl;
    
    cout << "\n$**********************$\n" << endl;
    
    cout << "--> Double  [ " << number3 << " ] to string [ " << to_string(number3) << " ]" << endl;
    
    cout << "\n$**********************$\n" << endl;
    
    cout << "--> Float   [ " << number2 << " ] to string [ " << to_string(number2) << " ]" << endl;
    cout << "--> Float   [ " << number2 << " ] to:" << endl;
    
    num = number2;
    cout << "  - Integer (Implicit Conversion)   : " << num << endl;
    
    num = (int) number2;
    cout << "  - Integer (explicit C Typecasting): " << num << endl;
    
    num = int(number2);
    cout << "  - Integer (explicit with int())   : " << num << endl;
    cout << "\n$=========================================$\n" << endl;

    //-------------------------------------------------------------------------//
  
	string string1, string2, string3;
 
	cout << "\n$=========================================$\n" << endl;
	cout << "--> Please enter String1: ";
    getline(cin, string1);
    cout << "--> Please enter String2 (as number): ";
    getline(cin, string2);
    cout << "--> Please enter String3 (as number):";
    getline(cin, string3);
    
    cout << "\n$**********************$\n" << endl;
    cout << " => The lengths of \"" << string1 << "\" is: [ " << string1.length() << " ]" << endl << endl;
    cout << " => Characters at position [ 0, 2 ,4 ,7 ] are [ " << string1[0] << ", " << string1[2] << ", " << string1[4] << ", " << string1[7] << " ]" << endl << endl;
    cout << " => Concatenating String2 and String2 = " << string2 + string3 << endl << endl;
    cout << " => [ " << string2 << " * " << string3 << " ] = " << stoi(string2) * stoi(string3) << endl;
    cout << "\n$**********************$\n" << endl;

    cout << "\n$=========================================$\n" << endl;
 

*/    	  
    
    
    return 0;
}
    