import React, { Component } from 'react';
import { withRouter } from 'react-router-dom';

class HistogramChart2021 extends Component {
    echartsScript = null;
    componentDidMount() {
        // 动态加载echarts库
        const echartsScript = document.createElement("script");
        echartsScript.src = "echarts.min.js";
        echartsScript.async = true;

        document.body.appendChild(echartsScript);
        echartsScript.onload = () => {

            window.echarts.registerTheme('them4', { "color": ["#516b91", "#59c4e6", "#edafda", "#93b7e3", "#a5e7f0", "#cbb0e3"], "backgroundColor": "rgba(255,255,255,1)", "textStyle": {}, "title": { "textStyle": { "color": "#516b91" }, "subtextStyle": { "color": "#93b7e3" } }, "line": { "itemStyle": { "borderWidth": "2" }, "lineStyle": { "width": "2" }, "symbolSize": "6", "symbol": "emptyCircle", "smooth": false }, "radar": { "itemStyle": { "borderWidth": "2" }, "lineStyle": { "width": "2" }, "symbolSize": "6", "symbol": "emptyCircle", "smooth": false }, "bar": { "itemStyle": { "barBorderWidth": 0, "barBorderColor": "#ccc" } }, "pie": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" } }, "scatter": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" } }, "boxplot": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" } }, "parallel": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" } }, "sankey": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" } }, "funnel": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" } }, "gauge": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" } }, "candlestick": { "itemStyle": { "color": "#edafda", "color0": "transparent", "borderColor": "#d680bc", "borderColor0": "#8fd3e8", "borderWidth": "2" } }, "graph": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" }, "lineStyle": { "width": 1, "color": "#aaaaaa" }, "symbolSize": "6", "symbol": "emptyCircle", "smooth": true, "color": ["#516b91", "#59c4e6", "#edafda", "#93b7e3", "#a5e7f0", "#cbb0e3"], "label": { "color": "#eeeeee" } }, "map": { "itemStyle": { "areaColor": "#f3f3f3", "borderColor": "#516b91", "borderWidth": 0.5 }, "label": { "color": "#000" }, "emphasis": { "itemStyle": { "areaColor": "#a5e7f0", "borderColor": "#516b91", "borderWidth": 1 }, "label": { "color": "#516b91" } } }, "geo": { "itemStyle": { "areaColor": "#f3f3f3", "borderColor": "#516b91", "borderWidth": 0.5 }, "label": { "color": "#000" }, "emphasis": { "itemStyle": { "areaColor": "#a5e7f0", "borderColor": "#516b91", "borderWidth": 1 }, "label": { "color": "#516b91" } } }, "categoryAxis": { "axisLine": { "show": true, "lineStyle": { "color": "#cccccc" } }, "axisTick": { "show": false, "lineStyle": { "color": "#333" } }, "axisLabel": { "show": true, "color": "#999999" }, "splitLine": { "show": true, "lineStyle": { "color": ["#eeeeee"] } }, "splitArea": { "show": false, "areaStyle": { "color": ["rgba(250,250,250,0.05)", "rgba(200,200,200,0.02)"] } } }, "valueAxis": { "axisLine": { "show": true, "lineStyle": { "color": "#cccccc" } }, "axisTick": { "show": false, "lineStyle": { "color": "#333" } }, "axisLabel": { "show": true, "color": "#999999" }, "splitLine": { "show": true, "lineStyle": { "color": ["#eeeeee"] } }, "splitArea": { "show": false, "areaStyle": { "color": ["rgba(250,250,250,0.05)", "rgba(200,200,200,0.02)"] } } }, "logAxis": { "axisLine": { "show": true, "lineStyle": { "color": "#cccccc" } }, "axisTick": { "show": false, "lineStyle": { "color": "#333" } }, "axisLabel": { "show": true, "color": "#999999" }, "splitLine": { "show": true, "lineStyle": { "color": ["#eeeeee"] } }, "splitArea": { "show": false, "areaStyle": { "color": ["rgba(250,250,250,0.05)", "rgba(200,200,200,0.02)"] } } }, "timeAxis": { "axisLine": { "show": true, "lineStyle": { "color": "#cccccc" } }, "axisTick": { "show": false, "lineStyle": { "color": "#333" } }, "axisLabel": { "show": true, "color": "#999999" }, "splitLine": { "show": true, "lineStyle": { "color": ["#eeeeee"] } }, "splitArea": { "show": false, "areaStyle": { "color": ["rgba(250,250,250,0.05)", "rgba(200,200,200,0.02)"] } } }, "toolbox": { "iconStyle": { "borderColor": "#999999" }, "emphasis": { "iconStyle": { "borderColor": "#666666" } } }, "legend": { "textStyle": { "color": "#999999" } }, "tooltip": { "axisPointer": { "lineStyle": { "color": "#cccccc", "width": 1 }, "crossStyle": { "color": "#cccccc", "width": 1 } } }, "timeline": { "lineStyle": { "color": "#8fd3e8", "width": 1 }, "itemStyle": { "color": "#8fd3e8", "borderWidth": 1 }, "controlStyle": { "color": "#8fd3e8", "borderColor": "#8fd3e8", "borderWidth": 0.5 }, "checkpointStyle": { "color": "#8fd3e8", "borderColor": "#8a7ca8" }, "label": { "color": "#8fd3e8" }, "emphasis": { "itemStyle": { "color": "#8fd3e8" }, "controlStyle": { "color": "#8fd3e8", "borderColor": "#8fd3e8", "borderWidth": 0.5 }, "label": { "color": "#8fd3e8" } } }, "visualMap": { "color": ["#516b91", "#59c4e6", "#a5e7f0"] }, "dataZoom": { "backgroundColor": "rgba(0,0,0,0)", "dataBackgroundColor": "rgba(255,255,255,0.3)", "fillerColor": "rgba(167,183,204,0.4)", "handleColor": "#a7b7cc", "handleSize": "100%", "textStyle": { "color": "#333" } }, "markPoint": { "label": { "color": "#eeeeee" }, "emphasis": { "label": { "color": "#eeeeee" } } } })
            const chart = window.echarts.init(this.refs.chart, 'them4');


            var option = {
                title: {
                    show: true,
                    text: "2021各学院就业去向",
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
                legend: {
                    show: true,
                    orient: "vertical",
                    icon: "inherit",
                    left: "left",
                    top: "middle"
                },
                xAxis: {
                    nameLocation: "end",
                    nameRotate: 0,
                    type: "category",
                    axisLine: {
                        show: true,
                        symbol: "none",
                        symbolSize: [10, 15],
                        lineStyle: {
                            width: 2
                        }
                    },
                    axisTick: {
                        show: false,
                        length: 5,
                        inside: false,
                        alignWithLabel: false,
                        lineStyle: {
                            width: 2,
                        }
                    },
                    nameTextStyle: {
                        fontSize: 16,
                        padding: [0, 0, 0, 0],
                        fontWeight: 400,
                        fontFamily: "Microsoft YaHei"
                    },
                    splitLine: {
                        show: false,
                        lineStyle: {
                            type: "solid",
                            width: 1,
                        }
                    },
                    axisLabel: {
                        interval: 0, // 强制显示所有标签
                        margin: 8,
                        fontSize: 16,
                        fontWeight: 400,
                        fontFamily: "Microsoft YaHei"
                    },
                    data: ["播音主持艺术学院", "电视艺术学院", "华策电影学院", "媒体工程学院", "动画与数字艺术学院", "文化创意与管理学院", "国际文化传播学院（国际教育学院）", "设计艺术学院", "文学院", "新闻与传播学院", "音乐学院", "研究生院"]
                },
                yAxis: {
                    show: true,
                    nameLocation: "end",
                    nameRotate: 0,
                    position: "left",
                    boundaryGap: false,
                    type: "value",
                    axisLine: {
                        show: false,
                        symbol: "none",
                        symbolSize: [10, 15],
                        symbolOffset: [0, 0],
                        lineStyle: {
                            type: "solid",
                            width: 2
                        }
                    },
                    axisTick: {
                        show: false,
                        length: 5,
                        inside: false,
                        alignWithLabel: false,
                        lineStyle: {
                            width: 2,
                        }
                    },
                    nameTextStyle: {
                        fontSize: 16,
                        padding: [0, 0, 0, 0],
                        fontWeight: 400,
                        fontFamily: "Microsoft YaHei"
                    },
                    splitLine: {
                        show: true,
                        lineStyle: {
                            type: "solid",
                            width: 1,
                        }
                    },
                    axisLabel: {
                        rotate: 0,
                        margin: 8,
                        fontSize: 16,
                        fontWeight: 400,
                        fontFamily: "Microsoft YaHei",
                        formatter: "{value}"
                    }
                },
                tooltip: {
                    show: true,
                    trigger: "axis",
                    axisPointer: {
                        type: "none"
                    }
                }, dataZoom: [
                    {
                        type: 'slider',
                        xAxisIndex: 0,
                        filterMode: 'none'
                    },
                    {
                        type: 'slider',
                        yAxisIndex: 0,
                        filterMode: 'none'
                    },
                    {
                        type: 'inside',
                        xAxisIndex: 0,
                        filterMode: 'none'
                    },
                    {
                        type: 'inside',
                        yAxisIndex: 0,
                        filterMode: 'none'
                    }
                ],
                grid: {
                    left: 120,
                    top: 60,
                    right: 100,
                    bottom: 60,
                    containLabel: true
                },
                series: [{
                    name: "签就业协议",
                    stack: true,
                    type: "bar",
                    data: ["64", "243", "70", "211", "86", "154", "56", "110", "15", "115", "35", "60"],
                    label: {
                        show: false
                    }
                }, {
                    name: "签劳动合同",
                    stack: true,
                    type: "bar",
                    data: ["85", "26", "45", "162", "95", "161", "82", "112", "128", "85", "47", "50"],
                    label: {
                        show: false
                    }
                }, {
                    name: "公务员",
                    stack: true,
                    type: "bar",
                    data: ["0", "5", "0", "9", "0", "3", "0", "1", "3", "1", "1", "0"],
                    label: {
                        show: false
                    }
                }, {
                    name: "事业单位",
                    stack: true,
                    type: "bar",
                    data: ["19", "14", "0", "4", "2", "4", "6", "5", "3", "3", "4", "0"],
                    label: {
                        show: false
                    }
                }, {
                    name: "其他录用",
                    stack: true,
                    type: "bar",
                    data: ["4", "0", "0", "55", "4", "36", "9", "11", "5", "21", "0", "7"],
                    label: {
                        show: false
                    }
                }, {
                    name: "科研助理",
                    stack: true,
                    type: "bar",
                    data: ["0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0"],
                    label: {
                        show: false
                    }
                }, {
                    name: "应征义务兵",
                    stack: true,
                    type: "bar",
                    data: ["0", "0", "0", "6", "0", "0", "0", "0", "1", "2", "0", "0"],
                    label: {
                        show: false
                    }
                }, {
                    name: "国家基层项目",
                    stack: true,
                    type: "bar",
                    data: ["0", "0", "0", "4", "1", "2", "1", "0", "1", "0", "0", "0"],
                    label: {
                        show: false
                    }
                }, {
                    name: "地方基层项目",
                    stack: true,
                    type: "bar",
                    data: ["0", "0", "0", "1", "0", "4", "2", "1", "2", "0", "0", "0"],
                    label: {
                        show: false
                    }
                }, {
                    name: "自主创业",
                    stack: true,
                    type: "bar",
                    data: ["4", "19", "4", "7", "6", "7", "5", "11", "1", "3", "2", "1"],
                    label: {
                        show: false
                    }
                }, {
                    name: "自由职业",
                    stack: true,
                    type: "bar",
                    data: ["10", "5", "7", "37", "26", "2", "3", "0", "28", "5", "31", "0"],
                    label: {
                        show: false
                    }
                }, {
                    name: "国内升学",
                    stack: true,
                    type: "bar",
                    data: ["5", "27", "2", "21", "13", "24", "18", "20", "26", "27", "12", "1"],
                    label: {
                        show: false
                    }
                }, {
                    name: "出国出境",
                    stack: true,
                    type: "bar",
                    data: ["27", "55", "10", "14", "16", "50", "38", "35", "41", "41", "13", "0"],
                    label: {
                        show: false
                    }
                }],
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

export default withRouter(HistogramChart2021);