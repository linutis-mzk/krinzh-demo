import React, {MouseEventHandler, useEffect, useState} from 'react';
import styles from './styles/Friendpage.module.scss';
import playerStyles from './styles/TwitchPlayer.module.scss';
import img_tyler1 from '../../images/tyler1.jpg';
import img_twitch from '../../images/twitch-logo.png';
import {GetStreamersStream} from "../../util/networking/send-request";
import {type streamerCard} from "../../util/networking/types/streamer-card";
import * as signalR from "@microsoft/signalr";
import {HubConnection} from "@microsoft/signalr";
import {TwitchEmbed, TwitchEmbedLayout, TwitchPlayer} from 'twitch-player';

export const Friendpage =()=> {
    
    
   const [userAuthorised, setUserAuthorised] = useState<boolean>(true);
   const [latestStreamerCard, UpdateLatestStreamerCard] = useState<streamerCard>(null);
   const [streamerCards, updateStreamerCards] = useState<streamerCard[]>([]);
   const [streamerOnPlayer, changeStreamerOnPlayer] = useState<string>(null);

    useEffect(() => {
        
        const subscribeToStreamersOnline = async (conn : signalR.HubConnection) => {
            //
            // Updates streamers online status
            //
            return await conn.start().then(() => {
                connection.stream("GetStreamers").subscribe({
                    next: (item : streamerCard) => {
                        UpdateLatestStreamerCard(item);
                    },
                    complete: () => {
                        console.log("Stream completed");
                    },
                    error: (err) => {
                        console.log("Error: ",err);
                    }
                });
            }).catch(err => console.log(err));
        }
        const connection = new signalR.HubConnectionBuilder().withUrl("/twitch-api").build();
        subscribeToStreamersOnline(connection);       
    }, []);

    useEffect(() => {
        
        // Update streamer cards with new value
        if (latestStreamerCard != null) {
            if (streamerCards.length == 0) {
                updateStreamerCards([latestStreamerCard]);
            } else {
                var blFound = false;
                var streamerCardsTemp = streamerCards.map(streamer => {
                    
                    if (streamer.user_name === latestStreamerCard.user_name) {
                        streamer.is_online = latestStreamerCard.is_online;
                        blFound = true;
                    }
                    return streamer;
                });
                streamerCardsTemp = blFound ? streamerCardsTemp : [latestStreamerCard, ...streamerCards];
                streamerCardsTemp.sort((a, b) => sortOnlineFirst(a, b));
                
                updateStreamerCards(streamerCardsTemp);
            }
        }
        
    }, [latestStreamerCard])
    
    useEffect ( ()=>{
        createTwitchPlayer(streamerOnPlayer);
    }, [streamerOnPlayer])
   
   
   const createStreamerSidePanel =()=> {
      return (
         <div className={styles.streamerSidePanel}>
            <div className={styles.labelContainer}>
               <label>Streamers</label>
            </div>
            <div className={userAuthorised ? styles.cardContainer : styles.cardContainer_unauthorised}>
                {streamerCards.length > 0 ? getStreamerNavigationCards() : <label>No streamer information available.</label>}
            </div>
         </div>
      );
   }
   const eventStreamerCardClicked = (streamer_name: string) => {
        changeStreamerOnPlayer(streamer_name);
   }
   
   
   const getStreamerNavigationCards =()=> {

        
       if (userAuthorised)
       {
           return (
                streamerCards.map(streamer =>
                   <div key={streamer.user_name} className={streamer.is_online ? styles.streamerCard : styles.streamerCard_offline} 
                        onClick={() => eventStreamerCardClicked(streamer.user_name)}>
                        <span className={styles.streamerCardInner}>
                            <img  src={streamer.profile_image_url} alt="Image failed to load."></img>
                            <label className={styles.streamerName}>{streamer.custom_alias}</label>
                        </span>
                   </div>
                ) 
           );
               
       }
       else
       {
           return (
              <span className={styles.buttonTwitchLogin}>
                  <img src={img_twitch} draggable="false" />
                  <a>Login with twitch</a>
              </span>
           );
       }
   }
   
   function sortOnlineFirst(a : streamerCard, b : streamerCard) {
        
       if (a.is_online && !b.is_online) {
           return -1;
       }
       else if (!a.is_online && b.is_online)
       {
           return 1;
       }
       return 0;
   }
   function createTwitchPlayer(streamer_name: string){
       document.getElementById("twitch-player").innerHTML = "";
       if (streamer_name == null) {
           return;
       }
       else
       {
           const embed = new TwitchEmbed("twitch-player", {
               width: 1280,
               height: 720,
               channel: streamer_name,
               allowFullscreen: true,
               autoplay: true,
               layout: TwitchEmbedLayout.VIDEO
           });
       }
       
   }
   
   
    return (
        <div className={styles.rootDiv}>
            {createStreamerSidePanel()}
            <div className={playerStyles.twitchPlayer} id="twitch-player"></div>
        </div>
    );
}

