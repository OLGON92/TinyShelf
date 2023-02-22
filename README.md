# _TinyShelf_

#### By _**Rafael Petrachini, Grace Lee, Oscar Gonzalez, and Remy Flores**_

#### _TinyShelf is a web application that lets you plan, add tasks, and share them with others. It's very similar to Task Pad. Here is the [link](https://github.com/rafapetra/TinyShelf.Solution) to the repository on GitHub._

## Technologies Used

* _C#_
* _.NET6_
* _HTML_
* _CSS_
* _Razor Markup_
* _MySQLWorkbench_
* _SQL Database_
* _AspNetCore_
* _Linq_
* _EntityFrameworkCore_
* _AspNetCore.Mvc_
* _AspNetCore.Mvc.Rendering_

## Description

_This is our eleventh project for Epicodus that is meant to showcase our understanding of basic web applications with a database while utilizing C#; it is also meant to show our understanding of many-to-many relationships._

## Setup/Installation Requirements

* _Follow the steps below_

#### Installing .NET & MySQL
* _First, you will need to install .NET 6 if it isn't already on your machine_
* _Here is a link where you can download for your Mac, Windows, or Linux-based machine: [link](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)_
* _Look in your downloads and open the file_
* _Follow the installation instructions_
* _To confirm that the installation was successful, type "dotnet --version" in your terminal_
* _Then you will need to install MySQL. Follow the instructions [here](https://www.learnhowtoprogram.com/c-and-net/getting-started-with-c/installing-and-configuring-mysql) to do so._

#### Configuring appsettings.json
* _Clone the repository using this link: [link](https://github.com/rafapetra/TinyShelf.Solution)_
* _Navigate to the 'TinyShelf.Solution' directory on your computer_
* _Open it with your favorite text editor _
* _From here you will go to the "TinyShelf" directory_
* _Create a new file called "appsettings.json_
* _In the appsettings.json file, you will add the following code below_
* _*Please note that your username and password are dependent on what you have set up in your MySQLWorkbench.*_
* _*Also, do not include the brackets []*_
```
{
"ConnectionStrings": {
"DefaultConnection": "Server=localhost;Port=3306;database=[YOUR-DATABASE-NAME];uid=[YOUR-USERNAME-HERE];pwd=[YOUR-PASSWORD-HERE];"
}
}
```
#### To add a migration, start a development server, and view the project in the browser follow the steps below:

* _Navigate to 'TinyShelf.Solution' in your command line_
* _From there, navigate to 'TinyShelf'_
* _Run the command "dotnet build"_
* _Then run the command "dotnet tool install --global dotnet-ef --version 6.0.0"_
* _Then you will run the command "dotnet add package Microsoft.EntityFrameworkCore.Design -v 6.0.0"_
* _Now you will need to create a migration; run the command "dotnet ef migrations add Initial"_
* _Anytime you make a change to any of the models, you will have to enter this command "dotnet ef migrations add [ChangeInMigration]"_

## Known Bugs
* _Currently still working on applying our model into the splash page by doing partial rendering. Also still working on linking our view/models. <strike>I also ran out ofCocaCola. (please sponsor me!!)</strike> (Offically off the juice but having a tough time - Remy)_
* _If I missed something, or a bug is found send me an email to_ [insert email here]

## License
* **SEE LICENSE [HERE](./LICENSE.txt)** 