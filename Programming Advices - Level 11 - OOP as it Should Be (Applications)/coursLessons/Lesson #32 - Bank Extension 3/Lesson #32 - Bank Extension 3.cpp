// Lesson #32 - Bank Extension 3.cpp : This file contains the 'main' function. Program execution begins and ends there.
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
