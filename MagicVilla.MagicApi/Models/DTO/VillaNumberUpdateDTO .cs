﻿using System.ComponentModel.DataAnnotations;

namespace MagicVilla.MagicApi.Model.DTO
{
    public class VillaNumberUpdateDTO
    {

        [Required]

        public int VillaNo { get; set; }
        public int VillaId { get; set; }
        public string SpecialDetails { get; set; }
    }
}



