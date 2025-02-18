﻿using EQTool.Models;
using EQTool.ViewModels.MobInfoComponents;
using System;
using System.Linq;
using System.Windows;

namespace EQTool.Services.Handlers
{
    public class ConHandler : BaseHandler
    {
        private readonly MobInfoViewModel mobInfoViewModel;
        private readonly WikiApi wikiApi;
        private readonly PigParseApi pigParseApi;
        private readonly MobInfoManagementViewModel mobInfoManagementViewModel;
        private readonly PetViewModel playerPet;

        public ConHandler(PetViewModel playerPet, MobInfoManagementViewModel mobInfoManagementViewModel, MobInfoViewModel mobInfoViewModel, BaseHandlerData baseHandlerData, WikiApi wikiApi, PigParseApi pigParseApi) : base(baseHandlerData)
        {
            this.mobInfoManagementViewModel = mobInfoManagementViewModel;
            this.playerPet = playerPet;
            this.mobInfoViewModel = mobInfoViewModel;
            this.wikiApi = wikiApi;
            this.pigParseApi = pigParseApi;
            logEvents.ConEvent += LogEvents_ConEvent;
        }

        private void LogEvents_ConEvent(object sender, ConEvent e)
        {
            if (e.Name == playerPet.PetName)
            {
                mobInfoManagementViewModel.MobInfoItemType = ViewModels.SettingsComponents.MobInfoItemType.Pet;
            }
            else
            {
                mobInfoManagementViewModel.MobInfoItemType = ViewModels.SettingsComponents.MobInfoItemType.Mob;
                try
                {
                    if (e.Name != mobInfoViewModel.Name)
                    {
                        mobInfoViewModel.Results = wikiApi.GetData(e.Name);
                        var items = mobInfoViewModel.KnownLoot.Where(a => a.HasUrl == Visibility.Visible).Select(a => a.Name?.Trim()).Where(a => !string.IsNullOrWhiteSpace(a)).ToList();
                        if (activePlayer?.Player?.Server != null && items.Any())
                        {
                            var itemprices = pigParseApi.GetData(items, activePlayer.Player.Server.Value);
                            foreach (var item in itemprices)
                            {
                                var loot = mobInfoViewModel.KnownLoot.FirstOrDefault(a => a.Name.Equals(item.ItemName, StringComparison.OrdinalIgnoreCase));
                                if (loot != null)
                                {
                                    loot.Price = item.TotalWTSLast6MonthsAverage.ToString();
                                    loot.PriceUrl = $"https://pigparse.azurewebsites.net/ItemDetails/{item.EQitemId}";
                                }
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    mobInfoViewModel.ErrorResults = ex.Message;
                    if (!mobInfoViewModel.ErrorResults.Contains("The underlying connection was closed:"))
                    {
                        mobInfoViewModel.ErrorResults = "The server is down. Try again";
                        App.LogUnhandledException(ex, $"LogParser_ConEvent {e.Name}", activePlayer?.Player?.Server);
                    }
                }
            }
        }
    }
}
