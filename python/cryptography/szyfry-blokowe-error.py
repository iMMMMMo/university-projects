from Crypto.Cipher import AES
from Crypto.Util.Padding import pad, unpad
from Crypto.Random import get_random_bytes

plaintext = "Politchnika Poznanska to bardzo dobra uczelnia."

key = get_random_bytes(16)

modes = ['ECB', 'CBC', 'OFB', 'CFB', 'CTR']
cipher_texts = {}

for mode in modes:
    if mode == 'ECB':
        cipher = AES.new(key, AES.MODE_ECB)
    elif mode == 'CBC':
        cipher = AES.new(key, AES.MODE_CBC)
    elif mode == 'OFB':
        cipher = AES.new(key, AES.MODE_OFB)
    elif mode == 'CFB':
        cipher = AES.new(key, AES.MODE_CFB)
    elif mode == 'CTR':
        cipher = AES.new(key, AES.MODE_CTR)
    
    padded_plaintext = pad(plaintext.encode(), AES.block_size)
    cipher_text = cipher.encrypt(padded_plaintext)
    cipher_texts[mode] = cipher_text

for mode in cipher_texts:
    cipher_texts[mode] = bytearray(cipher_texts[mode])
    cipher_texts[mode][0] = cipher_texts[mode][0] ^ 1

for mode in modes:
    if mode == 'ECB':
        cipher = AES.new(key, AES.MODE_ECB)
    elif mode == 'CBC':
        cipher = AES.new(key, AES.MODE_CBC)
    elif mode == 'OFB':
        cipher = AES.new(key, AES.MODE_OFB)
    elif mode == 'CFB':
        cipher = AES.new(key, AES.MODE_CFB)
    elif mode == 'CTR':
        cipher = AES.new(key, AES.MODE_CTR)
    
    decrypted_text = cipher.decrypt(cipher_texts[mode])
    try:
        decrypted_text = unpad(decrypted_text, AES.block_size).decode(errors='ignore')
    except ValueError:
        decrypted_text = decrypted_text.decode(errors='replace')
    
    print(f"Tryb szyfrowania: {mode}")
    print("Odszyfrowany tekst:")
    print(decrypted_text)
    print()
