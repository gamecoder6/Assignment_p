using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chess.Scripts.Core
{
    public class Knight : ChessPiece
    {
        public override List<Vector2Int> GetPossibleMoves()
        {
            var moves = new List<Vector2Int>();
            ShowPossibleMoves(moves);
            return moves;
        }

        private void ShowPossibleMoves(List<Vector2Int> moves)
        {
            // L-shaped move patterns for the Knight
            int[,] moveOffsets = new int[,]
            {
                { 2, 1 }, { 2, -1 }, { -2, 1 }, { -2, -1 },
                { 1, 2 }, { 1, -2 }, { -1, 2 }, { -1, -2 }
            };

            for (int i = 0; i < moveOffsets.GetLength(0); i++)
            {
                int newRow = row + moveOffsets[i, 0];
                int newCol = column + moveOffsets[i, 1];

                if (newRow >= 0 && newRow < 8 && newCol >= 0 && newCol < 8)
                {
                    var tile = ChessBoardPlacementHandler.Instance.GetTile(newRow, newCol);

                    if (tile != null)
                    {
                        // Check if the tile has a piece
                        if (tile.TryGetComponent<ChessPiece>(out var piece))
                        {
                            if (piece.isWhite != this.isWhite)
                            {
                                // Capture move if enemy piece
                                moves.Add(new Vector2Int(newRow, newCol));
                                Debug.Log("Enemy piece at: (" + newRow + ", " + newCol + ")");
                            }
                            else
                            {
                                Debug.Log("Blocked by friendly piece at: (" + newRow + ", " + newCol + ")");
                            }
                        }
                        else
                        {
                            // Empty tile, add move
                            moves.Add(new Vector2Int(newRow, newCol));
                            Debug.Log("Empty tile at: (" + newRow + ", " + newCol + ")");
                        }
                    }
                    else
                    {
                        Debug.Log("Invalid tile or out of bounds: (" + newRow + ", " + newCol + ")");
                    }
                }
            }
        }
    }
}
