"use strict";

function UnitState(spriteName, spriteHeight, spriteWidth, k){
  this.spriteName = spriteName;

  this.spriteWidth = spriteWidth;
  this.spriteHeight = spriteHeight;

  this.j = 0; //current sprite frame offset index
  
  this.k = k; // number of frames
}