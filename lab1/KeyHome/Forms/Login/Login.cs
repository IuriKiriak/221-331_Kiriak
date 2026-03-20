using System;
using System.Windows.Forms;
using System.Text;

using JsonHandler;
using CryptoHandler;
using HashUtils;
using KeyHome.Forms;


namespace KeyHome;


public class LoginForm : Form
{
    private TextBox codeTextBox;
    private Button submitButton;

    public LoginForm()
    {
        this.Text = "Введите кодовую фразу для расшифрования файл";
        this.Size = new System.Drawing.Size(300, 200);

        codeTextBox = new TextBox();
        codeTextBox.Location = new System.Drawing.Point(50, 30);
        codeTextBox.Size = new System.Drawing.Size(200, 20);
        codeTextBox.UseSystemPasswordChar = true;
        this.Controls.Add(codeTextBox);

        submitButton = new Button();
        submitButton.Text = "Подтвердить";
        submitButton.Location = new System.Drawing.Point(100, 70);
        submitButton.Click += submitButtonClick_;
        this.Controls.Add(submitButton);
    }

    private void submitButtonClick_(object? sender, EventArgs e)
    {
        string codeWord = codeTextBox.Text;
        if (string.IsNullOrEmpty(codeWord))
        {
            MessageBox.Show("Введите кодовую фразу!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        var fileHandler = new CredentialJsonHandler();
        var fileCredential = fileHandler.File;
        string encryptedText = fileCredential.FileReader.FileRead();
        byte[] decrypted = new byte[16];
        try
        {
            decrypted = CryptAES256OpenSSL.Decrypt(encryptedText, codeWord);
            string decryptedText = Encoding.UTF8.GetString(decrypted);
            
            
            DataForm dataForm = new DataForm(decryptedText);

            dataForm.FormClosed += (s, e) =>
            {
              this.Show(); 
            };

            dataForm.Show();
            this.Hide();
        }
        catch
        {
            MessageBox.Show("Коддовая фраза введена неправильно!");
        }
    }
}