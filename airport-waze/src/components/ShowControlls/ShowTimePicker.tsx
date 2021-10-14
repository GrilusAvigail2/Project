import React from 'react';
import TextField from '@material-ui/core/TextField';
import { MuiPickersUtilsProvider, TimePicker } from '@material-ui/pickers';
import './ShowControllers.css';


export interface ITimePickersProps {
  //שעת הטיסה לפי בחירת המשתמש
  selectflightTime: (time: string) => void;
}


export default class ShowTimePicker extends React.Component<ITimePickersProps> {

  constructor(props: ITimePickersProps) {
    super(props);
  }

  onSelectTime(event: React.ChangeEvent<HTMLTextAreaElement | HTMLInputElement>) {
    let time = event.target.value;
    this.props.selectflightTime(time);
  }

  render() {
    return (
      <div>
        <div className="time-pickers">
          <TextField
            id="time"
            label="הכנס שעת טיסה"
            type="time"
            defaultValue={new Date().toLocaleTimeString()}
            InputLabelProps={{
              shrink: true,
            }}
            inputProps={{
              step: 300, // 5 min
            }}
            style={{ width: 180 }}
            onChange={(event) => this.onSelectTime(event)}
            
          />
        
{/*           
              <TimePicker
                label="Basic example"
                value={new Date().toLocaleTimeString()}
                onChange={(event) => this.onSelectTime(event)}
                ampm={false}
               
              /> */}
            
        </div>
      </div>
    );
  }

}