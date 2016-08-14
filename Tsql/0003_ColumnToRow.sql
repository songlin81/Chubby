-- Use Northwind db instance for demo:
select 
	ord.OrderID, 
	(
		select cast(ProductID as nvarchar)+', ' from [Northwind].[dbo].[Order Details]
		where orderid=ord.OrderID
		For xml path('')
	) as productIDs 
from Orders ord
GROUP by ord.OrderID 
-- Sample output:
-- OrderID	productIDs
-- 10248	11, 42, 72, 
-- 10249	14, 51, 
-- 10250	41, 51, 65, 


-- Remove trailing ',':
select M.OrderID, LEFT(M.productIDs, LEN(M.productIDs)-1) as ProductIDsFinal
from (
	select 
		ord.OrderID, 
		(
			select cast(ProductID as nvarchar)+', ' from [Northwind].[dbo].[Order Details]
			where orderid=ord.OrderID
			For xml path('')
		) as productIDs 
	from Orders ord
	GROUP by ord.OrderID ) M
order by M.OrderID
-- Sample output:
-- OrderID	productIDs
-- 10248	11, 42, 72
-- 10249	14, 51 
-- 10250	41, 51, 65
