﻿using Microsoft.UI.Xaml;
using RandomQuestion.Utils;

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
        }
    }
}
