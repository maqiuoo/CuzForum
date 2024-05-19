import React, { Component } from 'react';
import { withRouter } from 'react-router-dom';

class RadarChart extends Component {
    chartRef = React.createRef();
    async componentDidMount() {

        window.echarts.registerTheme('them13', { "color": ["#9b8bba", "#e098c7", "#8fd3e8", "#71669e", "#cc70af", "#7cb4cc"], "line": { "itemStyle": { "borderWidth": "2" }, "lineStyle": { "width": "3" }, "symbolSize": "7", "symbol": "circle", "smooth": false }, "radar": { "itemStyle": { "borderWidth": "2" }, "lineStyle": { "width": "3" }, "symbolSize": "7", "symbol": "circle", "smooth": false }, "bar": { "itemStyle": { "barBorderWidth": 0, "barBorderColor": "#ccc" } }, "pie": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" } }, "scatter": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" } }, "boxplot": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" } }, "parallel": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" } }, "sankey": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" } }, "funnel": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" } }, "gauge": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" } }, "candlestick": { "itemStyle": { "color": "#e098c7", "color0": "transparent", "borderColor": "#e098c7", "borderColor0": "#8fd3e8", "borderWidth": "2" } }, "graph": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" }, "lineStyle": { "width": 1, "color": "#aaaaaa" }, "symbolSize": "7", "symbol": "circle", "smooth": true, "color": ["#9b8bba", "#e098c7", "#8fd3e8", "#71669e", "#cc70af", "#7cb4cc"], "label": { "color": "#eeeeee" } }, "map": { "itemStyle": { "areaColor": "#eee", "borderColor": "#444", "borderWidth": 0.5 }, "label": { "color": "#000" }, "emphasis": { "itemStyle": { "areaColor": "#e098c7", "borderColor": "#444", "borderWidth": 1 }, "label": { "color": "#ffffff" } } }, "geo": { "itemStyle": { "areaColor": "#eee", "borderColor": "#444", "borderWidth": 0.5 }, "label": { "color": "#000" }, "emphasis": { "itemStyle": { "areaColor": "#e098c7", "borderColor": "#444", "borderWidth": 1 }, "label": { "color": "#ffffff" } } }, "categoryAxis": { "axisLine": { "show": true, "lineStyle": { "color": "#cccccc" } }, "axisTick": { "show": false, "lineStyle": { "color": "#333" } }, "axisLabel": { "show": true, "color": "#cccccc" }, "splitLine": { "show": false, "lineStyle": { "color": ["#eeeeee", "#333333"] } }, "splitArea": { "show": true, "areaStyle": { "color": ["rgba(250,250,250,0.05)", "rgba(200,200,200,0.02)"] } } }, "valueAxis": { "axisLine": { "show": true, "lineStyle": { "color": "#cccccc" } }, "axisTick": { "show": false, "lineStyle": { "color": "#333" } }, "axisLabel": { "show": true, "color": "#cccccc" }, "splitLine": { "show": false, "lineStyle": { "color": ["#eeeeee", "#333333"] } }, "splitArea": { "show": true, "areaStyle": { "color": ["rgba(250,250,250,0.05)", "rgba(200,200,200,0.02)"] } } }, "logAxis": { "axisLine": { "show": true, "lineStyle": { "color": "#cccccc" } }, "axisTick": { "show": false, "lineStyle": { "color": "#333" } }, "axisLabel": { "show": true, "color": "#cccccc" }, "splitLine": { "show": false, "lineStyle": { "color": ["#eeeeee", "#333333"] } }, "splitArea": { "show": true, "areaStyle": { "color": ["rgba(250,250,250,0.05)", "rgba(200,200,200,0.02)"] } } }, "timeAxis": { "axisLine": { "show": true, "lineStyle": { "color": "#cccccc" } }, "axisTick": { "show": false, "lineStyle": { "color": "#333" } }, "axisLabel": { "show": true, "color": "#cccccc" }, "splitLine": { "show": false, "lineStyle": { "color": ["#eeeeee", "#333333"] } }, "splitArea": { "show": true, "areaStyle": { "color": ["rgba(250,250,250,0.05)", "rgba(200,200,200,0.02)"] } } }, "toolbox": { "iconStyle": { "borderColor": "#999999" }, "emphasis": { "iconStyle": { "borderColor": "#666666" } } }, "legend": { "textStyle": { "color": "#cccccc" } }, "tooltip": { "axisPointer": { "lineStyle": { "color": "#cccccc", "width": 1 }, "crossStyle": { "color": "#cccccc", "width": 1 } } }, "timeline": { "lineStyle": { "color": "#8fd3e8", "width": 1 }, "itemStyle": { "color": "#8fd3e8", "borderWidth": 1 }, "controlStyle": { "color": "#8fd3e8", "borderColor": "#8fd3e8", "borderWidth": 0.5 }, "checkpointStyle": { "color": "#8fd3e8", "borderColor": "#8a7ca8" }, "label": { "color": "#8fd3e8" }, "emphasis": { "itemStyle": { "color": "#8fd3e8" }, "controlStyle": { "color": "#8fd3e8", "borderColor": "#8fd3e8", "borderWidth": 0.5 }, "label": { "color": "#8fd3e8" } } }, "visualMap": { "color": ["#8a7ca8", "#e098c7", "#cceffa"] }, "dataZoom": { "backgroundColor": "rgba(0,0,0,0)", "dataBackgroundColor": "rgba(255,255,255,0.3)", "fillerColor": "rgba(167,183,204,0.4)", "handleColor": "#a7b7cc", "handleSize": "100%", "textStyle": { "color": "#333" } }, "markPoint": { "label": { "color": "#eeeeee" }, "emphasis": { "label": { "color": "#eeeeee" } } } })
        const chart = window.echarts.init(this.chartRef.current, 'them13')
        const year = await this.getData(this.props.year);
        // console.log(year);
        const yearV = await year[0];
        const yearJ = await year[1];
        // console.log(yearV);
        // console.log(yearJ);
        const option = {
            title: {
                show: true,
                text: `${this.props.year}浙传就业比对`,
                textStyle: {
                    fontFamily: "Microsoft YaHei",
                    fontWeight: 400
                },
                left: "center",
                top: "top"
            },
            tooltip: {
                show: true,
                trigger: "item",
                axisPointer: {
                    type: "none"
                }
            },
            series: [{
                name: "series-1",
                type: "radar",
                data: [{
                    name: "就业人数",
                    value: yearV,
                    label: {
                        show: true,
                        position: "top",
                        fontSize: 14,
                        formatter: "{c}"
                    },
                    lineStyle: {
                        type: "solid",
                        width: 2
                    },
                    areaStyle: {
                        opacity: 0.7
                    }
                }]
            }],
            radar: {
                indicator: yearJ,
                center: ["50%", "54%"],
                shape: "polygon",
                radius: "73%",
                startAngle: 90,
                axisLine: {
                    symbol: ["none"],
                    symbolSize: [10, 15],
                    lineStyle: {
                        type: "solid",
                        width: 2,
                    }
                },
                splitLine: {
                    lineStyle: {
                        type: "solid",
                        width: 2,
                    }
                },
                splitArea: {
                    areaStyle: {
                        color: ["rgba(250,250,250,0.3)", "rgba(200,200,200,0.3)"]
                    }
                },
                axisName: {
                    show: true,
                    fontSize: 14,
                    fontWeight: 400
                }
            },
        };
        chart.setOption(option);
        this.chart = chart;

    }

    componentDidUpdate(prevProps) {
        // 检查year或type是否有变化
        if (prevProps.year !== this.props.year) {
            this.updateChart();
        }
    }

    async getData(year) {
        // eslint-disable-next-line
        const yearV2021 = ["110", "68", "40", "218", "91", "47", "71", "83", "67", "35", "394", "48", "17", "73", "138", "58", "56", "108", "38", "122", "52", "60", "37", "531", "132", "39", "78", "249", "37", "52", "83", "72", "37", "81", "85", "447", "33", "30", "17", "73", "67", "220", "24", "38", "29", "89", "62", "32", "32", "306", "31", "67", "29", "94", "33", "254", "38", "43", "95", "39", "88", "303", "26", "23", "25", "71", "145", "3205", "119"];
        // eslint-disable-next-line
        const yearJ2021 = [{
            name: "播音与主持艺术",
            max: "111"
        }, {
            name: "播音与主持艺术（礼仪文化）",
            max: "71"
        }, {
            name: "播音与主持艺术（影视配音）",
            max: "41"
        }, {
            name: "播音主持艺术学院合计",
            max: "223"
        }, {
            name: "广播电视编导",
            max: "98"
        }, {
            name: "广播电视编导（文艺编导）",
            max: "49"
        }, {
            name: "录音艺术",
            max: "73"
        }, {
            name: "影视摄影与制作（电视节目制作）",
            max: "84"
        }, {
            name: "影视摄影与制作（电视摄像）",
            max: "69"
        }, {
            name: "影视摄影与制作（照明艺术）",
            max: "35"
        }, {
            name: "电视艺术学院合计",
            max: "408"
        }, {
            name: "表演",
            max: "48"
        }, {
            name: "戏剧影视导演",
            max: "17"
        }, {
            name: "影视摄影与制作（电影制作）",
            max: "78"
        }, {
            name: "华策电影学院合计",
            max: "143"
        }, {
            name: "电子科学与技术",
            max: "59"
        }, {
            name: "电子信息工程",
            max: "59"
        }, {
            name: "广播电视工程",
            max: "110"
        }, {
            name: "软件工程",
            max: "39"
        }, {
            name: "数字媒体技术",
            max: "131"
        }, {
            name: "通信工程",
            max: "54"
        }, {
            name: "网络工程",
            max: "65"
        }, {
            name: "信息管理与信息系统",
            max: "42"
        }, {
            name: "媒体工程学院合计",
            max: "559"
        }, {
            name: "动画",
            max: "153"
        }, {
            name: "动画（漫插画）",
            max: "43"
        }, {
            name: "数字媒体艺术",
            max: "79"
        }, {
            name: "动画与数字艺术学院合计",
            max: "275"
        }, {
            name: "公共关系学",
            max: "37"
        }, {
            name: "广播电视编导（媒体创意）",
            max: "52"
        }, {
            name: "广告学",
            max: "83"
        }, {
            name: "会展经济与管理",
            max: "76"
        }, {
            name: "经济学",
            max: "39"
        }, {
            name: "网络与新媒体",
            max: "84"
        }, {
            name: "文化产业管理",
            max: "92"
        }, {
            name: "文化创意与管理学院合计",
            max: "463"
        }, {
            name: "播音与主持艺术（法汉双语播音）",
            max: "35"
        }, {
            name: "播音与主持艺术(日汉双语播音)",
            max: "30"
        }, {
            name: "播音与主持艺术（双语播音）",
            max: "17"
        }, {
            name: "播音与主持艺术(英汉双语播音)",
            max: "76"
        }, {
            name: "英语",
            max: "71"
        }, {
            name: "国际文化传播学院（国际教育学院）合计",
            max: "229"
        }, {
            name: "产品设计",
            max: "27"
        }, {
            name: "服装与服饰设计",
            max: "39"
        }, {
            name: "环境设计",
            max: "31"
        }, {
            name: "摄影",
            max: "90"
        }, {
            name: "视觉传达设计",
            max: "64"
        }, {
            name: "戏剧影视美术设计",
            max: "32"
        }, {
            name: "戏剧影视美术设计(人物形象设计)",
            max: "34"
        }, {
            name: "设计艺术学院合计",
            max: "317"
        }, {
            name: "汉语国际教育",
            max: "35"
        }, {
            name: "汉语言文学",
            max: "78"
        }, {
            name: "秘书学",
            max: "34"
        }, {
            name: "戏剧影视文学",
            max: "98"
        }, {
            name: "戏剧影视文学(编剧与策划)",
            max: "37"
        }, {
            name: "文学院合计",
            max: "282"
        }, {
            name: "编辑出版学",
            max: "43"
        }, {
            name: "传播学",
            max: "44"
        }, {
            name: "广播电视学",
            max: "98"
        }, {
            name: "数字出版学",
            max: "39"
        }, {
            name: "新闻学",
            max: "91"
        }, {
            name: "新闻与传播学院合计",
            max: "315"
        }, {
            name: "舞蹈编导",
            max: "26"
        }, {
            name: "舞蹈编导（音乐剧）",
            max: "23"
        }, {
            name: "艺术与科技",
            max: "26"
        }, {
            name: "音乐表演",
            max: "82"
        }, {
            name: "音乐学院合计",
            max: "157"
        }, {
            name: "本科生院合计",
            max: "3371"
        }, {
            name: "新闻与传播（研究生）",
            max: "121"
        }]
        // eslint-disable-next-line
        const yearV2022 = [152, 65, 46, 263, 161, 78, 73, 97, 409, 51, 18, 86, 155, 45, 51, 103, 27, 76, 121, 21, 71, 515, 145, 34, 81, 260, 39, 128, 93, 46, 93, 92, 491, 99, 14, 84, 197, 26, 50, 82, 61, 55, 274, 33, 61, 25, 112, 231, 92, 42, 76, 26, 79, 315, 27, 29, 20, 74, 8, 158, 3358]
        // eslint-disable-next-line
        const yearJ2022 = [
            {
                "name": "播音与主持艺术",
                "max": 163
            },
            {
                "name": "播音与主持艺术（礼仪文化）",
                "max": 71
            },
            {
                "name": "播音与主持艺术（影视配音）",
                "max": 48
            },
            {
                "name": "播音主持艺术学院合计",
                "max": 282
            },
            {
                "name": "广播电视编导",
                "max": 170
            },
            {
                "name": "录音艺术",
                "max": 78
            },
            {
                "name": "影视摄影与制作（电视节目制作）",
                "max": 76
            },
            {
                "name": "影视摄影与制作（电视摄像）",
                "max": 100
            },
            {
                "name": "电视艺术学院合计",
                "max": 424
            },
            {
                "name": "表演",
                "max": 52
            },
            {
                "name": "戏剧影视导演",
                "max": 19
            },
            {
                "name": "影视摄影与制作（电影制作）",
                "max": 98
            },
            {
                "name": "华策电影学院合计",
                "max": 169
            },
            {
                "name": "电子科学与技术",
                "max": 52
            },
            {
                "name": "电子信息工程",
                "max": 52
            },
            {
                "name": "广播电视工程",
                "max": 107
            },
            {
                "name": "广播电视工程（中广班）",
                "max": 30
            },
            {
                "name": "软件工程",
                "max": 79
            },
            {
                "name": "数字媒体技术",
                "max": 134
            },
            {
                "name": "通信工程",
                "max": 22
            },
            {
                "name": "网络工程",
                "max": 76
            },
            {
                "name": "媒体工程学院合计",
                "max": 552
            },
            {
                "name": "动画",
                "max": 160
            },
            {
                "name": "动画（漫插画）",
                "max": 41
            },
            {
                "name": "数字媒体艺术",
                "max": 84
            },
            {
                "name": "动画与数字艺术学院合计",
                "max": 285
            },
            {
                "name": "广播电视编导（媒体创意）",
                "max": 43
            },
            {
                "name": "广告学",
                "max": 138
            },
            {
                "name": "会展经济与管理",
                "max": 97
            },
            {
                "name": "经济学",
                "max": 46
            },
            {
                "name": "网络与新媒体",
                "max": 99
            },
            {
                "name": "文化产业管理",
                "max": 107
            },
            {
                "name": "文化创意与管理学院合计",
                "max": 530
            },
            {
                "name": "播音与主持艺术（双语播音）",
                "max": 104
            },
            {
                "name": "播音与主持艺术（双语特色）",
                "max": 15
            },
            {
                "name": "英语",
                "max": 92
            },
            {
                "name": "国际文化传播学院（国际教育学院）合计",
                "max": 211
            },
            {
                "name": "产品设计",
                "max": 29
            },
            {
                "name": "服装与服饰设计",
                "max": 52
            },
            {
                "name": "摄影",
                "max": 88
            },
            {
                "name": "视觉传达设计",
                "max": 63
            },
            {
                "name": "戏剧影视美术设计",
                "max": 58
            },
            {
                "name": "设计艺术学院合计",
                "max": 290
            },
            {
                "name": "汉语国际教育",
                "max": 34
            },
            {
                "name": "汉语言文学",
                "max": 73
            },
            {
                "name": "秘书学",
                "max": 27
            },
            {
                "name": "戏剧影视文学",
                "max": 125
            },
            {
                "name": "文学院合计",
                "max": 259
            },
            {
                "name": "编辑出版学",
                "max": 94
            },
            {
                "name": "传播学",
                "max": 42
            },
            {
                "name": "广播电视学",
                "max": 89
            },
            {
                "name": "广播电视学（飘萍班）",
                "max": 26
            },
            {
                "name": "新闻学",
                "max": 83
            },
            {
                "name": "新闻与传播学院合计",
                "max": 334
            },
            {
                "name": "舞蹈编导",
                "max": 28
            },
            {
                "name": "舞蹈编导（音乐剧）",
                "max": 30
            },
            {
                "name": "艺术与科技",
                "max": 20
            },
            {
                "name": "音乐表演",
                "max": 87
            },
            {
                "name": "音乐表演（舞蹈）（专科）",
                "max": 8
            },
            {
                "name": "音乐学院合计",
                "max": 173
            },
            {
                "name": "本科生院合计",
                "max": 3619
            }
        ];
        // eslint-disable-next-line
        const yearV2023 = [44, 74, 116, 26, 260, 99, 72, 127, 75, 373, 125, 74, 36, 235, 99, 82, 13, 194, 19, 53, 90, 162, 125, 47, 71, 21, 42, 100, 27, 63, 496, 44, 54, 87, 58, 52, 295, 81, 125, 45, 91, 63, 95, 500, 104, 29, 72, 28, 233, 41, 83, 87, 82, 26, 319, 31, 18, 27, 83, 7, 166, 3233, 196]
        // eslint-disable-next-line
        const yearJ2023 = [
            {
                "name": "播音与主持艺术（影视配音）",
                "max": 44
            },
            {
                "name": "播音与主持艺术（礼仪文化）",
                "max": 77
            },
            {
                "name": "播音与主持艺术",
                "max": 121
            },
            {
                "name": "播音与主持艺术（体育评论解说）",
                "max": 26
            },
            {
                "name": "播音主持艺术学院合计",
                "max": 268
            },
            {
                "name": "影视摄影与制作（电视摄像）",
                "max": 102
            },
            {
                "name": "影视摄影与制作（电视节目制作）",
                "max": 75
            },
            {
                "name": "广播电视编导",
                "max": 134
            },
            {
                "name": "录音艺术",
                "max": 78
            },
            {
                "name": "电视艺术学院合计",
                "max": 389
            },
            {
                "name": "动画",
                "max": 133
            },
            {
                "name": "数字媒体艺术",
                "max": 82
            },
            {
                "name": "动画（漫插画）",
                "max": 39
            },
            {
                "name": "动画与数字艺术学院合计",
                "max": 254
            },
            {
                "name": "播音与主持艺术（双语播音）",
                "max": 100
            },
            {
                "name": "英语",
                "max": 83
            },
            {
                "name": "播音与主持艺术（双语特色）",
                "max": 13
            },
            {
                "name": "国际文化传播学院合计",
                "max": 196
            },
            {
                "name": "戏剧影视导演",
                "max": 19
            },
            {
                "name": "表演",
                "max": 54
            },
            {
                "name": "影视摄影与制作（电影摄影与制作）",
                "max": 95
            },
            {
                "name": "华策电影学院合计",
                "max": 168
            },
            {
                "name": "数字媒体技术",
                "max": 136
            },
            {
                "name": "电子科学与技术",
                "max": 49
            },
            {
                "name": "网络工程",
                "max": 72
            },
            {
                "name": "通信工程",
                "max": 23
            },
            {
                "name": "电子信息工程",
                "max": 46
            },
            {
                "name": "广播电视工程",
                "max": 105
            },
            {
                "name": "广播电视工程（中广班）",
                "max": 30
            },
            {
                "name": "软件工程",
                "max": 70
            },
            {
                "name": "媒体工程学院合计",
                "max": 531
            },
            {
                "name": "产品设计",
                "max": 45
            },
            {
                "name": "戏剧影视美术设计",
                "max": 57
            },
            {
                "name": "摄影",
                "max": 87
            },
            {
                "name": "视觉传达设计",
                "max": 60
            },
            {
                "name": "服装与服饰设计（中外合作办学）",
                "max": 55
            },
            {
                "name": "设计艺术学院合计",
                "max": 304
            },
            {
                "name": "会展经济与管理",
                "max": 86
            },
            {
                "name": "广告学",
                "max": 133
            },
            {
                "name": "广播电视编导（媒体创意）",
                "max": 46
            },
            {
                "name": "文化产业管理",
                "max": 94
            },
            {
                "name": "经济学",
                "max": 67
            },
            {
                "name": "网络与新媒体",
                "max": 99
            },
            {
                "name": "文化创意与管理学院合计",
                "max": 525
            },
            {
                "name": "戏剧影视文学",
                "max": 109
            },
            {
                "name": "汉语国际教育",
                "max": 30
            },
            {
                "name": "汉语言文学",
                "max": 73
            },
            {
                "name": "秘书学",
                "max": 30
            },
            {
                "name": "文学院合计",
                "max": 242
            },
            {
                "name": "传播学",
                "max": 42
            },
            {
                "name": "广播电视学",
                "max": 91
            },
            {
                "name": "新闻学",
                "max": 89
            },
            {
                "name": "编辑出版学",
                "max": 87
            },
            {
                "name": "广播电视学（飘萍班）",
                "max": 26
            },
            {
                "name": "新闻与传播学院合计",
                "max": 335
            },
            {
                "name": "舞蹈编导",
                "max": 31
            },
            {
                "name": "舞蹈编导（音乐剧）",
                "max": 20
            },
            {
                "name": "艺术与科技",
                "max": 29
            },
            {
                "name": "音乐表演",
                "max": 86
            },
            {
                "name": "音乐表演（舞蹈专科）",
                "max": 7
            },
            {
                "name": "音乐学院合计",
                "max": 173
            },
            {
                "name": "本科生院合计",
                "max": 3385
            },
            {
                "name": "新闻与传播（研究生）",
                "max": 200
            }
        ];
        // eslint-disable-next-line
        const yearV = eval(`yearV${year}`);
        // eslint-disable-next-line
        const yearJ = eval(`yearJ${year}`);

        return [yearV, yearJ];
    }

    async updateChart() {
        const { year } = await this.props;
        const newyear = await this.getData(year);
        const yearV = await newyear[0];
        const yearJ = await newyear[1];

        this.chart.setOption({
            title: {
                text: `${year}浙传就业比对`,
            },
            series: [{
                data:  [{
                    name: "就业人数",
                    value: yearV,
                    label: {
                        show: true,
                        position: "top",
                        fontSize: 14,
                        formatter: "{c}"
                    },
                    lineStyle: {
                        type: "solid",
                        width: 2
                    },
                    areaStyle: {
                        opacity: 0.7
                    }
                }]
            }],
            radar: {
                indicator: yearJ,
            },
        });
    }

    render() {
        return (
            <div>
                <div ref={this.chartRef} style={{
                    width: 'calc(85vw)',
                    height: 'calc(85vh)',
                    left: 0,
                    right: 0,
                    top: 0,
                    bottom: 0,
                    margin: 'auto'
                }}>
                </div>
            </div>

        );
    }
}

export default withRouter(RadarChart);