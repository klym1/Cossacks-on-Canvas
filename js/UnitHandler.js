"use strict";

define(function(){
	return{
		CreateHandler : function(unit){
			return new UnitHandler(unit);
		}
	}
})

/**
 * @constructor
 */
function UnitHandler(unit){
	this.unit = unit;
}

UnitHandler.prototype.Go = function(x,y) {
	unit.go(4);
};