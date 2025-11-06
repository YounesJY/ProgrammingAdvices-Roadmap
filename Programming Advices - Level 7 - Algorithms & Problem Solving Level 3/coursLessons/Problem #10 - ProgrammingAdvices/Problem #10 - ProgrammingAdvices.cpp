// Problem #10 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.

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

int getSumofMatrixNumbers(int matrix[3][3], int rows, int columns) {
	int sum = 0;

	for (int i = 0; i < rows; i++)
	{
		for (int j = 0; j < columns; j++)
			sum += matrix[i][j];
	}

	return sum;
}

int main()
{
	srand((unsigned)time(NULL));

	int matrix1[3][3];
	int array[3];


	fillMatrixWithRandomNumbers(matrix1, 3, 3, 1, 5);
	printMatrixElements(matrix1, 3, 3);
	cout << " -- The sum of matrix elements is: " << getSumofMatrixNumbers(matrix1, 3, 3);



}
