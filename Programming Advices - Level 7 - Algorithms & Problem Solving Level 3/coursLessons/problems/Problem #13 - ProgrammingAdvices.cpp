// Problem #13 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.

#include <iostream>
#include <iomanip>
#include "../MyMathLibrary.h";
#include "../MyArrayLibrary.h";


using namespace std;
using namespace math;
using namespace array;


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

int getSumOfMatrixNumbers(int matrix[3][3], int rows, int columns) {
	int sum = 0;

	for (int i = 0; i < rows; i++)
	{
		for (int j = 0; j < columns; j++)
			sum += matrix[i][j];
	}

	return sum;
}

bool isIdentityMatrix(int matrix[3][3], int rows, int columns) {

	if (!isASquareMatrix(rows, columns))
		return false;

	for (int i = 0; i < columns; i++)
	{
		for (int j = 0; j < columns; j++) {
			if ((i == j) && (matrix[i][j] != 1))
				return false;
			else if ((i != j) && (matrix[i][j] != 0))
				return false;
		}
	}

	return true;
}


int main()
{
	srand((unsigned)time(NULL));

	int matrixTest[3][3] = { {1, 0 , 0},
						{0, 1 , 0},
						{0, 0 , 1}
	};

	while (true)
	{
		fillMatrixWithRandomNumbers(matrixTest, 3, 3, 0, 1);
		if ((matrixTest[0][0] == 1) && (matrixTest[1][1] == 1) && (matrixTest[2][2] == 1) && getSumOfMatrixNumbers(matrixTest, 3, 3) == 3)
			break;
	}

	printMatrixElements(matrixTest, 3, 3);

	string result = (isIdentityMatrix(matrixTest, 3, 3)) ? " -> Yes! it's an identity martrix" : "-> No!it's not an identity martrix";

	cout << result << endl;
}
