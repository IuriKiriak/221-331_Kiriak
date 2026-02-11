using System;
using System.Collections.Generic;
using System.Windows.Forms;
using KeyHome.Handlers;
using KeyHome.Models;

namespace KeyHome.Forms
{
    public partial class DataForm : Form
    {
        private List<Credential> _credentials = new List<Credential>();

        public DataForm()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            // Загружаем JSON
            string jsonPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Data", "credentials.json");
            var handler = new JsonHandler(jsonPath);
            _credentials = handler.LoadCredentials();

            // Устанавливаем источник данных для таблицы
            dataGridView1.DataSource = _credentials;

            // Маскировка пароля — этот код нужно добавить **после** dataGridView1.DataSource
        }

    }
}
