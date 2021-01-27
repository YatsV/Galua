using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Forms;
using System.IO;

namespace Heming_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static BitArray array1;
        static BitArray array2;
        static BitArray Decodarray;
        static int dlina;
        bool flag = false;
        bool flag1 = false;
        string abc;
        string abcd;
        static BitArray ConvertFileToBitArray(string path)
            {
                byte[] fileBytes = File.ReadAllBytes(path);
                BitArray array = new BitArray(fileBytes);

                return array;
            }
        static BitArray Convert1FileToBitArray(string path1)
        {
            byte[] fileBytes = File.ReadAllBytes(path1);
            BitArray messageArray1 = new BitArray(fileBytes);


            return messageArray1;
        }

        public static byte[] BitArrayToBytes(System.Collections.BitArray Coded1) // переводимо масив бітів в масив байтів
        {
            if (Coded1.Length == 0)
            {
                throw new System.ArgumentException("must have at least length 1", "bitarray");
            }

            int num_bytes = Coded1.Length / 8;

            if (Coded1.Length % 8 != 0)
            {
                num_bytes += 1;
            }

            var bytes = new byte[num_bytes];
            Coded1.CopyTo(bytes, 0);
            return bytes;
        }

        static BitArray MyCoding(BitArray array)
        {
            int count = array1.Count; // кількість біт в масиві
            int newBits = (int)Math.Ceiling(count / 8.0) * 9;
            int last = count + newBits;
            if (newBits != 0) last = last + (8 - (count % 8));
            BitArray Coded = new BitArray(last, false); // новий пустий масив біт

            for (int i = 0; i < count; i += 8)
            {
                BitArray inf = new BitArray(8);

                for (int j = 0; j < 8; j++)
                {
                    if (j + i >= count)
                    {
                        inf[j] = false;
                    }
                    else
                    {
                        inf[j] = array1[j + i];
                    }
                }
                int[] rad = new int[8];
                BitArray resultat = new BitArray(9); 
                int counter = 0;
                BitArray rad0 = new BitArray(9);// { 1, 1, 1, 0, 1, 0, 0, 0 };
                rad0[0] = true; rad0[1] = true; rad0[2] = true; rad0[3] = false; rad0[4] = true; rad0[5] = false; rad0[6] = false; rad0[7] = false; rad0[8] = true; // { 1, 1, 1, 0, 1, 0, 0, 0, 1 };
                BitArray rad1 = new BitArray(9); //{ 0, 1, 1, 1, 0, 1, 0, 0 };
                rad1[0] = false; rad1[1] = true; rad1[2] = true; rad1[3] = true; rad1[4] = false; rad1[5] = true; rad1[6] = false; rad1[7] = false; rad1[8] = false;//{ 0, 1, 1, 1, 0, 1, 0, 0, 0 };
                BitArray rad2 = new BitArray(9); //{ 0, 0, 1, 1, 1, 0, 1, 0 };
                rad2[0] = false; rad2[1] = false; rad2[2] = true; rad2[3] = true; rad2[4] = true; rad2[5] = false; rad2[6] = true; rad2[7] = false; rad2[8] = true; //{ 0, 0, 1, 1, 1, 0, 1, 0, 1 };
                BitArray rad3 = new BitArray(9); //{ 0, 0, 0, 1, 1, 1, 0, 1 };
                rad3[0] = false; rad3[1] = false; rad3[2] = false; rad3[3] = true; rad3[4] = true; rad3[5] = true; rad3[6] = false; rad3[7] = true; rad3[8] = false; //{ 0, 0, 0, 1, 1, 1, 0, 1, 0 };
                BitArray rad4 = new BitArray(9); //{ 1, 1, 1, 0, 0, 1, 1, 0 };
                rad4[0] = true; rad4[1] = true; rad4[2] = true; rad4[3] = false; rad4[4] = false; rad4[5] = true; rad4[6] = true; rad4[7] = false; rad4[8] = false; //{ 1, 1, 1, 0, 0, 1, 1, 0, 0 };
                BitArray rad5 = new BitArray(9); //{ 0, 1, 1, 1, 0, 0, 1, 1 };
                rad5[0] = false; rad5[1] = true; rad5[2] = true; rad5[3] = true; rad5[4] = false; rad5[5] = false; rad5[6] = true; rad5[7] = true; rad5[8] = true; //{ 0, 1, 1, 1, 0, 0, 1, 1, 1 };
                BitArray rad6 = new BitArray(9); //{ 1, 1, 0, 1, 0, 0, 0, 1 };
                rad6[0] = true; rad6[1] = true; rad6[2] = false; rad6[3] = true; rad6[4] = false; rad6[5] = false; rad6[6] = false; rad6[7] = true; rad6[8] = false;
                BitArray rad7 = new BitArray(9); //{ 1, 1, 0, 1, 0, 0, 0, 1 };
                rad7[0] = true; rad7[1] = true; rad7[2] = false; rad7[3] = true; rad7[4] = false; rad7[5] = false; rad7[6] = false; rad7[7] = true; rad7[8] = true;
                BitArray res = new BitArray(17);

                for (int n = 0; n < 8; n++)
                {
                    if (inf[n] == true)
                    {
                        rad[n] = 1;
                        counter++;
                    }
                    if (inf[n] == false)
                    {
                        rad[n] = 0;
                    }
                }
                if (rad[0] == 1)
                {
                    for (int l = 0; l < 9; l++)
                    {
                        resultat[l] ^= rad0[l];
                    }
                }
                if (rad[1] == 1)
                {
                    for (int l = 0; l < 9; l++)
                    {
                        resultat[l] ^= rad1[l];
                    }
                }
                if (rad[2] == 1)
                {
                    for (int l = 0; l < 9; l++)
                    {
                        resultat[l] ^= rad2[l];
                    }
                }
                if (rad[3] == 1)
                {
                    for (int l = 0; l < 9; l++)
                    {
                        resultat[l] ^= rad3[l];
                    }
                }
                if (rad[4] == 1)
                {
                    for (int l = 0; l < 9; l++)
                    {
                        resultat[l] ^= rad4[l];
                    }
                }
                if (rad[5] == 1)
                {
                    for (int l = 0; l < 9; l++)
                    {
                        resultat[l] ^= rad5[l];
                    }
                }
                if (rad[6] == 1)
                {
                    for (int l = 0; l < 9; l++)
                    {
                        resultat[l] ^= rad6[l];
                    }
                }
                if (rad[7] == 1)
                {
                    for (int l = 0; l < 9; l++)
                    {
                        resultat[l] ^= rad7[l];
                    }
                }

                for (int l = 0; l <= 7; l++)
                {
                    res[l] = inf[l];
                }
                for (int l = 0; l < 9; l++)
                {
                    res[l + 8] = resultat[l];
                }

                for (int k = 0; k < 17; k++)
                {
                    Coded[k + (17 * (i / 8))] = res[k];
                }
            }
            return Coded;
        }

        static BitArray MyDeCoding(BitArray Array2)
        {
            int countBits = Array2.Count; // кількість біт в масиві
            BitArray Codedd = new BitArray(dlina, false);
            int count = 0;
            for (int i = 0; i < countBits; i += 17)
            {
                if (count == 1)
                {
                    break;
                }
                for (int j = 0; j < 8; j++)
                {
                    if (j + (8 * (i / 17)) == dlina)
                    {
                        count = 1;
                        break;
                    }
                    Codedd[j + (8 * (i / 17))] = Array2[j + (17 * (i / 17))];
                }
            }
            return Codedd;
        }



        private void button1_Click(object sender, EventArgs e)
            {
                if (flag == true)
                {
                    //File.Create(Directory.GetCurrentDirectory() + "\\test1.txt");
                    //string path1 = Directory.GetCurrentDirectory() + "\\test1.txt";
                    if (File.Exists(abc) == false) return; // перевірка на наявність файла
                    BitArray Array = ConvertFileToBitArray(abc); // читаємо файл і записуємо у BitArray
                    BitArray Coded = MyCoding(Array); // кодуємо bitArray

                //BitArrayToBytes(abcd, Coded); // записуємо bitArray у файл
                System.IO.File.WriteAllBytes(abcd, BitArrayToBytes(Coded));
                MessageBox.Show("Файл Сохранен");
            }
                if (flag1 == true)
                {
                    //string path1 = Directory.GetCurrentDirectory() + "\\test2.txt";
                    if (File.Exists(abc) == false) return;
                    BitArray Array = Convert1FileToBitArray(abc);
                    BitArray Decoded = MyDeCoding(array2);

                //WriteBitArrayToFile(abcd, Coded);
                System.IO.File.WriteAllBytes(abcd, BitArrayToBytes(Decoded));
                MessageBox.Show("Файл Сохранен");
            }

                flag = false;
                flag1 = false;


            }


            private void button2_Click(object sender, EventArgs e)
            {

            }

            private void button4_Click(object sender, EventArgs e)
            {
            if (flag == true)
                {
                if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                string filename = openFileDialog1.FileName;
                array1 = ConvertFileToBitArray(filename);
                dlina = array1.Count;
                textBox1.Text = filename;
                abc = filename;
                 }
            if (flag1 == true)
            {
                if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                string filename = openFileDialog1.FileName;
                array2 = ConvertFileToBitArray(filename);
                textBox1.Text = filename;
            }
            }

            private void button3_Click(object sender, EventArgs e)
            {
            if (flag == true || flag1 == true)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                string filename = saveFileDialog1.FileName;
                textBox2.Text = filename;
                abcd = filename;
            }
        }

            private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
            {
                flag = true;
                flag1 = false;
            }

            private void radioButton2_CheckedChanged_1(object sender, EventArgs e)
            {
                flag1 = true;
                flag = false;
            }

            private void button2_Click_1(object sender, EventArgs e)
            {
            textBox1.Clear();
            textBox2.Clear();
            }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
        }
    }
    }

