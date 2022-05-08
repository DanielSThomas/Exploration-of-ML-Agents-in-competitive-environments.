# ProjectMLArena
Personal Project exploring the use of Unity's machine learning agents within competitive enviroments.
Features a pixel art top-down tank game in where players and AI agents navigate levels to find and eliminate thier opponents. 

# Dependencies
* Unity 2021.1.17f1
* Python 3.7.9
* Pytorch 1.7.1
* mlagents 0.27.0

# Installation
Clone repository from the Github project.

Download the python dependencies using either the provided automatic setup bat found at MLArena\Builds\UnityMLToolKit (DownloadMLEnv.bat) or by following Unity’s installation guide found at: (https://github.com/Unity-Technologies/ml-agents/blob/main/docs/Installation.md).
It is recommended to use Python 3.7.9 as well as Pytorch 1.7.1 alongside mlagents 0.27.0. 

Download Unity, the version used for this project was 2021.1.17f1, but other versions may work as well. Unity should automatically download Unity dependencies from the packages manifest on project load.

# Usage
MLagents.exe can be run without any dependencies if uninterested in altering the Unity project or training AI. MLagents lets users set the amount of AI opponents on each team as well as indicate if they want manual control over an agent.

The main variables to change, if altering the project, are the elimination agent prefabs. Specifically, the Behaviour Parameters in which the Agent Model i.e. the trained brain, as well as the behaviour name, can both be changed. If training agents the Behaviour Name variable must match the name in the training YAML file as well as the run-id in the training bat. 

Additionally, provided in the builds folder can be found all YAML configs as well as two training bats called MLArenaTraining and MLArenaTrainingStandAlone. The main difference between the two is one is for training within the Unity editor while the other relies on a compiled build to be placed in the MLBuild folder.

It is recommended to reference the documentation provided by Unity for training your own agents found here: (https://github.com/Unity-Technologies/ml-agents/blob/main/docs/).
