using System.IO;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;



namespace tp10
{
	class Program
	{
		private static JsonFile fichierJson;
		//private static string _fichierJson= "C:/Users/34011-14-02/Documents/waldodevgitbash/archeologieBDD/tp10/bin/Debug/listIntervention.json";
		
		//private List<JsonFile> collection=new List<JsonFile>();
		
		/// <summary>
		/// Affichage de l'aide du programme en cas d'entrée erronée.
		/// </summary>
		private static void help()
		{
			Console.WriteLine("Usage:");
			Console.WriteLine("tp10.exe -f nom_du_fichier");
		}

		/// <summary>
		/// Point d'entrée du programme.
		/// </summary>
		/// <param name="args">Liste des paramètres de la ligne de commande.</param>
		static void Main(string[] args)
		{
			//Vérification des arguments de la ligne de commande.
			if (args.Length > 1)
			{
				switch (args[0])
				{
					case "-f":

						if (File.Exists(args[1]))
						{
							fichierJson = new JsonFile(args[1]);
							stockeEnBase(fichierJson);
						}
						else
						{
							Console.WriteLine("Le fichier {0} n'existe pas", args[1]);
						}

						break;
					default:
						help();
						break;
				}
			}
			else
			{
				help();
			}

		}

		private static void stockeEnBase(JsonFile fichierJson)
		{
            string nomDepartement;
			using (archeoContext context = new archeoContext())
			{
				context.Configuration.LazyLoadingEnabled = true;

				foreach (var itemjson in fichierJson.TableauJson)
				{
                    Console.WriteLine(itemjson.fields.departement);
                    // stockage des départements

                    //Interrogation de la base de donnée en LINQ
                    //Vérification si la base de données possède déjà le département
                    nomDepartement = itemjson.fields.departement;
                    var rechercheNomDep = from b in context.departements
                                          where b.nom == nomDepartement
                                          select b;
                    foreach(var p in rechercheNomDep)
                    {
                        Console.WriteLine(p.nom);
                    }
                   

                    // stockage ville

                    //suite
                    Console.ReadLine();

					//Console.ReadLine();
				}
			}
		}
	}
}
