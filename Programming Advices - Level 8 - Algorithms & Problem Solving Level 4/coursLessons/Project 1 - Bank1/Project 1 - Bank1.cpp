// Project 1 - Bank1.cpp : This file contains the 'main' function. Program execution begins and ends there.
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

struct User
{
	string username;
	string password;
	short permissions = 0;
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
	manageUsers = 7,
	logout = 8
};

enum transactionOperations {
	deposit = 1,
	withdraw = 2,
	getTotalBalance = 3,
};

enum usersManagementOperations {
	showUsersList = 1,
	addUser = 2,
	deleteUser = 3,
	updateUser = 4,
	findUser = 5,
};

enum permissions {
	allPermission = -1,
	showClientsListPermission = 1,
	addClientPermission = 2,
	deleteClientPermission = 4,
	updateClientPermission = 8,
	findClientPermission = 16,
	performTansactionsPermission = 32,
	manageUsersPermission = 64
};

//-------------------------------------------------------------------------------------------------------------------------

const string filePath = "clientsRecords.txt";
const string usersFilePath = "usersRecords.txt";
const string recordsDelimiter = "@--@";
User currentSystemUser;

//-------------------------------------------------------------------------------------------------------------------------

// Main prototypes declarations

void runBankApp();
void login();
void runTransactionsOperations();
void runUsersManagementOperation();
vector<Client> getClientsObjectsFromFile(string, string);

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

//	User related functions

short readALlowedUserPermissions() {
	short permissions = 0;

	cout << " -> Do you want to give to this user:" << endl;

	permissions += ((askForChoice("  - Show client list permission ? ") == choice::yes) ? permissions::showClientsListPermission : 0);
	permissions += ((askForChoice("  - Add client permission ? ") == choice::yes) ? permissions::addClientPermission : 0);
	permissions += ((askForChoice("  - delete client permission ? ") == choice::yes) ? permissions::deleteClientPermission : 0);
	permissions += ((askForChoice("  - Update client permission ? ") == choice::yes) ? permissions::updateClientPermission : 0);
	permissions += ((askForChoice("  - Find client permission ? ") == choice::yes) ? permissions::findClientPermission : 0);
	permissions += ((askForChoice("  - Perform transactions permission ? ") == choice::yes) ? permissions::performTansactionsPermission : 0);
	permissions += ((askForChoice("  - Manage users permission ? ") == choice::yes) ? permissions::manageUsersPermission : 0);

	return permissions;
}

User readUserData(string messageToDisplay = "", bool isAlreadyExist = false) {
	User user;

	cout << messageToDisplay;
	if (!isAlreadyExist)
		user.username = readString(" > Please enter username: ");

	user.password = readString(" > Please enter password: ");

	if (askForChoice(" -> Do you want to give this user full access (full permissions ) ? ") == choice::yes)
		user.permissions = permissions::allPermission;
	else
		user.permissions = readALlowedUserPermissions();

	return user;
}

void readClientLogin(string messageToDisplay = "", bool isAlreadyExist = false) {
	cout << "------------------------------------------" << endl;
	currentSystemUser.username = readString(" > Please enter username: ");
	currentSystemUser.password = readString(" > Please enter password: ");
	cout << "------------------------------------------" << endl;
}

void printUserDataCard(User user) {
	cout << "-------------- User Details --------------" << endl;
	cout << "------------------------------------------" << endl;
	cout << "--" << setw(20) << " Username       : " << setw(20) << user.username << endl;
	cout << "--" << setw(20) << " Password       : " << setw(20) << user.password << endl;
	cout << "--" << setw(20) << " Permissions    : " << setw(20) << user.permissions << endl;
	cout << "------------------------------------------" << endl;
}

string getUserRecord(User user, string delimiter = "@--@") {
	return (user.username + delimiter
		+ user.password + delimiter
		+ to_string(user.permissions));
}

User convertRecordToUserObject(string userRecord, string delimiter = " ") {
	vector<string> userData;
	User user;

	userData = splitStringWords(userRecord, delimiter);

	user.username = userData[0];
	user.password = userData[1];
	user.permissions = stod(userData[2]);

	return user;
}

vector<User> getUsersObjectsFromFile(string filePath, string delimiter = " ") {
	vector<User> users;
	vector<string> usersRecords;
	loadDataFromFileToVector(filePath, usersRecords);

	for (const string& record : usersRecords)
		users.push_back(convertRecordToUserObject(record, delimiter));

	return users;
}

