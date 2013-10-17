var KoMouseover = KoMouseover || {};

KoMouseover.SetupBindings = function(ko) {
    ko.bindingHandlers.hoverTargetId = {};
    ko.bindingHandlers.hoverVisible = {
        init: function (element, valueAccessor, allBindingsAccessor) {

            function showOrHideElement(show) {
                var canShow = ko.utils.unwrapObservable(valueAccessor());
                $(element).toggle(show && canShow);
            }

            var hideElement = showOrHideElement.bind(null, false);
            var showElement = showOrHideElement.bind(null, true);
            var $hoverTarget = $('#' + ko.utils.unwrapObservable(allBindingsAccessor().hoverTargetId));
            ko.utils.registerEventHandler($hoverTarget, 'mouseover', showElement);
            ko.utils.registerEventHandler($hoverTarget, 'mouseout', hideElement);
            hideElement();
        }
    };
}