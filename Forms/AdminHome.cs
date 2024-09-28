﻿using GameServer_Management.Class;
using GameServer_Management.Controller;
using Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameServer_Management.Forms
{
    public partial class AdminHome : Form
    {
        private KryptonCheckButton cb = new KryptonCheckButton();
        List<string> imageAddress = new List<string>();
        int countDown = 0;

        public AdminHome()
        {
            InitializeComponent();
            loading.Visible = true;
        }

        private void Button(KryptonCheckButton button)
        {
            cb.Checked = false;
            cb = button;
        }
        private void AddCat()
        {
            string query = "Select * from categorytbl";
            using (SqlConnection con = DBconnect.GetConnection())
            {
                if (con == null)
                {
                    MessageBox.Show("Database connection is not established.");
                    return;
                }
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        CategoryPanel.Controls.Clear();

                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                /*Button b = new Button();
                                b.BackColor = Color.FromArgb(23, 23, 26);
                                b.Size = new Size(263, 53);
                                b.Text = row["catName"].ToString();
                                b.Padding = new Padding(19, -1, 0, -1);
                                b.Font = new Font(b.Font.FontFamily, 16);
                                b.ForeColor = Color.White;  */

                                KryptonCheckButton b = new KryptonCheckButton();
                                b.ButtonStyle = ButtonStyle.LowProfile;
                                b.Cursor = Cursors.Hand;
                                b.Size = new Size(263, 53);
                                b.Text = row["catName"].ToString();

                                b.OverrideDefault.Back.Color1 = Color.FromArgb(23, 23, 26);
                                b.OverrideDefault.Back.Color2 = Color.FromArgb(23, 23, 26);

                                b.StateCheckedNormal.Back.Color1 = Color.FromArgb(60, 115, 120, 255);
                                b.StateCheckedNormal.Back.Color2 = Color.FromArgb(60, 115, 120, 255);
                                b.StateCheckedNormal.Border.Color1 = Color.Transparent;
                                b.StateCheckedNormal.Border.Color2 = Color.Transparent;
                                b.StateCheckedNormal.Back.ColorStyle = PaletteColorStyle.Solid;
                                b.StateCheckedNormal.Content.AdjacentGap = 20;
                                b.StateCheckedNormal.Content.ShortText.Color1 = Color.FromArgb(115, 120, 255);
                                b.StateCheckedNormal.Content.ShortText.Color2 = Color.FromArgb(115, 120, 255);

                                b.StateCheckedPressed.Back.Color1 = Color.FromArgb(60, 115, 120, 255);
                                b.StateCheckedPressed.Back.Color2 = Color.FromArgb(60, 115, 120, 255);
                                b.StateCheckedPressed.Back.ColorStyle = PaletteColorStyle.Solid;
                                b.StateCheckedPressed.Border.Color1 = Color.Transparent;
                                b.StateCheckedPressed.Border.Color2 = Color.Transparent;
                                b.StateCheckedPressed.Content.AdjacentGap = 20;
                                b.StateCheckedPressed.Content.ShortText.Color1 = Color.FromArgb(115, 120, 255);
                                b.StateCheckedPressed.Content.ShortText.Color2 = Color.FromArgb(115, 120, 255);

                                b.StateCheckedTracking.Back.Color1 = Color.FromArgb(40, 115, 120, 255);
                                b.StateCheckedTracking.Back.Color2 = Color.FromArgb(40, 115, 120, 255);
                                b.StateCheckedTracking.Border.Color1 = Color.Transparent;
                                b.StateCheckedTracking.Border.Color2 = Color.Transparent;
                                b.StateCheckedTracking.Back.ColorStyle = PaletteColorStyle.Solid;
                                b.StateCheckedTracking.Content.AdjacentGap = 20;
                                b.StateCheckedTracking.Content.ShortText.Color1 = Color.FromArgb(115, 120, 255);
                                b.StateCheckedTracking.Content.ShortText.Color2 = Color.FromArgb(115, 120, 255);

                                b.StateNormal.Back.Color1 = Color.FromArgb(23, 23, 26);
                                b.StateNormal.Back.Color2 = Color.FromArgb(23, 23, 26);
                                b.StateNormal.Border.Color1 = Color.Transparent;
                                b.StateNormal.Border.Color2 = Color.Transparent;
                                b.StateNormal.Back.ColorStyle = PaletteColorStyle.Solid;
                                b.StateNormal.Content.AdjacentGap = 20;
                                b.StateNormal.Content.ShortText.Color1 = Color.White;
                                b.StateNormal.Content.ShortText.Color2 = Color.White;

                                b.StatePressed.Back.Color1 = Color.FromArgb(60, 115, 120, 255);
                                b.StatePressed.Back.Color2 = Color.FromArgb(60, 115, 120, 255);
                                b.StatePressed.Border.Color1 = Color.Transparent;
                                b.StatePressed.Border.Color2 = Color.Transparent;
                                b.StatePressed.Back.ColorStyle = PaletteColorStyle.Solid;
                                b.StatePressed.Content.AdjacentGap = 20;
                                b.StatePressed.Content.ShortText.Color1 = Color.FromArgb(115, 120, 255);
                                b.StatePressed.Content.ShortText.Color2 = Color.FromArgb(115, 120, 255);

                                b.StateTracking.Back.Color1 = Color.DimGray;
                                b.StateTracking.Back.Color2 = Color.DimGray;
                                b.StateTracking.Border.Color1 = Color.Transparent;
                                b.StateTracking.Border.Color2 = Color.Transparent;
                                b.StateTracking.Back.ColorStyle = PaletteColorStyle.Solid;
                                b.StateTracking.Content.AdjacentGap = 20;
                                b.StateTracking.Content.ShortText.Color1 = Color.White;
                                b.StateTracking.Content.ShortText.Color2 = Color.White;

                                b.Click += new EventHandler(_Click);
                                CategoryPanel.Controls.Add(b);
                            }
                        }
                    }
                }catch (Exception ex) { MessageBox.Show($"Error! {ex.Message}"); }
            }
        }
        private void _Click(object sender, EventArgs e)
        {
            KryptonCheckButton b = (KryptonCheckButton)sender;
            foreach (var item in listPanel.Controls)
            {
                var g = (GameDesc)item;
                g.Visible = g.Category.ToLower().Contains(b.Text.Trim().ToLower());
            }Button(b);
        }

        private void AddItems(string id, string name, string cat,string price, Image img)
        {
            var v = new GameDesc
            {
                GName = name,
                Price = price,
                Category = cat,
                Pic = img,
                id = Convert.ToInt32(id)
            };
            listPanel.Controls.Add(v);
        }
        private void LoadItems()
        {
            string query = "select * from gamestbl t1 inner join categorytbl t2 on t2.catID = t1.categoryID";
            using (SqlConnection con = DBconnect.GetConnection())
            {
                if (con == null)
                {
                    MessageBox.Show("Database connection is not established.");
                    return;
                }
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        imageAddress.Clear(); // reset address

                        foreach (DataRow item in dt.Rows)
                        {
                            byte[] imgAry = (byte[])item["gameImage"];
                            imageAddress.Add(Convert.ToBase64String(imgAry));
                            Image img = Image.FromStream(new MemoryStream(imgAry));

                            AddItems(item["gameID"].ToString(), item["gameName"].ToString(), item["catName"].ToString(), item["gamePrice"].ToString(), img);
                        }

                        countDown = 0; // Reset counter
                        slideImgTimer.Start(); // Start the timer after images are loaded
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error! {ex.Message}");
                }
            }

        }

        private void searchtxtbox_TextChanged(object sender, EventArgs e)
        {
            foreach (var item in listPanel.Controls)
            {
                var g = (GameDesc)item;
                g.Visible = g.GName.ToLower().Contains(searchtxtbox.Text.Trim().ToLower());
            }
        }

        private async void AdminHome_Click(object sender, EventArgs e)
        {
            loading.Visible = true;
            loadingtimer.Start();
            listPanel.Controls.Clear();
            await Task.Delay(1000);

            cb.Checked = false;
            LoadItems();
            AddCat();
            //laodingpanel.Visible = true;
            //timer.Start();
        }

        private void AdminHome_Load(object sender, EventArgs e)
        {
            //laodingpanel.Visible = false;
            loading.Visible = true;
            loadingtimer.Start();
            AddCat();
            listPanel.Controls.Clear();
            LoadItems();
        }

        private void loadingtimer_Tick(object sender, EventArgs e)
        {
            loadingtimer.Stop();
            loadingtimer.Interval = 1000;
            loading.Visible = false;
        }

        private void slideImageBox_Click(object sender, EventArgs e)
        {

        }
        private void slideImgTimer_Tick(object sender, EventArgs e)
        {
            if (countDown < imageAddress.Count)
            {
                byte[] imgBytes = Convert.FromBase64String(imageAddress[countDown]);
                using (MemoryStream ms = new MemoryStream(imgBytes))
                {
                    slideImageBox.Image = Image.FromStream(ms);
                }
                countDown++;
            }
            else
            {
                countDown = 0;
            }
        }
    }
}
