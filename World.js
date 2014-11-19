"use strict";

function World(width, height) {
  this.width = width;
  this.height = height;
  this.units = [];
  
  this.AddUnits = function(unit) {
	if(this.units === null) throw "Unit list is null";
	if(unit === null) throw "Unit is null";

	this.units.push(unit);
  };
}