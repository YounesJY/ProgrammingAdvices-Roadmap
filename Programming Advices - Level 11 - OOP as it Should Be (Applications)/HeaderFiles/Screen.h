#pragma once

#include <iostream>
#include "Global.h"
#include "Date.h"

using namespace std;

class Screen {

private:


protected:
	static void _drawScreenHeader(string title, string subTitle = "") {

		//cout << "\t\t\t\t\t\t\t\t\t\t" << "=========================" << endl;
		//cout << "\t\t\t\t\t\t\t\t\t\t" << "User : " << currentUser.fullName() << endl;
		//cout << "\t\t\t\t\t\t\t\t\t\t" << "Date : " << Date::dateToString(Date::getSystemDate()) << endl;
		//cout << "\t\t\t\t\t\t\t\t\t\t" << "=========================" << endl;

		cout << setw(80) << left << "" << "=========================" << endl;;
		cout << setw(80) << left << "" << "User : " << currentUser.fullName() << endl;;
		cout << setw(80) << left << "" << "Date : " << Date::dateToString(Date::getSystemDate()) << endl;;
		cout << setw(80) << left << "" << "=========================" << endl;;


					
		cout << "\t\t\t\t\t______________________________________";
		cout << "\n\n\t\t\t\t\t  " << title;
		if (!subTitle.empty())
			cout << "\n\t\t\t\t\t  " << subTitle;
		cout << "\n\t\t\t\t\t______________________________________\n\n";

	}

	static bool checkAccessRights(User::enPermissions permission) {

		if (!currentUser.checkAccessPermission(permission)) {
			cout << "\t\t\t\t\t______________________________________";
			cout << "\n\n\t\t\t\t\t  Access Denied! Contact your Admin.";
			cout << "\n\t\t\t\t\t______________________________________\n\n";
			return false;
		}
		else
			return true;

	}

};