var http = require('http'); //1. module to req/res of the http service
var fs = require('fs');     //2. IO module

var events = require("events");     //7. Event pkg
var emitter = new events.EventEmitter(); 
emitter.on("testEvent", function(msg) { 
    console.log(msg); 
});

var module=require("./FrameWareNodeJS102Module.js");
module=require("./FrameWareNodeJS102Module.js");    //3. singleton of require

http.createServer(function (req, res) {
    res.writeHead(200, {'Content-Type': 'text/plain'});
    res.end('Hello World\n');                       // ...ref 1.
    
    fs.unlink('./ToDelete.js', function (err) {     // ...ref 2.
        if (err) throw err;
        console.log('successfully deleted ToDelete.js');    //4.log shown after 5.
    });
    
    var Caller=require("./FrameWareNodeJS103Module.js");    //5.module scope
    console.log(Caller.ID);
    console.log(Caller.Order);
    Caller.purgeLog();
    
    fs.readFile('./FrameWareNodeJS103Module.js', 'utf-8', function (err, data) {    //6. Async callback thread.
        if (err) throw err;
        console.log(data);
        
        emitter.emit("testEvent", "Simple outputs..."); // ...ref 6. Fire emitter
    });
    
    console.log('From main thread');
        
}).listen(1335, "127.0.0.1");

console.log('Server running at http://127.0.0.1:1335/');

//wwhdeMacBook-Air:_1_Node_101 wwh$ node FrameWareNodeJS101.js
//Ping from protal!
//Server running at http://127.0.0.1:1335/
//undefined
//OC1
//OC1 under UUID.
//From main thread
//successfully deleted ToDelete.js
//var ID="UUID";
//
//this.Order="OC1";
//
//this.purgeLog=function(){
//    console.log('OC1 under UUID.');
//};
//Simple outputs...

//if ToDelete.js exists, otherwise, throwing exception

///Users/wwh/Desktop/DemoDay/_1_Node_101/FrameWareNodeJS101.js:18
//        if (err) throw err;
//                       ^
//Error: ENOENT, unlink './ToDelete.js'
//    at Error (native)
