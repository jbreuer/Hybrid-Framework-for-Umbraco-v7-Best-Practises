app.requires.push('ngTable');

angular.module("umbraco").controller("DiploTraceLogEditController",
    function ($scope, $http, $routeParams, $route, $filter, $q, dialogService, ngTableParams) {

        console.log("Loaded DiploTraceLogEditController...");
        $scope.id = $routeParams.id;
        $scope.feedback = {};
        $scope.feedback.message = "Loading...";

        var dataUrl = '/Umbraco/Backoffice/TraceLogViewer/TraceLog/GetLogData?logfileName=' + $routeParams.id;
        var data;

        // Ajax request to controller for data-
        $http.get(dataUrl).success(function (data) {

            $scope.tableParams = new ngTableParams({
                page: 1,            // show first page
                count: 100,          // count per page
                sorting: {
                    Date: 'desc'     // initial sorting
                },
                filter: {
                    Message: ''       // initial filter
                }
            }, {
                total: data.length,
                getData: function ($defer, params) {

                    var filteredData = params.filter() ?
                            $filter('filter')(data, params.filter()) :
                            data;

                    var orderedData = params.sorting() ?
                            $filter('orderBy')(filteredData, params.orderBy()) :
                            data;

                    params.total(orderedData.length);

                    $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));

                    if (orderedData.length > 0) {
                        $scope.feedback.message = '';
                    } else {
                        $scope.feedback.message = 'No log enteries found matching your criteria';
                    }

                }
            })

        }).error(function (data, status, headers, config) {
            $scope.feedback.message = "Error retrieving log data for " + $routeParams.id + " :\n" + data.ExceptionMessage;
        });

        // Open detail modal
        $scope.openDetail = function (logItem) {

            var dialog = dialogService.open({
                template: '/App_Plugins/DiploTraceLogViewer/backoffice/diplotracelog/detail.html',
                dialogData: logItem, show: true, width: 800
            });
        }

        // Reload page
        $scope.reload = function () {
            $route.reload();
        }

    });