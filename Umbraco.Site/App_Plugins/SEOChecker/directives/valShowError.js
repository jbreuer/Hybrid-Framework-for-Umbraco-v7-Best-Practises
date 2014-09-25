function valShowerror() {
    return {
        restrict: "A",
        link: function (scope, element, attrs, ctrl) {

            scope.$watch(function () {
                return scope.$eval(attrs.valShowerror);
            }, function (newVal, oldVal) {
                if (newVal === true) {
                    element.addClass("highlight-error");
                } else {
                    element.removeClass("highlight-error");
                }
            });

        }
    };
}
angular.module('umbraco.directives').directive("valShowerror", valShowerror);