using System;
using Microsoft.Maui.Controls.Shapes;

// We only need Event, Site, Round, White, Black, WhiteElo, BlackElo, Result, EventDate
namespace ChessBrowser
{
    static class PgnReader
    {

        public static List<ChessGame> readFile(string filePath)
        {
            List<ChessGame> chessGames = new();
            ChessGame chessGame = new();

            string[] lines = File.ReadAllLines(filePath);

            for (int i = 0; i < lines.Count(); i++)
            {
                // If the beginning is Event then just grab everything inside the [], same thing apply to everything else. And if its something that I don't want, I just continue and one thing left will go to else move.
                if (lines[i].StartsWith("[Event "))
                {
                    int index = lines[i].IndexOf("\"");
                    int length = lines[i].Length - index - 3;

                    chessGame.setEventName(lines[i].Substring(index + 1, length));
                }
                else if (lines[i].StartsWith("[Site "))
                {
                    int index = lines[i].IndexOf("\"");
                    int length = lines[i].Length - index - 3;

                    chessGame.setSite(lines[i].Substring(index + 1, length));
                }

                else if (lines[i].StartsWith("[Date "))
                {
                    continue;
                }

                else if (lines[i].StartsWith("[Round "))
                {
                    int index = lines[i].IndexOf("\"");
                    int length = lines[i].Length - index - 3;

                    chessGame.setRound(lines[i].Substring(index + 1, length));
                }

                else if (lines[i].StartsWith("[White "))
                {
                    int index = lines[i].IndexOf("\"");
                    int length = lines[i].Length - index - 3;

                    chessGame.setWhite(lines[i].Substring(index + 1, length));
                }
                else if (lines[i].StartsWith("[Black "))
                {
                    int index = lines[i].IndexOf("\"");
                    int length = lines[i].Length - index - 3;

                    chessGame.setBlack(lines[i].Substring(index + 1, length));
                }

                else if (lines[i].StartsWith("[Result "))
                {
                    int index = lines[i].IndexOf("\"");
                    int length = lines[i].Length - index - 3;

                    chessGame.setResult(lines[i].Substring(index + 1, length));
                }

                else if (lines[i].StartsWith("[WhiteElo "))
                {
                    int index = lines[i].IndexOf("\"");
                    int length = lines[i].Length - index - 3;

                    chessGame.setWhiteElo(lines[i].Substring(index + 1, length));
                }

                else if (lines[i].StartsWith("[BlackElo "))
                {
                    int index = lines[i].IndexOf("\"");
                    int length = lines[i].Length - index - 3;

                    chessGame.setBlackElo(lines[i].Substring(index + 1, length));

                }

                else if (lines[i].StartsWith("[ECO "))
                {
                    continue;
                }

                else if (lines[i].StartsWith("[EventDate "))
                {
                    int index = lines[i].IndexOf("\"");
                    int length = lines[i].Length - index - 3;

                    chessGame.setEventDate(lines[i].Substring(index + 1, length));

                }

                else
                {
                    // Making sure the index won't go out of boundary
                    // if the next one is not an new event then it must be move, so we add it into our tempMove variable until the next one is not a new chessGame.
                    // If the next one is the new ChessGame (Start with Event), then we stop and the set the move to our ChessGame class and then add to the list and start a new chessGame class.
                    string tempMove = "";
                    while (i + 1 < lines.Count() && !lines[i + 1].StartsWith("[Event "))
                    {
                        tempMove += lines[i];
                        i += 1;
                    }
                    chessGame.setMoves(tempMove);
                    chessGames.Add(chessGame);
                    chessGame = new ChessGame();
                }

            }
            return chessGames;
        }
    }
}

