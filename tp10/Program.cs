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
            //Il n'y a que 2 arguments à vérifier dans notre programme, mais il nous faut en vérifier au moins 1
			if (args.Length > 1)
			{
                //Création d'un switch sur l'argument 0
				switch (args[0])
				{
					case "-f":
                        //Vérification de l'existance du fichier
						if (File.Exists(args[1]))
						{
                            //Si le fichier existe, on crée une variable de classe JsonFile, et on appelle une fonction de cette classe
							JsonFile fichierJson = new JsonFile(args[1]);
                            //Voir Classe JsonFile
							stockeEnBase(fichierJson);
						}
						else
						{
							Console.WriteLine("Le fichier {0} n'existe pas", args[1]);
						}

						break;
                        //Par défaut, appel de la fonction help afin de réexpliquer à l'utilisateur comment bien faire
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

        /// <summary>
        /// Stockage d'un fichier Json en Base de Données.
        /// </summary>
        /// <param name="fichierJson">Nom du fichier Json à rentrer en paramètres.</param>
		private static void stockeEnBase(JsonFile fichierJson)
		{
            //Utilisation d'un context basé sur les tables de la base de données
			using (archeoContext context = new archeoContext())
			{
                //Pour chaque éléments du fichier Json, on attribue les noms de colonnes en Json aux noms de colonnes en base de données
				foreach (var itemjson in fichierJson.TableauJson)
				{
					string nomDepartement = itemjson.fields.departement;
					int idDepartement;
					string nomCommune = itemjson.fields.commune;
					int idCommune;
					string nomsiteIntervention = itemjson.fields.nom_du_site;
					string idsiteIntervention = itemjson.recordid;
					float latiIenterention = itemjson.fields.coordonnee_wgs84[0];
					float longitudeIenterention = itemjson.fields.coordonnee_wgs84[1];

					// stockage des départements
					//Interrogation de la base de donnée en LINQ
					//Vérification si la base de données possède déjà le département
					var rechercheNomDep = from d in context.departements
										  where d.nom == nomDepartement
										  select d;

                    //Verification de l'existance d'au moins 1 département dans la base de données
					departement dpt = rechercheNomDep.FirstOrDefault();

                    //Si il y'a au moins un département 
					if (dpt == null)
					{
                        //Création d'une variable département pour attribuer la valeur du Json à celle de la BDD
						departement departement = new departement();
						departement.nom = nomDepartement;
                        //Ajout du département à la colonne des départements
						context.departements.Add(departement);
                        //Sauvegarde des changements dans la BDD
						context.SaveChanges();
                        //Attribution d'un identifiant au département
						idDepartement = departement.ID_departement;
                        //Affichage de la création d'un nouvel élément dans la base de donnée
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

					// insertion des thèmes
					string theme = itemjson.fields.theme_s;
					List<theme> listTheme = new List<theme>();
					if (theme != null)
					{
						string[] detailTheme = theme.Split('#');
						foreach (var item in detailTheme)
						{
							if (item!="")
							{
								var rechercheTheme = from t in context.themes
													 where t.nom == item
													 select t;
								theme existTheme = rechercheTheme.FirstOrDefault();
								if (existTheme == null)
								{
									theme newtheme = new theme();
									newtheme.nom = item;
									context.themes.Add(newtheme);
									context.SaveChanges();
									listTheme.Add(newtheme);
									Console.WriteLine("Thème {0} créé id: {1}", item, newtheme.ID_theme);
								}
								else
								{
									listTheme.Add(existTheme);
									Console.WriteLine("Thème {0} existe id: {1}", item, existTheme.ID_theme);
								}

							}
						}
					}
					else
					{
						Console.WriteLine("Pas de thème pour ce chantier.");
					}

					// insertion des types d'intervention
					string typesIntervention = itemjson.fields.type_d_intervention;
					List<type_intervention> listTypeIntervention = new List<type_intervention>();
					if (typesIntervention != null)
					{
						string[] listeTypesIntervention = typesIntervention.Split('#');
						foreach (var item in listeTypesIntervention)
						{
							if (item != "")
							{
								var rechercheType = from t in context.type_intervention
													 where t.nom == item
													 select t;
								type_intervention existType = rechercheType.FirstOrDefault();
								if (existType == null)
								{
									type_intervention newtype = new type_intervention();
									newtype.nom = item;
									context.type_intervention.Add(newtype);
									context.SaveChanges();
									listTypeIntervention.Add(newtype);
									Console.WriteLine("Type d'intervention {0} créé id: {1}", item, newtype.ID_type);
								}
								else
								{
									listTypeIntervention.Add(existType);
									Console.WriteLine("Type d'intervention {0} existe id: {1}", item, existType.ID_type);
								}

							}
						}
					}
					else
					{
						Console.WriteLine("Pas de type d'intervention pour ce chantier.");
					}

					// insertion des périodes
					string periode = itemjson.fields.periode_s;
					List<periode> listPeriode = new List<periode>();
					if (periode != null)
					{
						string[] listePeriode = periode.Split('#');
						foreach (var item in listePeriode)
						{
							if (item != "")
							{
								var recherchePeriode = from t in context.periodes
													   where t.nom == item
													   select t;
								periode existPeriode = recherchePeriode.FirstOrDefault();
								if (existPeriode == null)
								{
									periode newperiode = new periode();
									newperiode.nom = item;
									context.periodes.Add(newperiode);
									context.SaveChanges();
									listPeriode.Add(newperiode);
									Console.WriteLine("Période {0} créée id: {1}", item, newperiode.ID_periode);
								}
								else
								{
									listPeriode.Add(existPeriode);
									Console.WriteLine("Période {0} existe id: {1}", item, existPeriode.ID_periode);
								}

							}
						}
					}
					else
					{
						Console.WriteLine("Pas de période pour ce chantier.");
					}

					// insertion site intervention
					var rechercheSiteIntervention = from site in context.site_intervention
													where site.ID_site == idsiteIntervention
													select site;
					site_intervention itr = rechercheSiteIntervention.FirstOrDefault();
					if (itr == null)
					{
						site_intervention siteIntervention = new site_intervention();
						siteIntervention.ID_site = idsiteIntervention;
						siteIntervention.nom_site = nomsiteIntervention;
						siteIntervention.latitude = latiIenterention;
						siteIntervention.longitude = longitudeIenterention;
						siteIntervention.ID_commune = idCommune;

						siteIntervention.themes = listTheme;
						siteIntervention.type_intervention = listTypeIntervention;
						siteIntervention.periodes = listPeriode;
						
						context.site_intervention.Add(siteIntervention);


						context.SaveChanges();
						Console.WriteLine("Le site d'intervention {0} dans la commune {1} créé id: {2}", nomsiteIntervention, nomCommune, idsiteIntervention);
					}
					else
					{
						idsiteIntervention = itr.ID_site;
						Console.WriteLine("Le site d'intervention {0} dans la commune {1} existe id: {2}", nomsiteIntervention, nomCommune, idsiteIntervention);
					}


					//Ajout de l'intervention

					var uneIntervention = from actionIntervention in context.interventions
										  where idsiteIntervention  == actionIntervention.ID_site
										  select actionIntervention;
					intervention actinter = uneIntervention.FirstOrDefault();
					if(actinter==null)
					{
						intervention insererLintervention = new intervention();
						if (itemjson.fields.date_debut != null)
						{
							insererLintervention.date_debut = itemjson.fields.date_debut;
						}
						if (itemjson.fields.date_fin != null)
						{
							insererLintervention.date_fin = itemjson.fields.date_fin;
						}
						insererLintervention.ID_site = idsiteIntervention;
						context.interventions.Add(insererLintervention);
						context.SaveChanges();
						Console.WriteLine("L'intervention du {0} au {1} a été insérée :", insererLintervention.date_debut, insererLintervention.date_fin);						
					}
					else
					{
						Console.WriteLine("Cette intervention a déjà été enregistrée. Pas de clé étrangère associée");
					}

					
				}
                Console.WriteLine("Done!");
                Console.ReadLine();
			}
		}

	}
}
