#include<iostream>
using namespace std;

void DisplayInfosCard() {
    string Name, Country, City;
    int Age;

    cout << "-----------+ Informations Card +-----------" << endl;
    cout << "\n=======================================\n" << endl;
    cout << "--> Please enter your name: ";
    getline(cin, Name);
    cout << "--> Your Age: ";
    cin >> Age;
    cout << "--> Your country: ";
    cin.ignore(1, '\n');
    getline(cin, Country);
    cout << "--> Your City: ";
    getline(cin, City);

    cout << "\n$````````````````````````````\n";
    cout << " => Name   : [ " << Name	<< " ]" << endl;
    cout << " => Age    : [ " << Age 	<< " ]" << endl;
    cout << " => Country: [ " << Country << " ]" << endl;
    cout << " => City   : [ " << City	<< " ]" << endl;
    cout << "\n$````````````````````````````\n";
    cout << "\n=======================================\n" << endl;

}

void DisplayStarsSquare() {

    cout << "-----------+ Square Of Stars +-------------" << endl;
    cout << "\n=======================================\n" << endl;
    cout << "\n** ** ** ** \n** ** ** ** \n** ** ** ** \n** ** ** ** \n** ** ** ** \n** ** ** ** \n";
    cout << "\n=======================================\n" << endl;

}

void IloveProgramming() {

    cout << "-----------+ Programming Loving+-----------" << endl;
    cout << "\n=======================================\n" << endl;
    cout << "  I love programming! \n" << endl;
    cout << "  I promise to be the best developer ever! \n" << endl;
    cout << "  I know it will take some time to practice, but I will achieve my goals. \n" << endl;
    cout << "  Best regards, \n  JY" << endl;
    cout << "\n=======================================\n" << endl;

}

void PrintJY() {

    cout << "---------------+ Display JY+---------------" << endl;
    cout << "\n=======================================\n" << endl;
    cout << " **********  **             ** " << endl;
    cout << " **********    **    ++    **  " << endl;
    cout << "     **          ***+**+***    " << endl;
    cout << "     **            ******      " << endl;
    cout << "     **              **        " << endl;
    cout << "     **              **        " << endl;
    cout << " **  **              **        " << endl;
    cout << " **  **              **        " << endl;
    cout << "  ****               **        " << endl;
    cout << "\n=======================================\n" << endl;

}

void GetSumProcedure() {

    int num1, num2;

    cout << "-------------+ Sum Procedure +------------" << endl;
    cout << "\n=======================================\n" << endl;
    cout << "--> Please enter the first number: ";
    cin >> num1;
    cout << "--> Please enter the second number: ";
    cin >> num2;

    cout << "\n$````````````````````````````\n";
    cout << " => [ " << num1 << " + " << num2 << " ] = " << num1 + num2 << endl;
    cout << "\n$````````````````````````````\n";
    cout << "\n=======================================\n" << endl;

}



int GetSumFunction() {

    int num1, num2;

    cout << "------------+ Sum Functions +------------" << endl;
    cout << "\n=======================================\n" << endl;
    cout << "--> Please enter the first number: ";
    cin >> num1;
    cout << "--> Please enter the second number: ";
    cin >> num2;
    cout << "\n=======================================\n" << endl;
	cout << "\n$````````````````````````````\n";
    return (num1 + num2);
}



int main()
{
    //DisplayInfosCard();
    //DisplayStarsSquare();
    //IloveProgramming();
    //PrintJY();
    //GetSumProcedure();
    
    //cout << ". [ " << GetSumFunction() << " ]" << endl;
    //cout << "\n$````````````````````````````\n";
    return 0;
}
