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

		private void triCommune_Click(object sender, RoutedEventArgs e)
		{



		}
		public class ArcheoData
		{
			private int _lineNumber;
			private string _idline;
			private string _nomsite;
			private string _nomcommune;
			private float _latitude;
			private float _longitude;

			public ArcheoData(int line, string id, string no, string co, float lat, float lon)
			{
				_lineNumber = line;
				_idline = id;
				_nomsite = no;
				_nomcommune = co;
				_latitude = lat;
				_longitude = lon;
			}
			public int LineNumber
			{
				get
				{
					return _lineNumber;
				}
				set
				{
					_lineNumber = value;
				}
			}
			public string IDLigne
			{
				get
				{
					return _idline;
				}
				set
				{
					_idline = value;
				}
			}
			public string NomSite
			{
				get { return _nomsite; }
				set { _nomsite = value; }
			}
			public string NomCommune
			{
				get { return _nomcommune; }
				set { _nomcommune = value; }
			}
			public float Latitude
			{
				get { return _latitude; }
				set { _latitude = value; }
			}
			public float Longitude
			{
				get { return _longitude; }
				set { _longitude = value; }
			}
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
								 //join ti in contextSiteIntervention.type_intervention on site.ID_site equals ti.site_intervention  && 
								 select new
								 {
									 ID_site = site.ID_site,
									 nom_site = site.nom_site,
									 periodes = site.periodes,
									 IDcommune = site.ID_commune,
									 Commune = commune,
									 themes = site.themes,
									 latitude = site.latitude,
									 longitude = site.longitude
								 };

					int line = 1;
					foreach (var item in seeAll)
					{
						var_dump(item);

						archeologyData.Add(new ArcheoData(line, item.ID_site, item.nom_site, item.Commune.nom, item.latitude, item.longitude));
						line++;

						//ID_site = item.ID_site,
						//nom_site = item.nom_site,
						//periodes = item.periodes,
						//ID_commune = item.IDcommune,
						//Commune = item.Commune,
						//themes = item.themes,
						//latitude = item.latitude,
						//longitude = item.longitude
						
					}

				}
				catch (Exception err)
				{
					MessageBox.Show(ConfigurationManager.ConnectionStrings["archeoContext"].ToString() + "\nLa connexion n'est pas valide.\n" + err.Message, "Serveur/Database Incorrect");
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
			Debug.WriteLine("{0,-18} {1}", "Name", "Value");
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
