// Lesson #52 - Write Mode- Write Data To File.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include <fstream>
#include <vector>

using namespace std;

struct Employee {
	string firstName;
	string lastName;
	float salary;
};


void printEmployeeData(fstream& file, Employee employee) {
	file << "  ==================================\n";
	file << "  - First name : " << employee.firstName << endl;
	file << "  - Last name : " << employee.lastName << endl;
	file << "  - Salary : " << employee.salary << endl;
	file << "  ==================================\n";
}

int main()
{
	fstream myFile;

	Employee employees[] = { {"JY", "YounEsse", 28545}, {"Jay", "You", 12421}};
	myFile.open("myFileWithWriteMode", ios::out);

	if (myFile.is_open())
	{
		for (Employee employee : employees)
			printEmployeeData(myFile, employee);
		
		myFile.close();
	}

}
