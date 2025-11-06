#include <iostream>
#include "../../HeaderFiles/BankClient.h"
#include "../../HeaderFiles/InputValidate.h"

void readClientInfo(BankClient& client) {
	cout << "\nEnter FirstName: ";
	client.firstName = InputValidate::readString();

	cout << "\nEnter LastName: ";
	client.lastName = InputValidate::readString();

	cout << "\nEnter Email: ";
	client.email = InputValidate::readString();

	cout << "\nEnter Phone: ";
	client.phone = InputValidate::readString();

	cout << "\nEnter PinCode: ";
	client.pinCode = InputValidate::readString();

	cout << "\nEnter Account Balance: ";
	client.accountBalance = InputValidate::readFloatNumber();
}

void updateClient() {
	string accountNumber = "";

	cout << "\nPlease Enter client Account Number: ";
	accountNumber = InputValidate::readString();

	while (!BankClient::isClientExist(accountNumber)) {
		cout << "\nAccount number is not found, choose another one: ";
		accountNumber = InputValidate::readString();
	}

	BankClient client = BankClient::find(accountNumber);
	client.print();

	cout << "\n\nUpdate Client Info:";
	cout << "\n____________________\n";

	readClientInfo(client);

	BankClient::enSaveResults saveResult = client.save();

	switch (saveResult) {
		case BankClient::enSaveResults::svSucceeded: {
			cout << "\nAccount Updated Successfully :-)\n";
			client.print();
			break;
		}
		case BankClient::enSaveResults::svFaildEmptyObject: {
			cout << "\nError account was not saved because it's Empty";
			break;
		}
	}
}

int main() {
	updateClient();
	system("pause>0");
	return 0;
}