using System.Windows.Forms;
using ExSharpBase.Modules;

namespace ExSharpBase
{
    internal static class Main
    {
        public static void OnMain()
        {
            if (false && Utils.IsKeyPressed(Keys.Space) && Utils.IsGameOnDisplay())
            {
                OrbService.Orbwalker.Orbwalk();

                /*Point EnemyPosition = ObjectManager.GetEnemyPosition();

                if (EnemyPosition != Point.Empty)
                {
                    SpellBook.CastSpell(SpellBook.SpellSlot.Q, EnemyPosition);

                    SpellBook.SpellSlot[] SpellArray = new SpellBook.SpellSlot[]
                    {
                        SpellBook.SpellSlot.Q,
                        SpellBook.SpellSlot.W,
                        SpellBook.SpellSlot.E
                    };

                    SpellBook.CastMultiSpells(SpellArray, EnemyPosition);
                }
                else
                {
                    Engine.IssueOrder(Enums.GameObjectOrder.MoveTo, Cursor.Position);
                }*/
            }
        }
    }
}