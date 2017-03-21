using System.IO;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;


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
				//context.Configuration.LazyLoadingEnabled = true;
				foreach (var itemjson in fichierJson.TableauJson)
				{
					string nomDepartement = itemjson.fields.departement;
					int idDepartement;
					string nomCommune = itemjson.fields.commune;
					int idCommune;
					//Console.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9} {10} {11} {12} {13}", item.datasetid, item.recordid, item.fields.departement, item.fields.commune, item.fields.periode_s, item.fields.coordonnee_wgs84, item.fields.nom_du_site, item.fields.date_fin.getType(), item.fields.type_d_intervention, item.fields.date_debut.ToString.getType(), item.geometry.type, item.geometry.coordinates);
					//Console.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9} ", item.datasetid, item.recordid, item.fields.departement, item.fields.commune, item.fields.periode_s, item.fields.coordonnee_wgs84, item.fields.nom_du_site, item.fields.type_d_intervention, item.geometry.type, item.geometry.coordinates);
					//Console.WriteLine(item.datasetid + " | " + item.recordid + " | " + item.fields.departement + " | " + item.fields.commune + " | " + item.fields.periode_s + " | " + item.fields.coordonnee_wgs84 + " | " + item.fields.nom_du_site + " | " + item.fields.type_d_intervention + " | " + item.geometry.type + " | " + item.geometry.coordinates);

					// stockage des départements
					//Interrogation de la base de donnée en LINQ
					//Vérification si la base de données possède déjà le département
					var rechercheNomDep = from d in context.departements
										  where d.nom == nomDepartement
										  select d;
					departement dpt = rechercheNomDep.FirstOrDefault();
					if (dpt == null)
					{
						departement departement = new departement();
						departement.nom = nomDepartement;
						context.departements.Add(departement);
						context.SaveChanges();
						idDepartement = departement.ID_departement;
						Console.WriteLine("Département {0} créé id: {1}", nomDepartement, idDepartement);
					}
					else
					{
						idDepartement = dpt.ID_departement;
						Console.WriteLine("Département {0} existe id: {1}", nomDepartement, idDepartement);
					}

					// stockage ville
					var rechercheNomVille = from c in context.Communes
										  where c.nom == nomCommune && c.ID_departement == idDepartement
										  select c;
					Commune cmn = rechercheNomVille.FirstOrDefault();
					if (cmn == null)
					{
						Commune commune = new Commune();
						commune.nom = nomCommune;
						commune.ID_departement = idDepartement;
						context.Communes.Add(commune);
						context.SaveChanges();
						idCommune = commune.ID_commune;
						Console.WriteLine("Commune {0} dans département {1} créée id: {2}", nomCommune, nomDepartement, idCommune);
					}
					else
					{
						idCommune = cmn.ID_commune;
						Console.WriteLine("Commune {0} dans département {1} existe id: {2}", nomCommune, nomDepartement, idCommune);
					}


					// suite

					Console.ReadLine();
				}
			}
		}
	}
}
