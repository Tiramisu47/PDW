import { CoreService } from "./core.service";
import { WebSocket } from "ws";
import { User } from "./entity/user.entity";

export const CoreController = {
  handle(data: any, client: any): void {
    switch (data.eventType) {
      //FROM GAME
      case "try_createSession":
        this.createSession(client);
        break;

      //FROM SITE
      case "W_tryLoginSession_S":
        this.loginSession(client, data.sessionToken);
        break;
      case "W_tryToggleElementState_S":
        this.toggleElementState(data.sessionToken, data.elementUid);
        break;
      case "W_tryRangeElementState_S":
        this.rangeElementState(data.sessionToken, data.elementUid, data.value);
        break;
      default:
        break;
    }
  },

  ///HANDLE FUNCTIONS
  async createSession(client: WebSocket): Promise<boolean> {
    const newSessionToken = CoreService.CreateSession(client);
    const source_joinSessionS_message = JSON.stringify({
      channelType: "core",
      eventType: "source_joinSessionS",
      sessionToken: newSessionToken,
    });
    client.send(source_joinSessionS_message);
    return true;
  },

  async loginSession(
    client: WebSocket,
    sessionToken: string
  ): Promise<boolean> {
    const session = CoreService.ValidateSession(sessionToken);
    if (!session) {
      const source_loginSessionFailedS_message = JSON.stringify({
        channelType: "core",
        eventType: "source_loginSessionFailedS",
      });
      client.send(source_loginSessionFailedS_message);
      return false;
    }

    const remotePlayer = new User(client);
    session.remotePlayer = remotePlayer;
    const source_loginSessionS_message = JSON.stringify({
      channelType: "core",
      eventType: "source_loginSessionSuccesfulS",
      sessionToken: sessionToken,
    });
    client.send(source_loginSessionS_message);
    return true;
  },

  async toggleElementState(
    sessionToken: string,
    elementUid: number
  ): Promise<boolean> {
    const session = CoreService.ValidateSession(sessionToken);
    if (!session) return false;
    const other_toggleElementStateS_message = JSON.stringify({
      channelType: "core",
      eventType: "other_toggleElementStateS",
      elementUid: elementUid,
    });
    session.host.client.send(other_toggleElementStateS_message);
    return true;
  },

  async rangeElementState(
    sessionToken: string,
    elementUid: number,
    value: number
  ): Promise<boolean> {
    const session = CoreService.ValidateSession(sessionToken);
    if (!session) return false;
    const other_rangeElementStateS_message = JSON.stringify({
      channelType: "core",
      eventType: "other_rangeElementStateS",
      elementUid: elementUid,
      value: value,
    });
    session.host.client.send(other_rangeElementStateS_message);
    return true;
  },
};
