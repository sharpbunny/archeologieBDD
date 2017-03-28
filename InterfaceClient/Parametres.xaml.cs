using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml;
using System.Diagnostics;
using System.Data;

namespace InterfaceClient
{
	/// <summary>
	/// Logique d'interaction pour Parametres.xaml
	/// </summary>
	public partial class Parametres : Window
	{
		public Parametres()
		{
			InitializeComponent();
		}

		private void buttonConnect_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				// On construit le connexion string
				StringBuilder Connect = new StringBuilder("data Source=");
				Connect.Append(textServer.Text);
				Connect.Append(";initial Catalog=");
				Connect.Append(textDatabase.Text);
				Connect.Append(";integrated security=True;MultipleActiveResultSets=True;App=EntityFramework");
				string strCon = Connect.ToString();
				Debug.WriteLine(strCon);
				updateConfigFile(strCon);
				// On crée une nouvelle connexion.
				SqlConnection Db = new SqlConnection();
				// On rafraîchit sinon il utilise la valeur précédente.
				ConfigurationManager.RefreshSection("connectionStrings");
				Db.ConnectionString = ConfigurationManager.ConnectionStrings["archeoContext"].ToString();
				// On lance une requête pour vérifier que le connexion string fonctionne.
				SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM dbo.Commune", Db);
				DataTable dt = new DataTable();
				da.Fill(dt);
				comboBox.Items.Add("azerty");
				comboBox.SelectedIndex = 0;
			}
			catch (Exception err)
			{
				MessageBox.Show(ConfigurationManager.ConnectionStrings["archeoContext"] + ".La connexion n'est pas valide\n" + err.Message, "Serveur/Database Incorrect");
			}
		}

		/// <summary>
		/// Mise à jour fichier de configuration.
		/// </summary>
		/// <param name="conf"></param>
		public void updateConfigFile(string conf)
		{
			// mise à jour de la config
			XmlDocument XmlDoc = new XmlDocument();
			// On charge la config
			XmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
			foreach (XmlElement xElement in XmlDoc.DocumentElement)
			{
				// on cherche LES connexions strings
				if (xElement.Name == "connectionStrings")
				{
					// on initialise LE connexion string.
					xElement.FirstChild.Attributes["connectionString"].Value = conf;
				}
			}
			// On écrit le connexion string dans le fichier de config.
			XmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
		}

		/// <summary>
		/// Bouton OK pour fermer paramètres.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonOK_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}
