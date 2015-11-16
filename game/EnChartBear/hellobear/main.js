enchant();

window.onload = function(){
    var game = new Core(320, 320);
    game.fps = 15;
    game.preload("chara1.png");
    game.onload = function(){
        var bear = new Sprite(32, 32);
        bear.image = game.assets["chara1.png"];
        bear.x = 0;
        bear.y = 0;
        bear.frame = 10;
        game.rootScene.addChild(bear);

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
};