import os
import sys
import re
from pathlib import Path

# Get directory path from command line or use current directory
directory = sys.argv[1] if len(sys.argv) > 1 else os.getcwd()

if not os.path.isdir(directory):
    print(f"Directory not found: {directory}")
    sys.exit(1)

# Define file extensions to search for
extensions = [".ovl", ".hnd"]

lib_to_find = [
    "libxdoc",
    "libaccount",
    "librefinfo",
    "libmessages",
    "liboption",
    "libgenuid",
    "libatools",
    "libisocket",
    "libqoom",
    "libappa",
    "libquest",
    "libfury",
    "libcoom",
    "libboom",
    "libtypes",
]

# Get all files with specified extensions
files = []
for ext in extensions:
    pattern = f"*{ext}"
    for file in Path(directory).glob(pattern):
        files.append(str(file))

files.sort()

if not files:
    print("No files found with extensions: .ovl, .hnd")
    sys.exit(0)

# Process and display results
for file in files:
    file_name = Path(file).stem
    
    try:
        # Read file content
        with open(file, 'rb') as f:
            file_content = f.read().decode('utf-8', errors='ignore')
        
        # Extract last 9 symbols from file content
        version = file_content[-9:] if len(file_content) >= 9 else file_content
        
        print(f"{file_name} - {version}")
        
        # Find required libraries and their versions in file content
        for lib in lib_to_find:
            lib_index = file_content.lower().find(lib.lower())
            if lib_index >= 0:
                # Extract numbers following the library name
                start_index = lib_index + len(lib)
                remaining_content = file_content[start_index:]
                
                # Extract consecutive digits
                numbers = re.match(r'\d+', remaining_content)
                if numbers:
                    print(f"  required {lib}: {numbers.group()}")
    
    except Exception as ex:
        print(f"{file_name} - Error reading file: {str(ex)}")
