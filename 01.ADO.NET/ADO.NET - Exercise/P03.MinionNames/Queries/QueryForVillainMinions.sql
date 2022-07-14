   SELECT m.[Name] AS [Minion Name],
		  m.[Age] AS [Minion Age]
     FROM [Villains] AS v
LEFT JOIN [MinionsVillains] as mv
       ON v.[Id] = mv.[VillainId] 
LEFT JOIN [Minions] AS m
       ON mv.[MinionId] = m.[Id]
    WHERE v.[Id] = @VillainId
 ORDER BY m.[Name]