bool isUserObjectExistInFile(string filePath, User& user, string delimiter = " ") {
	vector<string> userData;
	string fileRecord;
	fstream file;

	file.open(filePath, ios::in);

	if (file.is_open())
	{
		while (getline(file, fileRecord))
		{
			userData = splitStringWords(fileRecord, delimiter);
			if ((stringToLower(userData[0]) == stringToLower(user.username)) && (userData[1] == user.password))
			{
				user = convertRecordToUserObject(fileRecord, delimiter);
				file.close();
				return true;
			}

		}
	}

	return false;
}

bool isUserObjectExistInFileByUsername(string filePath, string username, User& user, string delimiter = " ") {
	vector<string> userData;
	string fileRecord;
	fstream file;

	file.open(filePath, ios::in);

	if (file.is_open())
	{
		while (getline(file, fileRecord))
		{
			userData = splitStringWords(fileRecord, delimiter);
			if (stringToLower(userData[0]) == stringToLower(username))
			{
				user = convertRecordToUserObject(fileRecord, delimiter);
				file.close();
				return true;
			}

		}
	}

	return false;
}

bool isUserObjectExistInFileByUsername(string filePath, string username, string delimiter = " ") {
	User user;
	return isUserObjectExistInFileByUsername(filePath, username, user, delimiter);
}

void saveUsersObjectsToFile(string filePath, vector<User>& users) {
	fstream file;

	file.open(filePath, ios::out);
	if (file.is_open())
	{
		for (const User& user : users)
		{
			if (!user.isMarkedToDelete)
				file << getUserRecord(user) << endl;
		}
		file.close();
	}
}


void addNewUser(string filePath, string delimiter) {
	User user;
	string username;
	enum choice addMore;

	do
	{
		username = readString(" -> Please enter the username: ");
		if (isUserObjectExistInFileByUsername(filePath, username, delimiter))
			cout << "  A user with the username [" << username << "] is already exist! please try with another one." << endl;
		else
		{
			user = readUserData("", true);
			user.username = username;
			if (addDataLineToFile(filePath, getUserRecord(user, delimiter)))
			{
				addMore = askForChoice("  > User has been added succesfully, do you want to add more ? (yes/no): ");
				if (addMore == choice::no)
					break;
				cout << endl;
			}
		}
	} while (true);
}

bool markUserToDeleteByUsername(vector<User>& users, string username) {
	for (User& user : users)
	{
		if (lowerCaseAllString(user.username) == lowerCaseAllString(username))
		{
			user.isMarkedToDelete = true;
			return true;
		}
	}
	return false;
}

bool deleteUserRecordFromfileByusername(string filePath, string username, string delimiter = " ") {
	vector<User>  users;
	User userToDelete;
	enum choice deleteRecord;

	if (lowerCaseAllString(username) == "admin")
	{
		cout << " !! You can't delete this user !!" << endl;
		return false;
	}

	users = getUsersObjectsFromFile(filePath, delimiter);

	if (isUserObjectExistInFileByUsername(filePath, username, userToDelete, delimiter))
	{
		printUserDataCard(userToDelete);

		deleteRecord = askForChoice("--> Do you want to delete this record from file ? (yes / no) : ");
		if (deleteRecord == choice::yes)
		{
			markUserToDeleteByUsername(users, username);
			saveUsersObjectsToFile(filePath, users);
			return true;
		}
	}
	else
		cout << "  ! No user record has the username [" << username << "] !" << endl;
	return false;
}

bool updateUserDataByUsername(vector<User>& users, string username) {
	for (User& user : users)
	{
		if (lowerCaseAllString(user.username) == lowerCaseAllString(username))
		{
			user = readUserData("--> Please enter the new information: \n", true);
			user.username = username;
			return true;
		}
	}
	return false;
}

