// Problem #18 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.

#include <iostream>
#include <iomanip>
#include <vector>
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

bool isNumberInVector(vector<int> numbers, int numberToCheck) {
	for (int const& number : numbers) {
		if (number == numberToCheck)
			return true;
	}
	return false;
}

void printIntersectedNumbersInMatrices(int matrix1[3][3], int matrix2[3][3], int rows, int columns) {
	int length = rows * columns;
	int* ptr = &matrix1[0][0];
	int numberToCheck;
	vector<int> intersectedNums;

	cout << " -> The intersected numbers between the two matrices are:" << endl;
	for (int i = 0; i < length; i++) {
		numberToCheck = *(ptr + i);
		if (!isNumberInVector(intersectedNums, numberToCheck)) {
			if (isNumberInMatrix(matrix2, numberToCheck, 3, 3))
			{
				intersectedNums.push_back(numberToCheck);
				cout << setw(3) << *(ptr + i) << "\t";
			}
		}
	}
}

int main()
{
	srand((unsigned)time(NULL));

	int matrixTest[3][3], matrix[3][3];
	string result;

	fillMatrixWithRandomNumbers(matrixTest, 3, 3, 0, 10);
	printMatrixElements(matrixTest, 3, 3);

	fillMatrixWithRandomNumbers(matrix, 3, 3, 0, 10);
	printMatrixElements(matrix, 3, 3);

	printIntersectedNumbersInMatrices(matrixTest, matrix, 3, 3);

	}