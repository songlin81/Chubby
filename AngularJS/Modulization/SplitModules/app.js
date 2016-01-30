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
  });