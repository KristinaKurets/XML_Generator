CREATE PROCEDURE [dbo].[LastMonthPayments]
AS
	SELECT People.ID, People.[Name], People.LastName, [Sum] = SUM(Payments.[Sum])
	FROM People JOIN Payments ON People.ID = Payments.PersonId
	WHERE People.ID = Payments.PersonId AND Payments.[Date]>=GETDATE()-30
	GROUP BY People.ID, People.[Name], People.LastName;
GO