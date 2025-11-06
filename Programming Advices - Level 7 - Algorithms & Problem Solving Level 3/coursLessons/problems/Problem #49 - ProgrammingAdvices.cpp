// Problem #49 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.
#include <iostream>
#include <fstream>
#include <vector>
#include <string>
#include <iomanip>
#include "../MyArrayLibrary.h";

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
	string word = "";

	if (matchCase)
	{
		while ((position = text.find(delimiter)) != string::npos)
		{
			word = text.substr(0, position);

			if (!word.empty())
				textWords.push_back(word);

			text.erase(0, position + delimiter.size());
		}
	}
	else
	{
		delimiter = stringToLower(delimiter);
		string word = "";
		while ((position = stringToLower(text).find(delimiter)) != string::npos)
		{
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

Client convertRecordToClientObject(string clientRecord, string delimiter = "") {
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

bool isClientRecordExistInFile(string filePath, string accountNumber, Client& client, string delimiter = "") {
	vector<string> clientData;
	string fileRecord;
	fstream file;

	file.open(filePath, ios::in);

	if (file.is_open())
	{
		while (getline(file, fileRecord))
		{
			clientData = splitStringWords(fileRecord, delimiter);
			if (clientData[0] == accountNumber)
			{
				client = convertRecordToClientObject(fileRecord, delimiter);
				file.close();
				return true;
			}

		}
	}

	return 0;
}

void printClientsData(Client client) {
	cout << setw(20) << " Pin code: " << setw(20) << client.pinCode << endl;
	cout << setw(20) << " Account number: " << setw(20) << client.accountNumber << endl;
	cout << setw(20) << " Client name : " << setw(20) << client.name << endl;
	cout << setw(20) << " Phone number: " << setw(20) << client.phone << endl;
	cout << setw(20) << " Account balance: " << setw(20) << client.accountBalance << endl;
}

void searchForClientInFileByAccountNumber(string filePath, string accountNumber, string delimiter = "") {
	string clientRecord;
	fstream file;
	Client client;

	if (isClientRecordExistInFile(filePath, accountNumber, client, delimiter))
	{
		cout << " -> Client is found!" << endl;
		cout << " -> Account Details:" << endl;
		printClientsData(client);
	}
	else {
		cout << " No client record has the account number [" << accountNumber << "] !" << endl;
		return;
	}
}

int main()
{
	string filePath = "recordsUpdatedVersion";
	searchForClientInFileByAccountNumber(filePath, input::readString("--> Please enter the account number the client to search: "), "#--#");
}

