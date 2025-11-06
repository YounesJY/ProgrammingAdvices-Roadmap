// Problem #48 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.
#include <iostream>
#include <fstream>
#include <vector>
#include <string>
#include <iomanip>

using namespace std;
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

vector<string> splitStringWords(string text, string delimiter = " ", bool matchCase = true) {
	short position = 0;
	vector<string> textWords;

	if (matchCase)
	{
		while ((position = text.find(delimiter)) != string::npos)
		{
			string word = "";

			word = text.substr(0, position);

			if (!word.empty())
				textWords.push_back(word);

			text.erase(0, position + delimiter.size());
		}
	}
	else
	{
		delimiter = stringToLower(delimiter);
		while ((position = stringToLower(text).find(delimiter)) != string::npos)
		{
			string word = "";

			word = text.substr(0, position);

			if (!word.empty())
				textWords.push_back(word);

			text.erase(0, position + delimiter.size());

		}
	}

	if (!text.empty())
		textWords.push_back(text);

	return textWords;
}

void loadDataFromFileToVector(string filePath, vector<string>& records) {
	fstream file;
	string fileRecord;

	file.open(filePath, ios::in);

	if (file.is_open())
	{
		while (getline(file, fileRecord))
			records.push_back(fileRecord);

		file.close();
	}

}

Client convertRecordToClientObject(string clientRecord, string delimiter) {
	vector<string> clientData;
	Client client;

	clientData = splitStringWords(clientRecord, delimiter);

	client.accountNumber = clientData[0];
	client.pinCode = stoi(clientData[1]);
	client.name = clientData[2];
	client.phone = clientData[3];
	client.accountBalance = stod(clientData[4]);

	return client;
}

void getClientsObjectsFromFile(string filePath, vector<Client>& clients, string delimiter = " ") {
	vector<string> clientRecords;
	loadDataFromFileToVector(filePath, clientRecords);

	for (string& record : clientRecords)
		clients.push_back(convertRecordToClientObject(record, "#--#"));

}

string getTableFrame(short frameLength, char pattern = '-') {
	string frame = "";
	for (short i = 0; i < frameLength; i++)
		frame += pattern;
	return frame;
}

void printTableHeader(short cellsWidth, string tableFrame, string prefixDecorator = "| ") {
	cout << setw(cellsWidth * 3) << tableFrame << endl;
	cout
		<< prefixDecorator << setw(cellsWidth) << "Account number"
		<< prefixDecorator << setw(cellsWidth) << "Pin code"
		<< prefixDecorator << setw(cellsWidth) << "Client name"
		<< prefixDecorator << setw(cellsWidth) << "Phone number"
		<< prefixDecorator << setw(cellsWidth) << "Account balance"
		<< endl;
	cout << setw(cellsWidth * 3) << tableFrame << endl;
}

void printTableBody(vector<Client>& clients, short cellsWidth , string tableFrame, string prefixDecorator = "| ") {
	cout << setw(cellsWidth * 3) << tableFrame << endl;
	for (const Client& client : clients) {
		cout
			<< prefixDecorator << setw(cellsWidth) << client.accountNumber
			<< prefixDecorator << setw(cellsWidth) << client.pinCode
			<< prefixDecorator << setw(cellsWidth) << client.name
			<< prefixDecorator << setw(cellsWidth) << client.phone
			<< prefixDecorator << setw(cellsWidth) << client.accountBalance
			<< endl;
	}
	cout << setw(cellsWidth * 3) << tableFrame << endl;
}

void printClientsTable(vector<Client>& clients, short cellsWidth = 15,  short frameLength = 50, char pattern = '-', string prefixDecorator = "| ") {
	string tableFrame = getTableFrame(frameLength, pattern);

	printTableHeader(cellsWidth, tableFrame, prefixDecorator);
	printTableBody(clients, cellsWidth, tableFrame, prefixDecorator);
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

void deleteClientRecordFromfile(short accountNumber, string filePath, string delimiter = "") {
	vector<string> clientRecords;
	vector<string> clientData;
	loadDataFromFileToVector(filePath, clientRecords);

	for (const string& record : clientRecords) {
		clientData = splitStringWords(record, delimiter);
		if (stoi(clientData[0]) != accountNumber)
			addDataLineToFile(record, "recordsUpdatedVersion");
	}

}

int main()
{
	vector<Client> clients;
	vector<string> clientRecords;
	string filePath = "recordsUpdatedVersion";


	getClientsObjectsFromFile(filePath, clients);
	printClientsTable(clients, 20, 120, ':', " | ");


	float num = 18.21826535;

}

