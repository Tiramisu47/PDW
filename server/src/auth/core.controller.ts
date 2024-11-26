import { CoreService } from "./core.service";
import { WebSocket } from "ws";
import { User } from "./entity/user.entity";

export const CoreController = {
  handle(data: any, client: any): void {
    switch (data.eventType) {
      //GAME -> SERVER
      case "GS_tryCreateSession":
        this.GS_tryCreateSession(client);
        break;

      //WEB -> SERVER
      case "WS_tryLoginSession":
        this.WS_tryLoginSession(client, data.sessionToken);
        break;
      case "WS_tryToggleElementState":
        this.WS_tryToggleElementState(data.sessionToken, data.elementUid);
        break;
      case "WS_tryRangeElementState":
        this.WS_tryRangeElementState(
          data.sessionToken,
          data.elementUid,
          data.value
        );
        break;
      default:
        break;
    }
  },

  ///HANDLE FUNCTIONS
  async GS_tryCreateSession(client: WebSocket): Promise<boolean> {
    const newSessionToken = CoreService.CreateSession(client);
    const source_joinSessionS_message = JSON.stringify({
      channelType: "core",
      eventType: "source_joinSessionS",
      sessionToken: newSessionToken,
    });
    client.send(source_joinSessionS_message);
    return true;
  },

  async WS_tryLoginSession(
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

  async WS_tryToggleElementState(
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

  async WS_tryRangeElementState(
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
