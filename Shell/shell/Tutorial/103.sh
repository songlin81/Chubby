#!/bin/bash
COUNTER=0
while [ $COUNTER -lt 10 ]; do
echo The counter is $COUNTER
let COUNTER=COUNTER+1
done


#The counter is 0
#The counter is 1
#The counter is 2
#The counter is 3
#The counter is 4
#The counter is 5
#The counter is 6
#The counter is 7
#The counter is 8
#The counter is 9
