"use strict";

function Command(o){
	this.commandHandler = o.commandHandler;
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
				this.initData = this.init(this.commandHandler.unit);
				this.initialised = true;
			}
		}

		this.callback(this.commandHandler.unit, this.i, this.initData);
		this.commandHandler.commandIsExecuting = true;
	
		this.i++; 
		if(this.finishConditions(this.commandHandler.unit, this.i) === true){
		this.commandHandler.commandIsExecuting = false;
		}
	};
}

Command.prototype.toString = function() {
	return "[*" + this.priority + "] ";
};