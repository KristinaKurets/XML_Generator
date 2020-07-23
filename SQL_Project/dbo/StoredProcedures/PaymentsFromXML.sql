CREATE PROCEDURE [dbo].[PaymentsFromXML]
AS
BEGIN
SET IDENTITY_INSERT Payments ON
TRUNCATE TABLE Payments
CREATE TABLE #myTempTable
(
    [Xml] xml
);
INSERT INTO #myTempTable(Xml)
    SELECT CONVERT(XML, BulkColumn) AS BulkColumn
    FROM OPENROWSET(BULK 'C:\Users\Kris\source\repos\XML_Generator\LINQ\bin\Debug\netcoreapp3.1\BaseOfPayments.xml', SINGLE_BLOB) AS x;
DECLARE @XML AS XML, 
        @hDoc AS INT, 
        @SQL NVARCHAR (MAX)
SELECT @XML = [Xml] FROM #myTempTable
EXEC sp_xml_preparedocument @hDoc OUTPUT, @XML
INSERT INTO dbo.Payments(
    Id, PersonId, Sum,
    [Date]
)
SELECT Id,
    PersonId, 
    Sum, 
    [Date]
FROM OPENXML(@hDoc, 'ArrayOfPayment/Payment')
WITH 
( 
    ID int 'ID',
    PersonId int 'PersonId',
    Sum int 'Sum',
    [Date] date 'Date'
)
EXEC sp_xml_removedocument @hDoc
SELECT * FROM Payments
SET IDENTITY_INSERT Payments OFF
END




--CREATE PROCEDURE [dbo].[PaymentsFromXML]
--AS
--BEGIN
--INSERT INTO Payments(Sum, Date, PersonId)
--SELECT CONVERT(XML, BulkColumn) AS BulkColumn, GETDATE() 
--FROM OPENROWSET(BULK 'BaseOfPayments.xml', SINGLE_BLOB) AS x;

--SELECT * FROM Payments
--END