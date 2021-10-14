import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './components/App/App';
import * as serviceWorker from './serviceWorker';
import ViewWay from './components/ViewWay/ViewWay';
import HomePage from './components/HomePage/HomePage';
import { BrowserRouter,Switch,Route,Link} from "react-router-dom";
import ViewMap from './components/HomePage/ViewMap';
import FlightSchedule from './components/HomePage/FlightSchedule';


ReactDOM.render((
  <BrowserRouter>
    <div>
      <Switch>
        <Route exact path="/" component={HomePage} />
        <Route exact path="/Flight-schedule" component={FlightSchedule} />
        <Route exact path="/Navigat" component={ViewWay} />
        <Route exact path="/View-map" component={ViewMap} />
      </Switch>
    </div>
  </BrowserRouter>
), document.getElementById('root'));
// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.register();

