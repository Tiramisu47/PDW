import WebSocket, { Server } from "ws";

import { CoreController } from "./auth/core.controller";
const wss = new Server({ port: 81 });

wss.on("connection", (ws: WebSocket) => {
  ws.on("message", (message: WebSocket.MessageEvent) => {
    try {
      const data = JSON.parse(message.toString());
      console.log(message.toString());

      switch (data.channelType) {
        case "core":
          CoreController.handle(data, ws);
          break;
      }
    } catch (error) {
      console.log("ERROR:" + error);
    }
  });
});

console.log("WebSocket server is running on ws://51.83.180.196:81");
