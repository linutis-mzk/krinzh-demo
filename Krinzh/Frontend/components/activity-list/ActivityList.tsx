import React from 'react';
import {useState, useEffect} from 'react';
import styles from './styles/ActivityList.module.scss';
import {ActivityListItem} from './ActivityListItem'
import { SendRequest } from '../../util/networking/send-request';




const category = {
  default: 0
}


export const ActivityList =()=> {

  const [getState, setState] = useState({
    mouse_x: 0,
    mouse_y: 0,
    selected_acitivties_count: 0,
    activity_selected: 0,
    details_view: false,
    selected_activities: [false, false, false, false, false, false],
    popup_delete_visible: false
  });
  const [getActivities, setActivities] = useState([]);




  useEffect(() => {
    SendRequest().then(data =>
      setActivities(data)
    );
   }, [])


  const activityRightClicked =(event, id)=> 
    setState({ ...getState, mouse_x: event.clientX, mouse_y: event.clientY, popup_delete_visible: !getState.popup_delete_visible});



  const activityLeftClicked =(event, pos)=> {

    var selected_count = 0;
    var details_view = false;
    var new_selected_activities = getState.selected_activities; 


    if (getState.selected_activities.length > 0){
      new_selected_activities[pos] = !new_selected_activities[pos];

      // This count number of activities selected then decides whether to show details or not.
      for (const bool of new_selected_activities){
        if (bool){
          selected_count++;
        }
        if (selected_count === 2) break;
      }

      switch(selected_count) {
        case 0:
          details_view = false;
          break;
        case 1:
          details_view = true;
          break;
        default:
          details_view = false;
      }
    }

    setState({ ...getState, selected_acitivties_count: selected_count, selected_activities:  new_selected_activities, details_view: details_view, activity_selected: pos});

  }

  
  const redoPosition =(array)=> {
    array.forEach((entry, index) =>{
      if (entry.position !== index){
        entry.position = index;
      }    
    });
  }

  const deleteLeftClicked =()=> {

    var old_selected_activities = getState.selected_activities;

    // creates a new selected_activites array that excludes selected values.
    var new_selected_activities = old_selected_activities.filter(function(value, index, arr){ 
      return !value;
    });

    var filtered = getActivities.filter(function(value, index, arr){ 
      return !old_selected_activities[index];
    });

    redoPosition(filtered);
    setState({ ...getState, selected_activities: new_selected_activities, selected_acitivties_count:0, details_view: false});
    setActivities(filtered);
  
  } 

  const renderActivityList =()=>{
    return (
    <ul style={{width: getListWidth() }}>
      {Object.entries(getActivities).map(([key, field]) => 
        <ActivityListItem key={key} position={field.position} selected={getState.selected_activities[field.position]} description={field.description} onClick={(event) => activityLeftClicked(event, field.position)} onContextMenu={(event) => activityRightClicked(event, field.position)}></ActivityListItem>)}
    </ul>
    );
  }

  const renderDetailsView =()=> {

    var activity = getActivities[getState.activity_selected];
    if (getState.details_view){
      
      return (
        <div className={styles.infoDiv}>
            <div className={styles.infoHeader}>
              <div className={styles.infoHeaderLeft}>
                {activity.date}
                <div className={styles.bulletPoint} />
                {activity.category}
              </div>
              <div className={styles.infoHeaderRight}>
                {activity.done ? "Completed" : "Incomplete"}
              </div>
            </div>
            <h3>{activity.description}</h3>
            <p>{activity.description_extra}</p>
            <div className={styles.infoButtonContainer}>
              <input  type="button" className={styles['buttonStandard_view']} />
              <input type="button" className={styles['buttonStandard_delete']} onClick={deleteLeftClicked} />
            </div>
          </div>
      );
    }
    else{
      return (getState.selected_acitivties_count > 0) ? 
        <div className={styles.infoDiv}>
          <div className={styles.altText}>Multiple items selected.</div> 
          <div className={styles.infoButtonContainer}>

            <input  type="button" className={styles['buttonStandard_delete']} onClick={deleteLeftClicked} />
          </div>
        </div>
        : null;
    }
  }
  
  const getListWidth =()=> {
    
    var width_perc;

    switch(getState.selected_acitivties_count) {
      case 0:
        width_perc = '100%';
        break;
      case 1:
        width_perc = '50%'; 
        break;
      default:
        width_perc = '75%';
    }
    return width_perc;
  }

   
  return (
      <div className={styles.outerDiv}>
          {/* Left-side of the Activity */}
          {renderActivityList()}
          {/* Right-side of the Activity */}
          {renderDetailsView()}
      </div> 
  );   
}

