// Problem #22 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.

#include <iostream>
#include "../MyInputLibrary.h";

using namespace std;
using namespace input;

void printFibonacciSeriesByReccursion(int serieLength, int previous1 = 0, int previous2 = 1) {
	int result;

	if (serieLength <= 0)
		return;

	//	1       2       3       4       5       6       7       8       9       10
	//	0-1     1-1     1-2     2-3     3-5     5-8     8-13    13-21   21-34   34-55
	//	1-1     1-2     2-3     3-5     5-8     8-13    13-21   21-34   34-55   55-89
	// 
	//	previous1 with 0, 1 paramaters
	//	1       1       2       3       5       8       13      21      34      55
	//	previous2 with 0, 1 paramaters
	//	1       2       3       5       8       13      21      34      55      89


	result = previous1 + previous2;
	cout << previous2 << "\t";
	previous1 = previous2;
	previous2 = result;

	printFibonacciSeriesByReccursion(--serieLength, previous1, previous2);
}

int main()
{
	int serieLength = readPositiveInteger(2, " -> Please enter the fibonacci serie length: ");
	printFibonacciSeriesByReccursion(serieLength);
}

