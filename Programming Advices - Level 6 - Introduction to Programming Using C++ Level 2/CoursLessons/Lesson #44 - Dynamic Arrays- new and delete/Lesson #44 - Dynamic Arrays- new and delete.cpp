// Lesson #44 - Dynamic Arrays- new and delete - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include "../MyInputLibrary.h"

using namespace std;
using namespace input;


struct Student
{
	string fullName;
	float grade;
};

int main()
{
	int numOfStudents;
	Student* ptr;

	numOfStudents = readPositiveInteger(" -> Pleaser enter the number of students you want: ");
	ptr = new Student[numOfStudents];

	for (int i = 0; i < numOfStudents; i++)
	{
		cout << "= ============== Reading student " << (i + 1) << "  Data ============== = " << endl;
		cout << " -> Full Name? ";
		cin >> ptr[i].fullName;
		cout << " -> Grade? ";
		cin >> ptr[i].grade;
		cout << "= ===================================================================== = " << endl;
	}

	for (int i = 0; i < numOfStudents; i++)
	{
		cout << "=============== Displaying student " << (i + 1) << "  Data ============== = " << endl;
		cout << " -> Full Name: " << ptr[i].fullName << endl;
		cout << " -> Grade    : " << ptr[i].grade << endl;

	}

	cout << (*(ptr + 1)).fullName << endl;
	cout << ptr[1].fullName << endl;
	cout << (ptr + 1)->fullName << endl;

	delete[] ptr;


}
