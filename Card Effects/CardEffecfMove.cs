using Monopoly.Enums;
using Monopoly.Interfaces;
using Monopoly.Squares;

namespace Monopoly.Card_Effects
{
    public class CardEffecfMove : BaseCardEffect
    {
        public CardEffecfMove(string[] parameters) : base(parameters) {
            string targetPosition = parameters[2];
            string MoveEffect = parameters[4];

            if (targetPosition == "Rail") targetPositionType = SquareType.Rail;
            if (targetPosition == "Utility") targetPositionType = SquareType.Utilities;

            if (MoveEffect.Contains("Go")) moveEffect = MoveEffects.Go;
            if (MoveEffect.Contains("Dice")) moveEffect = MoveEffects.Dice;
            if (MoveEffect.Contains("Rail")) moveEffect = MoveEffects.Rent;
        }

        SquareType targetPositionType = SquareType.Go;
        MoveEffects moveEffect;
        public enum MoveEffects
        {
            Rent,
            Dice,
            Go
        }

        public override void PlayEffect()
        {
            Player player = Board.Instance.currentPlayer;
            int PlayerPosition = player.CurrentSqure;

            ISquare ClosestSquare = null;
            string TargetPosition = parameters[2];
            // parameters[2] targetPosition
            // parameters[5] Effect

            if (int.TryParse(TargetPosition, out int i)) // Check if it is the only one integar value
            {
                player.CurrentSqure -= 3;
                return;
            }

            if (targetPositionType != SquareType.Go) // Find the nearest rail // utility
            {
                ClosestSquare = Board.Instance.Squares.FindAll(e => e.Type == targetPositionType).OrderBy(i => Math.Abs(PlayerPosition - i.Position)).First();
            }
            else // Find Via name
            {
                ClosestSquare = Board.Instance.Squares.Find(e => e.Name.ToLower().Contains(TargetPosition.ToLower()));
                if (moveEffect == MoveEffects.Go && PlayerPosition > ClosestSquare.Position) player.Money += 200;
            }


            if (ClosestSquare.TryGetValue<OwnableLand>(out OwnableLand land) && (moveEffect == MoveEffects.Rent || moveEffect == MoveEffects.Dice))
            {
                int rent = moveEffect == MoveEffects.Dice ? Utilities.RollD6() * 10 : land.GetRent() * 2;

                if (land.Owner == null) player.BuyProperty(land);
                else if (land.Owner != player) land.PayRent(player, land.GetRent() * 2);
            }

            player.CurrentSqure = ClosestSquare.Position;
        }
    }
}
