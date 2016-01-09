#!/bin/bash
# A simple shell script to print list of cars
for car in bmw ford toyota nissan
   do
   echo "Value of car is: $car"
done


MAX=100000000000000000000
 
for (( i = 0; i < MAX ; i ++ ))
do
    curl http://localhost:8000/?params=$i
done
