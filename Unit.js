function Unit(name, spriteHeight, spriteWidth, n, k){
  this.name = name;
  this.hp = 100;    //unit health
  this.spriteWidth = spriteWidth;
  this.spriteHeight = spriteHeight;
  this.x = 0; 		// x xoordinate
  this.y = 0;		// y coordinate
  this.j = 0;       //current sprite frame offset index
  this.n = n || 9;  // direction
  this.k = k || 15; // number of frames
  
};
