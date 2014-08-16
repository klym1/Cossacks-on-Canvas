function Unit(name){
  this.name = name;
  this.hp = 100;    //unit health

  this.x = 0; 		// x xoordinate
  this.y = 0;		// y coordinate
    
  this.states = [];
  this.n = 0;  // direction
  this.activeState = 0;
};