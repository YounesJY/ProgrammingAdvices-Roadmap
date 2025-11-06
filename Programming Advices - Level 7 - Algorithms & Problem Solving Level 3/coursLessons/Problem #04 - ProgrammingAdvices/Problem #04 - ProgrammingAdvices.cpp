// Problem #04 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.

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

int getColumnsSum(int array[3][3], int columnNumber, int rows) {
	int rowSum = 0;

	for (int i = 0; i < rows; i++)
		rowSum += array[i][columnNumber];

	return rowSum;
}

void getSumOfMatrixColumns(int array[3][3], int arrSum[], int rows, int columns) {

	for (int i = 0; i < rows; i++)
		arrSum[i] = getColumnsSum(array, i, rows);

}

void printSumOfMatrixColumns(int array[], int length) {
	for (int i = 0; i < length; i++)
		cout << "  - Column N.[" << (i + 1) << "] = " << array[i] << endl;
}

int main()
{
	srand((unsigned)time(NULL));

	int numbers[3][3];
	int arrColumnsSum[3];

	fillMatrixWithRandomNumbers(numbers, 3, 3, 1, 5);
	printMatrixElements(numbers, 3, 3);

	getSumOfMatrixColumns(numbers, arrColumnsSum, 3, 3);
	cout << " -> The sum for each column of the matrix is: " << endl;
	printSumOfMatrixColumns(arrColumnsSum, 3);

}

