var Gpio = require('onoff').Gpio;
var virtualButton = new Gpio(4, 'out');
var sec_key = "secret_key";

fs = require('fs');
fs.readFile('/var/secret_key', 'utf8', function (err,data) {
  if (err) {
    return console.log(err);
  }
  sec_key = data.replace("\n", "");
});

var http = require('http'); http.createServer(function (req, res) { 
  var url = req.url.replace("/", "");
  var args = url.split("/");
  if(args.length > 0)
  {
    if(args.length == 2 && args[0] == "gate")
    {
      if(args[1] == sec_key)
      {
        res.writeHead(200, {'Content-Type': 'text/html'}); res.write("Otwieranie bramy... (System 2)");
        virtualButton.writeSync(1);
        setTimeout(endVirtualButton, 600);
      }
      else
      {
        res.writeHead(200, {'Content-Type': 'text/html'}); res.write("Zły klucz dostępu... (System 2)");
      }
    }
    else if(args.length == 1 && args[0] == "info")
    {
      res.writeHead(200, {'Content-Type': 'text/html'}); res.write("Ok!");
    }
  }
  res.end();
}).listen(21372);

function endVirtualButton() {
  virtualButton.writeSync(0);
}
