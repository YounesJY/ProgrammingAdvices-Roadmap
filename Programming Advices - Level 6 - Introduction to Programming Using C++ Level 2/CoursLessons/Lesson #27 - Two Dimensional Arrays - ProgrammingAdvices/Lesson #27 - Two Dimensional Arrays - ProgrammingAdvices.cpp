// Lesson #27 - Two Dimensional Arrays - ProgrammingAdvices.cpp : Ce fichier contient la fonction 'main'. L'exécution du programme commence et se termine à cet endroit.
//


#include <iostream>
#include <cstdio>
#include <iomanip>

using namespace std;

int main()
{
	int array[3][4] = { {1, 2, 3, 4},
		{5, 6, 7, 8},
		{9, 10,11,12} };

	//cout << setw(40) << "---------------------------------------------" << endl;
	//cout << setw(9) << "" << "|";
	//for (int column = 1; column <= 4; column++)
	//	cout << setw(8) << "column " << (column + 1) << "|";
	//
	//cout << endl;


	//for (int row = 0; row < 3; row++) {
	//	cout << setw(8) << "row " << (row + 1) << "|";
	//	for (int column = 0; column < 4; column++) 
	//		cout << setw(9) << array[row][column] << "|";
	//	cout << endl;
	//}

	int numbers[10][10];

	//for (int i = 0; i <10; i++) {
	//	for (int j = 0; j < 10; j++)
	//		numbers[i][j] = (i + 1)  * (j + 1);
	//}

	//for (int i = 0; i < 10; i++) {
	//	for (int j = 0; j < 10; j++)
	//		printf("%02d ", numbers[i][j]);
	//	cout << endl;
	//}


}

