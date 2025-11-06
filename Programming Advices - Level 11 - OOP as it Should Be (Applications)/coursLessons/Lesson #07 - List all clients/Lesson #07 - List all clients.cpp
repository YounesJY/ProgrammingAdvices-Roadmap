#include <iostream>
#include "../../HeaderFiles/BankClient.h"
#include "../../HeaderFiles/InputValidate.h"
#include <iomanip>

void printClientRecordLine(BankClient client) {
    cout << "| " << setw(15) << left << client.accountNumber();
    cout << "| " << setw(20) << left << client.fullName();
    cout << "| " << setw(12) << left << client.phone;
    cout << "| " << setw(20) << left << client.email;
    cout << "| " << setw(10) << left << client.pinCode;
    cout << "| " << setw(12) << left << client.accountBalance;
}

void showClientsList() {
    vector<BankClient> vClients = BankClient::getClientsList();

    cout << "\n\t\t\t\t\tClient List (" << vClients.size() << ") Client(s).";
    cout << "\n_______________________________________________________";
    cout << "_________________________________________\n" << endl;

    cout << "| " << left << setw(15) << "Account Number";
    cout << "| " << left << setw(20) << "Client Name";
    cout << "| " << left << setw(12) << "Phone";
    cout << "| " << left << setw(20) << "Email";
    cout << "| " << left << setw(10) << "Pin Code";
    cout << "| " << left << setw(12) << "Balance";
    cout << "\n_______________________________________________________";
    cout << "_________________________________________\n" << endl;

    if (vClients.size() == 0)
        cout << "\t\t\t\tNo Clients Available In the System!";
    else
        for (BankClient client : vClients) {
            printClientRecordLine(client);
            cout << endl;
        }

    cout << "\n_______________________________________________________";
    cout << "_________________________________________\n" << endl;
}

int main() {
    showClientsList();
    system("pause>0");
    return 0;
}