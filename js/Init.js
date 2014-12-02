"use strict";

var SHOW_DEBUG_INFO = true;
var MINIMAP_HEIGHT = 150; // width is calculated automatically (to have ratio same as the world has)

var WORLD_WIDTH = 1000;
var WORLD_HEIGHT = 500;

var GAME_WINDOW_WIDTH = 800;
var GAME_WINDOW_HEIGHT = 500;

var world = (function () {
    
    var world = new World(WORLD_WIDTH, WORLD_HEIGHT);

    var grenadier = new Unit("Grenadier");

    grenadier.x = world.width / 2;
    grenadier.y = world.height / 2;

    world.units.push(grenadier); 

    for(var i = 0; i < sprites.length; i++){
        
        var unit_state = new UnitState(
            sprites[i].UnitName, 
            sprites[i].SpriteHeight, 
            sprites[i].SpriteWidth, 
            sprites[i].NumberOfFrames,
            sprites[i].Id,
            sprites[i].XSymmetry,
            sprites[i].YSymmetry);

        grenadier.states.push(unit_state);
    }   
    
    grenadier.SetState(3);

    return world;
} ());

world.Run();

var render = new Render({
    world : world,
    height : GAME_WINDOW_HEIGHT,
    width : GAME_WINDOW_WIDTH
});
    render.init();

var minimapRender = new MinimapRender({
    render : render,
    world : world,
    height : MINIMAP_HEIGHT
});

minimapRender.init();

render.CenterView();

var unit = world.units[0];