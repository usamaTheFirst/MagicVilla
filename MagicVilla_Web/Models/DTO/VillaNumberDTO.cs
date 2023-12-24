﻿using System.ComponentModel.DataAnnotations;

namespace MagicVilla_Web.Model.DTO
{
    public class VillaNumberDTO
    {

        [Required]

        public int VillaNo { get; set; }
        public int VillaId { get; set; }

        public string SpecialDetails { get; set; }
        public VillaDTO Villa { get; set; }
    }
}



