// Lesson #41 - Objects and Vectors.cpp : This file contains the 'main' function. Program execution begins and ends there.
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
		cout << "  -> The value of x = " << x << endl;
	}
};


int main() {

	vector <clsA> objects;
	short numberOfObjects = 5;

	// inserting object at the end of vector
	for (int i = 0; i < numberOfObjects; i++)
		objects.push_back(clsA(i));

	// printing object content
	for (int i = 0; i < numberOfObjects; i++) {
		cout << " -> Object N.(" << (i + 1) << ") : " << &objects[i] << endl;
		objects[i].print();
	}

	system("pause>0");

	return 0;
}

