import React, { Component } from 'react';
import { withRouter } from 'react-router-dom';

class PieChart extends Component {
    chartRef = React.createRef();

    async componentDidMount() {
        // 动态加载echarts库
        window.echarts.registerTheme('them11', { "color": ["#e01f54", "#001852", "#f5e8c8", "#b8d2c7", "#c6b38e", "#a4d8c2", "#f3d999", "#d3758f", "#dcc392", "#2e4783", "#82b6e9", "#ff6347", "#a092f1", "#0a915d", "#eaf889", "#6699ff", "#ff6666", "#3cb371", "#d5b158", "#38b6b6"], "line": { "itemStyle": { "borderWidth": 1 }, "lineStyle": { "width": 2 }, "symbolSize": 4, "symbol": "emptyCircle", "smooth": false }, "radar": { "itemStyle": { "borderWidth": 1 }, "lineStyle": { "width": 2 }, "symbolSize": 4, "symbol": "emptyCircle", "smooth": false }, "bar": { "itemStyle": { "barBorderWidth": 0, "barBorderColor": "#ccc" } }, "pie": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" } }, "scatter": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" } }, "boxplot": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" } }, "parallel": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" } }, "sankey": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" } }, "funnel": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" } }, "gauge": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" } }, "candlestick": { "itemStyle": { "color": "#e01f54", "color0": "#001852", "borderColor": "#f5e8c8", "borderColor0": "#b8d2c7", "borderWidth": 1 } }, "graph": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" }, "lineStyle": { "width": 1, "color": "#aaaaaa" }, "symbolSize": 4, "symbol": "emptyCircle", "smooth": false, "color": ["#e01f54", "#001852", "#f5e8c8", "#b8d2c7", "#c6b38e", "#a4d8c2", "#f3d999", "#d3758f", "#dcc392", "#2e4783", "#82b6e9", "#ff6347", "#a092f1", "#0a915d", "#eaf889", "#6699ff", "#ff6666", "#3cb371", "#d5b158", "#38b6b6"], "label": { "color": "#eeeeee" } }, "map": { "itemStyle": { "areaColor": "#eeeeee", "borderColor": "#444444", "borderWidth": 0.5 }, "label": { "color": "#000000" }, "emphasis": { "itemStyle": { "areaColor": "rgba(255,215,0,0.8)", "borderColor": "#444", "borderWidth": 1 }, "label": { "color": "rgb(100,0,0)" } } }, "geo": { "itemStyle": { "areaColor": "#eeeeee", "borderColor": "#444444", "borderWidth": 0.5 }, "label": { "color": "#000000" }, "emphasis": { "itemStyle": { "areaColor": "rgba(255,215,0,0.8)", "borderColor": "#444", "borderWidth": 1 }, "label": { "color": "rgb(100,0,0)" } } }, "categoryAxis": { "axisLine": { "show": true, "lineStyle": { "color": "#333" } }, "axisTick": { "show": true, "lineStyle": { "color": "#333" } }, "axisLabel": { "show": true, "color": "#333" }, "splitLine": { "show": false, "lineStyle": { "color": ["#ccc"] } }, "splitArea": { "show": false, "areaStyle": { "color": ["rgba(250,250,250,0.3)", "rgba(200,200,200,0.3)"] } } }, "valueAxis": { "axisLine": { "show": true, "lineStyle": { "color": "#333" } }, "axisTick": { "show": true, "lineStyle": { "color": "#333" } }, "axisLabel": { "show": true, "color": "#333" }, "splitLine": { "show": true, "lineStyle": { "color": ["#ccc"] } }, "splitArea": { "show": false, "areaStyle": { "color": ["rgba(250,250,250,0.3)", "rgba(200,200,200,0.3)"] } } }, "logAxis": { "axisLine": { "show": true, "lineStyle": { "color": "#333" } }, "axisTick": { "show": true, "lineStyle": { "color": "#333" } }, "axisLabel": { "show": true, "color": "#333" }, "splitLine": { "show": true, "lineStyle": { "color": ["#ccc"] } }, "splitArea": { "show": false, "areaStyle": { "color": ["rgba(250,250,250,0.3)", "rgba(200,200,200,0.3)"] } } }, "timeAxis": { "axisLine": { "show": true, "lineStyle": { "color": "#333" } }, "axisTick": { "show": true, "lineStyle": { "color": "#333" } }, "axisLabel": { "show": true, "color": "#333" }, "splitLine": { "show": true, "lineStyle": { "color": ["#ccc"] } }, "splitArea": { "show": false, "areaStyle": { "color": ["rgba(250,250,250,0.3)", "rgba(200,200,200,0.3)"] } } }, "toolbox": { "iconStyle": { "borderColor": "#999999" }, "emphasis": { "iconStyle": { "borderColor": "#666666" } } }, "legend": { "textStyle": { "color": "#333333" } }, "tooltip": { "axisPointer": { "lineStyle": { "color": "#cccccc", "width": 1 }, "crossStyle": { "color": "#cccccc", "width": 1 } } }, "timeline": { "lineStyle": { "color": "#293c55", "width": 1 }, "itemStyle": { "color": "#293c55", "borderWidth": 1 }, "controlStyle": { "color": "#293c55", "borderColor": "#293c55", "borderWidth": 0.5 }, "checkpointStyle": { "color": "#e43c59", "borderColor": "#c23531" }, "label": { "color": "#293c55" }, "emphasis": { "itemStyle": { "color": "#a9334c" }, "controlStyle": { "color": "#293c55", "borderColor": "#293c55", "borderWidth": 0.5 }, "label": { "color": "#293c55" } } }, "visualMap": { "color": ["#e01f54", "#e7dbc3"] }, "dataZoom": { "backgroundColor": "rgba(47,69,84,0)", "dataBackgroundColor": "rgba(47,69,84,0.3)", "fillerColor": "rgba(167,183,204,0.4)", "handleColor": "#a7b7cc", "handleSize": "100%", "textStyle": { "color": "#333333" } }, "markPoint": { "label": { "color": "#eeeeee" }, "emphasis": { "label": { "color": "#eeeeee" } } } })
        const chart = window.echarts.init(this.chartRef.current, 'them11');
        const data = await this.getData(this.props.year);
        // console.log(data);
        var option = {
            title: {
                show: true,
                text: `${this.props.year}本科待就业`,
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
                name: `${this.props.year}本科待就业`,
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
                data: data,
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
        this.chart = chart;



        // };
    }
    componentDidUpdate(prevProps) {
        // 检查year或type是否有变化
        if (prevProps.year !== this.props.year) {
            this.updateChart();
        }
    }

    async getData(year) {
        try {
            const response = await fetch(`${year}/${year}本科待就业.json`);
            const data = await response.json();
            // console.log(data);
            return data;
        } catch (error) {
            console.error('Error fetching data:', error);
            return null;
        }
    }

    async updateChart() {
        const { year } = this.props;
        const data = await this.getData(year);
        this.chart.setOption({
            title: {
                text: `${year}本科待就业`
            },
            series: [{
                name: `${year}本科待就业`,
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

export default withRouter(PieChart);