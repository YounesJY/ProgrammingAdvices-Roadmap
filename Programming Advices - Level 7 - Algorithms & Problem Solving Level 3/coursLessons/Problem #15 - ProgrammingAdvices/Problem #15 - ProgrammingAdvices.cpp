// Problem #15 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.

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

int main()
{
	srand((unsigned)time(NULL));

	int matrixTest[3][3];
	int numberToSearch, counter;
	string foundedTimes;

	fillMatrixWithRandomNumbers(matrixTest, 3, 3, 1, 10);
	printMatrixElements(matrixTest, 3, 3);

	numberToSearch = readInteger(" -> Please enter the number you want to search for: ");
	counter = countNumberInMatrix(matrixTest, numberToSearch, 3, 3);
	foundedTimes = (countNumberInMatrix(matrixTest, numberToSearch, 3, 3) > 1) ? "times" : ((counter == 1) ? "only one time" : "no time");

	cout << " -> The number [" << numberToSearch << "] is found [" << counter << "] " << foundedTimes << " in the matrix!" << endl;

}
