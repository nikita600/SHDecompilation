using System;
using System.IO;
using SilentHillToolkit.Assets;

namespace SilentHillToolkit
{
	class Program
	{
		#region Methods

		static void Main(string[] args)
		{
			Console.WriteLine("Silent Hill Toolkit by nikita600 (02.08.2020)");

			//Console.WriteLine("Using: SHToolkit.exe BODYPROG.BIN");
			//DecryptCodeFile("BODYPROG.BIN");
			//DecryptCodeFile("B_KONAMI.BIN");

			Console.WriteLine("Press any key to exit...");
			Console.ReadKey();
		}

		private static void DecryptCodeFile(string filePath)
		{
			if (!File.Exists(filePath))
			{
				Console.WriteLine("File " + filePath + " not found.");
				return;
			}

			var fileBytes = File.ReadAllBytes(filePath);

			var decryptedBytes = OverlayCodeDecryptor.Decrypt(fileBytes);

			var outputPath = "DEC_" + filePath;
			File.WriteAllBytes(outputPath, decryptedBytes);
			Console.WriteLine("File " + filePath + " decrypted to " + outputPath);
		}

		#endregion
	}
}
