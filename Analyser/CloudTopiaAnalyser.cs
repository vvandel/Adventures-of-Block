/*
The MIT License (MIT)

Copyright (c) 2018 Twan Veldhuis, Ivar Troost

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyser
{
    class CloudTopiaAnalyser
    {
        List<float> star_start;
        List<float> star_end;
        List<float> star_x;

        int seg0, seg1;

        int segment(float tick) { return tick <= seg0 ? 0 : tick <= seg1 ? 1 : 2; }

        int segmentOfStar(float tick, float x)
        {
            int star = -1;
            for (int i = 0; i < star_x.Count; i++)
            {
                if (tick < star_start[i]) continue;
                if (tick > star_end[i]) continue;
                if (x >= star_x[i] - 0.45f && x <= star_x[i] + 0.45f)
                { star = i; break; }
            }

            if (star == -1) return segment(tick);
            else
            {
                float start = star_start[star];
                star_start.RemoveAt(star);
                star_end.RemoveAt(star);
                star_x.RemoveAt(star);
                return segment(start);
            }
        }

        public int[] AnalyseAttempt(string[] lines)
        {
            int[] points = new int[3];
            foreach (string line_ in lines)
            {
                string line = line_;
                if (!line.StartsWith("#"))
                    break;
                line = line.Replace("falling star", "falling_star");
                string[] words = line.Split();
                //#5 +1 star (-0.3, -0.1, 0.0) 5.919998
                float tick = float.Parse(words[6], System.Globalization.CultureInfo.InvariantCulture);
                string type = words[2];
                int pts = int.Parse(words[1].Replace("+", ""));
                if (type == "star")
                {
                    float x = float.Parse(words[3].Replace("(", "").Replace(",", ""), System.Globalization.CultureInfo.InvariantCulture);
                    points[segmentOfStar(tick, x)] += pts;
                }
                else
                    points[segment(tick)] += pts;
            }
            return points;
        }

        public void SetCloudTopia()
        {
            seg0 = 15; seg1 = 36;
            star_start = new List<float>() { 4, 10, 32 };
            star_end = new List<float>() { 100, 100, 100 };
            star_x = new List<float>() { 5.74f, 0, -4.46f };
        }

        public void SetLevel1()
        {
            seg0 = 12; seg1 = 29;
            star_start = new List<float>();
            star_end = new List<float>();
            star_x = new List<float>();
        }

        public void SetBombmania()
        {
            seg0 = 13; seg1 = 35;
            star_start = new List<float>();
            star_end = new List<float>();
            star_x = new List<float>();
        }

    }
}
