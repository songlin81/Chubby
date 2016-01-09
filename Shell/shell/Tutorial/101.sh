#!/bin/bash
T1="foo"
T2="foo"
if [ $T1 = $T2 ]; then
echo expression evaluated as true #<==
else
echo expression evaluated as false
fi

T3="foo"
T4="foo2"
if [ $T3 = $T4 ]; then
echo expression evaluated as true
else
echo expression evaluated as false 	#<==
fi