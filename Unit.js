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

  	//console.log("tick");
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

Unit.prototype.complexTurn = function(){
	var command = new Command(this, function(user, i){

		var k = (i % 8 >= 4)? -1: 1;
		user.n += 1 * k;

		if(i % 33 === 0) user.activeState++	;

	}, function(user, i){
		return i > 100;
	});

	this.commands.push(command);
};

Unit.prototype.go = function(N){

	var self = this;

	var command = new Command(this, function(user, i){

		var dy = Math.sin((N + 4) * Math.PI/8);
		var dx = Math.cos((N + 4) * Math.PI/8);	

		user.x += (dx * self.speed);
		user.y += (dy * self.speed);

		user.activeState = 1;
		user.n = N;

	}, function(user, i){
		return i > 30;
	});

	this.commands.push(command);
};

function Command(unit, callback, finishConditions){
	this.unit = unit;
	this.callback = callback;
	this.finishConditions = finishConditions;
	this.i = 0; //command iteration

	this.Execute = function(){
		this.callback(this.unit, this.i);
		this.unit.commandIsExecuting = true;
		console.log("i = " + this.i);
		
		this.i++; 
		if(this.finishConditions(this.unit, this.i) === true){
		this.unit.commandIsExecuting = false;
		}

	};
}