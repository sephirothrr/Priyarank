using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Priyarank.Models
{
    public static class Elo
    {
        private const int K = 32;

        public static Tuple<double, double> ExpectedValues(int r1, int r2)
        {
            var e1 = 1 / (1 + Math.Pow(10, (double)(r2 - r1) / 400));
            var e2 = 1 / (1 + Math.Pow(10, (double)(r1 - r2) / 400));

            return new Tuple<double, double>(e1, e2);
        }

        public static Tuple<int, int> NewScores(int win, int lose)
        {
            var (ev1, ev2) = ExpectedValues(win, lose);
            win = (int)(win + (K * (1 - ev1)));
            lose = (int)(lose + (K * (0 - ev2)));

            return new Tuple<int, int>(win, lose);
        }
    }
}
