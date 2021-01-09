using System;
using System.Drawing;
using System.Windows.Forms;
using ExSharpBase.Devices;
using ExSharpBase.Enums;
using ExSharpBase.Modules;

namespace ExSharpBase.Game
{
    internal static class Engine
    {
        public static int GetLocalPlayer { get; } = Memory.Read<int>(OffsetManager.Instances.LocalPlayer);

        public static float GetGameTime()
        {
            return API.GameStats.GetGameTime();
        }

        public static int GetObjectUnderMouse() //Use at risk [Possible Detection?]
        {
            return Memory.Read<int>(OffsetManager.Instances.UnderMouseObject);
        }

        public static void IssueOrder(GameObjectOrder Order, Point Vector2D = new Point())
        {
            if (!Utils.IsGameOnDisplay()) return;
            switch (Order)
            {
                case GameObjectOrder.HoldPosition:
                    Keyboard.SendKey((short) Keyboard.KeyBoardScanCodes.KEY_S);
                    break;
                case GameObjectOrder.MoveTo:
                    if (Vector2D.X == 0 && Vector2D.Y == 0)
                    {
                        Mouse.MouseClickRight();
                        break;
                    }

                    if (Vector2D == new Point(Cursor.Position.X, Cursor.Position.Y))
                    {
                        Mouse.MouseClickRight();
                        break;
                    }

                    Mouse.MouseMove(Vector2D.X, Vector2D.Y);
                    Mouse.MouseClickRight();
                    break;
                case GameObjectOrder.AttackUnit:
                    if (Vector2D.X == 0 && Vector2D.Y == 0)
                    {
                        Mouse.MouseMove(Cursor.Position.X, Cursor.Position.Y);
                        Mouse.MouseClickRight();
                        break;
                    }

                    Mouse.MouseMove(Vector2D.X, Vector2D.Y);
                    Mouse.MouseClickRight();
                    break;
                case GameObjectOrder.AutoAttack:
                    Keyboard.SendKey((short) Keyboard.KeyBoardScanCodes.KEY_OPENING_BRACKETS);
                    break;
                case GameObjectOrder.Stop:
                    Keyboard.SendKey((short) Keyboard.KeyBoardScanCodes.KEY_S);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(Order), Order, null);
            }
        }
    }
}