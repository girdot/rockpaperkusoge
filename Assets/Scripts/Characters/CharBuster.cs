using UnityEngine;

namespace RPKCharacters
{
    public class RPKCharBuster : RPKChar
    {
        public const string CharName = "Buster";
        public override string name { get { return CharName; } }
        protected override int maxHp { get { return 6; } }

        [RuntimeInitializeOnLoadMethod]
        static void Register()
        {
            RPKChar.Register(CharName, (me, them, ui) => new RPKCharBuster(me, them, ui));
        }

        public RPKCharBuster(Player p_me, Player p_them, RectTransform myUI) :
            base(p_me, p_them, myUI)
        {
            SelectThrow(RPKThrow.DefaultRPKThrow(RPKChoice.Rock, p_me, p_them));
            SelectThrow(RPKThrow.DefaultRPKThrow(RPKChoice.Paper, p_me, p_them));
            SelectThrow(RPKThrow.DefaultRPKThrow(RPKChoice.Scissors, p_me, p_them));
        }
    }
}
