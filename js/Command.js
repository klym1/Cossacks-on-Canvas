"use strict";

function Command(o){
	this.unit = o.unit;
	this.callback = o.callback;
	this.finishConditions = o.finishConditions;
	this.i = 0; //command iteration
	this.initialised = false;
	this.init = o.init;
	this.initData = {};
	this.priority = o.priority || 0;

	this.Execute = function(){

		if(this.initialised === false){
			if(this.init !== undefined){
				this.initData = this.init(this.unit);
				this.initialised = true;
				console.log("Command initialised");
			}
		}

		this.callback(this.unit, this.i, this.initData);
		this.unit.commandIsExecuting = true;
	
		this.i++; 
		if(this.finishConditions(this.unit, this.i) === true){
		this.unit.commandIsExecuting = false;
		}
	};
}

Command.prototype.toString = function() {
	return "[*" + this.priority + "] ";
};