angular.module('ngapp.directive', [])
  .directive('ngbkFocus', function(){
    return {
      link: function(scope, element, attrs, controller) {
        element[0].focus();
      }
    };
  });
