using UnityEngine;

namespace RPKCharacters
{
    public class RPKCharBuster : RPKChar
    {
        public override string name { get { return "Buster"; } }
        protected override int maxHp { get { return 6; } }
        public RPKCharBuster(Player p_me, Player p_them, RectTransform myUI) :
            base(p_me, p_them, myUI)
        {
            RegisterThrow(RPKThrow.DefaultRPKThrow(RPKChoice.Rock, p_me, p_them));
            RegisterThrow(RPKThrow.DefaultRPKThrow(RPKChoice.Paper, p_me, p_them));
            RegisterThrow(RPKThrow.DefaultRPKThrow(RPKChoice.Scissors, p_me, p_them));

            throwSelection[RPKChoice.Rock] = throwLibrary["Default Rock"];
            throwSelection[RPKChoice.Paper] = throwLibrary["Default Paper"];
            throwSelection[RPKChoice.Scissors] = throwLibrary["Default Scissors"];
        }
    }
}
