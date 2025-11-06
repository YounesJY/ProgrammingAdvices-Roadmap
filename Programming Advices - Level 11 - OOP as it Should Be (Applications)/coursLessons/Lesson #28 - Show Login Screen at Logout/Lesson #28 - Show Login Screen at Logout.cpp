// Lesson #28 - Show Login Screen at Logout.cpp : This file contains the 'main' function. Program execution begins and ends there.

#include <iostream>
#include "../../HeaderFiles/LoginScreen.h"

int main() {
	while (true)
		LoginScreen::showLoginScreen();

	return 0;
}
