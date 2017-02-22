(function () {
    var application = angular.module('Application');

    application.controller('AquariumController', AquariumController);

    AquariumController.$inject = ['$http'];

    function AquariumController($http) {
        var vm = this;          //vm stands for view model, standard practice

        vm.Aquarium = [];

        var promise = $http.get('/api/fishes');

        promise.then(function (result) {
            vm.Aquarium = result.data;
        });

        vm.Add = function (fish) {
            var copy = angular.copy(fish);
            fish.name = '';
            fish.type = '';
            fish.quantity = '';
            fish.description = '';
            

            var promise = $http.post('/api/fishes', copy);
            promise.then(function (result) {
                //success
                vm.Aquarium.push(result.data);
            }, function (result) {
                //failure
            });

        };

        vm.Remove = function (fish) {
            var url = '/api/fishes/{id}'.replace('{id}', fish.id);
            var promise = $http.delete(url);
            promise.then(function (result) {
                var index = vm.Aquarium.indexOf(fish);
                vm.Aquarium.splice(index, 1);
            });

        };

        //vm.Clear = function (fish) {
        //    var url = '/api/fishes/{id}'.relace('{id}', fish.id);
        //    var promise = $
        //}
    }

})();