screen -dm bash -c "wstunnel cli -token 'NiktNieMozeTegoUkrasc' -tunnel ws://172.104.158.181:21371 -server http://localhost:21371 & wstunnel cli -token 'NiktNieMozeTegoUkrasc' -tunnel ws://172.104.158.181:21372 -server http://localhost:21372 & wstunnel cli -token 'NiktNieMozeTegoUkrasc' -tunnel ws://172.104.158.181:21373 -server http://localhost:21373"
screen -dm bash -c "mono /root/MainGateServer1/MainGateHttpServer.exe"
screen -dm bash -c "node /root/MainGateServer2.js"
screen -dm bash -c "bash /root/check.sh"