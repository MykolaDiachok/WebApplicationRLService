(function () {
    'use strict';
    angular
        .module('rlbalance', ['ui.bootstrap'])
    .controller('BalanceController', ['$scope', 'BalanceService', function ($scope, BalanceService) {
        $scope.loaded = false;
        $scope.Balance = {};
        $scope.dataTomorrow = new Date(new Date().getTime() + 24 * 60 * 60 * 1000);;
        $scope.dataAfterTomorrow = new Date(new Date().getTime() + 2*24 * 60 * 60 * 1000);;
        BalanceService.getBalance().then(function (d) {
            $scope.Balance = d.data;
            $scope.loaded = true;
        }, function () {
            $scope.loaded = true;
            console.log("Balance error occured try again");
        });
    }])
        .factory("BalanceService", function ($http) {
            var fact = {};
            fact.getBalance = function () {
                return $http.get('/rlbalance/Home/GetBalance');
            };
            return fact;
        });
})();