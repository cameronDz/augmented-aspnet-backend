# Augmented-ASP.NET-Backend

An ASP.NET API microservice using Code First Entity Framework.

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

Unit tests have yet to be developed.

## Deployment

Once deployed, instructions on how to replicate will be added.

## Built With

* [Entity Framework](https://docs.microsoft.com/en-us/ef/) - Used generating Code First Database
* [Visual Studio](https://visualstudio.microsoft.com/vs/) - IDE for developing code
* [Azure](https://azure.microsoft.com/en-us/) - Cloud deployment

## Contributing

Please read [CONTRIBUTING.md](CONTRIBUTING.md) for details on our code of conduct, and the process for submitting pull requests to us.

## Authors

* **Cameron Dziurgot** - *Initial work* - [cameronDz](https://github.com/cameronDz)

See also the list of [contributors](https://github.com/cameronDz/augmented-aspnet-backend/contributors) who participated in this project.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

* [PurpleBooth](https://github.com/PurpleBooth) templates for [README](https://gist.github.com/PurpleBooth/109311bb0361f32d87a2) and [CONTRIBUTING] (https://gist.github.com/PurpleBooth/b24679402957c63ec426) files.
