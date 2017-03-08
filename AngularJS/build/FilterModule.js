angular.module('ngapp.filter', [])
    .filter('greet', function() {
        return function(name) {
            return 'Filter applied to: ' + name + '!';
        };
    })
    .filter('myFilter', function() {
      return function(stringValue) {
          return stringValue.substring(0,3);
      };
    })
    .filter('myFilter2', function() {
      return function(stringValue, startIndex, endIndex) {
          return stringValue.substring(parseInt(startIndex), parseInt(endIndex));
      };
    });
