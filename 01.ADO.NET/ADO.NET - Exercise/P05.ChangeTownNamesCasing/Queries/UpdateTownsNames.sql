UPDATE [Towns]
   SET [Name] = UPPER([Name])
 WHERE [CountryCode] = @CountryCode 