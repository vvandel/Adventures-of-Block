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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace Analyser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        float[] xaxis = new float[] { 0.2f, 0.4f, 0.6f, 0.8f, 1 };
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            var odfRes = ofd.ShowDialog();
            if(odfRes == DialogResult.OK)
            {
                string name = ofd.FileName;

                //md5 validation
                string lastLine =  LogParser.RemoveLastLineBackup("backup", name);
                byte[] rawHash;
                using (var md5 = MD5.Create())
                {
                    using (var stream = File.OpenRead("backup"))
                    {
                        rawHash = md5.ComputeHash(stream);
                    }
                }
                string hash = ToHex(rawHash, false);
                List<string> output = new List<string>();
                if (lastLine.Replace("SEED ", "") != hash)
                {
                    output.Add("CHEATER!");
                    File.WriteAllLines("output.txt", output.ToArray());
                    File.Delete("backup");
                    return;
                }

                //now actually do stuff with it
                

                string[] file = File.ReadAllLines("backup");
                var result = LogParser.ReadAttempts(file);
                string line = "";
                for (int i = 0; i < result.Length; i++)
                {
                    var ana = new CloudTopiaAnalyser();
                    if (result[i].levelName == "Cloudtopia")
                        ana.SetCloudTopia();
                    else if (result[i].levelName == "Level1")
                        ana.SetLevel1();
                    else if (result[i].levelName == "Bombmania")
                        ana.SetBombmania();

                    List<int[]> scores = new List<int[]>();

                    for (int k = 0; k < result[i].attempts.Length; k++)
                    {
                        int[] r = ana.AnalyseAttempt(result[i].attempts[k]);
                        scores.Add(r);
                    }

                   
                    for (int j= 0; j < 3; j++)
                    {
                        var lsr = new LinearLeastSquaresInterpolation(xaxis, scores.Select((r) => L(r[j], result[i].levelName, j)));
                        line += lsr.Slope.ToString(System.Globalization.CultureInfo.InvariantCulture) + "\t";
                    }
                    
                }
                output.Add(line);
                File.WriteAllLines("output.txt", output.ToArray());
                File.Delete("backup");
            }
        }

        float[] maxct = new float[] { 5, 11, 5 };
        float[] maxl1 = new float[] { 4, 5, 4 };
        float[] maxbm = new float[] { 5, 8, 6 };
        float L(float x, string levelName, int i)
        {
            float max = 0;
            switch(levelName)
            {
                case "Level1":
                    max = maxl1[i];
                    break;
                case "Cloudtopia":
                    max = maxct[i];
                    break;

                case "Bombmania":
                    max = maxbm[i];
                    break;
            }
            return Math.Max(0, x / max + 1) / 2;
        }
        public static string ToHex(byte[] bytes, bool upperCase)
        {
            StringBuilder result = new StringBuilder(bytes.Length * 2);

            for (int i = 0; i < bytes.Length; i++)
                result.Append(bytes[i].ToString(upperCase ? "X2" : "x2"));

            return result.ToString();
        }
    }
}
