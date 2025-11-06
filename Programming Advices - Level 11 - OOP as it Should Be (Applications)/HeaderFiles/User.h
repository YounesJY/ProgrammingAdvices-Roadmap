#pragma once
#include <iostream>
#include <fstream>
#include <string>
#include <vector>

#include "Utility.h"
#include "Person.h"
#include "String.h"
#include "Date.h"

using namespace std;

class User : public Person {
private:
	enum enMode { EmptyMode = 0, UpdateMode = 1, AddNewMode = 2 };

	static const short ENCRYPTION_DECRYPTION_KEY = 9;

	enMode _mode;
	string _userName;
	string _password;
	int _permissions;
	bool _markedForDelete = false;

	static User _convertLineToUserObject(string line, string seperator = "#//#") {
		vector<string> vUserData = String::split(line, seperator);
		return User(enMode::UpdateMode, vUserData[0], vUserData[1], vUserData[2], vUserData[3], vUserData[4], Utility::decryptText(vUserData[5], ENCRYPTION_DECRYPTION_KEY), stoi(vUserData[6]));
	}

	static string _convertUserObjectToLine(User user, string seperator = "#//#") {
		string userRecord = "";
		userRecord += user.firstName + seperator;
		userRecord += user.lastName + seperator;
		userRecord += user.email + seperator;
		userRecord += user.phone + seperator;
		userRecord += user.userName + seperator;
		userRecord += Utility::encryptText(user.password, ENCRYPTION_DECRYPTION_KEY) + seperator;
		userRecord += to_string(user.permissions);
		return userRecord;
	}


	static vector<User> _loadUsersDataFromFile() {
		vector<User> vUsers;
		fstream myFile;
		myFile.open("Users.txt", ios::in); // read mode

		if (myFile.is_open()) {
			string line;
			while (getline(myFile, line)) {
				User user = _convertLineToUserObject(line);
				vUsers.push_back(user);
			}
			myFile.close();
		}
		return vUsers;
	}

	static void _saveUsersDataToFile(vector<User> vUsers) {
		fstream myFile;
		myFile.open("Users.txt", ios::out); // overwrite

		if (myFile.is_open()) {
			for (User user : vUsers) {
				if (!user.markedForDeleted()) {
					// We only write records that are not marked for delete.
					string dataLine = _convertUserObjectToLine(user);
					myFile << dataLine << endl;
				}
			}
			myFile.close();
		}
	}


	void _update() {
		vector<User> vUsers = _loadUsersDataFromFile();
		for (User& user : vUsers) {
			if (user.userName == userName) {
				user = *this;
				break;
			}
		}
		_saveUsersDataToFile(vUsers);
	}

	void _addNew() {
		_addDataLineToFile(_convertUserObjectToLine(*this));
	}


	void _addDataLineToFile(string stDataLine) {
		fstream myFile;
		myFile.open("Users.txt", ios::out | ios::app);

		if (myFile.is_open()) {
			myFile << stDataLine << endl;
			myFile.close();
		}
	}

	static User _getEmptyUserObject() {
		return User(enMode::EmptyMode, "", "", "", "", "", "", 0);
	}

public:
	struct LoginRecord {
		string dateTime;
		string username;
		string password;
		short permissions = 0;
	};

	enum enSaveResults { svFaildEmptyObject = 0, svSucceeded = 1, svFaildUserExists = 2 };

	enum enPermissions {
		all = -1, listClients = 1, addNewClient = 2, deleteClient = 4,
		updateClients = 8, findClient = 16, transactions = 32, manageUsers = 64,
		loginRegister = 128
	};

	User(enMode mode, string firstName, string lastName, string email, string phone, string userName, string password, int permissions) :
		Person(firstName, lastName, email, phone) {
		_mode = mode;
		_userName = userName;
		_password = password;
		_permissions = permissions;
	}

	bool isEmpty() {
		return (_mode == enMode::EmptyMode);
	}

	bool markedForDeleted() {
		return _markedForDelete;
	}

	string getUserName() {
		return _userName;
	}

	void setUserName(string userName) {
		_userName = userName;
	}

	__declspec(property(get = getUserName, put = setUserName)) string userName;


	void setPassword(string password) {
		_password = password;
	}

	string getPassword() {
		return _password;
	}

	__declspec(property(get = getPassword, put = setPassword)) string password;


	void setPermissions(int permissions) {
		_permissions = permissions;
	}

	int getPermissions() {
		return _permissions;
	}

	__declspec(property(get = getPermissions, put = setPermissions)) int permissions;


