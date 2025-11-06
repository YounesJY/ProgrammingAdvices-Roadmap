// Lesson #42 - Pointer to Void - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
using namespace std;

int main()
{
	int iNumber = 33;
	float fNumber = 33;
	void* ptr = &iNumber;

	cout << ptr << endl;
	cout << static_cast<int*>(ptr) << endl;
	cout << *static_cast<int*>(ptr) << endl;
	cout << (int*) ptr << endl;
	cout << *(int*) ptr << endl;

	ptr = &fNumber;

	cout << ptr << endl;
	cout << static_cast<float*>(ptr) << endl;
	cout << (float*) ptr << endl;
}
