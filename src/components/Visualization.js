import React, { Component } from 'react';
import { withRouter } from 'react-router-dom';
import PieChart from './PieChart';
import ChinaMap from './ChinaMap';
import MobilityChart from './MobilityChart';
import FunnelChart from './FunnelChart';
import RadarChart from './RadarChart';
import HistogramChart from './HistogramChart';

class Visualization extends Component {
    state = {
        isScriptLoaded: false, // 初始时脚本未加载
        selectedYear: '2021', // 假设默认年份为2021
        selectedType: '本科' // 假设默认类型为本科
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

    componentWillUnmount() {
        // 组件卸载时移除脚本
        if (this.echartsScript) {
            document.body.removeChild(this.echartsScript);
        }

    }
    handleYearChange = (event) => {
        // 更新选中的年份
        this.setState({ selectedYear: event.target.value });
    }

    handleTypeChange = (event) => {
        // 更新选中的类型
        this.setState({ selectedType: event.target.value });
    }

    render() {
        const { selectedYear, selectedType, isScriptLoaded } = this.state;

        // 如果脚本未加载完成，则不渲染任何内容
        if (!isScriptLoaded) {
            return null;
        }

        // 如果选择的是硕士，并且选择的是2022年，则显示提示信息
        if (selectedType === '硕士') {
            if (selectedYear === '2022') {
                return (
                    <div className="box centered-container">
                        <p>暂无2022年硕士数据</p>
                        <select value={selectedYear} onChange={this.handleYearChange}>
                            <option value="2021">2021</option>
                            <option value="2022">2022</option>
                            <option value="2023">2023</option>
                        </select>
                        <br />
                        <br />
                        <select value={selectedType} onChange={this.handleTypeChange}>
                            <option value="本科">本科</option>
                            <option value="硕士">硕士</option>
                        </select>
                    </div>
                );
            }
            else{
                return (
                    <div className="centered-container">
                        <div className="box">
                            <select value={selectedYear} onChange={this.handleYearChange}>
                                <option value="2021">2021</option>
                                <option value="2022">2022</option>
                                <option value="2023">2023</option>
                            </select>
                            <br />
                            <br />
                            <select value={selectedType} onChange={this.handleTypeChange}>
                                <option value="本科">本科</option>
                                <option value="硕士">硕士</option>
                            </select>
                        </div>
                        <br />
                        <br />
                        <br />
                        <FunnelChart year={selectedYear} type={selectedType} t="行业" />
                        <FunnelChart year={selectedYear} type={selectedType} t="单位" />
                    </div>
                );
            }
        }
            // 脚本加载完成后，渲染组件
            return (
                <div className="centered-container">
                    <div className="box">
                        <select value={selectedYear} onChange={this.handleYearChange}>
                            <option value="2021">2021</option>
                            <option value="2022">2022</option>
                            <option value="2023">2023</option>
                        </select>
                        <br />
                        <br />
                        <select value={selectedType} onChange={this.handleTypeChange}>
                            <option value="本科">本科</option>
                            <option value="硕士">硕士</option>
                        </select>
                    </div>
                    <br />
                    <br />
                    <br />
                    <HistogramChart year={selectedYear} />
                    <RadarChart year={selectedYear} />
                    <FunnelChart year={selectedYear} type={selectedType} t="行业" />
                    <FunnelChart year={selectedYear} type={selectedType} t="单位" />
                    <MobilityChart year={selectedYear} />
                    <ChinaMap year={selectedYear} />
                    <PieChart year={selectedYear} />
                </div>
            );
        }
    }

export default withRouter(Visualization);