#pragma once
#include <iostream>

#include "Screen.h"
#include "Person.h"
#include "User.h"
#include "BankClient.h"
#include "InputValidate.h"

class LoginRegisterScreen :protected Screen {
private:
private:

	static void _printLoginRecordLine(User::LoginRecord loginRecord) {
		cout << setw(8) << left << "";
		cout << "| " << setw(25) << left << loginRecord.dateTime;
		cout << "| " << setw(25) << left << loginRecord.username;
		cout << "| " << setw(25) << left << loginRecord.password;
		cout << "| " << setw(25) << left << loginRecord.permissions;
	}

public:
	static void showLoginRegisterList() {
		if (!checkAccessRights(User::enPermissions::loginRegister))
			return;// this will exit the function and it will not continue


		vector<User::LoginRecord> vLoginRegister = User::getLoginRegisterList();
		string title = "\t  Login Register List Screen";
		string subTitle = "\t    (" + to_string(vLoginRegister.size()) + ") Records(s).";

		_drawScreenHeader(title, subTitle);

		cout << setw(8) << left << "";
		cout << "\n\t________________________________________________________________________________________________\n" << endl;
		cout << setw(8) << left << "";
		cout << "| " << left << setw(25) << "Date/Time";
		cout << "| " << left << setw(25) << "Username";
		cout << "| " << left << setw(25) << "Password";
		cout << "| " << left << setw(25) << "Permissions";
		cout << setw(8) << left << "";
		cout << "\n\t________________________________________________________________________________________________\n" << endl;

		if (vLoginRegister.size() == 0)
			cout << "\t\t\t\tNo Records Available In the System!";
		else {
			for (User::LoginRecord record : vLoginRegister) {
				_printLoginRecordLine(record);
				cout << endl;
			}
		}

		cout << setw(8) << left << "";
		cout << "\n\t________________________________________________________________________________________________\n" << endl;
	}

};

