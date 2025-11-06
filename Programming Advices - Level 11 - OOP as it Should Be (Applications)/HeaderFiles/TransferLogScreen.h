#pragma once
#include <iostream>
#include <iomanip>

#include "Screen.h"
#include "InputValidate.h"
#include "BankClient.h"


using namespace std;

class TransferLogScreen : protected Screen {
private:
	static void _printTransferLogLine(BankClient::TransferRecord transferRecord) {
		cout << setw(8) << left << "";
		cout << "| " << setw(25) << left << transferRecord.dateTime;
		cout << "| " << setw(10) << left << transferRecord.sourceAccount;
		cout << "| " << setw(10) << left << transferRecord.destinationAccount;
		cout << "| " << setw(10) << left << transferRecord.transferedAmount;
		cout << "| " << setw(10) << left << transferRecord.sourceBalance;
		cout << "| " << setw(10) << left << transferRecord.destinationBalance;
		cout << "| " << setw(10) << left << transferRecord.username;

	}
public:
	static void showTransferLogList() {


		vector<BankClient::TransferRecord> vTransferRecords = BankClient::getTransferLogList();
		string title = "\t  Transfer Log List Screen";
		string subTitle = "\t    (" + to_string(vTransferRecords.size()) + ") Records(s).";

		_drawScreenHeader(title, subTitle);

		cout << setw(8) << left << "";
		cout << "\n\t________________________________________________________________________________________________\n" << endl;
		cout << setw(8) << left << "";
		cout << "| " << left << setw(25 ) << "Date/Time";
		cout << "| " << left << setw(10) << "S. Account";
		cout << "| " << left << setw(10) << "D. Account";
		cout << "| " << left << setw(10) << "Amount";
		cout << "| " << left << setw(10) << "S. Balance";
		cout << "| " << left << setw(10) << "D. Balance";
		cout << "| " << left << setw(10) << "User";
		cout << setw(8) << left << "";
		cout << "\n\t________________________________________________________________________________________________\n" << endl;

		if (vTransferRecords.size() == 0)
			cout << "\t\t\t\tNo Records Available In the System!";
		else {
			for (BankClient::TransferRecord& record : vTransferRecords) {
				_printTransferLogLine(record);
				cout << endl;
			}
		}

		cout << setw(8) << left << "";
		cout << "\n\t________________________________________________________________________________________________\n" << endl;

	}
};