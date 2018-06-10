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
        private LearningRunner runner;
        private Button lastAgentPositionIndicator;

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

            ObservableCollection<ObservableCollection<UIGridTile>> rows = (ObservableCollection<ObservableCollection<UIGridTile>>) this.LearningGrid.ItemsSource;

            foreach(ObservableCollection<UIGridTile> row in rows)
            {
                foreach(UIGridTile tile in row)
                {
                    // build an actual Grid
                }
            }
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
                };
                this.LearningGrid.Columns.Add(column);
            }

            double margin = 8;
            this.LearningGrid.RowHeight = (this.LearningGrid.RenderSize.Height - margin) / dimension;
            this.LearningGrid.ItemsSource = list;
        }

        private void SetWallUp(object sender, RoutedEventArgs e)
        {
            ToggleWallOrExit((Button) sender, PossibleDirections.UP);
        }

        private void SetWallLeft(object sender, RoutedEventArgs e)
        {
            ToggleWallOrExit((Button)sender, PossibleDirections.LEFT);
        }

        private void SetWallDown(object sender, RoutedEventArgs e)
        {
            ToggleWallOrExit((Button)sender, PossibleDirections.DOWN);
        }

        private void SetWallRight(object sender, RoutedEventArgs e)
        {
            ToggleWallOrExit((Button)sender, PossibleDirections.RIGHT);
        }

        private void SetAgent(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            UIGridTile tile = ((ObservableCollection<UIGridTile>)button.DataContext)
                [((button.Parent as System.Windows.Controls.Grid).TemplatedParent as DataGridCell).TabIndex];

            tile.Agent = !tile.Agent;

            button.Background = tile.Agent ? Brushes.DarkSlateBlue : Brushes.LightGray;

            if (tile.Agent)
            {
                ClearPreviousAgents(tile);
                if (lastAgentPositionIndicator != null) {
                    lastAgentPositionIndicator.Background = Brushes.LightGray;
                }
                lastAgentPositionIndicator = button;
            }
            else
            {
                lastAgentPositionIndicator = null;
            }
        }

        private void ToggleWallOrExit(Button cellButton, PossibleDirections wallPosition)
        {
            UIGridTile tile = ((ObservableCollection<UIGridTile>)cellButton.DataContext)
                [((cellButton.Parent as System.Windows.Controls.Grid).TemplatedParent as DataGridCell).TabIndex];

            if (tile.Wall == wallPosition)
            {
                tile.Wall = null;
                cellButton.Background = Brushes.LightGray;
            }
            else
            {
                tile.Wall = wallPosition;
                cellButton.Background = Brushes.IndianRed;
                ClearWallsInCell((System.Windows.Controls.Grid) cellButton.Parent, cellButton);
            }
        }

        private void ClearWallsInCell(System.Windows.Controls.Grid cell, Button wall)
        {
            foreach(UIElement button in cell.Children)
            {
                if(button != wall && button != lastAgentPositionIndicator)
                {
                    (button as Button).Background = Brushes.LightGray;
                }
            }
        }

        private void ClearPreviousAgents(UIGridTile currentAgentPosition)
        {
            foreach(ObservableCollection<UIGridTile> row in LearningGrid.ItemsSource)
            {
                foreach (UIGridTile tile in row)
                {
                    if (tile.Agent && tile != currentAgentPosition)
                    {
                        tile.Agent = false;
                        DataGridRow gridRow = (DataGridRow)LearningGrid.ItemContainerGenerator.ContainerFromItem(row);
                    }
                }
            }
        }
    }
}
