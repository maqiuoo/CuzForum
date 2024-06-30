import React, { Component } from 'react';
import { connect } from 'react-redux';
import { withRouter, Redirect } from 'react-router-dom';
import { showToast } from '../actions/toast';
import { updateHeader } from '../actions/header';
import Loading from './Loading';
import Axios from '../js/axios';

class UserSettings extends Component {
  constructor(props) {
    super(props);
    this.state = {
      loading: true,
      user: {},
      enable_redirect: false,
      redirect_url: '/login'
    }
  }
  componentWillMount() {
    const username = localStorage.getItem("username");
    if (!username) {
      this.setState({
        enable_redirect: true
      })
    } else {
      Axios.get('/user/settings/profile')
        .then(({ data }) => {
          if (data.code === 200) {
            this.setState({
              loading: false,
              user: data.detail
            });
            localStorage.setItem("isAdmin", data.detail.isAdmin);
          } else {
            this.props.dispatch(showToast(data.description))
          }
        }).catch(err => this.props.dispatch(showToast(err.toString())));
    }
  }
  save() {
    const email = this.refs.email.value;
    const website = this.refs.website.value;
    const bio = this.refs.bio.value;
    const avatar = this.refs.avatar.files[0];
  
    if (avatar && !avatar.type.startsWith('image/')) {
      this.props.dispatch(showToast('头像必须是图片格式'));
      return;
    }
  
  
    if (avatar) {
      const reader = new FileReader();
      reader.onloadend = () => {
        const base64String = reader.result.split(',')[1]; // 去掉前缀
  

  
        Axios.post("/user/settings/profile",  {
          email: email,
          website: website,
          bio: bio,
          avatar: base64String,
          headers: {
            'Content-Type': 'multipart/form-data'
          },
        }).then(({ data }) => {
          if (data.code === 200) {
            this.props.dispatch(showToast('修改成功'));
          } else {
            this.props.dispatch(showToast(data.description));
          }
        }).catch(err => this.props.dispatch(showToast(err.toString())));
      };
      reader.readAsDataURL(avatar); // 读取文件并转换为Base64
    } 
  }
  updatePassword() {
    const rawPassword = this.refs.rawPassword.value;
    const newPassword = this.refs.newPassword.value;
    if (!rawPassword || !newPassword) {
      this.props.dispatch(showToast('原密码和新密码都不能为空'));
      return;
    }
    Axios.post("/user/settings/updatePassword", {
      rawPassword: rawPassword,
      newPassword: newPassword
    }).then(({ data }) => {
      if (data.code === 200) {
        localStorage.removeItem("accessToken");
        localStorage.removeItem("username");
        this.props.dispatch(showToast('修改成功, 请重新登录'));
        this.props.dispatch(updateHeader());
        this.setState({
          enable_redirect: true
        })
      } else {
        this.props.dispatch(showToast(data.description));
      }
    }).catch(err => this.props.dispatch(showToast(err.toString())))
  }
  render() {
    return (
      <section className="animated fadeIn user-settings">
        {
          this.state.loading
            ? <Loading />
            : <div className="user-settings">
              {
                this.state.enable_redirect
                  ? <Redirect to={this.state.redirect_url} />
                  : null
              }
              <table border="0">
                <caption><h3>修改资料</h3></caption>
                <tbody>
                  <tr>
                    <th>用户名</th>
                    <td><input type="text" readOnly value={this.state.user.username} /></td>
                  </tr>
                  <tr>
                    <th>邮箱</th>
                    <td><input type="email" ref="email" defaultValue={this.state.user.email} /></td>
                  </tr>
                  <tr>
                    <th>头像</th>
                    <td><input type="file" ref="avatar" accept="image/*" /></td>
                  </tr>
                  <tr>
                    <th>个人主页</th>
                    <td><input type="text" ref="website" defaultValue={this.state.user.website} /></td>
                  </tr>
                  <tr>
                    <th valign="top">个人简介</th>
                    <td><textarea ref="bio" rows="5" defaultValue={this.state.user.bio}></textarea></td>
                  </tr>
                  <tr>
                    <td colSpan="2" align="right">
                      <button onClick={() => this.save()}>保存</button>
                    </td>
                  </tr>
                </tbody>
              </table>
              <table border="0">
                <caption><h3>修改密码</h3></caption>
                <tbody>
                  <tr>
                    <th>原密码</th>
                    <td><input type="password" ref="rawPassword" /></td>
                  </tr>
                  <tr>
                    <th>新密码</th>
                    <td><input type="password" ref="newPassword" /></td>
                  </tr>
                  <tr>
                    <td colSpan="2" align="right">
                      <button onClick={() => this.updatePassword()}>更新</button>
                    </td>
                  </tr>
                </tbody>
              </table>
            </div>
        }
      </section>
    )
  }
}

export default connect((state) => {
  return {}
})(withRouter(UserSettings))