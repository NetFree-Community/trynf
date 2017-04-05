var VERSION, ip, readString;

ip = require('ip');

VERSION = require('./const').VERSION;

readString = function(data, offset) {
  var i;
  i = offset;
  while (data[i] && data[i] !== 0x00) {
    i++;
  }
  return data.slice(offset, i).toString();
};

exports.request = function(data) {
  var command, host, hostBuffer, port, portBuffer, userID, version, _ref, _ref1;
  if (data.length < 9) {
    throw new Error('Invalid request data');
  }
  version = data[0];
  if (version !== VERSION) {
    throw new Error("Wrong SOCKS version: " + version + ", expected " + VERSION);
  }
  command = data[1];
  portBuffer = data.slice(2, 4);
  port = portBuffer.readUInt16BE(0);
  hostBuffer = data.slice(4, 8);
  host = ip.toString(hostBuffer);
  if (((hostBuffer[0] === (_ref1 = hostBuffer[1]) && _ref1 === (_ref = hostBuffer[2])) && _ref === 0)) {
    userID = readString(data, 8);
    host = readString(data, 9 + userID.length);
  } else {
    userID = readString(data, 8);
  }
  return {
    version: version,
    command: command,
    portBuffer: portBuffer,
    port: port,
    hostBuffer: hostBuffer,
    host: host,
    userID: userID
  };
};
