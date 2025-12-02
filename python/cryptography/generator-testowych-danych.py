import os

def generate_test_data(file_sizes):
    for file_size in file_sizes:
        size_in_bytes = file_size * 1024 * 1024
        with open(f'plaintext_{file_size}MB.txt', 'wb') as f:
            f.write(os.urandom(size_in_bytes))

file_sizes = [100, 200, 300]

generate_test_data(file_sizes)
