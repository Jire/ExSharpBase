using System.Collections.Generic;
using ExSharpBase.Game.Objects;
using ExSharpBase.Modules;
using SharpDX;

namespace ExSharpBase.Events
{
    internal static class Drawing
    {
        public static readonly Dictionary<string, bool> DrawingProperties = new Dictionary<string, bool>()
        {
            {"DrawRange", true}
        };

        public static bool IsMenuBeingDrawn = false;

        public static void OnDeviceDraw()
        {
            if (!Utils.IsGameOnDisplay()) return;
            //When ~ key is pressed...
            DrawMenu();

            if (DrawingProperties["DrawRange"] != true) return;
            LocalPlayer.DrawAttackRange(Color.Cyan, 2.5f);

            LocalPlayer.DrawAllSpellRange(Color.OrangeRed);
        }

        private static void DrawMenu()
        {
            if (Utils.IsKeyPressed(System.Windows.Forms.Keys.Oemtilde))
            {
                Program.MenuBasePlate.Show();
                IsMenuBeingDrawn = true;
            }
            else
            {
                Program.MenuBasePlate.Hide();
            }
        }
    }
}