import React from 'react';
import styles from './styles/Calendar.module.scss';
import {useState} from 'react';
import img_select_button from '../../images/selectbuttonleft.png';


const monthNames = ["January", "February", "March", "April", "May", "June",
  "July", "August", "September", "October", "November", "December"];







export const Calendar =()=> {


  const [getState, setState] = useState({
    month:3,
    year:2022
  });



  // Switching between months, controlled by right/left arrows
  const eventMonthChanged =(event)=> {


    // This block handles the overflow of month >12
    let month = getState.month;
    let year = getState.year;
    let next_month = month === 12 ? 1 : month+1;
    let prev_month = month === 1 ? 12 : month-1;
    
    let next_year = next_month === 1 ? year+1 : year;
    let prev_year = prev_month === 12 ? year-1 : year;
    
    console.log(event.target.name);
    if (event.target.id === styles.imageSelectRight){
      setState({month: next_month, year: next_year});
      console.log("right");
    }
    else{
      setState({month: prev_month, year: prev_year});
      console.log("left");
    }
  }

  // Returns day of the week (0-6), and total number of days within a month; as a tuple
  const getMonthDays =(month, year)=> {
    let total_days =  new Date(year, month, 0).getDate();
    let first_day = new Date(year, month-1).getDay() - 1;
    if (first_day < 0) first_day = 6; 

    return [first_day, total_days];
  }

  const createTableBody =(month, year)=> {
    let first_dow = getMonthDays(month, year)[0]; //Week starts on sunday-6
    let days_total = getMonthDays(month, year)[1]; //Maximum 31
    
    let weeks_total =  Array.from(Array(6).keys()); // Creates an array of 7 elements [0,1,2,3,4,5,6]

    // Creates a table based on weeks, as each week is a single row
    let table_body = weeks_total.map(week_id => {

      return(
        <tr key={week_id} className={styles.table_row}>{
          Array.from(Array(7).keys()).map(week_day => {
            week_day = week_day+1;
            let month_day = week_id*7+week_day;
            let month_day_offset = month_day - first_dow;
            if (month_day_offset >= 1 && month_day_offset <= days_total)
              return(<td key={month_day_offset}>{month_day_offset}</td>)
            else
              return(<td key={month_day_offset} className={styles.empty_td}>{0}</td>);
          })}
        </tr>
      )

    });    
    return table_body;
  }


  return (
    <div className={styles.outer_div}>
      <header>
        <label className={styles.monthLabel}>{monthNames[getState.month-1]+", "+getState.year}</label>
      </header>
      <table className={styles.table}>
        <tbody>
          <tr>
            <th>Mon</th>
            <th>Tue</th>
            <th>Wed</th>
            <th>Thu</th>
            <th>Fri</th>
            <th>Sat</th>
            <th>Sun</th>
          </tr>
          {createTableBody(getState.month, getState.year)}
        </tbody>
      </table>
      <div className={styles.button_container}>

        <button id={styles.buttonSelectLeft} tabIndex={-1}><img src={img_select_button} onClick={eventMonthChanged} draggable="false" alt="button missing"></img></button>
        <button id={styles.buttonSelectRight} tabIndex={-1}><img id={styles.imageSelectRight} onClick={eventMonthChanged} src={img_select_button} draggable="false" alt="button missing"></img></button>

      </div>
    </div>
  );
  
}

