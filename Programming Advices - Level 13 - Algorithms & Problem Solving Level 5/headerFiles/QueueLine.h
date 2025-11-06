#pragma once

#include <iostream>
#include <queue>
#include "Date.h"

using namespace std;

class QueueLine {
private:
	class Ticket {
	private:
		string _ticketNumber;
		string _ticketTime;
		short _waitingClients;
		short _averageServeTime;
		short _expectedServeTime;

	public:
		Ticket(string ticketNumber, string time, short waitingClients, short averageServeTime, short expectedServeTime) {
			this->_ticketNumber = ticketNumber;
			this->_ticketTime = time;
			this->_waitingClients = waitingClients;
			this->_averageServeTime = averageServeTime;
			this->_expectedServeTime = expectedServeTime;
		}

		// Getters
		string getTicketNumber() { return _ticketNumber; }
		string getTime() { return _ticketTime; }
		short getWaitingClients() { return _waitingClients; }
		short getAverageServeTime() { return _averageServeTime; }
		short getExpectedServeTime() { return _expectedServeTime; }

		// MSVC __declspec properties (read-only)
		__declspec(property(get = getTicketNumber)) string ticketNumber;
		__declspec(property(get = getTime)) string time;
		__declspec(property(get = getWaitingClients)) short waitingClients;
		__declspec(property(get = getAverageServeTime)) short averageServeTime;
		__declspec(property(get = getExpectedServeTime)) short expectedServeTime;
	};

	queue<Ticket> _queueLine;
	string _queuePrefix = "";
	short _serveTime = 0;
	int _totalTickets = 0;
	int _servedClient = 0;

	Ticket _getNewTicket() {
		return Ticket(
			_queuePrefix + to_string(_totalTickets),
			Date::GetSystemDateTimeString(),
			waitingClients(),
			_serveTime,
			_serveTime * waitingClients()
		);
	}

	void _printTicketInfo(Ticket& ticket) {
		cout << "\n\t\t\t\t+-------------------------------+";
		cout << "\n\t\t\t\t|        Ticket Information     |";
		cout << "\n\t\t\t\t+-------------------------------+";
		cout << "\n\t\t\t\t| Time           : " << ticket.time;
		cout << "\n\t\t\t\t| Ticket Number  : " << ticket.ticketNumber;
		cout << "\n\t\t\t\t| Waiting Clients: " << ticket.waitingClients;
		cout << "\n\t\t\t\t| Estimated Wait : " << ticket.expectedServeTime << " minutes";
		cout << "\n\t\t\t\t+-------------------------------+";
	}

	void _reverseQueueRecursive(queue<Ticket>& queue) {
		if (queue.empty())
			return;

		Ticket front = queue.front();
		queue.pop();
		_reverseQueueRecursive(queue);
		queue.push(front);
	}

	void _printTicketsLine(bool rtl = true) {
		queue<Ticket> tempQueue = _queueLine;
		string direction[2] = { " --> ", " <-- " };

		if (!rtl)
			_reverseQueueRecursive(tempQueue);

		cout << "\n\t\t\t\t Tickets : ";
		while (!tempQueue.empty()) {
			cout << tempQueue.front().ticketNumber<< direction[rtl];
			tempQueue.pop();
		}
	}


public:
	QueueLine(string queuePrefix, short serveTime) {
		this->_queuePrefix = queuePrefix;
		this->_serveTime = serveTime;
		this->_totalTickets = 0;
		this->_servedClient = 0;
	}


	void issueTicket() {
		++_totalTickets;
		_queueLine.push(_getNewTicket());
	}

	void printAllTickets() {
		queue<Ticket> tempQueue = _queueLine;

		cout << "\n\n\t\t\t\t========= Issued Tickets =========";
		if (tempQueue.empty())
			cout << "\n\t\t\t\t(No tickets issued yet)";


		while (!tempQueue.empty()) {
			_printTicketInfo(tempQueue.front());
			tempQueue.pop();
		}
		cout << "\n\t\t\t\t==================================\n";
	}

	void printTicketsLineRTL() {
		_printTicketsLine(true);
	}

	void printTicketsLineLTR() {
		_printTicketsLine(false);
	}

	int waitingClients() {
		return _queueLine.size();
	}

	bool serveNextClient() {
		if (_queueLine.empty())
			return false;

		++_servedClient;
		_queueLine.pop();

		return true;
	}

	void printInfo() {
		cout << "\n\n\t\t\t\t+===============================+";
		cout << "\n\t\t\t\t|          Queue Info           |";
		cout << "\n\t\t\t\t+===============================+";
		cout << "\n\t\t\t\t| Prefix           : " << _queuePrefix;
		cout << "\n\t\t\t\t| Total Tickets    : " << _totalTickets;
		cout << "\n\t\t\t\t| Served Clients   : " << _servedClient;
		cout << "\n\t\t\t\t| Waiting Clients  : " << _queueLine.size();
		cout << "\n\t\t\t\t+===============================+\n";
	}
};
