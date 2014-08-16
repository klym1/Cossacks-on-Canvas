function Unit(name, n){
  this.name = name;
  this.hp = 100;    //unit health

  this.x = 0; 		// x xoordinate
  this.y = 0;		// y coordinate
    
  this.states = [];
  this.n = n || 9;  // direction
  this.activeState = 0;
};