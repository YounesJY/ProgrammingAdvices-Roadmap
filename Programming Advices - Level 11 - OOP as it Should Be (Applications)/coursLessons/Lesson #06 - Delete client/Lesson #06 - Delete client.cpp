#include <iostream>
#include "../../HeaderFiles/Person.h"
#include "../../HeaderFiles/BankClient.h"
#include "../../HeaderFiles/InputValidate.h"

void deleteClient() {
	char answer = 'n';
	string accountNumber = "";

	accountNumber = InputValidate::readString("\nPlease Enter Account Number : ");

	while (!BankClient::isClientExist(accountNumber)) {
		accountNumber = InputValidate::readString("\nAccount number is not found, choose another one: ");
	}

	BankClient client = BankClient::find(accountNumber);
	client.print();

	cout << "\nAre you sure you want to delete this client y/n? ";
	cin >> answer;

	if (answer == 'y' || answer == 'Y') {
		if (client.deleteClient()) {
			cout << "\nClient Deleted Successfully :-)\n";
			client.print();
		}
		else {
			cout << "\nError Client Was not Deleted\n";
		}
	}
}

int main() {
	deleteClient();
	system("pause>0");
	return 0;
}