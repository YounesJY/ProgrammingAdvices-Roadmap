// Problem #17 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.

#include <iostream>
#include <iomanip>
#include "../MyInputLibrary.h";
#include "../MyMathLibrary.h";

using namespace std;
using namespace input;
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
}

bool isNumberInMatrix(int matrix[3][3], int numberToCheck, int rows, int columns) {
	for (int i = 0; i < rows; i++)
	{
		for (int j = 0; j < columns; j++) {
			if (matrix[i][j] == numberToCheck)
				return true;
		}
	}

	return false;
}


int main()
{
	srand((unsigned)time(NULL));

	int matrixTest[3][3], numberToSearch;
	string result;

	fillMatrixWithRandomNumbers(matrixTest, 3, 3, 0, 3);
	printMatrixElements(matrixTest, 3, 3);

	numberToSearch = readInteger(" -> Please enter a number to check if exist in the matrix: ");
	result = (isNumberInMatrix(matrixTest, numberToSearch, 3, 3)) ? " -> Yes! it's there in the matrix" : "-> No!it's not in the matrix";

	cout << result << endl;

}
