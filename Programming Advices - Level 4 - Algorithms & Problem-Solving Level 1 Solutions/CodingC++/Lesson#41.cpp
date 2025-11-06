#include<iostream>
using namespace std;

struct stPerson {
    string firstName;
    string lastName;
    unsigned short int Age;
    string Phone;
};

void ReadPersonInfos(stPerson &Person) {

    cout << "+>>>>>>>> Get Person Informations <<<<<<<<+" << endl;
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

void DisplayPersonInfos(stPerson Person) {

    cout << "+>>>>>> Display Person Informations <<<<<<+" << endl;
    cout << "\n===========================================\n" << endl;
    cout << "  - Fist Name : [ " << Person.firstName << " ]" << endl;
    cout << "  - Last Name : [ " << Person.lastName  << " ]" << endl;
    cout << "  - Age       : [ " << Person.Age	   << " ]" << endl;
    cout << "  - Phone     : [ " << Person.Phone 	<< " ]" << endl;
    cout << "\n===========================================\n" << endl;
}


void ReadAllPersonsInfos(stPerson Persons[2]) {

    ReadPersonInfos(Persons[0]);
    cout << endl;
    ReadPersonInfos(Persons[1]);

}

void DisplayAllPersonsInfos(stPerson Persons[2]) {

    DisplayPersonInfos(Persons[0]);
    cout << endl;
    DisplayPersonInfos(Persons[1]);
}



int main()
{
	////--> Lesson #41
    stPerson Persons[2];

    ReadAllPersonsInfos(Persons);
    cout << "\n\n×××××××××××××××××××××××××××××××××××××××××××\n\n" << endl;
    DisplayAllPersonsInfos(Persons);

    return 0;
}

