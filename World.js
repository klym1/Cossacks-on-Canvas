"use strict";

function World(canvas, width, height) {
  this.width = width;
  this.height = height;
  this.canvas = canvas;
  this.ctx = this.canvas.getContext("2d");
  this.units = [];
  this.canvas_height = this.canvas.height;
  this.canvas_width = this.canvas.width;
  this._resources = {};

  this.AddUnits = function(unit) {
	if(this.units == null) throw "Unit list is null";
	if(unit == null) throw "Unit is null";

	this.units.push(unit);
}

  this.RenderUnits = function() {

	this.ctx.clearRect(0, 0, this.canvas_width , this.canvas_height);

	for (var i = this.units.length - 1; i >= 0; i--) 
	{
		
	var unit = this.units[i];

	var spriteWidth = unit.spriteWidth;
	var spriteHeight = unit.spriteHeight;

	unit.j++;
	unit.j %= unit.k;

	//Optional. The x coordinate where to start clipping
	var sx = spriteWidth * unit.n;
	var sy = spriteHeight * unit.j;

	//Optional. The width of the clipped image
	var swidth = spriteWidth;
	var sheight = spriteHeight;

	//where to place the image on the canvas
	//x = 
	//y = 

	//Optional. The width/height of the image to use (stretch or reduce the image)
	var width = spriteWidth;
	var height = spriteHeight;

	var image = this._resources[unit.name];

	this.ctx.drawImage(image, sx, sy, swidth, sheight, unit.x, unit.y, width, height);

	//this.ctx.fillRect(unit.x, unit.y,100,100);

	}
	}
}

