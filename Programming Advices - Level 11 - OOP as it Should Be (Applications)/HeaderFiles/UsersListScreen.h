#pragma once

#include <iostream>
#include <iomanip>
#include "Screen.h"
#include "User.h"

class UsersListScreen : protected Screen {
private:
	static void _printUserRecordLine(User User) {
		cout << setw(8) << left << "";
		cout << "| " << setw(12) << left << User.userName;
		cout << "| " << setw(25) << left << User.fullName();
		cout << "| " << setw(12) << left << User.phone;
		cout << "| " << setw(20) << left << User.email;
		cout << "| " << setw(10) << left << User.password;
		cout << "| " << setw(12) << left << User.permissions;
	}

public:
	static void showUsersList() {
		vector <User> vUsers = User::getUsersList();

		string Title = "\t  User List Screen";
		string SubTitle = "\t    (" + to_string(vUsers.size()) + ") User(s).";

		_drawScreenHeader(Title, SubTitle);

		cout << setw(8) << left << "";
		cout << "\n\t_____________________________________________________________________________________________________\n" << endl;

		cout << setw(8) << left << "";
		cout << "| " << left << setw(12) << "UserName";
		cout << "| " << left << setw(25) << "Full Name";
		cout << "| " << left << setw(12) << "Phone";
		cout << "| " << left << setw(20) << "Email";
		cout << "| " << left << setw(10) << "Password";
		cout << "| " << left << setw(12) << "Permissions";
		cout << setw(8) << left << "";
		cout << "\n\t_____________________________________________________________________________________________________\n" << endl;

		if (vUsers.size() == 0)
			cout << "\t\t\t\tNo Users Available In the System!";
		else {
			for (const User& User : vUsers) {
				_printUserRecordLine(User);
				cout << endl;
			}
		}

		cout << setw(8) << left << "";
		cout << "\n\t_____________________________________________________________________________________________________\n" << endl;
	}
};

