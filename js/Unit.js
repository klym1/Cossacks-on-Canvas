"use strict";

define(["CommandHandler", "Command"],function(commandHandlerModule, commandModule){
	return {
		createUnit : function(name){
			return new Unit(name, commandHandlerModule, commandModule);
		}
	}
})

/**
 * @constructor
 */
function Unit(name, commandHandlerModule, commandModule){
  this.name = name;
  this.hp = 100;    
  this.speed = 2.3;
  this.x = 0; 		// x xoordinate
  this.y = 0;		// y coordinate
    
  this.states = [];
  this.n = 0;  // direction
  this.activeState = 0;
   
  this.commandHandler = commandHandlerModule.CreateCommandHandler(this);
  this.commandModule = commandModule;
  this.State = {};
  this.IsSelected = false;
}

Unit.prototype.tick = function(){
	this.commandHandler.HandleCommandQueue();
 };

Unit.prototype.AddCommand = function(command){
	this.commandHandler.AddCommand(command);
}

Unit.prototype.GetDefaultCommand = function(){

	var random = Math.random() * 100;
	var rest = Math.random() > 0.2 ? 1: 0;
	
	var defaultCommand = this.commandModule.CreateCommand({
		commandHandler : this.commandHandler, 
		callback : function(user, i){
			if(!rest){

				user.State.j += 1;
				user.State.j %= user.State.k;	
			}
		}, 

		finishConditions : function(user, i){
			return i > user.State.k;
		}, 

		init: function(user){
			user.State = user.states[2];
			user.State.j = 0;
		}, 

		priority: -1});

	return defaultCommand;
}

Unit.prototype.SetState = function(state){

	this.activeState = state;
	this.State = this.states[state];
}

Unit.prototype.Rotate = function(){

	var command = this.commandModule.CreateCommand({commandHandler : this.commandHandler, callback : function(user, i){

		user.n = i % 16;

	}, finishConditions : function(user, i){
		return i > 100;
	}});

	this.AddCommand(command);
};

Unit.prototype.go = function(N, priority){

	var stepsNumber = 30;

	var command = this.commandModule.CreateCommand({
		commandHandler : this.commandHandler,
		callback : function(user, i, initData){

			//if(i < initData.length - 1)
			{
				user.x = initData[i][0];
				user.y = initData[i][1];

				user.State.j += 1;
				user.State.j %= user.State.k;	
			}

			user.SetState(3);
			user.n = N;
		},

		finishConditions: function(user, i){
			return i > stepsNumber - 10;
		},

		init: function(user){

			var cachedCoordinates = new Array(stepsNumber);

			var initCoordinates = [user.x, user.y];

			var dx = Math.cos((N + 4) * (Math.PI/8));
			var dy = Math.sin((N + 4) * (Math.PI/8));
			
			for (var i = 0; i < cachedCoordinates.length - 1; i++) {
			   cachedCoordinates[i] = [Math.round(initCoordinates[0] + dx * i * user.speed), 
			   						   Math.round(initCoordinates[1] + dy * i * user.speed)];
			};

			return cachedCoordinates;
		},
		priority : priority || 0});

	this.AddCommand(command);
};

Unit.prototype.toString = function()
{
	return this.name;
}