import React from 'react';
import ReactDOM from 'react-dom';
import styles from './index.module.scss';
import {BrowserRouter, Route, Routes} from "react-router-dom";
import {Homepage} from "./pages/homepage/Homepage";
import {CreatorBio} from "./pages/homepage/CreatorBio";
import {Friendpage} from "./pages/friendpage/Friendpage";
import {ErrorPage} from "./pages/error/ErrorPage";



document.addEventListener("contextmenu", (event) => {event.preventDefault();});




// All available routes on the website
// To add new route: 1. Insert new entry below 2. Use navigate functions from react-router-dom
const htmlToRender =
   <div className={styles.Content}>   
      <BrowserRouter>
           <Routes>
               <Route path="/" element={<Homepage />}>
                  <Route path="/creator" element={<CreatorBio />} />
               </Route>
              <Route path="/friendpage" element={<Friendpage />} />
               <Route path="*" element={<ErrorPage />} />
           </Routes>
      </BrowserRouter>
   </div>



ReactDOM.render(htmlToRender , document.getElementById('root'));