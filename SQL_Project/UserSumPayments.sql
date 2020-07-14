CREATE PROCEDURE [dbo].[UserSumPayments]
	@id bigint
AS
	SELECT People.ID, People.[Name], People.LastName, [Sum] = SUM(Payments.[Sum]) 
	FROM People JOIN Payments ON People.ID = Payments.PersonId
	WHERE People.ID = @id AND Payments.PersonId = @id
	GROUP BY People.ID, People.[Name], People.LastName;


	