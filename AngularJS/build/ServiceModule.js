angular.module('ngapp.service', [])
    .service("myService", function(){
        this.Hello = function () {
            return "Hello";
        };
        this.Sum = function (a, b) {
            return a + b;
        };
    });
