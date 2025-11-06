#pragma once
#include <iostream>
#include "MyInputLibrary.h";

using namespace std;
using namespace inputsFunctions;

enum evenOrOdd { odd = 1, even = 2 };
enum positiveNegativeOrNull { positive = 1, negative = 2, null = 3 };

namespace math {

	evenOrOdd isEvenOrOdd(int number) {
		if (number % 2 == 0)
			return evenOrOdd::even;
		else
			return evenOrOdd::odd;
	}

	positiveNegativeOrNull isPositiveOrNegative(int number) {
		if (number > 0)
			return positiveNegativeOrNull::positive;
		else if (number == 0)
			return positiveNegativeOrNull::null;
		else
			return positiveNegativeOrNull::negative;
	}

	bool isAPrimeNumber(int number) {
		for (int i = 2; i <= (number / 2); ++i) {
			if (number % i == 0)
				return false;
		}
		return true;
	}

	bool isAPerfectNumber(int number) {
		int sum = 0;
		int halfOfTheNumber = (number / 2);
		for (int i = 1; i <= halfOfTheNumber; ++i) {
			if (number % i == 0)
				sum += i;
		}
		return (sum == number);
	}

	void printPrimeNumbersFrom1ToN() {
		int number = readPositiveInteger("--> Please enter a positive number: ");

		for (int i = 1; i <= number; ++i) {
			if (isAPrimeNumber(i))
				cout << "- [ " << i << " ]" << endl;
		}

	}

	void printPerfectNumbersFrom1ToN() {
		int number = readPositiveInteger("--> Please enter a positive number: ");

		cout << "--> Perfect numbers from 1 to " << number << ": " << endl;
		for (int i = 1; i <= number; ++i) {
			if (isAPerfectNumber(i))
				cout << "--> [ " << i << " ]" << endl;
		}
	}

	int getANumerInReversedOrder(int number) {
		//int number = readAPositiveInteger("--> Please enter a positive number: ");
		int reveredNumber = 0, remainder = 0, numberOfDigits = 0;
		/*
			numberOfDigits = getNumberOfDigitsOfANumber(number);123 --> 3

			do {
				remainder = (number % 10); 123 % 10 == 3
				reveredNumber += remainder * pow(10, --numberOfDigits); 0 + 3 * 10**2
				number /= 10;
			} while (number > 0);
		*/
		do {
			remainder = (number % 10);
			reveredNumber = reveredNumber * 10 + remainder;
			number /= 10;
		} while (number > 0);

		//cout << "--> The reversed version is : [ " << reveredNumber << " ]" << endl;
		return reveredNumber;
	}

	bool isAPalindromeNumber(int number) {
		return (number == getANumerInReversedOrder(number));
	}

	void printTheSumOfANumberDigits() {
		int number = readPositiveInteger("--> Please enter a positive number: ");
		int sum = 0;

		do {
			sum += (number % 10);
			number /= 10;
		} while (number > 0);

		cout << "--> The sum of digits is : [ " << sum << " ]" << endl;
	}

	int randomNumbers(int from, int to) {
		//(rand() % (5 - 1 + 1) + 1) ==> [1, 2 ,3 ,4 ,5]
		//(rand() % (5) + 1) ==> [1, 2 ,3 ,4 ,5]
		//([0, 1, 2 ,3 ,4] + 1) ===> [1, 2 ,3 ,4 ,5]
		return (rand() % (to - from + 1) + from);
	}

	short printADigitFrequencyInANumber(int number, short digit) {
		short remainder = 0, counter = 0;

		do {
			remainder = (number % 10);
			if (remainder == digit)
				++counter;
			number /= 10;
		} while (number > 0);

		return counter;
	}

	void printNumberDigitsFrequency() {
		int number = readPositiveInteger("--> Please enter a positive number: ");
		short digitFrequency = 0;

		for (short digit = 0; digit <= 9; ++digit) {
			digitFrequency = printADigitFrequencyInANumber(number, digit);
			if (digitFrequency > 0)
				cout << "[" << digit << "] frequency is [" << digitFrequency << "] times." << endl;
		}
	}

	void printNumerDigitsInReversedOrder(int number) {
		//int number = readAPositiveInteger("--> Please enter a positive number: ");
		int remainder = 0;

		do {
			remainder = (number % 10);
			cout << remainder << endl;
			number /= 10;

		} while (number > 0);

	}

	void printNumberDigitsInOrder() {
		int number = readPositiveInteger("--> Please enter a positive number: ");
		int reversedVersion = getANumerInReversedOrder(number);
		printNumerDigitsInReversedOrder(reversedVersion);
	}

	int getNumberOfDigitsOfANumber(int number) {
		int remainder, counter = 0;

		do {
			remainder = (number % 10);
			number /= 10;
			++counter;
		} while (number > 0);

		return counter;
	}
}
