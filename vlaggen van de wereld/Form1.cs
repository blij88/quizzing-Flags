using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vlaggen_van_de_wereld
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string json = new WebClient().DownloadString("https://flagcdn.com/en/codes.json");
        Random rand = new Random();
        string correctFlag;
        int score = 0;


        private void Form1_Load(object sender, EventArgs e)
        {
            ShowFlag(pickFlag());
            DisplayOnButtons();
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
        //https://flagpedia.net
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool correct = checkAnswer(button1);
            if (correct == true)
            {
                label1.Text = "Welk land is dit?";
                ShowFlag(pickFlag());
                DisplayOnButtons();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool correct = checkAnswer(button2);
            if (correct == true)
            {
                label1.Text = "Welk land is dit?";
                ShowFlag(pickFlag());
                DisplayOnButtons();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bool correct = checkAnswer(button3);
            if (correct == true)
            {
                label1.Text = "Welk land is dit?";
                ShowFlag(pickFlag());
                DisplayOnButtons();
            }
        }

// picks a flag turns the different parts into an array
        public string[] pickFlag()
        {
            string flagCode;
            string[] nameAndCode = new string[4];
            bool isACountry = false;
            do
            {
                string cleanJson = json.Trim('{', '}', ' ', '\n');
                string[] allFlagCodes = cleanJson.Split(',');
                string chosenFlag = allFlagCodes[rand.Next(0, allFlagCodes.Length)].Trim();
                nameAndCode = chosenFlag.Split('"');
                flagCode = nameAndCode[1]; 
                
                if (flagCode.Length == 2)
                {
                    isACountry = true;
                }
            } while (isACountry == false);
            return nameAndCode;
        }
        // displays the Image
        public void ShowFlag(string[] flagInfo)
        {
            string flagCode = flagInfo[1];
            correctFlag = flagInfo[3];
            string link =  "https://flagcdn.com/h120/" + flagCode + ".jpg";
            pictureBox1.ImageLocation = link;
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
        }
        // Buttons Display 1 correct and two false answers
        public void DisplayOnButtons()
        {            
            string[] Answers = new string[3];
            Answers[0] = correctFlag;
            for (int i = 1; i < 3; i++)
            {
                bool isDiff = false;
                string[] checkWrongFlag = pickFlag();
                do
                {
                    if (checkWrongFlag[3] != correctFlag || checkWrongFlag[3] != Answers[1])
                    {
                        isDiff = true;
                        Answers[i] =  checkWrongFlag[3];
                    }

                } while (isDiff == false); ;
            }
            string[] randomAnswers = Answers.OrderBy(X => rand.Next()).ToArray();
            button1.Text = randomAnswers[0];
            button2.Text = randomAnswers[1];
            button3.Text = randomAnswers[2];
        }
        // check wether button clicked is correct

        public bool checkAnswer(Button chosen)
        {
            if (chosen.Text == correctFlag)
            {
                label1.Text = "Correct!";
                score += 1;
                label2.Text = "Score: " + score;
                return true;
            }
            else
            {
                label1.Text = "helaas, onjuist.";
                return false;
            }

        }


    }
}
