// Problem #08 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.

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

void getMatrixMultiplication(int arr1[3][3], int arr2[3][3], int arrMultiplication[3][3], int rows, int columns) {
	for (int i = 0; i < rows; i++)
	{
		for (int j = 0; j < columns; j++)
			arrMultiplication[i][j] = arr1[i][j] * arr2[i][j];
	}
}

int main()
{
	srand((unsigned)time(NULL));

	int matrix1[3][3], matrix2[3][3];
	int matrix3[3][3];


	fillMatrixWithRandomNumbers(matrix1, 3, 3, 1, 10);
	fillMatrixWithRandomNumbers(matrix2, 3, 3, 1, 10);
	
	printMatrixElements(matrix1, 3, 3);
	printMatrixElements(matrix2, 3, 3);

	getMatrixMultiplication(matrix1, matrix2, matrix3, 3, 3);
	printMatrixElements(matrix3, 3, 3);


}
