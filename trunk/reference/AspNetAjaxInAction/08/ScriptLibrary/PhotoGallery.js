Type.registerNamespace('Samples');

// PhotoGallery class.
Samples.PhotoGallery = function(element) {
    Samples.PhotoGallery.initializeBase(this, [element]);
    
    this._images = [];
    this._imageElement = null;
    this._nextElement = null;
    this._prevElement = null;
    this._progressElement = null;
    this._index = -1;
    this._imgPreload = null;
}
Samples.PhotoGallery.prototype = {
    initialize : function() {
        Samples.PhotoGallery.callBaseMethod(this, 'initialize');
        
        $addHandlers(this._nextElement, {click: this.viewNext}, this);
        $addHandlers(this._prevElement, {click: this.viewPrev}, this);
        
        this._imgPreload = document.createElement('IMG');
        $addHandlers(this._imgPreload, {load: this._onimageElementLoaded}, this);
               
        if(this._index >= 0) {
            this._render();
        }
    },
    
    dispose : function() {
        $clearHandlers(this._prevElement);
        $clearHandlers(this._nextElement);
        $clearHandlers(this._imgPreload);
        
        Samples.PhotoGallery.callBaseMethod(this, 'dispose');
    },
    
    viewPrev : function(evt) {
        if(this._index > 0) {
            this._index--;
            this._render();
        }
    },

    viewNext : function(evt) {
        if(this._index < this._images.length - 1) {
            this._index++;
            this._render();
        }
    },

    _render : function() {
        this._prevElement.disabled = (this._index == 0);
        this._nextElement.disabled = (this._index == this._images.length - 1);
        
        // Display the indicator.
        this._progressElement.style.visibility = 'visible';
        
        // Preload the image.
        this._imgPreload.src = this._images[this._index];
    },

    _onimageElementLoaded : function() {
        this._displayimageElement();
    },

    _displayimageElement : function() {
        // Hide the indicator.
        this._progressElement.style.visibility = 'hidden';
        
        // Display the image.
        this._imageElement.src = this._images[this._index];   
    },
       
    // Properties.
    get_images : function() {
        return this._images;
    },
    
    set_images : function(value) {
        this._images = value;
        
        if(this._images.length > 0) {
            this._index = 0;
            
            if(this.get_isInitialized()) {
                this._render();
            }
        }
    },
    
    get_prevElement : function() {
        return this._prevElement;
    },
    
    set_prevElement : function(value) {
        this._prevElement = value;
    },
    
    get_nextElement : function() {
        return this._nextElement;
    },
    
    set_nextElement : function(value) {
        this._nextElement = value;
    },
    
    get_imageElement : function() {
        return this._imageElement;
    },
    
    set_imageElement : function(value) {
        this._imageElement = value;
    },
    
    get_progressElement : function() {
        return this._progressElement;
    },
    
    set_progressElement : function(value) {
        this._progressElement = value;
    }
}
Samples.PhotoGallery.registerClass('Samples.PhotoGallery', Sys.UI.Control);
