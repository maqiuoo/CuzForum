import React, { Component } from 'react';
import { withRouter } from 'react-router-dom';

class WordCloud extends Component {
    chartRef = React.createRef();
    wordCloudScript = null;

    async componentDidMount() {
        // 动态加载echarts库
        const data0 = await this.getData(this.props.type);
        const data = await data0[0];
        const title = await data0[1];
        const wordCloudScript = document.createElement("script");
        wordCloudScript.src = "echarts-wordcloud.min.js";
        wordCloudScript.async = true;
        document.body.appendChild(wordCloudScript);
        window.echarts.registerTheme('them5', { "color": ["#893448", "#d95850", "#eb8146", "#ffb248", "#f2d643", "#ebdba4"], "title": { "textStyle": { "color": "#893448" }, "subtextStyle": { "color": "#d95850" } }, "line": { "itemStyle": { "borderWidth": "2" }, "lineStyle": { "width": "2" }, "symbolSize": "6", "symbol": "emptyCircle", "smooth": false }, "radar": { "itemStyle": { "borderWidth": "2" }, "lineStyle": { "width": "2" }, "symbolSize": "6", "symbol": "emptyCircle", "smooth": false }, "bar": { "itemStyle": { "barBorderWidth": 0, "barBorderColor": "#ccc" } }, "pie": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" } }, "scatter": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" } }, "boxplot": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" } }, "parallel": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" } }, "sankey": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" } }, "funnel": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" } }, "gauge": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" } }, "candlestick": { "itemStyle": { "color": "#eb8146", "color0": "transparent", "borderColor": "#d95850", "borderColor0": "#58c470", "borderWidth": "2" } }, "graph": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" }, "lineStyle": { "width": 1, "color": "#aaaaaa" }, "symbolSize": "6", "symbol": "emptyCircle", "smooth": true, "color": ["#893448", "#d95850", "#eb8146", "#ffb248", "#f2d643", "#ebdba4"], "label": { "color": "#ffffff" } }, "map": { "itemStyle": { "areaColor": "#f3f3f3", "borderColor": "#999999", "borderWidth": 0.5 }, "label": { "color": "#893448" }, "emphasis": { "itemStyle": { "areaColor": "#ffb248", "borderColor": "#eb8146", "borderWidth": 1 }, "label": { "color": "#893448" } } }, "geo": { "itemStyle": { "areaColor": "#f3f3f3", "borderColor": "#999999", "borderWidth": 0.5 }, "label": { "color": "#893448" }, "emphasis": { "itemStyle": { "areaColor": "#ffb248", "borderColor": "#eb8146", "borderWidth": 1 }, "label": { "color": "#893448" } } }, "categoryAxis": { "axisLine": { "show": true, "lineStyle": { "color": "#aaaaaa" } }, "axisTick": { "show": false, "lineStyle": { "color": "#333" } }, "axisLabel": { "show": true, "color": "#999999" }, "splitLine": { "show": true, "lineStyle": { "color": ["#e6e6e6"] } }, "splitArea": { "show": false, "areaStyle": { "color": ["rgba(250,250,250,0.05)", "rgba(200,200,200,0.02)"] } } }, "valueAxis": { "axisLine": { "show": true, "lineStyle": { "color": "#aaaaaa" } }, "axisTick": { "show": false, "lineStyle": { "color": "#333" } }, "axisLabel": { "show": true, "color": "#999999" }, "splitLine": { "show": true, "lineStyle": { "color": ["#e6e6e6"] } }, "splitArea": { "show": false, "areaStyle": { "color": ["rgba(250,250,250,0.05)", "rgba(200,200,200,0.02)"] } } }, "logAxis": { "axisLine": { "show": true, "lineStyle": { "color": "#aaaaaa" } }, "axisTick": { "show": false, "lineStyle": { "color": "#333" } }, "axisLabel": { "show": true, "color": "#999999" }, "splitLine": { "show": true, "lineStyle": { "color": ["#e6e6e6"] } }, "splitArea": { "show": false, "areaStyle": { "color": ["rgba(250,250,250,0.05)", "rgba(200,200,200,0.02)"] } } }, "timeAxis": { "axisLine": { "show": true, "lineStyle": { "color": "#aaaaaa" } }, "axisTick": { "show": false, "lineStyle": { "color": "#333" } }, "axisLabel": { "show": true, "color": "#999999" }, "splitLine": { "show": true, "lineStyle": { "color": ["#e6e6e6"] } }, "splitArea": { "show": false, "areaStyle": { "color": ["rgba(250,250,250,0.05)", "rgba(200,200,200,0.02)"] } } }, "toolbox": { "iconStyle": { "borderColor": "#999999" }, "emphasis": { "iconStyle": { "borderColor": "#666666" } } }, "legend": { "textStyle": { "color": "#999999" } }, "tooltip": { "axisPointer": { "lineStyle": { "color": "#cccccc", "width": 1 }, "crossStyle": { "color": "#cccccc", "width": 1 } } }, "timeline": { "lineStyle": { "color": "#893448", "width": 1 }, "itemStyle": { "color": "#893448", "borderWidth": 1 }, "controlStyle": { "color": "#893448", "borderColor": "#893448", "borderWidth": 0.5 }, "checkpointStyle": { "color": "#eb8146", "borderColor": "#ffb248" }, "label": { "color": "#893448" }, "emphasis": { "itemStyle": { "color": "#ffb248" }, "controlStyle": { "color": "#893448", "borderColor": "#893448", "borderWidth": 0.5 }, "label": { "color": "#893448" } } }, "visualMap": { "color": ["#893448", "#d95850", "#eb8146", "#ffb248", "#f2d643", "rgb(247,238,173)"] }, "dataZoom": { "backgroundColor": "rgba(255,255,255,0)", "dataBackgroundColor": "rgba(255,178,72,0.5)", "fillerColor": "rgba(255,178,72,0.15)", "handleColor": "#ffb248", "handleSize": "100%", "textStyle": { "color": "#333" } }, "markPoint": { "label": { "color": "#ffffff" }, "emphasis": { "label": { "color": "#ffffff" } } } })
        // console.log(data)
        wordCloudScript.onload = () => {
            const chart = window.echarts.init(this.chartRef.current, 'them5');
            // console.log(data);
            var option = {
                title: {
                    text: title,
                    left: "center"
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
                    type: "wordCloud",
                    shape: "circle",
                    left: "center",
                    top: "center",
                    width: "100%",
                    height: "100%",
                    keepAspect: false,
                    sizeRange: [14, 70],
                    rotationRange: [-90, 90],
                    rotationStep: 45,
                    gridSize: 15,
                    drawOutOfBound: true,
                    shrinkToFit: true,
                    layoutAnimation: true,
                    textStyle: {
                        fontFamily: "Microsoft YaHei",
                        fontWeight: 700,
                        color: function () {
                            return 'rgb(' + [
                                Math.round(Math.random() * 200),
                                Math.round(Math.random() * 250),
                                Math.round(Math.random() * 250)
                            ].join(',') + ')';
                        }
                    },
                    emphasis: {
                        focus: "none"
                    },
                    data: data
                }],
            };

            chart.setOption(option);
            this.chart = chart;
        };

    }
    async getData(type) {
        let title = null;

        try {
            if (type === "books") {
                title = "图书馆热度"
            }
            const response = await fetch(`http://127.0.0.1:4523/m1/4485527-4132188-default/wordcloud/${type}`);
            const data = await response.json();
            return [data,title];
        } catch (error) {
            console.error('Error fetching data:', error);
            return null;
        }
    }

    async updateChart() {
        // 根据新的props更新图表
        const { type } = this.props;
        const newdata = await this.getData(type);
        const data = await newdata[0];
        const title = await newdata[1];

        // 更新图表
        this.chart.setOption({
            title: {
                text: title,
                left: "center"
            },
            series: [{
                data: data
            }],
        });
    }

    componentDidUpdate(prevProps) {
        // 检查year或type是否有变化
        if (prevProps.type !== this.props.type) {
            this.updateChart();
        }
    }



    componentWillUnmount() {
        // 组件卸载时移除脚本
        if (this.wordCloudScript) {
            document.body.removeChild(this.wordCloudScript);
        }
    }


    render() {
        return (
            <div>
                <div ref={this.chartRef} style={{
                    width: 'calc(40vw)',
                    height: 'calc(40vh)',
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

export default withRouter(WordCloud);