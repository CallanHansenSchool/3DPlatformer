using UnityEngine;
using TMPro;

public class CaesarCipher : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI EncryptedSentence;
    [SerializeField] private TextMeshProUGUI DecryptedSentence;
    [SerializeField] private TextMeshProUGUI EncryptionAmount;

    public char Cipher(char _ch, int _encryptionAmount) // Return the enciphered character
    {
        if (!char.IsLetter(_ch))
        {
            return _ch;
        }

        char d = char.IsUpper(_ch) ? 'A' : 'a';
        return (char)(((_ch + _encryptionAmount - d) % 26) + d);
    }

    public string Encipher(string _input, int _encryptionAmount) // Encrypt the sentence using Caesar Cipher
    {
        string output = "";

        foreach (char ch in _input)
            output += Cipher(ch, _encryptionAmount); // Add the ciphered characters into the output string

        return output; // Return the encrypted 
    }

    public string Decipher(string input, int _encryptionAmount) // Decipher the encrypted sentence
    {
        return Encipher(input, 26 - _encryptionAmount); 
    }

    public void UpdateCipher()
    {
        string cipherText = Encipher(GameManager.Instance.WantedWord, GameManager.Instance.EncryptionAmount);
        EncryptedSentence.text = cipherText;
        GameManager.Instance.EncryptedSentence = cipherText;
        EncryptionAmount.text = GameManager.Instance.EncryptionAmount.ToString();

        //string decipheredSentence = Decipher(cipherText, GameManager.Instance.EncryptionAmount);
        DecryptedSentence.text = GameManager.Instance.CurrentSentence;
    }
}