/*
    Ex:
	var Office = Office || {};
	
	Office.Context = Office.Context || {};
	
	var waitObj = new waitObject(Office.Context);
	
	waitObj.when(function (obj) { 
		console.log('type is: ' + typeof obj.document);
		return typeof obj.document !== 'undefined' 
	})
	.then(function (obj) {
		console.log('document is ready');
	})
	.start();
*/
var waitObject = (function () {
    var MAX_TIMEOUT = 10;

    function waitObject(obj) {
        this._obj = obj;

        this._try = 0;
    }

    waitObject.prototype.when = function (fn) {
        this._when = fn;

        return this;
    }

    waitObject.prototype.then = function (fn) {
        this._then = fn;

        return this;
    }

    waitObject.prototype.start = function () {
        this._try++;

        if (this._try > MAX_TIMEOUT) {
            return;
        }

        setTimeout(function () {
            if (this._when.call(this, this._obj)) {
                this._then.call(this, this._obj);
            } else {
                this.start();
            }
        }.bind(this), 1000);
    }

    return waitObject;
})();