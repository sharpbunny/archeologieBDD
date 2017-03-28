using Newtonsoft.Json;
using System;
using System.IO;


namespace tp10
{
	class JsonFile
	{
		//Constructeur
		public JsonFile(string fichierJson)
		{
            //Une fois que le lien vers le fichier est rentré, on récupère le nom du fichier et on appelle la fonction
            //de chargement de ce dernier.
			Nomfichier = fichierJson;
			TableauJson = LoadJson(Nomfichier);
		}

		public string Nomfichier { get; set; }

		public dynamic TableauJson { get; set; }

		/// <summary>
		/// Chargement du fichier json.
		/// </summary>
		/// <param name="filename">Nom du fichier à parser.</param>
		public static dynamic LoadJson(string filename)
		{
            //Utilisation d'une variable StreamReader ou "lecteur de flux"
			using (StreamReader r = new StreamReader(filename))
			{
				// Variable qui va lire le fichier jusqu'à la fin.
				string json = r.ReadToEnd();

                //Création d'une variable dynamique qui représente un tableau contenant toute les donnés à rentrer dans la BDD
				dynamic ensembleIntervention = JsonConvert.DeserializeObject(json);
				
				return ensembleIntervention;
			}
		}

	}
}
