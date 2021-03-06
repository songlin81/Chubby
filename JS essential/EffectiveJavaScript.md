    *** JavaScript性能优化小知识总结 ***

1. 避免全局查找
在一个函数中会用到全局对象存储为局部变量来减少全局查找，因为访问局部变量的速度要比访问全局变量的速度更快些
	function search() {
            //当我要使用当前页面地址和主机域名
            alert(window.location.href + window.location.host);
        }
        //最好的方式是如下这样  先用一个简单变量保存起来
        function search() {
            var location = window.location;
            alert(location.href + location.host);
        }

2. 定时器
如果针对的是不断运行的代码，不应该使用setTimeout，而应该是用setInterval，
因为setTimeout每一次都会初始化一个定时器，而setInterval只会在开始的时候初始化一个定时器
        var timeoutTimes = 0;
        function timeout() {
            timeoutTimes++;
            if (timeoutTimes < 10) {
                setTimeout(timeout, 10);
            }
        }
        timeout();
        //可以替换为：
        var intervalTimes = 0;
        function interval() {
            intervalTimes++;
            if (intervalTimes >= 10) {
                clearInterval(interv);
            }
        }
        var interv = setInterval(interval, 10);
 
3. 字符串连接
如果要连接多个字符串，应该少使用+=，如
s+=a;
s+=b;
s+=c;
应该写成s+=a + b + c；
而如果是收集字符串，比如多次对同一个字符串进行+=操作的话，最好使用一个缓存，使用JavaScript数组来收集，
最后使用join方法连接起来
        var buf = [];
        for (var i = 0; i < 100; i++) {
            buf.push(i.toString());
        }
        var all = buf.join("");
 
4. 避免with语句
和函数类似 ，with语句会创建自己的作用域，因此会增加其中执行的代码的作用域链的长度，
由于额外的作用域链的查找，在with语句中执行的代码肯定会比外面执行的代码要慢，
在能不使用with语句的时候尽量不要使用with语句。
 with (a.b.c.d) {
            property1 = 1;
            property2 = 2;
        }
        //可以替换为：
        var obj = a.b.c.d;
        obj.property1 = 1;
        obj.property2 = 2;

7. 各种类型转换
        var myVar = "3.14159",
        str = "" + myVar, //  to string  
        i_int = ~ ~myVar,  //  to integer  
        f_float = 1 * myVar,  //  to float  
        b_bool = !!myVar,  /*  to boolean - any string with length and any number except 0 are true*/
        array = [myVar];  //  to array
如果定义了toString()方法来进行类型转换的话，推荐显式调用toString()，因为内部的操作在尝试所有可能性之后，
会尝试对象的toString()方法尝试能否转化为String，所以直接调用这个方法效率会更高

9. 插入迭代器
如var name=values[i]; i++;前面两条语句可以写成var name=values[i++]

11. 使用DocumentFragment优化多次append
一旦需要更新DOM,请考虑使用文档碎片来构建DOM结构，然后再将其添加到现存的文档中。
        for (var i = 0; i < 1000; i++) {
            var el = document.createElement('p');
            el.innerHTML = i;
            document.body.appendChild(el);
        }
        //可以替换为：
        var frag = document.createDocumentFragment();
        for (var i = 0; i < 1000; i++) {
            var el = document.createElement('p');
            el.innerHTML = i;
            frag.appendChild(el);
        }
        document.body.appendChild(frag);

12. 使用一次innerHTML赋值代替构建dom元素
对于大的DOM更改，使用innerHTML要比使用标准的DOM方法创建同样的DOM结构快得多。
        var frag = document.createDocumentFragment();
        for (var i = 0; i < 1000; i++) {
            var el = document.createElement('p');
            el.innerHTML = i;
            frag.appendChild(el);
        }
        document.body.appendChild(frag);
        //可以替换为：
        var html = [];
        for (var i = 0; i < 1000; i++) {
            html.push('<p>' + i + '</p>');
        }
        document.body.innerHTML = html.join('');

13. 通过模板元素clone，替代createElement
很多人喜欢在JavaScript中使用document.write来给页面生成内容。事实上这样的效率较低，
如果需要直接插入HTML，可以找一个容器元素，
比如指定一个div或者span，并设置他们的innerHTML来将自己的HTML代码插入到页面中。
通常我们可能会使用字符串直接写HTML来创建节点，
其实这样做，1无法保证代码的有效性2字符串操作效率低，所以应该是用document.createElement()方法，
而如果文档中存在现成的样板节点，应该是用cloneNode()方法，
因为使用createElement()方法之后，你需要设置多次元素的属性，
使用cloneNode()则可以减少属性的设置次数——同样如果需要创建很多元素，应该先准备一个样板节点
        var frag = document.createDocumentFragment();
        for (var i = 0; i < 1000; i++) {
            var el = document.createElement('p');
            el.innerHTML = i;
            frag.appendChild(el);
        }
        document.body.appendChild(frag);
        //替换为：
        var frag = document.createDocumentFragment();
        var pEl = document.getElementsByTagName('p')[0];
        for (var i = 0; i < 1000; i++) {
            var el = pEl.cloneNode(false);
            el.innerHTML = i;
            frag.appendChild(el);
        }
        document.body.appendChild(frag);

