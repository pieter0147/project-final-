﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Final_Project_Vispro
{
    public partial class paketMobilKecil : Form
    {
        private MySqlConnection koneksi;
        private MySqlDataAdapter adapter;
        private MySqlCommand perintah;
        private DataSet ds = new DataSet();
        private string alamat, query;
        private int param;
        public paketMobilKecil(int id)
        {
            alamat = "server=localhost; database=db_carwashh; username=root; password=;";
            koneksi = new MySqlConnection(alamat);

            InitializeComponent();
            param =id;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TipeMobil tipemobil = new TipeMobil(param);
            tipemobil.Show();
            this.Hide();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLicensePlate.Text))
            {
                MessageBox.Show("Data Tidak lengkap !!");
                return;
            }

            // Pastikan harga disimpan sebagai integer tanpa pemisah ribuan
            int harga = 0;
            bool isValid = int.TryParse(txtCost.Text.Replace(",", "").Trim(), out harga);  // Menghapus koma jika ada dan mencoba parsing ke integer

            if (!isValid)
            {
                MessageBox.Show("Harga tidak valid!");
                return;
            }

            string query = "INSERT INTO tbl_history (date, license_plate, cost, package, tipe_kendaraan) VALUES (@date, @licensePlate, @cost, @package,@TIpeKendaraan);";

            try
            {
                using (var koneksi = new MySqlConnection(alamat))
                {
                    koneksi.Open(); // Buka koneksi
                    using (var perintah = new MySqlCommand(query, koneksi))
                    {
                        perintah.Parameters.AddWithValue("@date", txtDate.Value);
                        perintah.Parameters.AddWithValue("@licensePlate", txtLicensePlate.Text);
                        perintah.Parameters.AddWithValue("@cost", harga);  // Gunakan harga yang sudah diubah menjadi integer
                        perintah.Parameters.AddWithValue("@package", cbPaket.Text);
                        perintah.Parameters.AddWithValue("@TipeKendaraan", txtTipeKendaraan.Text);

                        int res = perintah.ExecuteNonQuery();
                        if (res == 1)
                        {
                            MessageBox.Show("Insert Data Suksess ...");
                            paketMobilKecil_Load(null, null);
                        }
                        else
                        {
                            MessageBox.Show("Gagal insert Data . . . ");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void txtTipeKendaraan_Click(object sender, EventArgs e)
        {
            
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            FrmStruk frmStruk = new FrmStruk();
            frmStruk.Show();
            this.Show();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            TipeMobil tipeMobil = new TipeMobil(param);
            tipeMobil.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void cbPaket_SelectedIndexChanged(object sender, EventArgs e)
        {
            {
                // Dictionary untuk memetakan paket ke harga
                var paketHarga = new Dictionary<string, int>
    {
        { "basic wash", 50000 },
        { "deep wash", 100000 },
        { "premium", 150000 }
    };

                // Ambil nama paket yang dipilih dari ComboBox
                string selectedPackage = cbPaket.SelectedItem.ToString();

                // Cek apakah paket yang dipilih ada dalam dictionary
                if (paketHarga.ContainsKey(selectedPackage))
                {
                    // Ambil harga dari dictionary dan tampilkan di TextBox txtCost
                    txtCost.Text = paketHarga[selectedPackage].ToString("N0"); // Format harga dengan ribuan
                }
                else
                {
                    txtCost.Clear(); // Jika tidak ada, kosongkan text box
                    MessageBox.Show("Paket yang dipilih tidak ditemukan.");
                }
            }
        }

        private void paketMobilKecil_Load(object sender, EventArgs e)
        {

        }
    }
}