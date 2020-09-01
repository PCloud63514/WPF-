using OraganismSimulation.Core;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace OraganismSimulation.Views
{
    /// <summary>
    /// PlanetView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PlanetView : UserControl
    {
        private ObservableCollection<Ellipse> ElipseCollection { get; } = new ObservableCollection<Ellipse>();
        public PlanetView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Step이 변경되었을 때 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">각 유닛 및 지형에 대한 정보.</param>
        private void StepChanged(object sender, object e)
        {
            
        }

        private void Grid_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            //TEST Code

            Ellipse ellipse = new Ellipse();
            ellipse.Width = 50;
            ellipse.Height = 50;
            ellipse.Fill = new SolidColorBrush(Colors.Black);

            canvas.Children.Add(ellipse);

            Canvas.SetLeft(ellipse, 150);
            Canvas.SetTop(ellipse, 100);

            GlobalInstance.Instance.Broadcaster.Register(Properties.Resources.Broadcast_StepChanged, StepChanged);
        }
    } 
}
