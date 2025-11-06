// Problem #47 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.
#include <iostream>
#include <fstream>
#include <vector>
#include <string>
#include <iomanip>

using namespace std;
enum choice { yes, no };
struct Client
{
	string accountNumber;
	short pinCode;
	string name;
	string phone;
	float accountBalance;
};

string stringToLower(string text) {
	for (int i = 0; i < text.length(); i++)
		text[i] = tolower(text[i]);

	return text;
}

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


string getClientRecord(Client client, string delimiter = "@--@") {
	return client.accountNumber
		+ (delimiter + to_string(client.pinCode))
		+ (delimiter + client.name)
		+ (delimiter + client.phone)
		+ (delimiter + to_string(client.accountBalance));
}

string getAnswer(string messageToDisplay = "") {
	string userAnswer;

	cout << messageToDisplay;
	cin >> userAnswer;

	return userAnswer;
}


bool addDataLineToFile(string dataLine, string filePath) {
	fstream file;

	file.open(filePath, ios::app | ios::out);

	if (file.is_open()) {
		file << dataLine << endl;
		file.close();
	}
	else
	{
		cout << " !! Coudn't open the specified file. file not exist!" << endl;
		return false;
	}
	return true;
}

bool saveClientRecordToFile(string filePath) {
	Client client = readClientData("--> Please enter client data: \n");
	return addDataLineToFile(getClientRecord(client), filePath);
}


void addClientRecordsToFile(string filePath) {
	string userAnswer;
	enum choice addMore;

	while (true)
	{
		system("cls");
		if (saveClientRecordToFile(filePath) != true)
			return;
		
		userAnswer = stringToLower(getAnswer(">> Client has beem added succesfully, do you want to add more ? (yes/no): "));
		addMore = (userAnswer == "yes") ? choice::yes : choice::no;

		if (addMore == choice::no)
			break;
		cout << endl;
	}

}

int main()
{
	string filePath = "records.txt";
	addClientRecordsToFile(filePath);

}

