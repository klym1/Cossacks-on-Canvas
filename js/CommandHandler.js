"use strict";

define(["PriorityQueue"], function(priorityQueueModule){

	var queue = priorityQueueModule.CreatePriorityQueue("priority");

	return {
		CreateCommandHandler : function(unit){
			return new CommandHandler(unit, queue);
		}
	}
})

/**
 * @constructor
 */
function CommandHandler(unit, queue){
	this.unit = unit;
    this.commandsQueue = queue;
	this.HighPriorityInterruption = false;
	this.commandIsExecuting = false;
  	this.currentCommand = null;
}

CommandHandler.prototype.AddCommand = function(command){
	this.commandsQueue.insert(command);
}

CommandHandler.prototype.HandleCommandQueue = function(){
	
	 if(this.commandsQueue.IsEmpty()){

		if(this.commandIsExecuting === false){

			var defaultCommand = this.unit.GetDefaultCommand()
    		this.AddCommand(defaultCommand);
    	}

    } else {

    	var queuedCommand = this.commandsQueue.getHighestPriorityElement();	

    	if(this.currentCommand.priority < queuedCommand.priority){

    		this.AddCommand(this.currentCommand);
    		this.HighPriorityInterruption = true;
    	} 
    }
 
	if(this.commandIsExecuting === false || this.HighPriorityInterruption){
		this.currentCommand = this.commandsQueue.shiftHighestPriorityElement();
		this.HighPriorityInterruption = false
	}
 	
  	this.currentCommand.Execute();
}