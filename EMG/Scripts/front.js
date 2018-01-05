var app = angular.module('frontApp', ['frontApp']);
app.controller('frontController', ['$scope', function($scope) {
    var nowSlide = 1;
    $scope.slideStatus = { 'slide1': 'active', 'slide2': 'right', 'slide3': 'right', 'slide4': 'right' };
    $scope.prevSlide = function() {
        if (nowSlide != 1) {
            $scope.slideStatus['slide'+nowSlide] = 'right';
            nowSlide--;
            $scope.slideStatus['slide'+nowSlide] = 'active';
        }
    }
    $scope.nextSlide = function() {
    	console.log(nowSlide);
        if (nowSlide != 4) {
            $scope.slideStatus['slide'+nowSlide] = 'left';
            nowSlide++;
            $scope.slideStatus['slide'+nowSlide] = 'active';
        }
        console.log($scope.slideStatus)
    }
}]);
