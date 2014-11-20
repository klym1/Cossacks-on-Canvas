"use strict";

function World(width, height) {
  this.width = width;
  this.height = height;
  this.units = [];
  this.TimeQuantumNumber = 0;
  this.QuantumEpoch = 0;
  this.TickTime = 50;

  this.Run = function(){
  	
  	var self = this;

  	setInterval(function(){
  		for (var i = 0; i < self.units.length; i++) {
  			self.units[i].tick();
  		};

  		self.TimeQuantumNumber++;

  		if(self.TimeQuantumNumber == 1024){
  			self.QuantumEpoch++;
  			self.TimeQuantumNumber = 0;
  		}

  		console.log("World time: " + self.TimeQuantumNumber);	

  	}, this.TickTime);
  }
}