using System.IO;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Rogue;
using Newtonsoft.Json;
using System.Xml.Linq;

namespace EnemyEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonSaveToJson_Click(object sender, RoutedEventArgs e)
        {


            int EnemyCount = EnemyList.Items.Count;

            // TODO: Luo taulukko johon viholliset tallennetaan JSON muunnosta varten
            // Tee taulukosta niin iso että kaikki luodut viholliset mahtuvat siihen, eli sen koko
            // on sama kuin EnemyCount. Voit tehdä myös listan eli List<Enemy>
            List<Rogue.Enemy> tempList = new List<Rogue.Enemy>();

            // Käy jokainen vihollinen läpi ja hae sen tiedot.
            for (int i = 0; i < EnemyCount; i++)
            {
                // Muuta ListBox elementissä oleva Object tyyppinen olio Enemy tyyppiseksi
                Rogue.Enemy enemy = (Rogue.Enemy)EnemyList.Items[i];
                // Lisää vihollinen listaan
                tempList.Add(enemy);
            }

            // TODO: Muuta lista JSON muotoon,
            // käytä tässä NewtonSoft.JSON kirjastoa
            string enemiesArrayJSON = JsonConvert.SerializeObject(tempList);

            // TODO: Luo tiedosto enemies.json
            string filename = "enemiesData.json";
            using (StreamWriter enemyWriter = new StreamWriter(filename))
            {
                enemyWriter.Write(enemiesArrayJSON);
            }

            // TODO: Näytä käyttäjälle viesti että kirjoittaminen onnistui
            ErrorLable.ToolTip = "Save succesfull";
        }

        private void IDBox_Validation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);

        }

        private void HP_Validation(object sender, TextCompositionEventArgs e)
        {
            Regex regex;
            if (char.IsNumber(e.Text[0]))
            {
                regex = new Regex("[^1-9]+");
            }
            else
            {
                regex = new Regex("[^0-9]+");
            }
            e.Handled = regex.IsMatch(e.Text);
        }

        private void AddEnemyToList(object sender, RoutedEventArgs e)
        {
            // Get name and hitpoints

            // Check that they are ok

            // Create new Item
            string name = txtName.Text;
            string hpString = HP.Text;
            string spriteID = SpriteID.Text;
            int hpInt;
            int SpriteInt;
            Point2D PlaceHolderpos = new Point2D(0, 0);
            if (Int32.TryParse(hpString, out hpInt))
            {

                if (Int32.TryParse(spriteID, out SpriteInt))
                {
                    if (string.IsNullOrEmpty(name) == false)
                    {
                        // All ok: Create new Enemy and add it to ListBox
                        EnemyList.Items.Add(new Rogue.Enemy(name, PlaceHolderpos, hpInt, SpriteInt));
                    }
                }

            }
        }

        private void txtName_Validation(object sender, TextCompositionEventArgs e)
        {
            Name = e.Text;
        }
    }
}