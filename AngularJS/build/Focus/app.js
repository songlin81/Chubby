;(function(){

    var appModule = angular.module('app', ['ngapp.value', 'ngapp.directive', 'ngapp.filter']);

    appModule.run(function(greeter, user, constanttest) {
        greeter.localize({
          salutation: 'Bonjour' + constanttest
        });
        user.load('WORLD');
    })
    .controller('SomeController', ['$scope','greeter','user', function($scope, greeter, user) {
        $scope.greeting = greeter.greet(user.name);
        
        $scope.message = { text: 'nothing clicked yet' };
        
        $scope.clickUnfocused = function() {
          $scope.message.text = 'unfocused button clicked';
        };
        $scope.clickFocused = function() {
          $scope.message.text = 'focus button clicked';
        };
        
        $scope.todos = [
            { title: 'Learn Javascript', completed: true },
            { title: 'Learn Angular.js', completed: false },
            { title: 'Love this tutorial', completed: true },
            { title: 'Learn Javascript design patterns', completed: false },
            { title: 'Build Node.js backend', completed: false },
        ];
        
        $scope.$watch(
            function($scope) { return $scope.nameZZ },
            function(newValue, oldValue) {
              if (newValue.length>5) {
                alert(newValue + "-->" + oldValue);
              }
            }
        );
    }]);

    appModule.controller("HelloController", function($scope) {
        $scope.helloTo = {};
        $scope.helloTo.title = "...";
    });
    
    appModule.controller("MyController", function($scope) {
        $scope.myData = {};
        $scope.myData.textf = function() { return "A text from a function"; };
        
        $scope.myData.showIt = true;
        $scope.myData.hideIt = false;
        
        $scope.myData.switch = 3;
        
    });

})();