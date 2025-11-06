// Lesson #54 - Read Mode.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include <fstream>
#include <string>


using namespace std;

void printFileContent(string filePath) {

	fstream file;
	string fileRecord;

	file.open(filePath, ios::in);

	if (file.is_open())
	{
		while (getline(file, fileRecord))
			cout << fileRecord << endl;
		file.close();
	}


}

int main()
{
	printFileContent("myFile");
}

