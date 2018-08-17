using System;
using System.IO;
using CommandLine;

namespace DirEncryptor
{
  class Program
  {
    // Rfc2898DeriveBytes constants:

    static void Main(string[] args)
    {
      var options = new CommandLineOptions();

      if (Parser.Default.ParseArguments(args, options))
      {
        var logger = new CustomLogger(options.Verbose);

        var source = options.Source;
        
        if (options.Type.Equals(EncryptorType.Encrypt))
        {
          try
          {
            if (!new DirectoryInfo(source).Exists)
            {
              logger.Log(string.Format("Source path not found : {0}", source));
              return;
            }
            logger.Log(string.Format("Starting Encryption for the folder : {0}", options.Source));
            logger.Log("Creating zip folder");
            var zipFile = DirEncryptor.ZipFolder(source, source + ".zip");
            logger.Log(string.Format("Created zip : {0}", zipFile));
            logger.Log("Starting encryption ...");
            DirEncryptor.EncryptFile(zipFile, options.Destination, options.Password, DirEncryptor.salt,
            DirEncryptor.iterations);
            logger.Log("Encrypted !");
            logger.Log("Clearing the files ");
            File.Delete(zipFile);
            logger.Log("All done !!");
          }
          catch (Exception e)
          {
            logger.Log(string.Format("Something went wrong : {0}", e.Message));
          }


        }
        else if (options.Type.Equals(EncryptorType.Decrypt))
        {
          logger.Log(string.Format("Starting Decryption for the path : {0}", options.Source));
          try
          {
            if (! File.Exists(source))
            {
              logger.Log(string.Format("Source path not found : {0}", source));
              return;
            }
            DirEncryptor.DecryptFile(source, options.Destination + ".zip", options.Password, DirEncryptor.salt,
              DirEncryptor.iterations);
            logger.Log("Decryption done !");
          }
          catch (Exception e)
          {
            logger.Log(e.Message);
          }
         
        }


      }


      //while (true)
      //    {

      //    Console.WriteLine(" Enter folter path to encrypt");
      //    string folderPath = Console.ReadLine();
      //    var dir = new DirectoryInfo(folderPath);
      //    if (!dir.Exists)
      //    {
      //        Console.WriteLine("Path does not found : ${'0'}", folderPath);
      //    }
      //    else
      //    {

      //        var zipFile = DirEncryptor.ZipFolder(folderPath, folderPath + ".zip");
      //        DirEncryptor.EncryptFile(zipFile, folderPath+"en", "MyTestPassword", DirEncryptor.salt, DirEncryptor.iterations);
      //        DirEncryptor.DecryptFile(folderPath + "en", folderPath + "de.zip", "MyTestPassword", DirEncryptor.salt, DirEncryptor.iterations);
      //    }

      //    Console.WriteLine("You want to continue Yes/No");

      //    string op = Console.ReadLine();

      //    if(op != "Yes" && op != "Y")
      //    {
      //        break;
      //    }

      //    //Console.ReadLine();
      //}

    }
  }

  public class CustomLogger
  {
    private readonly bool Verbose;
    public CustomLogger(bool verbose)
    {
      Verbose = verbose;
    }
    public void Log(string message)
    {
      if (Verbose)
      {
        Console.WriteLine(message);
      }
    }
  }
}
