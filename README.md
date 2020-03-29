# Augmented-ASP.NET-Backend

An ASP.NET API microservice using Code First Entity Framework.

![build](https://img.shields.io/badge/build-passing-brightgreen.svg)
![coverage](https://img.shields.io/badge/code%20coverage-2.48%25-red.svg)

## Getting Started

Project needs to have Entity Framework installed through Visual Studios Nuget Manager.

### Prerequisites

Visual Studio 2017

### Installing

After clone the repository locally, Import the project into Visual Studio. Be sure that you are not working on the master branch locally.

## Versioning

This project essentially uses [SemVer](http://semver.org/) for versioning, with the PATCH number always assumed to be zero unless noted otherwise. For the versions available, see the [tags on this repository](https://github.com/cameronDz/augmented-aspnet-backend/tags). Releases will only be published for MAJOR versions.

No code development should be done on the "master" branch. Instead, the "develop" branch should be used to branch off of for various features/issues. Features, fixes, and issues should all be in the GitHub [Issues section for the repository](https://github.com/cameronDz/augmented-aspnet-backend/issues) before development work begins. Once a feature/fix/issue is done, all commits need to be squashed into a single commit. A Pull Request should then be made for that commit into the "master" branch. The pull request should contain [keywords to close the Issue](https://help.github.com/articles/closing-issues-using-keywords) that was being corrected. If the commit is deployed, it should be tagged as the latest version in git, as well as updated in the pom.xml and application.properties files.

The [Project section of the repository](https://github.com/cameronDz/augmented-aspnet-backend/projects) tracks the work planned for each major Release. Releases should consist of between 10 - 20 tags. Hot fixes should still follow the protocol for being done in "develop" and squashed into "master".

## Running the tests

Uses [AxoCover](https://marketplace.visualstudio.com/items?itemName=axodox1.AxoCover&showQnADialog=true) plugin to get Unit test coverage reports.

## Deployment

### Deploying API in Microsoft's Azure cloud
The following setup is done in Visual Studio 2019. Deploying the application with a database and server, for extended use on F tier under a Pay-as-you-Go subscription, will cost ~$15 a month. It is assumed whoever is going through these steps already has a Azure account connected to their Visual Studio.
1. Right-click the AugmentedAspnetBackend project and select "Publish"
1. For Azure App Serivce, select "Create New", then select and press "Create Profile" (you'll be ask to verify your credentials for Azure at this point).
1. Choose a Location near your region, and select Free tier for hosting plan. Also a good practice to have name for; "**App Name**", "**Resource Group**", and "**Hosting Plan**" should all have the same prefix. Example; "**App Name**": "_**MyAppName**_", "**Resource Group**": "_**MyAppNameResourceGroup**_", "**Hosting Plan**": "_**MyAppNameHostingPlan**_".
1. Under "Explore additional Azure services", select "Create a SQL Database"
1. For SQL Server, press "New..."
1. Set the Admin username and password.
7. Set the connection string name to match the connection string in the WorkoutContext.cs class.
1. Press "Create". This may take several minutes.
1. After the resources are created, a success message should appear in the IDE, with a button to "Publish".
1. Press "Publish". The first deploy may also take several minutes.

## Built With

* [Entity Framework](https://docs.microsoft.com/en-us/ef/) - Used generating Code First Database
* [Visual Studio](https://visualstudio.microsoft.com/vs/) - IDE for developing code
* [Azure](https://azure.microsoft.com/en-us/) - Cloud deployment
* [Postman](https://www.getpostman.com/) - Used for API HTTP Request testing 

## Contributing

Please read [CONTRIBUTING.md](CONTRIBUTING.md) for details on our code of conduct, and the process for submitting pull requests to us.

## Authors

* **Cameron Dziurgot** - *Initial work* - [cameronDz](https://github.com/cameronDz)

See also the list of [contributors](https://github.com/cameronDz/augmented-aspnet-backend/contributors) who participated in this project.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

* [PurpleBooth](https://github.com/PurpleBooth) templates for [README](https://gist.github.com/PurpleBooth/109311bb0361f32d87a2) and [CONTRIBUTING](https://gist.github.com/PurpleBooth/b24679402957c63ec426) files.
