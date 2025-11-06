// Lesson #09 - Ternary Operator_ Short Hand IF.cpp : Ce fichier contient la fonction 'main'. L'exécution du programme commence et se termine à cet endroit.

#include <iostream>

using namespace std;

int main()
{
    int mark = 34;
    string result;
    
    //// Normal if statement
    //if (mark >= 50)
    //    result = "pass";
    //else
    //    result = "fail";

    //cout << result << endl;
        
    //// Sort Hand if => Ternary Operator
    //result = (mark >= 50) ? "PASS" : "FAIL";

    //cout << result << endl;
    //
    //// Sort Hand if with print 
    //(mark >= 50) ? cout << "PASS" << endl : cout << "FAIL" << endl;
    //cout << ((mark >= 50) ? "PASS" : "FAIL") << endl;

    //===============================
    //===============================
    //HomeWork

    // Check if a number positive or negative
    int number = 0;
    cout << ((number > 0) ? "Positive" : "Negative") << endl;

        
    // Check if a number positive , Zero or negative
    string typeOfNumber;

    typeOfNumber = (number > 0) ? "Positive" : ((number == 0) ? "Zero" : "Negative");
    cout << typeOfNumber << endl;





}

