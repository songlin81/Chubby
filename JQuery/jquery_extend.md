jquery的$.extend和$.fn.extend作用及区别


1.  jQuery为开发插件提拱了两个方法，分别是：
    jQuery.fn.extend();
    jQuery.extend();
    虽然 javascript　没有明确的类的概念，但是用类来理解它，会更方便。


2.  jQuery便是一个封装得非常好的类，比如我们用 语句　$("#btn1") 会生成一个 jQuery类的实例。
    jQuery.extend(object);　为jQuery类添加类方法，可以理解为添加静态方法。如：
    
    jQuery.extend({
        min: function(a, b) { return a < b ? a : b; },
        max: function(a, b) { return a > b ? a : b; }
    });
    jQuery.min(2,3); //  2
    jQuery.max(4,5); //  5
    
    ObjectjQuery.extend( target, object1, [objectN])用一个或多个其他对象来扩展一个对象，返回被扩展的对象
    var settings = { validate: false, limit: 5, name: "foo" };
    var options = { validate: true, name: "bar" };
    jQuery.extend(settings, options);
    结果：settings == { validate: true, limit: 5, name: "bar" }
    
    
3.  jQuery.fn.extend(object); 对jQuery.prototype进得扩展，就是为jQuery类添加“成员函数”。jQuery类的实例可以使用这个“成员函数”。
    比如我们要开发一个插件，做一个特殊的编辑框，当它被点击时，便alert 当前编辑框里的内容。可以这么做：
    $.fn.extend({          
         alertWhileClick:function() {            
               $(this).click(function(){                 
                      alert($(this).val());           
                });           
          }       
    });       
    $("#input1").alertWhileClick(); // 页面上为：    
    $("#input1")　为一个jQuery实例，当它调用成员方法 alertWhileClick后，便实现了扩展，每次被点击时它会先弹出目前编辑里的内容。