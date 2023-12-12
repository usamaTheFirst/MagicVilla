﻿using System.ComponentModel.DataAnnotations;

namespace MagicVilla.MagicApi.Model.DTO
{
    public class VillaNumberCreateDTO
    {

        [Required]

        public int VillaNo { get; set; }
        [Required]
        public int VillaId { get; set; }

        public string SpecialDetails { get; set; }
    }
}



