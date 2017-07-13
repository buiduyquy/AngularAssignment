
function redditController($scope, $http) {  
    $scope.loading = true;
    $scope.addMode = false;

    //Used to display the data
    $http.get('/api/Reddit').success(function (data) {        
        $scope.friends = data;
        $scope.loading = false;
    })
    .error(function () {
        $scope.error = "An Error has occured while loading posts!";
        $scope.loading = false;
        });



    function loadEvents(isApprove) {
        //Used to display the data
        $http.get('/api/Search?isApprove=' + isApprove).success(function (data) {
                $scope.friends = data;
                $scope.loading = false;
            })
            .error(function () {
                $scope.error = "An Error has occured while loading posts!";
                $scope.loading = false;
            });
    };

    $scope.filters = {
        includeApproved: false
    };

    $scope.$watch('$scope.filters.includeApproved', function (newValue, oldValue) {

        if (newValue) {
            loadEvents(true);
        } else {
            //Used to display the data
            $http.get('/api/Reddit').success(function (data) {
                    $scope.friends = data;
                    $scope.loading = false;
                })
                .error(function () {
                    $scope.error = "An Error has occured while loading posts!";
                    $scope.loading = false;
                });
        }

    });

  
    $scope.toggleEdit = function () {
        this.friend.editMode = !this.friend.editMode;
    };
    $scope.toggleAdd = function () {
        $scope.addMode = !$scope.addMode;
    };

    //Used to save a record after edit
    $scope.Approve = function () {
        //alert("Approved Successfully!!");
        this.friend.isApprove = true;
        $scope.loading = true;
        var frien = this.friend;
        $http.post('/api/Reddit/', frien).success(function (data) {
            alert("Approved Successfully!!");
            //this.friend.isApprove = true;
            $scope.loading = false;
        }).error(function (data) {
            $scope.error = "An Error has occured while Saving Friend! " + data;
            $scope.loading = false;

            });



    };

    $scope.DisApprove = function () {
        //alert("DisApproved Successfully!!");
        this.friend.isApprove = false;
        $scope.loading = true;
        var frien = this.friend;
        $http.post('/api/Reddit/', frien).success(function (data) {
            alert("DisApproved Successfully!!");
            //this.friend.isApprove = false;
            $scope.loading = false;
        }).error(function (data) {
            $scope.error = "An Error has occured while Saving Friend! " + data;
            $scope.loading = false;

        });
    };


}