using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chess.Scripts.Core
{
    public static class MoveCalculator
    {
        //  Get linear moves for Rook and Queen (horizontal and vertical directions)
        public static void GetLinearMoves(int row, int col, List<Vector2Int> moves, bool isWhite)
        {
            var directions = new Vector2Int[] {
                new Vector2Int(1, 0), new Vector2Int(-1, 0),
                new Vector2Int(0, 1), new Vector2Int(0, -1)
            };

            foreach (var dir in directions)
            {
                int r = row + dir.x;
                int c = col + dir.y;

                while (IsValid(r, c))
                {
                    //  Stop if a piece is found (enemy or friendly)
                    if (HighlightMoveOrCapture(r, c, isWhite, moves)) break;
                    r += dir.x;
                    c += dir.y;
                }
            }
        }

        //  Get diagonal moves for Bishop and Queen
        public static void GetDiagonalMoves(int row, int col, List<Vector2Int> moves, bool isWhite)
        {
            var directions = new Vector2Int[] {
                new Vector2Int(1, 1), new Vector2Int(1, -1),
                new Vector2Int(-1, 1), new Vector2Int(-1, -1)
            };

            foreach (var dir in directions)
            {
                int r = row + dir.x;
                int c = col + dir.y;

                while (IsValid(r, c))
                {
                    // Stop if a piece is found (enemy or friendly)
                    if (HighlightMoveOrCapture(r, c, isWhite, moves)) break;
                    r += dir.x;
                    c += dir.y;
                }
            }
        }

        // Highlights moves and stops if a piece is found
        private static bool HighlightMoveOrCapture(int row, int col, bool isWhite, List<Vector2Int> moves)
        {
            var tile = ChessBoardPlacementHandler.Instance.GetTile(row, col);
            if (tile != null)
            {
                if (tile.TryGetComponent<ChessPiece>(out var piece))
                {
                    if (piece.isWhite != isWhite)
                    {
                        //  Highlight enemy piece position in RED
                        ChessBoardPlacementHandler.Instance.Highlight(row, col, Color.red);
                        moves.Add(new Vector2Int(row, col));
                    }
                    //  Stop if a piece is found (enemy or friendly)
                    return true;
                }
                else
                {
                    // Highlight empty tile in GREEN
                    ChessBoardPlacementHandler.Instance.Highlight(row, col, Color.green);
                    moves.Add(new Vector2Int(row, col));
                }
            }
            return false;  // Continue checking in this direction if tile is empty
        }

        // Valid position check
        private static bool IsValid(int row, int col)
        {
            return row >= 0 && row < 8 && col >= 0 && col < 8;
        }
    }
}
