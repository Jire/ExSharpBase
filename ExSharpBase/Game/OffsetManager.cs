using ExSharpBase.Modules;

namespace ExSharpBase.Game
{
    internal class OffsetManager
    {
        private static readonly int BaseAddress = Utils.GetLeagueProcess().MainModule.BaseAddress.ToInt32();

        public class Instances
        {
            public const int SPELL_BOOK = 0x2708;

            //Version 10.20.337.6669 [PUBLIC] // 8B 44 24 04 BA ? ? ? ? 2B D0
            public static readonly int
                LocalPlayer = BaseAddress + 0x34EEDE4; // 10.25.348.1797// string xref blueHero -> Above "hero" subrtn

            public static readonly int
                Renderer = BaseAddress +
                           0x35179E4; // 10.25.348.1797// 8B 15 ? ? ? ? 83 EC 08 F3 // ["blurKernelSigma", +0x27F] // xref the string, move -0x27f there should be a dword.

            public static readonly int
                ViewMatrix =
                    BaseAddress +
                    0x3514BE8; // 10.25.348.1797// B9 ? ? ? ? E8 ? ? ? ? B9 ? ? ? ? E9 ? ? ? ? // First result: unk_0x...

            public static readonly int UnderMouseObject = BaseAddress + 0x1BF2168; // no find// 8B 0D ? ? ? ? 89 0D
        }

        public class Object
        {
            private const int CHAMPION_NAME = 0x3134;
            public const int POS = 0x1D8;
            public const int NAME = 0x6C;
            public const int VISIBILITY = 0x270;
            public const int ATTACK_RANGE = 0x12B8;
        }
    }
}