CREATE PROCEDURE [dbo].[PeopleFromXML]
AS
BEGIN
SET IDENTITY_INSERT People ON
TRUNCATE TABLE People
CREATE TABLE #myTempTable
(
    [Xml] xml
);
INSERT INTO #myTempTable(Xml)
    SELECT CONVERT(XML, BulkColumn) AS BulkColumn
    FROM OPENROWSET(BULK 'C:\Users\Kris\source\repos\XML_Generator\LINQ\bin\Debug\netcoreapp3.1\BaseOfNames.xml', SINGLE_BLOB) AS x;
DECLARE @XML AS XML, 
        @hDoc AS INT, 
        @SQL NVARCHAR (MAX)
SELECT @XML = [Xml] FROM #myTempTable
EXEC sp_xml_preparedocument @hDoc OUTPUT, @XML
INSERT INTO dbo.People(
    Id, Name, LastName
)
SELECT Id,
    Name, 
    LastName
FROM OPENXML(@hDoc, 'ArrayOfPerson/Person')
WITH 
( 
    ID int 'ID',
    Name varchar (50) 'Name',
    LastName varchar (60) 'LastName'
   
)
EXEC sp_xml_removedocument @hDoc
SELECT * FROM People
SET IDENTITY_INSERT People OFF
END