bool updateUserRecordFromfileByUsername(string filePath, string username, string delimiter = "") {
	vector<User>  users;
	User userToUpdate;
	enum choice updateRecord;


	if (lowerCaseAllString(currentSystemUser.username) != "admin" && lowerCaseAllString(username) == "admin")
	{
		cout << " !! You can't update this user !!" << endl;
		cout << " !! You aren't the admin !!" << endl;
		return false;
	}

	users = getUsersObjectsFromFile(filePath, delimiter);

	if (isUserObjectExistInFileByUsername(filePath, username, userToUpdate, delimiter))
	{
		printUserDataCard(userToUpdate);

		updateRecord = askForChoice("--> Do you want to update this record ? (yes/no): ");
		if (updateRecord == choice::yes)
		{
			updateUserDataByUsername(users, username);
			saveUsersObjectsToFile(filePath, users);
			return true;
		}
	}
	else
		cout << "  ! No user record has the username [" << username << "] !" << endl;

	return false;
}

//-------------------------------------------------------------------------------------------------------------------------

//	Tables functions

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

//-------------------------------------------------------------------------------------------------------------------------

//	Client related functions

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

void printClientDataCard(Client client) {
	cout << "------------- Client Details -------------" << endl;
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
		printClientDataCard(clientToDelete);

		deleteRecord = askForChoice("--> Do you want to delete this record from file ? (yes / no) : ");
		if (deleteRecord == choice::yes)
		{
			markClientToDeleteByAccountNumber(clients, accountNumber);
			saveClientsObjectsToFile(filePath, clients);
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
		printClientDataCard(clientToUpdate);

		updateRecord = askForChoice("--> Do you want to update this record ? (yes/no): ");
		if (updateRecord == choice::yes)
		{
			updateClienDataByAccountNumber(clients, accountNumber);
			saveClientsObjectsToFile(filePath, clients);
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
				saveClientsObjectsToFile(filePath, clients);
				cout << "  > Client balance has been updated, current balance is [" << client.accountBalance << "]" << endl;
				return true;
			}
		}
	}

	return false;
}

//-------------------------------------------------------------------------------------------------------------------------

enum operations askForOperationToPerforme(string messageToDisplay, short from, short to) {
	cout << "  - What operation do you want execute ?" << endl;
	return (enum operations)readPositiveInteger(from, to, messageToDisplay);
}

//-------------------------------------------------------------------------------------------------------------------------

// General system funcions

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
	}
}

void printScreenDisplayTitle(enum transactionOperations transaction) {
	switch (transaction)
	{
	case transactionOperations::deposit:
		printScreenDisplayTitle("Deposit Transaction Screen");
		break;
	case transactionOperations::withdraw:
		printScreenDisplayTitle("Withdraw Transaction Screen");
		break;
	case transactionOperations::getTotalBalance:
		printScreenDisplayTitle("Totale Balance Screen");
		break;
	}
}

void printScreenDisplayTitle(enum usersManagementOperations operation) {
	switch (operation)
	{
	case showUsersList:
		printScreenDisplayTitle("Users list Screen");
		break;
	case addUser:
		printScreenDisplayTitle("Add User Screen");
		break;
	case deleteUser:
		printScreenDisplayTitle("Delete User Screen");
		break;
	case updateUser:
		printScreenDisplayTitle("Update User Screen");
		break;
	case findUser:
		printScreenDisplayTitle("Find User Screen");
		break;
	default:
		break;
	}
}

