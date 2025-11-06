// Problem #09 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.

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
	//for (int i = 0; i < length; i++)
	//	cout << " - " << *(ptr + i) << endl;
}

void getMatrixMidleRow(int matrix[3][3], int array[],  int rows, int columns) {
	short midleRow = (rows / 2);

	for (int i = 0; i < columns; i++)
		array[i] = matrix[midleRow][i];
	
}
void getMatrixMidleColumn(int matrix[3][3], int array[],  int rows, int columns) {
	short midleColumn = (columns / 2);

	for (int i = 0; i < rows; i++)
		array[i] = matrix[i][midleColumn];
	
}

int main()
{
	srand((unsigned)time(NULL));

	int matrix1[3][3];
	int array[3];


	fillMatrixWithRandomNumbers(matrix1, 3, 3, 1, 10);
	printMatrixElements(matrix1, 3, 3);

	cout << " -> Matrix's midle row is:" << endl;
	getMatrixMidleRow(matrix1, array, 3, 3);
	printArray(array, 3);
	cout << " -> Matrix's midle column is:" << endl;
	getMatrixMidleColumn(matrix1, array, 3, 3);
	printArray(array, 3);


}
