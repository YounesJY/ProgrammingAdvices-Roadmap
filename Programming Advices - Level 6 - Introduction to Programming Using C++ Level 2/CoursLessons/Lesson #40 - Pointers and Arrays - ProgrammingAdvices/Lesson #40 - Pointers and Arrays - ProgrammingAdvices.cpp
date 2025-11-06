// Lesson #40 - Pointers and Arrays - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
using namespace std;

int* fun() {
	int array[] = { 12, 214, 213, 21 };
	return array;
}

int main()
{

	int* ptr = fun();

	for (size_t i = 0; i < 4; i++)
	{
		cout << *ptr++ << endl;
	}

	delete ptr;

	for (size_t i = 0; i < 4; i++)
	{
		cout << *(ptr + i) << endl;
	}


}
