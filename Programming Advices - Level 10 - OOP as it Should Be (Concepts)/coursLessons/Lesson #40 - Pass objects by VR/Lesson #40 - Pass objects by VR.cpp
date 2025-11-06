// Lesson #40 - Passing Objects to Functions : This file contains the 'main' function. Program execution begins and ends there.
//ProgrammingAdivces.com
//Mohammed Abu-Hadhoud
#include<iostream>

using namespace std;

class clsA {
public:
	int x;

	void print() {
		cout << "The value of x= " << x << endl;
	}
};


//object sent by value, any updated will not b reflected on the original object
void function1(clsA A1) {
	A1.x = 100;
}

//object sent by reference, any updated will be reflected on the original object
void function2(clsA& A1) {
	A1.x = 200;
}

int main() {
	clsA A1;

	A1.x = 50;
	cout << "\nA.x before calling function1: \n";
	A1.print();


	//Pass by value, object will not be afected.
	function1(A1);
	cout << "\nA.x after calling function1 byval: \n";
	A1.print();

	//Pass by value, object will be afected.
	function2(A1);
	cout << "\nA.x after calling function2 byref: \n";
	A1.print();


	system("pause>0");
}

