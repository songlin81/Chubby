    *** Expression ***
    
表达式树的语法如下：
    Expression<Func<type,returnType>> = (param) => lamdaexpresion;

我们先来看一个简单例子：
    Expression<Func<int, int, int>> expr = (x, y) => x+y;

对于前面举的例子，主体部分即x+y，参数部分即(x,y)。Lambda表达式类型是Func<Int32, Int32, Int32>。注意主体部分可以是表达式，但是不能包含语句，如下这样：

Expression<Func<int, int, int>> expr = (x, y) => { return x+y; };

     会报编译错误“Lambada expression with state body cannot be converted to expression tree”：即带有语句的Lambda表达式不能转换成表达式树。
