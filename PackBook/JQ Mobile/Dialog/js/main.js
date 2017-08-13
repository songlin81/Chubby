;(function($){

    var StartUp=function(){    
        
        function applyStyles() {
            $("#footerlnk").addClass("cleanlink");
        }
        
        function applyContens() {
            var coo = new Lib.Coordinate(12, 123);
            $("#contents").text("EarthRadius " + coo.getDecimal());
        }
        
        var init = function(){
            applyStyles();
            applyContens();
        };
            
        return{
            Init:init
        };
        
    }

    var sup = new StartUp();
    sup.Init();
    
})(jQuery);
