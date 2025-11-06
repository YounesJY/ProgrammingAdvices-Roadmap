#pragma once
#include <iostream>
#include "Screen.h"
#include "Person.h"
#include "User.h"
#include "InputValidate.h"

class DeleteUserScreen :protected Screen {
private:
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
	static void showDeleteUserScreen() {
		string userName = "";
		Input::choice answer;

		_drawScreenHeader("\tDelete User Screen");

		userName = InputValidate::readString("\nPlease Enter UserName: ");
		while (!User::isUserExist(userName))
			userName = InputValidate::readString("\nUser is not found, choose another one: ");

		User user = User::find(userName);
		_printUser(user);

		answer = Input::getChoice(" \nAre you sure you want to delete this User (yes / no) ? ", false);
		if (answer == Input::choice::yes) {
			if (user.deleteUser()) {
				cout << "\nUser Deleted Successfully :-)\n";
				_printUser(user);
			}
			else
				cout << "\nError User Was not Deleted\n";
		}
	}

};

