const commandInput = document.getElementById('commandInput');
const output = document.getElementById('output');

let _sessionToken = null;
let socket = null;

//CONSOLE

function printLine(text, color = 'default') {
  const line = document.createElement('div');
  line.className = `color-${color}`;
  line.textContent = text;
  output.appendChild(line);
}

function printInputLine(text) {
  const line = document.createElement('div');
  line.textContent = _sessionToken
    ? '@' + _sessionToken + ' > ' + text
    : '> ' + text;
  output.appendChild(line);
}

commandInput.addEventListener('keydown', (event) => {
  if (event.key === 'Enter') {
    const command = commandInput.value.trim();
    if (command) {
      printInputLine(command);
      processCommand(command);
      commandInput.value = '';
      output.scrollTop = output.scrollHeight;
    }
  }
});

function processCommand(command) {
  const args = command.split(' ');
  const mainCommand = args[0];

  switch (mainCommand) {
    case '!login':
      handleLogin(args);
      break;
    case '!toggle':
      handleToggle(args);
      break;
    case '!range':
      handleRange(args);
      break;
    case '!help':
      handleHelp();
      break;
    case '!clear':
      handleClear();
      break;
    default:
      printLine(`Unknown command: ${command}`, 'red');
      break;
  }
}

function handleLogin(args) {
  if (args.length !== 2) {
    printLine(`Usage: !login [username]`, 'red');
    return;
  }

  const sessionToken = args[1];
  WS_tryLoginSession(sessionToken);
}

function handleToggle(args) {
  //PROTECTED
  if (!_sessionToken) {
    printLine('User not logged in', 'red');
    return;
  }

  if (args.length !== 2) {
    printLine('Usage: !toggle [UID]', 'red');
    return;
  }

  const uid = args[1];
  WS_tryToggleElementState(uid);
}

function handleRange(args) {
  //PROTECTED
  if (!_sessionToken) {
    printLine('User not logged in', 'red');
    return;
  }

  if (args.length !== 3) {
    printLine('Usage: !range [UID] [value]', 'red');
    return;
  }

  const uid = args[1];
  const value = args[2];
  WS_tryRangeElementState(uid, value);
}

function handleClear() {
  output.innerHTML = '';
}

function handleHelp() {
  printLine('!login [username]');
  printLine('!toggle [UID]');
  printLine('!range [UID] [value]');
  printLine('!clear');
  printLine('!help');
}

//CONNECTION

function tryToConnect() {
  printLine(`Connecting to the server...`);
  socket = new WebSocket('ws://51.83.180.196:81');
  socket.addEventListener('open', () => {
    printLine(`Connected to server successfully`, 'green');
    commandInput.disabled = false;
  });

  socket.addEventListener('message', (event) => {
    handleIncomingMessage(event.data);
  });

  socket.addEventListener('error', () => {
    printLine(`Error connecting to server`, 'red');
  });

  socket.addEventListener('close', () => {
    _sessionToken = null;
    printLine(`Disconnected from server`, 'red');
  });
}

function handleIncomingMessage(message) {
  try {
    const parsedMessage = JSON.parse(message);
    switch (parsedMessage.eventType) {
      case 'source_loginSessionSuccesfulS':
        _sessionToken = parsedMessage.sessionToken;
        printLine(`Successfully logged in as @${_sessionToken}`, 'green');
        break;
      case 'source_loginSessionFailedS':
        printLine('Error when connecting to session', 'red');
        break;
    }
  } catch (e) {
    // Handle error
  }
}

function WS_tryLoginSession(sessionToken) {
  const message = {
    channelType: 'core',
    eventType: 'WS_tryLoginSession',
    sessionToken: sessionToken,
  };
  sendJSON(message);
}

function WS_tryToggleElementState(uid) {
  const message = {
    channelType: 'core',
    eventType: 'WS_tryToggleElementState',
    sessionToken: _sessionToken,
    elementUid: uid,
  };
  sendJSON(message);
}

function WS_tryRangeElementState(uid, value) {
  const message = {
    channelType: 'core',
    eventType: 'WS_tryRangeElementState',
    sessionToken: _sessionToken,
    elementUid: uid,
    value: value,
  };
  sendJSON(message);
}

//HELPERS

function sendJSON(jsonObject) {
  if (socket.readyState === WebSocket.OPEN) {
    const jsonString = JSON.stringify(jsonObject);
    socket.send(jsonString);
    console.log('JSON sent:', jsonString);
  } else {
    console.log('WebSocket connection is not open');
  }
}

//ON START
tryToConnect();
commandInput.disabled = true;
