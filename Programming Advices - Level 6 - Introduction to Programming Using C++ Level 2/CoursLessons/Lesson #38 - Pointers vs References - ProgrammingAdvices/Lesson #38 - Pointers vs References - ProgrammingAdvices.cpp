// Lesson #38 - Pointers vs References - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>

using namespace std;




int main()
{
	int a = 32;
	int& ref = a;
	int* ptr = &a;

	cout << " -> a    = " << a << endl;
	cout << " -> &a   = " << &a << endl;
	cout << " -> ref  = " << ref << endl;
	cout << " -> &ref = " << &ref << endl;
	
	cout << endl << "=====================================" << endl;
	
	cout << " -> ptr  = " << ptr << endl;
	cout << " -> *ptr = " << *ptr << endl;
	cout << " -> &ptr = " << &ptr << endl;
	
	*ptr = 236;
	cout << " -> *&ptr = " << *(&ptr) << endl;

	cout << endl << "=====================================" << endl;

	cout << " -> a    = " << a << endl;
	cout << " -> &a   = " << &a << endl;
	cout << " -> ref  = " << ref << endl;
	cout << " -> &ref = " << &ref << endl;
	cout << " -> *ptr = " << *ptr << endl;
	cout << endl << "=====================================" << endl;

	//int size = 3;
	//int *ptr = new int[size];
	//size = 32;

	//delete [] ptr;

}

