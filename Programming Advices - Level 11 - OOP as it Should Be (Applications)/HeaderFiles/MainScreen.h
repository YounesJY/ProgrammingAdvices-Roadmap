#pragma once
#include <iostream>
#include <iomanip>
#include "InputValidate.h"

#include "Screen.h"
#include "ClientListScreen.h"
#include "AddNewClientScreen.h"
#include "DeleteClientScreen.h"
#include "UpdateClientScreen.h"
#include "FindClientScreen.h"
#include "TransactionsScreen.h"
#include "ManageUsersScreen.h"
#include "LoginRegisterScreen.h"
#include "CurrencyExchangeScreen.h"

#include "Global.h"

using namespace std;

class MainScreen : protected Screen {
private:
	enum enMainMenuOptions {
		listClients = 1, addNewClient = 2, deleteClient = 3, updateClient = 4, findClient = 5,
		showTransactionsMenu = 6, manageUsers = 7, loginRegister = 8, currencyExchange = 9, exit = 10
	};

	static short _readMainMenuOption() {
		cout << setw(37) << left << "" << "Choose what do you want to do? [1 to 10]? ";
		return InputValidate::readNumberBetween<short>(1, 10, "Enter Number between 1 to 10? ");
	}

	static void _goBackToMainMenu() {
		cout << setw(37) << left << "" << "\n\tPress any key to go back to Main Menu...\n";
		system("pause>0");
		showMainMenu();
	}


	static void _showAllClientsScreen() {
		// cout << "\nClient List Screen Will be here...\n";
		ClientListScreen::showClientsList();
	}

	static void _showAddNewClientsScreen() {
		// cout << "\nAdd New Client Screen Will be here...\n";
		AddNewClientScreen::showAddNewClientScreen();
	}

	static void _showDeleteClientScreen() {
		// cout << "\nDelete Client Screen Will be here...\n";
		DeleteClientScreen::showDeleteClientScreen();
	}

	static void _showUpdateClientScreen() {
		// cout << "\nUpdate Client Screen Will be here...\n";
		UpdateClientScreen::showUpdateClientScreen();
	}

	static void _showFindClientScreen() {
		// cout << "\nFind Client Screen Will be here...\n";
		FindClientScreen::showFindClientScreen();
	}

	static void _showTransactionsMenu() {
		// cout << "\nTransactions Menu Will be here...\n";
		TransactionsScreen::showTransactionsMenu();
	}

	static void _showManageUsersMenu() {
		// cout << "\nUsers Menue Will be here...\n";
		ManageUsersScreen::showManageUsersMenu();
	}

	static void _showLoginRegisterScreen() {
		LoginRegisterScreen::showLoginRegisterList();
	}
	static void _showCurrencyExchangeScreen() {
		CurrencyExchangeScreen::showCurrencyExchangeMenu();
	}

	// This screen has been replaced by _logout() 
	//static void _showEndScreen() {
	//	cout << "\nEnd Screen Will be here...\n";
	//}

	static void _logout() {
		// 1. Clear the current user session
		currentUser = User::find("", ""); // Resets to an empty/invalid user

		/*
			// ===== ! NEVER DO THIS! =====
			LoginScreen::showLoginScreen();
		 *
		 * ===== IMPORTANT NOTES =====
		 * 1. **Why Not Call LoginScreen Directly?**
		 *    - Avoids a circular dependency:
		 *      LoginScreen → MainMenu → Logout → LoginScreen → ...
		 *    - Prevents stack overflow from infinite recursion.
		 *
		 * 2. **Flow Control Responsibility**
		 *    - Let main() handle re-entry to LoginScreen. This ensures:
		 *      - Clean program state reset.
		 *      - No leftover stack frames from nested calls.
		 *
		 * 3. **Alternatives (If You Must Stay in the Same Context)**
		 *    - Use a loop in main() (recommended):
		 *
		 *      while (true) { LoginScreen::showLoginScreen(); }
		 *
		 *    - Throw a "logout" exception (advanced, but overkill here).
		 *
		 * 4. **Security Considerations**
		 *    - Always invalidate currentUser before logout.
		 *    - If using passwords/tokens, explicitly clear them too.
		 *
		 * 5. **Debugging Tip**
		 *    - Log the logout event (e.g., cout << "User logged out.";)
		 *      for auditing.
		 */

	}


	static void _performMainMenuOption(enMainMenuOptions mainMenuOption) {
		system("cls");

		switch (mainMenuOption) {
		case enMainMenuOptions::listClients:
			_showAllClientsScreen();
			_goBackToMainMenu();
			break;
		case enMainMenuOptions::addNewClient:
			_showAddNewClientsScreen();
			_goBackToMainMenu();
			break;
		case enMainMenuOptions::deleteClient:
			_showDeleteClientScreen();
			_goBackToMainMenu();
			break;
		case enMainMenuOptions::updateClient:
			_showUpdateClientScreen();
			_goBackToMainMenu();
			break;
		case enMainMenuOptions::findClient:
			_showFindClientScreen();
			_goBackToMainMenu();
			break;
		case enMainMenuOptions::showTransactionsMenu:
			_showTransactionsMenu();
			_goBackToMainMenu();
			break;
		case enMainMenuOptions::manageUsers:
			_showManageUsersMenu();
			_goBackToMainMenu();
			break;
		case enMainMenuOptions::loginRegister:
			_showLoginRegisterScreen();
			_goBackToMainMenu();
			break;
		case enMainMenuOptions::currencyExchange:
			_showCurrencyExchangeScreen();
			_goBackToMainMenu();
			break;
		case enMainMenuOptions::exit:
			_logout();
			break;
		}
	}

public:
	static void showMainMenu() {
		system("cls");
		_drawScreenHeader("\t\tMain Screen");

		cout << setw(37) << left << "" << "===========================================\n";
		cout << setw(37) << left << "" << "\t\t\tMain Menu\n";
		cout << setw(37) << left << "" << "===========================================\n";
		cout << setw(37) << left << "" << "\t[1] Show Client List.\n";
		cout << setw(37) << left << "" << "\t[2] Add New Client.\n";
		cout << setw(37) << left << "" << "\t[3] Delete Client.\n";
		cout << setw(37) << left << "" << "\t[4] Update Client Info.\n";
		cout << setw(37) << left << "" << "\t[5] Find Client.\n";
		cout << setw(37) << left << "" << "\t[6] Transactions.\n";
		cout << setw(37) << left << "" << "\t[7] Manage Users.\n";
		cout << setw(37) << left << "" << "\t[8] Login Register.\n";
		cout << setw(37) << left << "" << "\t[9] Currency Exchange.\n";
		cout << setw(37) << left << "" << "\t[10] Logout.\n";
		cout << setw(37) << left << "" << "===========================================\n";

		_performMainMenuOption((enMainMenuOptions)_readMainMenuOption());
	}
};