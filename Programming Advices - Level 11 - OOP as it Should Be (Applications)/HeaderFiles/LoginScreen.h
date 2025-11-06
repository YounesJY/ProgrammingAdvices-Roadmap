#pragma once

#include <iostream>
#include <iomanip>

#include "Screen.h"
#include "User.h"
#include "MainScreen.h"
#include "Global.h"
#include "InputValidate.h"

class LoginScreen : protected Screen {
private:
	static bool _login() {
		string username, password;
		bool loginFailed = false;
		short trialsToLogin = 3;

		do {
			if (loginFailed) {
				--trialsToLogin;

				cout << "\nInvlaid Username/Password!";
				cout << "\nYou have " << trialsToLogin << " trail(s) to login!\n\n";

				if (trialsToLogin == 0) {
					cout << "\n\tYou're locked after 3 failed trails! \n\n";
					return false;

					// or 
					// exit(0) --> No need to make these functions returning bool
				}
			}

			username = InputValidate::readString("Enter Username? ");
			password = InputValidate::readString("Enter Password? ");

			currentUser = User::find(username, password);
			loginFailed = currentUser.isEmpty();
		} while (loginFailed);

		currentUser.registerLogin();

		MainScreen::showMainMenu();

		return true;
	}

public:
	static bool showLoginScreen() {
		system("cls");
		_drawScreenHeader("\t  Login Screen");
		return _login();
	}
};