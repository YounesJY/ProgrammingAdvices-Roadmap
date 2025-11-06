	// Problem #01 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.
	//

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

		//for (int i = 0; i < length; i++)
		//	*(ptr + i) = randomNumbers(from, to);
	}

	void printMatrixElements(int array[3][3], int rows, int columns) {
		for (int i = 0; i < rows; i++)
		{
			for (int j = 0; j < columns; j++)
				cout << setw(3) << array[i][j] << "\t";
			cout << endl;
		}
		//for (int i = 0; i < length; i++)
		//	cout << " - " << *(ptr + i) << endl;
	}

	int main()
	{
		srand((unsigned)time(NULL));

		int numbers[3][3];

		fillMatrixWithRandomNumbers(numbers, 3, 3, 1, 100);
		printMatrixElements(numbers, 3, 3);
	}

