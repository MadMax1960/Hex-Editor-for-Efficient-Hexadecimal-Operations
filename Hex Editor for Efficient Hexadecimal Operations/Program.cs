using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CPKModifier
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Length < 1 || args.Length > 2)
			{
				Console.WriteLine("Usage: Hex Editor for Efficient Hexadecimal Operations.exe [filepath] [json name]");
				Console.WriteLine("You can drag and drop a file or folder onto the exe as well; it will use default.json for replacement if you do.");
				return;
			}

			string folderPath = args[0];
			string jsonFileName = args.Length > 1 ? args[1] : "default.json"; // Default JSON file name

			if (args.Length > 0)
			{
				folderPath = args[0];

				// Check if a JSON file argument was provided
				if (args.Length > 1)
				{
					jsonFileName = args[1];
				}
			}
			else
			{
				Console.Write("Enter the folder path: ");
				folderPath = Console.ReadLine();
			}

			if (!Directory.Exists(folderPath))
			{
				Console.WriteLine("The provided path is not a valid folder.");
				return;
			}

			// Construct the correct JSON file path using AppDomain.CurrentDomain.BaseDirectory
			string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, jsonFileName);

			if (!File.Exists(jsonFilePath))
			{
				Console.WriteLine($"JSON file '{jsonFileName}' not found. Creating a template file.");

				// Create a template JSON with an error message
				var templateReplacements = new Replacements
				{
					ReplacementsList = new List<Replacement>
					{
						new Replacement
						{
							SearchHex = "Please edit default.json",
							ReplaceHex = "Please add your replacements here"
						}
					}
				};

				// Serialize and write the template JSON to a file
				File.WriteAllText(jsonFilePath, JsonConvert.SerializeObject(templateReplacements, Formatting.Indented));

				Console.WriteLine($"Template JSON file '{jsonFileName}' created. Please edit this file with your replacements.");
				return;
			}

			// Deserialize the JSON data
			var replacements = JsonConvert.DeserializeObject<Replacements>(File.ReadAllText(jsonFilePath));

			foreach (string filePath in Directory.GetFiles(folderPath))
			{
				byte[] byteArray = File.ReadAllBytes(filePath);

				// Convert the byte array to a hex string without spaces or hyphens
				string byteString = BitConverter.ToString(byteArray).Replace("-", "");

				// Remove spaces from the byteString
				byteString = byteString.Replace(" ", "");

				// Initialize the starting position
				int position = 0;

				// Apply the replacements from the JSON file
				foreach (var replacement in replacements.ReplacementsList)
				{
					string searchHexWithoutSpaces = replacement.SearchHex.Replace(" ", "");
					string replaceHexWithoutSpaces = replacement.ReplaceHex.Replace(" ", "");

					// Continue until there are no more occurrences of searchHexWithoutSpaces
					while (position < byteString.Length)
					{
						int index = byteString.IndexOf(searchHexWithoutSpaces, position);

						if (index == -1)
						{
							break; // No more occurrences found
						}

						// Replace the matched segment with replaceHexWithoutSpaces
						byteString = byteString.Remove(index, searchHexWithoutSpaces.Length).Insert(index, replaceHexWithoutSpaces);

						// Update the position to continue searching
						position = index + replaceHexWithoutSpaces.Length;
					}
				}

				// Convert the modified hex string back to a byte array
				byte[] newByteArray = new byte[byteString.Length / 2];
				for (int i = 0; i < byteString.Length; i += 2)
				{
					newByteArray[i / 2] = Convert.ToByte(byteString.Substring(i, 2), 16);
				}

				File.WriteAllBytes(filePath, newByteArray);

				Console.WriteLine($"Processed: {Path.GetFileName(filePath)}");
			}

			Console.WriteLine("Your hees have been hoed.");
		}
	}

	// Define a class to hold the JSON data structure
	class Replacements
	{
		public List<Replacement> ReplacementsList { get; set; }
	}

	// Define a class to represent a replacement entry in the JSON file
	class Replacement
	{
		public string SearchHex { get; set; }
		public string ReplaceHex { get; set; }
	}
}
