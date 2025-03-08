using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chess.Scripts.Core
{
    public abstract class ChessPiece : MonoBehaviour
    {
        public int row, column;
        public bool isWhite;

        protected virtual void Start()
        {
            transform.position = ChessBoardPlacementHandler.Instance.GetTile(row, column).transform.position;
        }

        public abstract List<Vector2Int> GetPossibleMoves();

        // Common method to set position
        public void SetPosition(int r, int c)
        {
            row = r;
            column = c;
        }

        // Check if a target tile has an enemy piece
        protected bool IsEnemy(int targetRow, int targetCol)
        {
            if (targetRow < 0 || targetRow >= 8 || targetCol < 0 || targetCol >= 8)
                return false;  // Outside the board

            var tile = ChessBoardPlacementHandler.Instance.GetTile(targetRow, targetCol);
            if (tile != null && tile.TryGetComponent<ChessPiece>(out var piece))
            {
                if (piece.isWhite != this.isWhite)
                {
                    Debug.Log("Enemy piece at: (" + targetRow + ", " + targetCol + ")");
                    return true;  // Enemy piece found
                }
                else
                {
                    Debug.Log("Friendly piece at: (" + targetRow + ", " + targetCol + ")");
                }
            }
            else if (tile == null)
            {
                Debug.LogError("Tile not found at: (" + targetRow + ", " + targetCol + ")");
            }

            return false;  // No enemy piece on the tile or outside the board
        }

        // Check if a target tile has a friendly piece
        protected bool IsFriendly(int targetRow, int targetCol)
        {
            if (targetRow < 0 || targetRow >= 8 || targetCol < 0 || targetCol >= 8)
                return false;  // Outside the board

            var tile = ChessBoardPlacementHandler.Instance.GetTile(targetRow, targetCol);
            if (tile != null && tile.TryGetComponent<ChessPiece>(out var piece))
            {
                if (piece.isWhite == this.isWhite)
                {
                    Debug.Log("Friendly piece at: (" + targetRow + ", " + targetCol + ")");
                    return true;  // Friendly piece found
                }
                else
                {
                    Debug.Log("Enemy piece at: (" + targetRow + ", " + targetCol + ")");
                }
            }
            else if (tile == null)
            {
                Debug.LogError("Tile not found at: (" + targetRow + ", " + targetCol + ")");
            }

            return false;  // No friendly piece on the tile or outside the board
        }
    }
}
