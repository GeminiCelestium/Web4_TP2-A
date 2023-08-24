using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web2.API.Models
{
    public class Ville
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public Region Region { get; set; }
    }

    public enum Region
    { 
        ESTRIE,
        MAURICIE,
        OUTAOUAI,
        ABITIBI,
        COTE_NORD
    }
}
