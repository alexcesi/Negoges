﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace GestionInventaireWinForms
{
    public partial class AjoutArticle : Form
    {

        
        private long id;
        private string nameArticle, descriptionArticle, domaineArticle, urlImageArticle, volumeArticle;
        private int prixArticle, prixArticleV;

        int i;

        private HttpWebRequest webRequest;

        string CategorieSelect;
        public AjoutArticle()
        {
            InitializeComponent();
        }

        private void AjoutProduit_Load(object sender, EventArgs e)
        {
            Lecture_Provider("http://localhost:44319/api/Provider");
            Lecture_Categorie("http://localhost:44319/api/Categorie");

        }

        private void textBox_nomArticle_TextChanged(object sender, System.EventArgs e)
        {
            nameArticle = textBox_nomArticle.Text;
        }

        private void textBox_domaineArticle_TextChanged(object sender, System.EventArgs e)
        {
            domaineArticle = textBox_domaineArticle.Text;
        }

        private void textBox_descriptionArticle_TextChanged(object sender, System.EventArgs e)
        {
            descriptionArticle = textBox_descriptionArticle.Text;
        }

        private void textBox_urlImgArticle_TextChanged(object sender, System.EventArgs e)
        {
            urlImageArticle = textBox_urlImgArticle.Text;
        }

        private void textBox_volumeArticle_TextChanged(object sender, System.EventArgs e)
        {
            volumeArticle = textBox_volumeArticle.Text;
        }

        private void textBox_prixArticle_TextChanged(object sender, System.EventArgs e)
        {
            prixArticle = Int32.Parse(textBox_prixArticle.Text);
        }

        private void label1_nomArticle_Click(object sender, EventArgs e)
        {

        }

        private void btn_AjoutArticle_Click(object sender, System.EventArgs e)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create("http://localhost:44319/api/Categorie");
            var webResponse = (HttpWebResponse)webRequest.GetResponse();

            if ((webResponse.StatusCode == HttpStatusCode.OK))
            {
                var reader = new StreamReader(webResponse.GetResponseStream());
                string s = reader.ReadToEnd();
                var arr = JsonConvert.DeserializeObject<List<CategorieDTO>>(s);


                CategorieSelect = comboBox_categorieArticle.GetItemText(comboBox_categorieArticle.SelectedItem);
            }
            string url = "http://localhost:44319/api/Article";

            string requestParams = JsonTester();

            webRequest = (HttpWebRequest)WebRequest.Create(url);

            webRequest.Method = "POST";
            webRequest.ContentType = "application/json";

            byte[] byteArray = Encoding.UTF8.GetBytes(requestParams);
            webRequest.ContentLength = byteArray.Length;
            using (Stream requestStream = webRequest.GetRequestStream())
            {
                requestStream.Write(byteArray, 0, byteArray.Length);
            }
            url = "http://localhost:44319/api/Article";

            webRequest = (HttpWebRequest)WebRequest.Create(url);
            webResponse = (HttpWebResponse)webRequest.GetResponse();

            if ((webResponse.StatusCode == HttpStatusCode.OK))
            {

                var reader = new StreamReader(webResponse.GetResponseStream());
                string s = reader.ReadToEnd();
                var arr = JsonConvert.DeserializeObject<List<ArticleDTO>>(s);

                var maxId = from r in arr
                            orderby r.Id
                            select r;
                i = 1;

                foreach (var std in maxId)
                {
                    i++;

                }

                url = "http://localhost:44319/api/Stock";

                requestParams = JsonStock();

                webRequest = (HttpWebRequest)WebRequest.Create(url);

                webRequest.Method = "POST";
                webRequest.ContentType = "application/json";

                byte[] byteArrayy = Encoding.UTF8.GetBytes(requestParams);
                webRequest.ContentLength = byteArrayy.Length;
                using (Stream requestStream = webRequest.GetRequestStream())
                {
                    requestStream.Write(byteArrayy, 0, byteArrayy.Length);
                }

            }
            else
            {
                MessageBox.Show(string.Format("Status code == {0}", webResponse.StatusCode));
            }



            this.Close();
        }

        public string JsonStock()
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();

            var jsonData = new StockDTO()
            {


                Id = i,
                Quantity = 0


            };


            var result = ser.Serialize(jsonData);
            return result;
        }

        public string JsonTester()
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();

            var jsonData = new ArticleDTO()
            {

                Name = nameArticle,
                BuyPrice = prixArticle,
                SellPrice = prixArticleV,
                Description = descriptionArticle,
                Domaine = domaineArticle,
                ImgUrl = urlImageArticle,
                Volume = volumeArticle,
                isActive = true,
                commandeAuto = true,

            };


            var result = ser.Serialize(jsonData);
            return result;
        }

        public void Lecture_Provider(string url)
        {
            if (url == "http://localhost:44319/api/Provider")
            {
                var webRequest = (HttpWebRequest)WebRequest.Create(url);
                var webResponse = (HttpWebResponse)webRequest.GetResponse();

                if ((webResponse.StatusCode == HttpStatusCode.OK))
                {
                    var reader = new StreamReader(webResponse.GetResponseStream());
                    string s = reader.ReadToEnd();
                    var arr = JsonConvert.DeserializeObject<List<ProviderDTO>>(s);
                    var list = new BindingList<ProviderDTO>(arr);
                    var data = new BindingSource(list, null);
                    comboBox_fournisseurArticle.DataSource = data.DataSource;
                    comboBox_fournisseurArticle.DisplayMember = "nomEntreprise";
                    comboBox_fournisseurArticle.ValueMember = "nomEntreprise";

                }
                else
                {
                    MessageBox.Show(string.Format("Status code == {0}", webResponse.StatusCode));
                }
            }
        }
        public void Lecture_Categorie(string url)
        {
            if (url == "http://localhost:44319/api/Categorie")
            {
                var webRequest = (HttpWebRequest)WebRequest.Create(url);
                var webResponse = (HttpWebResponse)webRequest.GetResponse();

                if ((webResponse.StatusCode == HttpStatusCode.OK))
                {
                    var reader = new StreamReader(webResponse.GetResponseStream());
                    string s = reader.ReadToEnd();
                    var arr = JsonConvert.DeserializeObject<List<CategorieDTO>>(s);
                    var list = new BindingList<CategorieDTO>(arr);
                    var data = new BindingSource(list, null);
                    comboBox_categorieArticle.DataSource = data.DataSource;
                    comboBox_categorieArticle.DisplayMember = "libelle";
                    comboBox_categorieArticle.ValueMember = "libelle";

                }
                else
                {
                    MessageBox.Show(string.Format("Status code == {0}", webResponse.StatusCode));
                }
            }
        }
    }
}
