import React, { Component } from 'react';
import { withRouter } from 'react-router-dom';

class RadarChart2021 extends Component {
    echartsScript = null;
    componentDidMount() {
        // 动态加载echarts库
        const echartsScript = document.createElement("script");
        echartsScript.src = "echarts.min.js";
        echartsScript.async = true;

        document.body.appendChild(echartsScript);
        echartsScript.onload = () => {

            window.echarts.registerTheme('them4', { "color": ["#516b91", "#59c4e6", "#edafda", "#93b7e3", "#a5e7f0", "#cbb0e3"], "backgroundColor": "rgba(255,255,255,1)", "textStyle": {}, "title": { "textStyle": { "color": "#516b91" }, "subtextStyle": { "color": "#93b7e3" } }, "line": { "itemStyle": { "borderWidth": "2" }, "lineStyle": { "width": "2" }, "symbolSize": "6", "symbol": "emptyCircle", "smooth": false }, "radar": { "itemStyle": { "borderWidth": "2" }, "lineStyle": { "width": "2" }, "symbolSize": "6", "symbol": "emptyCircle", "smooth": false }, "bar": { "itemStyle": { "barBorderWidth": 0, "barBorderColor": "#ccc" } }, "pie": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" } }, "scatter": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" } }, "boxplot": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" } }, "parallel": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" } }, "sankey": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" } }, "funnel": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" } }, "gauge": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" } }, "candlestick": { "itemStyle": { "color": "#edafda", "color0": "transparent", "borderColor": "#d680bc", "borderColor0": "#8fd3e8", "borderWidth": "2" } }, "graph": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" }, "lineStyle": { "width": 1, "color": "#aaaaaa" }, "symbolSize": "6", "symbol": "emptyCircle", "smooth": true, "color": ["#516b91", "#59c4e6", "#edafda", "#93b7e3", "#a5e7f0", "#cbb0e3"], "label": { "color": "#eeeeee" } }, "map": { "itemStyle": { "areaColor": "#f3f3f3", "borderColor": "#516b91", "borderWidth": 0.5 }, "label": { "color": "#000" }, "emphasis": { "itemStyle": { "areaColor": "#a5e7f0", "borderColor": "#516b91", "borderWidth": 1 }, "label": { "color": "#516b91" } } }, "geo": { "itemStyle": { "areaColor": "#f3f3f3", "borderColor": "#516b91", "borderWidth": 0.5 }, "label": { "color": "#000" }, "emphasis": { "itemStyle": { "areaColor": "#a5e7f0", "borderColor": "#516b91", "borderWidth": 1 }, "label": { "color": "#516b91" } } }, "categoryAxis": { "axisLine": { "show": true, "lineStyle": { "color": "#cccccc" } }, "axisTick": { "show": false, "lineStyle": { "color": "#333" } }, "axisLabel": { "show": true, "color": "#999999" }, "splitLine": { "show": true, "lineStyle": { "color": ["#eeeeee"] } }, "splitArea": { "show": false, "areaStyle": { "color": ["rgba(250,250,250,0.05)", "rgba(200,200,200,0.02)"] } } }, "valueAxis": { "axisLine": { "show": true, "lineStyle": { "color": "#cccccc" } }, "axisTick": { "show": false, "lineStyle": { "color": "#333" } }, "axisLabel": { "show": true, "color": "#999999" }, "splitLine": { "show": true, "lineStyle": { "color": ["#eeeeee"] } }, "splitArea": { "show": false, "areaStyle": { "color": ["rgba(250,250,250,0.05)", "rgba(200,200,200,0.02)"] } } }, "logAxis": { "axisLine": { "show": true, "lineStyle": { "color": "#cccccc" } }, "axisTick": { "show": false, "lineStyle": { "color": "#333" } }, "axisLabel": { "show": true, "color": "#999999" }, "splitLine": { "show": true, "lineStyle": { "color": ["#eeeeee"] } }, "splitArea": { "show": false, "areaStyle": { "color": ["rgba(250,250,250,0.05)", "rgba(200,200,200,0.02)"] } } }, "timeAxis": { "axisLine": { "show": true, "lineStyle": { "color": "#cccccc" } }, "axisTick": { "show": false, "lineStyle": { "color": "#333" } }, "axisLabel": { "show": true, "color": "#999999" }, "splitLine": { "show": true, "lineStyle": { "color": ["#eeeeee"] } }, "splitArea": { "show": false, "areaStyle": { "color": ["rgba(250,250,250,0.05)", "rgba(200,200,200,0.02)"] } } }, "toolbox": { "iconStyle": { "borderColor": "#999999" }, "emphasis": { "iconStyle": { "borderColor": "#666666" } } }, "legend": { "textStyle": { "color": "#999999" } }, "tooltip": { "axisPointer": { "lineStyle": { "color": "#cccccc", "width": 1 }, "crossStyle": { "color": "#cccccc", "width": 1 } } }, "timeline": { "lineStyle": { "color": "#8fd3e8", "width": 1 }, "itemStyle": { "color": "#8fd3e8", "borderWidth": 1 }, "controlStyle": { "color": "#8fd3e8", "borderColor": "#8fd3e8", "borderWidth": 0.5 }, "checkpointStyle": { "color": "#8fd3e8", "borderColor": "#8a7ca8" }, "label": { "color": "#8fd3e8" }, "emphasis": { "itemStyle": { "color": "#8fd3e8" }, "controlStyle": { "color": "#8fd3e8", "borderColor": "#8fd3e8", "borderWidth": 0.5 }, "label": { "color": "#8fd3e8" } } }, "visualMap": { "color": ["#516b91", "#59c4e6", "#a5e7f0"] }, "dataZoom": { "backgroundColor": "rgba(0,0,0,0)", "dataBackgroundColor": "rgba(255,255,255,0.3)", "fillerColor": "rgba(167,183,204,0.4)", "handleColor": "#a7b7cc", "handleSize": "100%", "textStyle": { "color": "#333" } }, "markPoint": { "label": { "color": "#eeeeee" }, "emphasis": { "label": { "color": "#eeeeee" } } } })
            const chart = window.echarts.init(this.refs.chart);


            const option = {
                title: {
                    show: true,
                    text: "2021浙传就业比对",
                    padding: [10, 5, 5, 5],
                    textStyle: {
                        fontFamily: "Microsoft YaHei",
                        fontWeight: 400
                    },
                    subtextStyle: {
                        fontSize: 12,
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
                        value: ["110", "68", "40", "218", "91", "47", "71", "83", "67", "35", "394", "48", "17", "73", "138", "58", "56", "108", "38", "122", "52", "60", "37", "531", "132", "39", "78", "249", "37", "52", "83", "72", "37", "81", "85", "447", "33", "30", "17", "73", "67", "220", "24", "38", "29", "89", "62", "32", "32", "306", "31", "67", "29", "94", "33", "254", "38", "43", "95", "39", "88", "303", "26", "23", "25", "71", "145", "3205", "119"],
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
                    indicator: [{
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
                    }],
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


        };
    }
    componentWillUnmount() {
        // 组件卸载时移除脚本
        if (this.echartsScript) {
            document.body.removeChild(this.echartsScript);
        }
        if (this.geoJsonScript) {
            document.body.removeChild(this.geoJsonScript);
        }
    }


    render() {
        return (
            <div>
                <div ref="chart" style={{
                    width: 'calc(85vw)',
                    height: 'calc(85vh)',
                    position: 'absolute',
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

export default withRouter(RadarChart2021);