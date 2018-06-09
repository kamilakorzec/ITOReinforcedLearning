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
        public PossibleDirections? Wall;

        public UIGridTile() { }
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        LearningRunner runner;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Act(object sender, RoutedEventArgs e)
        {

        }

        private void Train(object sender, RoutedEventArgs e)
        {
            int dimension = int.Parse(this.Dimension.Text);
            int tryCount = int.Parse(this.StepNumbers.Text);

            //runner = new LearningRunner(new src.Grid(), new int[] { 1, 2 });
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
                var column = new DataGridTemplateColumn
                {
                    Width = new DataGridLength(1, DataGridLengthUnitType.Star),
                    CellTemplate = new DataTemplate()
                };
                this.LearningGrid.Columns.Add(column);
            }

            double margin = 8;
            this.LearningGrid.RowHeight = (this.LearningGrid.RenderSize.Height - margin) / dimension;
            this.LearningGrid.ItemsSource = list;
        }

        private void SetWallsOrAgent(object sender, RoutedEventArgs e)
        {
            var cell = (DataGridCell) sender;
            DataTemplate content = (DataTemplate) cell.Content;

            FrameworkElementFactory factory = new FrameworkElementFactory(typeof(Image));

            UIGridTile tile = ((ObservableCollection<UIGridTile>) cell.DataContext)[cell.TabIndex];

            if(tile.Wall == null)
            {
                tile.Wall = PossibleDirections.UP;
            } else
            {
                switch(tile.Wall) {
                    case PossibleDirections.UP:
                        tile.Wall = PossibleDirections.RIGHT;
                        break;
                    case PossibleDirections.RIGHT:
                        tile.Wall = PossibleDirections.DOWN;
                        break;
                    case PossibleDirections.DOWN:
                        tile.Wall = PossibleDirections.LEFT;
                        break;
                    default:
                        tile.Wall = null;
                        break;
                }
            }
        }
    }
}
