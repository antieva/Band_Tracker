# _Band Tracker App_

#### _Band Tracker, 05/11/18_

#### By _**Eva Antipina**_

## Description

_A web app to to track bands and the venues where they've played concerts._
_It will let user to:_
* _be able to create Bands that have played at a Venue;_
* _see the list of all Bands;_
* _see every Band's details, add Venues to that Band or delete the Band;_
* _see all of the Venues that have hosted that band and allow them to add a Venue to that Band;_
* _see a single concert venue, list all of the bands that have played at that venue so far and allow them to add a band to that venue.;_
* _see list all Venues stored in Database;_
* _see every Venue's details, add Bands to that Venue or delete Venue;


## Specifications


## Setup/Installation Requirements

* _Clone or download the repository._
* _Unzip the files into a single directory._
* _Open the Windows PowerShell and move to the directory_
* _Run "dotnet restore" command in the PowerShell._
* _Run "dotnet build" command in the PowerShell._
* _Run "dotnet run" command in the PowerShell._
* _Open a web browser of choice._
* _Enter "localhost:5000/home" into the address bar._

# Add Database to the Project

* _> CREATE DATABASE band_tracker;_
* _> USE band_tracker;_
* _> CREATE TABLE bands (id serial PRIMARY KEY, name VARCHAR(255), genre VARCHAR(255), leader VARCHAR(255), members VARCHAR(255), originPlace VARCHAR(255), originYear VARCHAR(255), agent VARCHAR(255), agentContact VARCHAR(255));_
* _> CREATE TABLE venues (id serial PRIMARY KEY, name VARCHAR(255), address(255), contact VARCHAR(255), eventDate VARCHAR(255));_
* _> CREATE TABLE bands_venues (id serial PRIMARY KEY, band_id INT(11), venue_id INT(11));_

## Known Bugs

_None._

## Support and contact details

_If You run into any issues or have questions, ideas, concerns or would like to make a contribution to the code, please contact me via email: eva.antipina@gmail.com_

## Technologies Used

_C#, HTML, Bootstrap_

### License

*Not licensed.*

Copyright (c) 2018 **_Eva Antipina_**
