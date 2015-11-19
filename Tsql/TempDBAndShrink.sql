SELECT name, size
FROM sys.master_files
WHERE database_id = DB_ID(N'tempdb');
-->
    name	    size
    tempdev	    1024 <-- the size column is listing the size of the file in 8Kb pages. 
    templog	    64    -- In this instance my “tempdev” file is 10Mb (( 1280 * 8 ) = 10240 kb)

DBCC SHRINKFILE(tempdev, 5);
-->
--CurrentSize is now 50% smaller than previously
    name	    size
    tempdev	    640
    templog	    64

---------------------------------------------------------------------------------------

*** Global and local [temporary tables] are created in here ***

    So for example if you write a create table statement starting like this global temporary table:
        CREATE TABLE ##Table....
    or this local temporary table…
        CREATE TABLE #Table....
    When you execute the create table script, the temporary table will be created in tempdb.


*** TempDB best practices ***

    Some guidance points
    
        *Place tempdb on a separate disk to user databases.
        
        *Place tempdb on the fastest IO subsystem possible.
        
        *Size tempdb accordingly and configure autogrow. This is especially important 
        if your system cannot tolerate performance degradation. If you size the database too small with autogrow enabled, 
        tempdb will automatically grow according to the size increments you have set up. 
        Imagine you have set it to grow in increments of 10Mb and you have a busy workload requiring tempdb to be used. 
        If tempdb needs to expand a number of times to accommodate load, these sizing operations can generate lots of IO. 
        Better to try and set your tempdb to be based on a staging server workload where you have witnessed 
        how large it can grow.
        
        *Create multiple data files for tempdb, but how many? Allocate 1 data file per physical or virtual CPU core.
        
        *Keep tempdb data files equally sized and have autogrow increments configured equally across data files.
        
        *If your SAN administrator can provide it, split the multiple data files over different LUNs 
        as opposed to holding everything on one LUN.
        
        *Configure the data files to be of equal size.
