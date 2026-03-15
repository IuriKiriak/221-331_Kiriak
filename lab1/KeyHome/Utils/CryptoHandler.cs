using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace CryptoHandler
{
    public static class CryptAES256OpenSSL
    {
        [DllImport("libcrypto-3-x64.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr EVP_CIPHER_CTX_new();

        [DllImport("libcrypto-3-x64.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void EVP_CIPHER_CTX_free(IntPtr ctx);

        [DllImport("libcrypto-3-x64.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr EVP_aes_256_cbc();

        [DllImport("libcrypto-3-x64.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int EVP_EncryptInit_ex(IntPtr ctx, IntPtr cipher, IntPtr engine, byte[] key, byte[] iv);

        [DllImport("libcrypto-3-x64.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int EVP_EncryptUpdate(IntPtr ctx, byte[] ciphertext, ref int outLen, byte[] plaintext, int plaintextLen);

        [DllImport("libcrypto-3-x64.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int EVP_EncryptFinal_ex(IntPtr ctx, byte[] ciphertext, ref int outLen);

        [DllImport("libcrypto-3-x64.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int EVP_DecryptInit_ex(IntPtr ctx, IntPtr cipher, IntPtr engine, byte[] key, byte[] iv);

        [DllImport("libcrypto-3-x64.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int EVP_DecryptUpdate(IntPtr ctx, byte[] plaintext, ref int outLen, byte[] ciphertext, int ciphertextLen);

        [DllImport("libcrypto-3-x64.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int EVP_DecryptFinal_ex(IntPtr ctx, byte[] plaintext, ref int outLen);

        [DllImport("libcrypto-3-x64.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int EVP_CIPHER_CTX_set_padding(IntPtr ctx, int pad);

        public static byte[] Encrypt(byte[] plaintext, byte[] key, out byte[] iv)
        {
            if (key.Length != 32) throw new ArgumentException("Ключ должен быть 32 байта");

            // Устанавливаем iv как массив из 16 нулевых байтов
            iv = new byte[16];  // Массив из 16 нулевых байтов

            IntPtr ctx = EVP_CIPHER_CTX_new();
            if (ctx == IntPtr.Zero) throw new Exception("Не удалось создать контекст OpenSSL");

            try
            {
                IntPtr cipher = EVP_aes_256_cbc();
                if (EVP_EncryptInit_ex(ctx, cipher, IntPtr.Zero, key, iv) != 1)
                    throw new Exception("Ошибка EVP_EncryptInit_ex");

                EVP_CIPHER_CTX_set_padding(ctx, 1);

                byte[] outBuf = new byte[plaintext.Length + 16];
                int outLen = 0;

                if (EVP_EncryptUpdate(ctx, outBuf, ref outLen, plaintext, plaintext.Length) != 1)
                    throw new Exception("Ошибка EVP_EncryptUpdate");

                int totalLen = outLen;

                int finalLen = 0;
                byte[] finalBuf = new byte[16];
                if (EVP_EncryptFinal_ex(ctx, finalBuf, ref finalLen) != 1)
                    throw new Exception("Ошибка EVP_EncryptFinal_ex");

                Array.Copy(finalBuf, 0, outBuf, totalLen, finalLen);
                totalLen += finalLen;

                byte[] ciphertext = new byte[totalLen];
                Array.Copy(outBuf, ciphertext, totalLen);

                return ciphertext;
            }
            finally
            {
                EVP_CIPHER_CTX_free(ctx);
            }
        }

        public static byte[] Decrypt(byte[] ciphertext, byte[] key, byte[] iv)
        {
            if (key.Length != 32) throw new ArgumentException("Ключ должен быть 32 байта");
            if (iv.Length != 16) throw new ArgumentException("IV должен быть 16 байт");

            IntPtr ctx = EVP_CIPHER_CTX_new();
            if (ctx == IntPtr.Zero) throw new Exception("Не удалось создать контекст OpenSSL");

            try
            {
                IntPtr cipher = EVP_aes_256_cbc();
                if (EVP_DecryptInit_ex(ctx, cipher, IntPtr.Zero, key, iv) != 1)
                    throw new Exception("Ошибка EVP_DecryptInit_ex");

                EVP_CIPHER_CTX_set_padding(ctx, 1);

                byte[] outBuf = new byte[ciphertext.Length];
                int outLen = 0;

                if (EVP_DecryptUpdate(ctx, outBuf, ref outLen, ciphertext, ciphertext.Length) != 1)
                    throw new Exception("Ошибка EVP_DecryptUpdate");

                int totalLen = outLen;

                int finalLen = 0;
                byte[] finalBuf = new byte[16];
                if (EVP_DecryptFinal_ex(ctx, finalBuf, ref finalLen) != 1)
                    throw new Exception("Ошибка EVP_DecryptFinal_ex (неверный ключ или IV)");

                Array.Copy(finalBuf, 0, outBuf, totalLen, finalLen);
                totalLen += finalLen;

                byte[] plaintext = new byte[totalLen];
                Array.Copy(outBuf, plaintext, totalLen);

                return plaintext;
            }
            finally
            {
                EVP_CIPHER_CTX_free(ctx);
            }
        }
    }
}