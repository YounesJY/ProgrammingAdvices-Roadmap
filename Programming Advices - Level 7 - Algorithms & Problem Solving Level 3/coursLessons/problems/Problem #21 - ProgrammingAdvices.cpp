// Problem #21 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.

#include <iostream>
#include "../MyInputLibrary.h";

using namespace std;
using namespace input;

void printFibonacciSeries(int serieLength) {

	int previous1 = 1, previous2 = 1;
	int result;

	cout << previous1 << "\t";
	cout << previous2 << "\t";
	
	serieLength -= 2;
	for (int i = 0; i < serieLength; i++)
	{
		result = previous1 + previous2;

		cout << result << "\t";

		previous1 = previous2;
		previous2 = result;
	}
}

int main()
{
	int serieLength = readPositiveInteger(2," -> Please enter the fibonacci serie length: ");
	printFibonacciSeries(serieLength);
}

