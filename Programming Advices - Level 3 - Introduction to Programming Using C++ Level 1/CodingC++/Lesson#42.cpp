#include<iostream>
using namespace std;

//----------------------------// //--> Problem #4

void ReadInfos(short int &Age, bool &hasDriverLicense) {

    cout << "--> Please enter your age: ";
    cin >> Age;
    cout << "--> Do you have a driver license ? \n(Yes -> 1 / No -> 0): ";
    cin >> hasDriverLicense;

}

void checkIfHired(short int Age, bool hasDriverLicense) {

    if ( (Age > 21) && hasDriverLicense == 1) {
        cout << "\n|`````````````````|\n" << endl;
        cout << " √ Hired" << endl;
        cout << "\n|.................|\n" << endl;

    }
    else {
        cout << "\n|`````````````````|\n" << endl;
        cout << " × Rejected" << endl;
        cout << "\n|.................|\n" << endl;

    }

}

//----------------------------// //--> Problem #8

void ReadMark(float &Mark) {

    cout << "---> Please enter you mark (0 - 100): ";
    cin >> Mark;

}

void checkIfPassed(float Mark) {
    if (Mark >= 50) {
        cout << "\n|``````|\n" << endl;
        cout << " √ Pass" << endl;
        cout << "\n|......|\n" << endl;

    }
    else {
        cout << "\n|``````|\n" << endl;
        cout << " × Fail" << endl;
        cout << "\n|......|\n" << endl;

    }
}

//----------------------------// //--> Problem #11

void ReadMarks(float Marks[3]) {

    cout << "--> Please enter the first mark: ";
    cin >> Marks[0];
    cout << "--> Please enter the second mark: ";
    cin >> Marks[1];
    cout << "--> Please enter the third mark: ";
    cin >> Marks[2];

}

float GetAverage(float Marks[3]) {

    return ( (Marks[0] + Marks[1] + Marks[2]) / 3 );

}

void CheckAverage(float Average) {

    if (Average >= 50) {
        cout << "\n|````````````````````````````|\n" << endl;
        cout << " √ Pass: Average = [ " << Average << " ]" << endl;
        cout << "\n|............................|\n" << endl;

    }
    else {
        cout << "\n|````````````````````````````|\n" << endl;
        cout << " × Fail: Average = [ " << Average << " ]" << endl;
        cout << "\n|............................|\n" << endl;
    }

}

//----------------------------// //--> Problem #24

void ReadUserAge(unsigned short int &userAge) {

    cout << "--> Please enter your age: ";
    cin >> userAge;

}

void ValidateUserAge(unsigned short int userAge) {
    if (userAge >= 18 && userAge <= 45) {
        cout << "\n|````````````````````|\n" << endl;
        cout << " √ Valid Age  : [ " << userAge << " ]" << endl;
        cout << "\n|....................|\n" << endl;
    }
    else {
        cout << "\n|````````````````````|\n" << endl;
        cout << " × Invalid Age: [ " << userAge << " ]" << endl;
        cout << "\n|....................|\n" << endl;

    }

}

//----------------------------// //--> Problem #49

void ReadUserATM_PIN(short int &userInputATM_PIN) {

    cout << "--> Please enter your ATM PIN: ";
    cin >> userInputATM_PIN;

}

void CheckUserATM_PIN(const short int userATM_PIN, short int userInputATM_PIN, float userBalance) {

    if (userInputATM_PIN == userATM_PIN) {
        cout << "\n|```````````````````````````|\n" << endl;
        cout << " √ Your Balance is: [ " << userBalance << " ]" << endl;
        cout << "\n|...........................|\n" << endl;
    }
    else {
        cout << "\n|`````````````````````|\n" << endl;
        cout << " × Wrong PIN: [ " << userInputATM_PIN << " ]" << endl;
        cout << "\n|.....................|\n" << endl;
    }

}

