using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using ITOReinforcedLearning.src;

namespace ITOReinforcedLearning
{
    class UIGridTile
    {
        public bool Agent = false;
        public bool Exit = false;
        public List<PossibleDirections> Walls = new List<PossibleDirections> { };

        public UIGridTile() { }
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Act(object sender, RoutedEventArgs e)
        {

        } 

        private void Train(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateGrid(object sender, RoutedEventArgs e)
        {
            ObservableCollection<ObservableCollection<UIGridTile>> list = new ObservableCollection<ObservableCollection<UIGridTile>>{ };
            this.LearningGrid.Columns.Clear();

            int dimension = int.Parse(this.Dimension.Text);

            for(int i = 0; i < dimension; i++)
            {
                var row = new ObservableCollection<UIGridTile> { };
                for(int j = 0; j < dimension; j++)
                {
                    row.Add(new UIGridTile());
                }

                list.Add(new ObservableCollection<UIGridTile>(row));
                var column = new DataGridTextColumn
                {
                    Width = new DataGridLength(1, DataGridLengthUnitType.Star)
                };
                this.LearningGrid.Columns.Add(column);
            }

            double margin = 8;
            this.LearningGrid.RowHeight = (this.LearningGrid.RenderSize.Height - margin) / dimension;
            this.LearningGrid.ItemsSource = list;
        }
    }
}
