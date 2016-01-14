define(
       function(){
            var jia = function(a, b){
                return a+b;
            }
            var jian = function(a, b){
                return a-b;
            }
            var cheng = function(a, b){
                return a*b;
            }
            var chu = function(a, b){
                return a/b;
            }
            
            return {
                jia: jia,
                jian: jian,
                cheng: cheng,
                chu: chu
                
            }
        }
)

