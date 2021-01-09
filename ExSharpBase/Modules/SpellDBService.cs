using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace ExSharpBase.Modules
{
    internal static class SpellDbService
    {
        public static void ParseSpellDbData()
        {
            //https://github.com/ZeroLP/SpellDB/blob/master/SpellDB.json%20Versions/SpellDB_10.12.json
            try
            {
                var spellDbDataString = File.ReadAllText(Directory.GetCurrentDirectory() + @"\SpellDB.json");
                var championName = Game.Objects.LocalPlayer.GetChampionName().Replace(" ", string.Empty);

                Game.Spells.SpellBook.SpellDB = JObject.Parse(spellDbDataString)[championName]?.ToObject<JObject>();

                LogService.Log("Successfully Parsed SpellDB.");
            }
            catch (Exception ex)
            {
                LogService.Log(ex.ToString(), Enums.LogLevel.Error);
                throw new Exception("SpellDBParseExecption");
            }
        }
    }
}