CREATE OR ALTER PROCEDURE CategoryMst_SelectAll
AS
	SELECT	ID,
			NAME,
			CREATIONDATE,
			MODIFICATIONDATE
	FROM 
	TBLCATEGORY WITH(NOLOCK)
	ORDER BY NAME





