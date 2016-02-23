var cluster = require('cluster');
var http = require('http');

var numCPUs = require('os').cpus().length;

console.log(numCPUs);

if (cluster.isMaster) {
  for (var i = 0; i < numCPUs; i++) {
    cluster.fork();
  }
  cluster.on('exit', function(worker, code, signal) {
    console.log('工作进程 ' + worker.process.pid + ' 被终止');
  });
} else {
  http.createServer(function(req, res) {
    res.writeHead(200);
    res.end("hello world\n");
  }).listen(8000);
}