void drawEndScreenFrame(short operation, short frameLength, char pattern = '-') {
	//if (operation == operations::showClientsList || operation == operations::logout || operation == transactionOperations::getTotalBalance)
	//	return;

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

void goBackToUsersManagementMainMenu() {
	cout << " -> Press any key to go back to users management main menu... ";
	system("pause>0");
	runUsersManagementOperation();
}

void ShowAccessDeniedScreen() {
	system("cls");

	cout << "======================================================" << endl;
	cout << " Access denied !" << endl;
	cout << " You don't have access to this permission !" << endl;
	cout << " Please contact your admin." << endl;
	cout << "======================================================" << endl;
}

void resetSystemUser() {
	currentSystemUser.username = "";
	currentSystemUser.password = "";
	currentSystemUser.permissions = 0;
}

//-------------------------------------------------------------------------------------------------------------------------

//	Transaction system functions

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
		printClientDataCard(clientForDeposit);

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
		printClientDataCard(clientForWithdraw);

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

void executeTransactionOperation(enum transactionOperations transaction) {
	system("cls");
	printScreenDisplayTitle(transaction);
	switch (transaction)
	{
	case transactionOperations::deposit:
		performDepositTransaction(filePath, recordsDelimiter);
		break;
	case transactionOperations::withdraw:
		performWithdrawTransaction(filePath, recordsDelimiter);
		break;
	case transactionOperations::getTotalBalance:
		checkForTotaleBalanceTransaction(filePath, 30, 120, recordsDelimiter);
		break;
	default:
		runBankApp();
	}
	drawEndScreenFrame(transaction, 120, '=');
	goBackToTransactionsMainMenu();
}

//-------------------------------------------------------------------------------------------------------------------------

//	Users management functions

void displayManageUserMainMenu() {
	cout << "======================================================" << endl;
	cout << "==             Manage Users Main Menu               ==" << endl;
	cout << "======================================================" << endl;
	cout << "==\t [1] Show users list                      ==" << endl;
	cout << "==\t [2] Add new user                         ==" << endl;
	cout << "==\t [3] Delete a user                        ==" << endl;
	cout << "==\t [4] Update a user                        ==" << endl;
	cout << "==\t [5] Find a user                          ==" << endl;
	cout << "==\t [6] Main menu                           ==" << endl;
	cout << "======================================================" << endl;
}

enum permissions getRequestedPermission(enum operations operationToPerform) {
	enum permissions permissionsToAccess[] = {
		permissions::allPermission,
		permissions::showClientsListPermission,
		permissions::addClientPermission,
		permissions::deleteClientPermission,
		permissions::updateClientPermission,
		permissions::findClientPermission,
		permissions::performTansactionsPermission,
		permissions::manageUsersPermission,
	};

	if (operationToPerform < 1 || operationToPerform > 7)
		return (enum permissions)0;

	return permissionsToAccess[operationToPerform];
}

bool isUserHasPeemissionToPerformOperation(string filePath, User userToVerify, enum operations operationToPerform, string delimiter = " ") {
	enum permissions requestedPermissions;

	if (userToVerify.permissions == permissions::allPermission || operationToPerform == operations::logout)
		return true;

	requestedPermissions = getRequestedPermission(operationToPerform);

	if ((requestedPermissions & userToVerify.permissions) == requestedPermissions)
		return true;

	return false;
}

void showUsersListOperation(string filePath, short cellsWidth, short frameLength, string delimiter = " ", string prefixDecorator = "| ") {
	vector<string> usersRecords;
	vector<User> users;
	string tableFrame = getTableFrame(frameLength);

	users = getUsersObjectsFromFile(filePath, delimiter);

	cout << "\t\t\t\t\t\t Users List - (" << users.size() << ") User(s) \t\t\t\t\t" << endl;

	cout << setw(cellsWidth * 3) << tableFrame << endl;
	cout
		<< prefixDecorator << setw(cellsWidth) << "Username" << "\t\t"
		<< prefixDecorator << setw(cellsWidth) << "Password" << "\t\t"
		<< prefixDecorator << setw(cellsWidth) << "Permissions" << "\t\t"
		<< endl;
	cout << setw(cellsWidth * 3) << tableFrame << endl;

	cout << setw(cellsWidth * 3) << tableFrame << endl;
	for (const User& user : users) {
		cout
			<< prefixDecorator << setw(cellsWidth) << user.username << "\t\t"
			<< prefixDecorator << setw(cellsWidth) << user.password << "\t\t"
			<< prefixDecorator << setw(cellsWidth) << user.permissions << "\t\t"
			<< endl;
	}
	cout << setw(cellsWidth * 3) << tableFrame << endl;

}

void addUsersOperation(string filePath, string delimiter = " ") {
	addNewUser(filePath, delimiter);
}

void deleteUsersOperation(string filePath, string delimiter = " ") {
	string usernameToDelete = readString("--> Please enter the username of the user to delete: ");

	if (lowerCaseAllString(currentSystemUser.username) != "admin" && lowerCaseAllString(usernameToDelete) == lowerCaseAllString(currentSystemUser.username))
	{
		deleteUserRecordFromfileByusername(filePath, usernameToDelete, delimiter);
		cout << "  > Your account has been deleted successfuly." << endl;
		system("pause");
		login();
	}

	if (deleteUserRecordFromfileByusername(filePath, usernameToDelete, delimiter))
		cout << "  > User record has been deleted successfuly." << endl;
}

void updateUsersOperation(string filePath, string delimiter = " ") {
	string username = readString("--> Please enter the username of the user to update: ");

	if (updateUserRecordFromfileByUsername(filePath, username, delimiter))
		cout << "  > User record has been updated successfuly." << endl;
}

void findUsersOperation(string filePath, string delimiter = " ") {
	User user;
	string username;

	do
	{
		username = readString("--> Please enter the username of the user to search for: ");
		if (isUserObjectExistInFileByUsername(filePath, username, user, delimiter))
		{
			printUserDataCard(user);
			break;
		}
		else
			cout << "  ! No user record has the username [" << username << "] !" << endl;
	} while (true);
}

void executeUsersManagementOperation(enum usersManagementOperations operation) {
	system("cls");
	printScreenDisplayTitle(operation);

	switch (operation)
	{
	case usersManagementOperations::showUsersList:
		showUsersListOperation(usersFilePath, 20, 120, recordsDelimiter, " || ");
		break;
	case usersManagementOperations::addUser:
		addUsersOperation(usersFilePath, recordsDelimiter);
		break;
	case usersManagementOperations::deleteUser:
		deleteUsersOperation(usersFilePath, recordsDelimiter);
		break;
	case usersManagementOperations::updateUser:
		updateUsersOperation(usersFilePath, recordsDelimiter);
		break;
	case usersManagementOperations::findUser:
		findUsersOperation(usersFilePath, recordsDelimiter);
		break;
	default:
		runBankApp();
	}

	drawEndScreenFrame(operation, 120, '=');
	goBackToUsersManagementMainMenu();
}

//-------------------------------------------------------------------------------------------------------------------------

//	Main app functions

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
	cout << "==\t [7] Manage users                           ==" << endl;
	cout << "==\t [8] Logout                                 ==" << endl;
	cout << "======================================================" << endl;
}

