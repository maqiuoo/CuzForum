import React, { Component } from 'react';
import { Link, withRouter, Redirect } from 'react-router-dom';
import { connect } from 'react-redux';
import { showToast } from '../actions/toast';
import { updateHeader } from '../actions/header';
import Axios from '../js/axios';

class Login extends Component {
  constructor(props) {
    super(props);
    this.state = {
      username: '',
      password: '',
      redirect_url: '/',
      enable_redirect: false
    }
  }

  handleInputChange = (event) => {
    this.setState({
      [event.target.name]: event.target.value
    });
  }

  login = () => {
    const { username, password } = this.state;
    Axios.post('/login', {
      username: username,
      password: password
    }).then(({ data }) => {
      if (data.code === 200) {
        this.props.showToast('登录成功');
        console.log(data.data);

        if (data.detail && typeof data.detail.token !== 'undefined') {
          console.log(data.detail);
          localStorage.setItem("accessToken", data.detail.token);
          localStorage.setItem("username", username);
          localStorage.setItem("admin", data.detail.admin);
          Axios.defaults.headers.common['Authorization'] = "Bearer " + data.detail.token
          this.props.updateHeader();
          this.setState({
            enable_redirect: true
          })
        }
      }
      else {
        this.props.showToast(data.msg);
      }
    }).catch(err => this.props.showToast(err.toString()));
  }

  render() {
    return (
      <section className="animated fadeIn">
        {this.state.enable_redirect ? <Redirect to={this.state.redirect_url} /> : null}
        <table border="0" className="login-register">
          <caption><h3>登录</h3></caption>
          <tbody>
            <tr>
              <th>用户名</th>
              <td><input type="text" name="username" value={this.state.username} onChange={this.handleInputChange} placeholder="用户名" /></td>
            </tr>
            <tr>
              <th>密码</th>
              <td><input type="password" name="password" value={this.state.password} onChange={this.handleInputChange} placeholder="密码" /></td>
            </tr>
            <tr>
              <td colSpan="2" valign="middle">
                <div className="action">
                  <span>没有帐号？<Link to="/register">去注册</Link></span>
                  <button onClick={this.login}>登录</button>
                </div>
              </td>
            </tr>
          </tbody>
        </table>
      </section>
    )
  }
}

const mapDispatchToProps = {
  showToast,
  updateHeader
};

export default connect(null, mapDispatchToProps)(withRouter(Login));