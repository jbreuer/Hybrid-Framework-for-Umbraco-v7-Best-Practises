angular.module("umbraco")
	.controller("seoChecker.redirectsController",
		function ($scope, $routeParams, $http, dialogService, notificationsService) {
			var nodeId = $routeParams.id;
		  
			$scope.bindData = function () {
				$http.get('/umbraco/backoffice/SEOChecker/SEOCheckerApi/GetRedirects?id=' + nodeId).then(function (res) {
					$scope.redirects = res.data;
				});
			};

			$scope.deleteRedirect = function (redirectId) {
				if (confirm($scope.model.config.overviewDeleteConfirm) == true) {
					$http.get('/umbraco/backoffice/SEOChecker/SEOCheckerApi/DeleteRedirect?id=' + redirectId).then(function () {
						$scope.bindData();
						notificationsService.success('Redirect deleted', 'The redirect is removed from the database');
					});
				}
			};
			
			$scope.setEditMode = function (redirectId) {
				$scope.validationResult = null;
				$http.get('/umbraco/backoffice/SEOChecker/SEOCheckerApi/GetEmptyModel?id=' + nodeId).then(function (res) {
					//Default set to empty value
					$scope.model.value = res.data;
					for (var i = 0; i < $scope.redirects.length; i++) {
						if ($scope.redirects[i].notFoundId == redirectId) {
							//Redirect found to edit use this one
							$scope.model.value = $scope.redirects[i];
						}
					}
					$scope.nodePicked = { id: $scope.model.value.documentID, name: $scope.model.value.documentName };
					
				});
				$scope.editMode = true;
			};

			$scope.cancelEditMode = function () {
				$scope.editMode = false;
			};
			
			$scope.submitRedirect = function () {
				$scope.model.value.documentID = $scope.getPickedNodeId();
				$http.post('/umbraco/backoffice/SEOChecker/SEOCheckerApi/SaveRedirect', $scope.model.value).then(function (res) {
					$scope.validationResult = res.data;
					if (res.data.valid == true) {
						//All is valid
						$scope.bindData();
						$scope.editMode = false;
						notificationsService.success('Redirect saved', 'The redirect is saved in the database');
					}
				});
				scope.$digest();
			};

			$scope.pickContentNode = function () {

				//When button clicked - Let's open the content picker dialog on the right
				dialogService.contentPicker({ callback: itemPicked });

				//When the node has been picked - do this...
				function itemPicked(pickedItem) {

					//Set the picked item to our scope
					$scope.nodePicked = pickedItem;
				}
			};
			
			$scope.getPickedNodeId = function () {
				if ($scope.nodePicked == null) {
					return 0;
				} else {
					return $scope.nodePicked.id;
				}
			};
			
			$scope.deletePickedNode = function () {
				$scope.nodePicked = null;
			};

			//Initialize
			$scope.bindData();
		});