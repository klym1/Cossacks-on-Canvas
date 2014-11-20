"use strict";

function Render (world) {
	
	var initRunTime = new Date();
	this._resources = {};
	this.world = world;
	this.gameLoop = function () {

		var self = this;

		var binded = self.gameLoop.bind(self);

		//setTimeout(binded, 100);
		window.requestAnimationFrame(binded);
		self.RenderUnits();
	}

	this.uppendLine = function(string){

		var difference = new Date() - initRunTime;
		
		var 
    	minutes = Math.floor(difference % 36e5 / 60000),
    	seconds = Math.floor(difference % 60000 / 1000),
    	milliseconds = Math.floor(difference % 1000);

		document.getElementById("loading-container").innerHTML += "[" + minutes + ":" + seconds + "." + milliseconds+ "] " + string + "<p>";
	}

	this.init = function() {

		var canvas_height = 500;
		var canvas_width = 800;
		
		var self = this;

		// Get canvas
		this.canvas = document.getElementById("can");
		this.canvas.width = canvas_width;
		this.canvas.height = canvas_height;
		
 		this.canvas_height = this.canvas.height;
	  	this.canvas_width = this.canvas.width;

 		this.ctx = this.canvas.getContext("2d");

		var imageLoader = new IM(this, function(){ 
			
			self.uppendLine("Loading finished in ");

			self.gameLoop.bind(self)();

		}, function(a,b){
			self.uppendLine( (b/a * 100 + "").substring(0,6) + " %");
		});
		
		for(var i = 0; i < sprites.length; i++){
			imageLoader.add('sprites_png/'+ sprites[i].UnitName + '.png', sprites[i].UnitName);
		}
	}
	
  	this.RenderUnits = function() 
  	{
		this.ctx.clearRect(0, 0, this.canvas_width , this.canvas_height);

		var units = this.world.units;

		for (var i = units.length - 1; i >= 0; i--) 
		{
			var unit = units[i];
			var state = unit.State;

			var spriteWidth = state.spriteWidth;
			var spriteHeight = state.spriteHeight;

			var sx = spriteWidth * unit.n;
			var sy = spriteHeight * state.j;

			var image = this._resources[state.spriteName];

			this.ctx.drawImage(image, sx, sy, spriteWidth, spriteHeight, unit.x, unit.y, spriteWidth, spriteHeight);
			this.ctx.rect(unit.x, unit.y, 2, 2);
			this.ctx.stroke();
		}
	};
};
