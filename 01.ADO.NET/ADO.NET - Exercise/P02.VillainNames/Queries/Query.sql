   SELECT v.[Name] AS [Villain Name],
          COUNT(m.[Id]) AS [Number of Minions]
     FROM [Villains] AS v
LEFT JOIN [MinionsVillains] as mv
       ON v.[Id] = mv.[VillainId] 
LEFT JOIN [Minions] AS m
       ON mv.[MinionId] = m.[Id]
 GROUP BY v.[Name]
   HAVING COUNT(m.[Id]) > 3
 ORDER BY COUNT(m.[Id]) DESC