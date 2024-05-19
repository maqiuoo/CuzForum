import React, { Component } from 'react';
import { withRouter } from 'react-router-dom';

class FunnelChart extends Component {
    chartRef = React.createRef();
    async componentDidMount() {

        window.echarts.registerTheme('them4', { "color": ["#516b91", "#59c4e6", "#edafda", "#93b7e3", "#a5e7f0", "#cbb0e3"], "line": { "itemStyle": { "borderWidth": "2" }, "lineStyle": { "width": "2" }, "symbolSize": "6", "symbol": "emptyCircle", "smooth": false }, "radar": { "itemStyle": { "borderWidth": "2" }, "lineStyle": { "width": "2" }, "symbolSize": "6", "symbol": "emptyCircle", "smooth": false }, "bar": { "itemStyle": { "barBorderWidth": 0, "barBorderColor": "#ccc" } }, "pie": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" } }, "scatter": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" } }, "boxplot": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" } }, "parallel": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" } }, "sankey": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" } }, "funnel": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" } }, "gauge": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" } }, "candlestick": { "itemStyle": { "color": "#edafda", "color0": "transparent", "borderColor": "#d680bc", "borderColor0": "#8fd3e8", "borderWidth": "2" } }, "graph": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" }, "lineStyle": { "width": 1, "color": "#aaaaaa" }, "symbolSize": "6", "symbol": "emptyCircle", "smooth": true, "color": ["#516b91", "#59c4e6", "#edafda", "#93b7e3", "#a5e7f0", "#cbb0e3"], "label": { "color": "#eeeeee" } }, "map": { "itemStyle": { "areaColor": "#f3f3f3", "borderColor": "#516b91", "borderWidth": 0.5 }, "label": { "color": "#000" }, "emphasis": { "itemStyle": { "areaColor": "#a5e7f0", "borderColor": "#516b91", "borderWidth": 1 }, "label": { "color": "#516b91" } } }, "geo": { "itemStyle": { "areaColor": "#f3f3f3", "borderColor": "#516b91", "borderWidth": 0.5 }, "label": { "color": "#000" }, "emphasis": { "itemStyle": { "areaColor": "#a5e7f0", "borderColor": "#516b91", "borderWidth": 1 }, "label": { "color": "#516b91" } } }, "categoryAxis": { "axisLine": { "show": true, "lineStyle": { "color": "#cccccc" } }, "axisTick": { "show": false, "lineStyle": { "color": "#333" } }, "axisLabel": { "show": true, "color": "#999999" }, "splitLine": { "show": true, "lineStyle": { "color": ["#eeeeee"] } }, "splitArea": { "show": false, "areaStyle": { "color": ["rgba(250,250,250,0.05)", "rgba(200,200,200,0.02)"] } } }, "valueAxis": { "axisLine": { "show": true, "lineStyle": { "color": "#cccccc" } }, "axisTick": { "show": false, "lineStyle": { "color": "#333" } }, "axisLabel": { "show": true, "color": "#999999" }, "splitLine": { "show": true, "lineStyle": { "color": ["#eeeeee"] } }, "splitArea": { "show": false, "areaStyle": { "color": ["rgba(250,250,250,0.05)", "rgba(200,200,200,0.02)"] } } }, "logAxis": { "axisLine": { "show": true, "lineStyle": { "color": "#cccccc" } }, "axisTick": { "show": false, "lineStyle": { "color": "#333" } }, "axisLabel": { "show": true, "color": "#999999" }, "splitLine": { "show": true, "lineStyle": { "color": ["#eeeeee"] } }, "splitArea": { "show": false, "areaStyle": { "color": ["rgba(250,250,250,0.05)", "rgba(200,200,200,0.02)"] } } }, "timeAxis": { "axisLine": { "show": true, "lineStyle": { "color": "#cccccc" } }, "axisTick": { "show": false, "lineStyle": { "color": "#333" } }, "axisLabel": { "show": true, "color": "#999999" }, "splitLine": { "show": true, "lineStyle": { "color": ["#eeeeee"] } }, "splitArea": { "show": false, "areaStyle": { "color": ["rgba(250,250,250,0.05)", "rgba(200,200,200,0.02)"] } } }, "toolbox": { "iconStyle": { "borderColor": "#999999" }, "emphasis": { "iconStyle": { "borderColor": "#666666" } } }, "legend": { "textStyle": { "color": "#999999" } }, "tooltip": { "axisPointer": { "lineStyle": { "color": "#cccccc", "width": 1 }, "crossStyle": { "color": "#cccccc", "width": 1 } } }, "timeline": { "lineStyle": { "color": "#8fd3e8", "width": 1 }, "itemStyle": { "color": "#8fd3e8", "borderWidth": 1 }, "controlStyle": { "color": "#8fd3e8", "borderColor": "#8fd3e8", "borderWidth": 0.5 }, "checkpointStyle": { "color": "#8fd3e8", "borderColor": "#8a7ca8" }, "label": { "color": "#8fd3e8" }, "emphasis": { "itemStyle": { "color": "#8fd3e8" }, "controlStyle": { "color": "#8fd3e8", "borderColor": "#8fd3e8", "borderWidth": 0.5 }, "label": { "color": "#8fd3e8" } } }, "visualMap": { "color": ["#516b91", "#59c4e6", "#a5e7f0"] }, "dataZoom": { "backgroundColor": "rgba(0,0,0,0)", "dataBackgroundColor": "rgba(255,255,255,0.3)", "fillerColor": "rgba(167,183,204,0.4)", "handleColor": "#a7b7cc", "handleSize": "100%", "textStyle": { "color": "#333" } }, "markPoint": { "label": { "color": "#eeeeee" }, "emphasis": { "label": { "color": "#eeeeee" } } } })
        const chart = window.echarts.init(this.chartRef.current, 'them4')
        const data = await this.getData(this.props.year, this.props.type, this.props.t);
        // console.log(data);
        var option = {
            title: {
                show: true,
                text: `${this.props.year}${this.props.type}就业${this.props.t}`,
                textStyle: {
                    fontFamily: "Microsoft YaHei",
                    fontWeight: 400
                },
                left: "center",
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
                name: `${this.props.year}${this.props.type}就业${this.props.t}`,
                type: "funnel",
                orient: "vertical",
                sort: "ascending",
                left: "center",
                itemStyle: {
                    opacity: 1,
                    borderWidth: 0,
                    borderColor: "#fff"
                },
                data: data,
                label: {
                    show: true,
                    position: "inside",
                    fontSize: 14,
                    borderWidth: 0,
                    padding: [5, 5, 5, 5],
                    borderRadius: [0, 0, 0, 0],
                    formatter: "{b}"
                }
            }],
        };

        chart.setOption(option);
        this.chart = chart;
    }

    componentDidUpdate(prevProps) {
        // 检查year或type是否有变化
        if (prevProps.year !== this.props.year || prevProps.type !== this.props.type) {
            this.updateChart();
        }
    }

    async getData(year, type, t) {
        try {
            const response = await fetch(`${year}/${year}${type}就业${t}.json`);
            const data = await response.json();
            // console.log(data);
            return data;
        } catch (error) {
            console.error('Error fetching data:', error);
            return null;
        }
    }

    async updateChart() {
        const { year, type, t } = this.props;
        const data = await this.getData(year, type, t);
        this.chart.setOption({
            title: {
                text: `${year}${type}就业${t}`
            },
            series: [{
                name: `${year}${type}就业${t}`,
                data: data
            }]
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

export default withRouter(FunnelChart);