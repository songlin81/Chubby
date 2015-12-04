# 1. Prerequisite
sudo apt-get install g++ curl libssl-dev apache2-utils 
sudo apt-get install git-core

# 2. git NodeJS package
git clone git://github.com/joyent/node.git   
cd node
./configure
make   
sudo make install

