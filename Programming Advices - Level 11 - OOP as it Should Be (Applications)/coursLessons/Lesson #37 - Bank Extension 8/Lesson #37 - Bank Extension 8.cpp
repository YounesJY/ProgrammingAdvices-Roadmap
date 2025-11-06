// Lesson #37 - Bank Extension 8.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include "../../HeaderFiles/LoginScreen.h"

int main() {
	while (true) {
		if (!LoginScreen::showLoginScreen())
			break;
	}

	return 0;
}