   SELECT v.[Name] AS [VillainName]
		  , mv.[MinionId]
     FROM [Villains] AS v
LEFT JOIN [MinionsVillains] AS mv
       ON v.[Id] = mv.[VillainId]
    WHERE v.[Id] = @VillainId
 ORDER BY v.[Name]