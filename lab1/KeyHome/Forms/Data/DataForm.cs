using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Windows.Forms;
using KeyHome.Models;

namespace KeyHome.Forms
{
    public partial class DataForm : Form
    {
        private string decryptedText_;

        public DataForm(string decryptedText)
        {
            InitializeComponent();
            decryptedText_ = decryptedText;
            Console.WriteLine($"Расшифрованные данные: {decryptedText}");

            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var credentialList = JsonSerializer.Deserialize<CredentialList>(decryptedText_);

                if (credentialList != null)
                {
                    dataGridView1.DataSource = credentialList.Credentials;
                }
                else
                {
                    MessageBox.Show("Не удалось десериализовать данные.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при десериализации данных: " + ex.Message);
            }
        }
    }
}