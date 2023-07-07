using System;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.ComponentModel;
using System.Text;
using System.Security.AccessControl;


// We only need Event, Site, Round, White, Black, WhiteElo, BlackElo, Result, EventDate
namespace ChessBrowser
{
    class ChessGame
    {
        public string eventName, site, round, white, black, whiteElo, blackElo, result, eventDate, moves;

        //ChessGame(string eventName, string site, string round, string white, string black, string whiteElo, string blackElo, string result, string eventDate)
        //{
        //    this.eventName = eventName;
        //    this.site = site;
        //    this.round = round;
        //    this.white = white;
        //    this.black = black;
        //    this.whiteElo = whiteElo;
        //    this.blackElo = blackElo;
        //    this.result = result;
        //    this.eventDate = eventDate;
        //}

        public void setEventName(string eventName) { this.eventName = eventName; }
        public void setSite(string site) { this.site = site; }
        public void setRound(string round) { this.round = round; }
        public void setWhite(string white) { this.white = white; }
        public void setBlack(string black) { this.black = black; }
        public void setWhiteElo(string whiteElo) { this.whiteElo = whiteElo; }
        public void setBlackElo(string blackElo) { this.blackElo = blackElo; }
        public void setResult(string result) { this.result = result; }
        public void setEventDate(string eventDate) { this.eventDate = eventDate; }
        public void setMoves(string moves) { this.moves = moves; }
    }
}

