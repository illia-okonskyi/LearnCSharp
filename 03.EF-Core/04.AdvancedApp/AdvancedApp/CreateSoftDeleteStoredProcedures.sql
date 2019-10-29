USE AdvancedDb
GO

DROP PROCEDURE IF EXISTS RestoreSoftDelete
DROP PROCEDURE IF EXISTS PurgeSoftDelete
GO

CREATE PROCEDURE RestoreSoftDelete
AS
	BEGIN
		UPDATE Employees
		SET SoftDeleted = 0 WHERE SoftDeleted = 1
	END
GO

CREATE PROCEDURE PurgeSoftDelete
AS
	BEGIN
		DELETE FROM SecondaryIdentity WHERE Id IN
		(SELECT Id FROM Employees  emp
			INNER JOIN SecondaryIdentity ident ON ident.PrimarySSN = emp.SSN
				AND ident.PrimaryFirstName = emp.FirstName
				AND ident.PrimaryFamilyName = emp.FamilyName
			WHERE SoftDeleted = 1
		)
	END
	BEGIN
		DELETE FROM Employees
		WHERE SoftDeleted = 1
	END