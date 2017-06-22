using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevisBack
{
    static class CBC
    {
        private static short TAILLE_BLOC = 4;     // en octets

        public static byte[] dechiffrer(byte[] uneCle, byte[] uneSource)
        {
            byte[][] aDechiffrer = new byte[(uneSource.Length - TAILLE_BLOC) / TAILLE_BLOC][];
            byte[][] dechiffre /* déchiffré */ = new byte[aDechiffrer.Length][];
            byte[] destination = new byte[uneSource.Length - TAILLE_BLOC];

            if (uneSource.Length % TAILLE_BLOC != 0)
            {
                throw new ArgumentException("Le tableau source n'a pas une taille valide. Spécifiez la taille des blocs et réessayez.");
            }

            byte[] iv = new byte[TAILLE_BLOC];

            // Initialisation du vecteur d'initialisation
            for (int i = 0; i < iv.Length; i++)
            {
                iv[i] = uneSource[destination.Length + i];
            }

            // Création du tableau de blocs à partir du fichier
            for (int j = 0; j < aDechiffrer.Length; j++)
            {
                aDechiffrer[j] = new byte[TAILLE_BLOC];
                for (int i = 0; i < TAILLE_BLOC; i++)
                {
                    aDechiffrer[j][i] = uneSource[j * TAILLE_BLOC + i];
                }
            }

            // Déchiffrement
            for (int numBloc = 0; numBloc < aDechiffrer.Length; numBloc++)
            {
                byte[] resultA = exclusiveOR(aDechiffrer[numBloc], uneCle);
                dechiffre[numBloc] = new byte[TAILLE_BLOC];
                // 1ère phase
                byte[] resultB = new byte[TAILLE_BLOC];
                resultB = exclusiveOR(iv, resultA);
                // 2ème phase
                for (int i = 0; i < TAILLE_BLOC; i++)
                {
                    dechiffre[numBloc][i] = resultB[i];
                    iv[i] = aDechiffrer[numBloc][i];
                }
            }

            // Création du fichier de destination à partir du tableau de blocs 
            for (int j = 0; j < dechiffre.Length; j++)
            {
                for (int i = 0; i < TAILLE_BLOC; i++)
                {
                    destination[j * TAILLE_BLOC + i] = dechiffre[j][i];
                }
            }

            return destination;
        }

        // Source : https://stackoverflow.com/questions/20802857/xor-function-for-two-hex-byte-arrays
        private static byte[] exclusiveOR(byte[] arr1, byte[] arr2)
        {
            if (arr1.Length != arr2.Length)
                throw new ArgumentException("arr1 and arr2 are not the same length");

            byte[] result = new byte[arr1.Length];

            for (int i = 0; i < arr1.Length; ++i)
                result[i] = (byte)(arr1[i] ^ arr2[i]);

            return result;
        }
    }
}
