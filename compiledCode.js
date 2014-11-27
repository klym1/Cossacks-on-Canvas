var sprites=[{SpriteHeight:181,SpriteWidth:190,NumberOfFrames:24,NumberOfDirections:16,UnitName:"GUSH"},{SpriteHeight:116,SpriteWidth:130,NumberOfFrames:20,NumberOfDirections:16,UnitName:"GUSN"},{SpriteHeight:116,SpriteWidth:130,NumberOfFrames:16,NumberOfDirections:16,UnitName:"GUSS"},{SpriteHeight:116,SpriteWidth:130,NumberOfFrames:16,NumberOfDirections:16,UnitName:"GUSU"}];function IM(b,c,a){this.store=b._resources;this.imagesLoaded=this.imagesAdded=0;this.add=function(e,b){var f=this,h=new Image;h.onload=function(){f.imagesLoaded++;a(f.imagesAdded,f.imagesLoaded);f.imagesAdded==f.imagesLoaded&&c()};h.src=e;this.store[b]=h;this.imagesAdded++}};function World(b,c){this.width=b;this.height=c;this.units=[];this.QuantumEpoch=this.TimeQuantumNumber=0;this.TickTime=30;this.Run=function(){var a=this;setInterval(function(){for(var e=0;e<a.units.length;e++)a.units[e].tick();a.TimeQuantumNumber++;1024==a.TimeQuantumNumber&&(a.QuantumEpoch++,a.TimeQuantumNumber=0);console.log("World time: "+a.TimeQuantumNumber)},this.TickTime)}};function Command(b){this.unit=b.unit;this.callback=b.callback;this.finishConditions=b.finishConditions;this.i=0;this.initialised=!1;this.init=b.init;this.initData={};this.Execute=function(){!1===this.initialised&&void 0!==this.init&&(this.initData=this.init(this.unit),this.initialised=!0,console.log("Command initialised"));this.callback(this.unit,this.i,this.initData);this.unit.commandIsExecuting=!0;this.i++;!0===this.finishConditions(this.unit,this.i)&&(this.unit.commandIsExecuting=!1)}}
Command.prototype.toString=function(){return this.i};function Unit(b){this.name=b;this.hp=100;this.speed=2.3;this.y=this.x=0;this.states=[];this.activeState=this.n=0;this.commands=[];this.commandIsExecuting=!1;this.currentCommand=null;this.State={};this.tick=function(){!1===this.commandIsExecuting?0<this.commands.length&&(console.log("Command added"),this.currentCommand=this.commands.shift(),this.currentCommand.Execute()):this.currentCommand.Execute()}}Unit.prototype.SetState=function(b){this.activeState=b;this.State=this.states[b]};
Unit.prototype.Turn45L=function(){var b=new Command({unit:this,callback:function(b){b.n++},finishConditions:function(b){return 5<b.n}});this.commands.push(b)};Unit.prototype.Rotate=function(){var b=new Command({unit:this,callback:function(b,a){b.n=a%16},finishConditions:function(b,a){return 1E3<a}});this.commands.push(b)};Unit.prototype.Turn45R=function(){var b=new Command({unit:this,callback:function(b){b.SetState(b.state++%14);b.n--},finishConditions:function(b,a){return 100<a}});this.commands.push(b)};
Unit.prototype.go=function(b){var c=new Command({unit:this,callback:function(a,e,d){e<d.length-1&&(a.x=d[e][0],a.y=d[e][1],a.State.j+=1,a.State.j%=a.State.k);a.SetState(1);a.n=b},finishConditions:function(a,b){return 30<=b},init:function(a){for(var e=Array(102),d=[a.x,a.y],f=Math.cos(Math.PI/8*(b+4)),c=Math.sin(Math.PI/8*(b+4)),g=0;g<e.length-1;g++)e[g]=[Math.round(d[0]+f*g*a.speed),Math.round(d[1]+c*g*a.speed)];return e}});this.commands.push(c)};Unit.prototype.toString=function(){return this.name};function UnitState(b,c,a,e){this.spriteName=b;this.spriteWidth=a;this.spriteHeight=c;this.j=0;this.k=e}UnitState.prototype.toString=function(){return"["+this.k+" "+this.j+"]"};(function(){for(var b=0,c=["ms","moz","webkit","o"],a=0;a<c.length&&!window.requestAnimationFrame;++a)window.requestAnimationFrame=window[c[a]+"RequestAnimationFrame"],window.cancelAnimationFrame=window[c[a]+"CancelAnimationFrame"]||window[c[a]+"CancelRequestAnimationFrame"];window.requestAnimationFrame||(window.requestAnimationFrame=function(a,d){var f=(new Date).getTime(),c=Math.max(0,16-(f-b)),g=window.setTimeout(function(){a(f+c)},c);b=f+c;return g});window.cancelAnimationFrame||(window.cancelAnimationFrame=
function(a){clearTimeout(a)})})();function Render(b){var c=new Date;this._resources={};this.world=b;this.gameLoop=function(){var a=this.gameLoop.bind(this);window.requestAnimationFrame(a);this.RenderUnits()};this.uppendLine=function(a){var b=new Date-c,d=Math.floor(b%36E5/6E4),f=Math.floor(b%6E4/1E3),b=Math.floor(b%1E3);document.getElementById("loading-container").innerHTML+="["+d+":"+f+"."+b+"] "+a+"<p>"};this.init=function(){var a=this;this.canvas=document.getElementById("can");this.canvas.width=800;this.canvas.height=500;this.canvas_height=
this.canvas.height;this.canvas_width=this.canvas.width;this.ctx=this.canvas.getContext("2d");for(var b=new IM(this,function(){a.uppendLine("Loading finished in ");a.gameLoop.bind(a)()},function(b,d){a.uppendLine((d/b*100+"").substring(0,6)+" %")}),d=0;d<sprites.length;d++)b.add("sprites_png/"+sprites[d].UnitName+".png",sprites[d].UnitName)};this.RenderUnits=function(){this.WriteStatusInfo(this.world);this.ctx.clearRect(0,0,this.canvas_width,this.canvas_height);for(var a=this.world.units,b=a.length-
1;0<=b;b--){var d=a[b],c=d.State,h=c.spriteWidth,g=c.spriteHeight;this.ctx.drawImage(this._resources[c.spriteName],h*d.n,g*c.j,h,g,d.x,d.y,h,g);this.ctx.rect(d.x,d.y,2,2);this.ctx.stroke()}};this.WriteStatusInfo=function(a){!0===SHOW_DEBUG_INFO?(document.getElementById("world-info").innerHTML=this.MaterializeStatusData(this.GetInfoRecursively(a)),document.getElementById("unit-info").innerHTML=this.MaterializeStatusData(this.GetInfoRecursively(a.units[0]))):(document.getElementById("world-info").innerHTML=
null,document.getElementById("unit-info").innerHTML=null)};this.MaterializeStatusData=function(a){for(var b="",d=0;d<a.length;d++)var c=a[d],b=b+(c[0]+": "+c[1]+"</p>");return b};this.GetInfoRecursively=function(a){var b=[],c;for(c in a)a.hasOwnProperty(c)&&"function"!=typeof a[c]&&b.push([c,a[c]]);return b}};var SHOW_DEBUG_INFO=!1,world=function(){var b=new World(5E3,5E3),c=new Unit("Grenadier");c.x=500;c.y=100;b.units.push(c);for(var a=0;a<sprites.length;a++){var e=new UnitState(sprites[a].UnitName,sprites[a].SpriteHeight,sprites[a].SpriteWidth,sprites[a].NumberOfFrames);c.states.push(e)}c.SetState(1);return b}();world.Run();var render=new Render(world);render.init();var unit=world.units[0];