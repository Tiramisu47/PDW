import { WebSocket } from "ws";
import ShortUniqueId from "short-unique-id";
import { User } from "./entity/user.entity";
import { Session } from "./entity/session.entity";
const { randomUUID } = new ShortUniqueId({ length: 6 });

export const CoreService = {
  generatedSessionsTokens: [] as string[],
  sessions: {} as { [key: string]: Session },

  GenerateSessionToken(): string {
    while (true) {
      const token = randomUUID();
      console.log(token);

      if (!this.generatedSessionsTokens.includes(token)) {
        this.generatedSessionsTokens.push(token);
        return token;
      } else {
        console.log("DUPLIKACJA, NOWY TOKEN");
      }
    }
  },

  CreateSession(client: WebSocket): string {
    const generatedSessionToken = this.GenerateSessionToken();
    const user = new User(client);
    const newSession = new Session(generatedSessionToken, user);
    this.sessions[generatedSessionToken] = newSession;
    return generatedSessionToken;
  },

  ValidateSession(sessionToken: string): Session {
    const session = this.sessions[sessionToken];
    if (!session) throw new Error();
    return session;
  },
};
