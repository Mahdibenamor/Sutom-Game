using Sutom.Core;
using Sutom.Mobile.Pages;
using Sutom.Mobile.ViewModels;
using System.Collections.ObjectModel;

namespace Sutom.Mobile
{
    [ViewModel(typeof(GamePageViewModel))]
    public partial class GamePage : ContentPage, IBasePage<GamePageViewModel>
    {
        Dictionary<int, Grid> enteriesMap = new();
        private GamePageViewModel _viewModel
        {
            get => BindingContext as GamePageViewModel;
            set => BindingContext = value;
        }

        public GamePage()
        {
            InitializeComponent();
            checkButton.IsEnabled = false;

        }

        public void Initialize()
        {
            _viewModel.PropertyChanged += ViewModel_PropertyChanged;
            CreateDynamicBoard();
        }


        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.Board))
            {
                CreateDynamicBoard();
            }
            if (e.PropertyName == nameof(_viewModel.CurrentAttempt))
            {
                DisableEntryRow(_viewModel.CurrentAttempt - 1);
                EnableEntryRow(_viewModel.CurrentAttempt);
            }
            if (e.PropertyName == nameof(_viewModel.CurrentLetterIndex))
            {
            }
        }

        private void CreateDynamicBoard()
        {
            BoardStack.Children.Clear();

            for (int rowIndex = 0; rowIndex < _viewModel.Board.Count; rowIndex++)
            {
                ObservableCollection<Models.Cell> row = _viewModel.Board[rowIndex];
                var grid = BuildBoardRow(row, rowIndex, _viewModel.WordLength);
                BoardStack.Children.Add(grid);
            }
        }


        private Grid BuildBoardRow(ObservableCollection<Models.Cell> rowCells, int rowIndex, int rowLenght)
        {
            ObservableCollection<Models.Cell> row = rowCells;
            var grid = new Grid();
            for (int i = 0; i < rowLenght; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            }

            for (int colIndex = 0; colIndex < rowLenght; colIndex++)
            {
                var frame = new Frame { Padding = 5, Margin = 2 };
                var entry = new Entry
                {
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    MaxLength = 1,
                    IsEnabled = _viewModel.CurrentAttempt == rowIndex,
                };
                entry.SetBinding(Entry.TextProperty, new Binding($"[{colIndex}].Letter", source: row));
                frame.SetBinding(BackgroundColorProperty, new Binding($"[{colIndex}].BackgroundColor", source: row));
                frame.Content = entry;
                grid.Children.Add(frame);
                Grid.SetColumn(frame, colIndex);

                int localRowIndex = rowIndex;
                int localColIndex = colIndex;

                entry.TextChanged += (s, e) => OnEntryTextChanged(s, e, localRowIndex, localColIndex);
            }
            if (enteriesMap.ContainsKey(rowIndex))
            {
                enteriesMap[rowIndex] = grid;
            }
            else
            {
                enteriesMap.Add(rowIndex, grid);
            }
            return grid;
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs e, int rowIndex, int colIndex)
        {
            var entry = sender as Entry;

            if (entry.Text.Length == 1)
            {
                _viewModel.LetterCommand.Execute(entry.Text);
                if (colIndex < _viewModel.WordLength - 1)
                {
                    // Move to the next column in the same row
                    var nextEntry = (Entry)((Frame)((Grid)BoardStack.Children[rowIndex]).Children[colIndex + 1]).Content;
                    nextEntry.Focus();
                }

            }
            else if (entry.Text.Length == 0 && e.OldTextValue != null)
            {
                _viewModel.BackCommand.Execute(entry.Text.Length);
                if (colIndex > 0)
                {
                    // Move to the previous column in the same row
                    var prevEntry = (Entry)((Frame)((Grid)BoardStack.Children[rowIndex]).Children[colIndex - 1]).Content;
                    prevEntry.Focus();
                }
            }
            checkButton.IsEnabled = _viewModel.CurrentLetterIndex == _viewModel.WordLength;
        }

        private void EnableEntryRow(int rowIndex)
        {
            if (rowIndex < _viewModel.MaxAttempts && rowIndex > 0)
            {
                for (int colIndex = 0; colIndex < _viewModel.WordLength; colIndex++)
                {
                    var entryToEnable = (Entry)((Frame)((Grid)BoardStack.Children[rowIndex]).Children[colIndex]).Content;
                    var entryToDiable = (Entry)((Frame)((Grid)BoardStack.Children[rowIndex - 1]).Children[colIndex]).Content;
                    entryToEnable.IsEnabled = true;
                    entryToDiable.IsEnabled = false;
                }

                var nextEntry = (Entry)((Frame)((Grid)BoardStack.Children[rowIndex]).Children[0]).Content;
                nextEntry.Focus();
                checkButton.IsEnabled = false;
            }
        }
        private void DisableEntryRow(int rowIndex)
        {
            if (rowIndex < _viewModel.MaxAttempts && rowIndex > 0)
            {
                for (int colIndex = 0; colIndex < _viewModel.WordLength; colIndex++)
                {
                    var entryToDiable = (Entry)((Frame)((Grid)BoardStack.Children[rowIndex]).Children[colIndex]).Content;
                    entryToDiable.IsEnabled = false;
                }
            }
        }
    }
}