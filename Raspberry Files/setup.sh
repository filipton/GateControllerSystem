apt update -y
mv /root/wstunnel /bin
chmod +x /bin/wstunnel
apt install git apache2 php wiringpi mono-complete -y
rm /var/www/html/index.html
mv /root/index.php /var/www/html
mv /root/test.php /var/www/html
mv /root/secret_key /var
chmod 777 /var/secret_key

curl -o node-v9.7.1-linux-armv6l.tar.gz https://nodejs.org/dist/v9.7.1/node-v9.7.1-linux-armv6l.tar.gz
tar -xzf node-v9.7.1-linux-armv6l.tar.gz
sudo cp -r node-v9.7.1-linux-armv6l/* /usr/local/
rm node-v9.7.1-linux-armv6l.tar.gz

echo "@reboot /root/startup.sh" | crontab -