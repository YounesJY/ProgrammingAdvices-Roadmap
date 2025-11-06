#pragma once
#include <iostream>
#include "Screen.h"
#include "Person.h"
#include "User.h"
#include "Global.h"
#include "InputValidate.h"

class UpdateUserScreen :protected Screen {
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

	static void _readUserInfo(User& user) {
		user.firstName = InputValidate::readString("\nEnter FirstName: ");
		user.lastName = InputValidate::readString("\nEnter LastName: ");
		user.email = InputValidate::readString("\nEnter Email: ");
		user.phone = InputValidate::readString("\nEnter Phone: ");
		user.password = InputValidate::readString("\nEnter Password: ");
		user.permissions = _readPermissionsToSet();
	}

	static void _printUser(User user) {
		cout << "\nUser Card:";
		cout << "\n___________________";
		cout << "\nFirstName   : " << user.firstName;
		cout << "\nLastName    : " << user.lastName;
		cout << "\nFull Name   : " << user.fullName();
		cout << "\nEmail       : " << user.email;
		cout << "\nPhone       : " << user.phone;
		cout << "\nUser Name   : " << user.userName;
		cout << "\nPassword    : " << user.password;
		cout << "\nPermissions : " << user.permissions;
		cout << "\n___________________\n";
	}

public:

	static void showUpdateUserScreen() {
		string userName = "";
		Input::choice answer;

		_drawScreenHeader("\tUpdate User Screen");
		userName = InputValidate::readString("\nPlease Enter UserName: ");

		while (!User::isUserExist(userName))
			userName = InputValidate::readString("\n UserName number is not found, choose another one: ");

		User user = User::find(userName);
		_printUser(user);

		answer = Input::getChoice(" \nAre you sure you want to update this User (yes / no) ? ", false);

		if (answer == Input::choice::yes) {
			cout << "\n\nUpdate User Info:";
			cout << "\n____________________\n";
			_readUserInfo(user);
			cout << "\n____________________\n";

			User::enSaveResults saveResult = user.save();

			switch (saveResult) {
			case  User::enSaveResults::svSucceeded: {
				cout << "\nUser Updated Successfully :-)\n";
				_printUser(user);
				
				// if any user updates its permissions, the action should take place immediatly
				// or prevent any user from update it self ? well, this's the most logical solution
				if (currentUser.userName == user.userName)
				{
					currentUser.permissions = user.permissions;
					cout << " \n[Your account permissions has been updated Successfully]\n";

				}

				break;
			}
			case User::enSaveResults::svFaildEmptyObject: {
				cout << "\nError User was not saved because it's Empty";
				break;
			}
			}
		}
	}
};

