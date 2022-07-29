using SoftUni;
using Newtonsoft.Json;

string json = File.ReadAllText("../../../JSON/test.json");

//Newtonsoft.Json
Town? town = JsonConvert.DeserializeObject<Town>(json);
File.WriteAllText("../../../JSON/testResult.json", json);

////System.Text.Json
//Town? town = JsonSerializer.Deserialize<Town>(json);
//Console.WriteLine(town.Name + Environment.NewLine + town.TownId);
//Console.WriteLine(JsonSerializer.Serialize(town));