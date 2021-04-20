#!/bin/bash

while true; do
    response=$(curl -s "http://51.83.133.89:21374/get")
    if [ "$response" = "true" ]; then
        gpio -g mode 4 out
        gpio -g write 4 1
        sleep 0.6
        gpio -g write 4 0
    fi
    sleep 1
done