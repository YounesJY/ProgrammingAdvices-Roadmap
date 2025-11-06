// Project 2 - ATM.cpp : This file contains the 'main' function. Program execution begins and ends there.


#include <iostream>
#include <fstream>
#include <vector>
#include <string>
#include <iomanip>
#include "../MyInputLibrary.h";
#include "../MyOthersStuffLibrary.h";

using namespace std;
using namespace input;
using namespace others;

//-------------------------------------------------------------------------------------------------------------------------

struct Client
{
	string accountNumber;
	string pinCode;
	string name;
	string phone;
	float accountBalance = 0;
	bool isMarkedToDelete = false;
};

enum ATMoperations
{
	quickWithdraw = 1,
	normalWithdraw = 2,
	deposit = 3,
	checkBalance = 4,
	changePassword = 5,
	logout = 6,
};

enum quickWithdrawOptions
{
	withdraw20 = 1,
	withdraw50 = 2,
	withdraw100 = 3,
	withdraw200 = 4,
	withdraw400 = 5,
	withdraw600 = 6,
	withdraw800 = 7,
	withdraw1000 = 8,
	exitQuickWithdrawOperation = 9
};

//-------------------------------------------------------------------------------------------------------------------------

const string clientsFilePath = "clientsRecords.txt";
const string recordsDelimiter = "@--@";
Client currentClient;

//-------------------------------------------------------------------------------------------------------------------------

// Main prototypes declarations

void runATMSystemApp();
void login();

//-------------------------------------------------------------------------------------------------------------------------

//	general functions for strings, choices, files data reding...

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

	if (!file.is_open()) {
		cerr << "Error: Unable to open file " << filePath << endl;
		return;
	}

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

//-------------------------------------------------------------------------------------------------------------------------

//	Client related functions

void readClientLogin() {
	cout << "------------------------------------------" << endl;
	currentClient.accountNumber = readString(" > Please enter acoount number: ");
	currentClient.pinCode = readString(" > Please enter the pin code: ");
	cout << "------------------------------------------" << endl;
}

