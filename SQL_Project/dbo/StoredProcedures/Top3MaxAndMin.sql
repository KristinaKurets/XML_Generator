CREATE PROCEDURE [dbo].[Top3MaxAndMin]
AS
BEGIN

	WITH a as (
		SELECT TOP 3 People.ID, People.[Name], People.LastName, SUM(Payments.[Sum]) as TotalSum
		FROM People JOIN Payments ON People.ID = Payments.PersonId
		WHERE Payments.[Date]>=GETDATE()-365
		GROUP BY People.ID, People.[Name], People.LastName
		ORDER BY SUM(Payments.[Sum]) DESC),
		
		b as (SELECT TOP 3 People.ID, People.[Name], People.LastName, SUM(Payments.[Sum]) as TotalSum
	FROM People JOIN Payments ON People.ID = Payments.PersonId
	WHERE Payments.[Date] >= GETDATE()-365
	GROUP BY People.ID, People.[Name], People.LastName
	ORDER BY SUM(Payments.[Sum]) ASC)
	
	SELECT * from a 
	union
	SELECT * from b

END