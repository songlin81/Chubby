var redis = require("redis"),
    client = redis.createClient(6379, "localhost");
 
client.on("error", function(err){
    console.log("Error: " + err);
});
 
client.on("connect", function(){
    // start server();
    client.set("name_key", "hello world", function(err, reply){
        console.log("set: " + reply.toString());
    });
 
    client.get("name_key", function(err, reply){
        console.log("get: " + reply.toString());
    });
});