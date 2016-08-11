;(function(){

    var appModule = angular.module('app',
                        ['ngapp.value',
                         'ngapp.directive',
                         'ngapp.filter',
                         'ngapp.factory',
                         'ngapp.service',
                         'ngapp.provider']);

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

    appModule.controller("HelloController", ['$scope', function($scope) {
        $scope.helloTo = {};
        $scope.helloTo.title = "...";
    }]);
    
    appModule.controller("MyController", ['$scope','myService','myProvider', function($scope, myService, myProvider) {
        $scope.myData = {};
        $scope.myData.textf = function() { return "A text from a function"; };
        
        $scope.myData.showIt = true;
        $scope.myData.hideIt = false;
        
        $scope.myData.switch = 3;
        
        $scope.myData.items = [ {text : "one"}, {text : "two"}, {text : "three"} ];
        $scope.myData.getItems = function() { return this.items; };
        
        $scope.myData.myObject = { var1 : "val1", var2 : "val2", var3 : "val3"};
        $scope.itemFilter = function(item) {
          if(item.text == "two") return false;
            return true;
        };
        $scope.sortField = "text";
        $scope.reverse = true;
        
        $scope.filterArray = function(item) {
          if(item.text == "two") return false;
            return true;
        };
        
        $scope.myData.textContent = "ABCDEFG";
        
        $scope.ServiceOutput = "Look for service output here.";
        $scope.HelloService = function() {
            $scope.ServiceOutput = myService.Hello();
        };
        $scope.SumService = function() {
            $scope.ServiceOutput = "The sum is : " + myService.Sum(2, 5);
        };
        
        $scope.ProviderOutput = "Look for factory output here.";
        $scope.HelloProvider = function() {
            $scope.ProviderOutput = myProvider.Hello();
        };
        $scope.SumProvider = function() {
            $scope.ProviderOutput = "The sum is : " + myProvider.Sum(22, 52);
        };
    }]);
    
    appModule.controller("myController1", function($scope) {
      $scope.data = { theVar : "Value One"};
    });
    appModule.controller("myController2", function($scope) {
      $scope.data = { theVar : "Value Two"};
      
      $scope.myData = {};
      $scope.myData.doClick = function(event) {
        alert("clicked: " + event.clientX + ", " + event.clientY);
      };
      $scope.myData2 = {};
      $scope.myData2.items = [{ v: "1"}, { v: "2"}, { v : "3"} ];
      $scope.myData2.doClick = function(item, event) {
        alert("clicked: " + item.v + " @ " + event.clientX + ": " + event.clientY);
      };
    });
    
    appModule.controller("MyControllerFcty", ["$scope", "StringUtil", function($scope, StringUtil) {
      $scope.originalString = "Sandeep Kumar Patel";
      $scope.reverseString = StringUtil.getReverseString($scope.originalString);
      $scope.characterCount = StringUtil.getCharacterCount($scope.originalString);
      }
    ]);
})();