需求是：

    我要消除name字段值重复的记录，同时又要得到id字段的值。

SELECT id,name FROM t1
WHERE id IN(
    SELECT MAX(id) FROM t1 GROUP BY name
) order by id desc
