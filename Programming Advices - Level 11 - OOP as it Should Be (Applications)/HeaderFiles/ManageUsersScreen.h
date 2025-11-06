#pragma once
#include <iostream>
#include <iomanip>
#include "Screen.h"
#include "InputValidate.h"

#include "UsersListScreen.h"
#include "AddNewUserScreen.h"
#include "DeleteUserScreen.h"
#include "UpdateUserScreen.h"
#include "FindUserScreen.h"

using namespace std;

class ManageUsersScreen : protected Screen {
private:
	enum enManageUsersMenuOptions {
		listUsers = 1, addNewUser = 2, deleteUser = 3,
		updateUser = 4, findUser = 5, mainMenu = 6
	};

	static void _goBackToManageUsersMenu() {
		cout << "\n\nPress any key to go back to Manage Users Menu...";
		system("pause>0");
		showManageUsersMenu();
	}


	static void _showListUsersScreen() {
		//cout << "\nList Users Screen Will Be Here.\n";
		UsersListScreen::showUsersList();
	}

	static void _showAddNewUserScreen() {
		// cout << "\nAdd New User Screen Will Be Here.\n";
		AddNewUserScreen::showAddNewUserScreen();
	}

	static void _showDeleteUserScreen() {
		// cout << "\nDelete User Screen Will Be Here.\n";
		DeleteUserScreen::showDeleteUserScreen();
	}

	static void _showUpdateUserScreen() {
		// cout << "\nUpdate User Screen Will Be Here.\n";
		UpdateUserScreen::showUpdateUserScreen();
	}

	static void _showFindUserScreen() {
		//cout << "\nFind User Screen Will Be Here.\n";
		FindUserScreen::showFindUserScreen();
	}


	static short _readManageUsersMenuOption() {
		cout << setw(37) << left << "" << "Choose what do you want to do? [1 to 6]? ";
		short choice = InputValidate::readNumberBetween<short>(1, 6, "Enter Number between 1 to 6? ");
		return choice;
	}

	static void _performManageUsersMenuOption(enManageUsersMenuOptions manageUsersMenuOption) {
		system("cls");

		switch (manageUsersMenuOption) {
		case enManageUsersMenuOptions::listUsers: {
			_showListUsersScreen();
			_goBackToManageUsersMenu();
			break;
		}
		case enManageUsersMenuOptions::addNewUser: {
			_showAddNewUserScreen();
			_goBackToManageUsersMenu();
			break;
		}
		case enManageUsersMenuOptions::deleteUser: {
			_showDeleteUserScreen();
			_goBackToManageUsersMenu();
			break;
		}
		case enManageUsersMenuOptions::updateUser: {
			_showUpdateUserScreen();
			_goBackToManageUsersMenu();
			break;
		}
		case enManageUsersMenuOptions::findUser: {
			_showFindUserScreen();
			_goBackToManageUsersMenu();
			break;
		}
		case enManageUsersMenuOptions::mainMenu:
			// Do nothing here, the main screen will handle it.
			break;
		}
	}

public:
	static void showManageUsersMenu() {
		if (!checkAccessRights(User::enPermissions::manageUsers))
			return;// this will exit the function and it will not continue

		system("cls");
		_drawScreenHeader("\t Manage Users Screen");

		cout << setw(37) << left << "" << "===========================================\n";
		cout << setw(37) << left << "" << "\t\t  Manage Users Menu\n";
		cout << setw(37) << left << "" << "===========================================\n";
		cout << setw(37) << left << "" << "\t[1] List Users.\n";
		cout << setw(37) << left << "" << "\t[2] Add New User.\n";
		cout << setw(37) << left << "" << "\t[3] Delete User.\n";
		cout << setw(37) << left << "" << "\t[4] Update User.\n";
		cout << setw(37) << left << "" << "\t[5] Find User.\n";
		cout << setw(37) << left << "" << "\t[6] Main Menu.\n";
		cout << setw(37) << left << "" << "===========================================\n";

		_performManageUsersMenuOption((enManageUsersMenuOptions)_readManageUsersMenuOption());
	}
};

/*
	class ManageUsersScreen : protected Screen {
private:
	enum enManageUsersMenuOptions {
		listUsers = 1, addNewUser = 2, deleteUser = 3,
		updateUser = 4, findUser = 5, mainMenu = 6
	};

	static short readManageUsersMenuOption() {
		cout << setw(37) << left << "" << "Choose what do you want to do? [1 to 6]? ";
		short choice = InputValidate::readShortNumberBetween(1, 6, "Enter Number between 1 to 6? ");
		return choice;
	}

	static void _showListUsersScreen() {
		cout << "\nList Users Screen Will Be Here.\n";
	}

	static void _showAddNewUserScreen() {
		cout << "\nAdd New User Screen Will Be Here.\n";
	}

	static void _showDeleteUserScreen() {
		cout << "\nDelete User Screen Will Be Here.\n";
	}

	static void _showUpdateUserScreen() {
		cout << "\nUpdate User Screen Will Be Here.\n";
	}

	static void _showFindUserScreen() {
		cout << "\nFind User Screen Will Be Here.\n";
	}

	static void performManageUsersMenuOption(enManageUsersMenuOptions manageUsersMenuOption) {
		switch (manageUsersMenuOption) {
			case enManageUsersMenuOptions::eListUsers: {
				system("cls");
				_showListUsersScreen();
				break;
			}
			case enManageUsersMenuOptions::eAddNewUser: {
				system("cls");
				_showAddNewUserScreen();
				break;
			}
			case enManageUsersMenuOptions::eDeleteUser: {
				system("cls");
				_showDeleteUserScreen();
				break;
			}
			case enManageUsersMenuOptions::eUpdateUser: {
				system("cls");
				_showUpdateUserScreen();
				break;
			}
			case enManageUsersMenuOptions::eFindUser: {
				system("cls");
				_showFindUserScreen();
				break;
			}
			case enManageUsersMenuOptions::eExit: {
				// Exit the Manage Users menu.
				return;
			}
		}
	}

public:
	static void showManageUsersMenu() {
		while (true) { // Use a loop to keep the menu running
			system("cls");
			drawScreenHeader("\t Manage Users Screen");

			cout << setw(37) << left << "" << "===========================================\n";
			cout << setw(37) << left << "" << "\t\t  Manage Users Menu\n";
			cout << setw(37) << left << "" << "===========================================\n";
			cout << setw(37) << left << "" << "\t[1] List Users.\n";
			cout << setw(37) << left << "" << "\t[2] Add New User.\n";
			cout << setw(37) << left << "" << "\t[3] Delete User.\n";
			cout << setw(37) << left << "" << "\t[4] Update User.\n";
			cout << setw(37) << left << "" << "\t[5] Find User.\n";
			cout << setw(37) << left << "" << "\t[6] Exit.\n"; // Renamed to Exit for clarity
			cout << setw(37) << left << "" << "===========================================\n";

			enManageUsersMenuOptions option = (enManageUsersMenuOptions)readManageUsersMenuOption();
			performManageUsersMenuOption(option);

			if (option == enManageUsersMenuOptions::eExit) {
				break; // Exit the loop and return to the caller.
			}

			cout << "\n\nPress any key to go back to Manage Users Menu...";
			system("pause>0");
		}
	}
};
*/