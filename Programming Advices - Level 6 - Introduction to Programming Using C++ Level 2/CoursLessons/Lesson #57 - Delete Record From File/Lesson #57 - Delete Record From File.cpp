// Lesson #57 - Delete Record From File.cpp : This file contains the 'main' function. Program execution begins and ends there.

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

void saveVectorToFile(vector<string>& records, string filePath) {
	fstream file;

	file.open(filePath, ios::out);

	if (file.is_open())
	{
		for (const string& record : records)
		{
			if (!(record == ""))
				file << record << endl;
		}

		file.close();
	}

}

void deleteRecordFromFile(string recordToDelete, string filePath) {
	fstream file;
	vector<string> records;

	loadDataFromFileToVector(filePath, records);
	file.open(filePath, ios::out);

	if (file.is_open()) {

		for (string& record : records) {
			if (record == recordToDelete)
				record = "";
		}

		saveVectorToFile(records, filePath);
		file.close();
	}

}

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
	cout << " -> File content before delete:" << endl;
	printFileContent("myfile");

	deleteRecordFromFile("jy younessre", "myFile");

	cout << " -> File content after delete:" << endl;
	printFileContent("myfile");
}




