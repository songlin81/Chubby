angular.module('ngapp.provider', [])
    .provider('myProvider', function() {
        this.$get = function() {
            return {
                Hello: function() {
                    return "Hello";
                },
                Sum: function(a, b) {
                    return a + b;
                }
            };
        };
    });
