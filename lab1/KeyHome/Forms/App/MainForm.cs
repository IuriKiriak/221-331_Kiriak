namespace KeyHome;

public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
        LoginForm loginForm = new LoginForm();
        loginForm.ShowDialog();
    }
}
