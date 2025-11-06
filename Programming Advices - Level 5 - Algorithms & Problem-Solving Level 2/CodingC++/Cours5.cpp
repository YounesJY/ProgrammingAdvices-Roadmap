#include<iostream>
#include<cmath>
#include<string.h>

using namespace std;

//------------------- Generic Functions ------------------//
//--------------------------------------------------------//

void printAndDisplayAMessage(string messageToDisplay) {

    cout << "\n======================================\n" << endl;
    cout << messageToDisplay << endl;
    cout << "\n======================================\n" << endl;

}

void printDetailedMessage (string messageToDisplay1, float value, string messageToDisplay2) {

    cout << "\n======================================\n" << endl;
    cout << messageToDisplay1 << value << messageToDisplay2 << endl;
    cout << "\n======================================\n" << endl;

}

//==================================//

void readAnumber(string messageToDisplay, float &number) {

    cout << messageToDisplay;
    cin >> number;

}

void readAPositiveNumber(string messageToDisplay, float &number) {

    do {

        cout << messageToDisplay;
        cin >> number;

        if (number <= 0) {
            cout << " !! Invalid negative value [ " << number << " ]" << endl;
            cout << "  - Try again: \n" << endl;
        }

    } while (number <= 0);

}

//==================================//

void readAnIntegerNumber(string messageToDisplay, int &number) {

    cout << messageToDisplay;
    cin >> number;

}

void readAPositiveIntegerNumber(string messageToDisplay, int &number) {

    do {

        cout << messageToDisplay;
        cin >> number;

        if (number < 0) {
            cout << " !! Invalid negative value [ " << number << " ]" << endl;
            cout << "  - Try again: \n" << endl;
        }

    } while (number < 0);

}

bool ValidateAValueInRange(int number, int from, int to) {

    return ( (number >= from) && (number <= to) );
}


void readAnIntegerNumberInRange(string messageToDisplay, int &number, int from, int to) {

    do {

        cout << messageToDisplay;
        cin >> number;

        if ( !ValidateAValueInRange(number, from, to) ) {
            cout << " !! Invalid value [" << number << "] in range (" << from << " - " << to << ")" << endl;
            cout << "  - Try again: \n" << endl;
        }

    } while ( !ValidateAValueInRange(number, from, to) );

}


bool isPrimeNumber(int number) {

    int halfOfNumber = (number / 2);



    // The largest and slowest method
    /*  int divisible = 0;
        for(int divisor = 1; divisor <= number; ++divisor) {

            if(number % divisor == 0)
                ++divisible;

        }

        return (divisible >= 2);
    */

    // The shortest and fastest method
    for(int divisor = 2; divisor <= halfOfNumber; ++divisor) {

        if(number % divisor == 0)
            return false;

    }

    return true;

}

bool isPerfectNumber(int number) {

    int sum = 0;
    int halfOfNumber = number / 2;

    for(int i = 1; i <= halfOfNumber; ++i) {
        if(number % i == 0)
            sum += i;
    }

    return (sum == number);
}
//--------------------------------------------------------//
//--------------------------------------------------------//

void printMultiplicationTableHeader() {

    for (int number = 1; number <= 10; ++number) {
        cout << number << "   ";
    }
    cout << "\n__________________________________________\n" ;

}

//==================================//

void columnFormater(int number) {

    if (number<= 9)
        cout << number << " |";
    else
        cout << number << "|";

}

//==================================//

void multiplicationResultsFormatter(int number) {

    if (number <= 9)
        cout << number << "   ";
    else if (number < 100)
        cout << number << "  ";
    else
        cout << number << " ";

}

//==================================//

void printMultiplicationTable() {

    printMultiplicationTableHeader();

    for (int i = 1; i <= 10; ++i) {

        columnFormater(i);
        for(int j = 1; j <= 10; ++j) {
            multiplicationResultsFormatter(i * j);
        }
        cout << endl;

    }

}

//==================================//

void printAllPrimeNumbersFrom1toN(int endPoint) {

    int counter = 1;

    cout << " >> All Prime numbers between 1 and " << endPoint << ":" << endl;
    for(int i = 1; i <= endPoint; ++i) {

        if ( isPrimeNumber(i) ) {
            columnFormater(counter);
            cout << " -- [ " << i << " ] is a prime number." << endl;
            ++counter;
        }

    }

}

void printIsPerfectOrNot(int number) {

    if( isPerfectNumber(number) )
        cout << "--» [ " << number << " ] is a perfect number." << endl;
    else
        cout << "--» [ " << number << " ] isn't a perfect number." << endl;

}

void printAllPerfectNumbersFrom1toN(int endPoint) {

    for(int i = 1; i <= endPoint; ++i) {
        printIsPerfectOrNot(i);
    }

}


int getNumberSize(int number) {

    int size = 1;

    while(number >= 10) {
        ++size;
        number /= 10;
    }

    return size;
}

string convertIntegerToString(int number) {

    return to_string(number);
}

void printDigitsInOrder(int number) {

    short remindDigit;
    
    while(number > 0) {
    
    remindDigit = number % 10;
    cout << " " << remindDigit << endl;
    number /= 10;
    
    }

}

