var Lib = (function($){

    var EarthRadius=6371000;
    
    var Coor = function(lon, lat){
        this.latitude=lat;
        this.longitude=lon;
        
        if(typeof this.getDecimal != "function"){
            Coor.prototype.getDecimal = function(){
                return EarthRadius * 123;
            };
        }
                
        Coor.prototype={
            constructor: Coor
        }
    };

    return {
        Coordinate : Coor
    };
    
})(jQuery);
