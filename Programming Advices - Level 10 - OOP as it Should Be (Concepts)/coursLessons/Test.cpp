#include <iostream>

using namespace std;

class clsEmployee {

    int _ID, _Salary;
    string _FirstName, _LastName, _Title,  _Email,  _Phone, _Department;

public:

    clsEmployee(int ID, string FirstName, string LastName, string Title, string Email, string Phone, string Department)
    {
        _ID = ID;
        _FirstName = FirstName;
        _LastName = LastName;
        _Title = Title;
        _Email = Email;
        _Phone = Phone;
        _Department = Department;

    }

    int ID() {

        return _ID;
    }

    void SetFirstName(string FirstName)
    {
        _FirstName = FirstName;
    }
    string FirstName() {

        return _FirstName;
    }

    void SetLastName(string LastName)
    {
        _LastName = LastName;
    }
    string LastName() {

        return _LastName;
    }

    string FullName()
    {
        return _FirstName + " " + _LastName;
    }

    void SetTitle(string Title)
    {
        _Title = Title;
    }
    string Title()
    {
        return _Title;
    }

    void SetEmail(string Email)
    {
        _Email = Email;
    }
    string Email()
    {
        return _Email;
    }

    void SetPhone(string Phone)
    {
        _Phone = Phone;
    }
    string Phone()
    {
        return _Phone;
    }

    void SetDepartment(string Department)
    {
        _Department = Department;
    }
    string Department()
    {
        return _Department;
    }

    void Print()
    {

        cout << "\nInfo:";
        cout << "\n---------------------------------";
        cout << "\nID   : " << _ID;
        cout << "\nFirstName : " << _FirstName;
        cout << "\nLastName : " << _LastName;
        cout << "\nFullName : " << FullName();
        cout << "\nTitle : " << _Title;
        cout << "\nEmail : " << _Email;
        cout << "\nPhone : " << _Phone;
        cout << "\nSalary : " << _Salary;
        cout << "\nDepartment : " << _Department;
        cout << "\n---------------------------------";


    }

    void SendEmail(string Subject, string Body)
    {
        cout << "\n\nThe following message sent successfully to email : " << _Email;
        cout << "\nSubject: " << Subject;
        cout << "\nBody: " << Body;
    }
    void SendSMS(string TextMessage)
    {
        cout << "\nThe following SMS sent successfully to phone : " << _Phone;
        cout << "\n" << TextMessage;
    }

};


int main()
{
    clsEmployee Employee(13, "Mohamed", "Mazighi", "Helllloooooo", "mazighi0mohamed@gamil.com", "065737534", "Milla");

    Employee.Print();

    Employee.SendEmail("Hi bro", "Let's go gym tomorow");
    Employee.SendSMS("No one");

    system("pause>0");
    return 0;

}