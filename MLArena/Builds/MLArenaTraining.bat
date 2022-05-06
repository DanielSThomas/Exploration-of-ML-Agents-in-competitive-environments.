::Runs the ML Training Python for use with the Unity Editor

Call UnityMLToolKit\ml-env\Scripts\activate.bat

mlagents-learn Config\EliminationAgentBasic.yaml --run-id=EliminationAgentBasic

start "" http://localhost:6006/
tensorboard --logdir results


