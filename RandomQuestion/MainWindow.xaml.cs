using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media.Animation;
using RandomQuestion.Utils;
using RandomQuestion.ViewModels;
using System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace RandomQuestion
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            this.Title = Constants.TITLE;
            ExtendsContentIntoTitleBar = true;
            SetTitleBar(TitleBar);

            ViewModel = Ioc.Default.GetService<MainWindowViewModel>();
            ViewModel.StartBlinking += Animation_StartBlinking;
        }

        private void Animation_StartBlinking()
        {
            var doubleAnimation = new DoubleAnimation
            {
                From = 1.0,
                To = 0.0,
                Duration = new Duration(TimeSpan.FromMilliseconds(300)),
                AutoReverse = true,
                RepeatBehavior = new RepeatBehavior(3)
            };
            Storyboard storyBoard = new Storyboard();
            storyBoard.Children.Add(doubleAnimation);
            Storyboard.SetTarget(doubleAnimation, mainText);
            Storyboard.SetTargetProperty(doubleAnimation, "Opacity");
            storyBoard.Begin();
        }

        public MainWindowViewModel? ViewModel { get; }
    }
}
