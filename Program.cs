using System;
using System.Text;

public class Solution
{
    public static void Main(string[] args)
    {
        //Declare contents
        int seed = 40;
        int m = 307 * 491; 
        int num_bits = 96;
        string key = blum_blum_shub(seed, m, num_bits);
        
        // Ensure the ciphertext is correctly formatted (remove any accidental spaces)
        string ciphertext = "011011011111101001010001000011101011010000001000000000110011110101000110001101010001110100100011";
        ciphertext = ciphertext.Replace(" ", ""); // Remove any spaces if present
        Console.WriteLine($"ciphertext length:\t{ciphertext.Length} ");
        //Mostly for debugging
        Console.WriteLine($"key length:\t\t{key.Length}");
        Console.WriteLine("ciphertext:\t\t"+ ciphertext);
        Console.WriteLine("key:\t\t\t"+key);

        xor_ciphertext_with_key(ciphertext, key);
    }

    public static string blum_blum_shub(int seed, int m, int num_bits)
    {
        //Will be needed, the 64-bit integer. (python does this by default :/)
        long x0 = seed; 
        StringBuilder bitStream = new StringBuilder();
        for (int i = 0; i < num_bits; i++)
        {
            Console.Write($"On the {i}th iteration the equation x0 = x0^2 % m = {x0} * {x0} % {m} = ");
            x0 = (x0 * x0) % m; //Upon going through the debugger it was found that sth like (40*96) % m 
            Console.WriteLine(x0);
            /*
            Modular addition:
            */
            bitStream.Append(x0 % 2); //Parity or LSB
        }
        return bitStream.ToString();
    }

    public static void xor_ciphertext_with_key(string ciphertext, string key)
    {
        StringBuilder plaintext = new StringBuilder();

        // Ensure the key length matches the ciphertext
        if (key.Length != ciphertext.Length)
        {
            Console.WriteLine("Error: Key length and ciphertext length do not match.");
            return;
        }

        for (int i = 0; i < ciphertext.Length; i += 8)
        {
            // Check if there are exactly 8 bits for each chunk
            if (i + 8 > ciphertext.Length || i + 8 > key.Length)
            {
                Console.WriteLine("Error: Chunk length is less than 8 bits.");
                break;
            }

            // Get 8-bit chunks of ciphertext and key
            string cipher_chunk = ciphertext.Substring(i, 8);
            string key_chunk = key.Substring(i, 8);

            // Check if the chunks contain only valid binary characters (0 and 1)
            if (!IsValidBinary(cipher_chunk) || !IsValidBinary(key_chunk))
            {
                Console.WriteLine($"Error: Invalid binary string in chunk at index {i}");
                break;
            }

            int cipher_int = Convert.ToInt32(cipher_chunk, 2);
            int key_int = Convert.ToInt32(key_chunk, 2);

            int xor_result = cipher_int ^ key_int;

            // Convert the XOR result to a character and append it to the plaintext
            char decrypted_char = (char)xor_result;
            plaintext.Append(decrypted_char);
        }

        Console.WriteLine("Decrypted Plaintext: " + plaintext.ToString());
    }

    // Helper function to check if a string is a valid binary string
    public static bool IsValidBinary(string binaryString)
    {
        foreach (char c in binaryString)
        {
            if (c != '0' && c != '1')
                return false;
        }
        return true;
    }
}
