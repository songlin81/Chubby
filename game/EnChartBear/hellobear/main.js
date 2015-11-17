enchant();

window.onload = function(){
    var game = new Core(320, 320);
    game.fps = 15;
    game.preload("chara1.png", 'map0.png', 'gameover.wav', 'jump.wav');
    
    game.onload = function(){
        
        //1. overlay
        var overlabel = new Label('');
        overlabel.x=230;
        overlabel.y=0;
        var red = new Sprite(60, 60);
        red.backgroundColor = 'red';
        red.moveTo(230, 230);
        game.rootScene.addChild(red);
        var blue = new Sprite(60, 60);
        blue.moveTo(245, 245);
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
        
        //2. map
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
        
        //3. walking bear
        var bear = new Sprite(32, 32);
        bear.image = game.assets["chara1.png"];
        bear.x = 0;
        bear.y = 150;
        bear.frame = 10;
        game.rootScene.addChild(bear);

        //4. heading
        var label = new Label("Song's 2D play yard");
        label.font="12px sanes-serif";
        label.color="rgb(126,0,0)";
        label.x=50;
        label.y=10;
        game.rootScene.addChild(label);
        
        //5. events bear
        bear.addEventListener("enterframe", function(){         
            if (this.scaleX === 1) {
                this.x += 1;
                this.frame = this.age % 3 + 10;
                if (this.x > 320-32) {
                    game.assets['jump.wav'].play();
                    this.scaleX=-1;
                }
            }
            else{
                this.x -= 1;
                this.frame = this.age % 3 + 10;
                if (this.x < 0) {
                    game.assets['jump.wav'].play();
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
            game.assets['gameover.wav'].play();
        });
        
        //6. event: tracking canvus
        var status = new Label("");
        status.x=0;
        status.y=230;
        status._log = [];
        status.add = function(str) {
            this._log.unshift(str);
            this._log = this._log.slice(0, 6);
            this.text = this._log.join('<br />');
        };
        game.rootScene.on('touchstart', function(evt) {
            status.add('touchstart (' + round(evt.x) + ', ' + round(evt.y) + ')');
        });
        game.rootScene.on('touchmove', function(evt){
            status.add('touchmove (' + round(evt.x) + ', ' + round(evt.y) + ')');
        });
        game.rootScene.on('touchend', function(evt){
            status.add('touchend (' + round(evt.x) + ', ' + round(evt.y) + ')');
        });       
        ['up', 'down', 'right', 'left'].forEach(function (direction){
            var d = direction;
            game.rootScene.on(direction + 'buttondown', function(){
                status.add(d + 'buttondown');
            })
            game.rootScene.on(direction + 'buttonup', function(){
                status.add(d + 'buttonup');
            })
        });
        game.rootScene.addChild(status);
        
        //7. scene
        game.rootScene.backgroundColor = 'rgb(182, 255, 255)';
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

function round(num) {
    return Math.round(num * 1e3) / 1e3;
};