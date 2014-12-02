"use strict";

define(function(){
    return {
        CreatePriorityQueue : function(criteria){
            return new PriorityQueue(criteria);
        }
    }
})

/**
 * @constructor
 */
function PriorityQueue(criteria) {
    this.criteria = criteria;
    this.length = 0;
    this.queue = [];
}

PriorityQueue.prototype.IsEmpty = function(){
    return this.queue.length === 0;
}

PriorityQueue.prototype.insert = function (value) {
    if (!value.hasOwnProperty(this.criteria)) {
        throw "Cannot insert " + value + " because it does not have a property by the name of " + this.criteria + ".";
    }
    this.queue.push(value);
    this.length++;
    this.bubbleUp(this.length - 1);
};
PriorityQueue.prototype.getHighestPriorityElement = function () {
    return this.queue[0];
};

PriorityQueue.prototype.shiftHighestPriorityElement = function () {
    this.length--;
    return this.queue.shift();
};

PriorityQueue.prototype.bubbleUp = function (index) {
    if (index === 0) {
        return;
    }

    for (var i = this.queue.length - 1; i > 0; i--) {
        if(this.queue[i][this.criteria] > this.queue[i-1][this.criteria]){
            this.swap(i-1, i);
        }
    };
};

PriorityQueue.prototype.swap = function (self, target) {
    var placeHolder = this.queue[self];
    this.queue[self] = this.queue[target];
    this.queue[target] = placeHolder;
};