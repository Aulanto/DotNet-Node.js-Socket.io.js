 var http = require('http')
    , io = require('socket.io')
    , express = require('express')
    , users = []
    , socketio;

var app = express();

var server = http.createServer(app).listen(8888, function () {
    console.log('Listening at: http://localhost:8888');
});

socketio = io.listen(server);

socketio.on('connection', function (socket) {

	socket.emit();;
    socket.on('login', function (guest) {
		console.log("有用户登录:" + guest.userid);
        var user = {};
		user.socketid = socket.id;
        user.userid = guest.userid;
        users.push(user);
    });
	
    socket.on('sendtousers', function (data) {
	    console.log('Receive data:'+data);
		data = JSON.parse(data);
		var i = 0;
		for (; i < users.length; i++) {
		    if(data.UserId==users[i].userid){
		        console.log(users[i].socketid + ":userId:" + users[i].userid);
				//socketio.sockets.socket(users[i].socketid).emit('message', data.Message);
				socketio.sockets.connected[users[i].socketid].emit('message', data.Message);
			}		
		}
    });

    socket.on('disconnect', function () {
        // remove the user from global users list
        var loop = 0;
        for (; loop < users.length; loop++) {
            if (users[loop].socketid == socket.id) {
                console.log('disconnect:', users[loop]);
                users[loop].socketid = "";		
                users[loop].userid = "";
                //delete users[loop];
                break;
            }
        }
    });
});