
import fetch from 'node-fetch';
import * as signalR from "@microsoft/signalr";
import { streamerCard } from './types/streamer-card';


const ip = '127.0.0.1';
const uri = `https://krinzh.com/twitch-api`;




export async function SendRequest()
{
       const response = await fetch(uri);
       const jsonData = await response.json();
       return jsonData;
}

export async function GetStreamersStream()
{      
           
       
}


/*
import grpc from 'grpc';
import { SimpleServiceDefinition } from "../protos/greet_pb";
export default new services.SongsClient();
*/

// const transport = createGrpcWebTransport({
//         baseUrl: "https://krinzh.com",
// });
//
// var htmlContent = "";
// const client = createCallbackClient(HTMLService, transport);
// client.getIndex({}, (err:any, res:any) => {
//         if (!err) {
//                 htmlContent = res.content;
//         }
// });