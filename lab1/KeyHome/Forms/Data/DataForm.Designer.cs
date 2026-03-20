using System.Windows.Forms;

namespace KeyHome.Forms
{
    partial class DataForm
    {
        private System.Windows.Forms.DataGridView dataGridView1;
        private TextBox _searchBox;

        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this._searchBox = new TextBox();
            
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();

            _searchBox.Dock = DockStyle.Top;
            _searchBox.Height = 30;
            _searchBox.PlaceholderText = "Введите URL";
            _searchBox.TextChanged += (s, e) =>
            {
                SearchUrl(_searchBox.Text);
            };


            dataGridView1.Dock = DockStyle.Fill; 
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Top = _searchBox.Bottom + 10;

            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "URL",
                DataPropertyName = "Url",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Login",
                DataPropertyName = "LoginMasked",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Password",
                DataPropertyName = "PasswordMasked",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            var buttonColumn = new DataGridViewButtonColumn
            {
                HeaderText = "Действие",
                Text = "Показать",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };
            dataGridView1.Columns.Add(buttonColumn);
            dataGridView1.CellClick += DataGridView1_CellClick;


            this.Controls.Add(this._searchBox); 
            this.Controls.Add(this.dataGridView1);  

            this.Text = "KeyHome - Учетные данные";
            this.Size = new System.Drawing.Size(500, 500);
            this.StartPosition = FormStartPosition.CenterScreen;

            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
        }
    }
}