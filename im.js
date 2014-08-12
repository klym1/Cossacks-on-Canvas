"use strict";

function IM(world, loading_completed_callback){
            this.store =  world._resources;
            this.imagesAdded = 0;
            this.imagesLoaded = 0;
            this.add = function(url, name) {
                var self  = this;
                var image = new Image();
                image.onload = function() {
                    self.imagesLoaded++;
                    if (self.imagesAdded == self.imagesLoaded) {
                        console.log('Images loaded');
                        loading_completed_callback();
                    }
                }
                image.src = url;
                this.store[name] = image;
                this.imagesAdded++;
            }
        };
      