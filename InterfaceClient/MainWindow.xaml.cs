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
        List<string> lesthemes = new List<string>();
        /// <summary>
        /// Déclaration d'une ObservableCollection "using System.Collections.ObjectModel" 
        /// Permet grace au Binding de la Grid ItemsSource="{Binding archeologyData}" dans .Xmal
        /// De remplir les colonnes associé avec les champ de la class ArcheoData
        /// </summary>
        public static ObservableCollection<ArcheoData> archeologyData { get; set; }

        public MainWindow()
        {

            InitializeComponent();
            this.DataContext = this;
            archeologyData = new ObservableCollection<ArcheoData>();
            ChargementDonnees();
        }

        private void triCommune_Click(object sender, RoutedEventArgs e)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// 
        /// </summary>
        public class ArcheoData
        {
            public ArcheoData()
            {

            }
            public ArcheoData(int line, string id, string no, string co, float lat, float lon, ICollection<theme> th, ICollection<intervention> inter, ICollection<periode>per)
            {
                LineNumber = line;
                IDLigne = id;
                NomSite = no;
                NomCommune = co;
                Latitude = lat;
                Longitude = lon;
                for (int i = 0; i < th.Count; i++)
                {
                    Theme = (th.ElementAt(i).nom);
                }

                DateDebut = Convert.ToString(inter.ElementAt(0).date_debut);
                DateFin = Convert.ToString(inter.ElementAt(0).date_fin);
                for (int i = 0; i < per.Count; i++)
                {
                    Periode = per.ElementAt(i).nom;
                }
                
            }

            /// <summary>
            /// Propriété
            /// </summary>
			public int LineNumber { get; set; }

            public string IDLigne { get; set; }

            public string NomSite { get; set; }

            public string NomCommune { get; set; }

            public float Latitude { get; set; }

            public float Longitude { get; set; }

            public string Theme { get; set; }

            public static ComboBox ComboTheme { get; set; }

            public string Periode { get; set; }

            public string DateDebut { get; set; }

            public string DateFin { get; set; }

        }

        /// <summary>
        /// Permet de charger les données des sites d'interventions depuis la BDD.
        /// </summary>
        /// <returns>Liste des interventions</returns>
        private void ChargementDonnees()
        {
            using (archeoContext contextSiteIntervention = new archeoContext())
            {
                //try
                //{
                var seeAll = from site in contextSiteIntervention.site_intervention
                             join commune in contextSiteIntervention.Communes on site.ID_commune equals commune.ID_commune
                             join intervention in contextSiteIntervention.interventions on site.ID_site equals intervention.ID_site
                             //join theme in contextSiteIntervention.themes on site.themes                   
                             //where site.ID_site == "1bc37299baa6fbf11e71c3e32cde79e8e99ad429"                              
                             select new
                             {
                                 ID_site = site.ID_site,
                                 nom_site = site.nom_site,
                                 periodes = site.periodes,
                                 IDcommune = site.ID_commune,
                                 Commune = commune,
                                 latitude = site.latitude,
                                 longitude = site.longitude,
                                 theme = site.themes,
                                 intervention = site.interventions,
                                 periode = site.periodes
                             };

                int line = 1;
                foreach (var item in seeAll)
                {
                    var_dump(item);

                    archeologyData.Add(new ArcheoData(line, item.ID_site, item.nom_site, item.Commune.nom, item.latitude, item.longitude, item.theme, item.intervention, item.periode));

                    //MessageBox.Show(""+ item.theme.Count);
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

                //}
                //catch (Exception err)
                //{
                //	MessageBox.Show(ConfigurationManager.ConnectionStrings["archeoContext"].ToString() + "\nLa connexion n'est pas valide.\n" + err.Message, "Serveur/Database Incorrect");
                //}
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

        public void NameComboTheme_Loaded(object sender, MouseButtonEventArgs e)
        {

            //ArcheoData Sitegetid = Grill_de_donne.SelectedItems as ArcheoData;
            //float idsiteintervention = Sitegetid.Latitude;
            // lesthemes.Add("salut");
            MessageBox.Show("" + sender);
          
            var comboBox = sender as ComboBox;
            comboBox.ItemsSource = lesthemes;
            comboBox.SelectedIndex = 0;

        }

        private void Changer_combobox(object sender, RoutedEventArgs e)
        {
            var select = sender as ComboBox;
            string name = select.SelectedItem as string;
        }

       
    }
}
