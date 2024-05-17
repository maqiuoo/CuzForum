import React, { Component } from 'react';
import { withRouter } from 'react-router-dom';

class ChinaMap extends Component {
    echartsScript = null;
    geoJsonScript = null;
    componentDidMount() {
        // 动态加载echarts库
        const echartsScript = document.createElement("script");
        echartsScript.src = "echarts.min.js";
        echartsScript.async = true;

        document.body.appendChild(echartsScript);
        echartsScript.onload = () => {

            // 动态加载china-map-geojson库
            const geoJsonScript = document.createElement("script");
            geoJsonScript.src = "china-map-geojson.min.js"; // 请确保这里的路径是正确的
            geoJsonScript.async = true;
            document.body.appendChild(geoJsonScript);

            geoJsonScript.onload = () => {
                // console.log('echarts和china-map-geojson已加载完成');

                window.echarts.registerMap('China', window['china-map-geojson']['ChinaData']);
                const chart = window.echarts.init(this.refs.chart);
                fetch(`http://127.0.0.1:4523/m1/4485527-4132188-default/${this.props.year}/chinamap`)
                    .then(response => response.json())
                    .then(data => {
                        console.log(data);
                        const option = {
                            title: {
                                show: true,
                                text: `${this.props.year}本科全国就业`,
                                padding: [10, 5, 5, 5],
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
                                range: [0, 1600],
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
                    }).catch(error => console.error('Error fetching data:', error));
            };
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

export default withRouter(ChinaMap);