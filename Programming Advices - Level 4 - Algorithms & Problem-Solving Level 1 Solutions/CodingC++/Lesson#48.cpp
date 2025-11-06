#include<iostream>
using namespace std;

struct stPerson {
    string firstName;
    string lastName;
    unsigned short int Age;
    string Phone;
};


//------------------------------//
//------------------------------//


void readPersonInfos(stPerson &Person) {

    cout << "\n===========================================\n" << endl;
    cout << "--> Please enter the first name: ";
    cin >> Person.firstName;
    cout << "--> Please enter the last name: ";
    cin >> Person.lastName;
    cout << "--> Please enter the age: ";
    cin >> Person.Age;
    cout << "--> Please enter the phone number: ";
    cin >> Person.Phone;
    cout << "\n===========================================\n" << endl;

}

void displayPersonInfos(stPerson Person) {

    cout << "\n===========================================\n" << endl;
    cout << "  - Fist Name : [ " << Person.firstName << " ]" << endl;
    cout << "  - Last Name : [ " << Person.lastName  << " ]" << endl;
    cout << "  - Age       : [ " << Person.Age	   << " ]" << endl;
    cout << "  - Phone     : [ " << Person.Phone 	<< " ]" << endl;
    cout << "\n===========================================\n" << endl;

}


void readAllPersonsInfos(stPerson Persons[100], unsigned short int &numberOfPersons) {

    cout << "--> Please enter the number of persons: ";
    cin >> numberOfPersons;

	cout << "\n\n-------+ Get Persons Informations +-------" << endl;
    for(int i = 0; i < numberOfPersons; ++i) {
        cout <<"\n\n             [ Person N.[" << i + 1 << "] ]" << endl;
        readPersonInfos(Persons[i]);
    }

}

void displayAllPersonsInfos(stPerson Persons[100], unsigned short int numberOfPersons) {

	cout << "\n\n-----+ Display Persons Informations +-----" << endl;
    for(int i = 0; i < numberOfPersons; ++i) {
        cout << "\n             [ Person N.[" << i + 1 << "] ]" << endl;
        displayPersonInfos(Persons[i]);
        cout << endl;
    }

}


//------------------------------//
//------------------------------//


int main()
{

    ////--> Lesson #48 Array & structures & Functions
    stPerson Persons[100];
    unsigned short int numberOfPersons;

    readAllPersonsInfos(Persons, numberOfPersons);
    for(int i = 0; i < 3; ++i) {
        cout << "\n\n×××××××××××××××××××××××××××××××××××××××××××\n" << endl;
    }
    displayAllPersonsInfos(Persons, numberOfPersons);

    return 0;
}

    