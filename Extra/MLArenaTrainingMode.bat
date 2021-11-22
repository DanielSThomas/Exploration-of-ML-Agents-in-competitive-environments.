::Runs the ML Training Python alongside MLArena

:: Usage --------------------------------------------------------------------------------------------------
:: - First path should lead to the .yaml config you wish to use for the training.
:: - Env should lead to your exe of MLAreana.
:: - Run-id equals the name of the training session, leaving this as default will override any previous training done with the same id.
:: - Time-scale is the speed the MLArena will run, this is useful for speeding up training. 1 = normal speed.
:: - Force is a parameter for overriding previous training with the same run ID.


Call UnityMLToolKit\ml-env\Scripts\activate.bat

mlagents-learn Config\CoinCollector.yaml --env=MLArena --run-id=CoinCollector --time-scale=10 --force --width 1920 --height 1080

start "" http://localhost:6006/
tensorboard --logdir results


