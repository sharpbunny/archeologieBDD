using System.IO;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;


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
			using (archeoContext context = new archeoContext())
			{
				context.Configuration.LazyLoadingEnabled = true;
				foreach (var item in fichierJson.TableauJson)
				{
					//Console.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9} {10} {11} {12} {13}", item.datasetid, item.recordid, item.fields.departement, item.fields.commune, item.fields.periode_s, item.fields.coordonnee_wgs84, item.fields.nom_du_site, item.fields.date_fin.getType(), item.fields.type_d_intervention, item.fields.date_debut.ToString.getType(), item.geometry.type, item.geometry.coordinates);
					//Console.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9} ", item.datasetid, item.recordid, item.fields.departement, item.fields.commune, item.fields.periode_s, item.fields.coordonnee_wgs84, item.fields.nom_du_site, item.fields.type_d_intervention, item.geometry.type, item.geometry.coordinates);
					Console.WriteLine(item.datasetid + " | " + item.recordid + " | " + item.fields.departement + " | " + item.fields.commune + " | " + item.fields.periode_s + " | " + item.fields.coordonnee_wgs84 + " | " + item.fields.nom_du_site + " | " + item.fields.type_d_intervention + " | " + item.geometry.type + " | " + item.geometry.coordinates);
					// stockage des départements

					// stockage ville

					//suite


					//Console.ReadLine();
				}
			}
		}
	}
}
