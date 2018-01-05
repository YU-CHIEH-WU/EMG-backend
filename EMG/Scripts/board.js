$(function () { 
    $('.clickable').on('mouseenter', function () {
        $('.block-inner').not(this).addClass('opacity');
        $('.block-alert').addClass('opacity');
    })
    $('.clickable').on('mouseleave', function () {
        $('.opacity').removeClass('opacity');
    })
    //分享圖表至Facebook
    $('.btn-saveSVG').on('click', function () {
        var chartName = $(this).data('chartname');
        var chartOption = getChartOption(chartName);
        var data = {
            options: JSON.stringify(chartOption),
            filename: 'chart',
            type: 'image/jpeg',
            width: 1200,
            async: true
        };
        var exportUrl = 'http://export.highcharts.com/';
        var url = '';
        $.post(exportUrl, data, function (data) {
            url = exportUrl + data;
            console.log(url);
            window.fbAsyncInit = function () {
                FB.init({
                    appId: '472853519550243',
                    status: true,
                    cookie: true,
                    xfbml: true
                });
            };
            var e = document.createElement('script');
            e.async = true;
            e.src = '//connect.facebook.net/zh-TW/all.js';
            document.getElementById('fb-root').appendChild(e);
            FB.ui({
                method: 'feed',
                name: 'FIT ME 肌不可失',
                link: 'http://163.17.136.197:8080/EMG',
                picture: url,
                caption: 'This article demonstrates how to use the jQuery dialog feature in ASP.Net.',
                description: 'FIT ME 肌不可失,您個人專屬的健身教練',
                message: 'hello raj message'
            });
        });       
    });
})
var app = angular.module('mainApp', ['mainApp']);
app.controller('blockController', ['$scope','$window', '$sce', '$http', '$timeout', '$location', '$anchorScroll',
    function ($scope, $window, $sce, $http, $timeout, $location, $anchorScroll) {
        $scope.userAccount = $window.account;
        var apiUrl = 'http://163.17.136.197:8080/EMG/Api/';
        // 首頁
        // true時隱藏首頁block
        $scope.isDetailActive = false;
        // true時顯示對應detail
        $scope.detail1Active = false;
        $scope.detail2Active = false;
        $scope.detail3Active = false;
        $scope.deatil3Options = { 'title': '', 'src': '' };
        $scope.detail4Active = false;
        $scope.detail4Options = { 'title': '', 'startDay': '' };
        $scope.detail5Active = false;
        $scope.detail6Active = false;
        // 以動畫差隱藏首頁block
        $scope.isOntop = false;
        this.prevType = '';
        // 顯示detail1BLock
        $scope.showDetail1 = function () {
            title = '訓練成效詳細資訊';
            chart1 = 'result';
            chart2 = 'fatigue';
            chart3 = 'pmvc';
            $scope.chartPosture = {
                'pos1': '伏地挺身',
                'pos2': '啞鈴單手後曲伸',
                'pos3': '啞鈴跨步蹲舉',
                'pos4': '立姿側平舉',
                'pos5': '仰臥腿上舉',
                'pos6': 'V型仰臥起坐'
            };
            // 設定detail1圖表
            var chart1Option = getChartOption(chart1);
            var chart2Option = getChartOption(chart2);
            var chart3Option = getChartOption(chart3);
            setChart('detail1-main-chart', chart1Option);
            setChart('detail1-left-chart', chart2Option);
            setChart('detail1-right-chart', chart3Option);
            $scope.detail1Active = true;
            $scope.isDetailActive = true;
        };
        $scope.showDetail2 = function () {
            title = '肌肉成長詳細資訊';
            chart1 = 'growPart';
            chart2 = '1rm';
            chart3 = '15rm';
            // 設定detail2圖表
            var chart1Option = getChartOption(chart1);
            var chart2Option = getChartOption(chart2);
            var chart3Option = getChartOption(chart3);
            setChart('detail2-main-chart', chart1Option);
            setChart('detail2-left-chart', chart2Option);
            setChart('detail2-right-chart', chart3Option);
            $scope.detail2Active = true;
            $scope.isDetailActive = true;

        }
        // 切換公斤與百分比圖表
        $scope.switchType = function (type) { };
        // 顯示detail3Block
        $scope.showDetail3 = function () {
            var photoApi = apiUrl + 'AlbumApi/GetPersonPhoto';
            $http.post(photoApi, { 'account': $scope.loginUser.account }).then(function (response) {
                $scope.photoList = [];
                angular.forEach(response.data, function (value, key) {
                    $scope.photoList.push(value);
                })
                $scope.detail3Active = true;
                $scope.isDetailActive = true;
                $scope.isphotoUpload = false;
            })
        };
        $scope.showPhoto = function (photo) {
            $scope.detailPhoto = photo;
            $scope.isPhotoClick = true;
            $timeout(function () {
                $scope.isPhotoShow = true;
            }, 250);
        }
        $scope.backPhotoList = function () {
            $scope.isPhotoShow = false;
            $timeout(function () {
                $scope.isPhotoClick = false;
            }, 250);
        };
        $scope.uploadPhoto = function () {
            $scope.isphotoUpload = true;
            var uploadPhotoApi = 'http://localhost:55546/Api/AlbumApi/UploadPhoto';
            var data = document.getElementById('photo-upload').files[0];
            console.log(data);
            $http.post(uploadPhotoApi, data).then(function (response) {
                $scope.showDetail3();
            })

        };
        $scope.deletePhoto = function (photo) {
            console.log(photo);
            var deletePhotoApi = apiUrl + 'AlbumApi/DeletePhoto';
            var data = { 'account': $scope.loginUser.account, 'p_Id': photo.P_Id };
            $http.post(deletePhotoApi, data).then(function (response) {
                $scope.showDetail3();
            })
        }
        $scope.showDetail4 = function (nowDate) {
            var title = '課程行事曆';
            var date = new Date(nowDate);
            var month = date.getMonth() + 1;
            var first = new Date('2016-' + month + '-01');
            var newDay = new Date(first);
            var weekday = first.getDay();
            if (weekday == 0) {
                weekday = 7;
            }
            $scope.calendar = [{ 'weekday': 1, 'weekdayTW': '一', 'day1': '', 'day1Posture': [], day1Thumb: 'images/null.png', 'day1Complete': false, 'day2': '', 'day2Posture': [], 'day2Thumb': 'images/null.png', 'day2Complete': false, 'day3': '', 'day3Posture': [], day3Thumb: 'images/null.png', 'day3Complete': false, 'day4': '', 'day4Posture': [], day4Thumb: 'images/null.png', 'day4Complete': false, 'day5': '', 'day5Posture': [], day5Thumb: 'images/null.png', 'day5Complete': false },
                { 'weekday': 2, 'weekdayTW': '二', 'day1': '', 'day1Posture': [], day1Thumb: 'images/null.png', 'day1Complete': false, 'day2': '', 'day2Posture': [], 'day2Thumb': 'images/null.png', 'day2Complete': false, 'day3': '', 'day3Posture': [], day3Thumb: 'images/null.png', 'day3Complete': false, 'day4': '', 'day4Posture': [], day4Thumb: 'images/null.png', 'day4Complete': false, 'day5': '', 'day5Posture': [], day5Thumb: 'images/null.png', 'day5Complete': false },
                { 'weekday': 3, 'weekdayTW': '三', 'day1': '', 'day1Posture': [], day1Thumb: 'images/null.png', 'day1Complete': false, 'day2': '', 'day2Posture': [], 'day2Thumb': 'images/null.png', 'day2Complete': false, 'day3': '', 'day3Posture': [], day3Thumb: 'images/null.png', 'day3Complete': false, 'day4': '', 'day4Posture': [], day4Thumb: 'images/null.png', 'day4Complete': false, 'day5': '', 'day5Posture': [], day5Thumb: 'images/null.png', 'day5Complete': false },
                { 'weekday': 4, 'weekdayTW': '四', 'day1': '', 'day1Posture': [], day1Thumb: 'images/null.png', 'day1Complete': false, 'day2': '', 'day2Posture': [], 'day2Thumb': 'images/null.png', 'day2Complete': false, 'day3': '', 'day3Posture': [], day3Thumb: 'images/null.png', 'day3Complete': false, 'day4': '', 'day4Posture': [], day4Thumb: 'images/null.png', 'day4Complete': false, 'day5': '', 'day5Posture': [], day5Thumb: 'images/null.png', 'day5Complete': false },
                { 'weekday': 5, 'weekdayTW': '五', 'day1': '', 'day1Posture': [], day1Thumb: 'images/null.png', 'day1Complete': false, 'day2': '', 'day2Posture': [], 'day2Thumb': 'images/null.png', 'day2Complete': false, 'day3': '', 'day3Posture': [], day3Thumb: 'images/null.png', 'day3Complete': false, 'day4': '', 'day4Posture': [], day4Thumb: 'images/null.png', 'day4Complete': false, 'day5': '', 'day5Posture': [], day5Thumb: 'images/null.png', 'day5Complete': false },
                { 'weekday': 6, 'weekdayTW': '六', 'day1': '', 'day1Posture': [], day1Thumb: 'images/null.png', 'day1Complete': false, 'day2': '', 'day2Posture': [], 'day2Thumb': 'images/null.png', 'day2Complete': false, 'day3': '', 'day3Posture': [], day3Thumb: 'images/null.png', 'day3Complete': false, 'day4': '', 'day4Posture': [], day4Thumb: 'images/null.png', 'day4Complete': false, 'day5': '', 'day5Posture': [], day5Thumb: 'images/null.png', 'day5Complete': false },
                { 'weekday': 7, 'weekdayTW': '日', 'day1': '', 'day1Posture': [], day1Thumb: 'images/null.png', 'day1Complete': false, 'day2': '', 'day2Posture': [], 'day2Thumb': 'images/null.png', 'day2Complete': false, 'day3': '', 'day3Posture': [], day3Thumb: 'images/null.png', 'day3Complete': false, 'day4': '', 'day4Posture': [], day4Thumb: 'images/null.png', 'day4Complete': false, 'day5': '', 'day5Posture': [], day5Thumb: 'images/null.png', 'day5Complete': false }
            ];
            var weekdayCount = 0;
            var firstFind = false;
            for (i = 1; i <= 5; i++) {
                angular.forEach($scope.calendar, function (value, key) {
                    if (firstFind) {
                        var result = new Date(newDay);
                        result.setDate(newDay.getDate() + 1);
                        newDay = result;
                        if (result.getMonth() + 1 == month) {
                            if (i == 1) {
                                $scope.calendar[key].day1 = newDay;
                            }
                            if (i == 2) {
                                $scope.calendar[key].day2 = newDay;
                            }
                            if (i == 3) {
                                $scope.calendar[key].day3 = newDay;
                            }
                            if (i == 4) {
                                $scope.calendar[key].day4 = newDay;
                            }
                            if (i == 5) {
                                $scope.calendar[key].day5 = newDay;
                            }
                        }
                    }
                    if ($scope.calendar[key].weekday == weekday) {
                        $scope.calendar[key].day1 = first;
                        firstFind = true;
                    }
                })
            }
            angular.forEach($scope.courseList, function (value, key) {
                var today = new Date().getTime();
                var date = new Date($scope.courseList[key].Date);
                var posture = $scope.courseList[key].P_Name;
                var thumb = $scope.courseList[key].thumb;
                angular.forEach($scope.calendar, function (value, key) {
                    var dateTime = date.getTime();
                    if (value.day1 != "") {
                        var day1Time = value.day1.getTime() + 28800000;
                    }
                    var day2Time = value.day2.getTime() + 28800000;
                    var day3Time = value.day3.getTime() + 28800000;
                    var day4Time = value.day4.getTime() + 28800000;
                    if (value.day5 != "") {
                        var day5Time = value.day5.getTime() + 28800000;
                    }
                    console.log(date, value.day3, dateTime, day3Time);
                    if (day1Time == dateTime) {
                        $scope.calendar[key].day1Posture.push({ 'thumb': thumb, 'posture': posture });
                        $scope.calendar[key].day1Thumb = 'images/gym.png';
                        if (today > dateTime) {
                            $scope.calendar[key].day1Complete = true;
                        }
                    }
                    if (day2Time == dateTime) {
                        $scope.calendar[key].day2Posture.push({ 'thumb': thumb, 'posture': posture });
                        $scope.calendar[key].day2Thumb = 'images/gym.png';
                        if (today > dateTime) {
                            $scope.calendar[key].day2Complete = true;
                        }
                    }
                    if (day3Time == dateTime) {
                        $scope.calendar[key].day3Posture.push({ 'thumb': thumb, 'posture': posture });
                        $scope.calendar[key].day3Thumb = 'images/gym.png';
                        if (today > dateTime) {
                            $scope.calendar[key].day3Complete = true;
                        }
                    }
                    if (day4Time == dateTime) {
                        $scope.calendar[key].day4Posture.push({ 'thumb': thumb, 'posture': posture });
                        $scope.calendar[key].day4Thumb = 'images/gym.png';
                        if (today > dateTime) {
                            $scope.calendar[key].day4Complete = true;
                        }
                    }
                    if (day5Time == dateTime) {
                        console.log(posture);
                        $scope.calendar[key].day5Posture.push({ 'thumb': thumb, 'posture': posture });
                        $scope.calendar[key].day5Thumb = 'images/gym.png';
                        if (today > dateTime) {
                            $scope.calendar[key].day5Complete = true;
                        }
                    }
                })
            })
            $scope.detail4Options.title = title;
            $scope.detail4Options.startDay = weekday;
            $scope.detail4Active = true;
            $scope.isDetailActive = true;
        };
        $scope.showDetail5 = function () {
            var getMessageApi = apiUrl + 'MessageApi/GetAllByAccount';
            var data = { 'account': $scope.userAccount };
            $scope.messageList = [];
            $http.post(getMessageApi, data).then(function (response) {
                console.log(response);
                angular.forEach(response.data, function (value, key) {
                    $scope.messageList.push({ 'MId': value.MId, 'title': value.Title, 'content': value.Messages })
                })
            })
            $scope.isDetailActive = true;
            $scope.detail5Active = true;
            $scope.edit = { 'MId': 0, 'title': '', 'content': '', 'holder': '請點選新增按鈕或列表中的標題' };
        }
        $scope.newMessage = function () {
            $scope.edit = { 'MId': 0, 'title': '', 'content': '', 'holder': '請新增內文' }
            $scope.isCreateMessage = true;
        }
        $scope.createMessage = function () {
            var createMessageApi = apiUrl + 'MessageApi/Create';
            var data = {
                'Title': $scope.edit.title,
                'Messages': $scope.edit.content,
                'Account': $scope.loginUser.account,
                'UserName': $scope.loginUser.name,
                'Photo': $scope.loginUser.photo
            };
            $http.post(createMessageApi, data).then(function (response) {
                alert('done!');
                $scope.isCreateMessage = false;
                $scope.showDetail5();
            })
        }
        $scope.editMessage = function (message) {
            $scope.isCreateMessage = false;
            $scope.edit = { 'MId': message.MId, 'title': message.title, 'content': message.content }
        }
        $scope.submitMessage = function () {
            var editMessageApi = apiUrl + 'MessageApi/Edit';
            var data = { 'MId': $scope.edit.MId, 'Title': $scope.edit.title, 'Messages': $scope.edit.content };
            $http.post(editMessageApi, data).then(function (response) {
                alert('done!');
                $scope.showDetail5();
            })
        }
        $scope.deleteMessage = function () {
            var deleteMessageApi = apiUrl + 'MessageApi/Delete';
            var data = { 'MId': $scope.edit.MId };
            $http.post(deleteMessageApi, data).then(function (response) {
                alert('done!');
                $scope.showDetail5();
            })
        }
        $scope.showDetail6 = function () {
            $scope.detail6Active = true;
            $scope.isDetailActive = true;
        }
        $scope.showCalendarPosture = function (day, posture, complete) {
            angular.forEach($scope.calendar, function (value, key) {
                if (value.day1Thumb == 'http://163.17.136.197:8080/EMG/images/gym-c.png') {
                    $scope.calendar[key].day1Thumb = 'http://163.17.136.197:8080/EMG/images/gym.png';
                }
                if (value.day2Thumb == 'http://163.17.136.197:8080/EMG/images/gym-c.png') {
                    $scope.calendar[key].day2Thumb = 'http://163.17.136.197:8080/EMG/images/gym.png';
                }
                if (value.day3Thumb == 'http://163.17.136.197:8080/EMG/images/gym-c.png') {
                    $scope.calendar[key].day3Thumb = 'http://163.17.136.197:8080/EMG/images/gym.png';
                }
                if (value.day4Thumb == 'http://163.17.136.197:8080/EMG/images/gym-c.png') {
                    $scope.calendar[key].day4Thumb = 'http://163.17.136.197:8080/EMG/images/gym.png';
                }
                if (value.day5Thumb == 'http://163.17.136.197:8080/EMG/images/gym-c.png') {
                    $scope.calendar[key].day5Thumb = 'http://163.17.136.197:8080/EMG/images/gym.png';
                }
                if (value.day1 == day && posture[0] != undefined) {
                    $scope.calendar[key].day1Thumb = 'http://163.17.136.197:8080/EMG/images/gym-c.png';
                }
                if (value.day2 == day && posture[0] != undefined) {
                    $scope.calendar[key].day2Thumb = 'http://163.17.136.197:8080/EMG/images/gym-c.png';
                }
                if (value.day3 == day && posture[0] != undefined) {
                    $scope.calendar[key].day3Thumb = 'http://163.17.136.197:8080/EMG/images/gym-c.png';
                }
                if (value.day4 == day && posture[0] != undefined) {
                    $scope.calendar[key].day4Thumb = 'http://163.17.136.197:8080/EMG/images/gym-c.png';
                }
                if (value.day5 == day && posture[0] != undefined) {
                    $scope.calendar[key].day5Thumb = 'http://163.17.136.197:8080/EMG/images/gym-c.png';
                }
            })
            $scope.postureDate = day;
            if (posture[0] == undefined) {
                $scope.postureTitle = '本日沒有課程';
                $scope.postureList = [];
            } else {
                if (complete) {
                    $scope.postureTitle = '本日課程已結束';
                } else {
                    $scope.postureTitle = '本日課程';
                }
                $scope.postureList = posture;
            }
        }
        $scope.isPostureListShow = true;
        $scope.isPostureDetailShow = false;
        $scope.showPosture = function (pos) {
            var posDataApi = apiUrl + 'CourseApi/GetPosData';
            var data = { 'name': pos };
            $http.post(posDataApi, data).then(function (response) {
                $scope.posture = { 'name': response.data.Name, 'src': $sce.trustAsResourceUrl(response.data.Iframe) };
            });
            $scope.isPostureDetailShow = true;
            $scope.isPostureListShow = false;
        };
        $scope.showPostureList = function () {
            $scope.isPostureDetailShow = false;
            $scope.isPostureListShow = true;
        };
        // 防止動畫重複觸發
        $scope.setOntop = function () {
            if ($scope.isDetailActive && !$scope.isOntop) {
                $scope.isOntop = true;
            };
        };
        // 顯示首頁
        $scope.showIndex = function () {
            $scope.detail1Active = false;
            $scope.detail2Active = false;
            $scope.detail3Active = false;
            $scope.detail4Active = false;
            $scope.detail5Active = false;
            $scope.detail6Active = false;
            $scope.isOntop = false;
            $timeout(function () { $scope.isDetailActive = false }, 50);
        };

        // 會員
        // 延遲載入使用者帳號並取得使用者資料 
        $timeout(function () {
            $scope.loginUser = {
                'account': $scope.userAccount,
                'name': '',
                'height': '',
                'weight': '',
                'bodyfat': '',
                'bmr': '',
                'disease': '',
                'age': '',
                'sex': '',
                'sports': '',
                'place': ''

            }
            var profileApi = apiUrl + 'UserApi/getUser';
            var apiData = { 'account': $scope.userAccount };
            $http.post(profileApi, apiData).then(function (response) {
                console.log(response.data);
                $scope.loginUser = {
                    'account': response.data.Account,
                    'name': response.data.Name,
                    'photo': response.data.Url,
                    'height': response.data.Height,
                    'weight': response.data.Weight,
                    'bodyfat': response.data.Bodyfat,
                    'bmr': response.data.BMR,
                    'disease': response.data.Disease,
                    'age': response.data.Age,
                    'sex': response.data.Sex
                }
                if ($scope.loginUser.account == "administrator") {
                    $scope.isAdmin = true;
                } else {
                    $scope.isAdmin = false;
                }
                var apiData = { 'account': $scope.userAccount };
                var ageApi = apiUrl + 'CourseApi/getAge';
                $scope.ageCourseList = [];
                $scope.isHaveAgeCourse = false;
                $http.post(ageApi, apiData).then(function (response) {
                    if (response.data.length > 0) {
                        $scope.isHaveAgeCourse = true;
                        angular.forEach(response.data, function (value, key) {
                            $scope.ageCourseList.push(value);
                        })
                    }
                })
                var sportApi = apiUrl + 'CourseApi/getSports';
                $scope.sportCourseList = [];
                $scope.isHaveSportCourse = false;
                $http.post(sportApi, apiData).then(function (response) {
                    if (response.data.length > 0) {
                        $scope.isHaveSportCourse = true;
                        angular.forEach(response.data, function (value, key) {
                            $scope.sportCourseList.push(value);
                        })
                    }
                })
                var placeApi = apiUrl + 'CourseApi/getPlace';
                $scope.placeCourseList = [];
                $scope.isHavePlaceCourse = false;
                $http.post(placeApi, apiData).then(function (response) {
                    if (response.data.length > 0) {
                        $scope.isHavePlaceCourse = true;
                        angular.forEach(response.data, function (value, key) {
                            $scope.placeCourseList.push(value);
                        })
                    }
                })
                var motivationApi = apiUrl + 'CourseApi/getMotivation';
                $scope.motivationCourseList = [];
                $scope.isHaveMotivationCourse = false;
                $http.post(motivationApi, apiData).then(function (response) {
                    if (response.data.length > 0) {
                        $scope.isHaveMotivationCourse = true;
                        angular.forEach(response.data, function (value, key) {
                            $scope.motivationCourseList.push(value);
                        })
                    }
                })
                var sameApi = apiUrl + 'CourseApi/getSame';
                $scope.sameCourseList = [];
                $scope.isHaveSameCourse = false;
                $http.post(sameApi, apiData).then(function (response) {
                    if (response.data.length > 0) {
                        $scope.isHaveSameCourse = true;
                        angular.forEach(response.data, function (value, key) {
                            $scope.sameCourseList.push(value);
                        })
                    }
                })
                var watchApi = apiUrl + 'CourseApi/getWatch';
                $scope.watchCourseList = [];
                $scope.isHaveWatchCourse = false;
                $http.post(watchApi, apiData).then(function (response) {
                    if (response.data.length > 0) {
                        $scope.isHaveWatchCourse = true;
                        angular.forEach(response.data, function (value, key) {
                            $scope.watchCourseList.push(value);
                        })
                    }
                })
                var hasCourseApi = apiUrl + 'CourseApi/hasCourseApp';
                $http.post(hasCourseApi, apiData).then(function (response) {
                    if (response.data.length > 0) {
                        $scope.isHaveCourse = true;
                        $scope.courseList = [];
                        angular.forEach(response.data, function (value, key) {
                            $scope.courseList.push(value);
                        })
                        console.log($scope.courseList);
                    }
                })
            })
        }, 10);
        $scope.logout = function () {
            var logoutApi = 'http://163.17.136.197:8080/EMG/api/UserApi/LogOut';
            $http.get(logoutApi).then(function (response) {  
                console.log(response);
                window.location.href = 'http://163.17.136.197:8080/EMG';
            });
        };
        $scope.goIndex = function () {
            $scope.account = $window.account;
            console.log($scope.account);
            if ($scope.account != '') {
                var profileApi = 'http://163.17.136.197:8080/EMG/Api/UserApi/getUser';
                var apiData = { 'account': $scope.account };
                $http.post(profileApi, apiData).then(function (response) {
                    console.log(response.data);
                    $scope.loginUser = {
                        'account': response.data.Account,
                        'name': response.data.Name,
                        'photo': response.data.Url
                    }
                });
                $scope.isLogin = true;
            }
            window.location.href = 'http://163.17.136.197:8080/EMG';
        };
        // 登入後使用者個人資料
        var status = getChartOption('status');
        var bodyfatThumb = getChartOption('bodyfatThumb');
        var training = getChartOption('training');
        var grow = getChartOption('grow');
        setChart('block-chart-status', status);
        setChart('block-bodyfat-thumb', bodyfatThumb);
        setChart('block-training-thumb', training);
        setChart('block-grow-thumb', grow);
        // 修改個人資料
        $scope.form = {};
        // 行事曆控制項與預設
        $scope.courseData = [{
            'thumb': 'http://163.17.136.197:8080/EMG/images/dumbbell.png',
            'title': '啞鈴仰臥推舉',
            'time': '2016-4-11',
            'weekday': ' 星期一'
        }, {
            'thumb': 'http://163.17.136.197:8080/EMG/images/dumbbell-c.png',
            'title': '槓鈴仰臥推舉',
            'time': '2016-4-11',
            'weekday': ' 星期一'
        }, {
            'thumb': 'http://163.17.136.197:8080/EMG/images/gym.png',
            'title': '寬版伏地挺身',
            'time': '2016-4-11',
            'weekday': ' 星期一'
        }, {
            'thumb': 'http://163.17.136.197:8080/EMG/images/dumbbell.png',
            'title': '啞鈴單手後屈伸',
            'time': '2016-4-11',
            'weekday': ' 星期一'
        }, {
            'thumb': 'http://163.17.136.197:8080/EMG/images/dumbbell.png',
            'title': '啞鈴單手後屈伸',
            'time': '2016-4-11',
            'weekday': ' 星期一'
        }, {
            'thumb': 'http://163.17.136.197:8080/EMG/images/dumbbell-c.png',
            'title': '槓鈴窄臥推舉',
            'time': '2016-4-11',
            'weekday': ' 星期一'
        }, {
            'thumb': 'http://163.17.136.197:8080/EMG/images/dumbbell.png',
            'title': '啞鈴仰臥推舉',
            'time': '2016-4-16',
            'weekday': ' 星期六'
        }, {
            'thumb': 'http://163.17.136.197:8080/EMG/images/dumbbell-c.png',
            'title': '槓鈴仰臥推舉',
            'time': '2016-4-16',
            'weekday': ' 星期六'
        }, {
            'thumb': 'http://163.17.136.197:8080/EMG/images/muscle.jpg',
            'title': '寬版伏地挺身',
            'time': '2016-4-16',
            'weekday': ' 星期六'
        }, {
            'thumb': 'http://163.17.136.197:8080/EMG/images/dumbbell.png',
            'title': '啞鈴單手後屈伸',
            'time': '2016-4-16',
            'weekday': ' 星期六'
        }, {
            'thumb': 'http://163.17.136.197:8080/EMG/images/dumbbell.png',
            'title': '啞鈴單手後屈伸',
            'time': '2016-4-16',
            'weekday': ' 星期六'
        }, {
            'thumb': 'http://163.17.136.197:8080/EMG/images/dumbbell-c.png',
            'title': '槓鈴窄臥推舉',
            'time': '2016-4-16',
            'weekday': ' 星期六'
        }, {
            'thumb': 'http://163.17.136.197:8080/EMG/images/dumbbell-c.png',
            'title': '硬舉',
            'time': '2016-4-17',
            'weekday': ' 星期日'
        }, {
            'thumb': 'http://163.17.136.197:8080/EMG/images/muscle.jpg',
            'title': '站姿負重俯身挺背',
            'time': '2016-4-17',
            'weekday': ' 星期日'
        }, {
            'thumb': 'http://163.17.136.197:8080/EMG/images/dumbbell-c.png',
            'title': '槓鈴曲體划船',
            'time': '2016-4-17',
            'weekday': ' 星期日'
        }, {
            'thumb': 'http://163.17.136.197:8080/EMG/images/dumbbell.png',
            'title': '坐姿啞鈴交替彎舉',
            'time': '2016-4-17',
            'weekday': ' 星期日'
        }, {
            'thumb': 'http://163.17.136.197:8080/EMG/images/dumbbell.png',
            'title': '站姿啞鈴交替彎舉',
            'time': '2016-4-17',
            'weekday': ' 星期日'
        }, {
            'thumb': 'http://163.17.136.197:8080/EMG/images/dumbbell-c.png',
            'title': '槓鈴彎舉',
            'time': '2016-4-17',
            'weekday': ' 星期日'
        }];
        $scope.courseList = [];
        $scope.isHaveCourse = false;
        // 課程選項格式
        var courseOptions = { 'goal': '', 'complex': '', 'chest': '', 'back': '', 'shoulder': '', 'belly': '', 'foot': '', 'two': '', 'three': '', 'TBar': '', 'WBarbell': '', 'Barbell': '', 'Dumbbell': '', 'Smith': '', 'LegPress': '', 'Butterfly': '', 'PullBack': '', 'LegExtension': '', 'PullUp': '', 'Squat': '', 'Pulley': '', 'Boating': '', 'PulleyChest': '', 'WeightBench': '', 'ParallelBars': '', 'Cross': '' };
        // 目標選項格式
        var goalList = { 'strength': '', 'endurance': '' };
        // 部位選項格式
        var partList = { 'complex': '', 'chest': '', 'back': '', 'shoulder': '', 'shoulder': '', 'belly': '', 'foot': '', two: '', 'three': '' };
        // 器材選項格式
        var deviceList = { 'TBar': '', 'WBarbell': '', 'Barbell': '', 'Dumbbell': '', 'Smith': '', 'LegPress': '', 'Butterfly': '', 'PullBack': '', 'LegExtension': '', 'PullUp': '', 'Squat': '', 'Pulley': '', 'Boating': '', 'PulleyChest': '', 'WeightBench': '', 'ParallelBars': '', 'Cross': '' };
        $scope.deviceButtonList = [{ 'eng': 'TBar', 'chi': 'T-Bar划船訓練機' }, { 'eng': 'WBarbell', 'chi': 'W槓' }, { 'eng': 'Barbell', 'chi': '槓鈴' }, { 'eng': 'Dumbbell', 'chi': '啞鈴' }, { 'eng': 'Smith', 'chi': '史密斯機架' }, { 'eng': 'LegPress', 'chi': '腿舉機' }, { 'eng': 'Butterfly', 'chi': '蝴蝶機' }, { 'eng': 'PullBack', 'chi': '拉背機' }, { 'eng': 'LegExtension', 'chi': '腿部伸展機' }, { 'eng': 'PullUp', 'chi': '引體上升器' }, { 'eng': 'Squat', 'chi': '蹲舉機' }, { 'eng': 'Pulley', 'chi': '滑輪機和滑輪握把' }, { 'eng': 'Boating', 'chi': '划船機' }, { 'eng': 'PulleyChest', 'chi': '滑輪擴胸機' }, { eng: 'WeightBench', 'chi': '舉重椅' }, { 'eng': 'ParallelBars', 'chi': '雙槓' }, { 'eng': 'Cross', 'chi': '交叉訓練機' }];
        // 存放課程預覽
        $scope.previewList = [];
        // 複製格式 active為控制按鈕class所用
        $scope.courseOptions = angular.copy(courseOptions);
        $scope.selectedGoal = angular.copy(goalList);
        $scope.selectedPart = angular.copy(partList);
        $scope.selectedDevice = angular.copy(deviceList);
        // 確認是否輸入選項
        $scope.isGoalSet = false;
        $scope.isPartSet = false;
        $scope.isDeviceSet = false;
        // 判斷是否有部位選取
        var isPartialSelected = false;
        var partialSelectedCount = 0;
        // 判斷是否有器材選取
        var deviceSelectedCount = 0;
        // true時顯示課程選項(不重複)
        $scope.isCreateCourse = false;
        // true時顯示產生新課程視窗
        $scope.isCourseShow = false;
        // 控制按鈕class所用
        $scope.selectedStrength = false;
        $scope.selectedEndurance = false;
        // 控制產生新課程步驟所用
        $scope.courseStatus = { 'options': '', 'preview': '', 'posture': '' };
        // 開始產生新課程
        $scope.newCourse = function () {
            if (!$scope.isCreateCourse) {
                $scope.courseStatus['options'] = 'active';
            }
            $scope.isCreateCourse = true;
            $scope.isCourseShow = true;
        }
        var refreshCourse = function () {
            $scope.previewList = [];
            $scope.courseOptions = angular.copy(courseOptions);
            $scope.selectedGoal = angular.copy(goalList);
            $scope.selectedPart = angular.copy(partList);
            $scope.selectedDevice = angular.copy(deviceList);
            $scope.isGoalSet = false;
            $scope.isPartSet = false;
            isPartialSelected = false;
            partialSelectedCount = 0;
            $scope.isDeviceSet = false;
            deviceSelectedCount = 0;
            $scope.isCreateCourse = false;
            $scope.isCourseShow = false;
            $scope.selectedStrength = false;
            $scope.selectedEndurance = false;
            $scope.courseStatus = { 'options': '', 'preview': '', 'posture': '' };
        }
        // 隱藏產生新課程block
        $scope.hideCourse = function (option) {
            // 重置產生新課程
            if (option == 'quit') {
                refreshCourse();
            }
            $scope.isCourseShow = false;
        }
        // 設定訓練目標
        $scope.setGoal = function (options) {
            $scope.courseOptions.goal = options;
            if (options == '肌力') {
                $scope.selectedGoal['endurance'] = '';
                if ($scope.selectedGoal['strength'] != '肌力') {
                    $scope.selectedGoal['strength'] = '肌力';
                    $scope.isGoalSet = true;
                } else {
                    $scope.selectedGoal['strength'] = '';
                    $scope.isGoalSet = false;
                }
            }
            if (options == '肌耐力') {
                $scope.selectedGoal['strength'] = '';
                if ($scope.selectedGoal['endurance]'] != '肌耐力') {
                    $scope.selectedGoal['endurance'] = '肌耐力';
                    $scope.isGoalSet = true;
                } else {
                    $scope.selectedGoal['endurance'] = '';
                    $scope.isGoalSet = false;
                }
            }
        }
        // 設定訓練部位
        $scope.setPart = function (options) {
            var index = '';
            if (options == '複合') {
                index = 'complex';
            }
            if (options == '胸') {
                index = 'chest';
            }
            if (options == '背') {
                index = 'back';
            }
            if (options == '肩') {
                index = 'shoulder';
            }
            if (options == '腹') {
                index = 'belly';
            }
            if (options == '腳') {
                index = 'foot';
            }
            if (options == '肱二頭') {
                index = 'two';
            }
            if (options == '肱三頭') {
                index = 'three';
            }
            // 如果有均衡訓練或部位訓練跨選則更新選項
            if (index == 'complex' && isPartialSelected) {
                $scope.selectedPart = angular.copy(partList);
                partialSelectedCount = 0;
            }
            if (index != 'complex' && $scope.selectedPart['complex'] != '') {
                $scope.selectedPart = angular.copy(partList);
            }
            if (index != 'complex' && $scope.selectedPart[index] == '') {
                partialSelectedCount++;
            }
            if (index != 'complex' && $scope.selectedPart[index] != '') {
                partialSelectedCount--;
            }
            if (partialSelectedCount > 0) {
                isPartialSelected = true;
                $scope.isPartSet = true;
            } else {
                isPartialSelected = false;
                $scope.isPartSet = false;
            }
            // toggle選取
            if ($scope.selectedPart[index] == '') {
                $scope.selectedPart[index] = options;
                if (index == 'complex') {
                    $scope.isPartSet = true;
                }
            } else {
                $scope.selectedPart[index] = '';
                if (index == 'complex') {
                    $scope.isPartSet = false;
                }
            }
        }
        // 設定訓練器材
        $scope.setDevice = function (options) {
            var index = '';
            if (options == 'T-Bar划船訓練機') {
                index = 'TBar';
            }
            if (options == 'W槓') {
                index = 'WBarbell';
            }
            if (options == '槓鈴') {
                index = 'Barbell';
            }
            if (options == '啞鈴') {
                index = 'Dumbbell';
            }
            if (options == '史密斯機架') {
                index = 'Smith';
            }
            if (options == '腿舉機') {
                index = 'LegPress';
            }
            if (options == '蝴蝶機') {
                index = 'Butterfly';
            }
            if (options == '拉背機') {
                index = 'PullBack';
            }
            if (options == '腿部伸展機') {
                index = 'LegExtension';
            }
            if (options == '引體上升器') {
                index = 'PullUp';
            }
            if (options == '蹲舉機') {
                index = 'Squat';
            }
            if (options == '滑輪機和滑輪握把') {
                index = 'Pulley';
            }
            if (options == '划船機') {
                index = 'Boating';
            }
            if (options == '滑輪擴胸機') {
                index = 'PulleyChest';
            }
            if (options == '舉重椅') {
                index = 'WeightBench';
            }
            if (options == '雙槓') {
                index = 'ParallelBars';
            }
            if (options == '交叉訓練機') {
                index = 'Cross';
            }
            if ($scope.selectedDevice[index] == '') {
                deviceSelectedCount++;
                $scope.selectedDevice[index] = options;
            } else if ($scope.selectedDevice[index] != '') {
                deviceSelectedCount--;
                $scope.selectedDevice[index] = '';
            }
            if (deviceSelectedCount > 0) {
                $scope.isDeviceSet = true;
            } else {
                $scope.isDeviceSet = false;
            }
        }
        // 產生個人化課表
        $scope.createCourse = function () {
            // 判斷輸入選項
            if (!$scope.isGoalSet) {
                alert('請選擇訓練目標');
                return;
            };
            if (!$scope.isPartSet) {
                alert('請選擇訓練部位');
                return;
            };
            if (!$scope.isDeviceSet) {
                alert('請選擇訓練器材');
                return;
            }
            // 放入訓練部位
            angular.forEach($scope.selectedPart, function (value, key) {
                $scope.courseOptions[key] = $scope.selectedPart[key];
            })
            angular.forEach($scope.selectedDevice, function (value, key) {
                $scope.courseOptions[key] = $scope.selectedDevice[key];
            })
            // 產生個人化課表
            // 產生課程api網址
            var createCourseApi = 'http://163.17.136.197:8080/EMG/api/CourseApi/CreateCourse';
            // 產生課程選項
            var courseData = $scope.courseOptions;
            $http.post(createCourseApi, courseData).then(function (response) {
                var data = response.data;
                // 產生課程成功
                if (data != "") {
                    // 儲存一週課程資料
                    $scope.course = data;
                    // 接API回傳課程放入previewTitle跟previewList
                    angular.forEach(data, function (value, key) {
                        var preview = {
                            'day': 'Day' + value.Days,
                            'posture': [{ 'pos': value.Pos1 }, { 'pos': value.Pos2 },
                                { 'pos': value.Pos3 }, { 'pos': value.Pos4 }, { 'pos': value.Pos5 }, { 'pos': value.Pos6 }
                            ]
                        };
                        $scope.previewList.push(preview);
                    })
                    //顯示一週日期
                    $scope.activeDate = ['', '', '', '', '', '', ''];
                    $scope.previewDate = [];
                    var week = ['禮拜天', '禮拜一', '禮拜二', '禮拜三', '禮拜四', '禮拜五', '禮拜六'];
                    for (i = 1; i < 8; i++) {
                        var currentDate = new Date(new Date().getTime() + i * 86400000);
                        var year = currentDate.getFullYear();
                        var month = currentDate.getMonth() + 1;
                        var day = currentDate.getDate();
                        var weekday = currentDate.getDay();
                        var date = { 'month': month, 'day': day, 'weekday': week[weekday], 'date': year + '-' + month + '-' + day }
                        $scope.previewDate.push(date);
                    };
                    // 顯示課程預覽
                    $scope.courseStatus['preview'] = 'active';
                    $scope.courseStatus['options'] = '';
                }
                    // 產生課程失敗
                else {
                    alert("產生課程失敗");
                }
            });
        };
        // 設定開始日期
        $scope.setDate = function (index) {
            $scope.activeDate = ['', '', '', '', '', '', ''];
            $scope.activeDate[index] = 'active';
            $scope.currentDate = $scope.previewDate[index];
            $location.hash('btn-confirm');
            $anchorScroll();
        };
        // 確認課程
        $scope.courseConfirm = function () {
            console.log($scope.currentDate, $scope.courseOptions);
            // 儲存個人化課表
            // 儲存課程api網址
            //var saveCourseApi = 'http://localhost:55546/api/CourseApi/SaveCourse'; //本機測試用網址
            var saveCourseApi = 'http://163.17.136.197:8080/EMG/api/CourseApi/SaveCourse';
            // 課程資料
            var saveCourseData = { 'courseList': $scope.course, 'date': $scope.currentDate.date, 'goal': $scope.courseOptions.goal, 'power': $scope.courseOptions.power };
            $http.post(saveCourseApi, saveCourseData).then(function (response) {
                var data = response.data;
                // 產生課程成功
                if (data != "") {
                    // 儲存一週課程資料
                    angular.forEach(data, function (value, key) {
                        var values = value['Date'].split('T');
                        var time = values[0];
                        console.log(values, time)
                        $scope.time = time;
                        var course = { 'thumb': 'images/muscle.jpg', 'title': value['P_Name'], 'time': time };
                        $scope.courseList.push(course);
                    })
                    console.log(data);
                    console.log("success")
                    $scope.isHaveCourse = true;
                    $scope.previewList = [];
                    $scope.courseOptions = angular.copy(courseOptions);
                    $scope.selectedGoal = angular.copy(goalList);
                    $scope.activePart = angular.copy(partList);
                    $scope.selectedPart = angular.copy(partList);
                    $scope.isGoalSet = false;
                    $scope.isPartSet = false;
                    $scope.isDeviceSet = false;
                    isPartialSelected = false;
                    partialSelectedCount = 0;
                    $scope.isCreateCourse = false;
                    $scope.isCourseShow = false;
                    $scope.selectedStrength = false;
                    $scope.selectedEndurance = false;
                    $scope.courseStatus = { 'options': '', 'preview': '', 'posture': '' };
                }
                    // 儲存課程失敗
                else {
                    alert("儲存課程失敗");
                }
            });
        };
        // 顯示姿勢詳細教學
        $scope.showPostureDetail = function (pos) {
            var posDataApi = apiUrl + 'CourseApi/GetPosData';
            var data = { 'name': pos };
            $http.post(posDataApi, data).then(function (response) {
                $scope.posture = { 'name': response.data.Name, 'src': $sce.trustAsResourceUrl(response.data.Iframe) }
            });
            $scope.courseStatus['posture'] = 'active';
            $scope.courseStatus['preview'] = 'left';
        };
        // 返回課程預覽
        //$scope.backPreviewList = function () {
        //    $scope.courseStatus['preview'] = 'active';
        //    $scope.courseStatus['posture'] = '';
        //};
        //$scope.showRecommend = function (filter) {
        //    $scope.recommendList = [];
        //    if (filter == 'age') {
        //        $scope.recommendList = $scope.ageCourseList;
        //        $scope.recommendList.title = '依據年齡與性別推薦給你/妳';
        //    }
        //    if (filter == 'sport') {
        //        $scope.recommendList = $scope.sportCourseList;
        //        $scope.recommendList.title = '依據運動項目推薦給你/妳';
        //    }
        //    if (filter == 'place') {
        //        $scope.recommendList = $scope.placeCourseList;
        //        $scope.recommendList.title = '依據注重肌肉部位推薦給你/妳';
        //    }
        //    if (filter == 'motivation') {
        //        $scope.recommendList = $scope.motivationCourseList;
        //        $scope.recommendList.title = '依據健身動力推薦給你/妳';
        //    }
        //    if (filter == 'same') {
        //        $scope.recommendList = $scope.sameCourseList;
        //        $scope.recommendList.title = '依據完成課程推薦給你/妳';
        //    }
        //    if (filter == 'watch') {
        //        $scope.recommendList = $scope.watchCourseList;
        //        $scope.recommendList.title = '依據關注課程推薦給你/妳';
        //    }
        //    $scope.recommendList.isHaveCourse = true;
        //    if ($scope.recommendList.length == 0) {
        //        $scope.recommendList.isHaveCourse = false;
        //        $scope.recommendList.message = '目前暫無推薦課程';
        //    }
        //    $scope.isRecommendClick = true;
        //    $timeout(function () {
        //        $scope.isRecommendShow = true;
        //    }, 30);
        //    console.log($scope.recommendList);
        //}
        //$scope.hideRecommend = function () {
        //    $scope.isRecommendShow = false;
        //    $timeout(function () {
        //        $scope.isRecommendClick = false;
        //    }, 250);
        //}
        $scope.backPreviewList = function () {
            $scope.courseStatus['preview'] = 'active';
            $scope.courseStatus['posture'] = '';
        };
        $scope.showRecommend = function (filter) {
            $scope.recommendList = [];
            if (filter == 'age') {
                $scope.recommendList = $scope.ageCourseList;
                $scope.recommendList.title = '依據年齡與性別推薦給你/妳';
            }
            if (filter == 'sport') {
                if ($scope.isAdmin) {
                    $scope.recommendList = [{
                        'Days': 'Days1',
                        'Pos1': '仰臥三頭肌伸展',
                        'Pos2': '槓鈴窄臥推舉',
                        'Pos3': '槓鈴窄臥推舉',
                        'Pos4': '槓鈴窄臥推舉',
                        'Pos5': '槓鈴窄臥推舉',
                        'Pos6': '槓鈴窄臥推舉'
                    }, {
                        'Days': 'Days2',
                        'Pos1': '槓鈴彎舉',
                        'Pos2': '槓鈴斜板彎舉',
                        'Pos3': '站姿啞鈴錘式彎舉',
                        'Pos4': '槓鈴斜板彎舉',
                        'Pos5': '槓鈴彎舉',
                        'Pos6': '集中彎舉'
                    }, {
                        'Days': 'Days3',
                        'Pos1': '啞鈴聳肩',
                        'Pos2': '立正划船',
                        'Pos3': '立正划船',
                        'Pos4': '立姿啞鈴前抬舉',
                        'Pos5': '立姿啞鈴前抬舉',
                        'Pos6': '槓鈴頸後推舉'
                    }, {
                        'Days': 'Day4',
                        'Pos1': '仰臥三頭肌伸展',
                        'Pos2': '槓鈴窄臥推舉',
                        'Pos3': '坐姿啞鈴三頭肌伸展',
                        'Pos4': '仰臥三頭肌伸展',
                        'Pos5': '仰臥三頭肌伸展',
                        'Pos6': '啞鈴單手後屈伸'
                    }, {
                        'Days': 'Days5',
                        'Pos1': '坐姿啞鈴交替彎舉',
                        'Pos2': '站姿啞鈴錘式彎舉',
                        'Pos3': '集中彎舉',
                        'Pos4': '站姿啞鈴錘式彎舉',
                        'Pos5': '站姿啞鈴錘式彎舉',
                        'Pos6': '坐姿啞鈴交替彎舉'
                    }, {
                        'Days': 'Days6',
                        'Pos1': '坐姿啞鈴推舉',
                        'Pos2': '立姿側平舉',
                        'Pos3': '立姿側平舉',
                        'Pos4': '立姿側平舉',
                        'Pos5': '啞鈴聳肩',
                        'Pos6': '坐姿啞鈴推舉'
                    }]
                } else {
                    $scope.recommendList = $scope.sportCourseList;
                }
                $scope.recommendList.title = '依據運動項目推薦給你/妳';
            }
            if (filter == 'place') {
                if ($scope.isAdmin) {
                    $scope.recommendList = [{
                        'Days': 'Days1',
                        'Pos1': '側向捲腹',
                        'Pos2': '交換碰跟捲腹',
                        'Pos3': '側身仰臥起坐',
                        'Pos4': '上臥捲腹',
                        'Pos5': '雙手摸膝捲腹',
                        'Pos6': '仰臥起坐'
                    }, {
                        'Days': 'Days2',
                        'Pos1': '側向捲腹',
                        'Pos2': '雙手摸膝捲腹',
                        'Pos3': '雙腳合併畫鐘',
                        'Pos4': '上臥捲腹',
                        'Pos5': '上臥捲腹',
                        'Pos6': '上背平躺抬腿'
                    }]
                } else {
                    $scope.recommendList = $scope.placeCourseList;
                }
                $scope.recommendList.title = '依據注重肌肉部位推薦給你/妳';
            }
            if (filter == 'motivation') {
                if ($scope.isAdmin) {
                    $scope.recommendList = $scope.motivationCourseList;
                } else {
                    $scope.recommendList = [{
                        'Days': 'Days1',
                        'Pos1': '寬版伏地挺身',
                        'Pos2': '坐式機械飛鳥',
                        'Pos3': '寬版伏地挺身',
                        'Pos4': '滑輪三頭肌下壓',
                        'Pos5': '滑輪三頭肌下壓',
                        'Pos6': '滑輪三頭肌下壓'
                    }, {
                        'Days': 'Days2',
                        'Pos1': '坐姿滑輪頸前下拉',
                        'Pos2': '坐姿滑輪頸前下拉',
                        'Pos3': '頸後引體向上',
                        'Pos4': '滑輪二頭肌彎舉',
                        'Pos5': '滑輪二頭肌彎舉',
                        'Pos6': '滑輪二頭肌彎舉'
                    }, {
                        'Days': 'Days3',
                        'Pos1': '腿後勾',
                        'Pos2': '前抬腿',
                        'Pos3': '前抬腿',
                        'Pos4': '前抬腿',
                        'Pos5': '前抬腿',
                        'Pos6': '前抬腿'
                    }, {
                        'Days': 'Days4',
                        'Pos1': '滑輪單手前胸側平舉',
                        'Pos2': '滑輪單手前胸側平舉',
                        'Pos3': '滑輪單手前胸側平舉',
                        'Pos4': '滑輪單手前胸側平舉',
                        'Pos5': '滑輪單手前胸側平舉',
                        'Pos6': '滑輪單手前胸側平舉'
                    }, {
                        'Days': 'Days5',
                        'Pos1': '交換碰跟捲腹',
                        'Pos2': '仰臥起坐',
                        'Pos3': '上臥捲腹',
                        'Pos4': '仰臥腿上舉',
                        'Pos5': '交換碰跟捲腹',
                        'Pos6': 'V型仰臥起坐'
                    }]
                }
                $scope.recommendList.title = '依據健身動力推薦給你/妳';
            }
            if (filter == 'same') {
                if ($scope.isAdmin) {
                    $scope.recommendList = $scope.sameCourseList;
                } else {
                    $scope.recommendList = [{
                        'Days': 'Days1',
                        'Pos1': '站姿啞鈴錘式彎舉',
                        'Pos2': '槓鈴彎舉',
                        'Pos3': '槓鈴斜板彎舉',
                        'Pos4': '站姿啞鈴錘式彎舉',
                        'Pos5': '滑輪二頭肌彎舉',
                        'Pos6': '槓鈴斜板彎舉'
                    }, {
                        'Days': 'Days2',
                        'Pos1': '坐姿啞鈴交替彎舉',
                        'Pos2': '槓鈴斜板彎舉',
                        'Pos3': '坐姿啞鈴交替彎舉',
                        'Pos4': '坐姿啞鈴交替彎舉',
                        'Pos5': '站姿啞鈴錘式彎舉',
                        'Pos6': '站姿啞鈴錘式彎舉'
                    }]
                }
                $scope.recommendList.title = '依據完成課程推薦給你/妳';
            }
            if (filter == 'watch') {
                if ($scope.isAdmin) {
                    $scope.recommendList = $scope.watchCourseList;
                } else {
                    $scope.recommendList = [{
                        'Days': 'Days1',
                        'Pos1': '硬舉',
                        'Pos2': '俯身挺背',
                        'Pos3': '俯身挺背',
                        'Pos4': '槓鈴曲體划船',
                        'Pos5': '啞鈴單臂划船',
                        'Pos6': '啞鈴單臂划船'
                    }, {
                        'Days': 'Days2',
                        'Pos1': '槓鈴曲體划船',
                        'Pos2': '硬舉',
                        'Pos3': '站姿負重俯身挺背',
                        'Pos4': '俯身挺背',
                        'Pos5': '啞鈴單臂划船',
                        'Pos6': '啞鈴單臂划船'
                    }]
                }
                $scope.recommendList.title = '依據關注課程推薦給你/妳';
            }
            $scope.recommendList.isHaveCourse = true;
            if ($scope.recommendList.length == 0) {
                $scope.recommendList.isHaveCourse = false;
                $scope.recommendList.message = '目前暫無推薦課程';
            }
            $scope.isRecommendClick = true;
            $timeout(function () {
                $scope.isRecommendShow = true;
            }, 30);
            console.log($scope.recommendList);
        }
        $scope.hideRecommend = function () {
            $scope.isRecommendShow = false;
            $timeout(function () {
                $scope.isRecommendClick = false;
            }, 250);
        }
        $scope.showPositionDetail = function (pos) {
            //接入姿勢api
            var posDataApi = apiUrl + 'CourseApi/GetPosData';
            var data = { 'name': pos };
            $http.post(posDataApi, data).then(function (response) {
                $scope.posture = { 'name': response.data.Name, 'src': $sce.trustAsResourceUrl(response.data.Iframe) };
            });
            $scope.isPositionShow = true;
        }
        $scope.backCourseList = function () {
            $scope.isPositionShow = false;
        }

        // 訓練成效與肌肉成長
        // true時顯示說明block
        $scope.hoverGrow = false;
        $scope.hoverTraining = false;
        // 顯示說明block
        $scope.showMessage = function (target) {
            if ($scope.isAdmin) {
                if (target == 'training') {
                    this.trainingMoving = true;
                    $scope.hoverTraining = true;
                };
                if (target == 'grow') {
                    this.growMoving = true;
                    $scope.hoverGrow = true;
                };
            }
        };
        // 隱藏說明block
        $scope.hideMessage = function (target) {
            if (target == 'training') {
                $scope.hoverTraining = false;
            };
            if (target == 'grow') {
                $scope.hoverGrow = false;
            };
        };
        // 分享圖表至Facebook
        $scope.shareChart = function () {
            $timeout(function () {
                var svg = $scope.trainingSVG;
                console.log(svg)
            }, 10);
        };
    }
]);
// 偵測TransitionEnd
app.directive('whenTransitionEnd', [
    '$parse',
    function ($parse) {
        var transitions = {
            "transition": "transitionend",
            "OTransition": "oTransitionEnd",
            "MozTransition": "transitionend",
            "WebkitTransition": "webkitTransitionEnd"
        };

        var whichTransitionEvent = function () {
            var t,
                el = document.createElement("fakeelement");

            for (t in transitions) {
                if (el.style[t] !== undefined) {
                    return transitions[t];
                }
            }
        };

        var transitionEvent = whichTransitionEvent();

        return {
            'restrict': 'A',
            'link': function (scope, element, attrs) {
                var expr = attrs['whenTransitionEnd'];
                var fn = $parse(expr);

                element.bind(transitionEvent, function (evt) {

                    var phase = scope.$root.$$phase;

                    if (phase === '$apply' || phase === '$digest') {
                        fn();
                    } else {
                        scope.$apply(fn);
                    };
                });
            },
        };
    }
]);
app.directive("fileread", [function () {
    return {
        scope: {
            fileread: "="
        },
        link: function (scope, element, attributes) {
            element.bind("change", function (changeEvent) {
                var reader = new FileReader();
                reader.onload = function (loadEvent) {
                    scope.$apply(function () {
                        scope.fileread = loadEvent.target.result;
                    });
                }
                reader.readAsDataURL(changeEvent.target.files[0]);
            });
        }
    }
}]);