14. 使用firstChild和nextSibling代替childNodes遍历dom元素
        var nodes = element.childNodes;
        for (var i = 0, l = nodes.length; i < l; i++) {
            var node = nodes[i];
            //……
        }
        //可以替换为：
        var node = element.firstChild;
        while (node) {
            //……
            node = node.nextSibling;

15. 删除DOM节点
删除dom节点之前,一定要删除注册在该节点上的事件,不管是用observe方式还是用attachEvent方式注册的事件,
否则将会产生无法回收的内存。
另外，在removeChild和innerHTML=’’二者之间,尽量选择后者.
因为在sIEve(内存泄露监测工具)中监测的结果是用removeChild无法有效地释放dom节点

16. 使用事件代理
任何可以冒泡的事件都不仅仅可以在事件目标上进行处理，目标的任何祖先节点上也能处理，
使用这个知识就可以将事件处理程序附加到更高的地方负责多个目标的事件处理，
同样，对于内容动态增加并且子节点都需要相同的事件处理函数的情况，可以把事件注册提到父节点上，
这样就不需要为每个子节点注册事件监听了。
另外，现有的js库都采用observe方式来创建事件监听,其实现上隔离了dom对象和事件处理函数之间的循环引用,
所以应该尽量采用这种方式来创建事件监听

18. 注意NodeList
最小化访问NodeList的次数可以极大的改进脚本的性能
        var images = document.getElementsByTagName('img');
        for (var i = 0, len = images.length; i < len; i++) {

        }
编写JavaScript的时候一定要知道何时返回NodeList对象，这样可以最小化对它们的访问
    进行了对getElementsByTagName()的调用
    获取了元素的childNodes属性
    获取了元素的attributes属性
    访问了特殊的集合，如document.forms、document.images等等
要了解了当使用NodeList对象时，合理使用会极大的提升代码执行速度

19. 优化循环
可以使用下面几种方式来优化循环
    减值迭代
大多数循环使用一个从0开始、增加到某个特定值的迭代器，在很多情况下，从最大值开始，
在循环中不断减值的迭代器更加高效

    简化终止条件
由于每次循环过程都会计算终止条件，所以必须保证它尽可能快，也就是说避免属性查找或者其它的操作，
最好是将循环控制量保存到局部变量中，
也就是说对数组或列表对象的遍历时，提前将length保存到局部变量中，避免在循环的每一步重复取值。
        var list = document.getElementsByTagName('p');
        for (var i = 0; i < list.length; i++) {
            //……
        }
        //替换为：
        var list = document.getElementsByTagName('p');
        for (var i = 0, l = list.length; i < l; i++) {
            //……
        }

    简化循环体
循环体是执行最多的，所以要确保其被最大限度的优化

    使用后测试循环
在JavaScript中，我们可以使用for(;;),while(),for(in)三种循环，
事实上，这三种循环中for(in)的效率极差，因为他需要查询散列键，只要可以，就应该尽量少用。
for(;;)和while循环，while循环的效率要优于for(;;)，可能是因为for(;;)结构的问题，需要经常跳转回去。
        var arr = [1, 2, 3, 4, 5, 6, 7];
        var sum = 0;
        for (var i = 0, l = arr.length; i < l; i++) {
            sum += arr[i];
        }
        //可以考虑替换为：
        var arr = [1, 2, 3, 4, 5, 6, 7];
        var sum = 0, l = arr.length;
        while (l--) {
            sum += arr[l];
        }

最常用的for循环和while循环都是前测试循环，而如do-while这种后测试循环，可以避免最初终止条件的计算，
因此运行更快。

展开循环
当循环次数是确定的，消除循环并使用多次函数调用往往会更快。

避免双重解释
如果要提高代码性能，尽可能避免出现需要按照JavaScript解释的字符串，也就是

    尽量少使用eval函数
使用eval相当于在运行时再次调用解释引擎对内容进行运行，需要消耗大量时间，
而且使用Eval带来的安全性问题也是不容忽视的。

    不要使用Function构造器
不要给setTimeout或者setInterval传递字符串参数
        var num = 0;
        setTimeout('num++', 10);
        //可以替换为：
        var num = 0;
        function addNum() {
            num++;
        }
        setTimeout(addNum, 10);

20. 缩短否定检测
       if (oTest != '#ff0000') {
            //do something
        }
        if (oTest != null) {
            //do something
        }
        if (oTest != false) {
            //do something
        }
        //虽然这些都正确，但用逻辑非操作符来操作也有同样的效果：
        if (!oTest) {
            //do something
        }

21. 条件分支

    将条件分支，按可能性顺序从高到低排列：可以减少解释器对条件的探测次数
    在同一条件子的多（>2）条件分支时，使用switch优于if：switch分支选择的效率高于if，
    在IE下尤为明显。4分支的测试，IE下switch的执行时间约为if的一半。
    使用三目运算符替代条件分支
        if (a > b) {
            num = a;
        } else {
            num = b;
        }
        //可以替换为：
        num = a > b ? a : b;

23. 避免与null进行比较
由于JavaScript是弱类型的，所以它不会做任何的自动类型检查，所以如果看到与null进行比较的代码，
尝试使用以下技术替换
    如果值应为一个引用类型，使用instanceof操作符检查其构造函数
    如果值应为一个基本类型，作用typeof检查其类型
    如果是希望对象包含某个特定的方法名，则使用typeof操作符确保指定名字的方法存在于对象上

25. 尊重对象的所有权
因为JavaScript可以在任何时候修改任意对象，这样就可以以不可预计的方式覆写默认的行为，
所以如果你不负责维护某个对象，它的对象或者它的方法，那么你就不要对它进行修改，具体一点就是说：
    不要为实例或原型添加属性
    不要为实例或者原型添加方法
    不要重定义已经存在的方法
    不要重复定义其它团队成员已经实现的方法，永远不要修改不是由你所有的对象，
    你可以通过以下方式为对象创建新的功能:
    创建包含所需功能的新对象，并用它与相关对象进行交互
    创建自定义类型，继承需要进行修改的类型，然后可以为自定义类型添加额外功能

26. 循环引用
如果循环引用中包含DOM对象或者ActiveX对象，那么就会发生内存泄露。内存泄露的后果是在浏览器关闭前，
即使是刷新页面，这部分内存不会被浏览器释放。
简单的循环引用：
        var el = document.getElementById('MyElement');
        var func = function () {
            //…
        }
        el.func = func;
        func.element = el;
但是通常不会出现这种情况。通常循环引用发生在为dom元素添加闭包作为expendo的时候。
        function init() {
            var el = document.getElementById('MyElement');
            el.onclick = function () {
                //……
            }
        }
        init();
init在执行的时候，当前上下文我们叫做context。这个时候，context引用了el，el引用了function，
function引用了context。这时候形成了一个循环引用。

下面2种方法可以解决循环引用：
1)  置空dom对象
       function init() {
            var el = document.getElementById('MyElement');
            el.onclick = function () {
                //……
            }
        }
        init();
        //可以替换为：
        function init() {
            var el = document.getElementById('MyElement');
            el.onclick = function () {
                //……
            }
            el = null;
        }
        init();
