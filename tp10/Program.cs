using System.IO;
using System;
using Newtonsoft.Json;

namespace tp10
{
	class Program
	{
		/// <summary>
		/// Chargement du fichier json.
		/// </summary>
		public static void LoadJson(string filename)
		{
			using (StreamReader r = new StreamReader(filename))
			{
				string json = r.ReadToEnd();
				//List<Item> items = JsonConvert.DeserializeObject<List<Item>>(json);
				dynamic array = JsonConvert.DeserializeObject(json);
				foreach (var item in array)
				{
					Console.WriteLine("{0} {1} {2} {3}", item.datasetid, item.recordid, item.fields.departement, item.fields.commune);
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public class Item
		{
			public string datasetid;
			public string recordid;
		}

		/// <summary>
		/// Affichage de l'aide du programme en cas d'entrée erronée.
		/// </summary>
		private static void help()
		{
			Console.WriteLine("Usage:");
			Console.WriteLine("tp10 -f nom_du_fichier");
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

						LoadJson(args[1]);

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
	}
}
