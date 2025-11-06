// ProgrammingAdvices.com
// Mohammed Abu-Hadhoud
#include <iostream>
#include "../../HeaderFiles/BankClient.h"

using namespace std;

int main() {
    BankClient client1 = BankClient::find("A101");
    if (!client1.isEmpty()) {
        cout << "\nClient Found :-)\n";
    }
    else {
        cout << "\nClient Was not Found :-(\n";
    }
    client1.print();

    BankClient client2 = BankClient::find("A101", "1234");
    if (!client2.isEmpty()) {
        cout << "\nClient Found :-)\n";
    }
    else {
        cout << "\nClient Was not Found :-(\n";
    }
    client2.print();

    cout << "\nIs Client Exist? " << BankClient::isClientExist("A101");

    system("pause>0");
    return 0;
}