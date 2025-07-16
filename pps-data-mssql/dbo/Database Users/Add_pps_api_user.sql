USE [master]
CREATE LOGIN [pps-api] WITH PASSWORD = 'pps-api!';


USE [PPS];
GO

CREATE USER [pps-api] FOR LOGIN [pps-api];

ALTER ROLE db_datareader ADD MEMBER [pps-api];
ALTER ROLE db_datawriter ADD MEMBER [pps-api];

-- For this to work, I had to turn on SQL Server and Windows Authentication mode in server -> properties -> security.