def blum_blum_shub(seed, m, num_bits):
    """Generates a pseudorandom bit sequence using the Blum Blum Shub generator."""
    x = seed
    bitstream = []

    for _ in range(num_bits):
        x = (x * x) % m
        bitstream.append(x % 2)  # Store the least significant bit

    return bitstream

def bits_to_string(bitstream):
    """Converts a list of bits to a string of bits (not ASCII characters)."""
    return ''.join(map(str, bitstream))

def xor_ciphertext_with_key(ciphertext, key):
    """Decrypts the ciphertext by XORing each 8-bit chunk with the key."""
    plaintext = []
    for i in range(0, len(ciphertext), 8):
        cipher_chunk = ciphertext[i:i+8]
        key_chunk = key[i:i+8]
        
        # Convert binary strings to integers for XOR operation
        cipher_int = int(cipher_chunk, 2)
        key_int = int(key_chunk, 2)
        
        # XOR the two values
        xor_result = cipher_int ^ key_int
        
        # Convert the XOR result back to an ASCII character
        decrypted_char = chr(xor_result)
        plaintext.append(decrypted_char)

    return ''.join(plaintext)

# Constants
p = 307
q = 491
m = p * q  # m = 307 * 491
x0 = 40    # Initial seed
num_bits = 96  # We need the first 96 bits

# Step 1: Generate the first 96 bits using Blum Blum Shub
bitstream = blum_blum_shub(x0, m, num_bits)

# Step 2: Convert the bitstream to a binary string (not ASCII characters)
key = bits_to_string(bitstream)

# Ciphertext provided in binary form
ciphertext = "011011011111101001010001000011101011010000001000000000110011110101000110001101010001110100100011"

# Step 3: Decrypt the ciphertext using XOR with the generated key
plaintext = xor_ciphertext_with_key(ciphertext, key)

# Output results
print("Generated Key (96 bits):", key)
print("Decrypted Plaintext Message:", plaintext)
