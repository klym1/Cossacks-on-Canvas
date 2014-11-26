//***************//
//Dog discription//
//***************//
NAME ver
MESSAGE ver
//ICON G_Kor
USERLC 0 ver SHADOW -332 -422
//Animation discription:
// #ANIMATION Rotations NFrames FileID_1 frame1 ... FileID_N frameN
#STAND    1 250 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0  0 1 0 1 0 1 0 1 0 1 0 1 0 1 0 1 0 1 0 1  0 1 0 1 0 1 0 1 0 1 0 1 0 1 0 1 0 1 0 1  0 1 0 1 0 1 0 1 0 1 0 1 0 1 0 1 0 1 0 1  0 1 0 1 0 1 0 1 0 1 0 1 0 1 0 1 0 1 0 1  0 1 0 1 0 1 0 1 0 1 0 1 0 1 0 1 0 1 0 1  0 1 0 1 0 1 0 1 0 1 0 1 0 1 0 1 0 1 0 1 0 2 0 2 0 2 0 2 0 2 0 2 0 2 0 2 0 2 0 2   0 2 0 2 0 2 0 2 0 2 0 2 0 2 0 2 0 2 0 2   0 2 0 2 0 2 0 2 0 2 0 2 0 2 0 2 0 2 0 2   0 2 0 2 0 2 0 2 0 2 0 2 0 2 0 2 0 2 0 2   0 2 0 2 0 2 0 2 0 2 0 2 0 2 0 2 0 2 0 2  0 3 0 3 0 3 0 3 0 3 0 3 0 3 0 3 0 3 0 3  0 3 0 3 0 3 0 3 0 3 0 3 0 3 0 3 0 3 0 3  0 3 0 3 0 3 0 3 0 3 0 3 0 3 0 3 0 3 0 3  0 3 0 3 0 3 0 3 0 3 0 3 0 3 0 3 0 3 0 3  0 3 0 3 0 3 0 3 0 3 0 3 0 3 0 3 0 3 0 3  0 3 0 3 0 3 0 3 0 3 0 3 0 3 0 3 0 3 0 3  0 3 0 3 0 3 0 3 0 3 0 3 0 3 0 3 0 3 0 3 0 4 0 4 0 4 0 4 0 4 0 4 0 4 0 4 0 4 0 4   0 4 0 4 0 4 0 4 0 4 0 4 0 4 0 4 0 4 0 4  0 4 0 4 0 4 0 4 0 4 0 4 0 4 0 4 0 4 0 4  0 4 0 4 0 4 0 4 0 4 0 4 0 4 0 4 0 4 0 4  0 4 0 4 0 4 0 4 0 4 0 4 0 4 0 4 0 4 0 4 
#MOTION_L 1 0 0 0   
#MOTION_R 1 0 0 0   
#MOTION_LB 1 0 0 0  
#MOTION_RB 1 0 0 0    
#DEATH     1 0 0 0     
#DEATHLIE1 1 0 0 0   
#ATTACK    1 0 0 0   
//Activepoints for shot
//GEOMETRY Radius1 Radius2 StepSize
GEOMETRY 1 64 16
DAMAGE   0 30
LIFE     300
COST     500 0 0
SHIELD   5
//RANGE AttackRange MaxAttackRange MinAttRange MaxNearRange ActionType VisibilityRadius
RANGE    2 1 4 0 0 9
MAGIC    256
TIME     50
KIND     1
//Matheruals:
//BODY,STONE,WOOD,IRON,AIR
MATHERIAL 1 WOOD
RECTANGLE 0 2 32 40
PORTION  4 WOOD 50 GOLD 50 STONE 50 FOOD 50
WEAPON 0 SHOTFOG
ATTACK_ANGLES		0 -255 255
ATTACK_RADIUS		0 20 2000
ADD_ATTACK_RADIUS	0 32
ATTACK_PAUSE		0 140
SEARCH_ENEMY_RADIUS 3000
CANKILL   1 BODY
ZPOINTS   30 30
[END]