#include<iostream>
using namespace std;


//----------------------------------//
//----------------------------------//


void getEndPoint(unsigned int &endPoint) {

    cout << "--> Please enter the end point: ";
    cin >> endPoint;

}

void printNumbersForm1ToEndPoint(unsigned int endPoint) {

    unsigned int i = 1;

    cout << "\n|----------------|\n" << endl;
    while (i <= endPoint) {

        cout << "  - [ " << i << " ]" << endl;
        ++i;

    }
    cout << "\n|----------------|\n" << endl;

}


//----------------------------------//
//----------------------------------//


unsigned int getBreakPoint() {

    unsigned int breakpoint;

    cout << "--> Please enter the break point: ";
    cin >> breakpoint;

    return breakpoint;
}

void printNumbersFormBreakPointTo1(unsigned int breakpoint) {

    unsigned int i = breakpoint;

    cout << "\n|----------------|\n" << endl;
    while (i >= 1) {

        cout << "  - [ " << i << " ]" << endl;
        --i;

    }
    cout << "\n|----------------|\n" << endl;

}


//----------------------------------//
//----------------------------------//


void printTheSumOfOddNumbers(unsigned int endPoint) {


    int i = 1, Sum = 0;

    while (i <= endPoint) {
        Sum += i;
        i += 2;
    }

    cout << "\n|------------------------|\n" << endl;
    cout << " -> The sum is: [ " << Sum << " ]" << endl;
    cout << "\n|------------------------|\n" << endl;

}


//----------------------------------//
//----------------------------------//


void printTheSumOfEvenNumbers(unsigned int endPoint) {


    int i = 1, Sum = 0;

    while (i <= endPoint) {

        if (i % 2 ==0) {
            Sum += i;
        }
        ++i;

    }

    cout << "\n|------------------------|\n" << endl;
    cout << " -> The sum is: [ " << Sum << " ]" << endl;
    cout << "\n|------------------------|\n" << endl;

}


//----------------------------------//
//----------------------------------//


long double getTheFactorial() {

    long double factorial;


    do {

        cout << "--> Please enter a positive number: ";
        cin >> factorial;

        if (factorial < 0) {
            cout << "\n !! Value Error !! [ " << factorial << " ]," << endl;
            cout << "  - This is not a positive number ! \n" << endl;
        }

    } while (factorial < 0);

    return factorial;
}

long double  calculateTheFactorial(long double  factorial) {

    int  i = 2;
    long double result = 1;

    while (i <= factorial) {

        result *= i;
        ++i;

    }

    return result;
}

void printTheFactorial(long double result) {

    cout << "\n|------------------------|\n" << endl;
    cout << " -> the result is : [ " << result << " ]" << endl;
    cout << "\n|------------------------|\n" << endl;

}


//----------------------------------//
//----------------------------------//


void getTheBase(float &theBase) {

    cout << "--> Please enter the base: ";
    cin >> theBase;

}

void getTheExponent(int &theExponent) {

    cout << "--> Please enter the exponent: ";
    cin >> theExponent;

}

float calculateThePower(float theBase, int theExponent) {

    int i = 1;
    float result = 1;

    if ( (theExponent == 0) || (theBase == 1) ) {
        return result;
    }

    else if (theBase == 0) {

        if (theExponent > 0) {
            return 0;
        }

        if (theExponent < 0) {
            return (result / 0);
        }

    }

    else if (theExponent < 0) {

        theExponent *= (-1);
        while (i <= theExponent) {

            result *= theBase;
            ++i;
        }
        return (1 / result);

    }

    else {

        while (i <= theExponent) {

            result *= theBase;
            ++i;
        }
        return result;

    }

}

void printThePowerResult(float theBase, int theExponent, float result) {

    cout << "\n|------------------------------------|\n" << endl;
    cout << " -> the result: [ " << theBase << " ^ " << theExponent << " ] = [ " << result << " ]" << endl;
    cout << "\n|------------------------------------|\n" << endl;

}


//----------------------------------//
//----------------------------------//


void getTheNumber(float &number, float counter) {

    cout << "--> please enter number N.[" << counter << "]: ";
    cin >> number;

}

float checkTheNumberAndCalculateTheSum() {

    float number, counter = 1, Sum = 0;

    getTheNumber(number, counter);
    while (number != -99) {

        Sum += number;
        ++counter;
        getTheNumber(number, counter);

    }

    return Sum;
}

void printTheSum(float sumResult) {

    cout << "\n|------------------------------------|\n" << endl;
    cout << " -> the sum is: [ " << sumResult << " ]" << endl;
    cout << "\n|------------------------------------|\n" << endl;

}


//----------------------------------//
//----------------------------------//


