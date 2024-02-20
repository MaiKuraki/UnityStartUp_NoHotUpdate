using UnityEngine;

namespace CycloneGames.GameFramework
{
    public class PlayerState : Actor
    {
        private Pawn pawnPrivate;
        public Pawn GetPawn() => pawnPrivate;
        public T GetPawn<T>() where T : Pawn
        {
            return pawnPrivate is T p ? p : null;
        }

        public void SetPawnPrivate(Pawn InPawn)
        {
            if (!InPawn.Equals(pawnPrivate))
            {
                pawnPrivate = InPawn;
            }
        }
    }
}