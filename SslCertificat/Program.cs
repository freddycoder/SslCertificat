// This is a program that take a certificate with the .pfx extension and convert it to .cer and .ket files.
// The .pfx file is the certificate that you want to convert.
// The .cer file is the certificate that you can use in your application.
// The .key file is the private key that you can use in your application.
using System.Security.Cryptography.X509Certificates;

namespace SslCertificat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Read the command line arguments.
            var pfxFile = args.LastOrDefault();

            if (string.IsNullOrEmpty(pfxFile))
            {
                Console.WriteLine("Usage: SslCertificat.exe <pfxFile>");
                return;
            }

            X509Certificate2 cert = new X509Certificate2(pfxFile, AskForPasswordSecurely());

            // Export the certificate to a .cer file.
            cert.Export(X509ContentType.Cert, Path.ChangeExtension(pfxFile, "crt"));

            // Export the certificate to a .key file.
            cert.Export(X509ContentType.Pkcs12, Path.ChangeExtension(pfxFile, "key"));

            Console.WriteLine("Done.");
        }

        private static string AskForPasswordSecurely()
        {
            Console.Write("Enter the password: ");
            
            // Don't print what the user types.
            // the number 8192 is the maximum number of characters that can be read.
            Console.SetIn(new StreamReader(Console.OpenStandardInput(8192)));

            // Read the password.
            string? password = Console.ReadLine();

            if (string.IsNullOrEmpty(password))
            {
                throw new InvalidOperationException("Password is required.");
            }

            // Print a new line.
            Console.WriteLine();

            // Set the console back to normal.
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));

            return password;
        }
    }
}