"use strict";

function Unit(name){
  this.name = name;
  this.hp = 100;    //unit health
  this.speed = 2;
  this.x = 0; 		// x xoordinate
  this.y = 0;		// y coordinate
    
  this.states = [];
  this.n = 0;  // direction
  this.activeState = 0;

  this.commands = [];
  this.commandIsExecuting = false;
  this.currentCommand = null;

  this.tick = function(){

  	if(this.commandIsExecuting === false){

		if(this.commands.length > 0){

  		console.log("Command added");

		this.currentCommand = this.commands.shift();
		this.currentCommand.Execute();

  		} else {
  			this.activeState = 0;
  		}
	} else {
  		this.currentCommand.Execute();
  	} 	
  };
}

Unit.prototype.Turn45L = function(){

	var command = new Command({unit : this, callback : function(user){
		user.n++;
	}, finishConditions : function(user){
		return user.n > 5;
	}});

	this.commands.push(command);
};

Unit.prototype.Turn45R = function(){

	var command = new Command({unit : this, callback : function(user){
		user.n--;
	}, finishConditions : function(user){
		return user.n < 1;
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
			}
			user.activeState = 1;
			user.n = N;
		},

		finishConditions: function(user, i){
			return i >= 100;
		},

		init: function(user){

			var stepsNumber = 100;

			var cachedCoordinates = new Array(stepsNumber);

			var initCoordinates = [user.x, user.y];

			var dx = Math.cos((N + 4) * Math.PI/8);
			var dy = Math.sin((N + 4) * Math.PI/8);
			
			for (var i = 0; i < cachedCoordinates.length - 1; i++) {
			   cachedCoordinates[i] = [Math.round(initCoordinates[0] + dx * i * user.speed), 
			   						   Math.round(initCoordinates[1] + dy * i * user.speed)];
			};

			return cachedCoordinates;
		}});

	this.commands.push(command);
};