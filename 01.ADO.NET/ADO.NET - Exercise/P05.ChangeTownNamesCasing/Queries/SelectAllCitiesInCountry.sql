	SELECT t.[Name] AS [Town Name],
		   t.[CountryCode] 
	  FROM [Countries] AS c
 LEFT JOIN [Towns] AS t
        ON t.[CountryCode] = c.Id
	 WHERE c.[Name] = @CountryName