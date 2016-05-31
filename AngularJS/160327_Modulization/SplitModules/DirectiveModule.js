angular.module('xmpl.directive', [])
.directive('ngbkFocus', function(){
  return {
    link: function(scope, element, attrs, controller) {
      element[0].focus();
    }
  };
});
