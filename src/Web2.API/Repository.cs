using System.Collections.Generic;
using Web2.API.Data.Models;

namespace Web2.API
{
    public static class Repository
    {
        public static int IdSequenceVille { get; set; } = 1;
        public static int IdSequenceEvenement { get; set; } = 1;
        public static int IdSequenceParticipation { get; set; } = 1;
        public static int IdSequenceCategory { get; set; } = 1;
        public static ISet<Ville> Villes { get; set; } = new HashSet<Ville>();
        public static ISet<Evenement> Evenements = new HashSet<Evenement>();
        public static ISet<Participation> Participations = new HashSet<Participation>();
        public static ISet<Categorie> Categories = new HashSet<Categorie>();

    }
}
