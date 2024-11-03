import WebSocket from "ws";
import { CoreService } from "../core.service";

export class User {
  client: WebSocket;

  constructor(client: WebSocket) {
    this.client = client;
    this.client.onclose = () => {
      //LOGOUT
    };
  }
}
