"use strict";

var world = (function () {
	
var initRunTime = new Date();

	function gameLoop () {
		window.requestAnimationFrame(gameLoop);
		world.RenderUnits();
	}

	var canvas_height = 500;
	var canvas_width = 800;
	
	// Get canvas
	var canvas = document.getElementById("can");
	canvas.width = canvas_width;
	canvas.height = canvas_height;
	
	var world = new World(canvas, 5000, 5000);

	var horseman = new Unit("horseman", 3);

	horseman.x = 200;
	horseman.y = 200;

	world.units.push(horseman); // 3 = direction

	for(var i=0;i<sprites.length; i++){
		
	var unit_state = new UnitState(sprites[i].UnitName, sprites[i].SpriteHeight, sprites[i].SpriteWidth, sprites[i].NumberOfFrames);

	horseman.states.push(unit_state);
	}

	//	document.addEventListener("keydown",keyDownHandler, false);	
	//	document.addEventListener("keyup",keyUpHandler, false);

	var imageLoader = new IM(world, function(){ 
		//when loading finished;

		console.log("Loading finished in " + (new Date() - initRunTime));

		return gameLoop(); 
	});
	
	for(var i = 0; i < sprites.length; i++){
		imageLoader.add('sprites_png/'+sprites[i].UnitName+'.png', sprites[i].UnitName);
	}

	return world;

} ());

