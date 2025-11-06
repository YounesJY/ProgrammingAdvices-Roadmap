#include<iostream>
using namespace std;

class Person {

private:
    int _id;
    string _firstName;
    string _lastName;
    string _email;
    string _phoneNumber;

public:
    Person(int id, string firstName, string lastName, string email, string phoneNumber) {
        _id = id;
        _firstName = firstName;
        _lastName = lastName;
        _email = email;
        _phoneNumber = phoneNumber;
    }

    int getId() {
        return _id;
    }
    string getFirstName() {
        return _firstName;
    }
    void setFirstName(string firstName) {
        _firstName = firstName;
    }
    string getLastName() {
        return _lastName;
    }
    void setLastName(string lastName) {
        _lastName = lastName;
    }
    string getFullName() {
        return ( _firstName + " " + _lastName );
    }
    string getEmail() {
        return _email;
    }
    void setEmail(string email) {
        _email = email;
    }
    string getPhoneNumber() {
        return _phoneNumber;
    }
    void setPhoneNumber(string phoneNumber) {
        _phoneNumber = phoneNumber;
    }

    void sendEmail(string subject, string body) {
        cout << " -> The following message sent successfully to the email [ " << _email << " ]" << endl;
        cout << " -- Subject: " << subject << endl;
        cout << " -- Body   : " << body << endl;
    }
    void sendSMS(string message) {
        cout << " -> The following sms sent successfully to the phone number [ " << _phoneNumber << " ]" << endl;
        cout << " -- Message: " << message << endl;
    }
    void printInformations() {
        cout << "\n----------+ Person Informations +----------\n" << endl;
        cout << "==================================" << endl;
        cout << "--> ID          : " << _id << endl;
        cout << "--> First Name  : " << _firstName << endl;
        cout << "--> Last Name   : " << _lastName << endl;
        cout << "--> Full Name   : " << getFullName() << endl;
        cout << "--> Email       : " << _email << endl;
        cout << "--> Phone Number: " << _phoneNumber << endl;
        cout << "==================================" << endl;
    }

};


class Employee : public Person {

private:
    string _title;
    float  _salary;
    string _department;

public:
    Employee(int id, string firstName, string lastName, string title, string email, string phoneNumber, float salary, string department)
        : Person(id, firstName, lastName, email, phoneNumber) {
        _title = title;
        _salary = salary;
        _department = department;
    }

    string getTitle() {
        return _title;
    }
    void setTitle(string title) {
        _title = title;
    }
    float getSalary() {
        return _salary;
    }
    void setSalary(float salary) {
        _salary = salary;
    }
    string getDepartment() {
        return _department;
    }
    void setDepartment(string department) {
        _department = department;
    }

    void printInformations() {
        cout << "\n----------+ Employee Informations +--------\n" << endl;
        cout << "==================================" << endl;
        cout << "--> ID          : " << getId() << endl;
        cout << "--> First Name  : " << getFirstName() << endl;
        cout << "--> Last Name   : " << getLastName() << endl;
        cout << "--> Full Name   : " << getFullName() << endl;
        cout << "--> Title       : " << _title << endl;
        cout << "--> Email       : " << getEmail() << endl;
        cout << "--> Phone Number: " << getPhoneNumber() << endl;
        cout << "--> Salary      : " << _salary << endl;
        cout << "--> Department  : " << _department << endl;
        cout << "==================================" << endl;
    }

};

class Developer : public Employee {
private:
    string _mainProgrammingLanguage;

public:
    Developer(int id, string firstName, string lastName, string title, string email, string phoneNumber, float salary, string department,string mainProgrammingLanguage)
        : Employee(id, firstName, lastName, title, email, phoneNumber, salary, department) {
        _mainProgrammingLanguage = mainProgrammingLanguage;
    }

    string getMainProgrammingLanguage() {
        return  _mainProgrammingLanguage;
    }
    void setMainProgrammingLanguage(string mainProgrammingLanguage) {
        _mainProgrammingLanguage = mainProgrammingLanguage;
    }
    void printInformations() {
        cout << "\n----------+ Developer Informations +-------\n" << endl;
        cout << "==================================" << endl;
        cout << "--> ID          : " << getId() << endl;
        cout << "--> First Name  : " << getFirstName() << endl;
        cout << "--> Last Name   : " << getLastName() << endl;
        cout << "--> Full Name   : " << getFullName() << endl;
        cout << "--> Title       : " << getTitle() << endl;
        cout << "--> Email       : " << getEmail() << endl;
        cout << "--> Phone Number: " << getPhoneNumber() << endl;
        cout << "--> Salary      : " << getSalary() << endl;
        cout << "--> Department  : " << getDepartment() << endl;
        cout << "--> Main Programming Language: " << _mainProgrammingLanguage << endl;
        cout << "==================================" << endl;
    }

};

int main()
{
    Developer me(12, "Jay", "You", "Some title", "jayyou@gmail.com", "06x823893", 267272.2, "Software Development", "C++");

    me.printInformations();
    //me.sendEmail("making this exercise dynamic", "it is totally simple to make this exercise dynamic I can't do it I really did it before so there is no reason to make it again I need to go to watch the next video");
    //me.sendSMS("into sent a message to your mom call it again");



    return 0;
}

    