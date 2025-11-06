// Problem #19 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.

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

int getMatrixMaxNumber(int matrix[3][3], int rows, int columns) {
	int maxNumber = matrix[0][0];

	for (int i = 0; i < rows; i++)
	{
		for (int j = 0; j < columns; j++) {
			if (matrix[i][j] > maxNumber)
				maxNumber = matrix[i][j];
		}
	}

	return maxNumber;
}

int getMatrixMinNumber(int matrix[3][3], int rows, int columns) {
	int minNumber = matrix[0][0];

	for (int i = 0; i < rows; i++)
	{
		for (int j = 0; j < columns; j++) {
			if (matrix[i][j] < minNumber)
				minNumber = matrix[i][j];
		}
	}

	return minNumber;
}


int main()
{
	srand((unsigned)time(NULL));

	int matrix1[3][3], matrix2[3][3];
	string result;

	fillMatrixWithRandomNumbers(matrix1, 3, 3, 0, 100);
	printMatrixElements(matrix1, 3, 3);

	fillMatrixWithRandomNumbers(matrix2, 3, 3, 0, 100);
	printMatrixElements(matrix2, 3, 3);

	cout << " -> Matrix 1: " << endl;
	cout << "  - Maximum value: "  << getMatrixMaxNumber(matrix1, 3, 3) << endl;
	cout << "  - Minimum value: "  << getMatrixMinNumber(matrix1, 3, 3) << endl;
	
	cout << " -> Matrix 2: " << endl;							   
	cout << "  - Maximum value: "  << getMatrixMaxNumber(matrix2, 3, 3) << endl;
	cout << "  - Minimum value: "  << getMatrixMinNumber(matrix2, 3, 3) << endl;



}