void showCurrentUserIdentity() {
	cout << setw(120) << "=========================" << endl;
	cout << setw(95) << "" << currentSystemUser.username << endl;
	cout << setw(120) << "=========================" << endl;
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

	//do
	//{
	accountNumber = readString("--> Please enter the account number of the client to search for: ");
	if (isClientRecordExistInFile(filePath, accountNumber, client, delimiter))
	{
		printClientDataCard(client);
		//break;
	}
	else
		cout << "  ! No client record has the account number [" << accountNumber << "] !" << endl;
	//} while (true);
}

void runTransactionsOperations() {
	while (true)
	{
		system("cls");
		displayTransactionsMainMenu();
		executeTransactionOperation((enum transactionOperations)askForOperationToPerforme(" -> Please enter your choice [1 to 4] ? ", 1, 4));
	}
}

void runUsersManagementOperation() {
	while (true)
	{
		system("cls");
		displayManageUserMainMenu();
		executeUsersManagementOperation((enum usersManagementOperations)askForOperationToPerforme(" -> Please enter your choice [1 to 6] ? ", 1, 6));
	}
}

void logoutAccountOperation() {
	system("cls");
	resetSystemUser();
	login();
}

//-------------------------------------------------------------------------------------------------------------------------

void executeOperation(operations operationToPerform) {
	if (!isUserHasPeemissionToPerformOperation(usersFilePath, currentSystemUser, operationToPerform)) {
		ShowAccessDeniedScreen();
		system("pause");
		return;
	}

	system("cls");
	showCurrentUserIdentity();
	printScreenDisplayTitle(operationToPerform);

	switch (operationToPerform)
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
	case operations::manageUsers:
		runUsersManagementOperation();
	case operations::logout:
		logoutAccountOperation();
	default:
		exit(0);
	}

	drawEndScreenFrame(operationToPerform, 120, '=');
	goBackToTheMainMenu();
}

void runBankApp() {
	while (true)
	{
		system("cls");
		showCurrentUserIdentity();
		displayBankMainMenu();
		executeOperation(askForOperationToPerforme(" -> Please enter your choice [1 to 8] ? ", 1, 8));
	}
}

void login() {
	system("cls");
	printScreenDisplayTitle("Login Screen");

	while (true)
	{
		readClientLogin();
		if (!isUserObjectExistInFile(usersFilePath, currentSystemUser, recordsDelimiter))
		{
			system("cls");
			printScreenDisplayTitle("Login Screen");
			cout << " > Invalid username or password!" << endl;
		}
		else
			runBankApp();
	}
}

//-------------------------------------------------------------------------------------------------------------------------

int main()
{
	login();
}

