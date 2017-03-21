using Newtonsoft.Json;
using System;
using System.IO;


namespace tp10
{
	class JsonFile
	{
		private dynamic _tableauJson;
		private string _nomfichier;

		//Constructeur
		public JsonFile(string fichierJson)
		{
			Nomfichier = fichierJson;
			TableauJson = LoadJson(Nomfichier);
		}
		public string Nomfichier
		{
			get
			{
				return _nomfichier;
			}

			set
			{
				_nomfichier = value;
			}
		}

		public dynamic TableauJson
		{
			get
			{
				return _tableauJson;
			}

			set
			{
				_tableauJson = value;
			}
		}

		//Méthodes
		/// <summary>
		/// Chargement du fichier json.
		/// </summary>
		/// <param name="filename">Nom du fichier à parser.</param>
		public static dynamic LoadJson(string filename)
		{
			using (StreamReader r = new StreamReader(filename))
			{
				//Variable qui va lire le fichier jusqu'à la fin
				string json = r.ReadToEnd();

				//List<Item> items = JsonConvert.DeserializeObject<List<Item>>(json);
				dynamic ensembleIntervention = JsonConvert.DeserializeObject(json);
				
				return ensembleIntervention;
			}
		}

	}
}
