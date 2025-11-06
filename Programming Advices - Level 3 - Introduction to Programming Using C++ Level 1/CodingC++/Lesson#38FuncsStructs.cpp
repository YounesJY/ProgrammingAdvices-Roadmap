#include<iostream>
using namespace std;

struct stAddress {
    string Country;
    string City;
};

struct stSalary {
    float monthlySalary;
    float yearlySalary;
};

struct stPerson {
    string fullName;
    int Age;
    string phoneNumber;
    stAddress Address;
    stSalary Salary;
    char Gender;
    bool isMarried;
};

void GetUserInfo(stPerson &User) {

    cout << "---------+ Get User Informations +---------" << endl;
    cout << "\n===========================================\n" << endl;
    cout << "--> Please enter your full name: ";
    getline(cin, User.fullName);

    cout << "--> Please enter your age: ";
    cin >> User.Age;

    cout << "--> Please enter your phone number: ";
    cin >> User.phoneNumber;

    cout << "--> Please enter your country: ";
    cin >> User.Address.Country;
    cout << "--> Please enter your city: ";
    cin >> User.Address.City;

    cout << "--> Please enter your monthly salary: ";
    cin >> User.Salary.monthlySalary;
    User.Salary.yearlySalary = User.Salary.monthlySalary * 12;

    cout << "--> Please enter your gender (M / F): ";
    cin >> User.Gender;

    cout << "--> Are you married ? (Yes => 1 / No => 0): ";
    cin >> User.isMarried;

    cout << "\n===========================================\n" << endl;

}

void PrintUserInfo(stPerson User) {

    cout << "-------+ Display User Informations +-------" << endl;
    cout << "\n===========================================\n" << endl;
    cout << " +> User Name      : [ " << User.fullName 	         << " ]" << endl;
    cout << " +> Age            : [ " << User.Age 	 	         << " ]" << endl;
    cout << " +> Phone Number   : [ " << User.phoneNumber       	<< " ]" << endl;
    cout << " +> Country        : [ " << User.Address.Country       << " ]" << endl;
    cout << " +> City           : [ " << User.Address.City          << " ]" << endl;
    cout << " +> Monthly Salary : [ " << User.Salary.monthlySalary  << " ]" << endl;
    cout << " +> Yearly Salary  : [ " << User.Salary.yearlySalary   << " ]" << endl;
    cout << " +> Gender         : [ " << User.Gender                << " ]" << endl;
    cout << " +> Is Married     : [ " << User.isMarried             << " ]" << endl;
    cout << "\n===========================================\n" << endl;

}

int main()
{
    //// --> Lesson #38 Functions and structures
    stPerson User1;
    GetUserInfo(User1);
    PrintUserInfo(User1);
    return 0;
}

