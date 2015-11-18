wwhdeAir:server wwh$ pwd
/Users/wwh/Documents/server

wwhdeAir:server wwh$ tar -xf nginx-1.8.0.tar.gz

wwhdeAir:server wwh$ cd nginx-1.8.0

wwhdeAir:nginx-1.8.0 wwh$ chmod a+rwx *

wwhdeAir:nginx-1.8.0 wwh$ ls
CHANGES		README		configure	man
CHANGES.ru	auto		contrib		src
LICENSE		conf		html
wwhdeAir:nginx-1.8.0 wwh$ ll
-bash: ll: command not found
wwhdeAir:nginx-1.8.0 wwh$ ls -l
total 1256
-rwxrwxrwx@  1 wwh  staff  249124  4 21  2015 CHANGES
-rwxrwxrwx@  1 wwh  staff  379021  4 21  2015 CHANGES.ru
-rwxrwxrwx@  1 wwh  staff    1397  4 21  2015 LICENSE
-rwxrwxrwx@  1 wwh  staff      49  4 21  2015 README
drwxrwxrwx@ 24 wwh  staff     816  4 21  2015 auto
drwxrwxrwx@ 11 wwh  staff     374  4 21  2015 conf
-rwxrwxrwx@  1 wwh  staff    2478  4 21  2015 configure
drwxrwxrwx@  6 wwh  staff     204  4 21  2015 contrib
drwxrwxrwx@  4 wwh  staff     136  4 21  2015 html
drwxrwxrwx@  3 wwh  staff     102  4 21  2015 man
drwxrwxrwx@  8 wwh  staff     272  4 21  2015 src

wwhdeAir:nginx-1.8.0 wwh$ ./configure --without-http_rewrite_module
checking for OS
 + Darwin 14.5.0 x86_64
checking for C compiler ... found
 + using Clang C compiler
 + clang version: 6.1.0 (clang-602.0.53) (based on LLVM 3.6.0svn)
 
 wwhdeAir:nginx-1.8.0 wwh$ make && make install
/Applications/Xcode.app/Contents/Developer/usr/bin/make -f objs/Makefile
cc -c -pipe  -O -Wall -Wextra -Wpointer-arith -Wconditional-uninitialized -Wno-unused-parameter -Wno-deprecated-declarations -Werror -g  -I src/core -I src/event -I src/event/modules -I src/os/unix -I objs \
		-o objs/src/core/nginx.o \
		src/core/nginx.c

sudo /usr/local/nginx/sbin/nginx

wwhdeAir:sbin wwh$ sudo /usr/local/nginx/sbin/nginx -s stop
Password:
wwhdeAir:sbin wwh$