	static User find(string userName) {
		fstream myFile;
		myFile.open("Users.txt", ios::in); // read mode

		if (myFile.is_open()) {
			string line;
			while (getline(myFile, line)) {
				User user = _convertLineToUserObject(line);
				if (user.userName == userName) {
					myFile.close();
					return user;
				}
			}
			myFile.close();
		}
		return _getEmptyUserObject();
	}

	static User find(string userName, string password) {
		fstream myFile;
		myFile.open("Users.txt", ios::in); // read mode

		if (myFile.is_open()) {
			string line;
			while (getline(myFile, line)) {
				User user = _convertLineToUserObject(line);
				if (user.userName == userName && user.password == password) {
					myFile.close();
					return user;
				}
			}
			myFile.close();
		}
		return _getEmptyUserObject();
	}


	enSaveResults save() {
		switch (_mode) {
		case enMode::EmptyMode:
			return enSaveResults::svFaildEmptyObject;
		case enMode::UpdateMode: {
			_update();
			return enSaveResults::svSucceeded;
			break;
		}
		case enMode::AddNewMode: {
			// This will add a new record to the file or database.
			if (User::isUserExist(_userName)) {
				return enSaveResults::svFaildUserExists;
			}
			else {
				_addNew();
				// We need to set the mode to update after adding a new record.
				_mode = enMode::UpdateMode;
				return enSaveResults::svSucceeded;
			}
			break;
		}
		}
	}

	static bool isUserExist(string userName) {
		User user = User::find(userName);
		return (!user.isEmpty());
	}

	bool deleteUser() {
		vector<User> vUsers = _loadUsersDataFromFile();
		for (User& u : vUsers) {
			if (u.userName == _userName) {
				u._markedForDelete = true;
				break;
			}
		}
		_saveUsersDataToFile(vUsers);
		*this = _getEmptyUserObject();
		return true;
	}


	static User getAddNewUserObject(string userName) {
		return User(enMode::AddNewMode, "", "", "", "", userName, "", 0);
	}

	static vector<User> getUsersList() {
		return _loadUsersDataFromFile();
	}

	bool checkAccessPermission(enPermissions permission) {
		if (this->permissions == enPermissions::all)
			return true;

		if ((this->permissions & permission) == permission)
			return true;

		return false;
	}

private:
	string _prepareLoginRecord(string seperator = "#//#") {
		string userRecord = "";

		userRecord += Date::GetSystemDateTimeString() + seperator;
		userRecord += this->userName + seperator;
		userRecord += Utility::encryptText(this->password, ENCRYPTION_DECRYPTION_KEY) + seperator;
		userRecord += to_string(this->permissions);

		return userRecord;
	}

	static vector<string> _loadLoginRecordsFromFile() {
		vector<string> vLoginRecords;
		fstream myFile;

		myFile.open("../LoginRegister.txt", ios::in); // read mode

		if (myFile.is_open()) {
			string loginRecord;
			while (getline(myFile, loginRecord))
				vLoginRecords.push_back(loginRecord);
			myFile.close();
		}

		return vLoginRecords;
	}

	static LoginRecord _convertLoginRecordToObject(string record, string separator = "#//#") {
		struct  LoginRecord loginRecord;
		vector<string> vloginRecordFields = String::split(record, separator);

		loginRecord.dateTime = vloginRecordFields[0];
		loginRecord.username = vloginRecordFields[1];
		loginRecord.password = Utility::decryptText(vloginRecordFields[2], ENCRYPTION_DECRYPTION_KEY);
		loginRecord.permissions = stoi(vloginRecordFields[3]);

		return loginRecord;
	}


public:
	void registerLogin() {
		fstream myFile;
		vector<string> vLoginRecords = _loadLoginRecordsFromFile();

		myFile.open("../LoginRegister.txt", ios::out); // write mode

		if (myFile.is_open()) {
			myFile << _prepareLoginRecord() << endl;
			for (string record : vLoginRecords)
				myFile << record << endl;
			myFile.close();
		}
	}

	static vector<LoginRecord> getLoginRegisterList() {
		vector<LoginRecord> vLoginRecordsObjects;
		fstream myFile;
		string loginRecord;

		myFile.open("../LoginRegister.txt", ios::in); // read mode

		if (myFile.is_open()) {
			while (getline(myFile, loginRecord))
				vLoginRecordsObjects.push_back(_convertLoginRecordToObject(loginRecord));
			myFile.close();
		}

		return vLoginRecordsObjects;
	}
};