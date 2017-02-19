(function () {
    var application = angular.module('Application');

    application.controller('AccountController', AccountController);

    AccountController.$inject = ['$http'];

    function AccountController($http) {
        var vm = this;          //vm stands for view model, standard practice

        vm.Account = [];

        var promise = $http.get('api/user');

        promise.then(function (result) {
            vm.Account = result.data;
        });

        vm.Add = function (user) {
            var copy = angular.copy(user);
            user.name = '';
            user.email = '';
            user.password = '';
            


            var promise = $http.post('api/user', copy);
            promise.then(function (result) {
                //success
                vm.Account.push(result.data);
            }, function (result) {
                //failure
            });

        };

        vm.Remove = function (user) {
            var url = 'api/user/{id}'.replace('{id}', user.id);
            var promise = $http.delete(url);
            promise.then(function (result) {
                var index = vm.Account.indexOf(user);
                vm.Account.splice(index, 1);
            });

        };
    }

})();