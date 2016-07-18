angular.module('ngapp.filter', [])
    .filter('greet', function() {
        return function(name) {
            return 'Hello, ' + name + '!';
        };
    });
