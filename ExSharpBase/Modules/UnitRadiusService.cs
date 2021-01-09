using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace ExSharpBase.Modules
{
    internal static class UnitRadiusService
    {
        public static void ParseUnitRadiusData()
        {
            try
            {
                var unitRadiusDataString = File.ReadAllText(Directory.GetCurrentDirectory() + @"\UnitRadius.json");
                Game.Objects.LocalPlayer.UnitRadiusData = JObject.Parse(unitRadiusDataString);

                LogService.Log("Successfully Parsed Unit Radius Data.");
            }
            catch (Exception ex)
            {
                LogService.Log(ex.ToString(), Enums.LogLevel.Error);
                throw new Exception("UnitRadiusParseExecption");
            }
        }
    }
}