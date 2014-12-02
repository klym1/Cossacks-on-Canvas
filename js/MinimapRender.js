"use strict";

define(function(){
	return {
		CreateRender : function(o){
			return new MinimapRender(o);
		}
	}
})

/*
*@constructor()
*/
function MinimapRender(o){
	this.world = o.world;
	this.render = o.render;
	this.height = o.height;
	// this.width will be calculated automatically
}

MinimapRender.prototype.gameLoop = function(){

	var self = this;

	var binded = self.gameLoop.bind(self);

	setTimeout(binded, 100);
	self.Render();	
}

MinimapRender.prototype.init = function(){

	this.miniMap = document.getElementById("minimap");

	this.miniMap.height = this.height;
	this.miniMap.width = this.miniMap.height * (this.world.width / this.world.height);

	this.minimapctx = this.miniMap.getContext("2d");

	this.gameLoop.bind(this)();
}

MinimapRender.prototype.Render = function(){

	this.minimapctx.clearRect(0, 0, this.miniMap.width, this.miniMap.height);

	this.DrawFrame();

	this.DrawUnitsDots();
}

MinimapRender.prototype.DrawUnitsDots = function(){

	var units = this.world.units;

	for (var i = 0; i < units.length; i++) {
		
		var unit = units[i];

		var unitDotX = ( unit.x + unit.State.xSymmetry ) * (this.miniMap.width / this.world.width);
		var unitDotY = ( unit.y + unit.State.ySymmetry ) * (this.miniMap.height / this.world.height);

		this.minimapctx.beginPath();
		this.minimapctx.strokeStyle = "red";
		this.minimapctx.rect(unitDotX, unitDotY, 2, 2);
		
		this.minimapctx.stroke();
	};
}

MinimapRender.prototype.DrawFrame = function(){

	var miniMapRectX = (this.render.canvasOffsetX / this.world.width) * this.miniMap.width;
	var miniMapRectY = (this.render.canvasOffsetY / this.world.height) * this.miniMap.height;

	var miniMapRectWidth = (this.render.canvas_width / this.world.width) * this.miniMap.width;
	var miniMapRectHeight = (this.render.canvas_height / this.world.height) * this.miniMap.height;

	this.minimapctx.beginPath();

	this.minimapctx.strokeStyle = "blue";
	this.minimapctx.rect(miniMapRectX, miniMapRectY, miniMapRectWidth, miniMapRectHeight);
	this.minimapctx.stroke();
}