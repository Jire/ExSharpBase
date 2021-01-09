using System.Drawing;
using System.Windows.Forms;
using ExSharpBase.Devices;
using ExSharpBase.Enums;
using ExSharpBase.Game;
using ExSharpBase.Game.Objects;

namespace ExSharpBase.OrbService
{
    internal static class Orbwalker
    {
        private static bool IsOrbAttackable = true;

        private static int LastAaTick;
        private static Point LastMovePoint;

        public static void Orbwalk()
        {
            var enemyPosition = ObjectManager.GetEnemyPosition();
            var attackDelay = (int) (1000.0f / LocalPlayer.GetAttackSpeed());

            if (IsOrbAttackable && enemyPosition != Point.Empty)
            {
                LastMovePoint = Cursor.Position;

                Engine.IssueOrder(GameObjectOrder.AttackUnit, enemyPosition);
                Engine.IssueOrder(GameObjectOrder.AttackUnit, enemyPosition);

                Engine.IssueOrder(GameObjectOrder.MoveTo, LastMovePoint);

                LastAaTick = (int) ((Engine.GetGameTime() * 1000) + attackDelay);

                IsOrbAttackable = false;
            }
            else
            {
                if ((Engine.GetGameTime() * 1000) >= LastAaTick)
                {
                    Mouse.MouseClickRight();

                    IsOrbAttackable = true;
                }
                else
                {
                    LastMovePoint = Cursor.Position;
                    Engine.IssueOrder(GameObjectOrder.MoveTo, LastMovePoint);
                    LastMovePoint = Cursor.Position;
                }
            }
        }
    }
}