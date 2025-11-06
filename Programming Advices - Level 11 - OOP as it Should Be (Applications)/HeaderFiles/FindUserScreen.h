#pragma once
#include <iostream>
#include "Screen.h"
#include "Person.h"
#include "User.h"
#include "InputValidate.h"

class FindUserScreen :protected Screen {
private:
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
	static void showFindUserScreen() {
		string userName = "";

		_drawScreenHeader("\t  Find User Screen");
		userName = InputValidate::readString("\nPlease Enter UserName: ");

		while (!User::isUserExist(userName))
			userName = InputValidate::readString("\nUser is not found, choose another one: ");

		User user = User::find(userName);

		if (!user.isEmpty())
			cout << "\nUser Found :-)\n";
		else
			cout << "\nUser Was not Found :-(\n";

		_printUser(user);
	}
};

