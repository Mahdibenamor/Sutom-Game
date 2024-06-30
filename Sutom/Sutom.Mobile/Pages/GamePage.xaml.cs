using Microsoft.Maui.Controls;
using Sutom.Core;
using Sutom.Mobile.Pages;
using Sutom.Mobile.ViewModels;

namespace Sutom.Mobile
{
    [ViewModel(typeof(GamePageViewModel))]
    public partial class GamePage : ContentPage, IBasePage<GamePageViewModel>
    {

        private GamePageViewModel _viewModel
        {
            get => BindingContext as GamePageViewModel;
            set => BindingContext = value;
        }

        public GamePage()
        {
            InitializeComponent();

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
        }

        private void CreateDynamicBoard()
        {
            // Clear existing dynamic content
            BoardStack.Children.Clear();

            // Create the dynamic board
            for (int rowIndex = 0; rowIndex < _viewModel.Board.Count; rowIndex++)
            {
                var row = _viewModel.Board[rowIndex];
                var grid = new Grid();
                for (int i = 0; i < _viewModel.WordLength; i++)
                {
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                }

                for (int colIndex = 0; colIndex < _viewModel.WordLength; colIndex++)
                {
                    var frame = new Frame { Padding = 5, Margin = 2 };
                    var entry = new Entry
                    {
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        MaxLength = 1,            
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

                BoardStack.Children.Add(grid);
            }
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs e, int rowIndex, int colIndex)
        {
            var entry = sender as Entry;

            if (entry.Text.Length == 1)
            {
                // Move to the next entry
                if (colIndex < _viewModel.WordLength - 1)
                {
                    // Move to the next column in the same row
                    var nextEntry = (Entry)((Frame)((Grid)BoardStack.Children[rowIndex]).Children[colIndex + 1]).Content;
                    nextEntry.Focus();
                }
                else if (rowIndex < _viewModel.MaxAttempts - 1)
                {
                    // Move to the first column of the next row
                    var nextEntry = (Entry)((Frame)((Grid)BoardStack.Children[rowIndex + 1]).Children[0]).Content;
                    nextEntry.Focus();
                }
            }
            else if (entry.Text.Length == 0 && e.OldTextValue != null)
            {
                // Move to the previous entry
                if (colIndex > 0)
                {
                    // Move to the previous column in the same row
                    var prevEntry = (Entry)((Frame)((Grid)BoardStack.Children[rowIndex]).Children[colIndex - 1]).Content;
                    prevEntry.Focus();
                }
                else if (rowIndex > 0)
                {
                    // Move to the last column of the previous row
                    var prevEntry = (Entry)((Frame)((Grid)BoardStack.Children[rowIndex - 1]).Children[_viewModel.WordLength - 1]).Content;
                    prevEntry.Focus();
                }
            }
        }
    }
}