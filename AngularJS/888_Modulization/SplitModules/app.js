angular.module('xmpl', ['xmpl.service', 'xmpl.directive', 'xmpl.filter'])
  .run(function(greeter, user) {
    // This is effectively part of the main method initialization code
    greeter.localize({
      salutation: 'Bonjour'
    });
    user.load('WORLD');
  })
  .controller('XmplController', function($scope, greeter, user){
    $scope.greeting = greeter.greet(user.name);
    
    $scope.message = { text: 'nothing clicked yet' };
    $scope.clickUnfocused = function() {
          $scope.message.text = 'unfocused button clicked';
    };
    $scope.clickFocused = function() {
          $scope.message.text = 'focus button clicked';
    };
  });