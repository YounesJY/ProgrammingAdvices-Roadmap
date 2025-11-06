// Problem #14 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.

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
}
int getSumofMatrixNumbers(int matrix[3][3], int rows, int columns) {
	int sum = 0;

	for (int i = 0; i < rows; i++)
	{
		for (int j = 0; j < columns; j++)
			sum += matrix[i][j];
	}

	return sum;
}

bool isASquareMatrix(int rows, int columns) {
	return (rows == columns);
}

void getMatrixDiagonal(int matrix[3][3], int diagonalElements[3], int rows, int columns) {
	if (!isASquareMatrix(rows, columns)) {
		cout << " -- The matrix isn't of square type! impossible to get a diagoanl for it.";
		return;
	}

	for (int i = 0; i < rows; i++)
		diagonalElements[i] = matrix[i][i];
}


bool isScalarMatrix(int matrix[3][3], int rows, int columns) {

	if (!isASquareMatrix(rows, columns))
		return false;

	int* diagonalPattern = new int;
	*diagonalPattern = matrix[0][0];

	for (int i = 0; i < columns; i++)
	{
		for (int j = 0; j < columns; j++) {
			if ((i == j) && (matrix[i][j] != *diagonalPattern))
				return false;
			else if ((i != j) && (matrix[i][j] != 0))
				return false;
		}
	}
	delete diagonalPattern;
	return true;
}


int main()
{
	srand((unsigned)time(NULL));

	int matrixTest[3][3] = { {3, 0 , 0},
						{0, 3 , 0},
						{0, 0 , 0}
	};

	while (true)
	{
		fillMatrixWithRandomNumbers(matrixTest, 3, 3, 0, 3);
		if (matrixTest[0][0] == matrixTest[1][1] && matrixTest[0][0] == matrixTest[2][2])
			if (getSumofMatrixNumbers(matrixTest, 3, 3) == 3 * matrixTest[0][0])
				break;
	}

	//fillMatrixWithRandomNumbers(matrixTest, 3, 3, 9, 10);
	printMatrixElements(matrixTest, 3, 3);

	string result = (isScalarMatrix(matrixTest, 3, 3)) ? " -> Yes! it's a scalar martrix" : "-> No!it's not a scalar martrix";


	cout << result << endl;
}
