enchant();

window.onload = function(){
    var game = new Core(320, 320);
    game.fps = 15;
    game.preload("chara1.png");
    
    game.onload = function(){
        var bear = new Sprite(32, 32);
        bear.image = game.assets["chara1.png"];
        bear.x = 0;
        bear.y = 100;
        bear.frame = 10;
        game.rootScene.addChild(bear);

        var r=rand(256);
        var g=rand(256);
        var b=rand(256);
        var x=100;
        var y=10;
        game.addLabel("Song's 2D game yard", "rgb("+r+","+g+","+b+")", x, y);
        
        bear.addEventListener("enterframe", function(){         
            if (this.scaleX === 1) {
                this.x += 1;
                this.frame = this.age % 3 + 10;
                if (this.x > 320-32) {
                    this.scaleX=-1;
                }
            }
            else{
                this.x -= 1;
                this.frame = this.age % 3 + 10;
                if (this.x < 0) {
                    this.scaleX = 1;
                }
            }
        });

        bear.addEventListener("touchstart", function(){
            game.rootScene.removeChild(bear);
        });
    };
    game.start();
    
    game.addLabel = function(text, color, x, y){
        var label=new Label(text);
        label.font = "12px 'Arial'";
        label.color=color;
        label.x=x;
        label.y=y;
        game.rootScene.addChild(label);
    };
};

function rand(num){
    return Math.floor(Math.random() * num);
}