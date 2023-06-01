# Computer Information

While debugging and solving computer related issues it is common to require information about the problematic device. Much of the information can be gathered using functions such as dxdiag, msconfig and poolmonish. This Windows Form application utilizes these functions to gather data about the system and present them in one place. Showing information about your system in different tabs. See image below for demonstration.


![license](https://img.shields.io/github/license/grebtsew/Overleaf-LaTeX-Compiler)
![size](https://img.shields.io/github/repo-size/grebtsew/Overleaf-LaTeX-Compiler)
![commit](https://img.shields.io/github/last-commit/grebtsew/Overleaf-LaTeX-Compiler)

![Demo](demo.png)


# How to execute
Either download the release `.exe` file from github and run it, or download/clone this project and build the project by opening the `.sln` project file in Visual Studio. The software is developed for .net framework v4.8.

# Docker
This repository has been dockerized. The project can be built using the docker container `mcr.microsoft.com/dotnet/framework/sdk:4.8`. Use the command below to build the project, the created `.exe` file will be placed in `./target` folder. Remember to switch to Windows Containers in Docker settings.

```bash
docker-compose up --build
```

# Requirements
This program is a Windows Form, and constructed to work on Windows exclusively.

# Testing
A MSTest project exist that performs extremely fundamental tests on the created Windows Form Project.

# License
This project is created under a [MIT LICENSE](./LICENSE).

Copyright (c) Grebtsew
