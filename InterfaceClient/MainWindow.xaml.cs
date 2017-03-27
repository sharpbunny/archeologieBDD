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
            columnCommune.CanUserSort = true;

            if(triCommune.Content.Equals("Ascendant"))
            {
                triCommune.Content = "Descendant";
                columnCommune.SortDirection = System.ComponentModel.ListSortDirection.Ascending;
                
            }
            else
            {
                triCommune.Content = "Ascendant";
                columnCommune.SortDirection = System.ComponentModel.ListSortDirection.Descending;

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
					IQueryable<site_intervention> filteredSite = contextSiteIntervention.site_intervention;
					filteredSite.Join(contextSiteIntervention.Communes, s => s.ID_commune, c => c.ID_commune, (s,c) => new { site_intervention = s, commune = c});
					filteredSite.Join(contextSiteIntervention.departements, c => c.ID_commune, d => d.ID_departement, (c, d) => new { commune = c, departement = d });
					filteredSite.Join(contextSiteIntervention.interventions, i => i.ID_site, s => s.ID_site, (i, s) => new { intervention = i, site_intervention = s });
					//filteredSite.Join(contextSiteIntervention.themes, t => t.themes, s => s.ID_theme, (t, s) => new { theme = t, site_intervention = s });
					//filteredSite.Where(s => s.nom_site == "Le moulin");
					//filteredSite.OrderBy(c => c.Commune.nom);
					filteredSite.Select(site => new site_intervention()
					{
						ID_site = site.ID_site,
						nom_site = site.nom_site,
						periodes = site.periodes,
						ID_commune = site.ID_commune,
						Commune = site.Commune,
						latitude = site.latitude,
						longitude = site.longitude,
						interventions = site.interventions,
						themes = site.themes
					});
					foreach (var item in filteredSite)
					{
						var_dump(item);
						var_dump(item.Commune);
						var_dump(item.themes);
					}

					//var seeAll = from site in contextSiteIntervention.site_intervention
					//			 join commune in contextSiteIntervention.Communes on site.ID_commune equals commune.ID_commune
					//			 join dept in contextSiteIntervention.departements on commune.ID_departement equals dept.ID_departement
					//			 join interv in contextSiteIntervention.interventions on site.ID_site equals interv.ID_site
					//			 //orderby commune.nom
					//			 select new
					//			 {
					//				 ID_site = site.ID_site,
					//				 nom_site = site.nom_site,
					//				 periodes = site.periodes,
					//				 IDcommune = site.ID_commune,
					//				 Commune = commune,
					//				 Departement = dept,
					//				 latitude = site.latitude,
					//				 longitude = site.longitude,
					//				 listIntervention = interv
					//			 };

					int line = 1;
					foreach (var item in filteredSite)
					{
						//var_dump(item);
						string themes = "a b c";
						//var_dump(item.listIntervention);
						archeologyData.Add
						(
							new ArcheoData
							(
								line, 
								item.ID_site,
								item.nom_site,
								item.Commune.nom,
								item.Commune.departement.nom,
								item.latitude,
								item.longitude,
								item.interventions.FirstOrDefault().date_debut,
								item.interventions.FirstOrDefault().date_fin,
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
