// Lesson #59-60 - Datetime- Local-UTC Time.cpp : This file contains the 'main' function. Program execution begins and ends there.
#pragma warning(disable : 4996)
#include <iostream>
#include <ctime>

using namespace std;

int main()
{
    time_t nowTime = time(0);

    char* dateTime = ctime(&nowTime) ;
    cout << "  - Local date and time is: " << dateTime;

    tm* universalDateTime = gmtime(&nowTime);
    dateTime = asctime(universalDateTime);
    cout << "  - UTC date and time is: " << dateTime;

}