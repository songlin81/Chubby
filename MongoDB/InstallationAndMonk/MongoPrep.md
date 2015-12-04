
curl -O http://downloads.mongodb.org/osx/mongodb-osx-x86_64-3.0.1.tgz

tar -zxvf mongodb-osx-x86_64-3.0.1.tgz

mkdir -p mongodb

cp -R -n mongodb-osx-x86_64-3.0.1/ mongodb

sudo mkdir -p /data/db

sudo chown -R wwh /data

sudo apt-get install mongodb-server


netstat -anp | grep mongodb


# try out with some data in mongo
$ mongo
mongo> use monk-app
mongo> db.products.insert({"name":"apple juice", "description":"good"})
mongo> db.products.find().pretty()


$ mkdir monk-app
$ cd monk-app
$ npm install #Remember to have package.json before calling this line!
