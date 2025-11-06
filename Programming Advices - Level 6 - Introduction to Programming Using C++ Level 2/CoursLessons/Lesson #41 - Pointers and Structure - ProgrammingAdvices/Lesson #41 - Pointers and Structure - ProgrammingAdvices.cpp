// Lesson #41 - Pointers and Structure - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>

using namespace std;

struct Emlpoyee
{
	string fullName;
	float salary;
};

int main()
{
	Emlpoyee emp;
	Emlpoyee* ptr = &emp;

	emp.fullName = "jygs";
	emp.salary = 12441;


	cout << ptr << endl; 
	cout << ptr->fullName<< endl;
	cout << (*ptr).fullName << endl;




}
