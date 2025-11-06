#include<iostream>
using namespace std;



//+++++++++++++++++++++++++++++++++++++++//
//+++++++++++++++++++++++++++++++++++++++//



void checkOfTheNumber(int arrayOfNumbers[]) {

    for(int i = 0; i < 11; ++i) {

        cout << "     ------+ Iteration N.[ " << i + 1 << " ] +--------" << endl;
        if(arrayOfNumbers[i] == 20) {

            cout << "--> Number { " << arrayOfNumbers[i] << " } Found in index [" << i << "] \n" << endl;
            break;

        }
        cout << "  ! Number not found.\n" << endl;
    }

}

void readArrayNumbers(int &arrayNumber, int i) {

    cout << " -- Please enter number N.[" << i + 1 << "]: ";
    cin >> arrayNumber;

}

int calculateTheSum(int array[]) {

    int Sum = 0;

    for(int i = 0; i < 5 ; ++i) {

        readArrayNumbers(array[i], i);
        if (array[i] > 50) {

            cout << "  ! Number is greater than 50,\n So that will not be added to the sum \n" << endl;
            continue;
        }
        Sum += array[i];

    }

    return Sum;
}

void printTheSum(int Sum) {

    cout << "\n|-------------------------| \n" << endl;
    cout << "--> The result is: [ " << Sum << " ]" << endl;
    cout << "\n|-------------------------| \n" << endl;

}



//+++++++++++++++++++++++++++++++++++++++//
//+++++++++++++++++++++++++++++++++++++++//



void readTheNumber(float &number, int i) {

    cout << " -- Please enter number N.[" << i << "]: ";
    cin >> number;

}




float getSum() {

    float number, Sum = 0;

    for(int i = 1; i <= 10; ++i) {

        readTheNumber(number, i);

        if (number  == -99) {
            cout << "  ! Number is -99,\n   So that will not be added to the sum \n" << endl;
            continue;
        }

        Sum += number;

    }

    return Sum;
}



//+++++++++++++++++++++++++++++++++++++++//
//+++++++++++++++++++++++++++++++++++++++//



int main()
{

    /*
        cout << "------+ Find A Number In The Array +------" << endl;
        cout << "\n===========================================\n" << endl;
        int arrayOfNumbers[] = { 18,427,37,28,20,73,63,73,83,72,8 };
        checkOfTheNumber(arrayOfNumbers);
        cout << "\n===========================================\n" << endl;
    


    cout << "------+ Sum Of 5 Numbers Under 50 +------" << endl;
    cout << "\n===========================================\n" << endl;
    int array[5];
    printTheSum(calculateTheSum(array));
    cout << "\n===========================================\n" << endl;

	*/
    
    
    cout << " ----+ Sum Of 15 Numbers Without -99 +----" << endl;
    cout << "\n===========================================\n" << endl;
    printTheSum(getSum());
    cout << "\n===========================================\n" << endl;


    return 0;
}

    