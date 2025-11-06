#pragma once

#include <iostream>

using namespace std;


class Person {
private:
	short _ID;
	string _firstName;
	string _lastName;
	string _email;
	string _phoneNumber;

public:

	Person(short ID, string firstName, string lastName, string email, string phoneNumber) {
		cout << " -> Parameterized constructor of Person has been called !" << endl;
		_ID = ID;
		_firstName = firstName;
		_lastName = lastName;
		_email = email;
		_phoneNumber = phoneNumber;
	}

	Person() {}
	short getId() { return _ID; }

	string getFirstName() { return _firstName; }

	void setFirstName(string firstName) {
		_firstName = firstName;
	}


	string getLastName() { return _lastName; }

	void setLastName(string lastName) {
		_lastName = lastName;
	}


	string getFullName() { return (_firstName + " " + _lastName); }


	string getPhoneNumber() { return _phoneNumber; }

	void setPhoneNumber(string phoneNumber) {
		_phoneNumber = phoneNumber;
	}


	string getEmail() { return _email; }

	void setEmail(string email) {
		_email = email;
	}


	void print() {
		cout << "-------- Person Details --------" << endl;
		cout << "--------------------------------" << endl;
		cout << " ID         : " << _ID << endl;
		cout << " First name : " << _firstName << endl;
		cout << " Last name  : " << _lastName << endl;
		cout << " Full name  : " << getFullName() << endl;
		cout << " Email      : " << _email << endl;
		cout << " Phone      : " << _phoneNumber << endl;
		cout << "--------------------------------" << endl;

	}

protected:

	void sendEmail(string  subject, string body) {
		cout << " - The following email sent successfuly the to [" << getEmail() << "]" << endl;
		cout << " - [Subject] : [" << subject << "] -" << endl;
		cout << " - [Body]    : [" << body << "] -" << endl;
	}

	void sendSMS(string body) {
		cout << " - The following email sent successfuly the to [" << getPhoneNumber() << "]" << endl;
		cout << " - [Subject] : [" << body << "] -" << endl;
	}

	//friend Test;
};


