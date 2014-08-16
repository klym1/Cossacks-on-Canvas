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
	var state = unit.states[unit.activeState];

	var spriteWidth = state.spriteWidth;
	var spriteHeight = state.spriteHeight;

	state.j++;
	state.j %= state.k;

	//Optional. The x coordinate where to start clipping
	var sx = spriteWidth * unit.n;
	var sy = spriteHeight * state.j;

	//Optional. The width of the clipped image
	//var swidth = spriteWidth;
	//var sheight = spriteHeight;

	//Optional. The width/height of the image to use (stretch or reduce the image)
	var width = spriteWidth;
	var height = spriteHeight;

	var image = this._resources[state.spriteName];

	this.ctx.drawImage(image, sx, sy, width, height, unit.x, unit.y, width, height);

	}
	}
}

