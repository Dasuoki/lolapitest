using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net;

namespace lolapitestapp
{
    public partial class Form1 : Form
    {
        string apikey = "?api_key=RGAPI-B1F0AC0F-8798-418B-B4A4-A2A9A1F1EAC2";
        string name = "";
        string region = "";
        string summonerinfo = "";
        string id = "";

        Dictionary<string, string> Summoner; 

        string downloadJson(string region, string jsonadd1, string jsonadd2, string name, string apikey)
        {
            WebClient client = new WebClient();
            string data = client.DownloadString("https://" + region + jsonadd1 + region + jsonadd2 + name + apikey);
            return data;
        }

        void getBasicInfo()
        {
            summonerinfo = downloadJson(region, ".api.pvp.net/api/lol/", "/v1.4/summoner/by-name/", name, apikey);
            summonerinfo = summonerinfo.Remove(0, 4 + name.Length);
            summonerinfo = summonerinfo.Remove(summonerinfo.Length - 1);
            Summoner = JsonConvert.DeserializeObject<Dictionary<string, string>>(summonerinfo);
            id = Summoner["id"];
        }

        void updateUI()
        {
            label4.Text = "ID: " + id;
            label5.Text = "Name: " + Summoner["name"];
            label6.Text = "Level: " + Summoner["summonerLevel"];
            pictureBox1.ImageLocation = "http://ddragon.leagueoflegends.com/cdn/6.15.1/img/profileicon/" + Summoner["profileIconId"] + ".png";
        }
        
        void setSummoner()
        {
            name = textBox1.Text;
            region = comboBox1.Text.ToLower();
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void doMagic(object sender, EventArgs e)
        {
            setSummoner();
            getBasicInfo();
            updateUI();
        }
    }
}