"use strict";

function Unit(name){
  this.name = name;
  this.hp = 100;    //unit health

  this.x = 0; 		// x xoordinate
  this.y = 0;		// y coordinate
    
  this.states = [];
  this.n = 0;  // direction
  this.activeState = 0;

  this.commands = [];
  this.commandIsExecuting = false;
  this.currentCommand = null;

  this.tick = function(){

  	console.log("tick");
  	console.log("Commands: " + this.commands.length);
  	if(this.commandIsExecuting === false){

		if(this.commands.length > 0){

  		console.log("Command added");

		this.currentCommand = this.commands.pop();
		this.currentCommand.Execute();

  		}

	} else {
  		this.currentCommand.Execute();
  	} 	
  };
}

Unit.prototype.Turn45L = function(){

	var command = new Command(this, function(user){
		user.n++;
	}, function(user){
		return user.n > 5;
	});

	this.commands.push(command);
};

Unit.prototype.Turn45R = function(){
	var command = new Command(this, function(user){
		user.n--;
	}, function(user){
		return user.n < 1;
	});

	this.commands.push(command);
};

function Command(unit, callback, finishConditions){
	this.unit = unit;
	this.callback = callback;

	this.Execute = function(){
		this.callback(this.unit);
		this.unit.commandIsExecuting = true;
		console.log("n = " + this.unit.n);

		if(finishConditions(this.unit) === true){
		this.unit.commandIsExecuting = false;
		}

	};
}