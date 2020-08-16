using System;
using System.IO;

namespace SilentHillToolkit
{
	class Program
	{
		#region Methods

		static void Main(string[] args)
		{
			Console.WriteLine("Silent Hill Toolkit by nikita600 (02.08.2020)");

			Console.WriteLine("Using: SHToolkit.exe BODYPROG.BIN");

			DecryptCodeFile("BODYPROG.BIN");
			DecryptCodeFile("B_KONAMI.BIN");

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

			var outputPath = "DEC_" + filePath;

			var fileBytes = File.ReadAllBytes(filePath);
			using (var encryptedReader = new BinaryReader(new MemoryStream(fileBytes)))
			{
				using (var decryptedStream = new MemoryStream())
				{
					using (var decryptedWriter = new BinaryWriter(decryptedStream))
					{
						var key = 0u;
						var size = fileBytes.Length / 4;
						for (var i = 0; i < size; ++i)
						{
							key = (key + 0x1309125) * 0x3a452f7;
							var data = encryptedReader.ReadUInt32();
							data = data ^ key;

							decryptedWriter.Write(data);
						}
					}

					var decryptedBytes = decryptedStream.ToArray();
					File.WriteAllBytes(outputPath, decryptedBytes);
				}
			}

			Console.WriteLine("File " + filePath + " decrypted to " + outputPath);
		}

		#endregion
	}
}
