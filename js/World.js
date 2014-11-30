"use strict";

function World(width, height) {
  this.width = width;
  this.height = height;
  this.units = [];
  this.TickTime = 30;

  this.Run = function(){
  	
  	var self = this;

  	setInterval(function(){
  		for (var i = 0; i < self.units.length; i++) {
  			self.units[i].tick();
  		};

  	}, this.TickTime);
  }
}