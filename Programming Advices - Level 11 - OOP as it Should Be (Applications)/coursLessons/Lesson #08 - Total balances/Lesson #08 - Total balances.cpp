#include <iostream>
#include <iomanip>
#include "../../HeaderFiles/BankClient.h"
#include "../../HeaderFiles/InputValidate.h"
#include "../../HeaderFiles/Utility.h"

void printClientRecordBalanceLine(BankClient client) {
    cout << "| " << setw(15) << left << client.accountNumber();
    cout << "| " << setw(40) << left << client.fullName();
    cout << "| " << setw(12) << left << client.accountBalance;
}

void showTotalBalances() {
    vector<BankClient> vClients = BankClient::getClientsList();

    cout << "\n\t\t\t\t\tBalances List (" << vClients.size() << ") Client(s).";
    cout << "\n_______________________________________________________";
    cout << "_________________________________________\n" << endl;

    cout << "| " << left << setw(15) << "Account Number";
    cout << "| " << left << setw(40) << "Client Name";
    cout << "| " << left << setw(12) << "Balance";
    cout << "\n_______________________________________________________";
    cout << "_________________________________________\n" << endl;

    double totalBalances = BankClient::getTotalBalances();

    if (vClients.size() == 0)
        cout << "\t\t\t\tNo Clients Available In the System!";
    else
        for (BankClient client : vClients) {
            printClientRecordBalanceLine(client);
            cout << endl;
        }

    cout << "\n_______________________________________________________";
    cout << "_________________________________________\n" << endl;
    cout << "\t\t\t\t\t   Total Balances = " << totalBalances << endl;
    cout << "\t\t\t\t\t   ( " << Utility::numberToText(totalBalances) << ")";
}

int main() {
    showTotalBalances();
    system("pause>0");
    return 0;
}