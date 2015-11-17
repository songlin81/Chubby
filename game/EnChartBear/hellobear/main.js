enchant();

window.onload = function(){
    var game = new Core(320, 320);
    game.fps = 15;
    game.preload("chara1.png", 'map0.png');
    
    game.onload = function(){
        
        var overlabel = new Label('');
        var red = new Sprite(120, 120);
        red.backgroundColor = 'red';
        red.moveTo(160, 160);
        game.rootScene.addChild(red);
        var blue = new Sprite(120, 120);
        blue.backgroundColor = 'blue';
        game.rootScene.addChild(blue);
        blue.on('touchstart', function(evt){
            overlabel.text += 'blue touchstart <br/>';
            red.dispatchEvent(evt);
        });
        red.on('touchstart', function(evt){
            overlabel.text += 'red touchstart <br/>';
        });
        game.rootScene.addChild(overlabel);
        
        var map = new Map(16, 16);
        map.image = game.assets['map0.png'];
        map.loadData(
            [
                [4, 4, 4, 4, 4, 4, 4],
                [4, 5, 5, 5, 5, 5, 4],
                [4, 5, 4, 5, 4, 5, 4],
                [4, 5, 5, 5, 5, 5, 4],
                [4, 5, 4, 5, 4, 5, 4],
                [4, 5, 5, 5, 5, 5, 4],
                [4, 4, 4, 4, 4, 4, 4]
            ]
        );
        map.x=100;
        map.y=100;
        game.rootScene.addChild(map);
        
        var bear = new Sprite(32, 32);
        bear.image = game.assets["chara1.png"];
        bear.x = 0;
        bear.y = 150;
        bear.frame = 10;
        game.rootScene.addChild(bear);

        var label = new Label("Song's 2D play yard");
        label.font="12px sanes-serif";
        label.color="rgb(126,0,0)";
        label.x=50;
        label.y=10;
        game.rootScene.addChild(label);
        
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
            
            if ((game.frame % 10  == 0) && (this.age<100)) {
                var r=rand(256);
                var g=rand(256);
                var b=rand(256);
                var x=rand(50)+50;
                var y=rand(50)+30;
                game.addLabel(game.frame, "rgb("+r+","+g+","+b+")", x, y);
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