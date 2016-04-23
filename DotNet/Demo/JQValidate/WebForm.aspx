
<%--<%
    String path = request.getContextPath();
    String basePath = request.getScheme() + "://" + request.getServerName() + ":"
            + request.getServerPort() + path + "/";
%>--%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
    <head>
        <title>jQuery Validate扩展验证方法</title>
        <meta http-equiv="pragma" content="no-cache">
        <meta http-equiv="cache-control" content="no-cache">
        <meta http-equiv="expires" content="0">
        <meta http-equiv="keywords" content="keyword1,keyword2,keyword3">
        <meta http-equiv="description" content="This is my page">
        
        <script src="Scripts/jquery-1.4.1.js"></script>
        <script src="Scripts/jquery.validate.js"></script>
        <script src="Scripts/validate-methods.js"></script>
        <script src="Scripts/FormValidator.js"></script>
    </head>
    <body>
        <form id="myform" method="post" action="">
            <p>
                <label for="zero">输入框：</label>
                <input id="zero" name="zero" />
            </p>    
            <p>
                <input class="submit" type="submit" value="验证" />
            </p>
            <div id="errorContainer"></div>
        </form>
    </body>
</html>