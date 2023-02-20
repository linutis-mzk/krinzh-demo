import React, {MouseEventHandler, useEffect, useState} from 'react';
import styles from './styles/Homepage.module.scss';
import {Outlet, useNavigate, useLocation} from "react-router-dom";




export const Homepage =()=> {


   const [buttonsDisabled, setButtonsDisabled] = useState(false);
   const [nextPage, setNextPage] = useState("/none");
   const navigate = useNavigate();
   const currentRoute = useLocation();

   /**
    * The function controls how Creator Bio is displayed and closed. UseLocation is used 
    * to determine whether Creator Bio window should be opened or closed.
    * 
    * @param event Caller element event
    */
   const event_ParentDiv_Click =(event:React.MouseEvent)=> {
      
      // Prevents multiple event handlers being called from overlapping
      // HTML elements.
      event.stopPropagation();
      
      let creatorBioShown = currentRoute.pathname.localeCompare("/creator") == 0;
      if (event.currentTarget.tagName.localeCompare("HEADER") == 0) 
      {
         navigate(creatorBioShown ? "/" : "/creator");
      }   
      else 
      {
         navigate( "/");
      }
   }
   
   const event_UserSelect_Click =(event:React.MouseEvent)=> {
      event.currentTarget.className = styles.active;
      setButtonsDisabled(true);
      
      // Set navigations to the next page after animation has completed
      switch(event.currentTarget.innerHTML){
         case ("Friend"):
            setNextPage("/friendpage");
            break;
      }
   }
   const event_UserSelect_AnimationEnd =(event:React.AnimationEvent)=> {
      if (buttonsDisabled)
      {
         event.currentTarget.parentElement.remove();
         navigate(nextPage)
      }
   }
    
    return (
        <div className={styles.parentDiv} > 
            <header className={styles.header} >
               <label>
                  CREATED BY LINAS SIDLAUSKAS CREATED BY LINAS SIDLAUSKAS CREATED BY LINAS SIDLAUSKAS CREATED BY LINAS SIDLAUSKAS 
                  CREATED BY LINAS SIDLAUSKAS CREATED BY LINAS SIDLAUSKAS CREATED BY LINAS SIDLAUSKAS CREATED BY LINAS SIDLAUSKAS&nbsp;
               </label>
               <label>
                  {/* To allow animation a second label is required to replace the first one.*/}
                  CREATED BY LINAS SIDLAUSKAS CREATED BY LINAS SIDLAUSKAS CREATED BY LINAS SIDLAUSKAS CREATED BY LINAS SIDLAUSKAS
                  CREATED BY LINAS SIDLAUSKAS CREATED BY LINAS SIDLAUSKAS CREATED BY LINAS SIDLAUSKAS CREATED BY LINAS SIDLAUSKAS&nbsp;
               </label>
            </header>
            <Outlet /> {/* Necessary for useNavigate to work. */}
           
           
            <div className={styles.buttonsContainer}>
              <button onClick={event_UserSelect_Click} disabled={buttonsDisabled} onAnimationEnd={event_UserSelect_AnimationEnd}>
                 Friend
              </button>
            </div>
        </div>
    );
}




