// Lesson #29 - Add elements - Programm.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include <vector>
#include "../MyInputLibrary.h"
#include "../MyArrayLibrary.h"

using namespace std;
using namespace input;


void printNumbers(vector<int>& numbers) {
	cout << "  The numbers are: " << endl;
	cout << "\n  ==================================\n" << endl;
	for (int& number : numbers)
		cout << " - " << number << endl;
	cout << "\n  ==================================\n" << endl;
}

void readNumbers(vector<int>& numbers) {
	enum choice again;

	do
	{
		numbers.push_back(readInteger("=> Please enter a number: "));

		again = getChoice("\n - Do you want to add more numbers: \n");
		if (again == no)
			break;

	} while (again == yes);
}


int main()
{
	vector<int> vNumbers;

	//vNumbers.push_back(1);
	//vNumbers.push_back(12);
	//vNumbers.push_back(21);
	//vNumbers.push_back(512);
	//vNumbers.push_back(412);

	readNumbers(vNumbers);
	printNumbers(vNumbers);
}
