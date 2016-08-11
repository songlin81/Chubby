angular.module('ngapp.factory', [])
    .factory("StringUtil", function(){
      return {
        getReverseString: function(inputString) {return inputString.split("").reverse().join("");},
        getCharacterCount: function(inputString) {return inputString.length;}
      };
    });
