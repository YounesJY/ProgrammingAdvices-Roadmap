// Lesson #19 - Recursion - ProgrammingAdvices.cpp : Ce fichier contient la fonction 'main'. L'exécution du programme commence et se termine à cet endroit.
//

#include <iostream>
#include "../MyOthersStuffLibrary.h";
using namespace std;

void printNumbersFromToASC(int from, int to) {
	if (from <= to)
	{
		cout << from << endl;
		printNumbersFromToASC(++from, to);
	}
}
void printNumbersFromToDESC(int from, int to) {
	if (from >= to)
	{
		cout << from << endl;
		printNumbersFromToDESC(--from, to);
	}
}

int calculatePow(int base, int power) {

	if (power != 0)
		return base * calculatePow(base, power - 1);
	else
		return 1;	
}

int sumOfNumbersFrom1ToN(int to) {

	if (to >= 0)
	{
		return (1 + (sumOfNumbersFrom1ToN(to - 1)));
	}
		return 0;
}

int main()
{
	
		//cout << "\n============================\n";
		//cout << " Printing numbers from 1 to 20: " << endl;
		//printNumbersFromToASC(1, 5000);
		//cout << "\n============================\n";	
		
		cout << "\n============================\n";
		cout << " Printing numbers from 33 to 19: " << endl;
		printNumbersFromToDESC(33, 1);
		cout << "\n============================\n";

		//cout << "\n============================\n";
		//cout << " calculate power: " << endl;
		//cout << calculatePow(2, 1)<< endl;
		//cout << "\n============================\n";
	

	//cout << "\n============================\n";
	//cout << " calculate power: " << endl;
	//cout << sumOfNumbersFrom1ToN(5) << endl;
	//cout << "\n============================\n";

 }

