import React,{Component} from 'react';
import {Link} from "react-router-dom";
import ViewWay from '../ViewWay/ViewWay';
import Fab from '@material-ui/core/Fab';
import NavigationIcon from '@material-ui/icons/Navigation';
import './HomePage.css';

export default class HomePage extends React.Component{

    render(){
        return (<div className="routing-image">
                 <div className="routing-buttons">
                    <Fab className="fab" variant="extended">
                      <Link className="link" to="/Flight-schedule">לוח זמני טיסות</Link>
                    </Fab>
                    <Fab className="fab" variant="extended">
                      <NavigationIcon style={{marginRight:70}}/>
                      <i className="fas fa-map-marker-alt"></i>
                      <Link className="link" to="/Navigat">ניווט</Link>
                    </Fab>
                    <Fab className="fab" variant="extended">
                      <Link className="link" to="/View-map">צפייה במפה</Link>
                    </Fab>
             </div>
           </div>

           );
    }
    
}

