#pragma once

#include <iostream>
#include "Screen.h"
#include "BankClient.h"
#include "InputValidate.h"

class DepositScreen : protected Screen {
private:
    static void _printClient(BankClient Client) {
        cout << "\nClient Card:";
        cout << "\n___________________";
        cout << "\nFirstName   : " << Client.firstName;
        cout << "\nLastName    : " << Client.lastName;
        cout << "\nFull Name   : " << Client.fullName();
        cout << "\nEmail       : " << Client.email;
        cout << "\nPhone       : " << Client.phone;
        cout << "\nAcc. Number : " << Client.accountNumber();
        cout << "\nPassword    : " << Client.pinCode;
        cout << "\nBalance     : " << Client.accountBalance;
        cout << "\n___________________\n";
    }

    static string _readAccountNumber() {
        return Input::readString("\nPlease enter account number? ");
    }

public:
    static void showDepositScreen() {
        string accountNumber = "";
        Input::choice answer;

        _drawScreenHeader("\t   Deposit Screen");

        accountNumber = _readAccountNumber();
        while (!BankClient::isClientExist(accountNumber)) {
            cout << "\nClient with [" << accountNumber << "] does not exist.\n";
            accountNumber = _readAccountNumber();
        }

        BankClient client = BankClient::find(accountNumber);
        _printClient(client);

        double amount = Input::readDouble("\nPlease enter deposit amount? ");
        answer = Input::getChoice("\nAre you sure you want to delete this client (yes / no) ? ", false);

        if (answer == Input::choice::yes) {
            client.deposit(amount);
            cout << "\nAmount deposited successfully.\n";
            cout << "\nNew balance is: " << client.accountBalance;
        }
        else 
            cout << "\nOperation was cancelled.\n";
        
    }
};