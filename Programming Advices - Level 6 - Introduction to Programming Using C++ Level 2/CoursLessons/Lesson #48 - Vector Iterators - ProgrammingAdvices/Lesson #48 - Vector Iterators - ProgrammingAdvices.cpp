// Lesson #48 - Vector Iterators - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include <vector>

using namespace std;

int main()
{
	vector<int> numbers = { 12,129,23,98 };
	vector<int>::iterator iter;

	cout << " -- Addresses: \n";
	for (const int& number : numbers)
		cout << " - " << &number << endl;

	for (iter = numbers.begin(); iter != numbers.end(); iter++)
	{
		cout << *iter << endl;
		cout << *iter << endl;
		cout << &(*iter) << endl;
	}

}
