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
using static System.Net.Mime.MediaTypeNames;

namespace _1103_2048
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        TextBlock[ , ] Tiles = new TextBlock[4,4];
        Dictionary<string, string> TileColorList = new Dictionary<string, string>();
        public MainWindow()
        {
            InitializeComponent();
            InitializeTileList();
            InitializeTileColorList();
            SpawnNewTile();
            RecolorTiles();

        }
        public void InitializeTileColorList()
        {
            TileColorList.Add("2", "#FFF0E6");
            TileColorList.Add("4", "#F6EBCD");
            TileColorList.Add("8", "#F3AE80");
            TileColorList.Add("16", "#E28B52");
            TileColorList.Add("32", "#DA5C36");
            TileColorList.Add("64", "#B33E1B");
            TileColorList.Add("128", "#FFDF84");
            TileColorList.Add("256", "#FAD775");
            TileColorList.Add("512", "#F9E062");
            TileColorList.Add("1024", "#EBD049");
            TileColorList.Add("2048", "#D5B720");
            TileColorList.Add("-", "#A69185");
        }
        public void InitializeTileList()
        {
            Tiles[0, 0] = Tile00;
            Tiles[0, 1] = Tile01;
            Tiles[0, 2] = Tile02;
            Tiles[0, 3] = Tile03;

            Tiles[1, 0] = Tile10;
            Tiles[1, 1] = Tile11;
            Tiles[1, 2] = Tile12;
            Tiles[1, 3] = Tile13;

            Tiles[2, 0] = Tile20;
            Tiles[2, 1] = Tile21;
            Tiles[2, 2] = Tile22;
            Tiles[2, 3] = Tile23;

            Tiles[3, 0] = Tile30;
            Tiles[3, 1] = Tile31;  
            Tiles[3, 2] = Tile32;
            Tiles[3, 3] = Tile33;
        }
        public void RewriteTiles()
        {
            for(int i = 0; i < 4; i++)
            {
                for (int k = 0; k < 4; k++)
                {
                    if (Tiles[i, k].Text != "-" && Tiles[i,k].Text.Length>1)
                    {
                        string subtext = Tiles[i, k].Text.Substring(0, Tiles[i, k].Text.Length / 2);
                        subtext = Convert.ToString(Convert.ToInt32(subtext) * 2);
                        Tiles[i, k].Text = subtext;
                    }
                }
            }  
        }
        public void RecolorTiles()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int k = 0; k < 4; k++)
                {
                    foreach(var item in TileColorList)
                    {
                        if (Tiles[i,k].Text==item.Key)
                        {
                            Tiles[i, k].Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(item.Value));
                        }
                    }
                }
            }
        }
        public void SpawnNewTile()
        {
            Random random = new Random();
            int CalculateTileValue = random.Next(1, 100);
            SolidColorBrush TileColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF0E6"));
            string TileValue;
            if (CalculateTileValue > 10) 
            {
                TileValue = "2"; 
            }
            else 
            { 
                TileValue = "4";
            }
            bool Success = false;
            var bc = new BrushConverter();
            while (!Success)
            {
                int RandomRow=random.Next(0,4);
                int RandomCol=random.Next(0,4);
                if(Tiles[RandomRow,RandomCol].Text == "-")
                {
                    Tiles[RandomRow, RandomCol].Text = TileValue;
                    Tiles[RandomRow, RandomCol].Background = TileColor;
                    Success = true;
                    break;
                }
            }
        }

        public void OnKeyDown(object sender, KeyEventArgs e)
        {
            RecolorTiles();
            if(e.Key == Key.Up)
            {
                for (int col = 0; col < 4; col++)
                {
                    int pivot = 0, row = 1;

                    while (row < 4)
                    {
                        if (Tiles[row, col].Text == "-")
                            row++;
                        else if (Tiles[pivot, col].Text == "-")
                        {
                            Tiles[pivot, col].Text = Tiles[row, col].Text;
                            Tiles[row++, col].Text = "-";
                        }
                        else if (Tiles[pivot, col].Text == Tiles[row, col].Text)
                        {

                            Tiles[pivot++, col].Text += Tiles[row, col].Text;
                            RewriteTiles();
                            RecolorTiles();
                            Tiles[row++, col].Text = "-";
                        }
                        else if (++pivot == row)
                            row++;
                    }
                }
                RecolorTiles();
                SpawnNewTile();
                RecolorTiles();
            }
            else if(e.Key == Key.Down)
            {
                RecolorTiles();
                for (int col = 0; col < 4; col++)
                {
                    int pivot = 3, row = 2;

                    while (row >= 0)
                    {
                        if (Tiles[row, col].Text == "-")
                            row--;
                        else if (Tiles[pivot, col].Text == "-")
                        {
                            Tiles[pivot, col].Text = Tiles[row, col].Text;
                            Tiles[row--, col].Text = "-";
                        }
                        else if (Tiles[pivot, col].Text == Tiles[row, col].Text)
                        {
                            Tiles[pivot--, col].Text += Tiles[row, col].Text;
                            RewriteTiles();
                            RecolorTiles();
                            Tiles[row--, col].Text = "-";
                        }
                        else if (--pivot == row)
                            row--;
                    }
                }
                RecolorTiles();
                SpawnNewTile();
                RecolorTiles();
            }
            else if(e.Key == Key.Left)
            {

            }
            else if (e.Key == Key.Right)
            {

            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