int main()
{

    ////--> Lesson 42
    /*
        //--> Problem #4 Hire a driver
        unsigned short int Age;
        bool hasDriverLicense;

        cout << "+>>>>>>>>>>>>> Hire A Driver <<<<<<<<<<<<<+" << endl;
        cout << "\n===========================================\n" << endl;
        cout << "--> Please enter your age: ";
        cin >> Age;
        cout << "--> Do you have a driver license ? \n(Yes -> 1 / No -> 0): ";
        cin >> hasDriverLicense;

        if ( (Age > 21) && hasDriverLicense == 1) {
        	cout << "\n|`````````````````|\n" << endl;
            cout << " √ Hired" << endl;
            cout << "\n|.................|\n" << endl;

        }
        else {
        	cout << "\n|`````````````````|\n" << endl;
            cout << " × Rejected" << endl;
            cout << "\n|.................|\n" << endl;

        }
        cout << "\n===========================================\n" << endl;

    //-----------------------------------------------------------------------------//
    	//--> Problem #8 Pass fail
    	float Mark;

    	cout << "+>>>>>>>>>>>>>> Pass Testing <<<<<<<<<<<<<+" << endl;
        cout << "\n===========================================\n" << endl;
    	cout << "---> Please enter you mark (0 - 100): ";
        cin >> Mark;

        if (Mark >= 50) {
        	cout << "\n|`````````````````|\n" << endl;
            cout << " √ Pass" << endl;
            cout << "\n|.................|\n" << endl;

        }
        else {
        	cout << "\n|`````````````````|\n" << endl;
            cout << " × Fail" << endl;
            cout << "\n|.................|\n" << endl;

        }
        cout << "\n===========================================\n" << endl;

    //-----------------------------------------------------------------------------//

    	//--> Problem #11 Average pass fail
        float Marks[3], Average;

        cout << "+> Pass Or Fail According To The Average <+" << endl;
        cout << "\n===========================================\n" << endl;
        cout << "--> Please enter the first mark: ";
    	cin >> Marks[0];
        cout << "--> Please enter the second mark: ";
        cin >> Marks[1];
        cout << "--> Please enter the third mark: ";
        cin >> Marks[2];

        Average = (Marks[0] + Marks[1] + Marks[2]) / 3;

        if (Average >= 50) {
        	cout << "\n|````````````````````````````|\n" << endl;
            cout << " √ Pass: Average = [ " << Average << " ]" << endl;
            cout << "\n|............................|\n" << endl;

        }
        else {
        	cout << "\n|````````````````````````````|\n" << endl;
            cout << " × Fail: Average = [ " << Average << " ]" << endl;
            cout << "\n|............................|\n" << endl;
        }
        cout << "\n===========================================\n" << endl;

    //-----------------------------------------------------------------------------//

        //--> Problem #24 Age validation
        unsigned short int userAge;

        cout << "+>>>>>>>>>>>>> Age Validation <<<<<<<<<<<<+" << endl;
        cout << "\n===========================================\n" << endl;
        cout << "--> Please enter your age: ";
        cin >> userAge;

        if (userAge >= 18 && userAge <= 45) {
        	cout << "\n|````````````````````|\n" << endl;
            cout << " √ Valid Age  : [ " << userAge << " ]" << endl;
            cout << "\n|....................|\n" << endl;
        }
        else {
        	cout << "\n|````````````````````|\n" << endl;
            cout << " × Invalid Age: [ " << userAge << " ]" << endl;
            cout << "\n|....................|\n" << endl;

        }
        cout << "\n===========================================\n" << endl;

    //-----------------------------------------------------------------------------//

        //--> Problem #49 Age validation
        const short int userATM_PIN = 1234;
        short int userInputATM_PIN;
        float userBalance = 38838;

        cout << "+>>>>>>>>>>>>>>>> ATM PIN <<<<<<<<<<<<<<<<+" << endl;
        cout << "\n===========================================\n" << endl;
    	cout << "--> Please enter your ATM PIN: ";
        cin >> userInputATM_PIN;

        if (userInputATM_PIN == userATM_PIN) {
        	cout << "\n|```````````````````````````|\n" << endl;
            cout << " √ Your Balance is: [ " << userBalance << " ]" << endl;
            cout << "\n|...........................|\n" << endl;
        }
        else {
        	cout << "\n|`````````````````````|\n" << endl;
            cout << " × Wrong PIN: [ " << userInputATM_PIN << " ]" << endl;
            cout << "\n|.....................|\n" << endl;
        }
        cout << "\n===========================================\n" << endl;
    */

    //-----------------------------------------------------------------------------//
    //-----------------------------------------------------------------------------//
    //-----------------------------------------------------------------------------//
    //-----------------------------------------------------------------------------//
    //-----------------------------------------------------------------------------//
    //-----------------------------------------------------------------------------//
    //-----------------------------------------------------------------------------//
    //-----------------------------------------------------------------------------//
    //-----------------------------------------------------------------------------//
    //-----------------------------------------------------------------------------//
    //-----------------------------------------------------------------------------//
    //-----------------------------------------------------------------------------//
    //-----------------------------------------------------------------------------//
    //-----------------------------------------------------------------------------//
    //-----------------------------------------------------------------------------//
    //-----------------------------------------------------------------------------//
    //-----------------------------------------------------------------------------//
    //-----------------------------------------------------------------------------//
    //-----------------------------------------------------------------------------//
    //-----------------------------------------------------------------------------//

    /*
        //--> Problem #4 Hire a driver
        short int Age;
        bool hasDriverLicense;

        cout << "+>>>>>>>>>>>>> Hire A Driver <<<<<<<<<<<<<+" << endl;
        cout << "\n===========================================\n" << endl;
        ReadInfos(Age, hasDriverLicense);
        checkIfHired(Age, hasDriverLicense);
        cout << "\n===========================================\n" << endl;

    //-----------------------------------------------------------------------------//

        //--> Problem #8 Pass fail
        float Mark;

        cout << "+>>>>>>>>>>>>>> Pass Testing <<<<<<<<<<<<<+" << endl;
        cout << "\n===========================================\n" << endl;
        ReadMark(Mark);
        checkIfPassed(Mark);
        cout << "\n===========================================\n" << endl;

    //-----------------------------------------------------------------------------//

        //--> Problem #11 Average pass fail
        float Marks[3], Average;

        cout << "+> Pass Or Fail According To The Average <+" << endl;
        cout << "\n===========================================\n" << endl;
        ReadMarks(Marks);
        CheckAverage(GetAverage(Marks));
        cout << "\n===========================================\n" << endl;

    //-----------------------------------------------------------------------------//

        //--> Problem #24 Age validation
        unsigned short int userAge;

        cout << "+>>>>>>>>>>>>> Age Validation <<<<<<<<<<<<+" << endl;
        cout << "\n===========================================\n" << endl;
        ReadUserAge(userAge);
        ValidateUserAge(userAge);
        cout << "\n===========================================\n" << endl;

    //-----------------------------------------------------------------------------//

        //--> Problem #49 Age validation
        const short int userATM_PIN = 1234;
        short int userInputATM_PIN;
        float userBalance = 38838;

        cout << "+>>>>>>>>>>>>>>>> ATM PIN <<<<<<<<<<<<<<<<+" << endl;
        cout << "\n===========================================\n" << endl;
        ReadUserATM_PIN(userInputATM_PIN);
        CheckUserATM_PIN(userATM_PIN, userInputATM_PIN, userBalance);
    	cout << "\n===========================================\n" << endl;
    */

    return 0;
}