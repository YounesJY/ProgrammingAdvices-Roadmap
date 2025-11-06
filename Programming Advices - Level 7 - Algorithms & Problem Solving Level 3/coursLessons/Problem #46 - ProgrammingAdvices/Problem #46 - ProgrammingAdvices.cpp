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

void printClientsData(vector<Client> clients) {
	for (Client client : clients) {
		cout << setw(20) << " Pin code: " << setw(20) << client.pinCode << endl;
		cout << setw(20) << " Account number: " << setw(20) << client.accountNumber << endl;
		cout << setw(20) << " Client name : " << setw(20) << client.name << endl;
		cout << setw(20) << " Phone number: " << setw(20) << client.phone << endl;
		cout << setw(20) << " Account balance: " << setw(20) << client.accountBalance << endl;
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


int main()
{
	vector<string> clientsRecords;
	vector<Client> clients;

	loadDataFromFileToVector("records.txt", clientsRecords);
	
	for (string& clientRecord : clientsRecords)
		clients.push_back(convertRecordToClientObject(clientRecord, "@--@"));
	
	printClientsData(clients);
}

