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
			Nomfichier = fichierJson;
			TableauJson = LoadJson(Nomfichier);
		}

		public string Nomfichier
		{
			get; set;
		}

		public dynamic TableauJson
		{
			get; set;
		}

		/// <summary>
		/// Chargement du fichier json.
		/// </summary>
		/// <param name="filename">Nom du fichier à parser.</param>
		public static dynamic LoadJson(string filename)
		{
			using (StreamReader r = new StreamReader(filename))
			{
				// Variable qui va lire le fichier jusqu'à la fin.
				string json = r.ReadToEnd();

				dynamic ensembleIntervention = JsonConvert.DeserializeObject(json);
				
				return ensembleIntervention;
			}
		}

	}
}
