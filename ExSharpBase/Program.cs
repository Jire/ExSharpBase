using System.Threading.Tasks;
using ExSharpBase.Modules;

namespace ExSharpBase
{
    internal static class Program
    {
        public static readonly Overlay.Base DrawBase = new Overlay.Base();
        public static readonly Menu.BasePlate MenuBasePlate = new Menu.BasePlate();

        private static void Main()
        {
            Task.Run(async () =>
            {
                await Task.Run(API.Service.IsLiveGameRunning);

                await Task.Run(() => LogService.Log("Found Live Instance of The Game."));

                await Task.Run(Events.EventsManager.SubscribeToEvents);

                await Task.Run(UnitRadiusService.ParseUnitRadiusData);

                await Task.Run(SpellDbService.ParseSpellDbData);

                await Task.Run(() => LogService.Log("Initialising Overlay Rendering..."));

                await Task.Run(() => DrawBase.Show());
            }).GetAwaiter().GetResult();
            LogService.Log("All done!");
        }
    }
}