﻿using EQTool.Models;
using EQTool.ViewModels;
using System.Diagnostics;

namespace EQTool.Services.Handlers
{
    //
    // class to create spawn timers
    //
    // watches for ExpGainedEvent, FactionEvent, and DeathEvent types
    //
    internal class SpawnTimerHandler : BaseHandler
    {
        //
        // ctor
        //
        // register this service as a listener for the Events it cares about
        //
        public SpawnTimerHandler(LogEvents logEvents, ActivePlayer activePlayer, EQToolSettings eQToolSettings, ITextToSpeach textToSpeach) : base(logEvents, activePlayer, eQToolSettings, textToSpeach)
        {
            this.logEvents.ExpGainedEvent += LogEvents_ExpGainedEvent;
            this.logEvents.SlainEvent += LogEvents_SlainEvent;
            this.logEvents.FactionEvent += LogEvents_FactionEvent;
        }


        //
        // function that gets called for a ExpGainedEvent
        //
        private void LogEvents_ExpGainedEvent(object sender, ExpGainedEvent expGainedEvent)
        {
            // debugging message
            Debug.WriteLine($"ExpGainedEvent: [{expGainedEvent.TimeStamp}] [{expGainedEvent.Line}]");

            // react


        }

        //
        // function that gets called for a SlainhEvent
        //
        private void LogEvents_SlainEvent(object sender, SlainEvent slainEvent)
        {
            // debugging message
            Debug.WriteLine($"SlainEvent: [{slainEvent.TimeStamp}], Killer = [{slainEvent.Killer}], Victim = [{slainEvent.Victim}]");

            // if the victim field matches to the SpawnTimer field, then react


        }

        //
        // function that gets called for a FactionEvent
        //
        private void LogEvents_FactionEvent(object sender, FactionEvent factionEvent)
        {
            // debugging message
            Debug.WriteLine($"FactionEvent: [{factionEvent.TimeStamp}], Faction group = [{factionEvent.Faction}]");

            // if the faction field matches to the SpawnTimer field, then react


        }

    }
}
