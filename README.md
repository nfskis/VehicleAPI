# Vehicle API

Implement a RESTful API to track vehicles location. The device will update the API every 30 seconds with the
new location.
  - Register a vehicle
  - Record its position

When the frontend team will implement the backoffice for the vehicle tracking, any authenticated administrator
should be able to :

  - Retrieve the current position of a vehicle
  - Retrieve the positions of a vehicle during a certain time, in order to display their journey on a map
(maps drawing is out of scope)

# Gettting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.

# Prerequisites
What things you need to install the Nuget packages and required Version **.NET 5.0 or Higher** and **SQL Server 2014 or higher**

```  

  Dapper(2.0.78)
  Swashbuckle.AspNetCore(5.6.3)
  System.Configuration.ConfigurationManager(5.0.0)
  System.Data.SqlClient(4.8.2)

```

# Install

- Right click on the **VehicleDBase** Project in Solution Explorer and select Publish.
- A dialog appears allowing you to enter a publish profile, which are settings to use during the publish operation.
- Click the Edit button to specify the SQL Server instance you wish to publish to (target).

  ![check pas script command](https://i.imgur.com/lXCLKjP.png)

# Running the tests

- Execute VehicleAPI.Sln file in Visual studio 2019.
- F5 key to run VehicleAPI project.
- After build a VehilcleAPI and will see as like below swagger website.

  ![check pas script command](https://i.imgur.com/ywpuBrH.png)

- Go to Demo panel 
- Click **/api/demo/setdefault** and Click **Try it out** 
- Web Api will be build a all default values automactially.

  ![check pas script command](https://i.imgur.com/Ip6FW33.png)

- Click **/api/Demo/trackRecordevery30sec** and Click **Try it out**  
- WebApi will be record ramdom 10,000 tracks every 30sec automactially. 

  ![check pas script command](https://i.imgur.com/wUnegdp.png)

- Go to Accout pannel and you can login at **/api/user/login**
- Admin accout login ID is as like blow :
  - ID : admin@gmail.com
  - Password : password

- User accout login ID is as like blow :
  - ID : user@gmail.com
  - Password : password  

- Login as like an Admin as like below 

  ![check pas script command](https://i.imgur.com/MUvgfd1.png)

- You can search tracks after login admin account 
- Go to GET Tracks pannel after then You can get a result will be as like below:

    > This Vehicle track API use to find address from google map API. So, The Google API Key will be expired on end of April  

  ![check pas script command](https://i.imgur.com/HD8uvPV.png)
