import { User } from "./user.entity";

export class Session {
  sessionToken: string;
  host: User;
  remotePlayer: User | null;

  constructor(sessionToken: string, host: User) {
    this.sessionToken = sessionToken;
    this.host = host;
    this.remotePlayer = null;
  }
}
