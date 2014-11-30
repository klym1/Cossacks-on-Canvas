"use strict";

function CommandHandler(unit){
    
	this.unit = unit;
    this.commandsQueue = new PriorityQueue("priority");
	this.HighPriorityInterruption = false;
}

CommandHandler.prototype.AddCommand = function(command){
	this.commandsQueue.insert(command);
}

CommandHandler.prototype.HandleCommandQueue = function(){
	 if(this.commandsQueue.IsEmpty()){

		if(this.unit.commandIsExecuting === false){

			var defaultCommand = this.unit.GetDefaultCommand()
    		this.AddCommand(defaultCommand);
    	}

    } else {

    	var queuedCommand = this.commandsQueue.getHighestPriorityElement();	

    	if(this.unit.currentCommand.priority < queuedCommand.priority){

    		this.AddCommand(this.unit.currentCommand);
    		this.HighPriorityInterruption = true;
    	} 
    }
 
	if(this.unit.commandIsExecuting === false || this.HighPriorityInterruption){
		this.unit.currentCommand = this.commandsQueue.shiftHighestPriorityElement();
		this.HighPriorityInterruption = false
	}
 	
  	this.unit.currentCommand.Execute();
}