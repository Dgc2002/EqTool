﻿using EQToolShared;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EQTool.UI
{
    public partial class PanAndZoomCanvas : Canvas
    {
        public event EventHandler<EventArgs> CancelTimerEvent;

        //public event EventHandler<StartTimerEvent> StartTimerEvent;
        public string ZoneName = "freportw";

        public PanAndZoomCanvas()
        {
            InitializeComponent();
            MouseDown += PanAndZoomCanvas_MouseDown;
            TimeSpanControl.DefaultValue = TimeSpan.FromMinutes(72);
            TimeSpanControl.Value = TimeSpan.FromMinutes(72);
            TimeSpanControl.DisplayDefaultValueOnEmptyText = true;
        }

        private int TimerCounter = 1;
        private void AddTimer(object sender, RoutedEventArgs e)
        {
            if (TimeSpanControl.Value.HasValue)
            {
                _ = $"Timer {TimerCounter++}";
                //StartTimerEvent?.Invoke(this,
                //    new StartTimerEvent
                //    {
                //        CustomTimer = new CustomTimer
                //        {
                //            Name = timername,
                //            DurationSeconds = (int)TimeSpanControl.Value.Value.TotalSeconds
                //        },
                //        TimeStamp = DateTime.Now
                //    });
                TimerMenu.IsOpen = false;
            }
        }

        private void DeleteTimer(object sender, RoutedEventArgs e)
        {
            CancelTimerEvent?.Invoke(this, new EventArgs());
        }

        private void PanAndZoomCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Right)
            {
                if (e.Source is MapWidget)
                {
                    AddTimerMenuItem.Visibility = Visibility.Collapsed;
                    DeleteTimerMenuItem.Visibility = Visibility.Visible;
                }
                else
                {
                    AddTimerMenuItem.Visibility = Visibility.Visible;
                    DeleteTimerMenuItem.Visibility = Visibility.Collapsed;
                }
            }
        }

        public event EventHandler<RoutedEventArgs> TimerMenu_ClosedEvent;
        private void TimerMenu_Closed(object sender, RoutedEventArgs e)
        {
            TimerMenu_ClosedEvent?.Invoke(this, e);
        }

        public event EventHandler<RoutedEventArgs> TimerMenu_OpenedEvent;
        private void TimerMenu_Opened(object sender, RoutedEventArgs e)
        {
            TimerMenu_OpenedEvent?.Invoke(this, e);
            TimeSpanControl.Value = ZoneSpawnTimes.GetSpawnTime(string.Empty, ZoneName);
        }
    }
}
