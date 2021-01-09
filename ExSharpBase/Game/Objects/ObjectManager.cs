using System;
using System.Collections.Generic;
using System.Linq;
using ExSharpBase.Modules;
using SharpDX;
using Color = System.Drawing.Color;
using Point = System.Drawing.Point;
using Rectangle = System.Drawing.Rectangle;

namespace ExSharpBase.Game.Objects
{
    internal static class ObjectManager
    {
        private static Color RGB_PLAYER_HP_BAR_COLOUR = Color.FromArgb(49, 134, 33);

        private static Color RGB_CHAMPION_NAME_COLOR = Color.FromArgb(0xE3, 0xE3, 0xE3);
        private static Color RGB_LOSED_HP_BAR_COLOR = Color.FromArgb(0x0A, 0x0E, 0x11);
        private static Color RGB_MANA_BAR_COLOR = Color.FromArgb(0x42, 0xAA, 0xDE);

        private static Color RGB_ALLY_HP_BAR_COLOR = Color.FromArgb(0x31, 0x8A, 0x21);
        private static Color RGB_ALLY_LEVEL_COLOR = Color.FromArgb(0x00, 0x1C, 0x2C);
        private static Color RGB_ALLY_MINION_HP_BAR_COLOR = Color.FromArgb(0x2B, 0x5D, 0x79);

        private static Color RGB_ENEMY_HP_BAR_COLOR = Color.FromArgb(0x94, 0x24, 0x18);
        private static Color RGB_ENEMY_LEVEL_COLOR = Color.FromArgb(0x35, 0x03, 0x00);
        private static Color RGB_ENEMY_MINION_HP_BAR_COLOR = Color.FromArgb(0x79, 0x39, 0x37);

        public static Point GetEnemyPosition()
        {
            var w2S = Renderer.WorldToScreen(LocalPlayer.GetPosition());
            var range = (int) (LocalPlayer.GetAttackRange() + LocalPlayer.GetAttackRange());

            var fov = new Rectangle((int) w2S.X - range / 2, (int) w2S.Y - range / 2, range + 60, range - 100);
            var searched = PixelSearch.Search(fov, RGB_ENEMY_LEVEL_COLOR, 1);

            var result = new Point();

            if (searched.Length == 0) return result;
            var orderedY = searched.OrderBy(t => t.Y).ToArray();

            var list = new List<Tuple<Vector2, double>>();
            var array3 = orderedY;

            foreach (var point in array3)
            {
                var current = new Vector2(point.X, point.Y);
                if ((from t in list
                    where (t.Item1 - current).Length() < 25f || Math.Abs(t.Item1.X - current.X) < 25f
                    select t).Any()) continue;
                list.Add(new Tuple<Vector2, double>(current, (current - new Vector2(fov.X, fov.Y)).Length()));
                if (list.Count > 2)
                {
                    break;
                }
            }

            var (item1, _) = (from t in list orderby t.Item2 select t).ElementAt(0);
            var point2 = new Point((int) item1.X, (int) item1.Y);

            result.X = point2.X + 50;
            result.Y = point2.Y + 100;

            return result;
        }
    }
}