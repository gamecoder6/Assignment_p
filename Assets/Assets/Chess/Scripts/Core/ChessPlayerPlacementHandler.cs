using System;
using UnityEngine;
using System.Collections.Generic;
namespace Chess.Scripts.Core
{

    public class ChessPlayerPlacementHandler : MonoBehaviour
    {
        [SerializeField] private int row, column;
        private ChessPiece selectedPiece;

        private void Start()
        {
            //  Place the piece at the specified tile position
            transform.position = ChessBoardPlacementHandler.Instance.GetTile(row, column).transform.position;
        }

        private void OnMouseDown()
        {
            Debug.Log("Piece Clicked");

            //  Check if a piece is clicked
            if (TryGetComponent<ChessPiece>(out selectedPiece))
            {
                //  Clear existing highlights
                ChessBoardPlacementHandler.Instance.ClearHighlights();

                //  Get possible moves for the selected piece
                var moves = selectedPiece.GetPossibleMoves();


                //  Highlight the possible moves (color handled in MoveCalculator)
                HighlightMoves(moves, selectedPiece.isWhite);
            }
            else
            {
                Debug.Log("No piece found on clicked tile.");
            }
        }

        // Highlights moves with color based on MoveCalculator logic
        private void HighlightMoves(List<Vector2Int> moves, bool isWhite)
        {
            foreach (var move in moves)
            {
                //  Determine highlight color based on whether the tile has an enemy piece
                var tile = ChessBoardPlacementHandler.Instance.GetTile(move.x, move.y);
                if (tile != null && tile.TryGetComponent<ChessPiece>(out _))
                {
                    //  Red for enemy pieces
                    ChessBoardPlacementHandler.Instance.Highlight(move.x, move.y, Color.red);
                }
                else
                {
                    //  Green for empty tiles
                    ChessBoardPlacementHandler.Instance.Highlight(move.x, move.y, Color.green);
                }
            }
        }
    }
}