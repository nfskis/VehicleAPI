# VehicleAPI

Implement a RESTful API to track vehicles location. The device will update the API every 30 seconds with the
new location.
  - Register a vehicle
  - Record its position

When the frontend team will implement the backoffice for the vehicle tracking, any authenticated administrator
should be able to :

  - Retrieve the current position of a vehicle
  - Retrieve the positions of a vehicle during a certain time, in order to display their journey on a map
(maps drawing is out of scope)

The solutions used framework:
  - .Net 5.0


# Account

## [POST] /api/Account/register
  - Role-based authorization
    
    - NONE

  - Request body    
  
    Variable                |	Description
    |----|---|
    firstName [string]	  | user first name
    lastName [string]	    | user last name
    email [string]	      | user email address
    password [string]	    | password 
    roleID [string]	      | Role index number
  
  - Return      
  
    NONE

## [POST] /api/Account/Login
  
 - Role-based authorization
    - NONE

  - Request body    

    Variable                |	Description
    |----|---|
    email [string]	      | user email address
    password [string]	    | password 
        
  - Return 

    ```JSON
    {
      "userSeqID": "6f0a003d-ac42-4914-b041-b4df7f52cc75",
      "firstName": "Insung",
      "lastName": "Kim",
      "email": "admin@gmail.com",
      "role": "Admin",
      "roleID": 0,
      "password": null
    }
    ```
### [POST] /api/Account/logOut
  
 - Role-based authorization
    - Required login 
    
  - Request body  
  
    NONE
  
  - Return  

    NONE

# Track

## [POST] /api/track/register

 - Role-based authorization
    - Required login 
    - User / Admin level.

  - Request body    

    Variable                |	Description
    |----|---|
    vehicleSeqID[string]        | Vehicle SeqID
    Latitude[number($double)]   | Latitude of Vehicle location 
    Longitude[number($double)]  | Longitude of Vehicle location 

  - Return  
    
    NONE

## [GET] /api/track/range

 - Role-based authorization
    - Required login 
    - Admin level.

  - Request variables    

    Variable                |	Description
    |----|---|
    vehicleSeqID[string]            | Vehicle SeqID
    startTime[string($date-time)]   | Start Time [2014-04-14 00:00:00.000]
    endTime[string($date-time)]     | End Time [2014-04-14 00:00:00.000]

  - Return  

    ```JSON
      [
        {
          "vehicleSeqID": "0188ae2f-3d3d-4435-a743-7a549c888b54",
          "trackSeqID": "f1187544-393a-4dd3-a922-907dffc3c8a9",
          "plateNumber": "8989",
          "latitude": 13.825103,
          "longitude": 100.99049,
          "googleMapAddress": "Unnamed Road Tambon Bang Nam Priao, Amphoe Bang Nam Priao, Chang Wat Chachoengsao 24150, Thailand",
          "createdDate": "2021-04-15T15:31:51.783"
        },
        {
          "vehicleSeqID": "0188ae2f-3d3d-4435-a743-7a549c888b54",
          "trackSeqID": "e42d2a72-f582-4838-b162-92641e65ae0f",
          "plateNumber": "8989",
          "latitude": 14.699862,
          "longitude": 101.071844,
          "googleMapAddress": "Unnamed Road, Tambon Hin Son, Amphoe Kaeng Khoi, Chang Wat Saraburi 18110, Thailand",
          "createdDate": "2021-04-15T15:31:00.473"
        }
      ]
    ```

## [GET] /api/track/current

 - Role-based authorization
    - Required login 
    - User / Admin level.
    
  - Request variables

    Variable                        |	Description
    |----|---|
    vehicleSeqID[string]            | Vehicle SeqID

  - Return  

    ```JSON
      [
         {
          "vehicleSeqID": "0188ae2f-3d3d-4435-a743-7a549c888b54",
          "trackSeqID": "a59e6e21-495b-497c-aaa2-1d41603bdf1d",
          "plateNumber": "8989",
          "latitude": 11.12312312,
          "longitude": 11.12312312,
          "googleMapAddress": "Jigawa, Nigeria",
          "createdDate": "2021-04-15T20:22:26.143"
        }
      ]
    ```

# Vehicle

## [POST] /api/Vehicle/register
 
 - Role-based authorization
    - Required login 
    - User / Admin level.
    
  - Request variables

    Variable                       |	Description
    |----|---|
    PlateNumber[string]            | Vehicle Plate Number
    Brand[string]                  | Vehicle brand maker 
    Model[string]                  | Vehicle Model name

  - Return  

    NONE

## [GET] /api/vehicle/search

  - Role-based authorization
    - Required login 
    - Admin level.

  - Request variables

    Variable                        |	Description
    |----|---|
    Plate Number[string]            | Vehicle Plate Number

  - Return  

    ```JSON
      [
          {
            "vehicleSeqID": "0188ae2f-3d3d-4435-a743-7a549c888b54",
            "plateNumber": "8989",
            "brand": "Honda",
            "model": "Camry",
            "createdDate": "2021-04-15T15:30:54.287",
            "lastModifiedDate": "2021-04-15T15:30:54.287"
          }
      ]
    ```   


## [GET] /api/vehicle/all

  - Role-based authorization
    
    - Required login
    - Admin level.

  - Request variables

   NONE

  - Return  

    ```JSON
      [
        {
          "vehicleSeqID": "0188ae2f-3d3d-4435-a743-7a549c888b54",
          "plateNumber": "8989",
          "brand": "Honda",
          "model": "Camry",
          "createdDate": "2021-04-15T15:30:54.287",
          "lastModifiedDate": "0001-01-01T00:00:00"
        },
        {
          "vehicleSeqID": "059a6d48-3405-487b-9ed1-2c61bf940765",
          "plateNumber": "5555",
          "brand": "Toyota",
          "model": "Sienna",
          "createdDate": "2021-04-15T15:30:54.29",
          "lastModifiedDate": "0001-01-01T00:00:00"
        }
      ]
    ```   