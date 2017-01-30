(function () {
    'use strict';

    function FullTextContains(innerText, searchTerm) {
        for (var x = 0; x < searchTerm.length; x++) {
            if (innerText.toLowerCase().indexOf(searchTerm[x].toLowerCase()) >= 0) {
                return true;
            }
        }
        return false;
    };


    function xpTypeaheadFilterString() {
        return function (items, props) {
            var out = [];

            if (angular.isArray(items)) {

                items.forEach(function (item) {

                    var text = props.toLowerCase();

                    for (var key in item) {
                        var itemLoverCase = item.toLowerCase();
                        var substr = itemLoverCase.substr(0, text.length);
                        if (substr === text) {
                            out.push(item);
                        }
                    }

                });
            } else {
                // Let the output be the input untouched
                out = items;
            }
            console.log("out lem", out.length);

            return out;
        };
    }

    function xpTypeaheadFilterJSONObject() {
        return function (items, props) {
            var out = [];

            if (angular.isArray(items)) {

                items.forEach(function (item) {

                    var text = props.toLowerCase();
                    //var searchTerm = text.split(' ');

                    for (var key in item) {
                        if (typeof item[key] == 'string') {
                            var itemLoverCase = item[key].toLowerCase();
                            var searchTerm = itemLoverCase.substr(0, text.length).split(' ');

                            //if (substr === text) {
                            if (FullTextContains(text, searchTerm) == true) {
                                console.log(searchTerm);
                                console.log(text);
                                out.push(item);
                            }
                        }
                    }

                });
            } else {
                // Let the output be the input untouched
                out = items;
            }
            console.log("out lem", out.length);

            return out;
        };
    }

    angular
        .module('rlservice', ['ui.bootstrap'])
    .controller('HomeController', ['$scope', 'ItemsService', 'ActionItemsService', '$http', '$location', function ($scope, ItemsService, ActionItemsService, $http, $location) {
        var _selected;
        $scope.selected = undefined;
        $scope.loaded = false;
        $scope.login = $location.search()["login"];
        $scope.password = $location.search()["password"];


        $scope.Items = undefined;
        $scope.regItem = { DateBuyer: new Date() };
        var today = new Date();
        $scope.start_date = today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate();
        //$scope.start_date = today.getTimezoneOffset();

        ItemsService.getItems().then(function (d) {
            $scope.Items = d.data;
            $scope.loaded = true;
        }, function (response) {
            alert(response.data || 'Request failed');
            $scope.loaded = true;
            //console.log("error occured try again");
        });

        $scope.clearfunction = function () {
            $scope.regItem = {};

        }

        $scope.formatLabel = function (item) {
            return item ? item.Name : '';
        };

        $scope.searchBarCode = function () {
            //alert($scope.regItem.BarCode);
            var data = $.param({
                barcode: $scope.regItem.BarCode
            });

            var config = {
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded;charset=utf-8;'
                }
            };

            $http.post('/rlservice/Home/GetItemsBarCode', data, config)
            .then(function (response) {
                //$scope.status = response.status;
                //$scope.data = response.data;
                if (Array.isArray(response.data)) {
                    //$scope.Items = response.data;
                    $scope.regItem.item = response.data[0];
                }
            }, function (response) {
                alert(response.data || 'Request failed');
                //$scope.data = response.data || 'Request failed';
                $scope.status = response.status;
            });
        };

        $scope.searchIMEI = function () {
            //alert($scope.regItem.BarCode);
            var data = $.param({
                serial: $scope.regItem.InputIMEI1
            });

            var config = {
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded;charset=utf-8;'
                }
            };

            $http.post('/rlservice/Home/GetItemsSerial', data, config)
            .then(function (response) {
                //$scope.status = response.status;
                //$scope.data = response.data;
                if (Array.isArray(response.data)) {
                    //$scope.Items = response.data;
                    //$scope.regItem = angular.copy(response.data[0]);
                    angular.merge($scope.regItem, response.data[0]);
                }
            }, function (response) {
                alert(response.data || 'Request failed');
                //$scope.data = response.data || 'Request failed';
                $scope.status = response.status;
            });
        };
        $scope.ActionItems = [];

        ActionItemsService.getActionItems().then(function (d) {
            $scope.ActionItems = d.data;
        }, function () {
            console.log("getActionItems error occured try again");
        });


        $scope.SendData = function () {
            // use $.param jQuery function to serialize data from JSON 

            var config = {
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                }
            };

            $http.post('/rlservice/Home/postData', $scope.regItem, config)
            .success(function (data, status, headers, config) {
                $scope.PostDataResponse = data;
            })
            .error(function (data, status, header, config) {
                $scope.ResponseDetails = "Data: " + data +
                    "<hr />status: " + status +
                    "<hr />headers: " + header +
                    "<hr />config: " + config;
            });
        };




    }])
    .controller('ContactController', ['$scope', 'ManagerService', function ($scope, ManagerService) {
        $scope.loaded = false;
        $scope.Manager = {};
        ManagerService.getManager().then(function (d) {
            $scope.Manager = d.data;
            $scope.loaded = true;
        }, function () {
            $scope.loaded = true;
            console.log("error occured try again");
        });
    }])

    .factory("ItemsService", function ($http) {
        var fact = {};
        fact.getItems = function () {
            return $http.get('/rlservice/Home/GetItems');
        };
        return fact;
    })
    .factory("ManagerService", function ($http) {
        var fact = {};
        fact.getManager = function () {
            return $http.get('/rlservice/Home/GetManager');
        };
        return fact;
    })
    .factory("ActionItemsService", function ($http) {
        var fact = {};
        fact.getActionItems = function () {
            return $http.get('/rlservice/Home/GetActionItems');
        };
        return fact;
    })
    .filter('xpTypeaheadFilterJSONObject', xpTypeaheadFilterJSONObject)
    .filter('search', function ($filter) {
        return function (items, text) {
            if (!text || text.length === 0)
                return items;

            // split search text on space
            var searchTerms = text.split(' ');

            // search for single terms.
            // this reduces the item list step by step
            searchTerms.forEach(function (term) {
                if (term && term.length)
                    items = $filter('filter')(items, term);
            });

            return items
        };
    })
    .config(function ($locationProvider) {
        $locationProvider.html5Mode({
            enabled: true,
            requireBase: false
        });
    });

})();