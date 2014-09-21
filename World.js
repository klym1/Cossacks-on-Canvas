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
	if(this.units === null) throw "Unit list is null";
	if(unit === null) throw "Unit is null";

	this.units.push(unit);
  };

	var unit, 
	state, 
	spriteWidth, 
	spriteHeight,
	sx,
	sy,
	image,
	i;

  this.RenderUnits = function() {

	this.ctx.clearRect(0, 0, this.canvas_width , this.canvas_height);

	for (i = this.units.length - 1; i >= 0; i--) 
	{
		
	unit = this.units[i];
	state = unit.states[unit.activeState];

	spriteWidth = state.spriteWidth;
	spriteHeight = state.spriteHeight;

	state.j++;
	state.j %= state.k;

	sx = spriteWidth * unit.n;
	sy = spriteHeight * state.j;

	image = this._resources[state.spriteName];

	this.ctx.drawImage(image, sx, sy, spriteWidth, spriteHeight, unit.x, unit.y, spriteWidth, spriteHeight);

	}
	};
}