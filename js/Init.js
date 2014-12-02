"use strict";

var SHOW_DEBUG_INFO = true;
var MINIMAP_HEIGHT = 150; // width is calculated automatically (to have ratio same as the world has)

var WORLD_WIDTH = 1000;
var WORLD_HEIGHT = 500;

var GAME_WINDOW_WIDTH = 800;
var GAME_WINDOW_HEIGHT = 500;

require.config({
    baseUrl: 'js'
});

var World;

require(["World", "Unit", "UnitState", "Render", "MinimapRender"], function(worldModule, unitModule, unitStateModule, renderModule, minimapRenderModule) {
   
    var world = worldModule.CreateNewWorld(WORLD_WIDTH, WORLD_HEIGHT);
    var grenadier = unitModule.createUnit("Grenadier");

    grenadier.x = world.width / 2;
    grenadier.y = world.height / 2;

    world.units.push(grenadier); 

    for(var i = 0; i < sprites.length; i++){
        
        var unit_state = unitStateModule.CreateUnitState(
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

    world.Run();

    window.render = renderModule.CreateRender({
        world : world,
        height : GAME_WINDOW_HEIGHT,
        width : GAME_WINDOW_WIDTH
    });
    
    window.render.init();

    var minimapRender = minimapRenderModule.CreateRender ({
        render : render,
        world : world,
        height : MINIMAP_HEIGHT
    });

    minimapRender.init();

    render.CenterView();

    window.unit = world.units[0];
});


