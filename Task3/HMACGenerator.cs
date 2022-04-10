using System.Security.Cryptography;
using System.Text;

namespace Task3;


//Use https://dinochiesa.github.io/hmachash/index.html to calculate HMAC with sha-256
public class HMACGenerator {
    public string HMAC { get; }
    public string HMACKey { get; private set; }
    public int Move { get; }

    public HMACGenerator(string[] moves) {
        var k = CreateHMACKey();
        Move = GenerateRandomMove(moves);
        using(HMACSHA256 sha2 = new HMACSHA256(k)) {
            var h = sha2.ComputeHash(Encoding.ASCII.GetBytes(moves[Move - 1]));
            HMAC = Convert.ToHexString(h);
        }
    }

    private int GenerateRandomMove(string[] moves) {
        var movesCount = moves.Length;
        var random = new Random(BitConverter.ToInt32(Convert.FromHexString(HMACKey), 0));
        return random.Next(0, moves.Length) + 1;
    }

    private byte[] GenerateHMACKey() {
        RandomNumberGenerator rng = RandomNumberGenerator.Create();
        byte[] tokenBytes = new byte[32];
        rng.GetBytes(tokenBytes);
        return tokenBytes;
    }

    private byte[] CreateHMACKey() {
        var k = GenerateHMACKey();
        HMACKey = Convert.ToHexString(k);
        return k;
    }

}

