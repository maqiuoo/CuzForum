import React, { Component } from 'react';
import { withRouter } from 'react-router-dom';
import Axios from '../js/axios';

class ChinaMap extends Component {
    chartRef = React.createRef();
    geoJsonScript = null;

    async componentDidMount() {
        // 动态加载china-map-geojson库
        const geoJsonScript = document.createElement("script");
        geoJsonScript.src = "china-map-geojson.min.js"; // 请确保这里的路径是正确的
        geoJsonScript.async = true;
        document.body.appendChild(geoJsonScript);
        const data = await this.getData(this.props.year);
        geoJsonScript.onload = () => {
            // console.log('echarts和china-map-geojson已加载完成');

            window.echarts.registerMap('China', window['china-map-geojson']['ChinaData']);
            window.echarts.registerTheme('them11', { "color": ["#e01f54", "#001852", "#f5e8c8", "#b8d2c7", "#c6b38e", "#a4d8c2", "#f3d999", "#d3758f", "#dcc392", "#2e4783", "#82b6e9", "#ff6347", "#a092f1", "#0a915d", "#eaf889", "#6699ff", "#ff6666", "#3cb371", "#d5b158", "#38b6b6"], "textStyle": {}, "title": { "textStyle": { "color": "#333333" } }, "line": { "itemStyle": { "borderWidth": 1 }, "lineStyle": { "width": 2 }, "symbolSize": 4, "symbol": "emptyCircle", "smooth": false }, "radar": { "itemStyle": { "borderWidth": 1 }, "lineStyle": { "width": 2 }, "symbolSize": 4, "symbol": "emptyCircle", "smooth": false }, "bar": { "itemStyle": { "barBorderWidth": 0, "barBorderColor": "#ccc" } }, "pie": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" } }, "scatter": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" } }, "boxplot": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" } }, "parallel": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" } }, "sankey": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" } }, "funnel": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" } }, "gauge": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" } }, "candlestick": { "itemStyle": { "color": "#e01f54", "color0": "#001852", "borderColor": "#f5e8c8", "borderColor0": "#b8d2c7", "borderWidth": 1 } }, "graph": { "itemStyle": { "borderWidth": 0, "borderColor": "#ccc" }, "lineStyle": { "width": 1, "color": "#aaaaaa" }, "symbolSize": 4, "symbol": "emptyCircle", "smooth": false, "color": ["#e01f54", "#001852", "#f5e8c8", "#b8d2c7", "#c6b38e", "#a4d8c2", "#f3d999", "#d3758f", "#dcc392", "#2e4783", "#82b6e9", "#ff6347", "#a092f1", "#0a915d", "#eaf889", "#6699ff", "#ff6666", "#3cb371", "#d5b158", "#38b6b6"], "label": { "color": "#eeeeee" } }, "map": { "itemStyle": { "areaColor": "#eeeeee", "borderColor": "#444444", "borderWidth": 0.5 }, "label": { "color": "#000000" }, "emphasis": { "itemStyle": { "areaColor": "rgba(255,215,0,0.8)", "borderColor": "#444", "borderWidth": 1 }, "label": { "color": "rgb(100,0,0)" } } }, "geo": { "itemStyle": { "areaColor": "#eeeeee", "borderColor": "#444444", "borderWidth": 0.5 }, "label": { "color": "#000000" }, "emphasis": { "itemStyle": { "areaColor": "rgba(255,215,0,0.8)", "borderColor": "#444", "borderWidth": 1 }, "label": { "color": "rgb(100,0,0)" } } }, "categoryAxis": { "axisLine": { "show": true, "lineStyle": { "color": "#333" } }, "axisTick": { "show": true, "lineStyle": { "color": "#333" } }, "axisLabel": { "show": true, "color": "#333" }, "splitLine": { "show": false, "lineStyle": { "color": ["#ccc"] } }, "splitArea": { "show": false, "areaStyle": { "color": ["rgba(250,250,250,0.3)", "rgba(200,200,200,0.3)"] } } }, "valueAxis": { "axisLine": { "show": true, "lineStyle": { "color": "#333" } }, "axisTick": { "show": true, "lineStyle": { "color": "#333" } }, "axisLabel": { "show": true, "color": "#333" }, "splitLine": { "show": true, "lineStyle": { "color": ["#ccc"] } }, "splitArea": { "show": false, "areaStyle": { "color": ["rgba(250,250,250,0.3)", "rgba(200,200,200,0.3)"] } } }, "logAxis": { "axisLine": { "show": true, "lineStyle": { "color": "#333" } }, "axisTick": { "show": true, "lineStyle": { "color": "#333" } }, "axisLabel": { "show": true, "color": "#333" }, "splitLine": { "show": true, "lineStyle": { "color": ["#ccc"] } }, "splitArea": { "show": false, "areaStyle": { "color": ["rgba(250,250,250,0.3)", "rgba(200,200,200,0.3)"] } } }, "timeAxis": { "axisLine": { "show": true, "lineStyle": { "color": "#333" } }, "axisTick": { "show": true, "lineStyle": { "color": "#333" } }, "axisLabel": { "show": true, "color": "#333" }, "splitLine": { "show": true, "lineStyle": { "color": ["#ccc"] } }, "splitArea": { "show": false, "areaStyle": { "color": ["rgba(250,250,250,0.3)", "rgba(200,200,200,0.3)"] } } }, "toolbox": { "iconStyle": { "borderColor": "#999999" }, "emphasis": { "iconStyle": { "borderColor": "#666666" } } }, "legend": { "textStyle": { "color": "#333333" } }, "tooltip": { "axisPointer": { "lineStyle": { "color": "#cccccc", "width": 1 }, "crossStyle": { "color": "#cccccc", "width": 1 } } }, "timeline": { "lineStyle": { "color": "#293c55", "width": 1 }, "itemStyle": { "color": "#293c55", "borderWidth": 1 }, "controlStyle": { "color": "#293c55", "borderColor": "#293c55", "borderWidth": 0.5 }, "checkpointStyle": { "color": "#e43c59", "borderColor": "#c23531" }, "label": { "color": "#293c55" }, "emphasis": { "itemStyle": { "color": "#a9334c" }, "controlStyle": { "color": "#293c55", "borderColor": "#293c55", "borderWidth": 0.5 }, "label": { "color": "#293c55" } } }, "visualMap": { "color": ["#e01f54", "#e7dbc3"] }, "dataZoom": { "backgroundColor": "rgba(47,69,84,0)", "dataBackgroundColor": "rgba(47,69,84,0.3)", "fillerColor": "rgba(167,183,204,0.4)", "handleColor": "#a7b7cc", "handleSize": "100%", "textStyle": { "color": "#333333" } }, "markPoint": { "label": { "color": "#eeeeee" }, "emphasis": { "label": { "color": "#eeeeee" } } } })
            const chart = window.echarts.init(this.chartRef.current, 'them11')

            const option = {
                title: {
                    show: true,
                    text: `${this.props.year}本科全国就业`,
                    textStyle: {
                        fontFamily: "Microsoft YaHei",
                        fontWeight: 400
                    },
                    left: "center",
                    top: "top"
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
                    },
                    formatter: function (params) {
                        if (Array.isArray(params)) {
                            let arr = []
                            params.forEach((re, i) => {
                                arr.push(re.name + ' : ' + re.value + '')
                            })
                            return arr.join('<br>')
                        } else {
                            return params.name + ' : ' + (Array.isArray(params.value) ? (params.value[params.value.length - 1] + '') : (params.value + ''))
                        }
                    }
                },
                visualMap: {
                    type: "continuous",
                    min: 0,
                    max: 250,
                    range: [0, 300],
                    orient: "vertical",
                    show: true,
                    inverse: false,
                    padding: [5, 5, 5, 5],
                    itemWidth: 20,
                    itemHeight: 140,
                    text: ["高", "低"],
                    left: "right",
                    top: "middle"
                },
                series: [{
                    name: `${this.props.year}本科全国就业`,
                    type: "map",
                    map: "China",
                    data: data,
                    layoutCenter: ["48%", "53%"],
                    layoutSize: "110%",
                    itemStyle: {
                        borderType: "solid",
                        borderWidth: 0.4,
                        areaColor: "rgba(145, 195, 255, 1)",
                        opacity: 1
                    },
                    roam: true,
                    label: {
                        show: true,
                        position: "inside",
                        fontSize: 12,
                        borderWidth: 0,
                        padding: [5, 5, 5, 5],
                        borderRadius: [0, 0, 0, 0],
                        formatter: "{b}"
                    }
                }],
            };
            chart.setOption(option);
            this.chart = chart;

        };

    }

    componentDidUpdate(prevProps) {
        // 检查year或type是否有变化
        if (prevProps.year !== this.props.year) {
            this.updateChart();
        }
    }
    async getData(year) {
        // 根据year获取数据
        try {
            const response = await Axios.get(`/${year}/chinamap`);
            // console.log(response);
            const data = await response.data;
            // console.log(data);
            return data;
        } catch (error) {
            console.error('Error fetching data:', error);
            return null;
        }
    }
    async updateChart() {
        // 根据新的props更新图表
        const { year } = this.props;
        // 假设我们根据year和type获取新的数据
        const newdata = await this.getData(year);

        // 更新图表
        this.chart.setOption({
            title: {
                text: `${year}本科全国就业`,

            },
            series: [{
                name: `${year}本科全国就业`,
                data: newdata
            }],
        });
    }
    componentWillUnmount() {
        // 组件卸载时移除脚本
        if (this.geoJsonScript) {
            document.body.removeChild(this.geoJsonScript);
        }
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

export default withRouter(ChinaMap);