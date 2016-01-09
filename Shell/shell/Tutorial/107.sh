#!/bin/bash

OPTIONS="Hello Quit"

select opt in $OPTIONS; do
    if [ "$opt" = "Quit" ]; then
        echo done
        exit
    elif [ "$opt" = "Hello" ]; then
        echo Hello World
    else
        clear
        echo bad option
    fi
done

#   bogon:BashProj wwh$ /Users/wwh/Downloads/BashProj/107.sh 
#   1) Hello
#   2) Quit
#   #? 3
#   bad option
#   #? 1
#   Hello World
#   #? 2
#   done
#   bogon:BashProj wwh$ 