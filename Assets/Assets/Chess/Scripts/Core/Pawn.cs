using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chess.Scripts.Core
{
    // Pawn inherits from ChessPiece
    public class Pawn : ChessPiece
    {
        // Override abstract method to provide possible moves
        public override List<Vector2Int> GetPossibleMoves()
        {
            var moves = new List<Vector2Int>();
            GetPawnMoves(moves);
            return moves;
        }

        // Define possible moves for the pawn
        private void GetPawnMoves(List<Vector2Int> moves)
        {
            int direction = isWhite ? 1 : -1;  // White moves up, Black moves down

            // Forward move
            if (IsValidMove(row + direction, column))
                moves.Add(new Vector2Int(row + direction, column));

            // Double move from starting position
            if ((isWhite && row == 1) || (!isWhite && row == 6))
            {
                if (IsValidMove(row + 2 * direction, column) && IsValidMove(row + direction, column))
                    moves.Add(new Vector2Int(row + 2 * direction, column));
            }

            // Capture moves (diagonal)
            if (IsEnemy(row + direction, column + 1))
                moves.Add(new Vector2Int(row + direction, column + 1));

            if (IsEnemy(row + direction, column - 1))
                moves.Add(new Vector2Int(row + direction, column - 1));
        }

        // Check if the move is valid (no piece blocking)
        private bool IsValidMove(int targetRow, int targetCol, bool checkForPiece = false)
        {
            if (targetRow < 0 || targetRow >= 8 || targetCol < 0 || targetCol >= 8)
                return false;  // Prevent out-of-bounds access

            var tile = ChessBoardPlacementHandler.Instance.GetTile(targetRow, targetCol);
            Debug.Log("Tile blocked at: " + targetRow + ", " + targetCol);
            if (tile != null)
            {
                if (checkForPiece && tile.TryGetComponent<ChessPiece>(out _)) // check if the tile has a piece when moving forawrd(not capture)
                    return false;   //blocked by piece

                return true;
            }
            return false;
        }
    }
}
