*** Javascript中currying的实现 ***

Currying好像是函数式语言都有的一个特性，比如Perl,Python,Javascript。
那么到底什么是Currying，我是在学习Closure时无意中接触到这个定义的，觉得很是有趣。
先看看 Wiki 中的定义：
Currying is the technique of transforming a function that takes multiple arguments 
in such a way that it can be called as a chain of functions each with a single argument.
大概的意思就是说，将拥有多个参数的函数Currying化为拥有单一参数的函数形式。

下面举一个简单的例子说明Javascript中的Currying实现，一个简单的求和函数：
function add(x, y) {
    return x + y;
}
console.log('add(2, 3) == ' + add(2, 3));

对其进行Currying，及调用方法：
function curry_add(x) {
    return function(y) {
        return x + y;
    }
}
console.log('curry_add(2)(3) == ' + curry_add(2)(3));
注意，curry_add(2) 返回的是函数。

-------------------------------------------------------

附：John Resig在Pro Javascript一书中关于Currying的实现代码：

// A function that generators a new function for adding numbers
function addGenerator( num ) {
    // Return a simple function for adding two numbers
    // with the first number borrowed from the generator
    return function( toAdd ) {
        return num + toAdd
    };
}

// addFive now contains a function that takes one argument,
// adds five to it, and returns the resulting number
var addFive = addGenerator( 5 );

// We can see here that the result of the addFive function is 9,
// when passed an argument of 4
alert( addFive( 4 ) == 9 );
