using Application.Core;

namespace API.Services
{
    public class CryptographyService
    {
        private IConfiguration Configuration { get; }
        private const int KeySize = 2048;
        private readonly bool OAEPPAdding = false;

        public RSAParameters RSAParameters { get; private set; }

        public CryptographyService(IConfiguration configuration)
        {
            Configuration = configuration;
            BuildRsaAlgorithm(KeySize, OAEPPAdding);
        }

        private Cryptografy.RSA BuildRsaAlgorithm(int keySize, bool oAEEPPAdding)
        {
            RSAParameters = Cryptografy.RSA.CreateParametersAndKeys(KeySize);
            return new Cryptografy.RSA(KeySize, RSAParameters.PrivateParameters, OAEPPAdding);
        }

        public Cryptografy.RSA GetRSA(int keySize = KeySize, bool oAEEPPAdding = false) => new Cryptografy.RSA(keySize, RSAParameters.PrivateParameters, oAEEPPAdding);

        public string ComputeSHA256(byte[] buffer)
        {
            var hasher = new System.Security.Cryptography.SHA256Managed();
            return BitConverter.ToString(hasher.ComputeHash(buffer)).Replace("-", "");
        }
    }
}
