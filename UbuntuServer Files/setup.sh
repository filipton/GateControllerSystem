mv /root/wstunnel /bin
chmod +x /bin/wstunnel
apt install apache2 php -y
rm /var/www/html/index.html
mv /root/index.php /var/www/html

echo "@reboot /root/startup.sh" | crontab -