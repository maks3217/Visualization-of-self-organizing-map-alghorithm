using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Kohonen
{
    class Siec
    {
        Random random = new Random();
        int Ile_neuronow;
        public List<Point> Neurony = new List<Point>();
        public Siec(int Ile_neuronow)
        {
            this.Ile_neuronow = Ile_neuronow;
            for (int i = 0; i < Ile_neuronow; i++)
            {
                double X = random.NextDouble() * 600;
                double Y = random.NextDouble() * 600;
                Point neuron = new Point(X, Y);
                Neurony.Add(neuron);
            }
        }

        public void Learn(int T, List<Point> punkty)
        {
            for (int t = 0; t < T; t++)
            {
                int indeks = random.Next() % punkty.Count;
                int winner = Winner(punkty[indeks]);
                ZmienWagi(winner, punkty[indeks], t, T);

            }
            Debug.WriteLine("siec nauczona");
        }

        void ZmienWagi(int i, Point punkt, double t, double T)
        {
            for (int j = 0; j < Ile_neuronow; j++)
            {
                Point neuron = Neurony[j];
                neuron.X = Neurony[j].X + ((T - t) / T) * G(i, j) * (punkt.X - neuron.X);
                neuron.Y = Neurony[j].Y + ((T - t) / T) * G(i, j) * (punkt.Y - neuron.Y);
                Neurony[j] = neuron;
            }
        }

        double G(int i, int j)
        {
            double lamda = 1;
            return Math.Exp((-1 * Ro(i,j) * Ro(i,j)) / (2 * lamda * lamda));
        }

        int Ro(int i, int j)
        {
            return Math.Min(Math.Abs(i - j), Math.Abs(i - (j + Ile_neuronow)));
        }

        int Winner(Point punkt)
        {
            int winer = 0;
            double min_lenght = lenght(Neurony[0], punkt);
            for (int i = 1; i < Ile_neuronow; i++)
            {
                if (lenght(Neurony[i], punkt) < min_lenght)
                {
                    min_lenght = lenght(Neurony[i], punkt);
                    winer = i;
                }
            }
            return winer;
        }

        double lenght(Point punkt1, Point punkt2)
        {
            return Math.Sqrt(((punkt1.X - punkt2.X) * (punkt1.X - punkt2.X)) + ((punkt1.Y - punkt2.Y) * (punkt1.Y - punkt2.Y)));
        }


    }
}
