// Problem #45 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>

#include "../MyInputLibrary.h";

using namespace std;
using namespace input;

struct Client
{
	string accountNumber;
	short pinCode;
	string name;
	string phone;
	float accountBalance;
};

Client readClientData(string messageToDisplay = "") {
	Client client;

	cout << messageToDisplay;

	cout << " -> Please enter account number: ";
	cin >> client.accountNumber;
	cout << " -> Please enter pin code: ";
	cin >> client.pinCode;
	cout << " -> Please enter client name: ";
	cin.ignore(1);
	getline(cin, client.name);
	cout << " -> Please enter client phone: ";
	cin >> client.phone;
	cout << " -> Please enter account balance: ";
	cin >> client.accountBalance;

	return client;
}


string getClientRecord(Client client, string delimiter = "#--#") {
	return client.accountNumber
		+ (delimiter + to_string(client.pinCode))
		+ (delimiter + client.name)
		+ (delimiter + client.phone)
		+ (delimiter + to_string(client.accountBalance));
}

int main()
{
	Client jy;
	jy = readClientData();
	
	cout << "--> client record for saving is:" << endl;
	cout << getClientRecord(jy, "@--@") << endl;


	
}

