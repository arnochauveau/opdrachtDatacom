

mainApp.controller('doctorcontroller', function ($scope, doctorservice) {

    var doctors

   
    var tmp = doctorservice.get().then(function (data) {
        $scope.doctors = data.Huisartsen;
        console.log(data.Huisartsen)
    });
            
    
    
    

});
