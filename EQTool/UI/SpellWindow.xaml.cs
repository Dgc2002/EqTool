﻿using EQTool.Models;
using EQTool.Services;
using EQTool.ViewModels;
using EQTool.ViewModels.SpellWindow;
using EQToolShared.Enums;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EQTool.UI
{
    public partial class SpellWindow : BaseSaveStateWindow
    {
        private readonly SpellWindowViewModel spellWindowViewModel;
        private readonly SettingsWindowViewModel settingsWindowViewModel;

        public SpellWindow(
            TimersService timersService,
            EQToolSettings settings,
            SpellWindowViewModel spellWindowViewModel,
            SettingsWindowViewModel settingsWindowViewModel,
            LogEvents logEvents,
            EQToolSettingsLoad toolSettingsLoad,
            ActivePlayer activePlayer,
            IAppDispatcher appDispatcher,
            LoggingService loggingService) : base(settings.SpellWindowState, toolSettingsLoad, settings)
        {
            this.settingsWindowViewModel = settingsWindowViewModel;
            loggingService.Log(string.Empty, EventType.OpenMap, activePlayer?.Player?.Server);
            spellWindowViewModel.WindowFrameBrush = spellWindowViewModel.NonRaidModeLinearGradientBrush = new LinearGradientBrush
            {
                StartPoint = new System.Windows.Point(0, 0.5),
                EndPoint = new System.Windows.Point(1, 0.5),
                GradientStops = new GradientStopCollection()
                    {
                            new GradientStop(System.Windows.Media.Colors.CadetBlue, .4),
                            new GradientStop(System.Windows.Media.Colors.Gray, 1)
                    }
            };
            spellWindowViewModel.RaidModeLinearGradientBrush = new LinearGradientBrush
            {
                StartPoint = new System.Windows.Point(0, 0.5),
                EndPoint = new System.Windows.Point(1, 0.5),
                GradientStops = new GradientStopCollection()
                    {
                            new GradientStop(System.Windows.Media.Colors.OrangeRed, .4),
                            new GradientStop(System.Windows.Media.Colors.Gray, 1)
                    }
            };
            DataContext = this.spellWindowViewModel = spellWindowViewModel;
            InitializeComponent();
            base.Init();
        }

        private void RemoveSingleItem(object sender, RoutedEventArgs e)
        {
            var name = (sender as Button).DataContext;
            _ = spellWindowViewModel.SpellList.Remove(name as PersistentViewModel);
        }

        private void RemoveFromSpells(object sender, RoutedEventArgs e)
        {
            var name = ((sender as Button).DataContext as dynamic)?.Name as string;
            var items = spellWindowViewModel.SpellList.Where(a => a.GroupName == name).ToList();
            foreach (var item in items)
            {
                _ = spellWindowViewModel.SpellList.Remove(item);
            }
        }

        private void ClearAllOtherSpells(object sender, RoutedEventArgs e)
        {
            spellWindowViewModel.ClearAllOtherSpells();
        }

        private void RaidModleToggle(object sender, RoutedEventArgs e)
        {
            settingsWindowViewModel.RaidModeDetection = !settingsWindowViewModel.RaidModeDetection;
        }
    }
}
