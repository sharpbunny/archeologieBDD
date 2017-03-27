using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using tp10;
using System.Collections.ObjectModel;

namespace InterfaceClient
{
	/// <summary>
	/// Logique d'interaction pour MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public ObservableCollection<ArcheoData> archeologyData { get; set; }

		public MainWindow()
		{

			InitializeComponent();
			this.DataContext = this;
			archeologyData = new ObservableCollection<ArcheoData>();
			ChargementDonnees();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void triCommune_Click(object sender, RoutedEventArgs e)
		{
			throw new NotSupportedException();
		}

		/// <summary>
		/// 
		/// </summary>
		public class ArcheoData
		{

			public ArcheoData(int line, string id, string no, string co, string dp, float lat, float lon, DateTime? ddeb, DateTime? dfin, string th)
			{
				LineNumber = line;
				IDLigne = id;
				NomSite = no;
				NomCommune = co;
				NomDepartement = dp;
				Latitude = lat;
				Longitude = lon;
				DateDebut = ddeb;
				DateFin = dfin;
				Theme = th;
			}

			public int LineNumber { get; }

			public string IDLigne { get; }

			public string NomSite { get; set; }

			public string NomCommune { get; set; }

			public string NomDepartement { get; set; }

			public float Latitude { get; set; }

			public float Longitude { get; set; }

			public DateTime? DateDebut { get; set; }

			public DateTime? DateFin { get; set; }

			public string Theme { get; set; }

		}

		/// <summary>
		/// Permet de charger les données des sites d'interventions depuis la BDD.
		/// </summary>
		/// <returns>Liste des interventions</returns>
		private void ChargementDonnees()
		{
			using (archeoContext contextSiteIntervention = new archeoContext())
			{
				try
				{
					
					var seeAll = from site in contextSiteIntervention.site_intervention
								 join commune in contextSiteIntervention.Communes on site.ID_commune equals commune.ID_commune
								 join dept in contextSiteIntervention.departements on commune.ID_departement equals dept.ID_departement
								 join intervention in contextSiteIntervention.interventions on site.ID_site equals intervention.ID_site
								 select new
								 {
									 ID_site = site.ID_site,
									 nom_site = site.nom_site,
									 periodes = site.periodes,
									 IDcommune = site.ID_commune,
									 Commune = commune,
									 Departement = dept,
									 latitude = site.latitude,
									 longitude = site.longitude,
									 listIntervention = intervention
								 };

					int line = 1;
					foreach (var item in seeAll)
					{
						var_dump(item);
						string themes = "a b c";
						var_dump(item.listIntervention);
						archeologyData.Add
						(
							new ArcheoData
							(
								line, item.ID_site,
								item.nom_site,
								item.Commune.nom,
								item.Departement.nom,
								item.latitude,
								item.longitude,
								item.listIntervention.date_debut,
								item.listIntervention.date_fin,
								themes
							)
						);
						line++;

					}

				}
				catch (Exception err)
				{
					MessageBox.Show(ConfigurationManager.ConnectionStrings["archeoContext"] + "\nLa connexion n'est pas valide.\n" + err.Message, "Serveur/Database Incorrect");
				}
			}
		}

		/// <summary>
		/// Sortie de l'application.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MenuQuitter_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}

		/// <summary>
		/// Paramètres de connexion à la base de données.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MenuParametres_Click(object sender, RoutedEventArgs e)
		{
			Parametres param = new Parametres();
			param.ShowDialog();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="obj"></param>
		public static void var_dump(object obj)
		{
			Debug.WriteLine("{0,-18} {1}", "Nom", "Valeur");
			string ln = @"-----------------------------------------------------------------";
			Debug.WriteLine(ln);

			Type t = obj.GetType();
			PropertyInfo[] props = t.GetProperties();

			for (int i = 0; i < props.Length; i++)
			{
				try
				{
					Debug.WriteLine("{0,-18} {1}",
						  props[i].Name, props[i].GetValue(obj, null));
				}
				catch (Exception err)
				{
					Debug.WriteLine(err.Message);  
				}
			}
			Debug.WriteLine("\n");
		}


		/// <summary>
		/// Action du click sur menu à propos.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MenuAbout_Click(object sender, RoutedEventArgs e)
		{
			About about = new About();
			about.ShowDialog();
		}
	}
}
