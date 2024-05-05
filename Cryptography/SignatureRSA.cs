using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace WebApiRis.Cryptography
{
	static class SignatureRSA
	{
		/// <summary>
		/// Проверка подписи исключения.
		/// </summary>
		/// <param name="message">Сообщение исключения.</param>
		/// <param name="signedHash">Подписанный хэш.</param>
		/// <param name="parameters">Параметры RSA.</param>
		/// <returns>True если подпись действительна.</returns>
		public static bool VerifySignature(string message, byte[] signedHash, string param)
		{
			var parametrs = JsonConvert.DeserializeObject<RSAParameters>(param);
			using SHA256 alg = SHA256.Create();
			byte[] data = Encoding.ASCII.GetBytes(message);
			byte[] hash = alg.ComputeHash(data);
			using (RSA rsa = RSA.Create())
			{
				rsa.ImportParameters(parametrs);

				RSAPKCS1SignatureDeformatter rsaDeformatter = new(rsa);
				rsaDeformatter.SetHashAlgorithm(nameof(SHA256));

				if (rsaDeformatter.VerifySignature(hash, signedHash))
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}
	}
}
