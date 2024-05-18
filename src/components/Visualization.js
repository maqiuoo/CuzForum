import React, { Component } from 'react';
import { withRouter } from 'react-router-dom';
import PieChart from './PieChart';
import ChinaMap from './ChinaMap';
import MobilityChart from './MobilityChart';
import FunnelChart from './FunnelChart';
import RadarChart2021 from './RadarChart2021';
import HistogramChart2021 from './HistogramChart2021';


class Visualization extends Component {
    state = {
        isScriptLoaded: false, // 初始时脚本未加载
    };

    componentDidMount() {
        const echartsScript = document.createElement("script");
        echartsScript.src = "echarts.min.js";
        echartsScript.async = true;
        document.body.appendChild(echartsScript);
        echartsScript.onload = () => {
            // 当脚本加载完成时，更新状态
            this.setState({ isScriptLoaded: true });
        };


    }

    render() {
        // 如果脚本未加载完成，则不渲染任何内容
        if (!this.state.isScriptLoaded) {
            return null;
        }

        // 脚本加载完成后，渲染组件
        return (
            <div className="centered-container">
                <HistogramChart2021 />
                <RadarChart2021 />
                <FunnelChart year="2021" t="行业" />
                <FunnelChart year="2021" t="单位" />
                <MobilityChart year="2021" />
                <ChinaMap year="2021" />
                <PieChart year='2021' />
            </div>
        );
    }
}

export default withRouter(Visualization);