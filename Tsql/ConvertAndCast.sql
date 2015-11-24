SELECT 'AB' + 1    --此语句报错，在将 varchar 值 'AB' 转换成数据类型 int 时失败

--为了ANSI/ISO兼容。CAST是ANSI兼容的，而CONVERT则不是。

SELECT 'AB' + CAST(1 AS varchar)    --输出 AB1
SELECT 'AB' + CONVERT(varchar,1)    --输出 AB1

SELECT CONVERT(DateTime,'2011-07-11')    --输出 2011-07-11 00:00:00.000
SELECT CAST('2011-07-11' AS DateTime)    --输出 2011-07-11 00:00:00.000

SELECT CONVERT(varchar,GETDATE(),5)    --输出 01-07-13
SELECT CONVERT(varchar,GETDATE(),111)  --输出 2013/07/01
SELECT CONVERT(varchar,GETDATE(),1)    --输出 07/01/13
SELECT CAST(GETDATE() AS varchar)    --输出 07 1 2013 9:56PM
