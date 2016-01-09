#!/bin/bash

function quit {
    exit
}
function e {
    echo $1
}

e Hello
e World
quit
echo foo

#bogon:BashProj wwh$ /Users/wwh/Downloads/BashProj/106.sh 
#Hello
#World
#bogon:BashProj wwh$ 
