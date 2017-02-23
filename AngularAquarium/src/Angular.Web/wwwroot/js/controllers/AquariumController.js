(function () {
    var application = angular.module('Application');

    application.controller('AquariumController', AquariumController);

    AquariumController.$inject = ['$http'];

    function AquariumController($http) {
        var vm = this;          //vm stands for view model, standard practice

        vm.Aquarium = [];

        activate();

        function activate() {
            var promise = $http.get('/api/fishes');
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


            var promise = $http.post('/api/fishes', copy);
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

        //vm.Clear = function (fish) {
        //    var url = '/api/fishes/{id}'.relace('{id}', fish.id);
        //    var promise = $
        //}
    }

})();