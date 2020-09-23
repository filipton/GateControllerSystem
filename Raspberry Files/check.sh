while true; do
sleep 30
echo "CHECKING..."
server1=$(curl --silent localhost:21371/info)
server2=$(curl --silent localhost:21372/info)

if [ "$server1" != "Ok!" ] ; then
    echo "System 1 nie dziala!"
    screen -dm bash -c "mono /root/MainGateServer1/MainGateHttpServer.exe"
fi
if [ "$server2" != "Ok!" ] ; then
    echo "System 2 nie dziala!"
    screen -dm bash -c "node /root/MainGateServer2.js"
fi
done
