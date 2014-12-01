"use strict";

/**
 * @constructor
 */

function handler(e){
	document.getElementById("world-info").innerHTML = "mouseup"
}

window.addEventListener('mouseup', handler, false);

function Render (world) {
	
	var initRunTime = new Date();
	this._resources = {};
	this.world = world;
	this.canvasOffsetX = 0;
	this.canvasOffsetY = 0;

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

	this.mouseDownHandler = function(e){

		var offsetX = e.offsetX;
		var offsetY = e.offsetY;

		//document.getElementById("can").addEventListener('mousemove', window.handler, false);

		//document.getElementById("can").removeEventListener('mouseup', window.handler, true);
	

		if(e.button == 2){
			this.HandleMouseLeftClick.bind(this)(e);
			window.removeEventListener('mouseup', handler, false);
		}

		for (var i = 0; i < this.world.units.length; i++) {

			var unit = this.world.units[i];
  		
			var actualUnitHalfWidth = 20;
			var actualUnitHalfHeight = 70;

  			unit.IsSelected = unit.x + unit.State.xSymmetry - actualUnitHalfWidth < offsetX 
  							&& unit.x + unit.State.xSymmetry + actualUnitHalfWidth > offsetX
  							&& unit.y + unit.State.ySymmetry - actualUnitHalfHeight < offsetY
  							&& unit.y + unit.State.ySymmetry + actualUnitHalfHeight > offsetY
  				 
  		};

		document.getElementById("world-info").innerHTML = "mousedown";
	}

	this.CenterView = function(){
		
		this.canvasOffsetX = (this.world.width - this.canvas_width) / 2;
		this.canvasOffsetY = (this.world.height - this.canvas_height) / 2;
	}

	this.MoveCanvasToRight = function(){
		this.canvasOffsetX += 50;
	}

	this.MoveCanvasToLeft = function(){
		this.canvasOffsetX -= 50;
	}

	this.HandleMouseLeftClick = function(e){
		var unitHandler = new UnitHandler(this.world.units[0]);
		unitHandler.Go(12,23);
	}

	this.mouseUpHandler = function(e){

		//document.getElementById("can").addEventListener('mousemove', window.handler, false);
		//document.getElementById("can").removeEventListener('mouseup', window.handler, true);

		document.getElementById("world-info").innerHTML = "mouseup";
	}

	this.init = function() {

		var canvas_height = 500;
		var canvas_width = 800;
		
		var self = this;

		this.canvas = document.getElementById("can");
		this.canvas.width = canvas_width;
		this.canvas.height = canvas_height;
	
		document.getElementById("can").addEventListener("mousedown", this.mouseDownHandler.bind(this));
		//document.getElementById("can").addEventListener("mouseup", window.handler);
				
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
	
	this.ToCanvasX = function(x){
		return x - this.canvasOffsetX;
	}

	this.ToCanvasY = function(y){
		return y - this.canvasOffsetY;
	}

  	this.RenderUnits = function() 
  	{
  		this.WriteStatusInfo(this.world);
		
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

			if(unit.IsSelected === true){
					this.drawEllipseWithEllipse(this.ctx, this.ToCanvasX(unit.x + state.xSymmetry), this.ToCanvasY(unit.y + state.ySymmetry), 25, 10 );
			}

			this.ctx.drawImage(image, sx, sy, spriteWidth, spriteHeight, this.ToCanvasX(unit.x), this.ToCanvasY(unit.y), spriteWidth, spriteHeight);
		}
	};

	this.drawEllipseWithEllipse = function (ctx, cx, cy, rx, ry) {
        if(ctx.ellipse)
        {
          ctx.beginPath();
          ctx.ellipse(cx, cy, rx, ry, 0, 0, Math.PI*2);
          ctx.strokeStyle = '#0000ff';
          ctx.stroke();
          ctx.lineWidth = 3;
        }
      };

	this.WriteStatusInfo = function(world)
	{
		if(SHOW_DEBUG_INFO === true)
		{
		//	document.getElementById("world-info").innerHTML = this.MaterializeStatusData(this.GetInfoRecursively(world));
			document.getElementById("unit-info").innerHTML = this.MaterializeStatusData(this.GetInfoRecursively(world.units[0].commandsQueue));
		} else {
			document.getElementById("world-info").innerHTML = null;
			document.getElementById("unit-info").innerHTML = null;
		}
	}

	this.MaterializeStatusData = function(items)
	{
		var resultString = "";

		for (var i = 0; i < items.length; i++) {
			var t = items[i];		

			resultString += t[0] + ": " + t[1] + "</p>";
		};

		return resultString;
	}

	this.GetInfoRecursively = function(rootObject)
	{
		var result = [];

		for (var property in rootObject) {
    		if (rootObject.hasOwnProperty(property) 
    			&& typeof rootObject[property] != 'function') {  			
    			
       			result.push([property, rootObject[property]])
    		}
		}

		return result;
	}
};
