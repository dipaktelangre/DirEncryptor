using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using CommandLine.Text;

namespace DirEncryptor
{
  public class CommandLineOptions
  {
    [Option('s', "source", Required = true, HelpText = "Source path, directory for encryption and file for decryption")]
    public string Source { get; set; }

    [Option('d', "destination", Required = true, HelpText = "Destination path")]
    public string Destination { get; set; }

    [Option('t', "type", HelpText = "Encrypt/Decrypt")]
    public EncryptorType Type { get; set; }

    [Option('v', "verbose", HelpText = "Verbose log")]
    public bool Verbose { get; set; }

    [Option('p', "password", Required = true, HelpText = "Password for Encryption/Decryption")]
    public string Password { get; set; }

    [HelpOption]
    public string GetUsage()
    {
      var help = new HelpText
      {
        Heading = new HeadingInfo("Dir Encryptor", "1.0.0.0"),
        Copyright = new CopyrightInfo("Dipak Telangre", 2018),
        AdditionalNewLineAfterOption = true,
        AddDashesToOption = true
      };
      help.AddPreOptionsLine("Free to use");
      help.AddPreOptionsLine("Usage: direncryptor -t encrypt -s sourcedir -d destinationdir");
      help.AddOptions(this);
      return help;
    }

  }

  public enum EncryptorType
  {
    Encrypt,
    Decrypt
  }
}
