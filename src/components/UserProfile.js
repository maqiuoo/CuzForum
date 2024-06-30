import React, { Component } from 'react';
import { Link, withRouter } from 'react-router-dom';
import { connect } from 'react-redux';
import { showToast } from '../actions/toast';
import Loading from './Loading';
import DefaultAvatar from '../imgs/default-avatar.jpg';
import Axios from '../js/axios';
import moment from 'moment';
import 'moment/locale/zh-cn.js';

class UserProfile extends Component {
  constructor(props) {
    super(props);
    this.state = {
      username: this.props.match.params.username,
      loading_info: true,
      loading_topics: true,
      loading_comments: true,
      info: {},
      topics: [],
      comments: []
    }
  }

  componentDidMount() {
    // Load user info
    Axios.get('/user/' + this.state.username)
      .then(({ data }) => {
        if (data.code === 200) {
          this.setState({
            loading_info: false,
            info: data.detail
          })
        } else {
          this.props.dispatch(showToast(data.description))
        }
      })
      .catch(err => this.props.dispatch(showToast(err.toString())));

    // Load user topics
    Axios.get('/user/' + this.state.username + '/topics', {
      params: {
        pageNo: 1,
        pageSize: 7
      }
    }).then(({ data }) => {
      if (data.code === 200) {
        // console.log("detail:",data.detail); // Log the response to inspect it
        // console.log("username:", this.state.username);

        this.setState({
          loading_topics: false,
          topics: this.state.topics.concat(data.detail.content)
        })
      } else {
        // console.log("username:", this.state.username);

        this.setState({
          loading_topics: false
        })
        this.props.dispatch(showToast(data.description))
      }
    }).catch(err => {
      this.props.dispatch(showToast(err.toString()))
    });

    // Load user comments
    Axios.get('/user/' + this.state.username + '/comments', {
      params: {
        pageNo: 1,
        pageSize: 7
      }
    }).then(({ data }) => {
      if (data.code === 200) {
        // console.log("username:", this.state.username);

        this.setState({
          loading_comments: false,
          comments: this.state.comments.concat(data.detail.content)
        })
      } else {
        // console.log("username:", this.state.username);

        this.setState({
          loading_comments: false
        })
        this.props.dispatch(showToast(data.description))
      }
    }).catch(err => this.props.dispatch(showToast(err.toString())));
  }

  render() {
    let topicsHtml;

    if (this.state.topics.length > 0) {
      // console.log("topics:",this.state.topics); // Log the topics to inspect them
      // 过滤掉未定义的元素并记录它们
      const validTopics = this.state.topics.filter((topic, index) => {
        if (topic === undefined) {
          console.error(`Topic at index ${index} is undefined:`, topic);
          return false;
        }
        return true;
      });

      if (validTopics.length > 0) {
        topicsHtml = validTopics.map((v, i) => {
          if (!v.id) {
            console.error('Topic is missing id:', v); // Log the problematic topic
            return null; // Skip rendering this topic
          }
          return (
            <tr key={i}>
              <td width="80%"><Link to={'/topic/' + v.id}>{v.title}</Link></td>
              <td>{moment(v.inTime).fromNow()}</td>
            </tr>
          );
        }).filter(topic => topic !== null); // Filter out null entries
      } else {
        topicsHtml = (
          <tr>
            <td colSpan="2">暂无话题</td>
          </tr>
        );
      }
    } else {
      topicsHtml = (
        <tr>
          <td colSpan="2">暂无话题</td>
        </tr>
      );
    }

    let commentsHtml;

    if (this.state.comments.length > 0) {
      // 过滤掉未定义的元素并记录它们
      const validComments = this.state.comments.filter((comment, index) => {
        if (comment === undefined) {
          console.error(`Comment at index ${index} is undefined:`, comment);
          return false;
        }
        return true;
      });

      if (validComments.length > 0) {
        // console.log("validComments",validComments); // Log the valid comments to inspect them
        commentsHtml = validComments.map((v, i) => {
          if (!v.id) {
            console.error('Comment is missing id', v); // Log the problematic comment
            return null; // Skip rendering this comment
          }
          if (!v.topic) {
            console.error('Comment is missing topic:', v); // Log the problematic comment
            return null; // Skip rendering this comment
          }
          return (
            <tr key={i}>
              <td width="80%"><Link to={'/topic/' + v.topicId}>{v.content}({v.topic.title})</Link></td>
              <td>{moment(v.inTime).fromNow()}</td>
            </tr>
          );
        }).filter(comment => comment !== null); // Filter out null entries
      } else {
        commentsHtml = (
          <tr>
            <td colSpan="2">暂无评论</td>
          </tr>
        );
      }
    } else {
      commentsHtml = (
        <tr>
          <td colSpan="2">暂无评论</td>
        </tr>
      );
    }
    return (
      <div>
        {
          this.state.loading_info ?
            <Loading />
            :
            <section className="user-info animated fadeIn">
              <div className="info">
                {
                  this.state.info
                    ? <div>
                      {/* <img src={this.state.info.avatar ? this.state.info.avatar : DefaultAvatar} className="avatar-lg" alt="avatar" /> */}
                      <img src={this.state.info.avatar ? `data:image/png;base64,${this.state.info.avatar}` : DefaultAvatar} className="avatar-lg" alt="avatar" />
                      <ul className="profile">
                        <li>积分 {this.state.info.score}&nbsp;&nbsp;<Link to="/top100">TOP100</Link></li>
                        <li className="username">{this.state.info.username}</li>
                        <li><Link to={'/user/' + this.state.username + '/collects'}>我的收藏</Link></li>
                        <li>{this.state.info.email}</li>
                        <li><a href={this.state.info.website}>{this.state.info.website}</a></li>
                        <li className="bio">{this.state.info.bio}</li>
                      </ul>
                    </div>
                    : <p>用户不存在</p>
                }
              </div>
              <div className="topics-comments">
                {
                  this.state.loading_topics
                    ? <Loading />
                    : <div className="topics">
                      <table border="0">
                        <caption>{this.state.username} 的话题</caption>
                        <tbody>
                          {topicsHtml}
                          <tr>
                            <td colSpan="2" align="right"><Link to={'/user/' + this.state.username + '/topics'}>查看更多&gt;&gt;</Link></td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                }
                {
                  this.state.loading_comments
                    ? <Loading />
                    : <div className="comments">
                      <table border="0">
                        <caption>{this.state.username} 的评论</caption>
                        <tbody>
                          {commentsHtml}
                          <tr>
                            <td colSpan="2" align="right"><Link to={'/user/' + this.state.username + '/comments'}>查看更多&gt;&gt;</Link></td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                }
              </div>
            </section>
        }
      </div>
    )
  }
}

export default connect((state) => {
  return {}
})(withRouter(UserProfile))