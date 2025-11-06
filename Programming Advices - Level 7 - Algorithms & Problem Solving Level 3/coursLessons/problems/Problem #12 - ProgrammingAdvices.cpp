// Problem #12 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.

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
}

bool areMatricesTypical(int matrix1[3][3], int matrix2[3][3], int rows, int columns) {
	for (int i = 0; i < rows; i++)
	{
		for (int j = 0; j < columns; j++) {
			if (matrix1[i][j] != matrix2[i][j])
				return false;
		}
	}

	return true;
}

int main()
{
	srand((unsigned)time(NULL));

	int matrix1[3][3], matrix2[3][3], matrix3[3][3];

	fillMatrixWithOrderedNumbers(matrix1, 3, 3);
	fillMatrixWithOrderedNumbers(matrix2, 3, 3);
	fillMatrixWithRandomNumbers(matrix3, 3, 3, 1, 3);

	printMatrixElements(matrix1, 3, 3);
	printMatrixElements(matrix2, 3, 3);
	printMatrixElements(matrix3, 3, 3);

	string result = (areMatricesTypical(matrix1, matrix2, 3, 3)) ? "Yes ! there are." : "No ! there'r not.";
	cout << " -- Are these matrices equals (matrix1, matrix2)? -->" << result << endl;

	result = (areMatricesTypical(matrix1, matrix3, 3, 3)) ? "Yes ! there are." : "No ! there'r not.";
	cout << " -- Are these matrices equals (matrix1, matrix3)? -->" << result << endl;
}
