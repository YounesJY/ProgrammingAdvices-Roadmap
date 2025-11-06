// Problem #06 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.

#include <iostream>
#include <iomanip>
#include "../MyMathLibrary.h";

using namespace std;
using namespace math;

void fillMatrixWithOrderedNumbers(int array[3][3], int rows, int columns) {
	int counter = 0;
	for (int i = 0; i < rows; i++)
	{
		for (int j = 0; j < columns; j++)
			array[i][j] = ++counter;
	}
}

void printMatrixElements(int array[3][3], int rows, int columns) {
	cout << " -> this is a " << rows << "x" << columns << " matrix elements: " << endl;
	for (int i = 0; i < rows; i++)
	{
		for (int j = 0; j < columns; j++)
			cout << setw(3) << array[i][j] << "\t";
		cout << endl;
	}
	//for (int i = 0; i < length; i++)
	//	cout << " - " << *(ptr + i) << endl;
}


int main()
{
	srand((unsigned)time(NULL));

	int numbers[3][3];
	int arrColumnsSum[3];

	fillMatrixWithOrderedNumbers(numbers, 3, 3);
	printMatrixElements(numbers, 3, 3);

}

