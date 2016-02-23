*** Javascript中currying的实现 ***

Currying is the technique of transforming a function that takes multiple arguments 
in such a way that it can be called as a chain of functions each with a single argument.

function add(x, y) {
    return x + y;
}
console.log('add(2, 3) == ' + add(2, 3));
function curry_add(x) {
    return function(y) {
        return x + y;
    }
}
console.log('curry_add(2)(3) == ' + curry_add(2)(3));

-------------------------------------------------------

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
