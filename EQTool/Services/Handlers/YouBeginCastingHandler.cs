﻿using EQTool.Models;
using EQTool.ViewModels;

namespace EQTool.Services.Handlers
{
    public class YouBeginCastingHandler : BaseHandler
    {
        private readonly IAppDispatcher appDispatcher;

        public YouBeginCastingHandler(IAppDispatcher appDispatcher, LogEvents logEvents, ActivePlayer activePlayer, EQToolSettings eQToolSettings, ITextToSpeach textToSpeach) : base(logEvents, activePlayer, eQToolSettings, textToSpeach)
        {
            this.appDispatcher = appDispatcher;
            this.logEvents.YouBeginCastingEvent += LogEvents_YouBeginCastingEvent;
        }

        private void LogEvents_YouBeginCastingEvent(object sender, YouBeginCastingEvent e)
        {
            appDispatcher.DispatchUI(() =>
            {
                activePlayer.UserCastingSpell = e.Spell;
                activePlayer.UserCastSpellDateTime = e.TimeStamp;
            });
        }
    }
}
