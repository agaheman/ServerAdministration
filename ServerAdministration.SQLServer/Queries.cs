using System;
using System.Collections.Generic;
using System.Text;

namespace ServerAdministration.SQLServer
{
    public static class Queries
    {
        public static string DiskUsageByTopTables(int numberOfTopTables = 10)
        {
            return @"SELECT TOP " + numberOfTopTables.ToString() +
                  @"a3.name AS SchemaName,
                    a2.name AS TableName,
                    a1.rows as Row_Count,
                    (a1.reserved )* 8.0 / 1024 AS reserved_mb,
                    a1.data * 8.0 / 1024 AS data_mb,
                    (CASE WHEN (a1.used ) > a1.data THEN (a1.used ) - a1.data ELSE 0 END) * 8.0 / 1024 AS index_size_mb,
                    (CASE WHEN (a1.reserved ) > a1.used THEN (a1.reserved ) - a1.used ELSE 0 END) * 8.0 / 1024 AS unused_mb

                FROM    (   SELECT
                            ps.object_id,
                            SUM ( CASE WHEN (ps.index_id < 2) THEN row_count    ELSE 0 END ) AS [rows],
                            SUM (ps.reserved_page_count) AS reserved,
                            SUM (CASE   WHEN (ps.index_id < 2) THEN (ps.in_row_data_page_count + ps.lob_used_page_count + ps.row_overflow_used_page_count)
                                        ELSE (ps.lob_used_page_count + ps.row_overflow_used_page_count) END
                                ) AS data,
                            SUM (ps.used_page_count) AS used
                            FROM sys.dm_db_partition_stats ps
                            GROUP BY ps.object_id
                        ) AS a1

                INNER JOIN sys.all_objects a2  ON ( a1.object_id = a2.object_id )

                INNER JOIN sys.schemas a3 ON (a2.schema_id = a3.schema_id)

                WHERE a2.type <> N'S' and a2.type <> N'IT'   
                order by a1.data desc";
        }

        public static string DatabaseFilesInfo(string databaseName = "TransportInsuranceServer")
        {
            return @"SELECT DB_NAME(database_id) as DBName,name as DbName,
                    CAST(SUM(CASE WHEN type_desc = 'LOG' THEN size END) * 8 / 1024.0 / 1024.0 AS DECIMAL(8, 2))  AS LogFileSizeGB,
                     CAST(SUM(CASE WHEN type_desc = 'ROWS'THEN size END) * 8 / 1024.0 / 1024.0 AS DECIMAL(8, 2)) AS DataFileSizeGB,
                       physical_name AS FileLocation

                    FROM sys.master_files MasterFiles WITH(NOWAIT)
                    WHERE database_id = DB_ID()-- for current db
                    GROUP BY database_id, name, physical_name";
        }

        public static string GetDatabasesInfo() => @"EXEC sp_spaceused TransportOrder";

        public static string GetSqlServerInfo()
        {
            return @"
                    SELECT
                        CASE 
                            WHEN CONVERT(VARCHAR(128), SERVERPROPERTY ('productversion')) like '8%' THEN 'SQL Server 2000'
                            WHEN CONVERT(VARCHAR(128), SERVERPROPERTY ('productversion')) like '9%' THEN 'SQL Server 2005'
                            WHEN CONVERT(VARCHAR(128), SERVERPROPERTY ('productversion')) like '10.0%' THEN 'SQL Server 2008'
                            WHEN CONVERT(VARCHAR(128), SERVERPROPERTY ('productversion')) like '10.5%' THEN 'SQL Server 2008 R2'
                            WHEN CONVERT(VARCHAR(128), SERVERPROPERTY ('productversion')) like '11%' THEN 'SQL Server 2012'
                            WHEN CONVERT(VARCHAR(128), SERVERPROPERTY ('productversion')) like '12%' THEN 'SQL Server 2014'
                            WHEN CONVERT(VARCHAR(128), SERVERPROPERTY ('productversion')) like '13%' THEN 'SQL Server 2016'     
                            WHEN CONVERT(VARCHAR(128), SERVERPROPERTY ('productversion')) like '14%' THEN 'SQL Server 2017' 
                            ELSE 'unknown'
                        END AS MajorVersion,
                        SERVERPROPERTY('ProductLevel') AS ProductLevel,
                        SERVERPROPERTY('Edition') AS Edition,
                        SERVERPROPERTY('ProductVersion') AS ProductVersion";
        }

        public static string IsServerAgentIsRunning()
        {
            return @"IF EXISTS (  SELECT 1 
                    FROM master.dbo.sysprocesses 
                    WHERE program_name = N'SQLAgent - Generic Refresher')

                    BEGIN
                        SELECT 1 AS 'SQLServerAgentRunning'
                    END
                    ELSE 
                    BEGIN
                        SELECT 0 AS 'SQLServerAgentRunning'
                    END";
        }
        public static string GetDatabaseTempDBsInfo()
        {
            return @"
                    
                    SELECT       DB_NAME(database_id)                       AS DatabaseName 
                                ,Name                                       AS LogicalName 
                                ,type_desc                                  AS FileTypeDesc 
                                ,Physical_Name                              AS PhysicalName 
                                ,State_Desc                                 AS StateDesc 

                                ,CASE WHEN max_size = 0  THEN N'No growth allowed'
                                      WHEN max_size = -1 THEN N'Automatic growth'
                                 ELSE LTRIM(STR(max_size * 8.0 / 1024 /1024, 14, 2)) + 'GB' END AS MaxSize 

                                ,CAST(size * 8.0 / 1024 AS DECIMAL(8, 2)) AS [Size]

                                FROM     sys.master_files
                                WHERE database_id =2 -- tempdb
                                ORDER BY 1";
        }
    }
}
