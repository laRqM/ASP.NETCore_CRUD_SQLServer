
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace Ejercicio3;

internal class Encriptacion {
    private readonly string _claveEncriptacion = "2z\\6;!Q5<iec;;,/";

    public string Encriptar(string plainText) {
        using (Aes algoritmoAes = Aes.Create()) {
            algoritmoAes.Key = Encoding.UTF8.GetBytes(_claveEncriptacion);
            algoritmoAes.IV = new byte[algoritmoAes.BlockSize / 8];

            ICryptoTransform encriptador = algoritmoAes.CreateEncryptor(algoritmoAes.Key, algoritmoAes.IV);

            using (MemoryStream msEncrypt = new MemoryStream()) {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encriptador, CryptoStreamMode.Write)) {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt)) {
                        swEncrypt.Write(plainText);
                    }
                }
                return Convert.ToBase64String(msEncrypt.ToArray());
            }
        }
    }

    public string Desencriptar(string? cipherText) {
        using (Aes algoritmoAes = Aes.Create()) {
            algoritmoAes.Key = Encoding.UTF8.GetBytes(_claveEncriptacion);
            algoritmoAes.IV = new byte[algoritmoAes.BlockSize / 8];

            ICryptoTransform decryptor = algoritmoAes.CreateDecryptor(algoritmoAes.Key, algoritmoAes.IV);

            using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText))) {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read)) {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt)) {
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
        }
    }
}