void getletteraFromAtoZ() {

    char alphabet = 'A';

    while (alphabet <= 'Z') {

        cout << "  - [ " << alphabet << " ]" << endl;
        ++alphabet;

    }

    cout << "\n  |---------------------------|\n" << endl;

    alphabet = 'a';
    while (alphabet <= 'z') {

        cout << "  - [ " << alphabet << " ]" << endl;
        ++alphabet;

    }

}


//----------------------------------//
//----------------------------------//


void readATM_PIN(unsigned short int &userInputPIN) {

    cout << "--> Please enter you ATM PIN code: ";
    cin >> userInputPIN;

}

void printUserBalance(float userBalance) {

    cout << "\n|------------------------------------|\n" << endl;
    cout << " -> Your balance is : [ " << userBalance << " ]" << endl;
    cout << "\n|------------------------------------|\n" << endl;

}

void checkUserInputCode(unsigned short int userATM_PIN, float userBalance) {

    unsigned short int userInputPIN;
    short int wrongPIN = 0;
    bool exitLoop = false;

    do  {

        readATM_PIN(userInputPIN);
        if (userInputPIN != userATM_PIN) {

            ++wrongPIN;
            cout << " !! Wrong PIN code !" << endl;

            if (wrongPIN < 3) {
                cout << "  - Try again, [ " << 3 - wrongPIN << " chances left] \n" << endl;
            }

        }

        else {

            printUserBalance(userBalance);
            exitLoop = true;

        }

    } while ( (wrongPIN != 3) && ( !exitLoop ) );

    if (wrongPIN == 3) {
        cout << " ⛔ Card Locked !!, no more chances" << endl;
    }

}


//----------------------------------//
//----------------------------------//


int main()
{
    //--> Lesson #50 While loops exercises

    /*
    //-------------------------------------------------------------------------------//
    unsigned int endPoint;

    cout << "-------+ Print Numbers From 1 To N +-------" << endl;
    cout << "\n===========================================\n" << endl;
    getEndPoint(endPoint);
    printNumbersForm1ToEndPoint(endPoint);
    cout << "\n===========================================\n" << endl;
    /*
    //-------------------------------------------------------------------------------//

     cout << "-------+ Print Numbers From N To 1 +-------" << endl;
     cout << "\n===========================================\n" << endl;
     printNumbersFormBreakPointTo1(getBreakPoint());
     cout << "\n===========================================\n" << endl;

    //-------------------------------------------------------------------------------//

     cout << " + Get The Sum of Odd Numbes From 1 To N +" << endl;
     cout << "\n===========================================\n" << endl;
     printTheSumOfOddNumbers(getBreakPoint());
     cout << "\n===========================================\n" << endl;

    //-------------------------------------------------------------------------------//

     cout << "+ Get The Sum of Even Numbes From 1 To N +" << endl;
     cout << "\n===========================================\n" << endl;
     printTheSumOfEvenNumbers(getBreakPoint());
     cout << "\n===========================================\n" << endl;

    //-------------------------------------------------------------------------------//

    cout << "  --+ Get The Factorial  Of A Number+--" << endl;
    cout << "\n===========================================\n" << endl;
    printTheFactorial(calculateTheFactorial(getTheFactorial()));
    cout << "\n===========================================\n" << endl;

    //-------------------------------------------------------------------------------//

            float theBase, result;
            int theExponent;

            cout << " --------+ Get Power Of A Number +--------" << endl;
            cout << "\n===========================================\n" << endl;
            getTheBase(theBase);
            getTheExponent(theExponent);
            result = calculateThePower(theBase, theExponent);
            printThePowerResult(theBase, theExponent, result);
            cout << "\n===========================================\n" << endl;

    //-------------------------------------------------------------------------------//

            cout << " --------+ Get The Sum Until -99 +--------" << endl;
            cout << "\n===========================================\n" << endl;
            printTheSum( checkTheNumberAndCalculateTheSum() );
            cout << "\n===========================================\n" << endl;

    //-------------------------------------------------------------------------------//

            cout << " ---+ Get English Letters Form A To Z +--- " << endl;
            cout << "\n===========================================\n" << endl;
            getletteraFromAtoZ();
            cout << "\n===========================================\n" << endl;

    //-------------------------------------------------------------------------------//

            unsigned short int userATM_PIN = 1234;
            float userBalance = 77383;

            cout << " ---------+ ATM PIN For 3 Times +--------- " << endl;
            cout << "\n===========================================\n" << endl;
            checkUserInputCode(userATM_PIN, userBalance);
            cout << "\n===========================================\n" << endl;

    //-------------------------------------------------------------------------------//
    */

    return 0;
}
