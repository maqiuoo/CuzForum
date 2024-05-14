import React, { Component } from 'react';
import { Link, Redirect, withRouter } from 'react-router-dom';
import Axios from '../js/axios';
import { connect } from 'react-redux';
import { showToast } from '../actions/toast';
import { updateHeader } from '../actions/header';

class Register extends Component {
  constructor(props) {
    super(props);
    this.state = {
      redirect_url: '/',
      enable_redirect: false
    };
    this.usernameRef = React.createRef();
    this.passwordRef = React.createRef();
  }

  register() {
    const username = this.usernameRef.current.value;
    const password = this.passwordRef.current.value;
    Axios.post('/register', {
      username: username,
      password: password
    }).then(response => {
      const data = response.data;
      if (data.code === 200) {
        this.props.dispatch(showToast('注册成功，正在跳转到登录页面...'));

        if (data.detail && typeof data.detail.token !== 'undefined') {
          localStorage.setItem("accessToken", data.detail.token);
          localStorage.setItem("username", username);
          localStorage.setItem("admin", data.detail.admin);
          this.props.dispatch(updateHeader());
          Axios.defaults.headers.common['Authorization'] = "Bearer " + data.detail.token;
          this.setState({ enable_redirect: true });
        }
        setTimeout(() => {
          this.props.history.push('/login');
        }, 1000); // 1000毫秒后执行跳转
      }
    }).catch(err => {
      this.props.dispatch(showToast(err.message));
    });
  }

  render() {
    return (
      <section className="animated fadeIn">
        {this.state.enable_redirect && <Redirect to={this.state.redirect_url} />}
        <table border="0" className="login-register">
          <caption><h3>注册</h3></caption>
          <tbody>
            <tr>
              <th>用户名</th>
              <td><input type="text" ref={this.usernameRef} placeholder="用户名" /></td>
            </tr>
            <tr>
              <th>密码</th>
              <td><input type="password" ref={this.passwordRef} placeholder="密码" /></td>
            </tr>
            <tr>
              <td colSpan="2" valign="middle">
                <div className="action">
                  <span>已有帐号？<Link to="/login">去登录</Link></span>
                  <button onClick={() => this.register()}>注册</button>
                </div>
              </td>
            </tr>
          </tbody>
        </table>
      </section>
    );
  }
}

export default connect()(withRouter(Register));