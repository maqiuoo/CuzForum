import React, { Component } from 'react';
import { withRouter } from 'react-router-dom';
import WordCloud from './WordCloud';
// eslint-disable-next-line
import ListInformation from './ListInformation';

class Viral extends Component {
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

    componentWillUnmount() {
        // 组件卸载时移除脚本
        if (this.echartsScript) {
            document.body.removeChild(this.echartsScript);
        }

    }

    render() {
        const { isScriptLoaded } = this.state;

        // 如果脚本未加载完成，则不渲染任何内容
        if (!isScriptLoaded) {
            return null;
        }
        // 脚本加载完成后，渲染组件
        return (
            <div className="box-centered-container">
                <WordCloud type="books" />
                <div className="list-centered-container">
                    <ListInformation type="lectures" />
                    <ListInformation type="notification" />
                </div>
            </div>
        );
    }
}

export default withRouter(Viral);