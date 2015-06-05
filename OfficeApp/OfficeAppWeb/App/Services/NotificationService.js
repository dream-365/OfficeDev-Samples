(function (app) {
    app.factory('notificationSvc', function () {
        var overylay = document.getElementById('overlay');

        function setAttribute(ele, name, value)
        {
            var attr = document.createAttribute(name);

            attr.value = value;

            ele.attributes.setNamedItem(attr);
        }

        function createDom(type, msg)
        {
            var div = document.createElement('div');

            setAttribute(div, 'class', 'alert alert-dismissible alert-' + type);

            setAttribute(div, 'role', 'alert');

            var button = document.createElement('button');

            // the line of code below will case 'member not found' error in IE
            // setAttribute(button, 'type', 'button');

            setAttribute(button, 'class', 'close');
            setAttribute(button, 'data-dismiss', 'alert');
            setAttribute(button, 'aria-label', 'Close');

            var span = document.createElement('span');
           
            setAttribute(span, 'aria-hidden', 'true');

            span.textContent = 'x';

            button.appendChild(span);

            div.appendChild(button);

            button.addEventListener('click', function () {
                var overylay = document.getElementById('overlay');

                overylay.style.display = "none";
            })

            var text = document.createTextNode(msg);

            div.appendChild(text);
            
            return div;
        }

        return {
            info: function (msg) {
                overylay.style.display = "block";

                overylay.appendChild(createDom('info', msg));
            },
            success: function (msg) {
                overylay.style.display = "block";

                overylay.appendChild(createDom('success', msg));
            },
            warning: function (msg) {
                overylay.style.display = "block";

                overylay.appendChild(createDom('warning', msg));
            },

            danger: function (msg) {
                overylay.style.display = "block";

                overylay.appendChild(createDom('danger', msg));
            }
        };
    });
})(app);