using System.IO;

namespace SilentHillToolkit.Assets
{
	public class OverlayCodeDecryptor
	{
		public static byte[] Decrypt(byte[] fileBytes)
		{
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

					return decryptedStream.ToArray();
				}
			}
		}
	}
}
