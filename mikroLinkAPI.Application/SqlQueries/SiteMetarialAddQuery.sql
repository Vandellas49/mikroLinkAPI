CREATE TABLE #TempCsv_[@tempName] (SiteId NVARCHAR(max),ComponentId NVARCHAR(MAX), Serinumarasi NVARCHAR(MAX), girisIrsaliye NVARCHAR(MAX),saglam NVARCHAR(MAX),arzali NVARCHAR(MAX),hurda NVARCHAR(MAX),raf NVARCHAR(70),malzemeTuru NVARCHAR(100)); 
BULK INSERT #TempCsv_[@tempName] FROM '{filepath}' WITH ( FIELDTERMINATOR = ',', ROWTERMINATOR = '\n', FIRSTROW = 2, FORMAT='CSV',CODEPAGE = '65001' );
INSERT INTO dbo.ComponentSerial (ComponentId, G_IrsaliyeNo, Sturdy,Defective,Scrap,Shelf,MaterialType,CompanyId,SiteId,TeamLeaderId,[CreatedDate],SeriNo,[CreatedBy],[State])
SELECT ComponentId,girisIrsaliye,saglam,arzali,hurda,raf, (Select Id from dbo.MalzemeTuru where Value=malzemeTuru) as malzemeTuru,null,SiteId,null,getdate(),Serinumarasi,{User},6 FROM #TempCsv_[@tempName]; 
SELECT * FROM #TempCsv_[@tempName]; 
DROP TABLE #TempCsv_[@tempName];