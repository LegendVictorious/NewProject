var io =  require('socket.io')(process.envPort||3000);

console.log("Server started");

io.on('connection', function(socket){
    console.log('client connected');
});