using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventaireWinForms
{
    class CommandeInterneProduitDTO
    {
        public long idCommandeInterne { get; set; }
        public long idArticle { get; set; }
        public int Quantity { get; set; }


    }

    class Datalist_Commande_Interne_Produit
    {
        public List<CommandeInterneProduitDTO> ListcommandeInterneProduit { get; set; }
    }
}
