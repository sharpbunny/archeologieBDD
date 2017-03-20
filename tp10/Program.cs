using System.IO;
using System;

namespace tp10
{
    class Program
    {

        static void Main(string[] args)
        {
            //Vérification du 
            if(args.Length > 1 && args.Length< 2)
            {
                switch(args[0])
                {
                    case "-f":
                        ////Ouvre le fichier / Open the file
                        //var stream = File.OpenText(args[1]);
                        ////Lire le fichier / Read the file
                        //string st = stream.ReadToEnd();
                        //var jsonArray = JsonArray.Parse(st);
                        //foreach (var item in jsonArray)
                        //{

                        //}


                        break;
                    default:
                        Console.WriteLine("Tu t'es planté mon gars !");
                        break;
                }
            }
        }
    }
}
