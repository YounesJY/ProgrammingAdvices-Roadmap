// Lesson #43 - Arrays - Par.Constructors.cpp : This file contains the 'main' function. Program execution begins and ends there.
//ProgrammingAdivces.com
//Mohammed Abu-Hadhoud
#include<iostream>
#include<vector>

using namespace std;

class clsA {
public:
	int x;
	
	//Parametarized Constructor
	clsA(int x) {
		this->x = x;
	}


	void print() {
		cout << " -> The value of x = " << x << endl;
	}
};


int main() {
	// Initializing 3 array Objects with function calls of parameterized constructor as elements of that array
	clsA objects[] = { clsA(10), clsA(20), clsA(30) };

	// using print method for each of three elements.
	for (int i = 0; i < 3; i++)
		objects[i].print();

	system("pause>0");

	return 0;
}

