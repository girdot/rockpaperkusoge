using UnityEngine;

namespace RPKCharacters
{
    public class RPKCharSamurai : RPKChar
    {
        public const string CharName = "Samurai";
        public override string name { get { return CharName; } }
        protected override int maxHp { get { return 6; } }

        [RuntimeInitializeOnLoadMethod]
        static void Register()
        {
            RPKChar.Register(CharName, (me, them, ui) => new RPKCharSamurai(me, them, ui));
        }

        public RPKCharSamurai(Player p_me, Player p_them, RectTransform myUI) :
            base(p_me, p_them, myUI)
        {
            SelectThrow(RPKThrow.DefaultRPKThrow(RPKChoice.Rock, p_me, p_them));
            SelectThrow(RPKThrow.DefaultRPKThrow(RPKChoice.Paper, p_me, p_them));
            SelectThrow(SamuraiScissors(p_me, p_them));
        }

        private static RPKThrow SamuraiScissors(Player p_me, Player p_opponent)
        {
            RPKThrow rpkThrow = new RPKThrow("Samurai Scissors", RPKChoice.Scissors, p_me, p_opponent);
            rpkThrow.OnWin += () => { p_opponent.character.Damage(2); };
            return rpkThrow;
        }
    }
}
