using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chess.Scripts.Core
{
    public class Queen : ChessPiece
    {
        public override List<Vector2Int> GetPossibleMoves()
        {
            var moves = new List<Vector2Int>();
            ShowPossibleMoves(moves);  // Get only the valid moves
            return moves;
        }

        private void ShowPossibleMoves(List<Vector2Int> moves)
        {
            // Horizontal moves
            CheckDirection(1, 0, moves);   // Up
            CheckDirection(-1, 0, moves);  // Down
            CheckDirection(0, 1, moves);   // Right
            CheckDirection(0, -1, moves);  // Left

            // Bdiagonals moves
            CheckDirection(1, 1, moves);   // Up-Right
            CheckDirection(1, -1, moves);  // Up-Left
            CheckDirection(-1, 1, moves);  // Down-Right
            CheckDirection(-1, -1, moves); // Down-Left
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
                    // Check for chess pieces on the tile
                    if (tile.TryGetComponent<ChessPiece>(out var piece))
                    {
                        if (piece.isWhite != this.isWhite)
                        {
                            // Capture move if it's an enemy piece
                            moves.Add(new Vector2Int(currentRow, currentCol));
                            Debug.Log("Enemy piece at: (" + currentRow + ", " + currentCol + ")");
                        }

                        // Stop further moves if blocked by any piece (friendly or enemy)
                        Debug.Log("Blocked by piece at: (" + currentRow + ", " + currentCol + ")");
                        break;
                    }
                    else
                    {
                        // Empty tile, add move
                        moves.Add(new Vector2Int(currentRow, currentCol));
                        Debug.Log("Empty tile at: (" + currentRow + ", " + currentCol + ")");
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
