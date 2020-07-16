CREATE PROCEDURE [dbo].[UserSumPayments]
	@id bigint
AS
	SELECT People.ID, People.[Name], People.LastName, SUM(Payments.[Sum]) as TotalSum
	FROM People JOIN Payments ON People.ID = Payments.PersonId
	WHERE People.ID = @id
	GROUP BY People.ID, People.[Name], People.LastName;


	