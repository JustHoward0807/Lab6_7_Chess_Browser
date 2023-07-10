using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.ApplicationModel.DataTransfer;
using Microsoft.Maui.Controls;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ChessBrowser
{
    internal class Queries
    {

        /// <summary>
        /// This function runs when the upload button is pressed.
        /// Given a filename, parses the PGN file, and uploads
        /// each chess game to the user's database.
        /// </summary>
        /// <param name="PGNfilename">The path to the PGN file</param>
        internal static async Task InsertGameData(string PGNfilename, MainPage mainPage)
        {
            // This will build a connection string to your user's database on atr,
            // assuimg you've typed a user and password in the GUI

            string connection = mainPage.GetConnectionString();
            Console.WriteLine("Finished Connection: " + connection);
            Console.WriteLine("Read File");
            List<ChessGame> chessGameList = PgnReader.readFile(PGNfilename);

            // TODO:
            //       Load and parse the PGN file
            //       We recommend creating separate libraries to represent chess data and load the file

            // TODO:
            //       Use this to tell the GUI's progress bar how many total work steps there are
            //       For example, one iteration of your main upload loop could be one work step
            mainPage.SetNumWorkItems(chessGameList.Count);


            using (MySqlConnection conn = new MySqlConnection(connection))
            {
                try
                {
                    // Open a connection
                    conn.Open();
                    Console.WriteLine("Inserting: ");


                    foreach (ChessGame chessGame in chessGameList)
                    {

                        //Prevent Injection attack
                        using (MySqlCommand command = conn.CreateCommand())
                        {
                            // Insert into Players

                            command.CommandText = "INSERT INTO Players(Name, Elo) VALUES(@white, @whiteElo) ON DUPLICATE KEY UPDATE Elo = GREATEST(VALUES(Elo), @whiteElo);";
                            command.Parameters.AddWithValue("@white", chessGame.white);
                            command.Parameters.AddWithValue("@whiteElo", chessGame.whiteElo);
                            command.ExecuteNonQuery();


                            command.CommandText = "INSERT INTO Players(Name, Elo) VALUES(@black, @blackElo) ON DUPLICATE KEY UPDATE Elo = GREATEST(VALUES(Elo), @blackElo);";

                            command.Parameters.AddWithValue("@black", chessGame.black);
                            command.Parameters.AddWithValue("@blackElo", chessGame.blackElo);
                            command.ExecuteNonQuery();

                            // Insert into Events

                            command.CommandText = $"INSERT IGNORE INTO Events (Name, Site, Date) VALUES (@eventName, @site, @eventDate)";
                            command.Parameters.AddWithValue("@eventName", chessGame.eventName);
                            command.Parameters.AddWithValue("@site", chessGame.site);
                            command.Parameters.AddWithValue("@eventDate", chessGame.eventDate);
                            command.ExecuteNonQuery();


                            // Insert into Games
                            command.CommandText = "INSERT IGNORE INTO Games VALUES(@round, @result, @moves, (SELECT pID FROM Players WHERE Name = @black), (SELECT pID FROM Players WHERE Name = @white), (SELECT eID FROM Events WHERE Name = @eventName and Site = @site and Date = @eventDate))";


                            command.Parameters.AddWithValue("@round", chessGame.round);
                            command.Parameters.AddWithValue("@result", chessGame.result);
                            command.Parameters.AddWithValue("@moves", chessGame.moves);
                            command.ExecuteNonQuery();
                        }

                        await mainPage.NotifyWorkItemCompleted();
                    }


                    Console.WriteLine("Finished reading");



                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }
            }

        }


        /// <summary>
        /// Queries the database for games that match all the given filters.
        /// The filters are taken from the various controls in the GUI.
        /// </summary>
        /// <param name="white">The white player, or null if none</param>
        /// <param name="black">The black player, or null if none</param>
        /// <param name="opening">The first move, e.g. "1.e4", or null if none</param>
        /// <param name="winner">The winner as "W", "B", "D", or null if none</param>
        /// <param name="useDate">True if the filter includes a date range, False otherwise</param>
        /// <param name="start">The start of the date range</param>
        /// <param name="end">The end of the date range</param>
        /// <param name="showMoves">True if the returned data should include the PGN moves</param>
        /// <returns>A string separated by newlines containing the filtered games</returns>
        internal static string PerformQuery(string white, string black, string opening,
          string winner, bool useDate, DateTime start, DateTime end, bool showMoves,
          MainPage mainPage)
        {
            // This will build a connection string to your user's database on atr,
            // assuimg you've typed a user and password in the GUI
            string connection = mainPage.GetConnectionString();

            // Build up this string containing the results from your query
            string parsedResult = "";

            // Use this to count the number of rows returned by your query
            // (see below return statement)
            int numRows = 0;

            using (MySqlConnection conn = new MySqlConnection(connection))
            {
                try
                {
                    Console.WriteLine("Reading...");
                    // Open a connection
                    conn.Open();
                    MySqlCommand command = conn.CreateCommand();

                    //Commands that will give me 8 result just like assignments
                    //SELECT* FROM(SELECT e.Date, e.Site, e.Name, g.Result, p.Name AS WhitePlayer, p.Elo, p2.Name as BlackPlayer FROM Games g     JOIN Players p ON g.WhitePlayer = p.pID     JOIN Events e ON g.eID = e.eID     JOIN Players p2 ON p2.pID = g.BlackPlayer) AS sub WHERE sub.WhitePlayer = 'Carlsen, Magnus' AND sub.Result = 'W';


                    //TODO: Prevent injection attack
                    //command.CommandText = $"select * from Games where WhitePlayer = 366953 and Result = 'W'";
                    //using (MySqlDataReader reader = command.ExecuteReader())
                    //{
                    //    while (reader.Read())
                    //    {
                    //        Console.WriteLine(reader.GetString("Round"));
                    //        //Console.WriteLine(reader.GetInt32("Elo"));
                    //        //Console.WriteLine(reader.GetInt32("pID"));
                    //    }
                    //}
                    // TODO:
                    //       Generate and execute an SQL command,
                    //       then parse the results into an appropriate string and return it.

                    Console.WriteLine("Finish");
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }
            }

            return numRows + " results\n" + parsedResult;
        }

    }
}
