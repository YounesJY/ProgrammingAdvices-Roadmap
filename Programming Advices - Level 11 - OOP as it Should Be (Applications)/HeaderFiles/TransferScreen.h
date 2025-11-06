#pragma once
#include <iostream>
#include <iomanip>

#include "Screen.h"
#include "InputValidate.h"
#include "BankClient.h"


using namespace std;

class TransferScreen : protected Screen {
private:
	static void _printClient(BankClient Client) {
		cout << "\nClient Card:";
		cout << "\n______________________________________";
		cout << "\nFull Name   : " << Client.fullName();
		cout << "\nAcc. Number : " << Client.accountNumber();
		cout << "\nBalance     : " << Client.accountBalance;
		cout << "\n______________________________________\n";
	}

	static string _readAccountNumber() {
		string accountNumber;

		accountNumber = InputValidate::readString("\nPlease Enter [Source] Account Number : ");
		while (!BankClient::isClientExist(accountNumber))
			accountNumber = InputValidate::readString("\nAccount number is not found, choose another one : ");

		return accountNumber;
	}

	static double _readAmountToTransfer(BankClient sourceClient) {
		double amountToTransfer;

		amountToTransfer = Input::readDouble("\nPlease enter amount to transfer? ");
		while (amountToTransfer > sourceClient.accountBalance) {
			cout << "\nCannot transfer, Insuffecient Balance!";
			cout << "\nAmout to transfer is:      " << amountToTransfer;
			cout << "\nSource account balance is: " << sourceClient.accountBalance;
			amountToTransfer = Input::readDouble("\n\n\tPlease enter another amount to transfer? ");
		}

		return amountToTransfer;
	}

public:
	static void showTransferScreen() {
		double amountToTransfer;
		Input::choice answer;

		_drawScreenHeader("\tTransfer Screen");


		BankClient sourceClient = BankClient::find(_readAccountNumber());
		_printClient(sourceClient);
		BankClient destinationClient = BankClient::find(_readAccountNumber());
		_printClient(destinationClient);

		amountToTransfer = _readAmountToTransfer(sourceClient);
		answer = Input::getChoice("\nAre you sure you want to perform this transaction? ", false);

		if (answer == Input::choice::yes) {
			if (sourceClient.transfer(amountToTransfer, destinationClient))
				cout << "\n Transfer done successfully.\n";
			else
				cout << "\n Transfer failed.\n";
		}
		else
			cout << "\nOperation was cancelled.\n";


		_printClient(sourceClient);
		_printClient(destinationClient);
	}
};