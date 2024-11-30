﻿using EQTool.Models;
using EQTool.ViewModels;
using EQToolShared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EQTool.Services.Parsing
{
    public class SpellCastOnOtherParser : IEqLogParser
    {
        private readonly LogEvents logEvents;
        private readonly EQSpells spells;
        private readonly SpellDurations spellDurations;
        private readonly DebugOutput debugOutput;
        private readonly ActivePlayer activePlayer;
        private readonly List<string> IgnoreSpellsForGuesses = new List<string>(){
            "Tigir's Insects"
        };

        public SpellCastOnOtherParser(SpellDurations spellDurations, LogEvents logEvents, EQSpells spells, DebugOutput debugOutput, ActivePlayer activePlayer)
        {
            this.debugOutput = debugOutput;
            this.spellDurations = spellDurations;
            this.logEvents = logEvents;
            this.spells = spells;
            this.activePlayer = activePlayer;
        }

        public bool Handle(string line, DateTime timestamp, int lineCounter)
        {
            return HandleBestGuessSpell(line, timestamp, lineCounter);
        }
        private bool HandleBestGuessSpell(string message, DateTime timestamp, int lineCounter)
        {
            var removename = message.IndexOf("'");
            if (removename != -1)
            {
                var spellmessage = message.Substring(removename).Trim();
                if (spells.CastOtherSpells.TryGetValue(spellmessage, out var foundspells))
                {
                    var foundspell = spellDurations.MatchDragonRoar(foundspells, timestamp);
                    if (foundspell != null)
                    {
                        debugOutput.WriteLine($"{foundspell.name} Message: {message}", OutputType.Spells);
                        logEvents.Handle(new DragonRoarEvent
                        {
                            Spell = foundspell,
                            TimeStamp = timestamp,
                            Line = message,
                            LineCounter = lineCounter
                        });
                        return true;
                    }

                    var targetname = message.Replace(foundspells.FirstOrDefault().cast_on_other, string.Empty).Trim();
                    var spellsfound = string.Join(",", foundspells.Select(a => a.name));
                    debugOutput.WriteLine($"{spellsfound} Message: {message}", OutputType.Spells);
                    logEvents.Handle(new SpellCastOnOtherEvent
                    {
                        Spells = foundspells,
                        TargetName = targetname,
                        TimeStamp = timestamp,
                        Line = message,
                        LineCounter = lineCounter
                    });
                    return true;
                }
            }
            else
            {
                var userCastingSpell = activePlayer.UserCastingSpell;
                var userCastSpellDateTime = activePlayer.UserCastSpellDateTime;
                if (userCastingSpell != null && userCastSpellDateTime != null)
                {
                    var dt = timestamp - userCastSpellDateTime.Value;
                    if (dt.TotalMilliseconds >= (userCastingSpell.casttime - 300) && message == userCastingSpell.cast_on_you)
                    {
                        debugOutput.WriteLine($"Casting on yourself Detected for {userCastingSpell.name}", OutputType.Spells);
                        logEvents.Handle(new YouFinishCastingEvent
                        {
                            Spell = userCastingSpell,
                            TargetName = EQSpells.SpaceYou,
                            TimeStamp = timestamp,
                            Line = message,
                            LineCounter = lineCounter
                        });
                        return true;
                    }
                }

                removename = 0;
                const int maxspaces = 5;
                for (var i = 0; i < maxspaces; i++)
                {
                    if (removename > message.Length)
                    {
                        break;
                    }
                    removename = message.IndexOf(" ", removename + 1);
                    if (removename != -1)
                    {
                        var firstpart = message.Substring(0, removename + 1).Trim();
                        var spellmessage = message.Substring(removename).Trim();
                        var matchedspell = Match(spellmessage, firstpart, timestamp, message, lineCounter);
                        if (matchedspell)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private bool Match(string spellmessage, string targetname, DateTime timestamp, string message, int lineCounter)
        {
            if (spells.CastOtherSpells.TryGetValue(spellmessage, out var foundspells))
            {
                foundspells = foundspells.Where(a => !IgnoreSpellsForGuesses.Contains(a.name)).ToList();
                foundspells = foundspells.Where(a =>
                     a.SpellType != SpellType.PointBlankAreaofEffect &&
                     a.SpellType != SpellType.TargetedAreaofEffect &&
                     a.SpellType != SpellType.TargetedAreaofEffectLifeTap &&
                     a.SpellType != SpellType.AreaofEffectUndead &&
                     a.SpellType != SpellType.AreaofEffectSummoned &&
                     a.SpellType != SpellType.AreaofEffectCaster &&
                     a.SpellType != SpellType.AreaPCOnly &&
                     a.SpellType != SpellType.AreaNPCOnly &&
                     a.SpellType != SpellType.AreaofEffectPCV2
                ).ToList();
                var spellsfound = string.Join(",", foundspells.Select(a => a.name));
                if (MasterNPCList.NPCs.Contains(targetname))
                {
                    targetname = " " + targetname;
                }
                debugOutput.WriteLine($"{spellsfound} Message: {spellmessage}", OutputType.Spells);
                logEvents.Handle(new SpellCastOnOtherEvent
                {
                    Spells = foundspells,
                    TargetName = targetname,
                    TimeStamp = timestamp,
                    Line = message,
                    LineCounter = lineCounter
                });
                return true;
            }

            return false;
        }
    }
}