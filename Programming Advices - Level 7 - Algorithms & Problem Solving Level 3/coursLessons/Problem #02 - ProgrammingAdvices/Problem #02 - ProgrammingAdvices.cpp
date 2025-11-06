// Problem #02 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.

#include <iostream>
#include <iomanip>
#include "../MyMathLibrary.h";

using namespace std;
using namespace math;

void fillMatrixWithRandomNumbers(int array[3][3], int rows, int columns, int from, int to) {
	for (int i = 0; i < rows; i++)
	{
		for (int j = 0; j < columns; j++)
			array[i][j] = randomNumbers(from, to);
	}
	// using pointer to be generale function
	// //(int *array, int rows, int columns, int from, int to)
	//for (int i = 0; i < length; i++)
	//	*(ptr + i) = randomNumbers(from, to);
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

int getRowSum(int array[3][3], int rowNumber, int length) {
	int rowSum = 0;

	for (int i = 0; i < length; i++)
		rowSum += array[rowNumber][i];

	return rowSum;
}

void printMatrixRowsSum(int array[3][3], int rows, int columns) {

	cout << " -> The sum for each row of the matrix is: " << endl;
	for (int i = 0; i < rows; i++)
		cout << "  - Row N.[" << (i + 1) << "] = " << getRowSum(array, i, columns) << endl;

}

int main()
{
	srand((unsigned)time(NULL));

	int numbers[3][3];

	fillMatrixWithRandomNumbers(numbers, 3, 3, 1, 100);
	printMatrixElements(numbers, 3, 3);
	printMatrixRowsSum(numbers, 3, 3);
}

