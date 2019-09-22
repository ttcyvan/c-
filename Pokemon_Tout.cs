using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public abstract class Pokemon : IComparable
    {
        string nom;

        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }
        float poids;

        public float Poids
        {
            get { return poids; }
            set { poids = value; }
        }
        public Pokemon(string nom, float poids)
        {
            this.nom = nom;
            this.poids = poids;
        }
        public abstract float vitesse();
        public override string ToString()
        {
            return "Pokemon : nom\t: " + nom + "poids\t:" + poids;
        }

        public int CompareTo(object O)
        {
            Pokemon p = (Pokemon)O;
            return this.vitesse().CompareTo(p.vitesse());
        }

        public abstract class PokemonPattes : Pokemon
        {
            int nbPattes;

            public int NbPattes
            {
                get { return nbPattes; }
                set { nbPattes = value; }
            }
            float taille;

            public float Taille
            {
                get { return taille; }
                set { taille = value; }
            }
            public PokemonPattes(string nom, float poids, float taille, int nbPattes)
                : base(nom, poids)
            { this.nbPattes = nbPattes; this.taille = taille; }

            public override float vitesse()
            { return nbPattes * Taille * 3; }

            public override string ToString()
            {
                return "PokemonPattes : nom\t: " + Nom + "\tpoids\t:" + Poids + "\tnombre pattes\t:" + NbPattes + "\ntaille\t:" + taille + "vitesse\t" + vitesse() + '\n';
            }



        }

        public class PokemonSportif : PokemonPattes
        {
            int freq;
            public PokemonSportif(string nom, float poids, float taille, int nbPattes, int freq)
                : base(nom, poids, taille, nbPattes)
            { this.freq = freq; }
            public override string ToString()
            {
                return "PokemonSportif : nom\t: " + Nom + "\tpoids\t:" + Poids + "\tnombre pattes\t:" + NbPattes + "\nfreq\t:" + freq + "\tvitesse\t" + vitesse() + "\n\n";
            }
        }

        public class PokemonCasanier : PokemonPattes
        {
            int tele;
            public PokemonCasanier(string nom, float poids, float taille, int nbPattes, int tele)
                : base(nom, poids, taille, nbPattes)
            { this.tele = tele; }

            public override string ToString()
            {
                return "PokemonCasanier : nom\t: " + Nom + "\tpoids\t:" + Poids + "\tnombre pattes\t:" + NbPattes + "\ntele\t:" + tele + "\tvitesse\t" + vitesse() + "\n\n";
            }
        }

        public class PokemonMer : Pokemon
        {
            int nbNag;
           public PokemonMer(string nom, float poids, int nbNag)
                : base(nom, poids)
            { this.nbNag = nbNag; }


            public override string ToString()
            {
                return "PokemonMer : nom\t: " + Nom + "\tpoids\t:" + Poids + "\tnombre nag\t:" + nbNag + "\nvitesse\t" + vitesse() + "\n\n";
            }

            public override float vitesse()
            {
                return Poids / 25 * nbNag;
            }
        }

        public class PokemonCroi : PokemonMer
        {
            public PokemonCroi(string nom, float poids, int nbNag) : base(nom, poids, nbNag) { }
            public override float vitesse()
            {

                return base.vitesse() / 2;
            }
        }

        public class TabPokemon : ArrayList
        {

            public Pokemon plusRapide()
            {
                int ret = 0;
                float vitesse;
                vitesse = ((Pokemon)this[0]).vitesse();
                for (int i = 1; i < this.Count; i++)
                    if (((Pokemon)this[i]).vitesse() > vitesse)
                        ret = i;
                return (Pokemon)this[ret];
            }

            public override string ToString()
            {
                string s = "";
                for (int i = 1; i < this.Count; i++)
                    s += this[i].ToString();
                return s;
            }
        }

        public class TriPoids : IComparer
        {
            public int Compare(Object o1, Object o2)
            {
                Pokemon c1 = (Pokemon)o1;
                Pokemon c2 = (Pokemon)o2;
                return c1.Poids.CompareTo(c2.Poids);
            }
        }








        public class Program
        {


            public static void Main()
            {
                TabPokemon tab = new TabPokemon();
                PokemonSportif s = new PokemonSportif("Pikachu", 18, 0.85f, 2, 120);
                PokemonCasanier c = new PokemonCasanier("Salameche", 12, 0.65f, 2, 8);
                PokemonMer m = new PokemonMer("Rondoudou", 45, 2);
                PokemonCroi cr = new PokemonCroi("Bulbizare", 15, 3);
                tab.Add(s);
                tab.Add(c);
                tab.Add(m);
                tab.Add(cr);
                Console.WriteLine(tab);
                Console.WriteLine("POKEMON LE PLUS RAPIDE");
                Console.WriteLine(tab.plusRapide());

                Console.WriteLine("TRI vitesse\n\n ");
                tab.Sort();
                foreach (Pokemon p in tab)
                    Console.WriteLine(p);

                Console.WriteLine("TRI SUR POIDS\n\n ");
                tab.Sort(new TriPoids());
                foreach (Pokemon p in tab)
                    Console.WriteLine(p);
            }
        }


    }
}
   
