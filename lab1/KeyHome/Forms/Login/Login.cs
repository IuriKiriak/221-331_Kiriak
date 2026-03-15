using System;
using System.Windows.Forms;
using System.Text;

using JsonHandler;
using CryptoHandler;
using HashUtils;


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

        // Преобразуем строку Base64 обратно в байты
        byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
        byte[] key = HashSHA256.CreateHash(codeWord);

        // Генерация IV (например, все нули)
        byte[] iv = new byte[16];  // Массив из 16 нулевых байтов

        Console.WriteLine("IV перед расшифровкой: " + BitConverter.ToString(iv));

        // Расшифровка данных
        byte[] decrypted = CryptAES256OpenSSL.Decrypt(encryptedBytes, key, iv);

        string decryptedText = Encoding.UTF8.GetString(decrypted);
        Console.WriteLine("Расшифрованный текст: " + decryptedText);
        if (string.IsNullOrEmpty(codeWord))
        {
            MessageBox.Show("Введите кодовую фразу!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        else
        {
            MessageBox.Show($"Кодовая фраза: ", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}