using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ITOReinforcedLearning.Learning;

namespace ITOReinforcedLearning
{
    class UIGridTile
    {
        public bool Agent = false;
        public bool Exit = false;
        public PossibleDirections? Wall;

        public UIGridTile() { }
    }

    class AgentGridTile
    {
        public double Reward {get; set;}
        public bool Agent = false;

        public AgentGridTile(double reward, bool agent = false)
        {
            Reward = reward;
            Agent = agent;
        }
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LearningRunner runner;
        private Button lastAgentPositionIndicator;
        private Button lastExitPositionIndicator;
        private int[] agentPos;
        private Learning.Grid map;

        public MainWindow()
        {
            InitializeComponent();

            this.act.IsEnabled = false;
            this.train.IsEnabled = false;
        }

        private void Act(object sender, RoutedEventArgs e)
        {
            runner.Act(agentPos);
        }

        private void Train(object sender, RoutedEventArgs e)
        {
            int dimension = int.Parse(this.Dimension.Text);
            int tryCount = int.Parse(this.StepNumbers.Text);

            map = new Learning.Grid(dimension);

            ObservableCollection<ObservableCollection<UIGridTile>> rows = (ObservableCollection<ObservableCollection<UIGridTile>>) this.LearningGrid.ItemsSource;

            int r = 0;
            foreach(ObservableCollection<UIGridTile> row in rows)
            {
                int col = 0;
                foreach(UIGridTile tile in row)
                {
                    map.AddTile(new Tile(tile.Wall, new int[] { r, col }, tile.Exit));
                    col++;
                }
                r++;
            }

            runner = new LearningRunner(map, agentPos, dimension * tryCount);
            
            runner.Learn(agentPos);

            this.act.IsEnabled = true;

            PopulateAgentGrid(map);
        }

        private void UpdateGrid(object sender, RoutedEventArgs e)
        {
            PopulateGrid();
        }

        private void PopulateGrid()
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


        private void PopulateAgentGrid(Learning.Grid grid)
        {
            ObservableCollection<ObservableCollection<AgentGridTile>> list = new ObservableCollection<ObservableCollection<AgentGridTile>> { };
            this.AgentGrid.Columns.Clear();

            int dimension = grid.Dimension;

            for (int i = 0; i < dimension; i++)
            {
                var row = new ObservableCollection<AgentGridTile> { };
                for (int j = 0; j < dimension; j++)
                {
                    Tile tile = grid.GetTileByCoordinates(i, j);

                    row.Add(new AgentGridTile(tile.getAverageReward()));
                }

                list.Add(new ObservableCollection<AgentGridTile>(row));
                var column = new DataGridTextColumn
                {
                    Width = new DataGridLength(1, DataGridLengthUnitType.Star),
                };
                this.AgentGrid.Columns.Add(column);
            }

            double margin = 8;
            this.AgentGrid.RowHeight = (this.AgentGrid.RenderSize.Height - margin) / dimension;
            this.AgentGrid.ItemsSource = list;
        }

        private void SetWallUp(object sender, RoutedEventArgs e)
        {
            ToggleWall((Button) sender, PossibleDirections.UP);
        }

        private void SetWallRight(object sender, RoutedEventArgs e)
        {
            ToggleWall((Button)sender, PossibleDirections.RIGHT);
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

                if (lastExitPositionIndicator != null)
                {
                    this.train.IsEnabled = true;
                }
                lastAgentPositionIndicator = button;
            }
            else
            {
                lastAgentPositionIndicator = null;
                this.train.IsEnabled = false;
            }
        }

        private void SetExit(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            UIGridTile tile = ((ObservableCollection<UIGridTile>)button.DataContext)
                [((button.Parent as System.Windows.Controls.Grid).TemplatedParent as DataGridCell).TabIndex];

            tile.Exit = !tile.Exit;

            button.Background = tile.Exit ? Brushes.DarkSeaGreen : Brushes.LightGray;

            if (tile.Exit)
            {
                ClearPreviousExits(tile);
                if (lastExitPositionIndicator != null)
                {
                    lastExitPositionIndicator.Background = Brushes.LightGray;
                }

                if (lastAgentPositionIndicator != null)
                {
                    this.train.IsEnabled = true;
                }
                lastExitPositionIndicator = button;
            }
            else
            {
                lastExitPositionIndicator = null;
                this.train.IsEnabled = false;
            }
        }

        private void ToggleWall(Button cellButton, PossibleDirections wallPosition)
        {
            this.act.IsEnabled = false;
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

                if(wallPosition == PossibleDirections.UP)
                {

                } else // right
                {
                    try {
                        UIGridTile nextTile = ((ObservableCollection<UIGridTile>)cellButton.DataContext)
                            [((cellButton.Parent as System.Windows.Controls.Grid).TemplatedParent as DataGridCell).TabIndex+1];

                        nextTile.Wall = PossibleDirections.LEFT;
                    } catch(ArgumentOutOfRangeException e) //index out of bounds - last column
                    {
                        Console.Write(e);
                    };
                }
            }
        }

        private void ClearWallsInCell(System.Windows.Controls.Grid cell, Button wall)
        {
            foreach(UIElement button in cell.Children)
            {
                if(button != wall && button != lastAgentPositionIndicator && button != lastExitPositionIndicator)
                {
                    (button as Button).Background = Brushes.LightGray;
                }
            }
        }

        private void ClearPreviousAgents(UIGridTile currentAgentPosition)
        {
            int r = 0;
            foreach(ObservableCollection<UIGridTile> row in LearningGrid.ItemsSource)
            {
                int c = 0;
                foreach (UIGridTile tile in row)
                {
                    if (tile.Agent)
                    {
                        if (tile != currentAgentPosition)
                        {
                            tile.Agent = false;
                            DataGridRow gridRow = (DataGridRow)LearningGrid.ItemContainerGenerator.ContainerFromItem(row);
                        }
                        else
                        {
                            agentPos = new int[] { r, c };
                        }
                    }
                    c++;
                }
                r++;
            }
        }

        private void ClearPreviousExits(UIGridTile currentExitPosition)
        {
            foreach (ObservableCollection<UIGridTile> row in LearningGrid.ItemsSource)
            {
                foreach (UIGridTile tile in row)
                {
                    if (tile.Exit && tile != currentExitPosition)
                    {
                        tile.Exit = false;
                        DataGridRow gridRow = (DataGridRow)LearningGrid.ItemContainerGenerator.ContainerFromItem(row);
                    }
                }
            }
        }
    }
}
