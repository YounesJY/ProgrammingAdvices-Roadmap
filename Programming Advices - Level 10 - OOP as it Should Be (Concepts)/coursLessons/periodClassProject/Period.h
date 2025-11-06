#pragma once
#include "../../../../HeaderFiles/DateFullEdition.h"

class Period {
public:

	DateFullEdition StartDate;
	DateFullEdition EndDate;

	Period(DateFullEdition StartDate, DateFullEdition DateTo) {
		this->StartDate = StartDate;
		this->EndDate = DateTo;

	}

	static bool IsOverlapPeriods(Period Period1, Period Period2) {

		if (
			DateFullEdition::compareDates(Period2.EndDate, Period1.StartDate) == DateFullEdition::enDateCompare::Before
			||
			DateFullEdition::compareDates(Period2.StartDate, Period1.EndDate) == DateFullEdition::enDateCompare::After
			)
			return false;
		else
			return true;
	}


	bool IsOverLapWith(Period Period2) {
		return IsOverlapPeriods(*this, Period2);
	}

	void Print() {
		cout << "Period Start: ";
		StartDate.print();

		cout << "Period End: ";
		EndDate.print();
	}
};
