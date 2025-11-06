// Lesson #20 - Static Variables.cpp : Ce fichier contient la fonction 'main'. L'exécution du programme commence et se termine à cet endroit.
//

#include <iostream>
using namespace std;

void function1() {
    static int number = 0;
    cout << number << endl;
    ++number;
}

int main()
{
    function1();
    function1();
    function1();

}

