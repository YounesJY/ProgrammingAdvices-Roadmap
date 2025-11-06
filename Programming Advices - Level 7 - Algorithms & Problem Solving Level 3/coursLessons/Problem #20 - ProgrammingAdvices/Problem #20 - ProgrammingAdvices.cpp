// Problem #20 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.

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

bool isAPalindromeMatrix(int matrix[3][3], int rows, int columns) {
	int lastElement = (columns - 1);
	int midleElement = (columns / 2);
	for (int i = 0; i < rows; i++)
	{
		for (int j = 0, k = lastElement; j < midleElement; j++, k--) {
			if (matrix[i][j] != matrix[i][k])
				return false;
		}
	}

	return true;
}


int main()
{
	srand((unsigned)time(NULL));

	int matrix[3][3] = { {1, 2, 1}, {2, 4, 2}, {1, 2, 1} };
	string result;

	//fillMatrixWithRandomNumbers(matrixTest, 3, 3, 0, 10);
	printMatrixElements(matrix, 3, 3);

	result = (isAPalindromeMatrix(matrix, 3, 3)) ? "-> Yes! it's a palindrome matrix." : "-> No! it's not a palindrome matrix.";

	cout << "  - Does the matrix a palindrome matrix ? --> " << result << endl;

}