import { DateTimePicker } from '@material-ui/pickers';
import React, { Component } from 'react';
// import { Item } from 'semantic-ui-react';
import { IOption, IPoint } from '../Interfaces';
import ShowCombobox from '../ShowControlls/ShowCombobox';
import ShowTimePicker from '../ShowControlls/ShowTimePicker';
import Alert from '@material-ui/lab/Alert';
import './ViewWay.css';
import { write } from 'fs';
import { Height } from '@material-ui/icons';
import logo from '../LogoAirWay.jpg';

export default class ViweWay extends React.Component {

    id: string = " ";
    //מערך המכיל את נקודת המקור ונקודת היעד שיעודכנו לפי בחירת המשתמש
    points: Array<IOption | null> = new Array<IOption | null>();
    idSource: number | undefined = 0;
    idTarget: number | undefined = 0;
    isEnoughTime = " ";
    flightTime: string = new Date().toLocaleTimeString();
    lengthEdges: Array<number> = new Array<number>();
    pointsInWay: Array<IPoint> = new Array<IPoint>();

    state = {
        //רשימה המכילה את הנקודות 
        pointsList: new Array<IOption>(),
        //מערך שמכיל את הנקודות לפי תוצאות הסינון
        result: new Array<IPoint>(),
        // isEnoughTime:" ",   
    };

    async componentDidMount() {
        console.log("componentDidMount");
        this.lengthEdges.push(1);
        //database מלוי לפי הנתונים מה
        let data = await this.getAllPoints();
        this.setState({ pointsList: data })

        //   this.setState({result:data})
    }

    //פונקציות קבלת/שליחת נתונים מהשרת/לשרת

    //GET פונקציית
    //databaseהמחזירה את כל הנקודות מה 
    async getAllPoints() {
        let array: Array<IOption> = new Array<IOption>();
        await fetch("http://localhost:50520/api/Point/getAll")
            .then(res => res.json())
            .then(data => {
                data.map((item: any) =>
                    array.push({ name: item.pointName, id: item.pointId }))
            }).catch();
        return array;
    }

    //GET פונקציית 
    //מחזירה את הנקודות שהכילו את המחרוזת שהמשתמש הכניס לפי התשובה שהתקבלה מהשרת
    async getPointById() {
        let array: Array<IOption> = new Array<IOption>();
        await fetch("http://localhost:50520/api/Point/byId/" + this.id)
            .then(res => res.json())
            .then(data => {
                data.map((item: any) =>
                    array.push({ name: item.name, id: item.id }))
            }).catch();
        return array;
    }

    //GET פונקציית
    //פונקציה המחזירה את כל הנקודות המרכיבות את המסלול הקצר ביותר
    //controllerשהופעלה ב findWay מתוך המסלול שהתקבל מהפונקציה  
    async getPointsInWay() {
        let array: Array<IPoint> = new Array<IPoint>();
        debugger;
        await fetch("http://localhost:50520/api/Point/viewWay/" + this.idSource + "/" + this.idTarget + "/" + this.flightTime)
            .then(res => res.json())
            .then(data => {
                debugger;
                data.map((item: any) =>
                    array.push({ name: item.name, length: item.length, walkingTime:item.walkingTime}))
                //    this.result=array;
            }).catch();
        return array;
    }

    //בדיקה אם שעת הטיסה לא עברה כבר  ועדכון השעה, אם עברה -הצגת הודעה למשתמש
    updateFlightTime = (time: string) => {
        debugger; 
        let t = new Date()
        t.setDate(Date.now());
        t.setTime(parseInt(time));
        let timesplit =  t.getTime().toString().split(":");
        let curTime = new Date().toLocaleTimeString();
        let curTimeSplit = curTime.split(":");
       
        if(timesplit[0]>curTimeSplit[0] || timesplit[0]==curTimeSplit[0] && timesplit[1]>curTimeSplit[1] ||  timesplit[0]==curTimeSplit[0] && timesplit[1]==curTimeSplit[1] && timesplit[2]==curTimeSplit[2] ){
           this.flightTime=time;  
        }
        else{
            alert("שעת הטיסה שנבחרה עברה, בחר שוב שעת טיסה")
        }   
    }

    //עדכון נקודות המקור והיעד והפעלת הפונקציה למציאת הדרך הקצרה ביותר
    changePoints = async (sourceAndTarget: Array<IOption | null>) => {
        this.isEnoughTime = " ";
        this.points = sourceAndTarget;
        if (this.points.length == 2) {
            this.idSource = sourceAndTarget[0]?.id;
            this.idTarget = sourceAndTarget[1]?.id;
            let points = await this.getPointsInWay();
            // this.pointsInWay = points;
            debugger;
            // this.lengthEdges = await this.getLengthEdgesInWay();
            if (points.length == 1 && points[0].name=="no time") {
                this.isEnoughTime = "הזמן הנותר עד לטיסה אינו מספיק לעבור את כל המסלול";
                this.setState({ result: points })
            }
            else{
               this.setState({ result: points }) 
            }
            
        }
    }

    //הצגת הזמן שלוקח לעבור כל קטע במסלול
    showTime(time:number){
        debugger;
        let str="";
        if(time==0){
            return " ";
        }
        if(Math.round(time/3600) > 0){
            str += Math.round(time/3600) +" שעות ";
        }
        if(time < 60){
            if(time<30){
                str += " חצי דקה ";
            }else{
                time=60;
            }   
        }
        if(time/60 > 0.5){
            str += Math.round(time/60) +" דקות ";
        }
       
        return str;
    }

    EntireRoutTime(route:IPoint[]){
        debugger;
        let routeTime=0;
        route.map((p)=>{
            if(p.walkingTime<30 && p.walkingTime>0){
                routeTime+=30;
            }else if(p.walkingTime<60 && p.walkingTime>30){
                routeTime+=60;
            }else{
                routeTime+=p.walkingTime;
            }
        });
        let time=this.showTime(routeTime);
        return time;
    }

    render() {
        console.log("render");
        let count=0;
        debugger;
        return (<div>
                <div className="show-all">
                <div className="show">
                <div className="airport-image"> 
                    <img className="airport-img" src={logo} alt="logo"  width="400" height="90"/>
                </div>
                    <div className="time-picker">
                        <ShowTimePicker selectflightTime={this.updateFlightTime}></ShowTimePicker>
                    </div>
                <div className="controllers">
                    <ShowCombobox points={this.state.pointsList} onClickChange={this.changePoints}></ShowCombobox>      
                    {this.isEnoughTime != " " ? alert(this.isEnoughTime) :<div className="show-way"><ul>{this.state.result.map((p,index) =>
                    <li>{index==0?"צא מ: "+ p.name :" לך אל: "+ p.name}<br></br>{index==0?"": p.length +" מטרים "+ this.showTime(p.walkingTime)}</li>)}</ul></div>}
                    <span className="length-route">{this.state.result.length>0 && this.state.result[0].name=="no time"?"אין מספיק זמן לעבור את כל המסלול":" אורך המסלול: " +this.EntireRoutTime(this.state.result)}</span>
                </div>
                </div>
                <div className="image"></div>
                </div>
        </div>)
    }

}