#pragma once
#include <iostream>
#include <iomanip>

#include "Screen.h"
#include "DepositScreen.h"
#include "WithdrawScreen.h"
#include "TransferScreen.h"
#include "TransferLogScreen.h"
#include "TotalBalancesScreen.h"
#include "InputValidate.h"

using namespace std;

class TransactionsScreen : protected Screen {
private:
	enum enTransactionsMenuOptions {
		deposit = 1, withdraw = 2, transfer = 3,
		transferLog = 4, showTotalBalance = 5, showMainMenu = 6
	};

	static short _readTransactionsMenuOption() {
		cout << setw(37) << left << "" << "Choose what do you want to do? [1 to 6]? ";
		return InputValidate::readNumberBetween<short>(1, 6, "Enter Number between 1 to 6? ");;
	}

	static void _showDepositScreen() {
		//cout << "\n Deposit Screen will be here.\n";
		DepositScreen::showDepositScreen();
	}

	static void _showWithdrawScreen() {
		//cout << "\n Withdraw Screen will be here.\n";
		WithdrawScreen::showWithdrawScreen();
	}

	static void _showTransferScreen() {
		//cout << "\n Transfer Screen will be here.\n";
		TransferScreen::showTransferScreen();
	}	
	
	static void _showTransferLogScreen() {
		//cout << "\n Transfer Log Screen will be here.\n";
		TransferLogScreen::showTransferLogList();
	}


	static void _showTotalBalancesScreen() {
		// cout << "\n Balances Screen will be here.\n";
		TotalBalancesScreen::showTotalBalances();
	}

	static void _goBackToTransactionsMenu() {
		cout << "\n\nPress any key to go back to Transactions Menu...";
		system("pause>0");
		showTransactionsMenu();
	}

	static void _performTransactionsMenuOption(enTransactionsMenuOptions transactionsMenuOption) {
		system("cls");

		switch (transactionsMenuOption) {
		case enTransactionsMenuOptions::deposit: {
			_showDepositScreen();
			_goBackToTransactionsMenu();
			break;
		}
		case enTransactionsMenuOptions::withdraw: {
			_showWithdrawScreen();
			_goBackToTransactionsMenu();
			break;
		}
		case enTransactionsMenuOptions::transfer: {
			_showTransferScreen();
			_goBackToTransactionsMenu();
			break;
		}
		case enTransactionsMenuOptions::transferLog: {
			_showTransferLogScreen();
			_goBackToTransactionsMenu();
			break;
		}
		case enTransactionsMenuOptions::showTotalBalance: {
			_showTotalBalancesScreen();
			_goBackToTransactionsMenu();
			break;
		}
		case enTransactionsMenuOptions::showMainMenu: {
			// Do nothing here, the main screen will handle it.
			break;
		}
		}
	}

public:
	static void showTransactionsMenu() {
		if (!checkAccessRights(User::enPermissions::transactions))
			return;// this will exit the function and it will not continue

		system("cls");
		_drawScreenHeader("\t Transactions Screen");

		cout << setw(37) << left << "" << "===========================================\n";
		cout << setw(37) << left << "" << "\t\t  Transactions Menu\n";
		cout << setw(37) << left << "" << "===========================================\n";
		cout << setw(37) << left << "" << "\t[1] Deposit.\n";
		cout << setw(37) << left << "" << "\t[2] Withdraw.\n";
		cout << setw(37) << left << "" << "\t[3] Transfer.\n";
		cout << setw(37) << left << "" << "\t[4] Transfer Log.\n";
		cout << setw(37) << left << "" << "\t[5] Total Balances.\n";
		cout << setw(37) << left << "" << "\t[6] Main Menu.\n";
		cout << setw(37) << left << "" << "===========================================\n";

		_performTransactionsMenuOption((enTransactionsMenuOptions)_readTransactionsMenuOption());
	}
};