// Lesson #47 - Change Elements - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include <vector>

using namespace std;

int main()
{
	vector<int> numbers = { 12, 1289, 21, 123 };

	for (const int& number : numbers)
		cout << number << endl;
	
	for (int& number : numbers)
		number += 12 * 3;

	for (const int& number : numbers)
		cout << number << endl;
}