void printDigitsInReversedOrder(int number) {

    int numberSize = getNumberSize(number);
    string numberDigits = convertIntegerToString(number);

    for(int i = numberSize - 1; i >= 0; --i) {
        cout << " " << numberDigits[i] << endl;
    }

}

int calculateTheSumOfNumberDigits(int number) {

    int sum = 0, remindDigit;

    while (number > 0) {

        remindDigit = number % 10;
        sum += remindDigit;
        number /= 10;

    }

    return sum;
}


void printTheReverseOfANumber(int number) {

    int remindDigit;

    while (number > 0) {

        remindDigit = number % 10;
        cout << remindDigit;
        number /= 10;

    }

}

/*
int getDigitFrequencyInANumber(int number, int digitToCount) {

    int remindDigit, frequency = 0;

    while(number > 0) {

        remindDigit = number % 10;

        if(remindDigit == digitToCount)
            ++frequency;

        number /= 10;
    }

    return frequency;
}
*/
void printNumberDigitsFrequency(int number) {

    int digitFrequency;

    for(int digit = 0; digit <= 9; ++digit) {

        digitFrequency = 0;
       // digitFrequency = getDigitFrequencyInANumber(number, digit);

        if(digitFrequency > 0)
            cout << "--» The digit [ " << digit << " ] frequency is [ " << digitFrequency << " ]"<< endl;

    }

}

int main()
{

    /*
    //--------------------------------------------------------//

    cout << "---+ Multiplication Table Form 1 To 10 +---" << endl;
    cout << "\n===========================================\n" << endl;
    printMultiplicationTable();
    cout << "\n===========================================\n" << endl;

    //--------------------------------------------------------//

    int endPoint;

    cout << "--+ Print All Prime Numbers From 1 to N +--" << endl;
    cout << "\n===========================================\n" << endl;
    readAPositiveIntegerNumber("Please enter a positive number: ", endPoint);
    printAllPrimeNumbersFrom1toN(endPoint);
    cout << "\n===========================================\n" << endl;

    //--------------------------------------------------------//

    int number;

    cout << "\n--+ Check If A Number Is Perfect Or Not +--\n" << endl;
    cout << "\n===========================================\n" << endl;
    readAPositiveIntegerNumber("--» Please enter a positive number: ", number);
    printIsPerfectOrNot(number);
    cout << "\n===========================================\n" << endl;

    //--------------------------------------------------------//

    int endPoint;

    cout << "\n--+ Get All Perfect Numbers From 1 to N +--\n" << endl;
    cout << "\n===========================================\n" << endl;
    readAPositiveIntegerNumber("--» Please enter a positive number: ", endPoint);
    printAllPerfectNumbersFrom1toN(endPoint);
    cout << "\n===========================================\n" << endl;



    int number;

    cout << "\n--+ Digits Of A Number In Reversed Order +--\n" << endl;
    cout << "\n===========================================\n" << endl;
    readAPositiveIntegerNumber("--» Please enter a positive number: ", number);
    printDigitsInReversedOrder(number);
    cout << "\n===========================================\n" << endl;



    int number;

    cout << "\n---------+ Sum Of Number Digits +---------\n" << endl;
    cout << "\n___________________________________________\n" << endl;
    readAPositiveIntegerNumber("--» Please enter a positive number: ", number);
    printAndDisplayAMessage("--» The sum of number digits is: [ " + to_string(calculateTheSumOfNumberDigits(number)) + " ]");
    cout << "\n___________________________________________\n" << endl;

    int number;

    cout << "\n-----------+ Reverse A Number +------------\n" << endl;
    cout << "\n___________________________________________\n" << endl;
    readAPositiveIntegerNumber("--» Please enter a positive number: ", number);
    printTheReverseOfANumber(number);
    cout << "\n___________________________________________\n" << endl;


    int number, digitToCount;

    cout << "\n--+ Count A Digit Frequency In A Number +--\n" << endl;
    cout << "\n___________________________________________\n" << endl;
    readAPositiveIntegerNumber("--» Please enter a positive number: ", number);
    readAnIntegerNumberInRange("--» Please enter a digit (0 - 9): ", digitToCount, 0, 9);
    printAndDisplayAMessage("--» The digit [ " + to_string(digitToCount) + " ] exits in the number [ " + to_string(number) + " ] { " + to_string( getDigitFrequencyInANumber( number, digitToCount) ) + " } times !");
    cout << "\n___________________________________________\n" << endl;


    int number;

    cout << "\n---+ Count All Number Digits Frequency +---\n" << endl;
    cout << "\n___________________________________________\n" << endl;
    readAPositiveIntegerNumber("--» Please enter a positive number: ", number);
    printNumberDigitsFrequency(number);
    cout << "\n___________________________________________\n" << endl;

    */
    
    int number = 3;
    number *= 3 + 5;
    void* pointer = &number;
    cout << "\n------+ Digits Of A Number In Order +------\n" << endl;
    cout << "\n===========================================\n" << endl;
    //readAPositiveIntegerNumber("--» Please enter a positive number: ", number);
   // printDigitsInReversedOrder(number);
    cout << ( (9 > 12) ? "V.O.N" : "0912"  ) << endl;
    cout << "\n===========================================\n" << endl;
    
    return 0;
}



    