using System.IO;
using System;
using Newtonsoft.Json;


namespace tp10
{
	class Program
	{
		private static JsonFile fichierJson;

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

						fichierJson = new JsonFile(args[1]);

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

			using (archeoContext context = new archeoContext())
			{
				context.Configuration.LazyLoadingEnabled = true;
			}
			foreach (var item in fichierJson.TableauJson)
			{
				Console.WriteLine("{0} {1} {2} {3}", item.datasetid, item.recordid, item.fields.departement, item.fields.commune);
			}
		}


	}
}
