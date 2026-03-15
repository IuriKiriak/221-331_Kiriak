namespace KeyHome;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
        LoginForm loginForm = new LoginForm();
        loginForm.Show();
    }
}
