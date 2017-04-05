var COMMAND, REQUEST_STATUS, RSV, VERSION, events, name, parsers, through, value, _ref, _ref1,
  __indexOf = [].indexOf || function(item) { for (var i = 0, l = this.length; i < l; i++) { if (i in this && this[i] === item) return i; } return -1; },
  __slice = [].slice;

through = require('through');

parsers = require('./parsers');

_ref = require('./const'), VERSION = _ref.VERSION, COMMAND = _ref.COMMAND, REQUEST_STATUS = _ref.REQUEST_STATUS, RSV = _ref.RSV;

events = {
  request: function(infos, callback) {
    return callback(REQUEST_STATUS.FAILED);
  }
};

exports.createHandler = function() {
  var handler, name, request, step, value;
  step = 'request';
  handler = through(function(chunk) {
    switch (step) {
      case 'request':
        return request.call(this, chunk);
    }
  });
  handler.version = VERSION;
  for (name in events) {
    value = events[name];
    handler.on(name, value);
  }
  handler.on('newListener', function(event, listener) {
    var _ref1;
    if (event in events && (_ref1 = events[event], __indexOf.call(handler.listeners(event), _ref1) >= 0)) {
      return handler.removeListener(event, events[event]);
    }
  });
  request = function(data) {
    var e,
      _this = this;
    try {
      request = parsers.request(data);
    } catch (_error) {
      e = _error;
      this.emit('error', e);
      return;
    }
    return this.emit('request', request, function(status) {
      _this.push(new Buffer([RSV, status].concat(__slice.call(request.portBuffer), __slice.call(request.hostBuffer))));
      if (status !== REQUEST_STATUS.GRANTED) {
        return _this.push(null);
      } else {
        step = 'ignore';
        return _this.emit('success');
      }
    });
  };
  return handler;
};

_ref1 = require('./const');
for (name in _ref1) {
  value = _ref1[name];
  exports[name] = value;
}
