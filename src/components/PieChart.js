import React, { Component } from 'react';
import { withRouter } from 'react-router-dom';

class PieChart extends Component {
    echartsScript = null;
    componentDidMount() {
        // 动态加载echarts库
        const echartsScript = document.createElement("script");
        echartsScript.src = "echarts.min.js";
        echartsScript.async = true;

        document.body.appendChild(echartsScript);
        echartsScript.onload = () => {

            window.echarts.registerTheme('them4', { "color": ["#516b91", "#59c4e6", "#edafda", "#93b7e3", "#a5e7f0", "#cbb0e3"], "backgroundColor": "rgba(255,255,255,1)", "textStyle": {}, "title": { "textStyle": { "color": "#516b91" }, "subtextStyle": { "color": "#93b7e3" } }, "line": { "itemStyle": { "borderWidth": "2" }, "lineStyle": { "width": "2" }, "symbolSize": "6", "symbol": "emptyCircle", "smooth": false }, "radar": { "itemStyle": { "borderWidth": "2" }, "lineStyle": { "width": "2" }, "symbolSize": "6", "symbol": "emptyCircle", "smooth": false }, "bar": { "itemStyle": { "barBorderWidth": 0, "barBorderColor": "#ccc" } }, "pie": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" } }, "scatter": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" } }, "boxplot": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" } }, "parallel": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" } }, "sankey": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" } }, "funnel": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" } }, "gauge": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" } }, "candlestick": { "itemStyle": { "color": "#edafda", "color0": "transparent", "borderColor": "#d680bc", "borderColor0": "#8fd3e8", "borderWidth": "2" } }, "graph": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" }, "lineStyle": { "width": 1, "color": "#aaaaaa" }, "symbolSize": "6", "symbol": "emptyCircle", "smooth": true, "color": ["#516b91", "#59c4e6", "#edafda", "#93b7e3", "#a5e7f0", "#cbb0e3"], "label": { "color": "#eeeeee" } }, "map": { "itemStyle": { "areaColor": "#f3f3f3", "borderColor": "#516b91", "borderWidth": 0.5 }, "label": { "color": "#000" }, "emphasis": { "itemStyle": { "areaColor": "#a5e7f0", "borderColor": "#516b91", "borderWidth": 1 }, "label": { "color": "#516b91" } } }, "geo": { "itemStyle": { "areaColor": "#f3f3f3", "borderColor": "#516b91", "borderWidth": 0.5 }, "label": { "color": "#000" }, "emphasis": { "itemStyle": { "areaColor": "#a5e7f0", "borderColor": "#516b91", "borderWidth": 1 }, "label": { "color": "#516b91" } } }, "categoryAxis": { "axisLine": { "show": true, "lineStyle": { "color": "#cccccc" } }, "axisTick": { "show": false, "lineStyle": { "color": "#333" } }, "axisLabel": { "show": true, "color": "#999999" }, "splitLine": { "show": true, "lineStyle": { "color": ["#eeeeee"] } }, "splitArea": { "show": false, "areaStyle": { "color": ["rgba(250,250,250,0.05)", "rgba(200,200,200,0.02)"] } } }, "valueAxis": { "axisLine": { "show": true, "lineStyle": { "color": "#cccccc" } }, "axisTick": { "show": false, "lineStyle": { "color": "#333" } }, "axisLabel": { "show": true, "color": "#999999" }, "splitLine": { "show": true, "lineStyle": { "color": ["#eeeeee"] } }, "splitArea": { "show": false, "areaStyle": { "color": ["rgba(250,250,250,0.05)", "rgba(200,200,200,0.02)"] } } }, "logAxis": { "axisLine": { "show": true, "lineStyle": { "color": "#cccccc" } }, "axisTick": { "show": false, "lineStyle": { "color": "#333" } }, "axisLabel": { "show": true, "color": "#999999" }, "splitLine": { "show": true, "lineStyle": { "color": ["#eeeeee"] } }, "splitArea": { "show": false, "areaStyle": { "color": ["rgba(250,250,250,0.05)", "rgba(200,200,200,0.02)"] } } }, "timeAxis": { "axisLine": { "show": true, "lineStyle": { "color": "#cccccc" } }, "axisTick": { "show": false, "lineStyle": { "color": "#333" } }, "axisLabel": { "show": true, "color": "#999999" }, "splitLine": { "show": true, "lineStyle": { "color": ["#eeeeee"] } }, "splitArea": { "show": false, "areaStyle": { "color": ["rgba(250,250,250,0.05)", "rgba(200,200,200,0.02)"] } } }, "toolbox": { "iconStyle": { "borderColor": "#999999" }, "emphasis": { "iconStyle": { "borderColor": "#666666" } } }, "legend": { "textStyle": { "color": "#999999" } }, "tooltip": { "axisPointer": { "lineStyle": { "color": "#cccccc", "width": 1 }, "crossStyle": { "color": "#cccccc", "width": 1 } } }, "timeline": { "lineStyle": { "color": "#8fd3e8", "width": 1 }, "itemStyle": { "color": "#8fd3e8", "borderWidth": 1 }, "controlStyle": { "color": "#8fd3e8", "borderColor": "#8fd3e8", "borderWidth": 0.5 }, "checkpointStyle": { "color": "#8fd3e8", "borderColor": "#8a7ca8" }, "label": { "color": "#8fd3e8" }, "emphasis": { "itemStyle": { "color": "#8fd3e8" }, "controlStyle": { "color": "#8fd3e8", "borderColor": "#8fd3e8", "borderWidth": 0.5 }, "label": { "color": "#8fd3e8" } } }, "visualMap": { "color": ["#516b91", "#59c4e6", "#a5e7f0"] }, "dataZoom": { "backgroundColor": "rgba(0,0,0,0)", "dataBackgroundColor": "rgba(255,255,255,0.3)", "fillerColor": "rgba(167,183,204,0.4)", "handleColor": "#a7b7cc", "handleSize": "100%", "textStyle": { "color": "#333" } }, "markPoint": { "label": { "color": "#eeeeee" }, "emphasis": { "label": { "color": "#eeeeee" } } } })
            const chart = window.echarts.init(this.refs.chart,'them4');
            fetch(`${this.props.year}/${this.props.year}本科待就业.json`)
                .then(response => response.json())
                .then(data => {
                    console.log(data);
                    var option = {
                        title: {
                            show: true,
                            text: `${this.props.year}本科待就业`,
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
                            left: "right",
                            top: "middle"
                        },
                        xAxis: {
                            show: false,
                            type: "category"
                        },
                        yAxis: {
                            show: false,
                            type: "value"
                        },
                        tooltip: {
                            show: true,
                            trigger: "item",
                            axisPointer: {
                                type: "none"
                            }
                        },
                        series: [{
                            name: "类型",
                            type: "pie",
                            radius: "50%",
                            startAngle: 90,
                            endAngle: "auto",
                            center: ["50%", "50%"],
                            roseType: "radius",
                            selectedMode: "delete",
                            select: {
                                disabled: true
                            },
                            itemStyle: {
                                borderRadius: 0,
                                borderWidth: 0,
                                borderColor: "#fff"
                            },
                            data:data,
                            label: {
                                show: true,
                                position: "outside",
                                fontSize: 14,
                                borderWidth: 0,
                                padding: [5, 5, 5, 5],
                                borderRadius: [0, 0, 0, 0],
                                formatter: "{b}"
                            }
                        }],
                    };

                    chart.setOption(option);
                }).catch(error => console.error('Error fetching data:', error));

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

export default withRouter(PieChart);