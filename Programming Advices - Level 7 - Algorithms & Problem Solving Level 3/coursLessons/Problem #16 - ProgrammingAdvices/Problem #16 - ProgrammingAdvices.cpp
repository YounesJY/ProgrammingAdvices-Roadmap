// Problem #16 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.

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

int countNumberInMatrix(int matrix[3][3], int numberToSearch, int rows, int columns) {
	int counter = 0;

	for (int i = 0; i < rows; i++)
	{
		for (int j = 0; j < columns; j++) {
			if (matrix[i][j] == numberToSearch)
				++counter;
		}
	}

	return counter;
}

bool isASparceMatrix(int matrix[3][3], int rows, int columns) {
	short foundedTimes = countNumberInMatrix(matrix, 0, rows, columns);
	return (foundedTimes >= (rows * columns / 2));
}

int main()
{
	srand((unsigned)time(NULL));

	int matrixTest[3][3];
	string result;

	fillMatrixWithRandomNumbers(matrixTest, 3, 3, 0, 3);
	printMatrixElements(matrixTest, 3, 3);

	result = (isASparceMatrix(matrixTest, 3, 3)) ? " -> Yes! it's a sparce matrix" : "-> No!it's not a sparce matrix";

	cout << result << endl;

}
