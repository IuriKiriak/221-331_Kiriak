using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;
using CryptoHandler;
using KeyHome.Models;

namespace KeyHome.Forms
{
    public partial class DataForm : Form
    {
        private string _decryptedText;
        private List<Credential> _credentials = new();

        public DataForm(string decryptedText)
        {
            InitializeComponent();
            _decryptedText = decryptedText;
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var credentialList = JsonSerializer.Deserialize<CredentialList>(_decryptedText);
                if (credentialList != null)
                {
                    _credentials = credentialList.Credentials;
                    dataGridView1.DataSource = _credentials;
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

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            {
                var cred = _credentials[e.RowIndex];
                ShowCredentialModal(cred);
            }
        }

        private void ShowCredentialModal(Credential cred)
        {
            string codeWord = PromptCodeWord();

            try
            {
                string login = System.Text.Encoding.UTF8.GetString(
                    CryptAES256OpenSSL.Decrypt(cred.Login, codeWord)
                );

                string password = System.Text.Encoding.UTF8.GetString(
                    CryptAES256OpenSSL.Decrypt(cred.Password, codeWord)
                );

                ShowDecryptedModal(cred.Url, login, password);
            }
            catch
            {
                MessageBox.Show("Неверная кодовая фраза или ошибка расшифровки");
            }
        }

        private string PromptCodeWord()
        {
            using (Form prompt = new Form())
            {
                prompt.Width = 300;
                prompt.Height = 150;
                prompt.Text = "Введите кодовую фразу";
                prompt.StartPosition = FormStartPosition.CenterParent;

                Label textLabel = new Label() { Left = 20, Top = 20, Text = "Кодовая фраза:" };
                TextBox inputBox = new TextBox() { Left = 20, Top = 50, Width = 240, UseSystemPasswordChar = true };
                Button confirmation = new Button() { Text = "OK", Left = 100, Width = 80, Top = 80 };
                confirmation.DialogResult = DialogResult.OK;

                prompt.Controls.Add(textLabel);
                prompt.Controls.Add(inputBox);
                prompt.Controls.Add(confirmation);
                prompt.AcceptButton = confirmation;

                return prompt.ShowDialog() == DialogResult.OK ? inputBox.Text : null;
            }
        }

        private void ShowDecryptedModal(string url, string login, string password)
        {
            using (Form modal = new Form())
            {
                modal.Text = "Данные учетной записи";
                modal.Width = 350;
                modal.Height = 250;
                modal.StartPosition = FormStartPosition.CenterParent;

                Label label = new Label
                {
                    Dock = DockStyle.Top,
                    Height = 100,
                    TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                    Text = $"URL: {url}\n\nLogin: {login}\n\nPassword: {password}"
                };

                Button copyLoginButton = new Button { Text = "Скопировать логин", Width = 150, Height = 30, Top = 120 };
                copyLoginButton.Left = (modal.ClientSize.Width - copyLoginButton.Width) / 2;
                copyLoginButton.Click += (s, e) => Clipboard.SetText(login);

                Button copyPasswordButton = new Button { Text = "Скопировать пароль", Width = 150, Height = 30, Top = 160 };
                copyPasswordButton.Left = (modal.ClientSize.Width - copyPasswordButton.Width) / 2;
                copyPasswordButton.Click += (s, e) => Clipboard.SetText(password);

                modal.Controls.Add(label);
                modal.Controls.Add(copyLoginButton);
                modal.Controls.Add(copyPasswordButton);

                modal.ShowDialog();
            }
        }

        private void SearchUrl(string searchText)
        {
            var filteredCredentials = string.IsNullOrWhiteSpace(searchText)
                ? _credentials
                : _credentials.Where(c => c.Url != null && c.Url.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0).ToList();

            dataGridView1.DataSource = filteredCredentials;
        }
    }
}