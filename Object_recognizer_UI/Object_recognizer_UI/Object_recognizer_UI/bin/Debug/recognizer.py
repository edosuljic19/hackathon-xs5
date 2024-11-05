import base64
import requests
import sys

def process_arguments(arg1, arg2, arg3):
    # Your logic here
    print(f"Argument 1: {arg1}")
    print(f"Argument 2: {arg2}")
    print(f"Argument 3: {arg3}")

if __name__ == "__main__":
    # Check if the correct number of arguments is provided
    if len(sys.argv) != 4:
        print("Usage: python script_name.py arg1 arg2")
        sys.exit(1)

    # Extract arguments from the command line
    arg1 = sys.argv[1]
    arg2 = sys.argv[2]
    arg3 = sys.argv[3]

    putanja_do_datoteke = arg2

    # Otvorite datoteku u načinu čitanja (read mode)
    with open(putanja_do_datoteke, 'r') as datoteka:
        sadrzaj = datoteka.read()

    # OpenAI API Key
    api_key = arg3 #"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"

    index_slk=arg1

    # Function to encode the image
    def encode_image(image_path):
      with open(image_path, "rb") as image_file:
        return base64.b64encode(image_file.read()).decode('utf-8')

    # Path to your image
    # image_path = "slika" + index_slk + ".jpg"
    image_path = arg1
    # Getting the base64 string
    base64_image = encode_image(image_path)

    headers = {
      "Content-Type": "application/json",
      "Authorization": f"Bearer {api_key}"
    }

    payload = {
      "model": "gpt-4-vision-preview",
      "messages": [
        {
          "role": "user",
          "content": [
            {
              "type": "text",
              "text": "Please say what word from the list describes the item in the picture. Sugested words are:" + sadrzaj + ". If an word is not in the list or is a composit of multiple words, return \"NA\" Please return the answer in one word."
            },
            {
              "type": "image_url",
              "image_url": {
                "url": f"data:image/jpeg;base64,{base64_image}"
              }
            }
          ]
        }
      ],
      "max_tokens": 300
    }

    response = requests.post("https://api.openai.com/v1/chat/completions", headers=headers, json=payload)

    main_word = response.json()['choices'][0]['message']['content']

    print(main_word)
