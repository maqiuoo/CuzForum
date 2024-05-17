import React from 'react';
import ReactDOM from 'react-dom';
import registerServiceWorker from './registerServiceWorker';
import './less/app.less';

import {
  HashRouter,
  Switch,
  Route
} from 'react-router-dom';

import { Provider } from 'react-redux';
import store from './store';

import Toast from './components/Toast';
import Home from './components/Home';
import CollectList from './components/CollectList';
import UserTopics from './components/UserTopics';
import UserComments from './components/UserComments';
import TopicDetail from './components/TopicDetail';
import Top100 from './components/Top100';
import Login from './components/Login';
import Register from './components/Register';
import UserProfile from './components/UserProfile';
import CreateTopic from './components/CreateTopic';
import EditTopic from './components/EditTopic';
import UserSettings from './components/UserSettings';
import Error from './components/Error';
import NotMatch from './components/NotMatch';

import DefaultLayout from './components/DefaultLayout';
import ChinaMap from './components/ChinaMap';
import RadarChart2021 from './components/RadarChart2021';
import PieChart from './components/PieChart';
import HistogramChart2021 from './components/HistogramChart2021';
import MobilityChart from './components/MobilityChart';
import WordCloud from './components/WordCloud';
import FunnelChart from './components/FunnelChart';


ReactDOM.render(
  <Provider store={store}>
    <HashRouter>
      <div>
        <Toast />
        <Switch>
          <DefaultLayout exact path='/' component={Home} />
          <DefaultLayout path='/topic/create' component={CreateTopic} />
          <DefaultLayout path='/topic/edit/:id' component={EditTopic} />
          <DefaultLayout path='/topic/:id' component={TopicDetail} />
          <DefaultLayout path='/error' component={Error} />
          <DefaultLayout path='/top100' component={Top100} />
          <DefaultLayout path='/login' component={Login} />
          <DefaultLayout path='/register' component={Register} />
          <DefaultLayout path='/user/settings' component={UserSettings} />
          <DefaultLayout path='/user/:username/collects' component={CollectList} />
          <DefaultLayout path='/user/:username/topics' component={UserTopics} />
          <DefaultLayout path='/user/:username/comments' component={UserComments} />
          <DefaultLayout path='/user/:username' component={UserProfile} />

          {/* 可视化部分 */}
          <DefaultLayout path='/wordcloud/' component={WordCloud} />
          <DefaultLayout path='/2021/chinamap/' component={() => <ChinaMap year="2021" />} />
          <DefaultLayout path='/2021/mobility/' component={() => <MobilityChart year="2021" />} />
          <DefaultLayout path='/2021/funnel/danwei/' component={() => <FunnelChart year="2021" t="单位" />} />
          <DefaultLayout path='/2021/funnel/hangye/' component={() => <FunnelChart year="2021" t="行业" />} />
          <DefaultLayout path='/2021/pie/' component={() => <PieChart year="2021" />} />
          <DefaultLayout path='/2021/radar/' component={RadarChart2021} />
          <DefaultLayout path='/2021/histogram/' component={HistogramChart2021} />



          <Route component={NotMatch} />
        </Switch>
      </div>
    </HashRouter>
  </Provider>
  , document.getElementById('root'));
registerServiceWorker();
