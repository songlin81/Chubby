//1. Attached function on the fly
object1 = {
    name:'frank',
    greet:function(){
        alert('hello '+this.name)
    }
};
object2 = {
    name:'andy'
};
// Note that object2 has no greet method, but we may "borrow" from object1:
object1.greet.call(object2);

//2.The difference is that apply lets you invoke the function with arguments as an array;
//call requires the parameters be listed explicitly.
function theFunction(name, profession) {
    alert("My name is " + name + " and I am a " + profession + ".");
}
theFunction("John", "fireman");
theFunction.apply(undefined, ["Susan", "school teacher"]);
theFunction.call(undefined, "Claude", "mathematician");

//3.It retrieves the slice function from an Array.
//It then calls that function, but using the result of document.querySelectorAll
//as the this object instead of an actual array.
document.write([].slice.call(document.querySelectorAll('a')))
