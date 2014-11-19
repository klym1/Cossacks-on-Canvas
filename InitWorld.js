"use strict";

var world = (function () {
	
	var world = new World(5000, 5000);

	var grenadier = new Unit("Grenadier");

	grenadier.x = 500;
	grenadier.y = 300;

	world.units.push(grenadier); 

	for(var i = 0; i < sprites.length; i++){
		
		var unit_state = new UnitState(sprites[i].UnitName, sprites[i].SpriteHeight, sprites[i].SpriteWidth, sprites[i].NumberOfFrames);
		grenadier.states.push(unit_state);
	}
	
	return world;
} ());