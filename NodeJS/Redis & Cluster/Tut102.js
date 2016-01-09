var http = require("http");
var cluster = require("cluster");
var numCPUs = require("os").cpus().length;

if (cluster.isMaster) {
    for (var i = 0; i < numCPUs; i++) {
        console.log("Forking child");
        cluster.fork();
        console.log(cluster.isMaster);
    }
}
else{
        http.createServer(function(request, response) {
            console.log(process.pid + ": request for " + request.url);
            console.log(cluster.isMaster);
            response.writeHead(200);
            response.end(" ! " + request.url + " ! ");
        }).listen(8000);
}