#pragma once
#include <iostream>

using namespace std;

class Person {
private:
	int _id;
	string _firstName;
	string _lastName;
	string _email;
	string _phone;

public:
	Person(int id, string firstName, string lastName, string email, string phone) {
		this->_id = id;
		this->_firstName = firstName;
		this->_lastName = lastName;
		this->_email = email;
		this->_phone = phone;
	}

	// Read Only Property
	int getId() {
		return _id;
	}

	// Property Set
	void setFirstName(string firstName) {
		_firstName = firstName;
	}

	// Property Get
	string getFirstName() {
		return _firstName;
	}

	// Property Set
	void setLastName(string lastName) {
		_lastName = lastName;
	}

	// Property Get
	string getLastName() {
		return _lastName;
	}

	// Property Set
	void setEmail(string email) {
		_email = email;
	}

	// Property Get
	string getEmail() {
		return _email;
	}

	// Property Set
	void setPhone(string phone) {
		_phone = phone;
	}

	// Property Get
	string getPhone() {
		return _phone;
	}

	string getFullName() {
		return _firstName + " " + _lastName;
	}

	void print() {
		cout << "\n-> Info:";
		cout << "\n___________________";
		cout << "\n  ID       : " << _id;
		cout << "\n  FirstName: " << _firstName;
		cout << "\n  LastName : " << _lastName;
		cout << "\n  Full Name: " << getFullName();
		cout << "\n  Email    : " << _email;
		cout << "\n  Phone    : " << _phone;
		cout << "\n___________________\n";
	}

	void sendEmail(string subject, string body) {
		cout << "\n The following message sent successfully to email: " << _email;
		cout << "\n Subject: " << subject;
		cout << "\n Body: " << body << endl;
	}

	void sendSms(string textMessage) {
		cout << "\n The following SMS sent successfully to phone: " << _phone;
		cout << "\n" << textMessage << endl;
	}
};