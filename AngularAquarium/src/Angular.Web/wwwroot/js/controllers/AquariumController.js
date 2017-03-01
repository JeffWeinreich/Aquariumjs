(function () {
    var application = angular.module('Application');

    application.controller('AquariumController', AquariumController);

    AquariumController.$inject = ['$http'];

    function AquariumController($http) {
        var vm = this;          //vm stands for view model, standard practice

        vm.Fishes = [];
        vm.tank = '';


        vm.GetInfo = function (tankId) {
            var promise = $http.get('/api/tanks/' + tankId);
            promise.then(function (result) {
                console.log(result);
                vm.tank = (result.data);
                vm.GetFishes();
            }, function (result) {
                console.log(result);
            });
        }

        vm.GetFishes = function() {
            var promise = $http.get('/api/tanks/' + vm.tank.id + '/fishes');
            promise.then(function (result) {
                vm.Fishes= result.data;
            });
        };

        vm.Add = function (fish) {
            var copy = angular.copy(fish);
            fish.name = '';
            fish.type = '';
            fish.quantity = '';
            fish.description = '';
            


            var promise = $http.post('/api/tanks/' + vm.tank + '/fishes', copy);              //used  to be just '/api/fishes'
            promise.then(function (result) {
                //success
                vm.Fishes.push(result.data);
            }, function (result) {
                //failure
            });

        };

        vm.Remove = function (fish) {
            var url = '/api/fishes/{id}'.replace('{id}', fish.id);
            var promise = $http.delete(url);
            promise.then(function (result) {
                var index = vm.Fishes.indexOf(fish);
                vm.Fishes.splice(index, 1);
            });

        };

        vm.Clear = function (fish) {
           // var url = '/api/fishes/{id}'.replace('{id}', fish.id);
            var promise = $http.clear('/api/fishes');
            promise.then(function (result) {
                var index = vm.Fishes.indexOf(fish);
                vm.Fishes.splice(index, 1);
            });
        };
    }

})();