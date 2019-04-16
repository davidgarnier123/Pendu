using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace PenduWForm
{
    public partial class Form1 : Form
    {
        char[] array = new char[26]; //creation tableau pour les lettres

        public Boolean testlettre(char lettre, string motcache, ref string test)
        {
            string lettrechar = lettre.ToString();
           
            
            if (Regex.IsMatch(motcache.ToUpper(), lettrechar) == true) //si la lettre utilise est dans le mot
            {
                char[] lettreArray = new char[motcache.Length];//je crée un tableau our mettre mon mot
                char[] testArray = new char[motcache.Length];//je crée un tableau our mettre mon test qui se remplit
                //je boucle pour le rempliravec mes lettres
                for (int o = 0; o < lettreArray.Length; o++)
                {
                    lettreArray[o] = Convert.ToChar(motcache.Substring(o, 1).ToUpper()); //je met chaque lettre dans une case [ o ]
                }

                for (int o = 0; o < lettreArray.Length; o++)
                {
                    testArray[o] = Convert.ToChar(test.Substring(o, 1).ToUpper()); //je met chaque lettre dans une case [ o ]
                }



                for (int i = 0; i < motcache.Length; i++) //on teste chaque lettre une par une et on les modifie
                {

                    if (lettreArray[i] != lettre && Char.IsLetter(testArray[i]) == false) { //si la lettre nest pas la bonne je la remplace par _
                        lettreArray[i] = '_';
                    }
                    
                
                     
                   
                }
                //fin de verif, je remet mon tableau en string pour sortir le mot avec les lettres rentrée et les manquantes


                test = new string(lettreArray);

               
                txtMot.Text = test;
                return true;
            }

            return false;
        }
        public Boolean Nouveaumot(string motcache)
        {

            if (Regex.IsMatch(motcache, @"^[a-zA-Z]+$") == true)
            {



                return true;
            } // je verifie si elle ne contient que des lettres valides

            MessageBox.Show("Ne pas utiliser de caracteres spéciaux ou de chiffres");

            return false;


        }
        public string motcachechar(string motcache)
        {
            char[] motcachecharray = new char[motcache.Length];
            string decouverte = "";
            for (int a = 0; a < motcache.Length; a++)
            {

                motcachecharray[a] = '_';
                decouverte = decouverte + motcachecharray[a];
            }

            return decouverte;
        }
        public Form1()
        {
            InitializeComponent();
        }
        public string motcache;
        int essai; //nombre d'essai, permet de choisir l'image correspondante
        public string test; 
        private void Form1_Load(object sender, EventArgs e)
        {

            int positionX = 30; //variable position
            int positionY = 10;//variable position


            grbLettre.Visible = false; //pour la premiere partie du jeu, je cache les lettres
            btnReset.Visible = false;// je cache le bouton rejouer
            pcbPendu.Visible = false;

            // génération des lettres
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = Convert.ToChar('a' + i);
                // création du bouton
                Button btn = new Button();
                // ajout du bouton dans le groupe de boutons pour l'affichage
                grbLettre.Controls.Add(btn);
                // fixe la taille du bouton
                btn.Size = new Size(30, 30);
                // positionne le bouton dans le groupe (x et y sont les coordonnées)
                btn.Text = Convert.ToChar('a' + i).ToString().ToUpper();


                if (positionX == (30 * 8))
                {
                    positionX = 30;
                    positionY = positionY + 30;
                }
                if (positionX < (30 * 8))
                {
                    positionX = positionX + 30;

                }
                btn.BackColor = Color.Gray;

                btn.Location = new Point(positionX, positionY);

                btn.Click += new System.EventHandler(btnAlpha_Click);
            }





        }
        private void btnAlpha_Click(object sender, EventArgs e)
        {


            if (testlettre(Convert.ToChar(((Button)sender).Text), motcache, ref test) == false)
            {
                essai++;
                if (essai == 11) { btnReset.Focus(); txtMot.Enabled = false; txtMot.Text = motcache.ToUpper(); grbLettre.Enabled = false; txtMot.BackColor = Color.LightGreen; }
                pcbPendu.Visible = true;
                string location = Application.StartupPath + "//" + essai + ".png";
                pcbPendu.ImageLocation = location;
                txtMot.BackColor = Color.Red;
            }
            if (testlettre(Convert.ToChar(((Button)sender).Text), motcache, ref test) == true)
            {
                txtMot.BackColor = Color.LightGreen;
                if (motcache.ToUpper() == test) {
                    pcbPendu.Visible = true; grbLettre.Enabled = false; grbLettre.Visible = false; string location = Application.StartupPath + "//win.jpg";
                    pcbPendu.ImageLocation = location;
                }
            }
                ((Button)sender).Enabled = false; //desactive le boutton quand il est click
        }


        private void txtMot_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                try
                {

                    motcache = txtMot.Text;
                    if (Nouveaumot(motcache) == true)
                    {
                        test = motcachechar(motcache);
                        for (int y = 1; y <= (motcachechar(motcache).Length + motcachechar(motcache).Length); y = y + 2) { test = test.Insert(y, " "); } //remplace les lettres par _

                        txtMot.Text = test;
                        txtMot.Enabled = false;
                        grbLettre.Visible = true; //jaffiche l'ecran pour le joueur deux
                        btnReset.Visible = true;//jaffiche l'ecran pour le joueur deux

                    }


                }
                catch (Exception) { txtMot.Text = "Erreur de saisi"; };


                e.Handled = true;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            motcache = "";
            txtMot.Text = "";
            txtMot.Enabled = true;
            txtMot.Focus();
            pcbPendu.Image = null;
            pcbPendu.Visible = false;
            grbLettre.Visible = false;
            grbLettre.Enabled = true;
            txtMot.BackColor = Color.White;
            foreach (Button field in grbLettre.Controls)
            {
                if (field is Button)
                    ((Button)field).Enabled = true;
               
            }

        }

    }
}

