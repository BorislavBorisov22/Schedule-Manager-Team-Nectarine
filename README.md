# Schedule-Manager-Team-Nectarine (Teamwork project for Telerik Academy C Sharp OOP course)

The purpose of this program is to serve as a calendar scheduling application for businesses with commonly recurring activities of predefined type as the call-centers for example.

The program consists of … classes, … interfaces, … enumerations and ….. that implement the logic of the application. The classes are organized in groups and their role as building blocks of the application is as follows:

## 1.	The Users group contains:
 -	Basic *abstract class User* that implements the *ILoggable interface* for logging into the system. There are rules for validation of the username and the password. The username must not be shorter than 5 symbols and must contain only digits, letters, dot and low dash. The password should contain at least 1 letter and 5 digits.
 -	The user types are given with the *enumeration UserType* – there are 4 user types: Admin, RegularWorker, TeamLeader and Unknown.
 -	The *GuestUser class* inherits the User class and implements the ILoggable interface – this type of user can only have username and password, and view company info.
 -	The users that work for the company can have different contract types with the company – this is also given with enumeration. It is named ContractType and the contract types are defined as FullTime, PartTime6Hours and PartTime4Hours.
 -	The Employee class is also abstract. It inherits the User class and implements ILoggable and IEmployee interfaces – this class allows the users to be given first name and last neme, which are related to specific username and password. Also this class is serializable, i.e. it could be converted to string in order to be used in other parts of the programme.
 -	The Worker class inherits the Employee class and implements the interfaces ILoggable, IEmployee and ISchedulable. The last of the described interfaces serves for adding and removing events in the user calendar. This means that the worker can schedule (add or remove) events by giving day, month, year, start and end for the event. Also event type could be selected from the EventType enumeration, which could be Work, Break, OffDuty, Absent, OnHoliday, Sick, Lunch, InTraining, Meeting.
 -	RegularWorker, TeamLeaderWorker and Administrator classes inherit the Employee class – regular worker could be a team member, and the team leader additionally could add or release members from the team. The team leader can add or remove events in the team calendar, while the administrator can access and work with the company calendar.
## 2.	The Calendars group contains:
- Calendars group...
## 3.	…
- next
