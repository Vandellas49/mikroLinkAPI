CREATE TABLE #Hatalar_[@tempName] ( Id INT PRIMARY KEY IDENTITY(1,1), HataKodu INT, HataMesaji NVARCHAR(500), ComponentId NVARCHAR(MAX) NULL, Serinumarasi NVARCHAR(MAX) NULL, MalzemeTuru NVARCHAR(MAX) NULL ); 
CREATE TABLE #TempCsv_[@tempName] (CsvId NVARCHAR(MAX), CsvColumn1 NVARCHAR(MAX), CsvColumn2 NVARCHAR(MAX));
BULK INSERT #TempCsv_[@tempName] FROM '{filepath}' 
WITH (FIELDTERMINATOR = ',', ROWTERMINATOR = '\n', FIRSTROW = 2,FORMAT='CSV',CODEPAGE = '65001');
INSERT INTO #Hatalar_[@tempName] (HataKodu, HataMesaji, ComponentId) SELECT 1, 'ParçaKodu excelde birden fazla kez bulunuyor.', CsvId FROM #TempCsv_[@tempName] GROUP BY CsvId HAVING COUNT(*) > 1; 
INSERT INTO #Hatalar_[@tempName] (HataKodu, HataMesaji, ComponentId) SELECT 1, 'ParçaKodu uygulamada zaten mevcut.', t.CsvId FROM #TempCsv_[@tempName] AS t JOIN Component AS cs ON t.CsvId = cs.Id; 
SELECT * FROM #Hatalar_[@tempName]; 
DROP TABLE #TempCsv_[@tempName];
DROP TABLE #Hatalar_[@tempName];


