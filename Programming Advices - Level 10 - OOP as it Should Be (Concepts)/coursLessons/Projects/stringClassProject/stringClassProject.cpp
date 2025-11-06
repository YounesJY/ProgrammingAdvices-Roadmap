// stringClassProject.cpp : This file contains the 'main' function. Program execution begins and ends there.

#include <iostream>
#include <string>
#include "Input.h";
#include "String.h";
#include "StringFullEdition.h";


int main()
{
	String text= String(Input::readString(" -> What to enter ? "));
	cout << text.value << endl;

	
	if (Input::getChoice(" Want to do ? ", false) == 1)
		cout << text.value << endl;
}
