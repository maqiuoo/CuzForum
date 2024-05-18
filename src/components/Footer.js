import React, { Component } from 'react';
import { Link } from 'react-router-dom';

class Footer extends Component {
  render() {
    return (
      <div className="footer">
        <Link to="/visualization" >就业快看</Link>&nbsp;|&nbsp;
        <a href="/wordcloud">热点讯息</a>
      </div>
    )
  }
}

export default Footer;