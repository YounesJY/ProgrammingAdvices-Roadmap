// Lesson #42 - Objects and Dynamic Array.cpp : This file contains the 'main' function. Program execution begins and ends there.
//ProgrammingAdivces.com
//Mohammed Abu-Hadhoud
#include<iostream>
#include<vector>

using namespace std;

class clsA {
public:
	int x;

	// dummy constructor
	clsA() {}

	//Parametarized Constructor
	clsA(int x) {
		this->x = x;
	}


	void print() {
		cout << "  - The value of x = " << x << endl;
	}
};


int main() {
	short numberOfObjects = 5;

	// allocating dynamic array of Size numberOfObjects using new keyword
	clsA* objects = new clsA[numberOfObjects];

	// calling constructor for each index of array
	for (int i = 0; i < numberOfObjects; i++)
		objects[i] = clsA(i);
	
	// printing contents of array
	for (int i = 0; i < numberOfObjects; i++) {
		cout << " -> Object N.(" << (i + 1) << ") at Memory Address: " << (objects + i) << endl;
		objects[i].print();
	}


	system("pause>0");
	return 0;
}

