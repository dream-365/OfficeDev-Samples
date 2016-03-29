(function ($) {
    function executeQueryAsync(succeededCallback, failedCallback)
    {
        var period = Math.random() * 10000;

        setTimeout(function () {
            succeededCallback();
        }, period);

    }

    function forEachAsync(items, funcAsync, callback)
    {
        var count = 0;

        var total = $.Deferred();

        function increment()
        {
            count++;

            if(count == items.length)
            {
                total.resolve();
            }
        }

        for (var i = 0; i < items.length; i++)
        {
            (function exec(item) {
                var deferred = $.Deferred(function (defer) {
                    funcAsync(function () {
                        callback();
                        defer.resolve();
                    });
                });

                deferred.done(function () {
                    increment();
                });
            })(items[i]);
        }

        return total.promise();
    }

    var promise = forEachAsync([1, 2, 3, 4, 5], executeQueryAsync, function () {
        console.log('call back executing + ' + Date.now());
    });

    promise.done(function () {
        console.log("promise done");
    });
})(jQuery);