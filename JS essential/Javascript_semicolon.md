*** 浅谈javascript的分号的使用 ***

Js多个文件集成成一个文件后，压缩代码时避免发生语法错误，可以如下处理

一、js 前加分号
例如：;(function($){...此处代码...})();
Javascript中分号表示语句结束，在开头加上，是为了压缩的时候和别的方法分割一下，表示一个新的语句开始

二、js函数后加分号
例如
// 模块1
// 前面有若干代码
var Manager = {
    prop: '',
    method: function () {
    }
}
// 模块2，开头是个立即执行函数
(function () {
    // 代码
})()

经过压缩后变成：  }}(function 那里，会被当成一个函数来执行，于是整体的解析就会出错了
var Manager = {prop: '',method: function (){}}(function () {})()

解决方法： 是在Manager函数后加分号.
