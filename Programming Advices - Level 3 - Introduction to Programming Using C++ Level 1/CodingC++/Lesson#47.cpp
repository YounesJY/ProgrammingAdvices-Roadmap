#include<iostream>
using namespace std;



//------------------------------------// Problem #26
//------------------------------------//


void readTheBreakPoint(unsigned int &breakPoint) {

    cout << "--> Please enter the breakPoint: ";
    cin >> breakPoint;

}

void printNumbersSequence(unsigned int breakPoint) {

    cout << "\n|--------------|\n" << endl;

    for(int i = 1; i <= breakPoint; ++i) {
        cout << " - [ " << i << " ]" << endl;
    }

    cout << "\n|--------------|\n" << endl;
}


//------------------------------------// Problem #27
//------------------------------------//


void readTheCountdown(unsigned int &countDown) {

    cout << "--> Please enter the countdown: ";
    cin >> countDown;

}

void printCountdownSequence(unsigned int countDown) {

    cout << "\n|--------------|\n" << endl;

    for(int i = countDown; i >= 1; --i) {
        cout << " - [ " << i << " ]" << endl;
    }

    cout << "\n|--------------|\n" << endl;

}


//------------------------------------// Problem #28
//------------------------------------//


void readTheEnd(unsigned int &theEnd) {

    cout << "--> Please enter the breakPoint: ";
    cin >> theEnd;

}


unsigned int getTheSumOfOdds(unsigned int theEnd) {

    unsigned int Sum = 0;

    for(int i = 1; i <= theEnd; i+= 2) {
        Sum += i;
    }

    return (Sum);
}

void printTheSumOfOdds(unsigned int Sum) {

    cout << "\n|-------------------------|\n" << endl;
    cout << " -> The sum is: [ " << Sum << " ]" << endl;
    cout << "\n|-------------------------|\n" << endl;

}


//------------------------------------// Problem #29
//------------------------------------//


void readTheEndPoint(unsigned int &endPoint) {

    cout << "--> Please enter the endpoint: ";
    cin >> endPoint;

}


unsigned int getTheSumOfEvens(unsigned int endPoint) {

    unsigned int Sum = 0;

    for(int i = 1; i <= endPoint; ++i) {
        if (i % 2 == 0) {
            Sum += i;
        }
    }

    return (Sum);
}

void printTheSumOfEvens(unsigned int Sum) {

    cout << "\n|-------------------------|\n" << endl;
    cout << " -> The sum is: [ " << Sum << " ]" << endl;
    cout << "\n|-------------------------|\n" << endl;

}


//------------------------------------// Problem #30
//------------------------------------//


void readFactorial(unsigned int &factorial) {

    cout << "--> Please enter a positive number: ";
    cin >> factorial;

}

unsigned int getFactorial(unsigned int factorial) {

    unsigned int result = 1;

    if ( (factorial == 0) || (factorial == 1) ) {
        return ( 1 );
    }

    for(int i = 2; i <= factorial; ++i) {
        result *= i;
    }

    return (result);
}

void printTheFactorial(unsigned int factorial, unsigned int factorialResult) {

    cout << "\n|------------------------------------|\n" << endl;
    cout << " -> The factorial of [" << factorial << "] is: [ " << factorialResult << " ]" << endl;
    cout << "\n|------------------------------------|\n" << endl;

}


//------------------------------------// Problem 32
//------------------------------------//


void readTheNumber(float &number) {

    cout << "--> Please enter a number: ";
    cin >> number;

}
void readThePower(int &power) {

    cout << "--> Please enter the power value: ";
    cin >> power;

}


float getPowerResult(float number, int power) {

    float result = 1;

    if ( (power == 0) || (number == 1) ) {
        return ( result );
    }

    else if ( (number == 0) && (power > 0) )  {
        return ( 0 );
    }

    else if (power < 0) {
        power *= (-1);
        for(int i = 1; i <= power; ++i) {
            result *= number;
        }
        return ( 1 / result );
    }

    else {
        for(int i = 1; i <= power; ++i) {
            result *= number;
        }
        return ( result );
    }

}

void printPowerResult(float number, int power, float result) {

    cout << "\n|------------------------------------|\n" << endl;
    cout << " -> [ " <<  number << " ^ " << power << " ] = [ " << result << " ]" << endl;
    cout << "\n|------------------------------------|\n" << endl;

}


//------------------------------------// Problem #46
//------------------------------------//


void printAllEnglishLetters() {

    // Method 1
    for(unsigned char letter = 'A'; letter <= 'Z'; ++letter) {
        cout << "  - [ " << letter << " ]" << endl;
    }

    // Mothod 3
    for(short int letter = (int) 'A'; letter <= (int) 'Z'; ++letter) {
        cout << "  - [ " << (char) letter << " ]" << endl;
    }

}

int main()
{
    ////--> Lesson #47 For loops exercises
    /*
    //------------------------------------------------------------------------------//

        //--> Problem #26 Print numbers from 1 to N
        unsigned int breakPoint;

        cout << "-------+ Print Numbers from 1 To N +-------" << endl;
        cout << "\n===========================================\n" << endl;
        readTheBreakPoint(breakPoint);
        printNumbersSequence(breakPoint);
        cout << "\n===========================================\n" << endl;

    //------------------------------------------------------------------------------//

        //--> Problem #27 Print numbers from N to 1
        unsigned int countDown;

        cout << "-------+ Print Numbers from N To +-------" << endl;
        cout << "\n===========================================\n" << endl;
        readTheCountdown(countDown);
        printCountdownSequence(countDown);
        cout << "\n===========================================\n" << endl;

    //------------------------------------------------------------------------------//

        //--> Problem #28 Print the sum of odd numbers from 1 to N
        unsigned int theEnd;

        cout << "----+ Get Odd Numbers Sum From 1 To N +----" << endl;
        cout << "\n===========================================\n" << endl;
        ReadTheEnd(theEnd);
        PrintTheSumOfOdds(GetTheSumOfOdds(theEnd));
        cout << "\n===========================================\n" << endl;

    //------------------------------------------------------------------------------//

        //--> Problem #29 Print the sum of even numbers from 1 to N
        unsigned int endPoint;

        cout << " ---+ Get Even Numbers Sum From 1 To N +---" << endl;
        cout << "\n===========================================\n" << endl;
        readTheEndPoint(endPoint);
        printTheSumOfEvens(getTheSumOfEvens(endPoint));
        cout << "\n===========================================\n" << endl;

    //------------------------------------------------------------------------------//

        //--> Problem #30 Get factorial of N
        unsigned int factorial;

        cout << "------+ Get Factorial Of A Number +-------" << endl;
        cout << "\n===========================================\n" << endl;
        readFactorial(factorial);
        printTheFactorial(factorial, getFactorial(factorial));
        cout << "\n===========================================\n" << endl;

    //------------------------------------------------------------------------------//

        //--> Problem #32 Get power of M without pow() function { using for loop}
        float number;
        int power;

        cout << " --------+ Get Power Of A Number +--------" << endl;
        cout << "\n===========================================\n" << endl;
        readTheNumber(number);
        readThePower(power);
        printPowerResult( number, power, getPowerResult(number, power) );
        cout << "\n===========================================\n" << endl;

    //------------------------------------------------------------------------------//

        //--> Problem #46 Print lettrrs from A to Z

        cout << "-------+ Print lettrrs from A to Z +-------" << endl;
        cout << "\n===========================================\n" << endl;
        printAllEnglishLetters();
        cout << "\n===========================================\n" << endl;

    //------------------------------------------------------------------------------//
    */
    return 0;
}
