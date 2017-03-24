﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using tp10;
using System.Configuration;

namespace InterfaceClient
{
	/// <summary>
	/// Logique d'interaction pour MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			affichageDonnees.ItemsSource = ChargementDonnees();

		}

		private void triCommune_Click(object sender, RoutedEventArgs e)
		{



		}

		/// <summary>
		/// Permet de charger les données des sites d'interventions depuis la BDD.
		/// </summary>
		/// <returns>Liste des interventions</returns>
		private List<site_intervention> ChargementDonnees()
		{
			List<site_intervention> siteInterventions = new List<site_intervention>();
			using (archeoContext contextSiteIntervention = new archeoContext())
			{
				try
				{
					var seeAll = from u in contextSiteIntervention.site_intervention
								 select u;
					foreach (var item in seeAll)
					{
						siteInterventions.Add(new tp10.site_intervention()
						{

							ID_site = "hidden",

							nom_site = item.nom_site,

							periodes = item.periodes,

							themes = item.themes,

							latitude = item.latitude,

							longitude = item.longitude
						});
					}

				}
				catch (Exception err)
				{
					MessageBox.Show(ConfigurationManager.ConnectionStrings["archeoContext"].ToString() + "\nLa connexion n'est pas valide" + err.Message, "Serveur/Database Incorrect");
				}
				return siteInterventions;
			}
		}

		/// <summary>
		/// Sortie de l'application.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MenuItem_Click(object sender, RoutedEventArgs e)
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
	}
}
