"use strict";

function Unit(name){
  this.name = name;
  this.hp = 100;    
  this.speed = 2.3;
  this.x = 0; 		// x xoordinate
  this.y = 0;		// y coordinate
    
  this.states = [];
  this.n = 0;  // direction
  this.activeState = 0;

  this.commandsQueue = new PriorityQueue("priority");
  this.commandIsExecuting = false;
  this.currentCommand = null;

  this.State = {};

    this.tick = function(){

  	if(this.commandIsExecuting === false){

		if(this.commandsQueue.IsEmpty() === false){

  		console.log("Command added");

		this.currentCommand = this.commandsQueue.getHighestPriorityElement();

  		} else {
			
			this.currentCommand = this.GetDefaultCommand();		
  		}
	} 
  	
  	this.currentCommand.Execute();
  };
}

Unit.prototype.AddCommand = function(command){

	this.commandsQueue.insert(command);

}

Unit.prototype.GetDefaultCommand = function(){

	var random = Math.random() * 100;
	var rest = Math.random() > 0.2 ? 1: 0;
	
		var defaultCommand = new Command({unit : this, callback : function(user, i){

					if(!rest){

					user.State.j += 1;
					user.State.j %= user.State.k;	
				}

				}, finishConditions : function(user, i){
					return i > user.State.k;
				}, init: function(user){
					user.State = user.states[2];
					user.State.j = 0;
				}, priority: -1
			});

	return defaultCommand;
}

Unit.prototype.Death = function(commandId){
	
	var c = this.AvailablecommandsQueue[commandId];
	var numberOfStates = c.Data.length;

	var command = new Command({unit : this, callback : function(user, i){

		user.State.j = c.Data[i % numberOfStates][1];

		for (var j = 0; j < user.states.length; j++) {
			if(user.states[j].Id == c.Data[i % numberOfStates][0]){
				user.State = user.states[j];
			}
		};

	}, finishConditions : function(user, i){
		return i > 100;
	}});

	this.AddCommand(command);
}

Unit.prototype.SetState = function(state){

	this.activeState = state;
	this.State = this.states[state];
}

Unit.prototype.Rotate = function(){

	var command = new Command({unit : this, callback : function(user, i){

		user.n = i % 16;

	}, finishConditions : function(user, i){
		return i > 1000;
	}});

	this.AddCommand(command);
};

Unit.prototype.go = function(N, priority){

	var command = new Command({
		unit : this,
		callback : function(user, i, initData){

			if(i < initData.length - 1)
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
			return i >= 100;
		},

		init: function(user){

			var stepsNumber = 102;

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

