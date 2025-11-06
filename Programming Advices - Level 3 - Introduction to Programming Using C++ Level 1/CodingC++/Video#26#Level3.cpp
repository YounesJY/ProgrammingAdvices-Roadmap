#include<iostream>
using namespace std;

int main()
{
    //// --> Level 3 #26
/*
    // --> Problem #7 Half of a number
    float number;

    cout << "-------- Get The Half Of A Number --------\n" << endl;
    cout << "\n=========================================\n" << endl;
    cout << "--> Please enter a number: ";
    cin >> number;
    cout << " +> The half of " << number << " is: " << number / 2 << endl;
    cout << "\n=========================================\n" << endl;

    // -----------------------------------------------------------------------------

    // --> Problem #9 Sum of three numbers
    float number1, number2, number3;

    cout << "------- Get The Sum Of Three Numbers ------\n" << endl;

    cout << "\n=========================================\n" << endl;
    cout << "--> Please enter: " << endl;
    cout << " +> The first number: ";
    cin >> number1;
    cout << " +> The second number: ";
    cin >> number2;
    cout << " +> The third number: ";
    cin >> number3;
    cout << "  - The sum of [ " << number1 << ", " << number2 << ", " << number3 << " ] = " << number1 + number2 + number3 << endl;
    cout << "\n=========================================\n" << endl;

    // -----------------------------------------------------------------------------

    // --> Problem #10 The average of three marks
    float Mark1, Mark2, Mark3;

    cout << "------ Get The Average Of Three Marks -----\n" << endl;
    cout << "\n=========================================\n" << endl;
    cout << "--> Please enter: " << endl;
    cout << " +> The first mark: ";
    cin >> Mark1;
    cout << " +> The second mark: ";
    cin >> Mark2;
    cout << " +> The third mark: ";
    cin >> Mark3;
    cout << "  - The average of [" << Mark1 << ", " << Mark2 << ", " << Mark3 << "] = " << (Mark1 + Mark2 + Mark3) / 3 << endl;
    cout << "\n=========================================\n" << endl;

    // -----------------------------------------------------------------------------

    // --> Problem #14 Swap numbers
    float number1, number2, tempSwap;

    cout << "--------------+ Swap Numbers +-------------\n" << endl;
    cout << "\n=========================================\n" << endl;
    cout << "--> Please enter the first number: ";
    cin >> number1;
    cout << "--> Please enter the first number: ";
    cin >> number2;

    cout << "\n#############################\n" << endl;
    cout << "    ---- Before Swapping ----    \n" << endl;
    cout << "  - First number : " << number1 << endl;
    cout << "  - Second number: " << number2 << endl << endl;

    tempSwap = number1;
    number1 = number2;
    number2 = tempSwap;

    cout << "    ---- After Swapping ----    \n" << endl;
    cout << "  - First number : " << number1 << endl;
    cout << "  - Second number: " << number2 << endl;
    cout << "\n#############################\n" << endl;

    cout << "\n=========================================\n" << endl;

    // -----------------------------------------------------------------------------

    // -->  Problem #15 Get the rectangle Are
    unsigned int Height, Width;

    cout << "---------+ Get The Rectangle Area +--------\n" << endl;

    cout << "\n=========================================\n" << endl;
    cout << "--> Please enter the height value: ";
    cin >> Height;
    cout << "--> Please enter the width value: ";
    cin >> Width;
    cout << "  - The area of rectangle is: " << Height << " * " << Width << " = " << Height * Width << endl;
    cout << "\n=========================================\n" << endl;

    // -----------------------------------------------------------------------------

    // -->  Problem #17 Get the triangle area
    unsigned int Base, Height;

    cout << "---------+ Get The Triangle Area +--------\n" << endl;

    cout << "\n=========================================\n" << endl;
    cout << "--> Please enter the base value: ";
    cin >> Base;
    cout << "--> Please enter the height value: ";
    cin >> Height;
    cout << "  - The area of triangle is: (" << Height << " * " << Base << ") / 2 = " << (Height * Base) / 2 << endl;
    cout << "\n=========================================\n" << endl;

    // -----------------------------------------------------------------------------

    //-->  Problem #19 Get the circle area via diameter
    const float PI = 3.14;
    unsigned int Diameter;

    cout << "------+ Get The Circle Area Via Diameter +-----\n" << endl;

    cout << "\n=========================================\n" << endl;
    cout << "--> Please enter the diameter value: ";
    cin >> Diameter;
    cout << "  - The area of circle is: " << (PI * ( Diameter * Diameter)) / 4 << endl;
    cout << "\n=========================================\n" << endl;

    // -----------------------------------------------------------------------------

    // --> Problem #20 circle area inscribed in a square
    const float PI = 3.14;
    unsigned int ValueofA;

    cout << "+Get The Are Of A Circle Inscribed In A Square+\n" << endl;

    cout << "\n=========================================\n" << endl;
    cout << "--> Please enter the value Of A: ";
    cin >> ValueofA;
    cout << "  - The area of circle is: " << (PI * ( ValueofA * ValueofA)) / 4 << endl;
    cout << "\n=========================================\n" << endl;

    // -----------------------------------------------------------------------------

    // --> Problem #21 circle area along the circumference
    const float PI = 3.14;
    float circleCircumference;

    cout << "--+ Circle Area Along the Circumference +--\n" << endl;
    cout << "\n=========================================\n" << endl;
    cout << "--> Please enter the value of circumference: ";
    cin >> circleCircumference;
    cout << "  - The area of circle is: " << (circleCircumference * circleCircumference) / (4 * PI) << endl;
    cout << "\n=========================================\n" << endl;

    // -----------------------------------------------------------------------------

    // --> Problem #22 Circle area inscribed in an isosceles triangle
    const float PI = 3.14;
    float A, B, Area;
    cout << "+ Get Circle Area Inscribed In An Isosceles Triangle +\n" << endl;

    cout << "\n=========================================\n" << endl;
    cout << "--> Please enter: " << endl;
    cout << "  - Value of A : ";
    cin >> A;
    cout << "  - Value of B : ";
    cin >> B;

    Area = (PI * (B * B) / 4) * ((2 * A - B) / (2 * A + B));

    cout << "  - The area of triangle is: " << Area << endl;
    cout << "\n=========================================\n" << endl;

    // -----------------------------------------------------------------------------

    // --> Problem #31 Get power of ^2, ^3, ^4
    int number,Power2, Power3, Power4;

    cout << "--------+ Get power of ^2, ^3, ^4 +--------\n" << endl;

    cout << "\n=========================================\n" << endl;
    cout << "--> Please enter a number: ";
    cin >> number;

    Power2 = number * number;
    Power3 = Power2 * number;
    Power4 = Power3 * number;

    cout << "  - [" << number << " ^ 2] = " << Power2 << endl;
    cout << "  - [" << number << " ^ 3] = " << Power3 << endl;
    cout << "  - [" << number << " ^ 4] = " << Power4 << endl;
    cout << "\n=========================================\n" << endl;

    // -----------------------------------------------------------------------------

    // --> Problem #35 Piggy bank calculator

    unsigned short int numOfPennys, numOfNickels, numOfDimes, numQuarters, numOfDollars;
    float totalOfPennys, totalOfDollars;

    cout << "-+ Get The Number Of Pennys And Dollars +-\n" << endl;

    cout << "\n=========================================\n" << endl;
    cout << "--> Please enter number of: " << endl;
    cout << "  - Pennys: ";
    cin >> numOfPennys;
    cout << "  - Nickel : ";
    cin >> numOfNickels;
    cout << "  - Dimes: ";
    cin >> numOfDimes;
    cout << "  - Quarters : ";
    cin >> numQuarters;
    cout << "  - Dollars: ";
    cin >> numOfDollars;

    totalOfPennys = numOfPennys + numOfNickels * 5 + numOfDimes * 10 + numQuarters * 25  + numOfDollars * 100;
    totaleOfDollars = totalOfPennys / 100;

    cout << " => Total number of pennys is : " << totalOfPennys << endl;
    cout << " => Total number of dollars is: " << totalOfDollars << endl;
    cout << "\n=========================================\n" << endl;

    // -----------------------------------------------------------------------------

    // --> Problem #39 Pay remainder
    int totalBill, cashPaid;

    cout << "----+ Get The Remainder Of Cash Payd +----\n" << endl;

    cout << "\n=========================================\n" << endl;
    cout << "--> Please enter the total bill: ";
    cin >> totalBill;
    cout << "--> Please enter the cash paid: ";
    cin >> cashPaid;
    cout << "  - The remainder is: " << cashPaid - totalBill << endl;
    cout << "\n=========================================\n" << endl;

    // -----------------------------------------------------------------------------

    // --> Problem #40 Service free and sales tax
    float totalBill;

    cout << "----------+ Get The Totale Bill +----------\n" << endl;
    cout << "\n=========================================\n" << endl;
    cout << "--> Please enter the total bill: ";
    cin >> totalBill;

    totalBill = totalBill * 1.1;  // totalBill + 10%
    totalBill = totalBill * 1.16; // totalBill + 16%

    cout << "  - The total Bill is: " << totalBill << endl;
    cout << "\n=========================================\n" << endl;

    // -----------------------------------------------------------------------------

    // --> Problem #41 Calculate the numberoof days and weeks

    unsigned int numOfHours, numOfDays, numOfWeeks;

    cout << "---+ Get The Number Of Days And Weeks +---\n" << endl;
    cout << "\n=========================================\n" << endl;
    cout << "--> Please enter the total number of hours: ";
    cin >> numOfHours;

    numOfDays = numOfHours / 24 ;
    numOfWeeks = numOfDays / 7;

    cout << "\n => With " << numOfHours << " hours you will get:" << endl;
    cout << "  - " << numOfDays << " Days." << endl;
    cout << "  - " << numOfWeeks << " Weeks." << endl;
    cout << "\n=========================================\n" << endl;

    // -----------------------------------------------------------------------------

    // --> Problem #42 Task duration in seconds

    int numOfDays, numOfHours, numOfMinutes, numOfSeconds, totalDurationInSeconds;

    cout << "-----+ Get Task Duration In Seconds +-----\n" << endl;

    cout << "\n=========================================\n" << endl;
    cout << "--> Please enter the number of:" << endl;
    cout << " +> The number of days: ";
    cin >> numOfDays;
    cout << " +> The number of hours: ";
    cin >> numOfHours;
    cout << " +> The number of minutes: ";
    cin >> numOfMinutes;
    cout << " +> The number of seconds: ";
    cin >> numOfSeconds;

    totalDurationInSeconds = numOfSeconds + numOfMinutes * 60 + numOfHours * 3600 + numOfDays * 24 * 3600;

    cout << "\n => The total task duration is: " << totalDurationInSeconds << " seconds." << endl;
    cout << "\n=========================================\n" << endl;

    // -----------------------------------------------------------------------------

    // --> Problem #43 seconds to (D, H, M ,S)
    unsigned int numOfSeconds, numOfMinutes, numOfHours, numOfDays;

    cout << "-----+ Seconds to ( D, H, M ,S) +-----\n" << endl;
    cout << "\n=========================================\n" << endl;
    cout << "--> Please enter the total number of seconds: ";
    cin >> numOfSeconds;

    numOfDays = numOfSeconds / (24 * 3600);
    numOfSeconds = numOfSeconds - (numOfDays * 24 * 3600);

    numOfHours = numOfSeconds / 3600;
    numOfSeconds = numOfSeconds - (numOfHours * 3600);

    numOfMinutes = numOfSeconds / 60;
    numOfSeconds = numOfSeconds - (numOfMinutes * 60);

    cout << "\n => The results is : [ " << numOfDays << ":" << numOfHours << ":" << numOfMinutes << ":" << numOfSeconds << " ]" << endl;
    cout << "\n=========================================\n" << endl;

    // -----------------------------------------------------------------------------

    // --> Problem #47 Loan instalment months
    unsigned int loanAmount, monthlyPayment;

    cout << "----+ Loan Amount And Monthly Payment +----\n" << endl;

    cout << "\n=========================================\n" << endl;
    cout << "--> Please enter the loan amount: ";
    cin >> loanAmount;
    cout << "--> Please enter the monthly payment: ";
    cin >> monthlyPayment;
    cout << "\n => Number of months needed to settle the loan is: [ " << loanAmount / monthlyPayment << " months. ]" << endl;
    cout << "\n=========================================\n" << endl;

    // -----------------------------------------------------------------------------

    // --> Problem #48 Monthly loan instalment
    unsigned int loanAmount, numOfMonths;

    cout << "----+ Loan Amount And Monthly Payment +----\n" << endl;

    cout << "\n=========================================\n" << endl;
    cout << "--> Please enter the loan amount: ";
    cin >> loanAmount;
    cout << "--> Please enter the number of months you need to settle the loan: ";
    cin >> numOfMonths;
    cout << "\n => To settle all the loan, your monthly payment will be: [ " << loanAmount / numOfMonths << " ]" << endl;
    cout << "\n=========================================\n" << endl;


    // --> Video #30 Relational operators

    int number1, number2;

    cout << "---------+ Compare Two Numbers +---------\n" << endl;

    cout << "\n=========================================\n" << endl;
    cout << "--> Please enter the first number: ";
    cin >> number1;
    cout << "--> Please enter the second number: ";
    cin >> number2;

    cout << "\n  - " << number1 << " == " << number2 << " -> [ " << (number1 == number2) << " ]." << endl;
    cout << "  - " << number1 << " != " << number2 << " -> [ " << (number1 != number2) << " ]." << endl;
    cout << "  - " << number1 << " >  "  << number2 << " -> [ " << (number1 > number2)  << " ]." << endl;
    cout << "  - " << number1 << " <  "  << number2 << " -> [ " << (number1 < number2)  << " ]." << endl;
    cout << "  - " << number1 << " >= " << number2 << " -> [ " << (number1 >= number2) << " ]." << endl;
    cout << "  - " << number1 << " <= " << number2 << " -> [ " << (number1 <= number2) << " ]." << endl;
    cout << "\n=========================================\n" << endl;



    //// --> Video #31 Logical operators
    cout << "-------+ Binary Logical Operators +-------- \n" << endl;

    cout << "\n=========================================\n" << endl;
    cout << "> [ " << (12 >= 12)  << " ]\t [ " << !(12 >= 12)  << " ]\t [ " << (1 && 1)    << " ]\t [ " << ((7 == 7) && (7 > 5))  << " ] ." << endl;
    cout << "> [ " << (12 > 7)    << " ]\t [ " << !(12 > 7)    << " ]\t [ " << (true && 0) << " ]\t [ " << ((7 == 7) && (7 < 5))  << " ] ." << endl;
    cout << "> [ " << (8 < 6)     << " ]\t [ " << !(8 < 6)     << " ]\t [ " << (0 || 1)    << " ]\t [ " << ((7 == 7) || (7 < 5))  << " ] ." << endl;
    cout << "> [ " << (8 == 8)    << " ]\t [ " << !(8 == 8)    << " ]\t [ " << (0 || 0)    << " ]\t [ " << ((7 < 7) || (7 > 5))   << " ] ." << endl;
    cout << "> [ " << (12 <= 12)  << " ]\t [ " << !(12 <= 12)  << " ]\t [ " << !(0)        << " ]\t [ " << (!(7 == 7) && (7 > 5)) << " ] ." << endl;
    cout << "> [ " << (7 == 5)    << " ]\t [ " << !(7 == 5)    << " ]\t [ " << !(1 || 0)   << " ]\t [ " << ((7 == 7) && !(7 < 5)) << " ] ." << endl;
    cout << "\n=========================================\n" << endl;
*/

    //// --> Video #31 Logical operators
    cout << "-------+ Binary Logical Operators +-------- \n" << endl;
    cout << "\n=========================================\n" << endl;
    cout << "-> [ " << ((5 > 6 && 7 == 7) || (1 || 0)) 					  << " ] ." << endl;
    cout << "-> [ " << (!(5 > 6 && 7 == 7) || (1 || 0)) 					 << " ] ." << endl;
    cout << "-> [ " << (!(5 > 6 && 7 == 7) || !(1 || 0)) 					<< " ] ." << endl;
    cout << "-> [ " << (!(5 > 6 && 7 == 7) && !(1 || 0)) 					<< " ] ." << endl;
    cout << "-> [ " << (((5 > 6 && 7 <= 8) || (8 > 1 && 4 <= 3)) && true)    << " ] ." << endl;
    cout << "-> [ " << (((5 > 6 && !(7 <= 8)) && (8 > 1 || 4 <= 3)) || true) << " ] ." << endl;
    cout << "\n=========================================\n" << endl;
	


    return 0;
}



    