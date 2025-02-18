﻿using EQToolShared.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace EQToolShared.APIModels.BoatControllerModels
{
    public class BoatActivityRequest
    {
        [Required]
        public string Zone { get; set; } 
        [EnumDataType(typeof(Servers))]
        public Servers Server { get; set; }
    } 
    public class BoatActivityResponce
    {
        [Required]
        public string Zone { get; set; }
        [Required]
        public DateTimeOffset LastSeen { get; set; }
    }
}
