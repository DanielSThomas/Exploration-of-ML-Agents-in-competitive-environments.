::Runs the ML Training Python alongside MLArena

Call UnityMLToolKit\ml-env\Scripts\activate.bat

mlagents-learn Config\EliminationAgentSAC.yaml --env=MLBuild\MLArena.exe --run-id=EliminationAgentSac --width 800 --height 600 --num-envs=2
start "" http://localhost:6006/
tensorboard --logdir results


