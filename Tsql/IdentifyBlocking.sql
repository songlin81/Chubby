Using sp_who2 to help identify blocking queries

--Issue below in session 1
SELECT 1
WHILE @@ROWCOUNT > 0
BEGIN
select TOP (10) *
FROM dbo.Orders 
END


--Issue below in session 2
sp_who2
----get running query:
DBCC INPUTBUFFER(56)
----terminate that query:
kill 56
