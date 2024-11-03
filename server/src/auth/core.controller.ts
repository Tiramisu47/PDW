import { CoreService } from "./core.service";
import { WebSocket } from "ws";

export const CoreController = {
  handle(data: any, client: any): void {
    switch (data.eventType) {
      case "try_createSession":
        this.createSession(client);
        break;
      case "try_toggleElementState":
        this.toggleElementState(data.sessionToken, data.elementUid);
        break;
      case "try_rangeElementState":
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

  async toggleElementState(
    sessionToken: string,
    elementUid: number
  ): Promise<boolean> {
    const session = CoreService.ValidateSession(sessionToken);
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
