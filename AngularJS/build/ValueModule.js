angular.module('ngapp.value', [])
  .value('greeter', {
      salutation: 'Fib',  //1. property
      localize: function(localization) {
        this.salutation = localization.salutation;
      },
      greet: function(name) {
        return this.salutation + ' ' + name + '!!!';
      }
  })
  .value('user', {
    load: function(name) {
      this.name = name;
    }
  })
  .constant('constdata', ' --- ');  //2. constant 
