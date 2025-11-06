// dateClassProject.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include "Date.h";


using namespace std;

int main() {
	Date date1 = Date::getSystemDate();
	Date date2(2024, 12, 31);
	Date date3(2024, 426);
	Date date4("29/2/2025");
	Date date5;


	cout << " -> " << date1.dateToString() << endl;
	cout << " -> " << date2.dateToString() << endl;
	cout << " -> " << date3.dateToString() << endl;
	cout << " -> " << date4.dateToString() << endl;
	cout << " -> " << date5.dateToString() << endl;
	cout << " -> " << Date::dateToString(2024, 12, 31) << endl;
	cout << " -> " << Date::dateToString(2024, 11, 30) << endl;
	cout << " -> " << Date::dateToString(Date("28/2/2023")) << endl;
}
