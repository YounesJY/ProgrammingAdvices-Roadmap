// Lesson #1 - #8.cpp : This file contains the 'main' function. Program execution begins and ends there.
//
#include <iostream>

using namespace std;


class Client {
private:
	string _accountNumber;
	string _pinCode;
	string _fullName;
	string _phoneNumber;
	float _accountBalance;

public:
	static short numberOfClients;


	Client(string accountNumber, string pinCode, string fullName, string phoneNumber, float accountBalance) {
		_accountNumber = accountNumber;
		_pinCode = pinCode;
		_fullName = fullName;
		_phoneNumber = phoneNumber;
		_accountBalance = accountBalance;
	}

	Client() {
		++numberOfClients;
		cout << "Object has bee constructed !" << endl;
		::Client("Empty", "Empty", "Empty", "Empty", 0);
	}

	Client(Client& client) {
		_accountNumber = client._accountNumber;
		_pinCode = client._pinCode;
		_fullName = client._fullName;
		_phoneNumber = client._phoneNumber;
		_accountBalance = client._accountBalance;
	}
	string getAccountNumber() { return _accountNumber; }

	void setAccountNumber(string accountNumber) {
		_accountNumber = accountNumber;
	}

	_declspec(property(get = getAccountNumber, put = setAccountNumber)) string accountNumber;


	string getPinCode() { return _pinCode; }

	void setPinCode(string pinCode) {
		_pinCode = pinCode;
	}

	_declspec(property(get = getPinCode, put = setPinCode)) string pinCode;


	string getFullName() { return _fullName; }

	void setFullName(string fullName) {
		_fullName = fullName;
	}

	_declspec(property(get = getFullName, put = setFullName)) string fullName;


	string getPhoneNumber() { return _phoneNumber; }

	void setPhoneNumber(string phoneNumber) {
		_phoneNumber = phoneNumber;
	}

	_declspec(property(get = getPhoneNumber, put = setPhoneNumber)) string phoneNumber;


	float getAccountBalance() { return _accountBalance; }

	void setAccountBalance(float accountBalance) {
		_accountBalance = accountBalance;
	}

	_declspec(property(get = getAccountBalance, put = setAccountBalance)) float accountBalance;

	void printClientDataCard() {
		cout << "-------- Cient Details --------" << endl;
		cout << "-------------------------------" << endl;
		cout << " Account number  : " << accountNumber << endl;
		cout << " Pin code        : " << pinCode << endl;
		cout << " Client name     : " << fullName << endl;
		cout << " Phone number    : " << phoneNumber << endl;
		cout << " Account balance : " << accountBalance << endl;
		cout << "-------------------------------" << endl;

	}


	~Client() {
		cout << "Object has been destroyed !" << endl;
	}

};


short Client::numberOfClients = 0;

int main()
{
	Client client;
	cout << Client::numberOfClients << endl;
	cout << ++client.numberOfClients << endl;

}

