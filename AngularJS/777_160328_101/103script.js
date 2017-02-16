var app=angular.module('app', []);

//1. controller plus repeater, and two-way binding，and watch.
app.controller('TodoController', ['$scope', function ($scope) {
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

//2. controller plus object literal.
app.controller("HelloController", function($scope) {
    $scope.helloTo = {};
    $scope.helloTo.title = "...";
} );

app.controller("MyController", function($scope) {
    //3. function call, and binding
    $scope.myData = {};
    $scope.myData.textf = function() { return "A text from a function"; };
   
    //4. hide and show
    $scope.myData.showIt = true;    //6. ng-if 
    $scope.myData.hideIt = false;
           
    //5. ng-switch
    $scope.myData.switch = 3;

    //8. data binding, and func call
    $scope.myData.items = [ {text : "one"}, {text : "two"}, {text : "three"} ];
    $scope.myData.getItems = function() { return this.items; };



   
    //9. indexer
    $scope.myData.myObject = { var1 : "val1", var2 : "val2", var3 : "val3"};
    
    //10. Customer filter
    $scope.itemFilter = function(item) {
      if(item.text == "two") return false;
        return true;
    }
    $scope.sortField = "text";
    $scope.reverse = true;
    
    //11. in and customer filter
    $scope.filterArray = function(item) {
      if(item.text == "two") return false;
        return true;
    }
    
    //13. Filter for string
    $scope.myData.textContent = "ABCDEFG";
});
app.filter('myFilter', function() {
  return function(stringValue) {
      return stringValue.substring(0,3);
  };
});
app.filter('myFilter2', function() {
  return function(stringValue, startIndex, endIndex) {
      return stringValue.substring(parseInt(startIndex), parseInt(endIndex));
  };
});

//14. Same named value but diff controllers.
app.controller("myController1", function($scope) {
  $scope.data = { theVar : "Value One"};
});
app.controller("myController2", function($scope) {
  $scope.data = { theVar : "Value Two"};
  
  //15. events 
  $scope.myData = {};
  $scope.myData.doClick = function(event) {
    alert("clicked: " + event.clientX + ", " + event.clientY);
  }
  $scope.myData2 = {};
  $scope.myData2.items = [{ v: "1"}, { v: "2"}, { v : "3"} ];
  $scope.myData2.doClick = function(item, event) {
    alert("clicked: " + item.v + " @ " + event.clientX + ": " + event.clientY);
  }
});

//16. factory
app.factory("StringUtil", function(){
  return {
    getReverseString: function(inputString) {return inputString.split("").reverse().join("");},
    getCharacterCount: function(inputString) {return inputString.length;}
  };
});
app.controller("MyControllerFcty", ["$scope", "StringUtil", function($scope, StringUtil) {
  $scope.originalString = "Sandeep Kumar Patel";
  $scope.reverseString = StringUtil.getReverseString($scope.originalString);
  $scope.characterCount = StringUtil.getCharacterCount($scope.originalString);
  }
]);
