using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ReadBrowserhistory
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ReadChrome();
            //string profile_name = Get_FireFox_profile_name();
            //string firefox = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Mozilla\Firefox\Profiles\" + profile_name + @"\places.sqlite";
            //if (profile_name.Length > 2)
            //{
            //    ReadFireFox(firefox);
            //}
            this.Close();
        }

        private bool ReadFireFox(string firefox)
        {

            //try
            //{
            //    firefox = @"C:\Users\Exception\AppData\Roaming\Mozilla\Firefox\Profiles\ntjt01so.default";

            //    SQLiteConnection cn = new SQLiteConnection("Data Source=" + firefox + ";Version=3;New=False;Compress=True");
            //    cn.Open();

            //    SQLiteDataAdapter sd = new SQLiteDataAdapter("select * from moz_places order by last_visit_date desc", cn);
            //    System.Data.DataSet ds = new System.Data.DataSet();
            //    sd.Fill(ds);
            //    //dataGridView1.DataSource = ds.Tables[0];


            //    cn.Close();
               return true;
            //}
            //catch { return false; }

        }

        private void ReadChrome()
        {
            try
            {

                string google = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Google\Chrome\User Data\Default\History";
                SQLiteConnection cn = new SQLiteConnection("Data Source=" + google + ";Version=3;New=False;Compress=True");
                cn.Open();
                
                //SQLiteDataAdapter sd = new SQLiteDataAdapter("select title,url from urls order by last_visit_time desc", cn);
                //System.Data.DataSet ds = new System.Data.DataSet();
                //sd.Fill(ds);
                //dataGridView1.DataSource = ds.Tables[0];


                 SQLiteCommand com = new SQLiteCommand("select title,url from urls order by last_visit_time desc", cn);
                 SQLiteDataReader reader = com.ExecuteReader();

                 string data="";
                 //data = ReadOldData("History_files.rog");

        while (reader.Read())
        {
            string u = reader["url"].ToString();
            if (u.StartsWith("http") == false)
                continue;
            data += "\r\n" + Spaces(reader["title"].ToString(), 100) + "   :   " + u;
        }
        WriteNewData(data);
                
                cn.Close();
            }
            catch { }
        }

        private object Spaces(string p1, int p2)
        {
            while (p1.Length < p2)
                p1 += " ";
            return p1;
        }

        private void WriteNewData(string data)
        {
            try
            {
                System.IO.File.WriteAllText("History_files.rog", data);
            }
            catch { } 
        }

        private string ReadOldData(string p)
        {

            try
            {
                return System.IO.File.ReadAllText(p);
            }
            catch { return ""; }
        }

        private string Get_FireFox_profile_name()
        {
            string path = @"C:\Users\"+Environment.UserName+@"\AppData\Roaming\Mozilla\Firefox\Profiles\";
            string[] dirs = System.IO.Directory.GetDirectories(path);
            foreach (string d in dirs)
            {
                if (d.EndsWith(".default"))
                {
                    char sep = '\\';
                    if (d.Contains('/'))
                        sep = '/';
                    if(d.Contains(sep))
                    {
                        var ds = d.Split(sep);
                        return ds[ds.Length - 1];
                    }
                    else
                     return d;
                }
            }

            return "";
        }

         
    }
}
