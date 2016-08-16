$(function () {
    var validate = $("#myform").validate({
        debug: true,
        submitHandler: function (form) {
            alert("提交表单");
            form.submit();
        },
        rules: {
            zero: {
                //isIntEqZero: true,
                //isFloat:true
                //isInteger:true
                //isNumber:true
                //isMobile:true
                //isPhone:true
                //isTel:true
                //isIdCardNo:true
                //isRightfulString:true
                isContainsSpecialChar: true
            }
        },
        //如果验证控件没有message，将调用默认的信息
        messages: {
            zero: {
                //isIntEqZero:"请输入0"
                //isFloat:"请输入浮点数"
                //isInteger:"请输入整数"
            }
        },
        errorPlacement: function (error, element) {
            error.html(error.html() + "<br/>");
            error.appendTo("#errorContainer");
            $('<br/>').appendTo("#errorContainer");
        }
    });
});
