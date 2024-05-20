import React, { Component } from 'react';
import { Link } from 'react-router-dom';

class Footer extends Component {
  render() {
    return (
      <div className="footer">
        <Link to="/visualization" >就业快看</Link>&nbsp;|&nbsp;
        <Link to="/viral">热点讯息</Link>
      </div>
    )
  }
}

export default Footer;