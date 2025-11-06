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

//-------------------------------------------------------------------------------------------------------------------------

struct Client {
    string accountNumber;
    string pinCode;
    string name;
    string phone;
    float accountBalance = 0;
    bool isMarkedToDelete = false;
};

enum ATMoperations {
    quickWithdraw = 1,
    normalWithdraw = 2,
    deposit = 3,
    checkBalance = 4,
    changePassword = 5,
    logout = 6,
};

enum quickWithdrawOptions {
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

Client convertRecordToClientObject(string clientRecord, string delimiter = " ");
string getTableFrame(short frameLength, char pattern = '-');
void login();

// Helper Functions

void printMenu(const string& title, const vector<string>& options) {
    cout << "======================================================" << endl;
    cout << "== " << title << " ==" << endl;
    cout << "======================================================" << endl;
    for (size_t i = 0; i < options.size(); ++i) {
        cout << "==\t [" << (i + 1) << "] " << options[i] << " ==" << endl;
    }
    cout << "======================================================" << endl;
}

void printScreenDisplayTitle(const string& screenTitle) {
    string screenFrame = getTableFrame(120, '=');
    cout << screenFrame << endl;
    cout << "\t\t\t\t\t\t" << screenTitle << " \t\t\t\t\t\t" << endl;
    cout << screenFrame << endl;
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

string getTableFrame(short frameLength, char pattern = '-') {
    string frame = "";
    for (short i = 0; i < frameLength; i++)
        frame += pattern;
    return frame;
}

void drawEndScreenFrame(short operation, short frameLength, char pattern = '-') {
    cout << getTableFrame(frameLength, pattern) << endl;
}

void goBackToTheMainMenu() {
    cout << " -> Press any key to go back to the main menu... ";
    system("pause>0");
    runATMSystemApp();
}

//-------------------------------------------------------------------------------------------------------------------------

// Utility Functions

void loadDataFromFileToVector(string filePath, vector<string>& records) {
    fstream file;
    string fileRecord;

    file.open(filePath, ios::in);

    if (file.is_open()) {
        while (getline(file, fileRecord)) {
            if (!fileRecord.empty())
                records.push_back(fileRecord);
        }
        file.close();
    }
}

vector<string> splitStringWords(string text, string delimiter = " ", bool matchCase = true) {
    short position = 0;
    vector<string> textWords;

    if (matchCase) {
        while ((position = text.find(delimiter)) != string::npos) {
            string word = text.substr(0, position);
            if (!word.empty())
                textWords.push_back(word);
            text.erase(0, position + delimiter.size());
        }
    }
    else {
        delimiter = stringToLower(delimiter);
        while ((position = stringToLower(text).find(delimiter)) != string::npos) {
            string word = text.substr(0, position);
            if (!word.empty())
                textWords.push_back(word);
            text.erase(0, position + delimiter.size());
        }
    }

    if (!text.empty())
        textWords.push_back(text);

    return textWords;
}

string stringToLower(string text) {
    for (int i = 0; i < text.length(); i++)
        text[i] = tolower(text[i]);
    return text;
}

//-------------------------------------------------------------------------------------------------------------------------

// File Handling Functions

vector<Client> getClientsObjectsFromFile(string filePath, string delimiter = " ") {
    vector<Client> clients;
    vector<string> clientRecords;
    loadDataFromFileToVector(filePath, clientRecords);

    for (const string& record : clientRecords)
        clients.push_back(convertRecordToClientObject(record, delimiter));

    return clients;
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

vector<Client> loadClients() {
    return getClientsObjectsFromFile(clientsFilePath, recordsDelimiter);
}

bool saveClients(const vector<Client>& clients) {
    fstream file;
    file.open(clientsFilePath, ios::out);
    if (file.is_open()) {
        for (const Client& client : clients) {
            if (!client.isMarkedToDelete) {
                file << getClientRecord(client) << endl;
            }
        }
        file.close();
        return true;
    }
    return false;
}

//-------------------------------------------------------------------------------------------------------------------------

// Client Operations

bool updateClientBalance(vector<Client>& clients, const string& accountNumber, double newBalance) {
    for (Client& client : clients) {
        if (lowerCaseAllString(client.accountNumber) == lowerCaseAllString(accountNumber)) {
            client.accountBalance = newBalance;
            saveClients(clients);
            cout << "  > Client balance has been updated, current balance is [" << client.accountBalance << "]" << endl;
            return true;
        }
    }
    return false;
}

bool changeClientPassword(vector<Client>& clients, const string& accountNumber, const string& newPinCode) {
    for (Client& client : clients) {
        if (lowerCaseAllString(client.accountNumber) == lowerCaseAllString(accountNumber)) {
            client.pinCode = newPinCode;
            saveClients(clients);
            cout << "  > Client password has been updated successfully!" << endl;
            return true;
        }
    }
    return false;
}

//-------------------------------------------------------------------------------------------------------------------------

// ATM Operations

short getQuickWithdrawOperationOptionAmmount(enum quickWithdrawOptions option) {
    short quickWithdrawAmmounts[] = {
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

void performWithdrawOperation(bool isQuickWithdraw) {
    vector<Client> clients = loadClients();
    int amountToWithdraw;

    if (isQuickWithdraw) {
        enum quickWithdrawOptions option = (enum quickWithdrawOptions)readPositiveInteger(1, 9, " -> What amount to withdraw [1 to 9]? ");
        amountToWithdraw = getQuickWithdrawOperationOptionAmmount(option);
    }
    else {
        do {
            amountToWithdraw = readPositiveInteger(" -> Please enter a 5's multiple amount: ");
            if (amountToWithdraw % 5 != 0) {
                cout << " !! Amount must be a multiple of 5." << endl;
            }
        } while (amountToWithdraw % 5 != 0);
    }

    if (amountToWithdraw > currentClient.accountBalance) {
        cout << " !! The amount exceeds your account balance." << endl;
        return;
    }

    updateClientBalance(clients, currentClient.accountNumber, currentClient.accountBalance - amountToWithdraw);
}

void performDepositOperation() {
    vector<Client> clients = loadClients();
    double amountToDeposit = readPositiveNumber("--> Please enter the amount to deposit: ");
    updateClientBalance(clients, currentClient.accountNumber, currentClient.accountBalance + amountToDeposit);
}

bool isAValidPINCode(string pinCode) {
    int numericPINCode;

    try {
        numericPINCode = stoi(pinCode);
    }
    catch (invalid_argument) {
        return false;
    }

    return (numericPINCode >= 0 && numericPINCode <= 9999);
}

bool isMatchPINCode(string password) {
    return (readString(" -> Retype the new pin code: ") == password);
}

void changeAccountPassword() {
    vector<Client> clients = loadClients();
    string newPinCode = readString(" -> Please enter the new pin code: ");

    if (!isMatchPINCode(newPinCode)) {
        cout << "  > No password matching!" << endl;
        return;
    }

    if (!isAValidPINCode(newPinCode)) {
        cout << " !! Invalid pin code [" << newPinCode << "]" << endl;
        return;
    }

    changeClientPassword(clients, currentClient.accountNumber, newPinCode);
}

//-------------------------------------------------------------------------------------------------------------------------

// Main App Functions

void displayATMSystemMainMenu() {
    vector<string> options = {
        "Quick Withdraw",
        "Normal Withdraw",
        "Deposit",
        "Check Balance",
        "Change Password",
        "Logout"
    };
    printMenu("ATM System Main Menu", options);
}

void executeATMSystemOperation(ATMoperations operationToPerform) {
    system("cls");
    printScreenDisplayTitle("ATM Operation Screen");
    showCurrentClientAccountBalance();

    switch (operationToPerform) {
    case quickWithdraw:
        performWithdrawOperation(true);
        break;
    case normalWithdraw:
        performWithdrawOperation(false);
        break;
    case deposit:
        performDepositOperation();
        break;
    case checkBalance:
        showCurrentClientAccountBalance();
        break;
    case changePassword:
        changeAccountPassword();
        break;
    case logout:
        resetCurrentClient();
        printScreenDisplayTitle("Logout Successfully");
        system("pause");
        login();
        break;
    default:
        exit(0);
    }

    drawEndScreenFrame(operationToPerform, 120, '=');
    goBackToTheMainMenu();
}

void runATMSystemApp() {
    while (true) {
        system("cls");
        displayATMSystemMainMenu();
        executeATMSystemOperation((ATMoperations)readPositiveInteger(1, 6, " -> Please enter your choice [1 to 6]? "));
    }
}

void readClientLogin() {
    cout << "------------------------------------------" << endl;
    currentClient.accountNumber = readString(" > Please enter account number: ");
    currentClient.pinCode = readString(" > Please enter the pin code: ");
    cout << "------------------------------------------" << endl;
}

bool isClientRecordExistInFile(string filePath, Client& client, string delimiter = " ") {
    vector<string> clientData;
    string fileRecord;
    fstream file;

    file.open(filePath, ios::in);

    if (file.is_open()) {
        while (getline(file, fileRecord)) {
            clientData = splitStringWords(fileRecord, delimiter);
            if ((stringToLower(clientData[0]) == stringToLower(client.accountNumber)) && (clientData[1] == client.pinCode)) {
                client = convertRecordToClientObject(fileRecord, delimiter);
                file.close();
                return true;
            }
        }
    }

    return false;
}

void login() {
    system("cls");
    printScreenDisplayTitle("Login Screen");

    while (true) {
        readClientLogin();
        if (!isClientRecordExistInFile(clientsFilePath, currentClient, recordsDelimiter)) {
            system("cls");
            printScreenDisplayTitle("Login Screen");
            cout << " > Invalid account number or pin code!" << endl;
        }
        else {
            runATMSystemApp();
        }
    }
}

//-------------------------------------------------------------------------------------------------------------------------

int main() {
    login();
}