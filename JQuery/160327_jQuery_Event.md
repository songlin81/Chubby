    *** jquery 注册事件的方法 ***

1.使用事件名来绑定，可用的事件名有
    change,click,dblclick,error,focus,focusin,focusout,keydown,keypress,keyup,
    mousedown,mouseenter,mouseleave,mousemove,mouseout,mouseover,mouseup,resize,scroll,select,submit,unload
    例如：
    $('#target').click(function(){
        alert('#target元素绑定了click事件');
    });


2.使用bind方法来绑定事件 bind(type,[data],fn) 。type 参数及为我们上面讲的各种事件名，例如：
当每个段落被点击的时候，弹出其文本。


3.使用on方法来注册事件 ，on方法基本和bind方法差不多，其实bind方法最好也是调用on方法来实现的
$('.scv').on('click', function(){
    $(this).clone(true).appendTo('#container');
});


4.即使是后来加进来也有效的方法live方法
在老的jQuery版本中，我们有一个方法专门用来处理动态生成的元素的事件绑定 - live()，使用live()方法可以将方法绑定的效果应用到已存在或者新创建的DOM元素。代码如下：
$('.scv').live('click', function(){
    $(this).clone().appendTo('#container');
});


5.一次性的事件绑定方法one方法，为每一个匹配元素的特定事件（像click）绑定一个一次性的事件处理函数。当所有段落被第一次点击的时候，显示所有其文本。
$("p").one("click", function(){
    alert( $(this).text() );
});