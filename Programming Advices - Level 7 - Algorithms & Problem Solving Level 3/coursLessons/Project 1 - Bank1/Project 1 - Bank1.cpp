// Project 1 - Bank1.cpp : This file contains the 'main' function. Program execution begins and ends there.
#include <iostream>
#include <fstream>
#include <vector>
#include <string>
#include <iomanip>
#include "../MyInputLibrary.h"
#include "../MyOthersStuffLibrary.h"

using namespace std;
using namespace input;
using namespace others;

struct Client
{
	string accountNumber;
	string pinCode;
	string name;
	string phone;
	float accountBalance = 0;
	bool isMarkedToDelete = false;
};
enum operations
{
	showClientsList = 1,
	addClient = 2,
	deleteClient = 3,
	updateClient = 4,
	findClient = 5,
	performTansactions = 6,
	exitApp = 7
};
enum transactions {
	deposit = 1,
	withdraw = 2,
	getTotalBalance = 3,
};
const string filePath = "clientsRecords.txt";
const string recordsDelimiter = "@--@";


void runBankApp();
void runTransactionsOperations();

enum choice askForChoice(string messageToDisplay = "") {
	string userChoice;

	cout << endl;
	userChoice = lowerCaseAllString(readString(messageToDisplay));

	return ((userChoice == "yes") ? choice::yes : choice::no);
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


void loadDataFromFileToVector(string filePath, vector<string>& records) {
	fstream file;
	string fileRecord;

	file.open(filePath, ios::in);

	if (file.is_open())
	{
		while (getline(file, fileRecord))
		{
			if (!fileRecord.empty())
				records.push_back(fileRecord);
		}

		file.close();
	}

}

bool addDataLineToFile(string filePath, string dataLine) {
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

Client convertRecordToClientObject(string clientRecord, string delimiter = " ") {
	vector<string> clientData;
	Client client;

	clientData = splitStringWords(clientRecord, delimiter);

	client.accountNumber = clientData[0];
	client.pinCode = clientData[1];
	client.name = clientData[2];
	client.phone = clientData[3];
	client.accountBalance = stod(clientData[4]);

	return client;
}

vector<Client> getClientsObjectsFromFile(string filePath, string delimiter = " ") {
	vector<Client> clients;
	vector<string> clientRecords;
	loadDataFromFileToVector(filePath, clientRecords);

	for (const string& record : clientRecords)
		clients.push_back(convertRecordToClientObject(record, delimiter));

	return clients;
}

bool isClientRecordExistInFile(string filePath, string accountNumber, Client& client, string delimiter = " ") {
	vector<string> clientData;
	string fileRecord;
	fstream file;

	file.open(filePath, ios::in);

	if (file.is_open())
	{
		while (getline(file, fileRecord))
		{
			clientData = splitStringWords(fileRecord, delimiter);
			if (lowerCaseAllString(clientData[0]) == lowerCaseAllString(accountNumber))
			{
				client = convertRecordToClientObject(fileRecord, delimiter);
				file.close();
				return true;
			}

		}
	}

	return false;
}

bool isClientRecordExistInFile(string filePath, string accountNumber, string delimiter = " ") {
	Client client;
	return isClientRecordExistInFile(filePath, accountNumber, client, delimiter);
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

void printTableBody(vector<Client>& clients, short cellsWidth, string tableFrame, string prefixDecorator = "| ") {
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

void printClientsTable(vector<Client>& clients, short cellsWidth = 15, short frameLength = 50, char pattern = '-', string prefixDecorator = "| ") {
	string tableFrame = getTableFrame(frameLength, pattern);

	cout << "\t\t\t\t\t Clients List - (" << clients.size() << ") Client(s) \t\t\t\t\t" << endl;
	printTableHeader(cellsWidth, tableFrame, prefixDecorator);
	printTableBody(clients, cellsWidth, tableFrame, prefixDecorator);
}

void printClientsTable(string filePath, string delimiter = " ", short cellsWidth = 15, short frameLength = 50, char pattern = '-', string prefixDecorator = "| ") {
	string tableFrame = getTableFrame(frameLength, pattern);
	vector<Client> clients = getClientsObjectsFromFile(filePath, delimiter);
	cout << "\t\t\t\t\t Clients List - (" << clients.size() << ") Client(s) \t\t\t\t\t" << endl;
	printTableHeader(cellsWidth, tableFrame, prefixDecorator);
	printTableBody(clients, cellsWidth, tableFrame, prefixDecorator);
}



Client readClientData(string messageToDisplay = "", bool isAlreadyExist = false) {
	Client client;

	cout << messageToDisplay;
	cout << "------------------------------------------" << endl;
	if (!isAlreadyExist)
		client.accountNumber = readString(" > Please enter account number: ");
	client.pinCode = readString(" > Please enter pin code: ");
	client.name = readString(" > Please enter client name: ");
	client.phone = readString(" > Please enter client phone: ");
	client.accountBalance = readDouble(" > Please enter account balance: ");
	cout << "------------------------------------------" << endl;

	return client;
}

void printClientsDataCard(Client client) {
	cout << "------------------------------------------" << endl;
	cout << "--" << setw(20) << " Account number : " << setw(20) << client.accountNumber << endl;
	cout << "--" << setw(20) << " Pin code       : " << setw(20) << client.pinCode << endl;
	cout << "--" << setw(20) << " Client name    : " << setw(20) << client.name << endl;
	cout << "--" << setw(20) << " Phone number   : " << setw(20) << client.phone << endl;
	cout << "--" << setw(20) << " Account balance: " << setw(20) << client.accountBalance << endl;
	cout << "------------------------------------------" << endl;
}

string getClientRecord(Client client, string delimiter = "@--@") {
	return upperCaseAllString(client.accountNumber)
		+ (delimiter + client.pinCode)
		+ (delimiter + client.name)
		+ (delimiter + client.phone)
		+ (delimiter + to_string(client.accountBalance));
}


void addNewClient(string filePath, string delimiter = " ") {
	Client client;
	string accountNumber;
	enum choice addMore;

	do
	{
		accountNumber = readString(" -> Please enter account number: ");
		if (isClientRecordExistInFile(filePath, accountNumber, delimiter))
			cout << "  A client with account number [" << accountNumber << "] is already exist! please try with another one." << endl;
		else
		{
			client = readClientData("", true);
			client.accountNumber = accountNumber;
			if (addDataLineToFile(filePath, getClientRecord(client, delimiter)))
			{
				addMore = askForChoice("  > Client has beem added succesfully, do you want to add more ? (yes/no): ");
				if (addMore == choice::no)
					break;
				cout << endl;
			}
		}
	} while (true);
}

void saveClientObjectsToFile(string filePath, vector<Client>& clients) {
	fstream file;

	file.open(filePath, ios::out);
	if (file.is_open())
	{
		for (const Client& client : clients)
		{
			if (!client.isMarkedToDelete)
				file << getClientRecord(client) << endl;
		}
		file.close();
	}
}

bool markClientToDeleteByAccountNumber(vector<Client>& clients, string accountNumber) {
	for (Client& client : clients)
	{
		if (lowerCaseAllString(client.accountNumber) == lowerCaseAllString(accountNumber))
		{
			client.isMarkedToDelete = true;
			return true;
		}
	}
	return false;
}

bool deleteClientRecordFromfileByAccountNumber(string filePath, string accountNumber, string delimiter = " ") {
	vector<Client>  clients;
	Client clientToDelete;
	enum choice deleteRecord;

	clients = getClientsObjectsFromFile(filePath, delimiter);

	if (isClientRecordExistInFile(filePath, accountNumber, clientToDelete, delimiter))
	{
		cout << " -> Account Details:" << endl;
		printClientsDataCard(clientToDelete);

		deleteRecord = askForChoice("--> Do you want to delete this record from file ? (yes / no) : ");
		if (deleteRecord == choice::yes)
		{
			markClientToDeleteByAccountNumber(clients, accountNumber);
			saveClientObjectsToFile(filePath, clients);
			return true;
		}
	}
	else
		cout << "  ! No client record has the account number [" << accountNumber << "] !" << endl;
	return false;
}

bool updateClienDataByAccountNumber(vector<Client>& clients, string accountNumber) {
	for (Client& client : clients)
	{
		if (lowerCaseAllString(client.accountNumber) == lowerCaseAllString(accountNumber))
		{
			client = readClientData("--> Please enter the new information: \n", true);
			client.accountNumber = accountNumber;
			return true;
		}
	}
	return false;
}

bool updateClientRecordFromfileByAccountNumber(string filePath, string accountNumber, string delimiter = "") {
	vector<Client>  clients;
	Client clientToUpdate;
	enum choice updateRecord;

	clients = getClientsObjectsFromFile(filePath, delimiter);

	if (isClientRecordExistInFile(filePath, accountNumber, clientToUpdate, delimiter))
	{
		cout << " -> Account Details:" << endl;
		printClientsDataCard(clientToUpdate);

		updateRecord = askForChoice("--> Do you want to update this record ? (yes/no): ");
		if (updateRecord == choice::yes)
		{
			updateClienDataByAccountNumber(clients, accountNumber);
			saveClientObjectsToFile(filePath, clients);
			return true;
		}
	}
	else
		cout << "  ! No client record has the account number [" << accountNumber << "] !" << endl;

	return false;
}

bool updateClientBalance(vector<Client>& clients, string accountNumber, double newBalance) {
	enum choice userChoice;

	userChoice = askForChoice(" -> Are you sure you want to perform this operation? ");
	if (userChoice == choice::yes)
	{
		for (Client& client : clients)
		{
			if (lowerCaseAllString(client.accountNumber) == lowerCaseAllString(accountNumber))
			{
				client.accountBalance = newBalance;
				saveClientObjectsToFile(filePath, clients);
				cout << "  > Client balance has been updated, current balance is [" << client.accountBalance << "]" << endl;
				return true;
			}
		}
	}

	return false;
}


enum operations askForOperationToPerforme(string messageToDisplay, short from, short to) {
	cout << "  - What operation do you want execute ?" << endl;
	return (enum operations)readPositiveInteger(from, to, messageToDisplay);
}


void printScreenDisplayTitle(string screenTitle) {
	string screenFrame = getTableFrame(120, '=');

	cout << screenFrame << endl;
	cout << "\t\t\t\t\t\t" << screenTitle << " \t\t\t\t\t\t" << endl;
	cout << screenFrame << endl;
}

void printScreenDisplayTitle(enum operations operation) {
	switch (operation)
	{
	case operations::showClientsList:
		printScreenDisplayTitle("Clients List Screen");
		break;
	case operations::addClient:
		printScreenDisplayTitle("Add Client Screen");
		break;
	case operations::deleteClient:
		printScreenDisplayTitle("Delete Client Screen");
		break;
	case operations::updateClient:
		printScreenDisplayTitle("Update Client Screen");
		break;
	case operations::findClient:
		printScreenDisplayTitle("Find Client Screen");
		break;
	case operations::exitApp:
		printScreenDisplayTitle("Exit The App");
		break;
	}
}

void printScreenDisplayTitle(enum transactions transaction) {
	switch (transaction)
	{
	case transactions::deposit:
		printScreenDisplayTitle("Deposit Transaction Screen");
		break;
	case transactions::withdraw:
		printScreenDisplayTitle("Withdraw Transaction Screen");
		break;
	case transactions::getTotalBalance:
		printScreenDisplayTitle("Totale Balance Screen");
		break;
	}
}

void drawEndScreenFrame(short operation, short frameLength, char pattern = '-') {
	if (operation == operations::showClientsList || operation == operations::exitApp || operation == transactions::getTotalBalance)
		return;

	cout << getTableFrame(frameLength, '=') << endl;
}

void goBackToTransactionsMainMenu() {
	cout << " -> Press any key to go back to transactions main menu... ";
	system("pause>0");
	runTransactionsOperations();
}

void goBackToTheMainMenu() {
	cout << " -> Press any key to go back to the main menu... ";
	system("pause>0");
	runBankApp();
}


void displayTransactionsMainMenu() {
	cout << "======================================================" << endl;
	cout << "==             Transactions Main Menu               ==" << endl;
	cout << "======================================================" << endl;
	cout << "==\t [1] Deposit                               ==" << endl;
	cout << "==\t [2] Withdraw                              ==" << endl;
	cout << "==\t [3] Totale balances                       ==" << endl;
	cout << "==\t [4] Main Menu                             ==" << endl;
	cout << "======================================================" << endl;
}

void performDepositTransaction(string filePath, string delimiter = " ") {
	string accountNumber;
	vector<Client> clients;
	Client clientForDeposit;
	double amountToDeposit;

	accountNumber = readString("--> Please enter the account number: ");
	if (isClientRecordExistInFile(filePath, accountNumber, clientForDeposit, delimiter))
	{
		clients = getClientsObjectsFromFile(filePath, delimiter);
		printClientsDataCard(clientForDeposit);

		amountToDeposit = readDouble("--> Please enter the ammount to deposit: ");
		updateClientBalance(clients, accountNumber, (clientForDeposit.accountBalance + amountToDeposit));
		return;
	}
	else
		cout << "  ! No client has the account number [" << accountNumber << "] !" << endl;
}

void performWithdrawTransaction(string filePath, string delimiter = " ") {
	string accountNumber;
	vector<Client> clients;
	Client clientForWithdraw;
	double amountToWithdraw;

	accountNumber = readString("--> Please enter the account number: ");
	if (isClientRecordExistInFile(filePath, accountNumber, clientForWithdraw, delimiter))
	{
		clients = getClientsObjectsFromFile(filePath, delimiter);
		printClientsDataCard(clientForWithdraw);

		do
		{
			amountToWithdraw = readDouble("--> Please enter the ammount to withdraw: ");
			if (amountToWithdraw > clientForWithdraw.accountBalance)
				cout << "  ! Amount exceeds the balance, you only have in your [" << clientForWithdraw.accountBalance << "] account" << endl;
		} while (amountToWithdraw > clientForWithdraw.accountBalance);

		updateClientBalance(clients, accountNumber, (clientForWithdraw.accountBalance - amountToWithdraw));
		return;
	}
	else
		cout << "  ! No client has the account number [" << accountNumber << "] !" << endl;
}

void checkForTotaleBalanceTransaction(string filePath, short cellsWidth, short frameLength, string delimiter = " ", string prefixDecorator = "| ") {
	vector<Client> clients = getClientsObjectsFromFile(filePath, delimiter);
	float totaleBalance = 0;
	string totaleBalanceStr = "";
	string tableFrame = getTableFrame(frameLength);

	cout << "\t\t\t\t\t Clients List - (" << clients.size() << ") Client(s) \t\t\t\t\t" << endl;

	cout << setw(cellsWidth * 3) << tableFrame << endl;
	cout
		<< prefixDecorator << setw(cellsWidth) << "Account number"
		<< prefixDecorator << setw(cellsWidth) << "Client name"
		<< prefixDecorator << setw(cellsWidth) << "Account balance"
		<< endl;
	cout << setw(cellsWidth * 3) << tableFrame << endl;

	cout << setw(cellsWidth * 3) << tableFrame << endl;
	for (const Client& client : clients) {
		cout
			<< prefixDecorator << setw(cellsWidth) << client.accountNumber
			<< prefixDecorator << setw(cellsWidth) << client.name
			<< prefixDecorator << setw(cellsWidth) << client.accountBalance
			<< endl;
		totaleBalance += client.accountBalance;
	}
	cout << setw(cellsWidth * 3) << tableFrame << endl;

	totaleBalanceStr = to_string(totaleBalance);
	printScreenDisplayTitle("Totale Balance : " + totaleBalanceStr.substr(0, totaleBalanceStr.length() - 1));
}

void executeTransactionOperation(enum transactions transaction) {
	system("cls");
	printScreenDisplayTitle(transaction);
	switch (transaction)
	{
	case transactions::deposit:
		performDepositTransaction(filePath, recordsDelimiter);
		break;
	case transactions::withdraw:
		performWithdrawTransaction(filePath, recordsDelimiter);
		break;
	case transactions::getTotalBalance:
		checkForTotaleBalanceTransaction(filePath, 30, 120, recordsDelimiter);
		break;
	default:
		runBankApp();
	}
	drawEndScreenFrame(transaction, 120, '=');
	goBackToTransactionsMainMenu();
}


void displayBankMainMenu() {
	cout << "======================================================" << endl;
	cout << "==                 Bank Main Menu                   ==" << endl;
	cout << "======================================================" << endl;
	cout << "==\t [1] Show clients list                      ==" << endl;
	cout << "==\t [2] Add new client                         ==" << endl;
	cout << "==\t [3] Delete a client                        ==" << endl;
	cout << "==\t [4] Update a client                        ==" << endl;
	cout << "==\t [5] Find a client                          ==" << endl;
	cout << "==\t [6] Transactions                           ==" << endl;
	cout << "==\t [7] Exit                                   ==" << endl;
	cout << "======================================================" << endl;
}

void showClientsListOperation(string filePath, string delimiter = " ", short cellsWidth = 15, short frameLength = 50) {
	printClientsTable(filePath, delimiter, cellsWidth, frameLength, '-', " | ");
}

void addClientsOperation(string filePath, string delimiter = " ") {
	addNewClient(filePath, delimiter);
}

void deleteClientsOperation(string filePath, string delimiter = " ") {
	string accountNumber = readString("--> Please enter the account number of the client to delete: ");

	if (deleteClientRecordFromfileByAccountNumber(filePath, accountNumber, delimiter))
		cout << "  > Client record has been deleted successfuly." << endl;
}

void updateClientsOperation(string filePath, string delimiter = " ") {
	string accountNumber = readString("--> Please enter the account number of the client to update: ");

	if (updateClientRecordFromfileByAccountNumber(filePath, accountNumber, delimiter))
		cout << "  > Client record has been updated successfuly." << endl;
}

void findClientsOperation(string filePath, string delimiter = " ") {
	Client client;
	string accountNumber;

	do
	{
		accountNumber = readString("--> Please enter the account number of the client to search for: ");
		if (isClientRecordExistInFile(filePath, accountNumber, client, delimiter))
		{
			printClientsDataCard(client);
			break;
		}
		else
			cout << "  ! No client record has the account number [" << accountNumber << "] !" << endl;
	} while (true);
}

void runTransactionsOperations() {
	while (true)
	{
		system("cls");
		displayTransactionsMainMenu();
		executeTransactionOperation((enum transactions)askForOperationToPerforme(" -> Please enter your choice [1 to 4] ? ", 1, 4));
	}
}

void exitTheBankAppOperation() {
	system("pause");
	exit(0);
}


void executeOperation(operations operationToPerforme) {
	system("cls");
	printScreenDisplayTitle(operationToPerforme);

	switch (operationToPerforme)
	{
	case operations::showClientsList:
		showClientsListOperation(filePath, recordsDelimiter, 20, 120);
		break;
	case operations::addClient:
		addClientsOperation(filePath, recordsDelimiter);
		break;
	case operations::deleteClient:
		deleteClientsOperation(filePath, recordsDelimiter);
		break;
	case operations::updateClient:
		updateClientsOperation(filePath, recordsDelimiter);
		break;
	case operations::findClient:
		findClientsOperation(filePath, recordsDelimiter);
		break;
	case operations::performTansactions:
		runTransactionsOperations();
	case operations::exitApp:
		exitTheBankAppOperation();
	default:
		exit(0);
	}

	drawEndScreenFrame(operationToPerforme, 120, '=');
	goBackToTheMainMenu();
}

void runBankApp() {
	while (true)
	{
		system("cls");
		displayBankMainMenu();
		executeOperation(askForOperationToPerforme(" -> Please enter your choice [1 to 7] ? ", 1, 7));
	}
}

int main()
{
	runBankApp();
}

