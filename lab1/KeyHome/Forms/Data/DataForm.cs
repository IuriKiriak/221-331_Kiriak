using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Models;
using JsonHandler;

namespace KeyHome.Forms
{
    public partial class DataForm : Form
    {
        // private List<Credential> _credentials = new List<Credential>();
        private string decriptedText_;
        public DataForm(string decriptedText)
        {
            InitializeComponent();
            LoadData();
            decriptedText_ = decriptedText;
            Console.WriteLine($"расшифрованные данные: {decriptedText}");
        }

        private void LoadData()
        {
            // Загружаем JSON
            string jsonPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Data", "credentials.json");
            // var handler = new JsonHandler(jsonPath);
            // _credentials = handler.LoadCredentials();

            // Устанавливаем источник данных для таблицы
            // dataGridView1.DataSource = _credentials;

            // Маскировка пароля — этот код нужно добавить **после** dataGridView1.DataSource
        }

    }
}
