"use strict";

/**
 * @constructor
 */
function UnitState(spriteName, spriteHeight, spriteWidth, k, id, xSymmetry, ySymmetry){
  this.spriteName = spriteName;

  this.spriteWidth = spriteWidth;
  this.spriteHeight = spriteHeight;
  
  this.j = 0; //current sprite frame offset index
  this.k = k; // number of frames

  this.Id = id;
  this.xSymmetry = xSymmetry;
  this.ySymmetry = ySymmetry;
}

UnitState.prototype.toString = function()
{
    return "[" + this.spriteWidth + " " + this.spriteHeight + "]";
}