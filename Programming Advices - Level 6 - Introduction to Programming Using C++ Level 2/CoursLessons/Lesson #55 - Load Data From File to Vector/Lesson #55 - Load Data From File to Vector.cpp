// Lesson #55 - Load Data From File to Vector.cpp : This file contains the 'main' function. Program execution begins and ends there.

#include <iostream>
#include <fstream>
#include <string>
#include <vector>
using namespace std;

void loadDataFromFileToVector(string filePath, vector<string>& records) {
	fstream file;
	string fileRecord;

	file.open(filePath, ios::in);

	if (file.is_open())
	{
		while (getline(file, fileRecord))
			records.push_back(fileRecord);

		file.close();
	}

}

void printVectorContent(vector<string>& records) {
	for (const string& record : records)
		cout << record << endl;
}

void saveVectorToFile(vector<string>& records, string filePath) {
	fstream file;

	file.open(filePath, ios::out);

	if (file.is_open())
	{
		for (const string& record : records) {
			if (record != "")
				file << record << endl;
		}

		file.close();
	}

}

int main()
{
	vector<string> records;

	loadDataFromFileToVector("myFile", records);
	printVectorContent(records);
	saveVectorToFile(records, "myFile");

}






