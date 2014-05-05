mainApp.service('doctorservice', function ($http, $q) {

    var deferred = $q.defer();
    $http.get('http://datatank.gent.be/Gezondheid/Huisartsen.json').success(function (data) {
        deferred.resolve(data);
    });
    this.get = function () {
      

        return deferred.promise;
    }

});