#pragma once

#include <iostream>
#include <iomanip>
#include "Screen.h"
#include "User.h"
#include "InputValidate.h"

class AddNewUserScreen : protected Screen {
private:
	static int _readPermissionsToSet() {
		int permissions = 0;
		char answer = 'n';

		answer = Input::getChoice("\nDo you want to give full access? (yes / no) ? ", false);
		if (answer == Input::choice::yes)
			return -1;

		cout << "\nDo you want to give access to : " << endl;

		answer = Input::getChoice("\nShow Client List? (yes / no) ? ", false);
		if (answer == Input::choice::yes)
			permissions += User::enPermissions::listClients;

		answer = Input::getChoice("\nAdd New Client? (yes / no) ? ", false);
		if (answer == Input::choice::yes)
			permissions += User::enPermissions::addNewClient;

		answer = Input::getChoice("\nDelete Client? (yes / no) ? ", false);
		if (answer == Input::choice::yes)
			permissions += User::enPermissions::deleteClient;

		answer = Input::getChoice("\nUpdate Client? (yes / no) ? ", false);
		if (answer == Input::choice::yes)
			permissions += User::enPermissions::updateClients;


		answer = Input::getChoice("\nFind Client? (yes / no) ? ", false);
		if (answer == Input::choice::yes)
			permissions += User::enPermissions::findClient;

		answer = Input::getChoice("\nTransactions? (yes / no) ? ", false);
		if (answer == Input::choice::yes)
			permissions += User::enPermissions::transactions;


		answer = Input::getChoice("\nManage Users? (yes / no) ?  ", false);
		if (answer == Input::choice::yes)
			permissions += User::enPermissions::manageUsers;

		answer = Input::getChoice("\nLogin Register? (yes / no) ?  ", false);
		if (answer == Input::choice::yes)
			permissions += User::enPermissions::loginRegister;

		return permissions;
	}

	static void _readUserInfo(User& User) {
		User.firstName = InputValidate::readString("\nEnter FirstName: ");
		User.lastName = InputValidate::readString("\nEnter LastName: ");
		User.email = InputValidate::readString("\nEnter Email: ");
		User.phone = InputValidate::readString("\nEnter Phone: ");
		User.password = InputValidate::readString("\nEnter Password: ");
		User.permissions = _readPermissionsToSet();
	}

	static void _printUser(User User) {
		cout << "\nUser Card:";
		cout << "\n___________________";
		cout << "\nFirstName   : " << User.firstName;
		cout << "\nLastName    : " << User.lastName;
		cout << "\nFull Name   : " << User.fullName();
		cout << "\nEmail       : " << User.email;
		cout << "\nPhone       : " << User.phone;
		cout << "\nUser Name   : " << User.userName;
		cout << "\nPassword    : " << User.password;
		cout << "\nPermissions : " << User.permissions;
		cout << "\n___________________\n";
	}

public:
	static void showAddNewUserScreen() {
		string userName = "";

		_drawScreenHeader("\t  Add New User Screen");
		userName = InputValidate::readString("\nPlease Enter UserName: ");

		while (User::isUserExist(userName))
			userName = InputValidate::readString("\nUserName Is Already Used, Choose another one: ");

		User newUser = User::getAddNewUserObject(userName);

		_readUserInfo(newUser);

		User::enSaveResults saveResult = newUser.save();

		switch (saveResult) {
		case  User::enSaveResults::svSucceeded: {
			cout << "\nUser Addeded Successfully :-)\n";
			_printUser(newUser);
			break;
		}
		case User::enSaveResults::svFaildEmptyObject: {
			cout << "\nError User was not saved because it's Empty";
			break;
		}
		case User::enSaveResults::svFaildUserExists: {
			cout << "\nError User was not saved because UserName is used!\n";
			break;
		}
		}
	}
};