string getClientRecord(Client client, string delimiter = "@--@") {
	return upperCaseAllString(client.accountNumber)
		+ (delimiter + client.pinCode)
		+ (delimiter + client.name)
		+ (delimiter + client.phone)
		+ (delimiter + to_string(client.accountBalance));
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

bool isClientRecordExistInFile(string filePath, Client& client, string delimiter = " ") {
	vector<string> clientData;
	string fileRecord;
	fstream file;

	file.open(filePath, ios::in);

	if (file.is_open())
	{
		while (getline(file, fileRecord))
		{
			clientData = splitStringWords(fileRecord, delimiter);
			if ((stringToLower(clientData[0]) == stringToLower(client.accountNumber)) && (clientData[1] == client.pinCode))
			{
				client = convertRecordToClientObject(fileRecord, delimiter);
				file.close();
				return true;
			}

		}
	}

	return false;
}

void saveClientsObjectsToFile(string filePath, vector<Client>& clients) {
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

bool updateClientBalance(vector<Client>& clients, string accountNumber, Client& clientToUpdate, double newBalance) {
	enum choice userChoice;

	userChoice = askForChoice(" -> Are you sure you want to perform this operation? ");
	if (userChoice == choice::yes)
	{
		for (Client& client : clients)
		{
			if (lowerCaseAllString(client.accountNumber) == lowerCaseAllString(accountNumber))
			{
				client.accountBalance = newBalance;
				saveClientsObjectsToFile(clientsFilePath, clients);
				cout << "  > Client balance has been updated, current balance is [" << client.accountBalance << "]" << endl;
				clientToUpdate = client;
				return true;
			}
		}
	}

	return false;
}

bool isAValidPINCode(string pinCode) {
	int numericPINCode;

	try
	{
		numericPINCode = stoi(pinCode);
	}
	catch (invalid_argument)
	{
		return false;
	}

	return (numericPINCode >= 0 && numericPINCode <= 9999);
}

bool isMatchPINCode(string password) {
	return (readString(" -> Retype the new pin code: ") == password);
}



//-------------------------------------------------------------------------------------------------------------------------

//	Tables functions

string getTableFrame(short frameLength, char pattern = '-') {
	string frame = "";
	for (short i = 0; i < frameLength; i++)
		frame += pattern;
	return frame;
}

//-------------------------------------------------------------------------------------------------------------------------

enum ATMoperations askForOperationToPerforme(string messageToDisplay, short from, short to) {
	cout << "  - What operation do you want execute ?" << endl;
	return (enum ATMoperations)readPositiveInteger(from, to, messageToDisplay);
}

//-------------------------------------------------------------------------------------------------------------------------


void printScreenDisplayTitle(string screenTitle) {
	string screenFrame = getTableFrame(120, '=');

	cout << screenFrame << endl;
	cout << "\t\t\t\t\t\t" << screenTitle << " \t\t\t\t\t\t" << endl;
	cout << screenFrame << endl;
}

void printScreenDisplayTitle(enum ATMoperations operation) {
	switch (operation)
	{
	case ATMoperations::quickWithdraw:
		printScreenDisplayTitle("Quick Withdraw Operation Screen");
		break;
	case ATMoperations::normalWithdraw:
		printScreenDisplayTitle("Normal Withdraw Operation Screen");
		break;
	case ATMoperations::checkBalance:
		printScreenDisplayTitle("Checking Account Balance Operation Screen");
		break;
	case ATMoperations::deposit:
		printScreenDisplayTitle("Deposit Operation Screen");
		break;
	case ATMoperations::changePassword:
		printScreenDisplayTitle("Changing Password Operation Screen");
		break;
	default:
		return;
	}
}

void drawEndScreenFrame(short operation, short frameLength, char pattern = '-') {
	//if (operation == ATMoperations::showClientsList || operation == ATMoperations::logout || operation == transactionOperations::getTotalBalance)
	//	return;

	cout << getTableFrame(frameLength, '=') << endl;
}

void goBackToTheMainMenu() {
	cout << " -> Press any key to go back to the main menu... ";
	system("pause>0");
	runATMSystemApp();
}

void showCurrentClientAccountBalance() {
	cout << "------------------------------------------------------" << endl;
	cout << "-- Your account balance is: [" << currentClient.accountBalance << "]" << endl;
	cout << "------------------------------------------------------" << endl;
}

void resetCurrentClient() {
	currentClient.accountNumber = "";
	currentClient.pinCode = "";
}

//-------------------------------------------------------------------------------------------------------------------------

//-------------------------------------------------------------------------------------------------------------------------

//	Main app functions

void displayATMSystemMainMenu() {
	cout << "======================================================" << endl;
	cout << "==              ATM System Main Menu                ==" << endl;
	cout << "======================================================" << endl;
	cout << "==\t [1] Quick withraw                          ==" << endl;
	cout << "==\t [2] Normal withdraw                        ==" << endl;
	cout << "==\t [3] Deposit                                ==" << endl;
	cout << "==\t [4] Check account balance                  ==" << endl;
	cout << "==\t [5] Change account password                ==" << endl;
	cout << "==\t [6] Logout                                 ==" << endl;
	cout << "======================================================" << endl;
}

void showCurrentClientIdentity() {
	cout << setw(120) << "=========================" << endl;
	cout << getTableFrame(95, ' ') << "[" << currentClient.name << " - " << currentClient.accountNumber << "]" << endl;
	cout << setw(120) << "=========================" << endl;
}

void showQuickWithdrawMenuOptions() {
	cout << "======================================================" << endl;
	cout << "==\t [1] 20  \t [2] 50                     ==" << endl;
	cout << "==\t [3] 100 \t [4] 200                    ==" << endl;
	cout << "==\t [5] 400 \t [6] 600                    ==" << endl;
	cout << "==\t [7] 800 \t [8] 1000                   ==" << endl;
	cout << "==\t [9] exit\t                            ==" << endl;
	cout << "======================================================" << endl;
}

short getQuickWithdrawOperationOptionAmmount(enum quickWithdrawOptions option) {
	short  quickWithdrawAmmounts[] = {
	0,
	20,
	50,
	100,
	200,
	400,
	600,
	800,
	1000,
	};

	if (option < 1 || option > 8)
		return quickWithdrawAmmounts[0];

	return quickWithdrawAmmounts[option];
}

void performQuickWithdrawOperation(quickWithdrawOptions withdrawOption) {
	vector<Client> clients;
	int ammountToWithdraw;
	if (withdrawOption == quickWithdrawOptions::exitQuickWithdrawOperation)
		return;

	ammountToWithdraw = getQuickWithdrawOperationOptionAmmount(withdrawOption);

	if (ammountToWithdraw > currentClient.accountBalance)
	{
		cout << " !! The ammount exceeds your account balance, make another choice." << endl;
		goBackToTheMainMenu();
	}

	clients = getClientsObjectsFromFile(clientsFilePath, recordsDelimiter);

	updateClientBalance(clients, currentClient.accountNumber, currentClient, (currentClient.accountBalance - ammountToWithdraw));
}

void runQuickWithdrawOperation() {
	enum quickWithdrawOptions ammountToWithdrawOption;

	showQuickWithdrawMenuOptions();
	showCurrentClientAccountBalance();

	ammountToWithdrawOption = (enum quickWithdrawOptions)input::readPositiveInteger(1, 9, " -> What ammount to withdraw [1 to 9] ? ");

	performQuickWithdrawOperation(ammountToWithdrawOption);
}

void performNormalWithdrawOperation() {
	vector<Client> clients;
	int ammountToWithdraw;
	showCurrentClientAccountBalance();

	do {
		ammountToWithdraw = input::readPositiveInteger(" -> Please enter a 5's multiple amount ? ");

		if (ammountToWithdraw > currentClient.accountBalance) {
			cout << " !! The ammount [" << ammountToWithdraw << "] exceeds your account balance [" << currentClient.accountBalance << "], make another choice." << endl;
			return;
		}

		if ((ammountToWithdraw % 5) != 0)
			cout << " !! Amount [" << ammountToWithdraw << "] isn't a multiple of 5." << endl;
	} while ((ammountToWithdraw % 5) != 0);

	clients = getClientsObjectsFromFile(clientsFilePath, recordsDelimiter);
	updateClientBalance(clients, currentClient.accountNumber, currentClient, (currentClient.accountBalance - ammountToWithdraw));
}

void performDepositOperation() {
	vector<Client> clients;
	double amountToDeposit;

	showCurrentClientAccountBalance();

	clients = getClientsObjectsFromFile(clientsFilePath, recordsDelimiter);
	amountToDeposit = readPositiveNumber("--> Please enter the ammount to deposit: ");

	updateClientBalance(clients, currentClient.accountNumber, currentClient, (currentClient.accountBalance + amountToDeposit));
}

void changeAccountPassword() {
	vector<Client> clients = getClientsObjectsFromFile(clientsFilePath, recordsDelimiter);
	string newPinCode;
	choice userChoice;

	newPinCode = readString(" -> Please enter the new pin code: ");

	if (!isMatchPINCode(newPinCode)) {
		cout << "  > No password matching!" << endl;
		cout << "  > Failed to update password!" << endl;
		return;
	}

	if (!isAValidPINCode(newPinCode)) {
		cout << " !! Invalid pin code [" << newPinCode << "]" << endl;
		cout << "  > Failed to update password!" << endl;
		return;
	}
	
	userChoice = askForChoice(" -> Are you sure you want to perform this operation? ");
			
	if (userChoice == choice::yes) {
		for (Client& client : clients)
		{
			if (lowerCaseAllString(client.accountNumber) == lowerCaseAllString(currentClient.accountNumber))
			{
				client.pinCode = newPinCode;
				currentClient = client;
			
				saveClientsObjectsToFile(clientsFilePath, clients);
				cout << "  > Client password has been updated Successfully!" << endl;
			
				return;
			}
		}
	}

}

void logoutAccountOperation() {
	system("cls");
	resetCurrentClient();
	printScreenDisplayTitle("Logout Successfully");
	system("pause");
	login();
}

//-------------------------------------------------------------------------------------------------------------------------
void executeATMSystemOperation(ATMoperations operationToPerform) {
	system("cls");
	showCurrentClientIdentity();
	printScreenDisplayTitle(operationToPerform);

	switch (operationToPerform)
	{
	case ATMoperations::quickWithdraw:
		runQuickWithdrawOperation();
		break;
	case ATMoperations::normalWithdraw:
		performNormalWithdrawOperation();
		break;
	case ATMoperations::deposit:
		performDepositOperation();
		break;
	case ATMoperations::checkBalance:
		showCurrentClientAccountBalance();
		break; \
	case ATMoperations::changePassword:
		changeAccountPassword();
		break;
	case ATMoperations::logout:
		logoutAccountOperation();
	default:
		exit(0);
	}

	drawEndScreenFrame(operationToPerform, 120, '=');
	goBackToTheMainMenu();
}

void runATMSystemApp() {
	while (true)
	{
		system("cls");
		showCurrentClientIdentity();
		displayATMSystemMainMenu();
		executeATMSystemOperation(askForOperationToPerforme(" -> Please enter your choice [1 to 6] ? ", 1, 6));
	}
}

void login() {
	system("cls");
	printScreenDisplayTitle("Login Screen");

	while (true)
	{
		readClientLogin();
		if (!isClientRecordExistInFile(clientsFilePath, currentClient, recordsDelimiter))
		{
			system("cls");
			printScreenDisplayTitle("Login Screen");
			cout << " > Invalid account number or pin code!" << endl;
		}
		else
			runATMSystemApp();
	}
}

//-------------------------------------------------------------------------------------------------------------------------


int main()
{
	login();
}

