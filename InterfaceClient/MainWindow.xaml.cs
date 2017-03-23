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
using System.Windows.Navigation;
using System.Windows.Shapes;

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

            affichageDonnees.ItemsSource = ChargmentDonnees();

		}

        private void triCommune_Click(object sender, RoutedEventArgs e)
        {
            


        }

        /// <summary>
        /// Permet de charger les données des sites d'interventions depuis la BDD.
        /// </summary>
        /// <returns>Liste des interventions</returns>
        private List<tp10.site_intervention>ChargmentDonnees()
        {
            List<tp10.site_intervention> siteInterventions = new List<tp10.site_intervention>();
            using (tp10.archeoContext contextSiteIntervention = new tp10.archeoContext())
            {
                var seeAll = from u in contextSiteIntervention.site_intervention
                             select u;
                foreach(var item in seeAll)
                {
                    siteInterventions.Add(new tp10.site_intervention() {

                        ID_site = "hidden",

                        nom_site = item.nom_site,

                        periodes = item.periodes,

                        themes = item.themes,

                        latitude = item.latitude,

                        longitude = item.longitude
                        });
                }

                return siteInterventions;

            }
            
            
        }

    }
}
