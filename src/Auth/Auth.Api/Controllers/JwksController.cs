using System.Security.Cryptography;

using Auth.Application.Abstractions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Api.Controllers;

[Route(".well-known")]
[ApiController]
public class JwksController(IApplicationDbContext context) : ControllerBase
{
    [HttpGet]
    public IActionResult GetJwks()
    {
        var keys = context.SigningKeys
            .Where(k => k.IsActive)
            .ToList();

        var jwks = new
        {
            keys = keys.Select(k => new
            {
                kty = "RSA",
                use = "sig",
                kid = k.KeyId,
                alg = "RS256",
                n = Base64UrlEncoder.Encode(GetModulus(k.PublicKey.Value)),
                e = Base64UrlEncoder.Encode(GetExponent(k.PublicKey.Value))
            })
        };

        return Ok(jwks);
    }
    
    private static byte[] GetModulus(string publicKey)
    {
        using var rsa = RSA.Create();
        
        rsa.ImportRSAPublicKey(Convert.FromBase64String(publicKey), out _);

        var parameters = rsa.ExportParameters(false);

        return parameters.Modulus
               ?? throw new InvalidOperationException("RSA Parameters are not valid.");
    }

    private static byte[] GetExponent(string publicKey)
    {
        using var rsa = RSA.Create();
        
        rsa.ImportRSAPublicKey(Convert.FromBase64String(publicKey), out _);

        var parameters = rsa.ExportParameters(false);

        return parameters.Exponent
               ?? throw new InvalidOperationException("RSA parameters are not valid.");
    }
}