将el置空，context中不包含对dom对象的引用，从而打断循环应用。

如果我们需要将dom对象返回，可以用如下方法：
        function init() {
            var el = document.getElementById('MyElement');
            el.onclick = function () {
                //……
            }
            return el;
        }
        init();
        //可以替换为：
        function init() {
            var el = document.getElementById('MyElement');
            el.onclick = function () {
                //……
            }
            try {
                return el;
            } finally {
                el = null;
            }
        }
        init();

2)  构造新的context
        function init() {
            var el = document.getElementById('MyElement');
            el.onclick = function () {
                //……
            }
        }
        init();
        //可以替换为：
        function elClickHandler() {
            //……
        }
        function init() {
            var el = document.getElementById('MyElement');
            el.onclick = elClickHandler;
        }
        init();

把function抽到新的context中，这样，function的context就不包含对el的引用，从而打断循环引用。

27. 通过javascript创建的dom对象，必须append到页面中
IE下，脚本创建的dom对象，如果没有append到页面中，刷新页面，这部分内存是不会回收的！
        function create() {
            var gc = document.getElementById('GC');
            for (var i = 0; i < 5000; i++) {
                var el = document.createElement('div');
                el.innerHTML = "test";
                //下面这句可以注释掉，看看浏览器在任务管理器中，点击按钮然后刷新后的内存变化
                gc.appendChild(el);
            }
        }

37. 总是检查数据类型
要检查你的方法输入的所有数据，一方面是为了安全性，另一方面也是为了可用性。用户随时随地都会输入错误的数据。
这不是因为他们蠢，而是因为他们很忙，并且思考的方式跟你不同。
用typeof方法来检测你的function接受的输入是否合法

38. 何时用单引号，何时用双引号
虽然在JavaScript当中，双引号和单引号都可以表示字符串, 为了避免混乱，我们建议在HTML中使用双引号，
在JavaScript中使用单引号，但为了兼容各个浏览器，也为了解析时不会出错，
定义JSON对象时，最好使用双引号
