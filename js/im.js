"use strict";

/**
 * @constructor
 */
function IM(render, loading_completed_callback, update_callback){
            this.store =  render._resources;
            this.imagesAdded = 0;
            this.imagesLoaded = 0;
            this.add = function(url, name) {
                var self  = this;
                var image = new Image();
                image.onload = function() {
                    self.imagesLoaded++;
                    update_callback(self.imagesAdded, self.imagesLoaded);
                    if (self.imagesAdded == self.imagesLoaded) {
                        loading_completed_callback();
                    }
                };
                image.src = url;
                this.store[name] = image;
                this.imagesAdded++;
            };
        }
      