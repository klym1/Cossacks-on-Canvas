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

  this.commands = [];
  this.commandIsExecuting = false;
  this.currentCommand = null;

  this.State = {};

  this.AvailableCommands = [];

  this.tick = function(){

  	if(this.commandIsExecuting === false){

		if(this.commands.length > 0){

  		console.log("Command added");

		this.currentCommand = this.commands.shift();
		this.currentCommand.Execute();

  		}

	} else {
  		this.currentCommand.Execute();
  	} 	
  };
}

Unit.prototype.Death = function(commandId){
	
	var c = this.AvailableCommands[commandId];
	var numberOfStates = c.Data.length;

	var command = new Command({unit : this, callback : function(user, i){

		user.State.j = c.Data[i % numberOfStates][1];

		for (var j = 0; j < user.states.length; j++) {
			if(user.states[j].Id == c.Data[i % numberOfStates][0]){
				user.State = user.states[j];
			}
		};

		//user.State.j += 1;
		//user.State.j %= user.State.k;	

	}, finishConditions : function(user, i){
		return i > 100;
	}});

	this.commands.push(command);
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

	this.commands.push(command);
};

Unit.prototype.Turn45R = function(){

	var command = new Command({unit : this, callback : function(user){
		
		user.SetState((user.state++) % 14);

		user.n--;
	}, finishConditions : function(user, i){
		return i > 100;
	}});

	this.commands.push(command);
};

Unit.prototype.go = function(N){

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

			user.SetState(2);
			user.n = N;
		},

		finishConditions: function(user, i){
			return i >= 30;
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
		}});

	this.commands.push(command);
};

Unit.prototype.toString = function()
{
	return this.name;
}