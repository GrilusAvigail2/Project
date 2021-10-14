import React, { Component } from 'react';
import TextField from '@material-ui/core/TextField';
import Autocomplete, { AutocompleteChangeReason } from '@material-ui/lab/Autocomplete'
import { withWidth, Button } from '@material-ui/core';
import { IOption } from '../Interfaces';
import './ShowControllers.css';

export interface IComboboxProps {
  //מערך המכיל את כל הנקודות בגרף
  points: Array<IOption>,
  //מצביע לפונקציה בקומפוננטת האבא
  onClickChange: (sourceAndDestination: Array<IOption|null>) => void
}

export default class ShowCombobox extends React.Component<IComboboxProps> {

  constructor(props: IComboboxProps) {
    super(props);
  }

  points = Array<IOption|null>();

  //combobox פונקציה המופעלת כאשר המשתמש בוחר נקודה מתוך ה
  onChange( event: React.ChangeEvent<{}>,value: any, type: string) {
    //של נקודת המקור יתעדכן המקור combobox אם הערך שהשתנה הוא מתוך ה
    if (type == 'source') {
       this.points[0] = value;
    }
    //של נקודת היעד יתעדכן היעד combobox אם הערך שהשתנה הוא מתוך ה
    if (type == 'destination') {
        this.points[1] = value;
    }
    //debugger;
  }

  //הפונקציה מופעלת כאשר לוחצים של כפתור חשב מסלול
  findWay(event:React.MouseEvent<HTMLButtonElement, MouseEvent>) {
    if (this.points.length == 2) {
      //onClickChange שליחת הנקודות שהמשתמש בחר, לפונקציה המוצבעת ע"י 
      this.props.onClickChange(this.points);
    }
  }

  render() {
    return (<div>
      <div className="comboboxes">
        {/*להכנסת נקודת המקור combobox פקד  */}
        <Autocomplete
          id="source-point"
          options={this.props.points}
          getOptionLabel={(option) => option.name}
          style={{ width: 300 }}
          renderInput={(params) => <TextField {...params} label="הכנס מקור" variant="outlined" />}
          onChange={(event, value) => this.onChange(event, value, 'source')}
        />
      
      {/*להכנסת נקודת היעד combobox פקד  */}
      <Autocomplete
          id="destination-point"
          options={this.props.points}
          getOptionLabel={(option) => option.name}
          style={{ width: 300 }}
          renderInput={(params) => <TextField {...params} label="הכנס יעד" variant="outlined" />}
          onChange={(event, value) => this.onChange(event, value, 'destination')}
        />
        </div>
        <div className="button">
          <Button  variant="contained" color="primary" onClick={(event:any)=>this.findWay(event)}>חשב מסלול</Button>
        </div>
    </div>);
  }
}