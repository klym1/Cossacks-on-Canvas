"use strict";

var world = (function () {
	
var initRunTime = new Date();

	function gameLoop () {

	setTimeout(gameLoop, 50);

	//	mover.
		//window.requestAnimationFrame(gameLoop);
		world.RenderUnits();
	}

	function uppendLine(string){

		var difference = new Date() - initRunTime;
		
		var 
    	minutes = Math.floor(difference % 36e5 / 60000),
    	seconds = Math.floor(difference % 60000 / 1000),
    	milliseconds = Math.floor(difference % 1000);

		document.getElementById("loading-container").innerHTML += "[" + minutes + ":" + seconds + "." + milliseconds+ "] " + string + "<p>";
	}

	var canvas_height = 500;
	var canvas_width = 800;
	
	// Get canvas
	var canvas = document.getElementById("can");
	canvas.width = canvas_width;
	canvas.height = canvas_height;
	
	var world = new World(canvas, 5000, 5000);

	var horseman = new Unit("horseman");

	horseman.x = 200;
	horseman.y = 200;

	world.units.push(horseman); 

	for(var i = 0; i < sprites.length; i++){
		
	var unit_state = new UnitState(sprites[i].UnitName, sprites[i].SpriteHeight, sprites[i].SpriteWidth, sprites[i].NumberOfFrames);

	horseman.states.push(unit_state);
	}
	
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

var unit = world.units[0];