:: Sets up a python enviroment and downloads pytorch and mlagents api

py -m venv ml-env
Call ml-env\Scripts\activate
pip3 install torch~=1.7.1 -f https://download.pytorch.org/whl/torch_stable.html
py -m pip install mlagents==0.27.0