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
			IQueryable<site_intervention> filteredSite = null;
			using (archeoContext contextSiteIntervention = new archeoContext())
			{
				try
				{
					filteredSite = contextSiteIntervention.site_intervention;
					filteredSite.Join(contextSiteIntervention.Communes, s => s.ID_commune, c => c.ID_commune, (s,c) => new { site_intervention = s, commune = c});
					filteredSite.Join(contextSiteIntervention.departements, c => c.ID_commune, d => d.ID_departement, (c, d) => new { commune = c, departement = d });
					filteredSite.Join(contextSiteIntervention.interventions, i => i.ID_site, s => s.ID_site, (i, s) => new { intervention = i, site_intervention = s });
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

				}
				catch (Exception err)
				{
					MessageBox.Show(ConfigurationManager.ConnectionStrings["archeoContext"] + "\nLa connexion n'est pas valide.\n" + err.Message, "Serveur/Database Incorrect");
				}

				// Affichage.
				try
				{ 
					int line = 1;
					foreach (var item in filteredSite)
					{
						StringBuilder listthemes = new StringBuilder();
						foreach (var itemtheme in item.themes)
						{
							listthemes.Append("#" + itemtheme.nom);
						}
						StringBuilder listTypeInter = new StringBuilder();
						foreach (var itemtype in item.type_intervention)
						{
							listTypeInter.Append("#" + itemtype.nom);
						}
						StringBuilder listPeriodes = new StringBuilder();
						foreach (var itemPeriode in item.periodes)
						{
							listPeriodes.Append("#" + itemPeriode.nom);
						}
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
								listthemes.ToString(),
								listTypeInter.ToString(),
								listPeriodes.ToString()
							)
						);
						line++;

					}

				}
				catch (Exception err)
				{
					MessageBox.Show("\nProblème d'affichage.\n" + err.Message);
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
