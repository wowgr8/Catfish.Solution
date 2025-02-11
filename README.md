# CatFish

#### By _**Cesar Lopez, Ebru Rice, Jack Skelton, Marni Sucher, Patrick Dolan**_

#### _A web application for pets to find other pets to play with._

## Table of Contents

1. [Previews](#previews)
2. [Technologies Used](#technologies)
3. [Description](#description)
4. [Setup/Installation Requirements](#setup)
5. [Schema](#schema)
6. [Known Bugs](#bugs)
7. [Contact Information](#contact)
8. [License](#license)


## Previews <a id="previews"></a>
Login and browse to find potential play mates: <br>
![General Preview](https://github.com/Patrick-Dolan/Catfish.Solution/blob/main/README_IMAGES/Previews/GeneralPreview.gif)  

Swipe left and right to make matches <br>
![Swipe System](https://github.com/Patrick-Dolan/Catfish.Solution/blob/main/README_IMAGES/Previews/SwipeSystem.gif)  
Check your matches and contact them through email or unmatch if you dont mesh well <br>
![Matches Page](https://github.com/Patrick-Dolan/Catfish.Solution/blob/main/README_IMAGES/Previews/MatchesPage.gif)  

## Technologies Used <a id="technologies"></a>

* C#
* .NET 5.0
* dotnet
* MySql/Workbench
* Bootstrap 5
* JQuery 3.6.0
* Animate.css

## Description <a id="description"></a>

A web application for pets to find other pets to play with. You can create a user account and add, and edit account information, and delete your entire account if you so choose. Checking the browse page will allow you to find new people to match with and if the other user you try to match with wants to match with you they will be added to the matches page.  

## Setup/Installation Requirements <a id="setup"></a>

* Make sure you have MySql Workbench installed on your computer.
* Make sure to have dotnet-ef installed too.<br>
<em>This project uses <code>dotnet-ef --version 3.0.0</code> which I have globally installed but you can install it however you want. 
* Download repo to your computer using either clone or the download link.
* Open the project in VScode or your terminal/IDE of choice.
* Create a <code>appsettings.json</code> file in the root directory of the project folder. And add the following code replacing anything in square brackets with the information it represents specific to the project database:
```
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;database=[DATABASE-NAME-HERE];uid=[USER-ID-HERE];pwd=[YOUR-PASSWORD-HERE];"
  }
}

```

Example of complete appsettings.json:
```
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;database=catfish;uid=root;pwd=MySuperStrongPassword;"
  }
}

```

* Make sure to run your mysql server and open MySql workbench.
* Open MySql Workbench and login to your server.
* From your terminal navigate to the <code>CatFish/</code> folder and run the command <code>dotnet ef database update</code> to create the database from migrations.
* Now using your IDE in the CatFish.Solution/CatFish/ folder use the command <code>dotnet run</code> to launch the program. 
* The site should be available at the server address you used in the <code>appsettings.json</code> folder.

* Make sure to change the Identity services configuration to make passwords more secure as the current settings are in place to make development easier but are not good settings for secure passwords

## Schema <a id="schema"></a>
Intial Schema<br>
![Initial Schema](https://github.com/Patrick-Dolan/Catfish.Solution/blob/main/README_IMAGES/InitialDogSchema.PNG)

Final Schema<br>
![Final Schema](https://github.com/Patrick-Dolan/Catfish.Solution/blob/main/README_IMAGES/FinalDogSchema.PNG)

## Known Bugs <a id="bugs"></a>

* _No known issues_
  
## License <a id="license"></a>

_MIT_

Copyright (c) _2022_ _Cesar Lopez, Ebru Rice, Jack Skelton, Marni Sucher, Patrick Dolan_

  
## Contact Me <a id="contact"></a>

If you encounter any issues or have any questions or ideas for this site, please contact:

Cesar Lopez:
* Email: [Lopez.cesar.aug@gmail.com](mailto:lopez.cesar.aug@gmail.com)
* Github: [wowgr8](https://github.com/wowgr8)
* LinkedIn: [https://www.linkedin.com/in/cesar-aug-lopez](https://www.linkedin.com/in/cesar-aug-lopez)
