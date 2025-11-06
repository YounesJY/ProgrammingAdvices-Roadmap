#include <iostream>
#include "../../HeaderFiles/BankClient.h"
#include "../../HeaderFiles/InputValidate.h"

void readClientInfo(BankClient& client) {
	cout << "\nEnter FirstName: "; client.firstName = InputValidate::readString();
	cout << "\nEnter LastName: "; client.lastName = InputValidate::readString();
	cout << "\nEnter Email: "; client.email = InputValidate::readString();
	cout << "\nEnter Phone: "; client.phone = InputValidate::readString();
	cout << "\nEnter PinCode: "; client.pinCode = InputValidate::readString();
	cout << "\nEnter Account Balance: "; client.accountBalance = InputValidate::readFloatNumber();
}

void addNewClient() {
	string accountNumber = "";

	accountNumber = InputValidate::readString("\nPlease Enter Account Number: ");

	while (BankClient::isClientExist(accountNumber)) {
		accountNumber = InputValidate::readString("\nAccount Number Is Already Used, Choose another one: ");
	}

	BankClient newClient = BankClient::getAddNewClientObject(accountNumber);
	readClientInfo(newClient);

	BankClient::enSaveResults saveResult = newClient.save();

	switch (saveResult) {
	case BankClient::enSaveResults::svSucceeded:
		cout << "\nAccount Added Successfully :-)\n";
		newClient.print();
		break;
	case BankClient::enSaveResults::svFaildEmptyObject:
		cout << "\nError account was not saved because it's Empty";
		break;
	case BankClient::enSaveResults::svFaildAccountNumberExists:
		cout << "\nError account was not saved because account number is used!\n";
		break;
	}
}

int main() {
	addNewClient();
	system("pause>0");
	return 0;
}