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
        // базовые настройки формы
        this.Text = "Введите кодовую фразу для расшифрования файл";
        this.Size = new System.Drawing.Size(300, 200);

        // настройки для ввода фразы
        codeTextBox = new TextBox();
        codeTextBox.Location = new System.Drawing.Point(50, 30);
        codeTextBox.Size = new System.Drawing.Size(200, 20);
        this.Controls.Add(codeTextBox);

        // кнопка для подтверждения ввода
        submitButton = new Button();
        submitButton.Text = "Подтвердить";
        submitButton.Location = new System.Drawing.Point(100, 70);
        submitButton.Click += submitButtonClick_;
        this.Controls.Add(submitButton);
    }

    private void submitButtonClick_(object sender, EventArgs e)
    {
        string codeWord = codeTextBox.Text;

        var fileHandler = new CredentialJsonHandler();
        var fileCredential = fileHandler.File;
        string encryptedText = fileCredential.FileReader.FileRead();
        Console.WriteLine($"Зашифрованные данные из файла: {encryptedText}");
        Console.WriteLine("\n\n");

        // Расшифровка данных
        byte[] decrypted = CryptAES256OpenSSL.Decrypt(encryptedText, codeWord);

        string decryptedText = Encoding.UTF8.GetString(decrypted);
        Console.WriteLine($"Расшифрованные данные из файла: {decryptedText}");
        if (string.IsNullOrEmpty(codeWord))
        {
            MessageBox.Show("Введите кодовую фразу!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        else
        {
            MessageBox.Show($"Кодовая фраза: ", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

            DataForm dataForm = new DataForm(decryptedText);
            dataForm.Show();
            this.Close();
        }
    }
}