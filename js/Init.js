var SHOW_DEBUG_INFO = false;

var world = (function () {
    
    var world = new World(5000, 5000);

    var grenadier = new Unit("Grenadier");

    grenadier.x = 500;
    grenadier.y = 100;

    world.units.push(grenadier); 

    for(var i = 0; i < sprites.length; i++){
        
        var unit_state = new UnitState(
            sprites[i].UnitName, 
            sprites[i].SpriteHeight, 
            sprites[i].SpriteWidth, 
            sprites[i].NumberOfFrames,
            sprites[i].Id,
            sprites[i].XSymmetry);

        grenadier.states.push(unit_state);
    }

    for(var i = 0; i < transitions.length; i++){
        
        grenadier.AvailableCommands.push(transitions[i]);
    }
    
    grenadier.SetState(0);

    return world;
} ());

world.Run();

var render = new Render(world);
    render.init();

var unit = world.units[0];