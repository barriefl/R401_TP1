using System.ComponentModel.DataAnnotations;

namespace WSConvertisseur.Models
{
    public class Devise
    {
        private int id;
        private string? nomDevise;
        private double taux;

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        [Required]
        public string? NomDevise
        {
            get
            {
                return nomDevise;
            }

            set
            {
                nomDevise = value;
            }
        }

        public double Taux
        {
            get
            {
                return this.taux;
            }

            set
            {
                this.taux = value;
            }
        }

        public Devise()
        {
        }

        /// <summary>
        /// Create a single currency.
        /// </summary>
        /// <returns>Devise object</returns>
        /// <param name="id">The id of the currency</param>
        /// <param name="nomDevise">The name of the currency</param>
        /// <param name="taux">The rate of the currency</param>
        public Devise(int id, string? nomDevise, double taux)
        {
            this.Id = id;
            this.NomDevise = nomDevise;
            this.Taux = taux;
        }
    }
}
