﻿using EQTool.Models;
using EQTool.Services;
using EQTool.Services.Fight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace EQTool.ViewModels
{
    public class DPSWindowViewModel : INotifyPropertyChanged
    {
        private readonly IAppDispatcher appDispatcher;
        private readonly FightLogService fightLogService;
        public DPSWindowViewModel(IAppDispatcher appDispatcher, FightLogService fightLogService, ActivePlayer activePlayer, SessionPlayerDamage sessionPlayerDamage)
        {
            this.appDispatcher = appDispatcher;
            this.fightLogService = fightLogService;
            Title = "Dps Meter v" + App.Version;
            this.ActivePlayer = activePlayer;
            this.SessionPlayerDamage = sessionPlayerDamage;
        }

        public ObservableCollection<EntittyDPS> _EntityList = new ObservableCollection<EntittyDPS>();
        public ObservableCollection<EntittyDPS> EntityList
        {
            get => _EntityList;
            set
            {
                _EntityList = value;
                OnPropertyChanged();
            }
        }

        public void UpdateStuff()
        {
            OnPropertyChanged(nameof(EntityList));
        }

        private SessionPlayerDamage _LastPlayerDamage = null;
        public SessionPlayerDamage LastPlayerDamage
        {
            get => _LastPlayerDamage;
            set
            {
                _LastPlayerDamage = value;
                OnPropertyChanged();
            }
        }

        private SessionPlayerDamage _SessionPlayerDamage = null;
        public SessionPlayerDamage SessionPlayerDamage
        {
            get => _SessionPlayerDamage;
            set
            {
                _SessionPlayerDamage = value;
                OnPropertyChanged();
            }
        }

        private ActivePlayer _activePlayer = null;
        public ActivePlayer ActivePlayer
        {
            get => _activePlayer;
            set
            {
                _activePlayer = value;
                OnPropertyChanged();
            }
        }

        private string _Title = null;
        public string Title
        {
            get => _Title;
            set
            {
                _Title = value;
                OnPropertyChanged();
            }
        }

        public static bool ShouldRemove(DateTime now, DateTime? lastdmgdone, DateTime startime, int groupcount)
        {
            var lasttime = lastdmgdone.HasValue && lastdmgdone.Value > startime ? lastdmgdone.Value : startime;
            var timeup = Math.Abs((now - lasttime).TotalSeconds);
            var secondstosubtract = groupcount * 20;
            secondstosubtract = Math.Min(60, secondstosubtract);
            var secthreshhold = 80 - secondstosubtract;
            return timeup > 40;
        }

        public void UpdateDPS()
        {
            appDispatcher.DispatchUI(() =>
            {
                var itemstormove = new List<EntittyDPS>();
                var now = DateTime.Now;
                var groups = _EntityList.GroupBy(a => a.TargetName).ToList();
                foreach (var item in _EntityList)
                {
                    if (ShouldRemove(now, item.LastDamageDone, item.StartTime, groups.Count))
                    {
                        itemstormove.Add(item);
                    }
                    else
                    {
                        item.UpdateDps();
                    }
                }

                foreach (var item in groups)
                {
                    var totaldmg = item.Sum(a => a.TotalDamage);
                    foreach (var e in item)
                    {
                        e.TargetTotalDamage = totaldmg;
                    }
                }
                fightLogService.Log(itemstormove);
                foreach (var item in itemstormove)
                {
                    _ = EntityList.Remove(item);
                }

                var you = _EntityList.FirstOrDefault(a => a.SourceName == "You" && a.TotalSeconds > 20);
                if (you != null)
                {
                    if (this.ActivePlayer.Player != null)
                    {
                        this.ActivePlayer.Player.BestPlayerDamage.HighestDPS = Math.Max(this.ActivePlayer.Player.BestPlayerDamage.HighestDPS, you.DPS);
                        this.ActivePlayer.Player.BestPlayerDamage.TargetTotalDamage = Math.Max(this.ActivePlayer.Player.BestPlayerDamage.TargetTotalDamage, you.TotalDamage);
                        this.ActivePlayer.Player.BestPlayerDamage.HighestHit = Math.Max(this.ActivePlayer.Player.BestPlayerDamage.HighestHit, you.HighestHit);
                    }
                    //this.OnPropertyChanged(nameof(ActivePlayer));
                    this.SessionPlayerDamage.CurrentSessionPlayerDamage.HighestDPS = Math.Max(this.SessionPlayerDamage.CurrentSessionPlayerDamage.HighestDPS, you.DPS);
                    this.SessionPlayerDamage.CurrentSessionPlayerDamage.TargetTotalDamage = Math.Max(this.SessionPlayerDamage.CurrentSessionPlayerDamage.TargetTotalDamage, you.TotalDamage);
                    this.SessionPlayerDamage.CurrentSessionPlayerDamage.HighestHit = Math.Max(this.SessionPlayerDamage.CurrentSessionPlayerDamage.HighestHit, you.HighestHit);
                    //this.OnPropertyChanged(nameof(SessionPlayerDamage));
                }
            });
        }

        public void TargetDied(string target)
        {
            if (string.IsNullOrWhiteSpace(target))
            {
                return;
            }

            appDispatcher.DispatchUI(() =>
            {
                var t = target.ToLower();
                var itemstoremove = EntityList.Where(a => a.TargetName.ToLower() == t).ToList();
                foreach (var item in itemstoremove)
                {
                    item.DeathTime = DateTime.Now;
                }
            });
        }

        public void TryAdd(DamageEvent entity)
        {
            //when charmed pet and nps have the same name, everything is messed up
            if (entity == null || entity.AttackerName == entity.TargetName)
            {
                return;
            }

            appDispatcher.DispatchUI(() =>
            {
                var item = EntityList.FirstOrDefault(a => a.SourceName == entity.AttackerName && a.TargetName == entity.TargetName && !a.DeathTime.HasValue);
                if (item == null)
                {
                    item = new EntittyDPS
                    {
                        SourceName = entity.AttackerName,
                        TargetName = entity.TargetName,
                        StartTime = entity.TimeStamp,
                        TotalDamage = entity.DamageDone,
                        TotalTwelveSecondDamage = entity.DamageDone,
                        TrailingDamage = entity.DamageDone,
                        HighestHit = entity.DamageDone
                    };
                    EntityList.Add(item);
                }
                else
                {
                    //Debug.WriteLine($"{entity.TargetName} {entity.DamageDone}");
                    item.AddDamage(new EntittyDPS.DamagePerTime
                    {
                        TimeStamp = entity.TimeStamp,
                        Damage = entity.DamageDone
                    });
                }
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
