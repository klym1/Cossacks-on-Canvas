"use strict";

var world = (function () {
	
var initRunTime = new Date();

	function gameLoop () {
		window.requestAnimationFrame(gameLoop);
		world.RenderUnits();
	}

	function uppendLine(string){
		loadingContainer.innerText += "[" + (new Date() - initRunTime) + "]\t" + string + "\n";
	}

	var canvas_height = 500;
	var canvas_width = 800;
	
	// Get canvas
	var canvas = document.getElementById("can");
	canvas.width = canvas_width;
	canvas.height = canvas_height;
	
	var world = new World(canvas, 5000, 5000);

	var horseman = new Unit("horseman", 4); // 3 = direction

	horseman.x = 200;
	horseman.y = 200;

	world.units.push(horseman); 

	for(var i = 0; i < sprites.length; i++){
		
	var unit_state = new UnitState(sprites[i].UnitName, sprites[i].SpriteHeight, sprites[i].SpriteWidth, sprites[i].NumberOfFrames);

	horseman.states.push(unit_state);
	}

	var loadingContainer = document.getElementById("loading-container");

	var imageLoader = new IM(world, function(){ 
		
		uppendLine("Loading finished in ");

		return gameLoop(); 
	}, function(a,b){
		uppendLine( (b/a * 100 + "").substring(0,6) + " %");
	});
	
	for(var i = 0; i < sprites.length; i++){
		imageLoader.add('sprites_png/'+ sprites[i].UnitName + '.png', sprites[i].UnitName);
	}

	return world;

} ());

