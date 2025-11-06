// Problem #01 - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include "../libs/MyOthersStuffLibrary.h"
#include "../libs/MyMathLibrary.h"

enum bounds {
	none,
	hundred,
	thousand,
	million,
	billion
};

using namespace std;


string digitToText(short digit) {
	switch (digit)
	{
	case 1:
		return "one";
	case 2:
		return "two";
	case 3:
		return "three";
	case 4:
		return "four";
	case 5:
		return "five";
	case 6:
		return "sexe";
	case 7:
		return "seven";
	case 8:
		return "eight";
	case 9:
		return "nine";
	default:
		return "";
	}
}

string digitToTens(short digit) {
	switch (digit)
	{
	case 1:
		return "ten";
	case 2:
		return "twenty";
	case 3:
		return "therty";
	case 4:
		return "fourty";
	case 5:
		return "fifty";
	case 6:
		return "sexty";
	case 7:
		return "seventy";
	case 8:
		return "eighty";
	case 9:
		return "ninety";
	default:
		return "";
	}
}

string digitToHundreds(short digit) {
	if (digit == 0)
		return "";
	return ((digit == 1) ? "Hundred" : "Hundreds");
}

string digitToThousands(short digit) {
	if (digit == 0)
		return "";
	return ((digit == 1) ? "Thousand" : "Thousands");
}

string digitToMillions(short digit) {
	if (digit == 0)
		return "";
	return ((digit == 1) ? "Million" : "Millions");
}

string digitToBillions(short digit) {
	if (digit == 0)
		return "";
	return ((digit == 1) ? "Billion" : "Billions");
}


string getNextBound(enum bounds currentBound) {
	switch (currentBound)
	{
	case bounds::none:
		return "Hundred";
	case bounds::hundred:
		return "Thousand";
	case bounds::thousand:
		return "Million";
	case bounds::million:
		return "Billion";
	case bounds::billion:
		return "";
	default:
		return "";
	}
}
//	67 464 758 % 1000
string numberToText(long long number, enum bounds currentBound = bounds::none) {
	string numberToStr = "";

	if (number == 0)
		return "";

	numberToStr = digitToText(number % 10);
	number /= 10;

	numberToStr = digitToTens(number % 10) + " " + numberToStr;
	number /= 10;

	numberToStr = digitToText(number % 10) + " " + digitToHundreds(number % 10) + " " + numberToStr;
	number /= 10;

	if (currentBound != bounds::none)
	{
		numberToStr += " " + getNextBound(currentBound);
	}

	return numberToText(number, (enum bounds)(currentBound + 1)) + " " + numberToStr;
}

int main()
{
	/*long long number;
	while (true)
	{
	cout << " -> What to convert ? ";
	cin >> number;



		cout << numberToText(number) << endl;
	}*/

	cout << "\"Hello World\"";
}
