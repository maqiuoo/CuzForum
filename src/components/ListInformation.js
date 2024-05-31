import React, { Component } from 'react';
import Axios from '../js/axios';

class ListInformation extends Component {
    constructor(props) {
        super(props);
        this.state = {
            items: [],
            currentPage: 1,
            itemsPerPage: 5 // 假设每页显示5项
        };
    }

    async componentDidMount() {
        const response = await Axios.get(`/${this.props.type}`);
        const data = await response.data;
        this.setState({ items: data });
    }

    setPage = (page) => {
        this.setState({ currentPage: page });
    }

    prevPage = () => {
        this.setState(prevState => ({
            currentPage: Math.max(prevState.currentPage - 1, 1)
        }));
    }

    nextPage = () => {
        this.setState(prevState => ({
            currentPage: Math.min(prevState.currentPage + 1, Math.ceil(this.state.items.length / this.state.itemsPerPage))
        }));
    }

    render() {
        const { items, currentPage, itemsPerPage } = this.state;
        const indexOfLastItem = currentPage * itemsPerPage;
        const indexOfFirstItem = indexOfLastItem - itemsPerPage;
        const currentItems = items.slice(indexOfFirstItem, indexOfLastItem);
    
        const renderItems = currentItems.map((item, index) => (
            <li key={index}>
                <a href={item.link}>{item.title}</a>
                <p>日期：{item.date}</p>
            </li>
        ));
    
        const totalPageCount = Math.ceil(items.length / itemsPerPage);
        let startPage, endPage;
    
        if (totalPageCount <= 5) {
            startPage = 1;
            endPage = totalPageCount;
        } else {
            if (currentPage <= 3) {
                startPage = 1;
                endPage = 5;
            } else if (currentPage + 2 >= totalPageCount) {
                startPage = totalPageCount - 4;
                endPage = totalPageCount;
            } else {
                startPage = currentPage - 2;
                endPage = currentPage + 2;
            }
        }
    
        const renderPageNumbers = [];
        for (let i = startPage; i <= endPage; i++) {
            renderPageNumbers.push(
                <li
                    key={i}
                    onClick={() => this.setPage(i)}
                    style={{ cursor: 'pointer', opacity: currentPage === i ? 1 : 0.5 }}
                    className={currentPage === i ? 'active' : ''}
                >
                    {i}
                </li>
            );
        }
    
        return (
            <div style={{width:'540px'}}>
                <div>
                    <h2>{this.props.type === 'lectures' ? '大咖讲座' : '通知公告'}</h2>
                    <ul className="list">
                        {renderItems}
                    </ul>
                </div>
    
                <ul id="page-numbers" className="horizontal-list">
                    <li
                        onClick={() => this.setPage(1)}
                        style={{ cursor: 'pointer', opacity: currentPage === 1 ? 0.5 : 1 }}
                    >
                        首页
                    </li>
                    <li
                        onClick={() => { if (currentPage > 1) this.setPage(currentPage - 1) }}
                        style={{ cursor: currentPage === 1 ? 'not-allowed' : 'pointer', opacity: currentPage === 1 ? 0.5 : 1 }}
                    >
                        上一页
                    </li>
                    {renderPageNumbers}
                    <li
                        onClick={() => { if (currentPage < totalPageCount) this.setPage(currentPage + 1) }}
                        style={{ cursor: currentPage === totalPageCount ? 'not-allowed' : 'pointer', opacity: currentPage === totalPageCount ? 0.5 : 1 }}
                    >
                        下一页
                    </li>
                    <li
                        onClick={() => this.setPage(totalPageCount)}
                        style={{ cursor: 'pointer', opacity: currentPage === totalPageCount ? 0.5 : 1 }}
                    >
                        尾页
                    </li>
                </ul>
            </div>
        );
    }
}

export default ListInformation;