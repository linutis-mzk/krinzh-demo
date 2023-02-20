import React from 'react';
import {useState, MouseEventHandler, useEffect} from 'react';
import styles from './styles/ActivityListItem.module.scss';



interface iProps {
  position: number,
  selected: boolean;
  onClick: MouseEventHandler;
  onContextMenu: MouseEventHandler;
  description: string;
}


export const ActivityListItem = ({selected, onClick, onContextMenu, description, position} : iProps)=> {

  const [getState, setState] = useState({
    position: 0,
    date: "01/01/1990",
    category: "Category",
    description: "Description",
    description_extra: "Description Extra",
    important: false, // 0 - Low, 1 - Medium, 2 - High
    done: false,
    selected: false
  });

  useEffect(() =>{
    setState(getState => ({ ...getState, description: description, selected: selected, position: position}));
  }, [selected, description, position]); // Empty array signifies no dependencies, therefore this runs only upon ComponentMount


  return (
    <li className={getState.selected ? styles.outerLiActive : styles.outerLi } onClick={onClick} onContextMenu={onContextMenu}>
        <div className={ styles.bulletPoint }></div> 
        <p>{getState.position+" "+getState.description}</p>
    </li>
  );
}

