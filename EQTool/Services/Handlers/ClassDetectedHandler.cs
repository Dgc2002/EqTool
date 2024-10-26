﻿using EQTool.Models;
using EQTool.ViewModels;

namespace EQTool.Services.Handlers
{
    public class ClassDetectedHandler : BaseHandler
    {
        private readonly EQToolSettings settings;
        private readonly EQToolSettingsLoad toolSettingsLoad;
        private readonly IAppDispatcher appDispatcher;

        public ClassDetectedHandler(
            IAppDispatcher appDispatcher,
            LogEvents logEvents,
            ActivePlayer activePlayer,
            EQToolSettings eQToolSettings,
            ITextToSpeach textToSpeach,
            EQToolSettings settings,
            EQToolSettingsLoad toolSettingsLoad) : base(logEvents, activePlayer, eQToolSettings, textToSpeach)
        {
            this.appDispatcher = appDispatcher;
            this.settings = eQToolSettings;
            this.toolSettingsLoad = toolSettingsLoad;
            this.logEvents.ClassDetectedEvent += LogEvents_ClassDetectedEvent;
        }

        private void LogEvents_ClassDetectedEvent(object sender, ClassDetectedEvent e)
        {
            if (activePlayer?.Player != null)
            {
                appDispatcher.DispatchUI(() =>
                {
                    activePlayer.Player.PlayerClass = e.PlayerClass;
                });

                toolSettingsLoad.Save(settings);
            }
        }
    }
}