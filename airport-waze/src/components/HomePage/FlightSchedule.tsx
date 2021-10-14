import React,{Component} from 'react';
import Button from '@material-ui/core/Button';
import Fab from '@material-ui/core/Fab';
import './HomePage.css';

export default class FlightSchedule extends React.Component{
    render(){
        return(
         <div className="flights-image">
             <div className="flights">
                <Fab className="fab" variant="extended" href="https://www.iaa.gov.il/airports/ben-gurion/flight-board/?flightType=arrivals">
                   נחיתות
                </Fab>
                <Fab className="fab" variant="extended" href="https://www.iaa.gov.il/airports/ben-gurion/flight-board/?flightType=arrivals">
                   המראות
                </Fab>
            </div>
         </div>
        )
    }
}