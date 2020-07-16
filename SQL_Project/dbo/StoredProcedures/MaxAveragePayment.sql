CREATE PROCEDURE [dbo].[MaxAveragePayment]
AS
	SELECT TOP 5  People.ID, People.[Name], People.LastName, AVG(Payments.[Sum]) as TotalSum
	FROM People JOIN Payments ON People.ID = Payments.PersonId
	WHERE People.ID = Payments.PersonId AND Payments.[Date] >= GETDATE()-180
	GROUP by People.ID, People.[Name], People.LastName
	ORDER BY AVG(Payments.Sum) DESC
