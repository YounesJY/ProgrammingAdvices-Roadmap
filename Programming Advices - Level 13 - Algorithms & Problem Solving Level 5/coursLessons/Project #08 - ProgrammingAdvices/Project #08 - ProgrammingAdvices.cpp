// Project #08 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.
// ProgrammingAdvices.com
// Mohammed Abu-Hadhoud

#include <iostream>
#include "../../headerFiles/QueueLine.h"

using namespace std;

int main()
{
    QueueLine payBillsQueue("A0", 10);
    QueueLine subscriptionsQueue("B0", 5);

    // Issue tickets for pay bills queue
    payBillsQueue.issueTicket();
    payBillsQueue.issueTicket();
    payBillsQueue.issueTicket();
    payBillsQueue.issueTicket();
    payBillsQueue.issueTicket();

    cout << "\nPay Bills Queue Info:\n";
    payBillsQueue.printInfo();
    payBillsQueue.printTicketsLineRTL();
    payBillsQueue.printTicketsLineLTR();
    payBillsQueue.printAllTickets();

    payBillsQueue.serveNextClient();
    cout << "\nPay Bills Queue After Serving One Client:\n";
    payBillsQueue.printInfo();

    cout << "\nSubscriptions Queue Info:\n";

    // Issue tickets for subscriptions queue
    subscriptionsQueue.issueTicket();
    subscriptionsQueue.issueTicket();
    subscriptionsQueue.issueTicket();

    subscriptionsQueue.printInfo();
    subscriptionsQueue.printTicketsLineRTL();
    subscriptionsQueue.printTicketsLineLTR();
    subscriptionsQueue.printAllTickets();

    subscriptionsQueue.serveNextClient();
    cout << "\nSubscriptions Queue After Serving One Client:\n";
    subscriptionsQueue.printInfo();

    return 0;
}