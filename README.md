# Cost-Tracking-Application

I built this system using the following stack
* HTML, CSS, JavaScript
* C# ASP.NET
* WCF Service
* SQL Database


The aim of the application is to minimize page usage and time taken to record expenditures from school admins, circuit admins and districts

* All the entities(schools, circuits, and districts) are required to register before using the platform

## Functionality for Schools
* Input the amounts they spent on the required fields
* Also the schools are supposed to upload documents to validate the inputs they provided
* The schools can also see the annoucements made by the circuits they are under(e.g., if school A is under circuit B, they cannot see the annoucements made by circuit B)
* They can also download the data they uploaded to the system

## Functionality for Circuit level
* Input the values/amounts spent in that particular month
* Can be able to see how much the schools that fall under them spent (In terms of food scheme, expeditures likely to include food, veges, stipends to cooks, bank charges and more)
* Can make annoucements to the schools that fall under them
* Can see the annoucements made by the district they fall under
* Can download documentation that was uploaded by the schools
* Can also unlink the schools from them (if the school made a mistake of connecting with the wrong circuits)

## Functionality for District level
* It is similar to that of circuits by it has more
* The view can view how much the schools under a certain circuit spend on a certain month (can see the reports from the previous 3 years)

## Functionality for Provincial level
* Overviews all the districts under the them, circuits and schools too.
* Ideally, all the reports from the school level to district level reach the provincial level electronically

## Admin
* Has the SUDO privilenges😁

