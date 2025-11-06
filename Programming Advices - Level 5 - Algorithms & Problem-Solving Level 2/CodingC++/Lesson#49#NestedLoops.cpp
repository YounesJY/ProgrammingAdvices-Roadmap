#include<iostream>
using namespace std;


//--------------------------------------//
//--------------------------------------//


void printAllCoupleLetters() {

    for(char firstLetter = 'A'; firstLetter <= 'Z'; ++firstLetter) {

        cout << " ------------------+ [ " << firstLetter << " ] +---------------" << endl;
        for(char secondLetter = 'A'; secondLetter <= 'Z'; ++secondLetter) {
            cout << " - " << firstLetter << secondLetter << endl;
        }

    }

}


//--------------------------------------//
//--------------------------------------//


void printReversedStarsPyramid() {

    cout << "\n|-------+ Reversed Stars Pyramid +-------|\n" << endl;
    for(unsigned short int i = 10; i >= 1; --i ) {

        for(unsigned short int j = 1; j <= i; ++j ) {
            cout << "*";
        }
        cout << endl;

    }

}


//--------------------------------------//
//--------------------------------------//


void printReversedNumbersPyramid() {

    cout << "\n|-------+ Reversed Numbers Pyramid +------|\n" << endl;
    for(unsigned short int i = 10; i >= 1; --i ) {

        for(unsigned short int j = 1; j <= i; ++j ) {
            cout << j << " ";
        }
        cout << endl;

    }

}


//--------------------------------------//
//--------------------------------------//


void printNumbersPyramid() {

    cout << "\n|---------+ Get Numbers Pyramid +---------|\n" << endl;
    for(unsigned short int i = 1; i <= 10; ++i ) {

        for(unsigned short int j = 1; j <= i; ++j ) {
            cout << j << " ";
        }
        cout << endl;

    }

}


//--------------------------------------//
//--------------------------------------//


void printLettersPyramid() {

    cout << "\n|---------+ Get Letters Pyramid +---------|\n" << endl;
    for(unsigned char i = 'A'; i <= 'Z'; ++i ) {

        for(unsigned char j = 'A'; j <= i; ++j ) {
            cout << j << " ";
        }
        cout << endl;

    }

}


//--------------------------------------//
//--------------------------------------//


void printLessFirstNumberPyramid() {

    cout << "\n|----+ Get Less First Number Pyramid +----|\n" << endl;
    for(unsigned short int i = 1; i <= 10; ++i ) {

        cout << "" << endl;
        for(unsigned short int j = i; j <= 10; ++j ) {

            cout << j << " ";
        }
        cout << endl;

    }

}




int main()
{

    ////--> Lesson #49 Nested loops

    //------------------------------------------------------------------------------//

    printAllCoupleLetters();

    //------------------------------------------------------------------------------//

    printReversedStarsPyramid();

    //------------------------------------------------------------------------------//

    printReversedNumbersPyramid();

    //------------------------------------------------------------------------------//

    printNumbersPyramid();

    //------------------------------------------------------------------------------//

    printLettersPyramid();

    //------------------------------------------------------------------------------//

    printLessFirstNumberPyramid();

    //------------------------------------------------------------------------------//


    return 0;
}

