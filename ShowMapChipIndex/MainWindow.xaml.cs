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

namespace ShowMapChipIndex
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            var files = e.Data.GetData(DataFormats.FileDrop, true) as String[];
            if (files != null)
            {
                var file = files[0];
                try
                {
                    this.image.Source = new BitmapImage(new Uri(file));
                    this.Width = (this.image.Source as BitmapImage).PixelWidth + 35;
                    this.MaxWidth = this.Width;

                    this.tutorialMSG.Visibility = Visibility.Collapsed;

                    this.showMapChipIndex(int.Parse(this.chipSize.Text));
                }
                catch (Exception es)
                {
                    MessageBox.Show(es.StackTrace);
                }
            }
        }

        private void showMapChipIndex(int size)
        {
            this.canvas.Children.Clear();

            int widthSize = (this.image.Source as BitmapImage).PixelWidth / size;
            int heightSize = (this.image.Source as BitmapImage).PixelHeight / size;

            int mapNum = widthSize * heightSize;
            for (int i = 0; i < mapNum; i++)
            {
                int x = i % widthSize;
                int y = i / widthSize;

                // Border
                var border = new Border()
                {
                    Width = size,
                    Height = size,
                    BorderBrush = Brushes.Black,
                    BorderThickness = new Thickness(1)
                };
                Canvas.SetLeft(border, x*size);
                Canvas.SetTop(border, y*size);

                // Border Child
            
                // TextBlock
                var textBlock = new TextBlock()
                {
                    Text = i.ToString(),
                    TextAlignment = TextAlignment.Center,
                    Foreground = Brushes.White
                };

                // Rectangle
                var rectangle = new Rectangle()
                {
                    Width = size,
                    Height = size,
                    Fill = Brushes.Black,
                    Opacity = 0.25
                };

                // Grid
                var innerGrid = new Grid();
                innerGrid.Children.Add(rectangle);
                innerGrid.Children.Add(textBlock);

                var borderCanvas = new Canvas();
                borderCanvas.Children.Add(innerGrid);
                border.Child = borderCanvas;

                this.canvas.Children.Add(border);
            }

            //this.scrollViewer.Measure(this.scrollViewer.DesiredSize);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.showMapChipIndex(int.Parse(this.chipSize.Text));
        }
    }
}
