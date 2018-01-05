$(function () {
    $('.datepicker').datepicker({
        language: "zh-TW",
        autoclose: true
    });
})
var app = angular.module('mainApp', ['mainApp']);
app.controller('blockController', ['$scope', '$sce', '$http', '$timeout', '$interval', '$window', function ($scope, $sce, $http, $timeout, $interval, $window) {
    // 轉html標籤方法
    $scope.trustHtml = function (string) {
        return $sce.trustAsHtml(string);
    };
    // 首頁
    // 首頁-最新消息 接api
    var newsApi = 'http://163.17.136.197:8080/EMG/Api/NewsApi/GetAll';
    $scope.newsList = [];
    $http.get(newsApi).then(function (response) {
        angular.forEach(response.data, function (value, key) {
            $scope.newsList.push({ 'id': value.N_Id, 'account': value.Account, 'title': value.Title, 'content': value.Detail, 'time': value.CreateTime });
        });
    });
    //$scope.newsList = [{ 'id': '1', 'title': '健身與精神疾病小知識', 'content': '患有精神分裂症（schizophrenia）的患者通常會被要求服用抗精神病藥物（antipsuchotics）以緩和病情，但抗精神病藥物通常只能改善精神的狀況，像是妄想與幻覺等，卻不能改善其它大腦的症狀，讓許多醫生相當困擾。而根據最新的研究顯示，若患者搭配健身的話，將能更加改善精神分裂症的症狀。\n\n精神分裂症，又稱為思覺失調症，患有此病的人有著相當多的病徵，其中大家最了解的徵狀就是出現幻覺與幻想。然而，其他像是影響記憶力、處理接收資訊的速度、注意力、解決問題的技巧等病徵雖看似不起眼，影響程度卻比出現幻覺還來的深遠。加州大學洛杉磯分校神經科學與人類行為研究所的 Keith Nuechterlein 教授表示，這些不起眼的病徵，都是導致他們患有殘疾與遭社會孤立的原因。\n\n世界上大約 1% 的人口深受精神分裂之苦，除了服用抗精神病藥物外，有的專家也會使用電腦遊戲來治療精神分裂症，大約能改善病患在記憶、思考能力、與社會認知中，四分之一到三分之一的病徵。但 Nuechterlein 教授的團隊發現，若在這個療程中搭配有氧運動的話，將可以大大改善精神病患者的症狀。\n在第一項進行大約 10 周的研究中，Nuechterlein 的團隊對 16 名患有早期精神分裂症的青壯年進行實驗，其中 9 名在這幾周中分別進行神經認知以及社會認知訓練的電腦程式操作，而剩下的 7 名除了進行相同的訓練外，更加上了每周 150 分鐘的有氧運動，看看其成果是否會有所不同。10 周過後，研究團隊發現，有搭配有氧運動的認知表現有著兩倍左右的差距，相當的驚人。\n\n而在第二項進行六個月的實驗中，共有 32 個患有早期精神分裂的患者參與實驗，其中一半的實驗者除了每周進行 4 小時的電腦遊戲訓練外，也搭配定期的有氧運動。六個月過後，研究學者更發現，有在運動的實驗者比沒運動的實驗者的症狀改善程度還好上三倍，顯示定期運動的時間若拉長，將會對患者有著更好的效果。\n\n然而，為什麼運動會有著這樣的成果呢？研究學者表示，這是因為在有氧運動的過程中，大腦會釋放出一種叫做腦源性神經營養生長因子（BDNF）的蛋白質，BDNF 能夠刺激在大腦中的學習與長期記憶中心—海馬迴，讓它能生長出新的神經，並且增加神經之間的連結，除了一般人以外，對於患有精神病的患者來說，更是能增長他們的記憶力以及學習力。\n\n團隊成員之一的心理學家研究助理 Sarah McEwen 表示，在第二項實驗裡，有運動的患者腦部裡的 BDNF 含量增加了 35%，其中過半的 BDNF 是在實驗剛進行的前兩個禮拜就已增加。此外，他們也測量了其他沒有運動的人大腦中的 BDNF 含量，在這六個月的過程中，則是一點也沒有增加。而研究學者也表示，這項研究對於患有早期精神分裂的人來說是較有效的，因為他們的病情比較具有長期的改善。\n\nNuechterlein 最後表示，他們做此研究希望的是，能夠避免慢性殘疾在精神分裂患者中出現，並且讓他們盡可能的在社會上繼續生活，有著規律的上學生活或者是正常的朋友關係。他們也認為，在未來，服用抗精神病藥物和電腦遊戲訓練只是基本能緩和精神病病徵的治療方式，搭配有氧運動的療程才能使患者更能讓患者大幅改善。' },
    //    { 'id': '1', 'title': '健身消息標題', 'content': '健身消息內文' }, { 'id': '1', 'title': '健身消息標題', 'content': '健身消息內文' },
    //    { 'id': '1', 'title': '健身消息標題', 'content': '健身消息內文' }, { 'id': '1', 'title': '健身消息標題', 'content': '健身消息內文' }, { 'id': '1', 'title': '健身消息標題', 'content': '健身消息內文' }, { 'id': '1', 'title': '健身消息標題', 'content': '健身消息內文' },
    //    { 'id': '1', 'title': '健身消息標題', 'content': '健身消息內文' }, { 'id': '1', 'title': '健身消息標題', 'content': '健身消息內文' }, { 'id': '1', 'title': '健身消息標題', 'content': '健身消息內文' }, { 'id': '1', 'title': '健身消息標題', 'content': '健身消息內文' },
    //    { 'id': '1', 'title': '健身消息標題', 'content': '健身消息內文' }, { 'id': '1', 'title': '健身消息標題', 'content': '健身消息內文' }, { 'id': '1', 'title': '健身消息標題', 'content': '健身消息內文' }, { 'id': '1', 'title': '健身消息標題', 'content': '健身消息內文' }
    //];
    // true時隱藏首頁block
    $scope.isDetailActive = false;
    // true時顯示對應detail
    $scope.detail3Active = false;
    $scope.detail4Active = false;
    $scope.detail5Active = false;
    // 以動畫差隱藏首頁block
    $scope.isOntop = false;
    // 防止動畫重複觸發
    $scope.setOntop = function () {
        if ($scope.isDetailActive && !$scope.isOntop) {
            $scope.isOntop = true;
        };
    };
    // 顯示首頁
    $scope.showIndex = function () {
        $scope.detail3Active = false;
        $scope.detail4Active = false;
        $scope.detail5Active = false;
        $scope.detail6Active = false;
        $scope.isOntop = false;
        $timeout(function () { $scope.isDetailActive = false }, 50);
        $scope.isMessageShow = false;
        $scope.message = {};
    };

    // 訓練成果相簿
    $scope.showDetail3 = function () {
        // 相簿API 取得相簿列表 放進$scope.albumList
        $scope.albumList = [];
        var getRankApi = 'http://163.17.136.197:8080/EMG/Api/AlbumApi/GetRank';
        $http.get(getRankApi).then(function (response) {
            var i = 0;
            angular.forEach(response.data, function (value, key) {
                $scope.albumList.push({ 'name': value.Name, 'Rank': value.Rank, 'photoList': [] });
                angular.forEach(value.photoList, function (value, key) {
                    $scope.albumList[i].photoList.push({ 'id': value.P_Id, 'src': $sce.valueOf($sce.trustAsHtml(value.Url)) });
                })
                i++;
            });
            $scope.isDetailActive = true;
            $scope.detail3Active = true;
            $scope.isAlbumListShow = true;
            $scope.isPhotoListShow = false;
        });
    };

    $scope.showPhotoList = function (album) {
        $scope.photo = { 'title': album.name, list: [] };
        $scope.photo.list = album.photoList;
        $scope.isPhotoListShow = true;
        $scope.isAlbumListShow = false;
    };
    $scope.showAlbumList = function () {
        $scope.isAlbumListShow = true;
        $scope.isPhotoListShow = false;
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

    // 最新消息
    $scope.showDetail4 = function (news) {
        $scope.detailTitle = news.title;
        $scope.detailContent = $sce.trustAsHtml(news.content.replace(new RegExp('\r?\n', 'g'), '<br />'));
        $scope.detail4Active = true;
        $scope.isDetailActive = true;
    };
    $scope.showDetail5 = function () {
        $scope.editHolder = '請輸入消息內文，或點選列表中的標題以編輯';
        $scope.detail5Active = true;
        $scope.isDetailActive = true;
        $scope.edit = { 'title': '', 'content': '' };
    };
    // 肌友交流
    $scope.showDetail6 = function () {
        $scope.messageList = [];
        var messageApi = 'http://163.17.136.197:8080/EMG/Api/MessageApi/getAll';    
        $http.get(messageApi).then(function (response) {
            angular.forEach(response.data, function (value, key) {
                $scope.messageList.push({ 'MId': value.MId, 'title': value.Title, 'content': value.Messages, 'author': value.UserName, 'photo': value.Photo, 'time': value.CreateTime });
            });
        });
        //$scope.messageList = [{
        //    'title': '肌友交流標題',
        //    'content': '患有精神分裂症（schizophrenia）的患者通常會被要求服用抗精神病藥物（antipsuchotics）以緩和病情，但抗精神病藥物通常只能改善精神的狀況，像是妄想與幻覺等，卻不能改善其它大腦的症狀，讓許多醫生相當困擾。而根據最新的研究顯示，若患者搭配健身的話，將能更加改善精神分裂症的症狀。\n\n精神分裂症，又稱為思覺失調症，患有此病的人有著相當多的病徵，其中大家最了解的徵狀就是出現幻覺與幻想。然而，其他像是影響記憶力、處理接收資訊的速度、注意力、解決問題的技巧等病徵雖看似不起眼，影響程度卻比出現幻覺還來的深遠。加州大學洛杉磯分校神經科學與人類行為研究所的 Keith Nuechterlein 教授表示，這些不起眼的病徵，都是導致他們患有殘疾與遭社會孤立的原因。\n\n世界上大約 1% 的人口深受精神分裂之苦，除了服用抗精神病藥物外，有的專家也會使用電腦遊戲來治療精神分裂症，大約能改善病患在記憶、思考能力、與社會認知中，四分之一到三分之一的病徵。但 Nuechterlein 教授的團隊發現，若在這個療程中搭配有氧運動的話，將可以大大改善精神病患者的症狀。\n在第一項進行大約 10 周的研究中，Nuechterlein 的團隊對 16 名患有早期精神分裂症的青壯年進行實驗，其中 9 名在這幾周中分別進行神經認知以及社會認知訓練的電腦程式操作，而剩下的 7 名除了進行相同的訓練外，更加上了每周 150 分鐘的有氧運動，看看其成果是否會有所不同。10 周過後，研究團隊發現，有搭配有氧運動的認知表現有著兩倍左右的差距，相當的驚人。\n\n而在第二項進行六個月的實驗中，共有 32 個患有早期精神分裂的患者參與實驗，其中一半的實驗者除了每周進行 4 小時的電腦遊戲訓練外，也搭配定期的有氧運動。六個月過後，研究學者更發現，有在運動的實驗者比沒運動的實驗者的症狀改善程度還好上三倍，顯示定期運動的時間若拉長，將會對患者有著更好的效果。\n\n然而，為什麼運動會有著這樣的成果呢？研究學者表示，這是因為在有氧運動的過程中，大腦會釋放出一種叫做腦源性神經營養生長因子（BDNF）的蛋白質，BDNF 能夠刺激在大腦中的學習與長期記憶中心—海馬迴，讓它能生長出新的神經，並且增加神經之間的連結，除了一般人以外，對於患有精神病的患者來說，更是能增長他們的記憶力以及學習力。\n\n團隊成員之一的心理學家研究助理 Sarah McEwen 表示，在第二項實驗裡，有運動的患者腦部裡的 BDNF 含量增加了 35%，其中過半的 BDNF 是在實驗剛進行的前兩個禮拜就已增加。此外，他們也測量了其他沒有運動的人大腦中的 BDNF 含量，在這六個月的過程中，則是一點也沒有增加。而研究學者也表示，這項研究對於患有早期精神分裂的人來說是較有效的，因為他們的病情比較具有長期的改善。\n\nNuechterlein 最後表示，他們做此研究希望的是，能夠避免慢性殘疾在精神分裂患者中出現，並且讓他們盡可能的在社會上繼續生活，有著規律的上學生活或者是正常的朋友關係。他們也認為，在未來，服用抗精神病藥物和電腦遊戲訓練只是基本能緩和精神病病徵的治療方式，搭配有氧運動的療程才能使患者更能讓患者大幅改善。',
        //    'author': '使用者',
        //    'photo': 'images/profile.jpg',
        //    'time': '2016/04/23 上午06:42:23'
        //},
        //{ 'title': '肌友交流標題', 'content': '肌友交流內文', 'author': '使用者', 'photo': 'images/profile.jpg', 'time': '2016/04/23 上午06:42:23' },
        //{ 'title': '肌友交流標題', 'content': '肌友交流內文', 'author': '使用者', 'photo': 'images/profile.jpg', 'time': '2016/04/23 上午06:42:23' },
        //{ 'title': '肌友交流標題', 'content': '肌友交流內文', 'author': '使用者', 'photo': 'images/profile.jpg', 'time': '2016/04/23 上午06:42:23' }
        //];
        $scope.message = { 'placeholder': $sce.trustAsHtml('請點選列表中的肌友交流標題') };
        $scope.detail6Active = true;
        $scope.isDetailActive = true;
        $scope.create = { 'replys': '' };
    };
    $scope.showMessage = function (message) {
        $scope.isMessageShow = true;
        $scope.message = angular.copy(message);
        $scope.message.content = $sce.trustAsHtml(message.content.replace(new RegExp('\r?\n', 'g'), '<br />'));
        $scope.message.MId = message.MId;
        $scope.replyList = []; 
        var replyApi = 'http://163.17.136.197:8080/EMG/Api/ReplyApi/GetAll';
        var replyData = { 'MId': $scope.message.MId };
        var i = 0;
        $http.post(replyApi, replyData).then(function (response) {
            var data = response.data;
            angular.forEach(data, function (value, key) {
                console.log(value);
                $scope.replyList.push({ 'RId': value.rID, 'content': value.Replys, 'account': value.Account, 'author': value.UserName, 'photo': value.Photo, 'time': value.ReplyTime });
            });
        });
        //$scope.replyList = [{ 'title': '回覆標題', 'content': '回覆內文\n', 'author': '使用者', 'photo': 'images/profile.jpg', 'time': '2016/04/23 上午08:42:13' },
        //    { 'title': '回覆標題', 'content': '回覆內文\n', 'author': '使用者', 'photo': 'images/profile.jpg', 'time': '2016/04/23 上午08:42:13' },
        //    { 'title': '回覆標題', 'content': '回覆內文\n', 'author': '使用者', 'photo': 'images/profile.jpg', 'time': '2016/04/23 上午08:42:13' }
        //]
        angular.forEach($scope.replyList, function (value, key) {
            $scope.replyList[key].content = $sce.valueOf($sce.trustAsHtml(value.content.replace(new RegExp('\r?\n', 'g'), '<br />')));
        });
    };
    $scope.createReply = function (message) {
        var createApi = 'http://163.17.136.197:8080/EMG/Api/ReplyApi/Create';
        var data = { 'MId': message.MId, 'Account': $scope.login.account,'UserName': $scope.userName, 'Photo': $scope.userPhoto, 'Replys': $scope.create.replys };
        $http.post(createApi, data).then(function (response) {
            alert('done!');
            $scope.replyList = [];
            var replyApi = 'http://163.17.136.197:8080/EMG/Api/ReplyApi/GetAll';
            var replyData = { 'MId': $scope.message.MId };
            var i = 0;
            $http.post(replyApi, replyData).then(function (response) {
                var data = response.data;
                angular.forEach(data, function (value, key) {
                    $scope.replyList.push({ 'RId': value.rID, 'content': value.Replys, 'account': value.Account, 'author': value.UserName, 'photo': value.Photo, 'time': value.ReplyTime });
                    i++;
                });
            });
        });
        $scope.create.replys = '';
    };   
    $scope.deleteReply = function (reply) {
        var deleteApi = 'http://163.17.136.197:8080/EMG/Api/ReplyApi/Delete';
        var data = { 'rId': reply.RId };        
        $http.post(deleteApi, data).then(function (response) {
            alert('done!');
            $scope.replyList = [];
            var replyApi = 'http://163.17.136.197:8080/EMG/Api/ReplyApi/GetAll';
            var replyData = { 'MId': $scope.message.MId };
            var i = 0;
            $http.post(replyApi, replyData).then(function (response) {
                var data = response.data;
                angular.forEach(data, function (value, key) {
                    $scope.replyList.push({ 'RId': value.rID, 'content': value.Replys, 'account': value.Account,'author': value.UserName, 'photo': value.Photo, 'time': value.ReplyTime });
                    i++;
                });
            });
        });
    };
    $scope.isSame = function (reply) {
        return reply.account == $scope.login.account;
    };
    $scope.showNews = function (news) {
        $scope.detailTitle = news.title;
        $scope.detailContent = $sce.trustAsHtml(news.content.replace(new RegExp('\r?\n', 'g'), '<br />'));
    }
    $scope.editNews = function (news) {
        $scope.editTitle = news.title;
        $scope.editContent = news.content;
        $scope.editId = news.id;
        $scope.editAccount = news.account;
        $scope.editTime = news.time;
    };
    $scope.submitNews = function () {
        var editApi = 'http://163.17.136.197:8080/EMG/Api/NewsApi/Edit';
        var data = { 'N_Id': $scope.editId, 'Account': $scope.editAccount, 'Title': $scope.editTitle, 'Detail': $scope.editContent };
        $http.post(editApi, data).then(function (response) {
            alert('done!');
            var newsApi = 'http://163.17.136.197:8080/EMG/Api/NewsApi/GetAll';
            $scope.newsList = [];
            $http.get(newsApi).then(function (response) {
                angular.forEach(response.data, function (value, key) {
                    $scope.newsList.push({ 'id': value.N_Id, 'account': value.Account, 'title': value.Title, 'content': value.Detail, 'time': value.CreateTime });
                });
            });
        });
    };
    $scope.deleteNews = function () {
        var deleteApi = 'http://163.17.136.197:8080/EMG/Api/NewsApi/Delete';
        var data = { 'N_Id': $scope.editId };
        $http.post(deleteApi, data).then(function (response) {
            alert('done!');
            var newsApi = 'http://163.17.136.197:8080/EMG/Api/NewsApi/GetAll';
            $scope.newsList = [];
            $http.get(newsApi).then(function (response) {
                angular.forEach(response.data, function (value, key) {
                    $scope.newsList.push({ 'id': value.N_Id, 'account': value.Account, 'title': value.Title, 'content': value.Detail, 'time': value.CreateTime });
                });
            });
        });
    };
    $scope.isAdmin = function () {
        return $scope.loginUser.account == 'administrator';
    };
    $scope.createNews = function () {
        $scope.editTitle = '';
        $scope.editContent = '';
        $scope.editHolder = '請輸入消息內文';
    };

    // 取得問卷資料
    // categories 放運動項目 series name放健身部位 data放各個部位的值
    var questList = [];
    var femaleCount = 0;
    var maleCount = 0;
    var wantList = [{ 'name': '腿部', 'y': 0 }, { 'name': '胸部', 'y': 0 }, { 'name': '背部', 'y': 0 }, { 'name': '腹部', 'y': 0 }, { 'name': '肩部', 'y': 0 }, { 'name': '肱二頭肌', 'y': 0 }, { 'name': '肱三頭肌', 'y': 0 }]
    var focusList = [{ 'name': '均衡發展', 'y': 0 }, { 'name': '腿部', 'y': 0 }, { 'name': '胸部', 'y': 0 }, { 'name': '背部', 'y': 0 }, { 'name': '腹部', 'y': 0 }, { 'name': '肩部', 'y': 0 }, { 'name': '肱二頭肌', 'y': 0 }, { 'name': '肱三頭肌', 'y': 0 }]
    var wantCount = { '腿部': 0, '胸部': 0, '背部': 0, '腹部': 0, '肩部': 0, '肱二頭肌': 0, '肱三頭肌': 0 };
    var focusCount = { '均衡發展': 0, '腿部': 0, '胸部': 0, '背部': 0, '腹部': 0, '肩部': 0, '肱二頭肌': 0, '肱三頭肌': 0 };
    var ageList = [{ 'name': '15~20', 'y': 0 }, { 'name': '21~25', 'y': 0 }, { 'name': '26~30', 'y': 0 }, { 'name': '30~35', 'y': 0 }, { 'name': '35~40', 'y': 0 }, { 'name': '40歲以上', 'y': 0 }];
    var ageCount = { '15~20': 0, '21~25': 0, '26~30': 0, '30~35': 0, '35~40': 0, '40歲以上': 0 };
    var maleWant = angular.copy(wantList);
    var maleWantCount = angular.copy(wantCount);
    var femaleWant = angular.copy(wantList);
    var femaleWantCount = angular.copy(wantCount);
    var maleFocus = angular.copy(focusList);
    var maleFocusCount = angular.copy(focusCount);
    var femaleFocus = angular.copy(focusList);
    var femaleFocusCount = angular.copy(focusCount);
    var maleAge = angular.copy(ageList);
    var maleAgeCount = angular.copy(ageCount);
    var femaleAge = angular.copy(ageList);
    var femaleAgeCount = angular.copy(ageCount);
    $scope.isMale = false;
    $scope.isFemale = true;
    var motiveList = [];
    var motiveCount = {};
    var sportList = ['其他'];
    var sportTempCount = {};
    var sportCount = [{ 'name': '其他', 'y': 0, 'data': [] }];
    var sportFocus = [];
    var sportSeries = [{ 'name': '均衡發展', 'data': [] }, { 'name': '腿部', 'data': [] }, { 'name': '胸部', 'data': [] }, { 'name': '背部', 'data': [] }, { 'name': '腹部', 'data': [] }, { 'name': '肩部', 'data': [] }, { 'name': '肱二頭肌', 'data': [] }, { 'name': '肱三頭肌', 'data': [] }, ]
    var questApi = 'https://spreadsheets.google.com/feeds/list/1ZiCa4_VaaajJ2mNj36Mb-Z51AwuwH5O7g60NN7a4SPQ/od6/public/values?alt=json';
    $http.get(questApi).then(function (response) {
        var entry = response.data.feed.entry;
        angular.forEach(entry, function (value, key) {
            var sex = entry[key]['gsx$sex'].$t;
            var age = entry[key]['gsx$age'].$t;
            var food = entry[key]['gsx$food'].$t;
            var frult = entry[key]['gsx$frult'].$t;
            var digest = entry[key]['gsx$digest'].$t;
            var regular = entry[key]['gsx$regular'].$t;
            var fried = entry[key]['gsx$fried'].$t;
            var sugary = entry[key]['gsx$sugary'].$t;
            var sleep = entry[key]['gsx$sleep'].$t;
            var frequency = entry[key]['gsx$frequency'].$t;
            var time = entry[key]['gsx$time'].$t;
            var sport = entry[key]['gsx$sport'].$t;
            var motive = entry[key]['gsx$motive'].$t;
            var level = entry[key]['gsx$level'].$t;
            var focus = entry[key]['gsx$focus'].$t;
            var want = entry[key]['gsx$want'].$t;
            var sustain = entry[key]['gsx$sustain'].$t;
            var request = entry[key]['gsx$request'].$t;
            var plan = entry[key]['gsx$plan'].$t;
            var data = {
                'sex': sex,
                'age': age,
                'food': food,
                'frult': frult,
                'digest': digest,
                'regular': regular,
                'fried': fried,
                'sugary': sugary,
                'sleep': sleep,
                'frequency': frequency,
                'time': time,
                'sport': sport,
                'motive': motive,
                'level': level,
                'focus': focus,
                'want': want,
                'sustain': sustain,
                'request': request,
                'plan': plan
            };
            questList.push(data);
            if (motiveCount[motive] == undefined) {
                motiveCount[motive] = 1;
                motiveList.push({ 'name': motive, 'y': 0 });
            } else {
                motiveCount[motive]++;
            }
            if (sportTempCount[sport] == undefined) {
                sportTempCount[sport] = 1;
            } else {
                sportTempCount[sport]++;
            }
            if (sex == '男生') {
                maleWantCount[want]++;
                maleFocusCount[focus]++;
                maleAgeCount[age]++;
                maleCount++;
            }
            if (sex == '女生') {
                femaleWantCount[want]++;
                femaleFocusCount[focus]++;
                femaleAgeCount[age]++;
                femaleCount++;
            }
        })
        angular.forEach(sportTempCount, function (value, key) {
            if (value >= 3 && key != '無') {
                sportCount.push({ 'name': key, 'y': value, 'data': [] });
                sportList.push(key);
            } else {
                sportCount[0].y += value;
            }
        })
        sportCount.sort(function (a, b) {
            return a.y - b.y;
        });
        sportCount.reverse();
        angular.forEach(motiveList, function (value, key) {
            motiveList[key].y = parseFloat((motiveCount[value.name] / questList.length * 100).toFixed(2));
        })
        angular.forEach(maleWant, function (value, key) {
            maleWant[key].y = parseFloat((maleWantCount[value.name] / maleCount * 100).toFixed(2));
        })
        angular.forEach(femaleWant, function (value, key) {
            femaleWant[key].y = parseFloat((femaleWantCount[value.name] / femaleCount * 100).toFixed(2));
        })
        angular.forEach(maleFocus, function (value, key) {
            maleFocus[key].y = parseFloat((maleFocusCount[value.name] / maleCount * 100).toFixed(2));
        })
        angular.forEach(femaleFocus, function (value, key) {
            femaleFocus[key].y = parseFloat((femaleFocusCount[value.name] / femaleCount * 100).toFixed(2));
        })
        angular.forEach(maleAge, function (value, key) {
            maleAge[key].y = parseFloat((maleAgeCount[value.name] / maleCount * 100).toFixed(2));
        })
        angular.forEach(femaleAge, function (value, key) {
            femaleAge[key].y = parseFloat((femaleAgeCount[value.name] / femaleCount * 100).toFixed(2));
        })
        maleWant.sort(function (a, b) {
            return a.y - b.y;
        })
        maleWant.reverse();
        femaleWant.sort(function (a, b) {
            return a.y - b.y;
        })
        femaleWant.reverse();
        maleFocus.sort(function (a, b) {
            return a.y - b.y;
        })
        maleFocus.reverse();
        femaleFocus.sort(function (a, b) {
            return a.y - b.y;
        })
        femaleFocus.reverse();
        $scope.showBigdata('want');
    })
    $scope.showBigdata = function (type) {
        $scope.type = type;
        var bigdataOption = {};
        if (type == 'want') {
            bigdataOption = getChartOption('tippie', '女生最想看異性哪個肌肉部位', femaleWant);
            $scope.bigdata = { 'want': true, 'focus': false, 'motivation': false, 'age': false, 'dot': false };
            $scope.isFemale = true;
            $scope.isMale = false;
        }
        if (type == 'focus') {
            bigdataOption = getChartOption('tippie', '男生最注重自己哪個肌肉部位', maleFocus);
            $scope.bigdata = { 'want': false, 'focus': true, 'motivation': false, 'age': false, 'dot': false };
            $scope.isFemale = false;
            $scope.isMale = true;
        }
        if (type == 'motivation') {
            bigdataOption = getChartOption('pie', '大家健身最大的動機為何', motiveList);
            $scope.bigdata = { 'want': false, 'focus': false, 'motivation': true, 'age': false, 'dot': false };
            $scope.isFemale = true;
            $scope.isMale = true;
        }
        if (type == 'age') {
            bigdataOption = getChartOption('age', '會員年齡分佈狀況', maleAge, femaleAge);
            $scope.bigdata = { 'want': false, 'focus': false, 'motivation': false, 'age': true, 'dot': false };
            $scope.isFemale = true;
            $scope.isMale = true;
        }
        if (type == 'dot') {
            bigdataOption = getChartOption('dotThumb');
            $scope.bigdata = { 'want': false, 'focus': false, 'motivation': false, 'age': false, 'dot': true };
            $scope.isFemale = true;
            $scope.isMale = true;
        }
        console.log(maleCount, femaleCount);
        setChart('block-bigdata1-thumb', bigdataOption);
    }
    $scope.changeSex = function (sex) {
        var chartOption = {};
        if ($scope.type == 'want') {
            if (sex == 'female') {
                $scope.isFemale = true;
                $scope.isMale = false;
                chartOption = getChartOption('tippie', '女生最想看異性哪個肌肉部位', femaleWant);
                setChart('block-bigdata1-thumb', chartOption);
            } else {
                $scope.isMale = true;
                $scope.isFemale = false
                chartOption = getChartOption('tippie', '男生最想看異性哪個肌肉部位', maleWant);
                setChart('block-bigdata1-thumb', chartOption);
            }
        }
        if ($scope.type == 'focus') {
            if (sex == 'female') {
                $scope.isFemale = true;
                $scope.isMale = false;
                chartOption = getChartOption('tippie', '女生最注重自己哪個肌肉部位', femaleFocus);
                setChart('block-bigdata1-thumb', chartOption);
            } else {
                $scope.isMale = true;
                $scope.isFemale = false;
                chartOption = getChartOption('tippie', '男生最注重自己哪個肌肉部位', maleFocus);
                setChart('block-bigdata1-thumb', chartOption);
            }
        }
    }
    // 等待登入
    $scope.loginLoading = false;
    // 是否登入
    $scope.isLogin = false;
    // 登入狀況說明
    $scope.loginMessage = "登入";
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
    // 登入
    $scope.login = function () {
            if ($scope.login.account == undefined) {
                alert('請填入帳號');
                return;
            }
            if ($scope.login.password == undefined) {
                alert('請填入密碼');
                return;
            }
            $scope.loginLoading = true;
            $scope.loginMessage = "Loading...";
            // 登入api網址
            var loginApi = 'http://163.17.136.197:8080/EMG/api/UserApi/Login';
            // 帳號密碼
            var loginData = { 'Account': $scope.login.account, 'Password': $scope.login.password };
            $http.post(loginApi, loginData).then(function (response) {
                var data = response.data;
                // 登入成功
                if (data.checkStr == "") {
                    // 轉往個人首頁
                    //$scope.userAccount = $scope.login.account;
                    //$scope.userName = data.userName;
                    //$scope.userPhoto = data.userPhoto;
                    
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
                    location.reload();
                }
                    // 登入失敗
                else {
                    alert(data.checkStr);
                }
                $scope.loginMessage = "登入"
                $scope.loginLoading = false;
            });
    };
    $scope.loginTest = function () {
        $scope.loginUser = { 'account': 's1410131021', 'name': '林明禎', 'photo': 'images/T52.jpg' }
        $scope.isLogin = true;
    };
    $scope.logout = function () {
        var logoutApi = 'http://163.17.136.197:8080/EMG/api/UserApi/LogOut';
        $http.get(logoutApi).then(function (response) {
            $scope.loginUser = {};
            $scope.isLogin = false;
            console.log(response);
        });
    };
    $scope.goBoard = function () {
        window.location.href = 'http://163.17.136.197:8080/EMG/Home/Board';
    };
    // 註冊
    $scope.form = {};
    $scope.register = function () {
        $scope.registerUser = {
            'account': '',
            'password': '',
            'name': '',
            'sex': '',
            'birth': '',
            'age': '',
            'blood': '',
            'height': '',
            'weight': '',
            'bodyfat': '',
            'bmr': '',
            'disease': '',
            'email': '',
            'phone': '',
            'url': 'images/profile.jpg',
            'balance': '',
            'strength': '',
            'endurance': '',
            'effect': '',
            'burden': '',
        };
        $scope.passwordMatch = '';
        $scope.registerBlur = {
            'account': '',
            'password': '',
            'passwordMatch': '',
            'name': '',
            'sex': '',
            'birth': '',
            'age': '',
            'blood': '',
            'height': '',
            'weight': '',
            'bodyfat': '',
            'bmr': '',
        };
        $scope.inputBlur = function (name) {
            if (name == 'account') {
                $scope.registerBlur.account = true;
            }
            if (name == 'password') {
                $scope.registerBlur.password = true;
            }
            if (name == 'passwordMatch') {
                $scope.registerBlur.passwordMatch = true;
            }
            if (name == 'name') {
                $scope.registerBlur.name = true;
            }
            if (name == 'sex') {
                $scope.registerBlur.sex = true;
            }
            if (name == 'birth') {
                $scope.registerBlur.birth = true;
                // 計算年齡
                var thisYear = new Date().getFullYear();
                var birthYear = new Date($scope.registerUser.birth).getFullYear();
                $scope.registerUser.age = thisYear - birthYear;
            }
            if (name == 'age') {
                $scope.registerBlur.age = true;
            }
            if (name == 'blood') {
                $scope.registerBlur.blood = true;
            }
            if (name == 'height') {
                $scope.registerBlur.height = true;
            }
            if (name == 'weight') {
                $scope.registerBlur.weight = true;
            }
            if (name == 'bodyfat') {
                $scope.registerBlur.bodyfat = true;
            }
            if (name == 'bmr') {
                $scope.registerBlur.bmr = true;
            }
        }
        $scope.inputFocus = function (name) {
            if (name == 'account') {
                $scope.registerBlur.account = '';
            }
            if (name == 'password') {
                $scope.registerBlur.password = '';
            }
            if (name == 'passwordMatch') {
                $scope.registerBlur.passwordMatch = '';
            }
            if (name == 'name') {
                $scope.registerBlur.name = '';
            }
            if (name == 'sex') {
                $scope.registerBlur.sex = '';
            }
            if (name == 'birth') {
                $scope.registerBlur.birth = '';
            }
            if (name == 'age') {
                $scope.registerBlur.age = '';
            }
            if (name == 'blood') {
                $scope.registerBlur.blood = '';
            }
            if (name == 'height') {
                $scope.registerBlur.height = '';
            }
            if (name == 'weight') {
                $scope.registerBlur = '';
            }
            if (name == 'bodyfat') {
                $scope.registerBlur = '';
            }
            if (name == 'bmr') {
                $scope.registerBlur = '';
            }
        }
        $scope.registerHabit = {
            'Account': '',
            'control': '',
            'fruit': '',
            'ability': '',
            'ontime': '',
            'fry': '',
            'drink': '',
            'quality': '',
            'sportnum': '',
            'sporttime': '',
            'sports': '',
            'motivation': '',
            'grade': '',
            'place': '',
            'look': '',
            'want': '',
            'request': '',
            'increase': '',
        }
        $scope.isRegisterClick = true;
        $scope.isFormShow = true;
        $scope.isQuestShow = false;
        $timeout(function () {
            $scope.isRegisterShow = true;
        }, 30);
    }
    $scope.hideRegister = function () {
        $scope.isRegisterShow = false;
        $timeout(function () {
            $scope.isRegisterClick = false;
        }, 250);
    }
    $scope.autoSet = function (name) {
        var age = $scope.registerUser.age;
        var sex = $scope.registerUser.sex;
        var height = $scope.registerUser.height;
        var weight = $scope.registerUser.weight;
        if (name == 'bodyfat') {
            var bmi = weight / ((height / 100) * (height / 100));
            if (sex == '男') {
                $scope.registerUser.bodyfat = parseFloat(((1.2 * bmi) + (0.23 * age) - 5.4 - 10.8).toFixed(2));
            }
            if (sex == '女') {
                $scope.registerUser.bodyfat = parseFloat(((1.2 * bmi) + (0.23 * age) - 5.4).toFixed(2));
            }
        }
        if (name == 'bmr') {
            if (sex == '男') {
                $scope.registerUser.bmr = parseFloat(((13.7 * weight) + (5 * height) - (6.8 * age) + 66).toFixed(2));
            }
            if (sex == '女') {
                $scope.registerUser.bmr = parseFloat(((9.6 * weight) + (1.8 * height) - (4.7 * age) + 655).toFixed(2));
            }
        }
    }
    $scope.doQuest = function (event) {
        event.preventDefault();
        // if($scope.form.registerForm.$invalid){
        //     alert('請確實填寫註冊資料');
        //     return false;
        // }
        $scope.isFormShow = false;
        $scope.isQuestClick = true;
        $timeout(function () {
            $scope.isQuestShow = true;
        }, 30);
    }
    $scope.otherValue = {
        'fruit': '',
        'sports': '',
        'motivation': '',
        'want': '',
        'request': ''
    };
    $scope.regsiterSubmit = function (event) {
        event.preventDefault();
        var registerApi = 'http://163.17.136.197:8080/EMG/Api/UserApi/Register';
        angular.forEach($scope.otherValue, function (value, key) {
            if (value != '') {
                $scope.registerHabit[key] = value;
            }
        })
        // if ($scope.form.habitForm.$invalid) {
        //     alert('請確實填寫問卷問項');
        //     return false;
        // }
        var data = {
            'account': $scope.registerUser.account,
            'password': $scope.registerUser.password,
            'name': $scope.registerUser.name,
            'sex': $scope.registerUser.sex,
            'birth': $scope.registerUser.birth,
            'age': $scope.registerUser.age,
            'blood': $scope.registerUser.blood,
            'height': $scope.registerUser.height,
            'weight': $scope.registerUser.weight,
            'bodyfat': $scope.registerUser.bodyfat,
            'bmr': $scope.registerUser.bmr,
            'disease': $scope.registerUser.disease,
            'email': $scope.registerUser.email,
            'phone': $scope.registerUser.phone,
            'url': $scope.registerUser.url,
            'balance': $scope.registerUser.balance,
            'strength': $scope.registerUser.strength,
            'endurance': $scope.registerUser.endurance,
            'effect': $scope.registerUser.effect,
            'burden': $scope.registerUser.burden,
            'Account': $scope.registerUser.account,
            'control': $scope.registerHabit.control,
            'fruit': $scope.registerHabit.fruit,
            'ability': $scope.registerHabit.ability,
            'ontime': $scope.registerHabit.ontime,
            'fry': $scope.registerHabit.fry,
            'drink': $scope.registerHabit.drink,
            'quality': $scope.registerHabit.quality,
            'sportnum': $scope.registerHabit.sportnum,
            'sporttime': $scope.registerHabit.sporttime,
            'sports': $scope.registerHabit.sports,
            'motivation': $scope.registerHabit.motivation,
            'grade': $scope.registerHabit.grade,
            'place': $scope.registerHabit.place,
            'look': $scope.registerHabit.look,
            'want': $scope.registerHabit.want,
            'request': $scope.registerHabit.request,
            'increase': $scope.registerHabit.increase        
        };
        console.log(data);
        $http.post(registerApi, data).then(function (response) {
            console.log(response);
        });
        $scope.registerUser = {};
        $scope.registerHabit = {};
        $scope.hideRegister();

    };

    // 名人榜
    var fameList = [{ 'title': '第一名', 'thumb': 'http://163.17.136.197:8080/EMG/images/profile.jpg', 'name': '姜琇森', 'slide': 'active', opacity: 'opacity' },
        { 'title': '第二名', 'thumb': 'http://163.17.136.197:8080/EMG/images/profile.jpg', 'name': '王麒瑞', 'slide': 'right', opacity: 'opacity' },
        { 'title': '第三名', 'thumb': 'http://163.17.136.197:8080/EMG/images/profile.jpg', 'name': '鄧方晴', 'slide': 'right', opacity: '' },
        { 'title': '第四名', 'thumb': 'http://163.17.136.197:8080/EMG/images/profile.jpg', 'name': '張哲瑋', 'slide': 'right', opacity: '' },
        { 'title': '第五名', 'thumb': 'http://163.17.136.197:8080/EMG/images/profile.jpg', 'name': '吳彧傑', 'slide': 'right', opacity: '' }
    ];
    $scope.fameList = angular.copy(fameList);
    var fameCount = 1;
    $interval(function () {
        if (fameCount == 0) {
            $scope.fameList[3].opacity = '';
            $scope.fameList[4].slide = 'left';
            $scope.fameList[0].slide = 'active';
            $scope.fameList[1].opacity = 'opacity';
            $scope.fameList[2].slide = 'right';
        }
        if (fameCount == 1) {
            $scope.fameList[4].opacity = '';
            $scope.fameList[0].slide = 'left';
            $scope.fameList[1].slide = 'active';
            $scope.fameList[2].opacity = 'opacity';
            $scope.fameList[3].slide = 'right';
        }
        if (fameCount == 2) {
            $scope.fameList[0].opacity = '';
            $scope.fameList[1].slide = 'left';
            $scope.fameList[2].slide = 'active';
            $scope.fameList[3].opacity = 'opacity';
            $scope.fameList[4].slide = 'right';
        }
        if (fameCount == 3) {
            $scope.fameList[1].opacity = '';
            $scope.fameList[2].slide = 'left';
            $scope.fameList[3].slide = 'active';
            $scope.fameList[4].opacity = 'opacity';
            $scope.fameList[0].slide = 'right';
        }
        if (fameCount == 4) {
            $scope.fameList[2].opacity = '';
            $scope.fameList[3].slide = 'left';
            $scope.fameList[4].slide = 'active';
            $scope.fameList[0].opacity = 'opacity';
            $scope.fameList[1].slide = 'right';
        }
        fameCount++;
        if (fameCount == 5) {
            fameCount = 0;
        }
    }, 2000);
    // 推薦課程 接入api
    //var popularCourse = {'title':'熱門課程','courseList':[]};
    //var getPopularApi = 'http://163.17.136.197:8080/EMG/Api/CourseApi/getPopular';
    //$http.get(getPopularApi).then(function (response) {
    //    angular.forEach(response.data, function (value, key) {
    //        popularCourse.courseList.push({ 'c_Id': value.C_Id, 'days': 'Day' + value.Days, 'pos1': value.Pos1, 'pos2': value.Pos2, 'pos3': value.Pos3, 'pos4': value.Pos4, 'pos5': value.Pos5, 'pos6': value.Pos6 });
    //    });
    //});

    //var partCourse = { 'title': '部位課程', 'courseList': [] };
    //var getThemeApi = 'http://163.17.136.197:8080/EMG/Api/CourseApi/getTheme';
    //$http.get(getThemeApi).then(function (response) {
    //    angular.forEach(response.data, function (value, key) {
    //        partCourse.courseList.push({ 'c_Id': value.C_Id, 'days': 'Day' + value.Days, 'pos1': value.Pos1, 'pos2': value.Pos2, 'pos3': value.Pos3, 'pos4': value.Pos4, 'pos5': value.Pos5, 'pos6': value.Pos6 });
    //    });
    //});

    //var bodyfatCourse = { 'title': '減脂課程', 'courseList': [] };
    //var getBodyfatApi = 'http://163.17.136.197:8080/EMG/Api/CourseApi/getBodyfat';
    //$http.get(getBodyfatApi).then(function (response) {
    //    angular.forEach(response.data, function (value, key) {
    //        bodyfatCourse.courseList.push({ 'c_Id': value.C_Id, 'days': 'Day' + value.Days, 'pos1': value.Pos1, 'pos2': value.Pos2, 'pos3': value.Pos3, 'pos4': value.Pos4, 'pos5': value.Pos5, 'pos6': value.Pos6 });
    //    });
    //});

    // 推薦課程 接入api
    var popularCourse = {
        title: '熱門課程',
        'courseList': [{
            'day': 'Day1',
            'position': [{ 'pos': '集中彎舉' }, { 'pos': '坐姿啞鈴交替彎舉' }, { 'pos': '站姿啞鈴錘式彎舉' },
                { 'pos': '槓鈴彎舉' }, { 'pos': '槓鈴斜板彎舉' }, { 'pos': '槓鈴彎舉' }
            ]
        }, {
            'day': 'Day2',
            'position': [{ 'pos': '槓鈴斜板彎舉' }, { 'pos': '槓鈴斜板彎舉' }, { 'pos': '集中彎舉' },
                { 'pos': '集中彎舉' }, { 'pos': '站姿啞鈴錘式彎舉' }, { 'pos': '槓鈴斜板彎舉' }
            ]
        }

        ]
    };
    var partCourse = {
        title: '部位課程',
        'courseList': [{
            'day': 'Day1',
            'position': [{ 'pos': '硬舉' }, { 'pos': '俯身挺背' }, { 'pos': '俯身挺背' },
                { 'pos': '槓鈴曲體划船' }, { 'pos': '啞鈴單臂划船' }, { 'pos': '啞鈴單臂划船' }
            ]
        }, {
            'day': 'Day2',
            'position': [{ 'pos': '槓鈴曲體划船' }, { 'pos': '硬舉' }, { 'pos': '站姿負重俯身挺背' },
                { 'pos': '俯身挺背' }, { 'pos': '啞鈴單臂划船' }, { 'pos': '啞鈴單臂划船' }
            ]
        }]
    };
    var bodyfatCourse = {
        title: '減脂課程',
        'courseList': [{
            'day': 'Day1',
            'position': [{ 'pos': '伏地挺身' }, { 'pos': '啞鈴仰臥推舉' }, { 'pos': '上斜槓鈴臥推' },
                { 'pos': '槓鈴窄臥推舉' }, { 'pos': '坐姿啞鈴三頭肌伸展' }, { 'pos': '槓鈴窄臥推舉' }
            ]
        }, {
            'day': 'Day2',
            'position': [{ 'pos': '硬舉' }, { 'pos': '硬舉' }, { 'pos': '啞鈴單臂划船' },
                { 'pos': '站姿啞鈴錘式彎舉' }, { 'pos': '站姿啞鈴錘式彎舉' }, { 'pos': '站姿啞鈴錘式彎舉' }
            ]
        }, {
            'day': 'Day3',
            'position': [{ 'pos': '直膝硬舉' }, { 'pos': '直膝硬舉' }, { 'pos': '啞鈴跨步深蹲' },
                { 'pos': '槓鈴深蹲' }, { 'pos': '直膝硬舉' }, { 'pos': '直膝硬舉' }
            ]
        }, {
            'day': 'Day4',
            'position': [{ 'pos': '槓鈴頸後推舉' }, { 'pos': '坐姿啞鈴推舉' }, { 'pos': '啞鈴聳肩' },
                { 'pos': '立姿啞鈴前抬舉' }, { 'pos': '立正划船' }, { 'pos': '坐姿啞鈴推舉' }
            ]
        }, {
            'day': 'Day5',
            'position': [{ 'pos': '交換碰跟捲腹' }, { 'pos': '側身仰臥起坐' }, { 'pos': '側身仰臥起坐' },
                { 'pos': '上背平躺抬腿' }, { 'pos': '上背平躺抬腿' }, { 'pos': '俄羅斯扭轉' }
            ]
        }]
    };
    $scope.isCourseClick = false;
    $scope.isCourseShow = false;
    $scope.isPositionShow = false;
    $scope.showCourse = function (filter) {
        if (filter == 'beginner') {
            $scope.courseTitle = angular.copy(beginnerCourse.title);
            $scope.courseList = angular.copy(beginnerCourse.courseList);
        }
        if (filter == 'popular') {
            $scope.courseTitle = angular.copy(popularCourse.title);
            $scope.courseList = angular.copy(popularCourse.courseList);
        }
        if (filter == 'part') {
            $scope.courseTitle = angular.copy(partCourse.title);
            $scope.courseList = angular.copy(partCourse.courseList);
        }
        if (filter == 'bodyfat') {
            $scope.courseTitle = angular.copy(bodyfatCourse.title);
            $scope.courseList = angular.copy(bodyfatCourse.courseList);
        }
        $scope.isCourseClick = true;
        $timeout(function () {
            $scope.isCourseShow = true;
        }, 30);
    }
    $scope.hideCourse = function () {
        $scope.isCourseShow = false;
        $timeout(function () {
            $scope.isCourseClick = false;
        }, 250);
    }
    $scope.showPositionDetail = function (pos) {
        //接入姿勢api
        $scope.positionName = pos;
        $scope.isPositionShow = true;
    }
    $scope.backCourseList = function () {
        $scope.isPositionShow = false;
    }
}]);
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
