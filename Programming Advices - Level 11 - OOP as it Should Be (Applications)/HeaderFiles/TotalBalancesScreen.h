#pragma once
#include <iostream>
#include "Screen.h"
#include "BankClient.h"
#include <iomanip>
#include "Utility.h"

class TotalBalancesScreen : protected Screen {
private:
	static void _printClientRecordBalanceLine(BankClient client) {
		cout << setw(25) << left << "";
		cout << "| " << setw(15) << left << client.accountNumber();
		cout << "| " << setw(40) << left << client.fullName();
		cout << "| " << setw(12) << left << client.accountBalance;
	}

public:
	static void showTotalBalances() {
		vector<BankClient> clients = BankClient::getClientsList();
		string title = "\t  Balances List Screen";
		string subtitle = "\t    (" + to_string(clients.size()) + ") Client(s).";


		_drawScreenHeader(title, subtitle);

		cout << setw(25) << left << "";
		cout << "\n\t\t_________________________________________________________________________________\n" << endl;
		cout << setw(25) << left << "";
		cout << "| " << left << setw(15) << "Account Number";
		cout << "| " << left << setw(40) << "Client Name";
		cout << "| " << left << setw(12) << "Balance";
		cout << setw(25) << left << "";
		cout << "\t\t_________________________________________________________________________________\n" << endl;

		double totalBalances = BankClient::getTotalBalances();

		if (clients.size() == 0)
			cout << "\t\t\t\tNo Clients Available In the System!";
		else {
			for (const BankClient& client : clients) {
				_printClientRecordBalanceLine(client);
				cout << endl;
			}
		}

		cout << setw(25) << left << "";
		cout << "\n\t\t_________________________________________________________________________________\n" << endl;

		cout << setw(8) << left << "" << "\t\t\t\t\t     Total Balances = " << totalBalances << endl;
		cout << setw(8) << left << "" << "\t\t\t\t  ( " << Utility::numberToText(totalBalances) << ")";
	}
};