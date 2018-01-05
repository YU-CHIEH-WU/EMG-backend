//TODO:參數增加data傳入
function setChart(targetId, chartOption) {
    var target = $('#' + targetId);
    target.highcharts(chartOption);
}

function getChartOption(ChartName, title, data1, data2) {
    var chartOption = {};
    if (ChartName == "age") {
        chartOption = {
            chart: { type: 'column' },
            title: { text: title },
            exporting: { enabled: false },
            xAxis: {
                categories: ['15~20', '20~25', '25~30', '30~35', '35~40', '40歲以上'],
                crosshair: true
            },
            yAxis: {
                min: 0,
                title: { text: '百分比' }
            },
            tooltip: {
                headerFormat: '<b>{series.name}</b><br>',
                pointFormat: '<b>{point.y:.1f}%</b>'
            },
            legend: {
                layout: 'vertical',
                align: 'left',
                verticalAlign: 'top',
                floating: true,
                backgroundColor: (Highcharts.theme && Highcharts.theme.legendBackgroundColor) || '#FFFFFF',
                borderWidth: 1,
                y: 40,
                x: 50
            },
            plotOptions: {
                column: {
                    pointPadding: 0.2,
                    borderWidth: 0
                }
            },
            credits: { enabled: false },
            series: [{
                name: '男性',
                data: data1
            }, {
                name: '女性',
                data: data2
            }]
        };
    }
    // 大數據分析圖
    if (ChartName == "tippie") {
        chartOption = {
            chart: {
                plotBackgroundColor: null,
                plotBorderWidth: null,
                plotShadow: false,
                type: 'pie'
            },
            title: {
                text: title
            },
            tooltip: {
                pointFormat: '<b>{point.percentage:.1f}%</b>'
            },
            legend: {
                enabled: false
            },
            plotOptions: {
                pie: {
                    allowPointSelect: true,
                    cursor: 'pointer',
                    dataLabels: {
                        enabled: true,
                        format: '<b>{point.name}</b>: {point.percentage:.1f} %',
                        style: {
                            color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                        }
                    },
                    showInLegend: true
                }
            },
            series: [{
                colorByPoint: true,
                data: data1
            }],
            credits: {
                enabled: false
            }
        };
    }
    if (ChartName == "pie") {
        chartOption = {
            chart: {
                plotBackgroundColor: null,
                plotBorderWidth: null,
                plotShadow: false,
                type: 'pie'
            },
            title: {
                text: title
            },
            tooltip: {
                pointFormat: '<b>{point.percentage:.1f}%</b>'
            },
            legend: {
                enabled: false
            },
            plotOptions: {
                pie: {
                    allowPointSelect: true,
                    cursor: 'pointer',
                    dataLabels: {
                        enabled: false,
                        format: '<b>{point.name}</b>: {point.percentage:.1f} %',
                        style: {
                            color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                        }
                    },
                    showInLegend: true
                }
            },
            series: [{
                colorByPoint: true,
                data: data1
            }],
            credits: {
                enabled: false
            }
        };
    }
    // 肌力變化-公斤&百分比
    if (ChartName == "1rm") {
        chartOption = {
            chart: {
                zoomType: 'xy',
                spacing: [10, 0, 0, 0]
            },
            title: {
                text: '訓練期間肌力變化圖'
            },
            subtitle: {
                text: '1RM為評估最大肌力的指標'
            },
            xAxis: [{
                categories: ['2/17', '2/24', '3/2', '3/9', '3/16', '3/23', '3/30', '4/6', '4/13', '4/20', '4/27', '5/4', '5/11', '5/18', '5/25'],
                crosshair: true
            }],
            yAxis: [{ // Primary yAxis
                labels: {
                    format: '{value}%',
                    style: {
                        color: Highcharts.getOptions().colors[1]
                    }
                },
                title: {
                    enabled: false,
                    text: '成長百分比',
                    style: {
                        color: Highcharts.getOptions().colors[1]
                    },
                    align: 'high',
                    rotation: 0,
                    x: 50,
                    y: -20
                }
            }, { // Secondary yAxis
                title: {
                    enabled: false,
                    text: '成長公斤數',
                    style: {
                        color: Highcharts.getOptions().colors[0]
                    },
                    align: 'high',
                    rotation: 0,
                    x: -50,
                    y: -20
                },
                labels: {
                    format: '{value} KG',
                    style: {
                        color: Highcharts.getOptions().colors[0]
                    }
                },
                opposite: true
            }],
            tooltip: {
                shared: true
            },
            legend: {
                borderWidth: 0,
                itemDistance: 50,
                margin: 24,
                itemStyle: {
                    "font-size": "14px"
                }
            },
            series: [{
                name: '成長公斤數',
                type: 'column',
                yAxis: 1,
                data: [10, 12, 13, 14, 16, 18, 19, 21, 23, 26, 27, 29, 30, 31, 33],
                tooltip: {
                    valueSuffix: ' KG'
                }

            }, {
                name: '成長百分比',
                type: 'spline',
                data: [0, 20.0, 30.0, 40.0, 60.0, 80.0, 90.0, 110.0, 130.0, 160.0, 170.0, 190.0, 200.0, 210.0, 230.0],
                tooltip: {
                    valueSuffix: '%'
                }
            }],
            credits: {
                enabled: false
            }
        };
    }
    //肌力變化-公斤
    if (ChartName == "1rmKG") {
        chartOption = {
            chart: {
                type: 'spline'
            },
            title: {
                text: '肌力變化-公斤',
                x: -20 //center
            },
            subtitle: {
                text: '1RM為評估最大肌力的指標'
            },
            legend: {
                borderWidth: 0,
                itemDistance: 50,
                margin: 24,
                itemStyle: {
                    "font-size": "14px"
                }
            },
            xAxis: {
                categories: ['2/17', '2/19', '2/20', '2/22', '2/23', '2/25']
            },
            yAxis: {
                title: {
                    text: null
                },
                labels: {
                    format: '{value}KG'
                },
                plotLines: [{
                    value: 0,
                    width: 1,
                    color: '#808080'
                }]
            },
            tooltip: {
                valueSuffix: 'KG'
            },
            series: [{
                name: '肌力',
                data: [82, 87, 94, 100, 106, 112]
            }],
            credits: {
                enabled: false
            }
        };
    }
    //肌力變化-百分比
    if (ChartName == "1rmGrow") {
        chartOption = {
            chart: {
                type: 'spline'
            },
            title: {
                text: '肌力變化-百分比',
                x: -20 //center
            },
            legend: {
                borderWidth: 0,
                itemDistance: 50,
                margin: 24,
                itemStyle: {
                    "font-size": "14px"
                }
            },
            xAxis: {
                type: 'datetime'
            },
            yAxis: {
                title: {
                    text: null
                },
                labels: {
                    format: '{value}%'
                },
                plotLines: [{
                    value: 0,
                    width: 1,
                    color: '#808080'
                }]
            },
            tooltip: {
                valueSuffix: '%'
            },
            plotOptions: {
                spline: {
                    pointInterval: 86400000, // one hour
                    pointStart: Date.UTC(2016, 1, 17)
                }
            },
            series: [{
                name: '肌力',
                data: [0, 2, 3, 5, 8, 10, 12]
            }],
            credits: {
                enabled: false
            }
        };
    }
    //肌力變化-訓練動作
    if (ChartName == "1rmWays") {
        chartOption = {
            chart: {
                type: 'bar'
            },
            title: {
                text: '肌力變化-訓練動作'
            },
            xAxis: {
                categories: ['啞鈴集中彎舉', '啞鈴斜板彎舉', '引體向上', '槓鈴站立彎舉'],
                title: {
                    text: null
                }
            },
            yAxis: {
                min: 0,
                title: {
                    text: null,
                    align: 'high'
                },
                labels: {
                    overflow: 'justify'
                }
            },
            tooltip: {
                valueSuffix: 'KG'
            },
            plotOptions: {
                bar: {
                    dataLabels: {
                        enabled: true,
                        format: '{point.y}KG'
                    }
                }
            },
            credits: {
                enabled: false
            },
            series: [{
                name: '肌力',
                data: [21, 12, 6, 9]
            }]
        };
    }
    //肌耐力變化-公斤&百分比
    if (ChartName == "15rm") {
        chartOption = {
            chart: {
                zoomType: 'xy',
                spacing: [10, 0, 0, 0]
            },
            title: {
                text: '訓練期間肌耐力變化圖'
            },
            subtitle: {
                text: '12RM為評估肌耐力的指標'
            },
            xAxis: [{
                categories: ['2/17', '2/24', '3/2', '3/9', '3/16', '3/23', '3/30', '4/6', '4/13', '4/20', '4/27', '5/4', '5/11', '5/18', '5/25'],
                crosshair: true
            }],
            yAxis: [{ // Primary yAxis
                labels: {
                    format: '{value}%',
                    style: {
                        color: Highcharts.getOptions().colors[1]
                    }
                },
                title: {
                    enabled: false,
                    text: '成長百分比',
                    style: {
                        color: Highcharts.getOptions().colors[1]
                    },
                    align: 'high',
                    rotation: 0,
                    x: 50,
                    y: -20,
                    offset: 50
                }
            }, { // Secondary yAxis
                title: {
                    enabled: false,
                    text: '成長公斤數',
                    style: {
                        color: Highcharts.getOptions().colors[0]
                    },
                    align: 'high',
                    rotation: 0,
                    x: -50,
                    y: -20,
                    offset: 50
                },
                labels: {
                    format: '{value} KG',
                    style: {
                        color: Highcharts.getOptions().colors[0]
                    }
                },
                opposite: true
            }],
            tooltip: {
                shared: true
            },
            legend: {
                borderWidth: 0,
                itemDistance: 50,
                margin: 24,
                itemStyle: {
                    "font-size": "14px"
                }
            },
            series: [{
                name: '成長公斤數',
                type: 'column',
                yAxis: 1,
                data: [3, 5, 7, 8, 10, 12, 13, 15, 16, 18, 19, 20, 21, 23, 25],
                tooltip: {
                    valueSuffix: ' KG'
                }

            }, {
                name: '成長百分比',
                type: 'spline',
                data: [0, 66.6, 133.3, 166.6, 233.3, 300.0, 333.3, 400.0, 433.3, 500.0, 533.3, 566.6, 600.0, 633.3, 733.3],
                tooltip: {
                    valueSuffix: '%'
                }
            }],
            credits: {
                enabled: false
            }
        };
    }
    //肌耐力變化-百分比
    if (ChartName == "15rmGrow") {
        chartOption = {
            chart: {
                type: 'spline'
            },
            title: {
                text: '肌耐力變化-百分比',
                x: -20 //center
            },
            legend: {
                borderWidth: 0,
                itemDistance: 50,
                margin: 24,
                itemStyle: {
                    "font-size": "14px"
                }
            },
            xAxis: {
                type: 'datetime'
            },
            yAxis: {
                title: {
                    text: null
                },
                labels: {
                    format: '{value}%'
                },
                plotLines: [{
                    value: 0,
                    width: 1,
                    color: '#808080'
                }]
            },
            tooltip: {
                valueSuffix: '%'
            },
            plotOptions: {
                spline: {
                    pointInterval: 86400000, // one hour
                    pointStart: Date.UTC(2016, 1, 17)
                }
            },
            series: [{
                name: '肌耐力',
                data: [0, 2, 3, 5, 6, 8, 9]
            }],
            credits: {
                enabled: false
            }
        };
    }
    //肌耐力變化-訓練動作
    if (ChartName == "15rmWays") {
        chartOption = {
            chart: {
                type: 'bar'
            },
            title: {
                text: '肌耐力變化-訓練動作'
            },
            xAxis: {
                categories: ['啞鈴集中彎舉', '啞鈴斜板彎舉', '引體向上', '槓鈴站立彎舉'],
                title: {
                    text: null
                }
            },
            yAxis: {
                min: 0,
                title: {
                    text: null,
                    align: 'high'
                },
                labels: {
                    overflow: 'justify'
                }
            },
            tooltip: {
                valueSuffix: 'KG'
            },
            plotOptions: {
                bar: {
                    dataLabels: {
                        enabled: true,
                        format: '{point.y}KG'
                    }
                }
            },
            credits: {
                enabled: false
            },
            series: [{
                name: '肌耐力',
                data: [14, 8, 5, 2]
            }]
        };
    }
    //體脂變化-原始百分比
    if (ChartName == "bodyfat") {
        var data = { "sex": "男", "age": 21 };
        var standard = [];
        var fat = { "fatEnd": 45 };
        //判斷體脂標準
        if (data.sex == "男") {
            if (data.age >= 18 && data.age <= 20) {
                standard = {};
            }
            if (data.age >= 21 && data.age <= 25) {
                standard = { "leanStart": 2.5, "leanEnd": 8.4, "idealStart": 8.4, "idealEnd": 15.4, "averageStart": 15.4, "averageEnd": 21.2, "aboveStart": 21.2, "aboveEnd": 25.8, };
            }
        }
        chartOption = {
            chart: {
                type: 'spline'
            },
            title: {
                text: null,
                x: -20 //center
            },
            legend: {
                enabled: false,
                borderWidth: 0,
                itemDistance: 50,
                margin: 24,
                itemStyle: {
                    "font-size": "14px"
                }
            },
            xAxis: {
                type: 'datetime'
            },
            yAxis: {
                title: {
                    text: null
                },
                labels: {
                    format: '{value}%'
                },
                plotBands: [{ // lean
                    from: standard.leanStart,
                    to: standard.leanEnd,
                    color: 'rgba(0,188,212,0.1)',
                    label: {
                        text: '過瘦',
                        style: {
                            color: '#fff'
                        }
                    }
                }, { // ideal
                    from: standard.idealStart,
                    to: standard.idealEnd,
                    color: 'rgba(76,175,80,0.1)',
                    label: {
                        text: '理想',
                        style: {
                            color: '#fff'
                        }
                    }
                }, { // average
                    from: standard.averageStart,
                    to: standard.averageEnd,
                    color: 'rgba(255,235,59,0.1)',
                    label: {
                        text: '標準',
                        style: {
                            color: '#fff'
                        }
                    }
                }, { // above
                    from: standard.aboveStart,
                    to: standard.aboveEnd,
                    color: 'rgba(255,193,7,0.1)',
                    label: {
                        text: '微胖',
                        style: {
                            color: '#fff'
                        }
                    }
                }, { // fat
                    from: standard.aboveEnd,
                    to: fat.fatEnd,
                    color: 'rgba(244,67,54,0.1)',
                    label: {
                        text: '過胖',
                        style: {
                            color: '#fff'
                        }
                    }
                }]
            },
            tooltip: {
                valueSuffix: '%'
            },
            plotOptions: {
                spline: {
                    pointInterval: 86400000, // one hour
                    pointStart: Date.UTC(2016, 1, 17)
                }
            },
            series: [{
                name: '體脂率',
                data: [29.8, 26.1, 23.4, 19.3, 16.9, 15.8, 14.6]
            }],
            credits: {
                enabled: false
            }
        };
    }
    //體脂變化-變化百分比
    if (ChartName == "bodyfatGrow") {
        chartOption = {
            chart: {
                type: 'spline'
            },
            title: {
                text: '體脂變化-變化百分比',
                x: -20 //center
            },
            legend: {
                borderWidth: 0,
                itemDistance: 50,
                margin: 24,
                itemStyle: {
                    "font-size": "14px"
                }
            },
            xAxis: {
                type: 'datetime'
            },
            yAxis: {
                title: {
                    text: null
                },
                labels: {
                    format: '{value}%'
                },
                plotLines: [{
                    value: 0,
                    width: 1,
                    color: '#808080'
                }]
            },
            tooltip: {
                valueSuffix: '%'
            },
            plotOptions: {
                spline: {
                    pointInterval: 86400000, // one hour
                    pointStart: Date.UTC(2016, 1, 17)
                }
            },
            series: [{
                name: '體脂率',
                data: [0, 2, 3, 5, 6, 8, 10]
            }],
            credits: {
                enabled: false
            }
        };
    }
    //體脂變化-訓練動作
    if (ChartName == "bodyfatWays") {
        chartOption = {
            chart: {
                type: 'bar'
            },
            title: {
                text: '訓練動作之體脂變化'
            },
            xAxis: {
                categories: ['啞鈴集中彎舉', '啞鈴斜板彎舉', '引體向上', '槓鈴站立彎舉'],
                title: {
                    text: null
                }
            },
            yAxis: {
                min: 0,
                title: {
                    text: null,
                    align: 'high'
                },
                labels: {
                    overflow: 'justify'
                }
            },
            tooltip: {
                valueSuffix: '%'
            },
            plotOptions: {
                bar: {
                    dataLabels: {
                        enabled: true,
                        format: '{point.y}%'
                    }
                }
            },
            credits: {
                enabled: false
            },
            series: [{
                name: '體脂變化率',
                data: [4, 2, 1, 0]
            }]
        };
    }
    //訓練姿勢相對成長
    if (ChartName == "growPart") {
        chartOption = {
            chart: {
                type: 'bar'
            },
            title: {
                text: '肌肉部位之相對肌肉成長'
            },
            xAxis: {
                categories: ['肱二頭肌', '肱三頭肌', '胸肌', '腹肌', '背肌', '肩膀', '腿部'],
                title: {
                    text: null
                }
            },
            yAxis: {
                min: 0,
                title: {
                    text: null,
                    align: 'high'
                },
                labels: {
                    overflow: 'justify'
                }
            },
            tooltip: {
                valueSuffix: '%'
            },
            plotOptions: {
                bar: {
                    dataLabels: {
                        enabled: true,
                        format: '{point.y}%'
                    }
                },
                series: {
                    pointPadding: 0.15
                }
            },
            credits: {
                enabled: false
            },
            series: [{
                name: '肌力',
                data: [31, 25, 15, 6, 11, 4, 8]
            }, {
                name: '肌耐力',
                data: [41, 24, 10, 6, 9, 3, 7]
            }]
        };
    }
    //訓練成效-肌肉疲勞
    if (ChartName == "fatigue") {
        chartOption = {
            chart: {
                type: 'areaspline'
            },
            colors: ['#FF9A00'],
            title: {
                text: '肌肉疲勞'
            },
            tooltip: {
                valueSuffix: '%'
            },
            xAxis: {
                categories: ['1-1', '1-2', '1-3', '1-4', '1-5', '1-6', '1-7', '1-8', '2-1', '2-2', '2-3', '2-4', '2-5', '2-6', '2-7', '2-8', '3-1', '3-2', '3-3', '3-4', '3-5', '3-6', '3-7', '3-8']
            },
            yAxis: {
                title: {
                    text: null
                },
                min: 0,
                max: 100,
                tickInterval: 20,
                labels: {
                    format: '{value}%'
                }
            },
            legend: {
                enabled: false
            },
            plotOptions: {
                area: {
                    marker: {
                        enabled: false,
                        symbol: 'circle',
                        radius: 2,
                        states: {
                            hover: {
                                enabled: true
                            }
                        }
                    }
                }
            },
            series: [{
                type: 'area',
                name: '肌肉疲勞',
                data: [0, 8, 12, 15, 21, 28, 32, 45, 54, 60, 68, 73, 80, 84, 86, 90, 92, 94, 95, 97, 98, 100, 100, 100]
            }],
            credits: {
                enabled: false
            }
        }
    }
    //訓練成效-出力百分比
    if (ChartName == "pmvc") {
        chartOption = {
            chart: {
                tpye: 'spline'
            },
            colors: ['#FF9A00', '#249400'],
            title: {
                text: '肌肉出力百分比'
            },
            tooltip: {
                valueSuffix: '%'
            },
            xAxis: {
                categories: ['1-1', '1-2', '1-3', '1-4', '1-5', '1-6', '1-7', '1-8', '2-1', '2-2', '2-3', '2-4', '2-5', '2-6', '2-7', '2-8', '3-1', '3-2', '3-3', '3-4', '3-5', '3-6', '3-7', '3-8']
            },
            yAxis: {
                title: {
                    text: null
                },
                min: 0,
                max: 100,
                tickInterval: 20,
                labels: {
                    format: '{value}%'
                }
            },
            legend: {
                enabled: true
            },
            plotOptions: {
                area: {
                    marker: {
                        enabled: false,
                        symbol: 'circle',
                        radius: 2,
                        states: {
                            hover: {
                                enabled: true
                            }
                        }
                    }
                }
            },
            series: [{
                type: 'spline',
                name: '向心',
                data: [65, 72, 80, 80, 79, 82, 78, 81, 75, 73, 78, 82, 80, 84, 86, 78, 75, 72, 80, 71, 75, 71, 69, 68]
            }, {
                type: 'spline',
                name: '離心',
                data: [45, 68, 72, 68, 81, 74, 81, 73, 62, 68, 81, 75, 68, 74, 88, 81, 65, 84, 77, 75, 67, 74, 80, 75]
            }],
            credits: {
                enabled: false
            }
        }
    }
    if (ChartName == "result") {
        //$('#'+targetId).html('<div class="progress-title"><p>本次訓練成效</p></div><div class="block-progress"><div class="progress progress-striped active"><div class="progress-bar progress-bar-theme" role="progressbar" aria-valuenow="40" aria-valuemin="0" aria-valuemax="100" style="width: 82%"><span class="pull-right">82%</span></div></div></div>')
        chartOption = {
            chart: {
                type: 'bar'
            },
            colors: ['#FF9A00', '#249400'],
            title: {
                text: '訓練姿勢之訓練成效'
            },
            xAxis: {
                categories: ['伏地挺身', '啞鈴單手後屈伸', '啞鈴跨步蹲舉', '立姿側平舉', '仰臥腿上舉', 'V型仰臥起坐'],
                title: {
                    text: null
                }
            },
            legend: {
                enabled: true
            },
            yAxis: {
                min: 0,
                title: {
                    text: null,
                    align: 'high'
                },
                labels: {
                    overflow: 'justify'
                }
            },
            tooltip: {
                valueSuffix: '%'
            },
            plotOptions: {
                bar: {
                    dataLabels: {
                        enabled: true,
                        format: '{point.y}%'
                    }
                }
            },
            credits: {
                enabled: false
            },
            series: [{
                name: '肌肉疲勞',
                data: [72, 62, 85, 92, 68, 79]
            }, {
                name: '肌肉出力',
                data: [90, 82, 87, 90, 85, 78]
            }]
        };
    }
    //個人素質
    if (ChartName == "status") {
        chartOption = {
            chart: {
                polar: true,
                type: 'line'
            },
            colors: ['#f45b5b'],
            title: {
                text: null,
                x: -80
            },
            pane: {
                size: '70%'
            },
            legend: {
                enabled: false
            },
            xAxis: {
                categories: ['肌群均衡', '肌力', '肌耐力', '訓練成效', '超負荷', '體脂率'],
                tickmarkPlacement: 'on',
                lineWidth: 0,
                padding: -10
            },
            yAxis: {
                gridLineInterpolation: 'polygon',
                lineWidth: 0,
                min: 0
            },
            tooltip: {
                shared: true,
                pointFormat: '<span style="color:{series.color}"><b>{point.y}%</b><br/>'
            },
            series: [{
                name: 'SELF',
                type: 'area',
                data: [32, 71, 53, 42, 68, 85],
                pointPlacement: 'on'
            }],
            credits: {
                enabled: false
            }
        };
    }
    //訓練成效
    if (ChartName == "training") {
        chartOption = {
            chart: {
                type: 'column'
            },
            colors: ['#FF9A00'],
            title: {
                text: null,
                x: -20 //center
            },
            legend: {
                enabled: false
            },
            xAxis: {
                categories: ['2/17', '2/19', '2/20', '2/22', '2/23', '2/25']
            },
            yAxis: {
                title: {
                    text: null
                },
                labels: {
                    format: '{value}%'
                },
                plotLines: [{
                    value: 0,
                    width: 1,
                    color: '#808080'
                }]
            },
            tooltip: {
                valueSuffix: '%'
            },
            series: [{
                name: '訓練成效',
                data: [66, 82, 71, 60, 0, 84]
            }],
            credits: {
                enabled: false
            }
        };
    }
    //肌肉成長
    if (ChartName == "grow") {
        chartOption = {
            chart: {
                type: 'spline'
            },
            colors: ['#FF9A00'],
            title: {
                text: null,
                x: -20 //center
            },
            legend: {
                enabled: false
            },
            xAxis: {
                categories: ['2/17', '2/19', '2/20', '2/22', '2/23', '2/25']
            },
            yAxis: {
                title: {
                    text: null
                },
                labels: {
                    format: '{value}%'
                },
                plotLines: [{
                    value: 0,
                    width: 1,
                    color: '#808080'
                }]
            },
            tooltip: {
                valueSuffix: '%'
            },
            series: [{
                name: '肌肉成長',
                data: [23, 28, 32, 38, 42, 49]
            }],
            credits: {
                enabled: false
            }
        };
    }
    //體脂縮圖
    if (ChartName == "bodyfatThumb") {
        chartOption = {
            chart: {
                type: 'spline'
            },
            colors: ['#f45b5b'],
            title: {
                text: null,
                x: -20 //center
            },
            legend: {
                enabled: false
            },
            xAxis: {
                categories: ['2/17', '2/19', '2/20', '2/22', '2/23', '2/25']
            },
            yAxis: {
                title: {
                    text: null
                },
                labels: {
                    format: '{value}%'
                },
                plotLines: [{
                    value: 0,
                    width: 1,
                    color: '#808080'
                }]
            },
            tooltip: {
                valueSuffix: '%'
            },
            series: [{
                name: '體脂率',
                data: [22.4, 21.9, 21.5, 21.1, 20.8, 20.6]
            }],
            credits: {
                enabled: false
            }
        };
    }
    //落點分析縮圖
    if (ChartName == "dotThumb") {
        chartOption = {
            chart: {
                type: 'scatter',
                zoomType: 'xy'
            },
            title: {
                text: '會員身高體重散布狀況'
            },
            legend: {
                layout: 'vertical',
                align: 'left',
                verticalAlign: 'top',
                floating: true,
                backgroundColor: (Highcharts.theme && Highcharts.theme.legendBackgroundColor) || '#FFFFFF',
                borderWidth: 1,
                y: 25,
                x: 50
            },
            plotOptions: {
                scatter: {
                    marker: {
                        radius: 5,
                        states: {
                            hover: {
                                enabled: true,
                                lineColor: 'rgb(100,100,100)'
                            }
                        }
                    },
                    states: {
                        hover: {
                            marker: {
                                enabled: false
                            }
                        }
                    },
                    tooltip: {
                        headerFormat: '<b>{series.name}</b><br>',
                        pointFormat: '{point.x} 公分, {point.y} 公斤'
                    }
                }
            },
            xAxis: {
                title: {
                    enabled: true,
                    text: '身高'
                },
                labels: {
                    enabled: true
                },
                startOnTick: true,
                endOnTick: true,
                showLastLabel: true,
            },
            yAxis: {
                title: {
                    text: '體重'
                },
                labels: {
                    enabled: true
                },
                allowDecimals: false,
                endOnTick: false,
                max: 120,
                min: 30,
                ordinal: false,
                startOnTick: false,
                minTickInterval: 10

            },
            series: [{
                name: '女性',
                color: 'rgba(223, 83, 83, .5)',
                data: [
                    [161.2, 51.6],
                    [167.5, 59.0],
                    [159.5, 49.2],
                    [157.0, 63.0],
                    [155.8, 53.6],
                    [170.0, 59.0],
                    [159.1, 47.6],
                    [166.0, 69.8],
                    [176.2, 66.8],
                    [160.2, 75.2],
                    [172.5, 55.2],
                    [170.9, 54.2],
                    [172.9, 62.5],
                    [153.4, 42.0],
                    [160.0, 50.0],
                    [147.2, 49.8],
                    [168.2, 49.2],
                    [175.0, 73.2],
                    [157.0, 47.8],
                    [167.6, 68.8],
                    [159.5, 50.6],
                    [175.0, 82.5],
                    [166.8, 57.2],
                    [176.5, 87.8],
                    [170.2, 72.8],
                    [174.0, 54.5],
                    [173.0, 59.8],
                    [179.9, 67.3],
                    [170.5, 67.8],
                    [160.0, 47.0],
                    [154.4, 46.2],
                    [162.0, 55.0],
                    [176.5, 83.0],
                    [160.0, 54.4],
                    [152.0, 45.8],
                    [162.1, 53.6],
                    [170.0, 73.2],
                    [160.2, 52.1],
                    [161.3, 67.9],
                    [166.4, 56.6],
                    [168.9, 62.3],
                    [163.8, 58.5],
                    [167.6, 54.5],
                    [160.0, 50.2],
                    [161.3, 60.3],
                    [167.6, 58.3],
                    [165.1, 56.2],
                    [160.0, 50.2],
                    [170.0, 72.9],
                    [157.5, 59.8],
                    [167.6, 61.0],
                    [160.7, 69.1],
                    [163.2, 55.9],
                    [152.4, 46.5],
                    [157.5, 54.3],
                    [168.3, 54.8],
                    [180.3, 60.7],
                    [165.5, 60.0],
                    [165.0, 62.0],
                    [164.5, 60.3],
                    [156.0, 52.7],
                    [160.0, 74.3],
                    [163.0, 62.0],
                    [165.7, 73.1],
                    [161.0, 80.0],
                    [162.0, 54.7],
                    [166.0, 53.2],
                    [174.0, 75.7],
                    [172.7, 61.1],
                    [167.6, 55.7],
                    [151.1, 48.7],
                    [164.5, 52.3],
                    [163.5, 50.0],
                    [152.0, 59.3],
                    [169.0, 62.5],
                    [164.0, 55.7],
                    [161.2, 54.8],
                    [155.0, 45.9],
                    [170.0, 70.6],
                    [176.2, 67.2],
                    [170.0, 69.4],
                    [162.5, 58.2],
                    [170.3, 64.8],
                    [164.1, 71.6],
                    [169.5, 52.8],
                    [163.2, 59.8],
                    [154.5, 49.0],
                    [159.8, 50.0],
                    [173.2, 69.2],
                    [170.0, 55.9],
                    [161.4, 63.4],
                    [169.0, 58.2],
                    [166.2, 58.6],
                    [159.4, 45.7],
                    [162.5, 52.2],
                    [159.0, 48.6],
                    [162.8, 57.8],
                    [159.0, 55.6],
                    [179.8, 66.8],
                    [162.9, 59.4],
                    [161.0, 53.6],
                    [151.1, 73.2],
                    [168.2, 53.4],
                    [168.9, 69.0],
                    [173.2, 58.4],
                    [171.8, 56.2],
                    [178.0, 70.6],
                    [164.3, 59.8],
                    [163.0, 72.0],
                    [168.5, 65.2],
                    [166.8, 56.6],
                    [172.7, 105.2],
                    [163.5, 51.8],
                    [169.4, 63.4],
                    [167.8, 59.0],
                    [159.5, 47.6],
                    [167.6, 63.0],
                    [161.2, 55.2],
                    [160.0, 45.0],
                    [163.2, 54.0],
                    [162.2, 50.2],
                    [161.3, 60.2],
                    [149.5, 44.8],
                    [157.5, 58.8],
                    [163.2, 56.4],
                    [172.7, 62.0],
                    [155.0, 49.2],
                    [156.5, 67.2],
                    [164.0, 53.8],
                    [160.9, 54.4],
                    [162.8, 58.0],
                    [167.0, 59.8],
                    [160.0, 54.8],
                    [160.0, 43.2],
                    [168.9, 60.5],
                    [158.2, 46.4],
                    [156.0, 64.4]
                ]

            }, {
                name: '男性',
                color: 'rgba(119, 152, 191, .5)',
                data: [
                    [174.0, 65.6],
                    [175.3, 71.8],
                    [193.5, 80.7],
                    [186.5, 72.6],
                    [187.2, 78.8],
                    [181.5, 74.8],
                    [184.0, 86.4],
                    [184.5, 78.4],
                    [175.0, 62.0],
                    [184.0, 81.6],
                    [180.0, 76.6],
                    [177.8, 83.6],
                    [192.0, 90.0],
                    [176.0, 74.6],
                    [174.0, 71.0],
                    [184.0, 79.6],
                    [192.7, 93.8],
                    [171.5, 70.0],
                    [173.0, 72.4],
                    [176.0, 85.9],
                    [176.0, 78.8],
                    [180.5, 77.8],
                    [172.7, 66.2],
                    [176.0, 86.4],
                    [173.5, 81.8],
                    [178.0, 89.6],
                    [180.3, 82.8],
                    [180.3, 76.4],
                    [164.5, 63.2],
                    [173.0, 60.9],
                    [183.5, 74.8],
                    [175.5, 70.0],
                    [188.0, 72.4],
                    [189.2, 84.1],
                    [172.8, 69.1],
                    [170.0, 59.5],
                    [182.0, 67.2],
                    [170.0, 61.3],
                    [177.8, 68.6],
                    [184.2, 80.1],
                    [186.7, 87.8],
                    [171.4, 84.7],
                    [172.7, 73.4],
                    [175.3, 72.1],
                    [180.3, 82.6],
                    [182.9, 88.7],
                    [188.0, 84.1],
                    [177.2, 94.1],
                    [172.1, 74.9],
                    [167.0, 59.1],
                    [169.5, 75.6],
                    [174.0, 86.2],
                    [172.7, 75.3],
                    [182.2, 87.1],
                    [164.1, 55.2],
                    [163.0, 57.0],
                    [171.5, 61.4],
                    [184.2, 76.8],
                    [174.0, 86.8],
                    [174.0, 72.2],
                    [177.0, 71.6],
                    [186.0, 84.8],
                    [167.0, 68.2],
                    [171.8, 66.1],
                    [182.0, 72.0],
                    [167.0, 64.6],
                    [177.8, 74.8],
                    [164.5, 70.0],
                    [192.0, 101.6],
                    [175.5, 63.2],
                    [171.2, 79.1],
                    [181.6, 78.9],
                    [167.4, 67.7],
                    [181.1, 66.0],
                    [177.0, 68.2],
                    [174.5, 63.9],
                    [177.5, 72.0],
                    [170.5, 56.8],
                    [182.4, 74.5],
                    [197.1, 90.9],
                    [180.1, 93.0],
                    [175.5, 80.9],
                    [180.6, 72.7],
                    [184.4, 68.0],
                    [175.5, 70.9],
                    [180.6, 72.5],
                    [177.0, 72.5],
                    [177.1, 83.4],
                    [181.6, 75.5],
                    [176.5, 73.0],
                    [175.0, 70.2],
                    [174.0, 73.4],
                    [165.1, 70.5],
                    [177.0, 68.9],
                    [192.0, 102.3],
                    [176.5, 68.4],
                    [169.4, 65.9],
                    [182.1, 75.7],
                    [179.8, 84.5],
                    [175.3, 87.7],
                    [184.9, 86.4],
                    [177.3, 73.2],
                    [167.4, 53.9],
                    [178.1, 72.0],
                    [168.9, 55.5],
                    [157.2, 58.4],
                    [180.3, 83.2],
                    [170.2, 72.7],
                    [177.8, 64.1],
                    [172.7, 72.3],
                    [165.1, 65.0],
                    [186.7, 86.4],
                    [165.1, 65.0],
                    [174.0, 88.6],
                    [175.3, 84.1],
                    [185.4, 66.8],
                    [177.8, 75.5],
                    [180.3, 93.2],
                    [180.3, 82.7],
                    [177.8, 58.0],
                    [177.8, 79.5],
                    [177.8, 78.6],
                    [177.8, 71.8],
                    [177.8, 116.4]
                ]
            }],
            credits: {
                enabled: false
            }
        };
    }
    return chartOption;
}
