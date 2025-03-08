using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chess.Scripts.Core
{
    public class Rook : ChessPiece
    {
        public override List<Vector2Int> GetPossibleMoves()
        {
            var moves = new List<Vector2Int>();
            GetRookMoves(moves);
            return moves;
        }

        private void GetRookMoves(List<Vector2Int> moves)
        {
            // Check all four directions (up, down, left, right)
            CheckDirection(1, 0, moves);   // Up
            CheckDirection(-1, 0, moves);  // Down
            CheckDirection(0, 1, moves);   // Right
            CheckDirection(0, -1, moves);  // Left
        }

        private void CheckDirection(int rowDir, int colDir, List<Vector2Int> moves)
        {
            int currentRow = row + rowDir;
            int currentCol = column + colDir;

            while (currentRow >= 0 && currentRow < 8 && currentCol >= 0 && currentCol < 8)
            {
                var tile = ChessBoardPlacementHandler.Instance.GetTile(currentRow, currentCol);

                if (tile != null)
                {
                    // Check if a non-chess object is blocking the path
                    if (tile.TryGetComponent<Collider2D>(out _))
                    {
                        if (!tile.TryGetComponent<ChessPiece>(out _))
                        {
                            Debug.LogWarning("Blocked by non-chess object at: (" + currentRow + ", " + currentCol + ")");
                            break;  // Stop if blocked by a non-chess object
                        }
                    }

                    // Check for chess pieces
                    if (tile.TryGetComponent<ChessPiece>(out var piece)) // declare and assign a variable
                    {
                        if (piece.isWhite != this.isWhite)
                        {
                            // Capture move if enemy piece
                            moves.Add(new Vector2Int(currentRow, currentCol));
                            Debug.LogWarning("Enemy piece at: (" + currentRow + ", " + currentCol + ")");
                            break;  // Stop further moves beyond enemy piece
                        }
                        else
                        {
                            Debug.Log("Friendly piece at: (" + currentRow + ", " + currentCol + ")");
                            break;  // Stop if blocked by friendly piece
                        }
                    }
                    else
                    {
                        // Empty tile, add move
                        moves.Add(new Vector2Int(currentRow, currentCol));
                        Debug.LogWarning("Empty tile at: (" + currentRow + ", " + currentCol + ")");
                    }
                }
                else
                {
                    Debug.Log("Invalid tile or out of bounds: (" + currentRow + ", " + currentCol + ")");
                    break;  // Stop if out of bounds or invalid tile
                }

                // Move to the next tile in the same direction
                currentRow += rowDir;
                currentCol += colDir;
            }
        }
    }
}
