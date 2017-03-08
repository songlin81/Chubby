;(function(){
    var appModule = angular.module(
        'app',
        ['ngapp.value',
         'ngapp.directive',
         'ngapp.filter',
         'ngapp.factory',
         'ngapp.service',
         'ngapp.provider',
         'ngCookies',
         'ngMessages']
    );
    appModule.run(function(greeter, user, constdata) {
        greeter.localize({salutation: 'Fibonacci' + constdata});
        user.load('NUMBER');
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
            { title: 'T1', completed: true },
            { title: 'T2', completed: false },
            { title: 'T3', completed: true },
            { title: 'T4', completed: false },
            { title: 'T5', completed: false },
        ];  
        $scope.$watch(
            function($scope) { return $scope.bd },
            function(newValue, oldValue) {
              if (newValue.length>5) {
                alert(newValue + "-->" + oldValue);
              }
            }
        );
    }]);
    appModule.controller("InnerController", ['$scope', function($scope) {
        $scope.obj = {};
        $scope.obj.title = "context from embedded controller";
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

    appModule.controller("MyControllerFcty", ["$scope","StringUtil",'$cookies','$cookieStore', function($scope,StringUtil,$cookies,$cookieStore) {
        $scope.originalString = "timberland";
        $scope.reverseString = StringUtil.getReverseString($scope.originalString);
        $scope.characterCount = StringUtil.getCharacterCount($scope.originalString);
        $cookies.sKey = 'key';
        $scope.platformCookie = $cookies.sKey;
        $cookieStore.put('key1', 'a1b');
        $cookieStore.put('key2', 'c1d');
        $scope.key1Value = $cookieStore.get('key1');
    }]);

})();