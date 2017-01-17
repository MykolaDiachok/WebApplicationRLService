﻿(function () {
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
    .controller('HomeController', ['$scope', 'ItemsService', 'ManagerService', function ($scope, ItemsService, ManagerService) {
        var _selected;
        $scope.selected = undefined;

        $scope.Items = undefined;
        $scope.regItem = { DateBuyer: new Date() };
        var today = new Date();
        $scope.start_date = today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate();
        //$scope.start_date = today.getTimezoneOffset();

        ItemsService.getItems().then(function (d) {
            $scope.Items = d.data;
        }, function () {
            console.log("error occured try again");
        });

  
        $scope.formatLabel = function (item) {
            return item ? item.Name : '';
        };

    }])
    .controller('ContactController', ['$scope', 'ManagerService', function ($scope, ManagerService) {
        $scope.Manager = null;
        ManagerService.getManager().then(function (d) {
            $scope.Manager = d.data;
        }, function () { console.log("error occured try again"); });
    }])

    .factory("ItemsService", function ($http) {
        var fact = {};
        fact.getItems = function () {
            return $http.get('/Home/GetItems');
        };
        return fact;
    })
    .factory("ManagerService", function ($http) {
        var fact = {};
        fact.getManager = function () {
            return $http.get('/Home/GetManager');
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
    });

})();