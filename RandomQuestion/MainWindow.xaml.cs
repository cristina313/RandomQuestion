using RandomQuestion.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace RandomQuestion
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ArrayList shuffledQuestions;
        int currentQ;
        private static BackgroundWorker backgroundWorker;
        private IDataManager dataManager;
        public MainWindow()
        {
            InitializeComponent();
            var databaseSettings = new DatabaseSettings();
            dataManager = new DatabaseManager(databaseSettings);
            Init();
            
            backgroundWorker = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            backgroundWorker.DoWork += backgroundWorker_DoWork;
            backgroundWorker.ProgressChanged += backgroundWorker_ProgressChanged;
            backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Blink(lblMain, 300, 3);
            if (currentQ < shuffledQuestions.Count)
            {
                lblQuestion.Text = shuffledQuestions[currentQ].ToString();
                currentQ++;
            }
            else
            {
                currentQ = 0;
                Init();
            }
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lblQuestion.Text = e.UserState.ToString();
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            int nrQ = 20; 
            int sec = 1;
            var subArr = ShuffleManager.PickUpSubArray(shuffledQuestions, nrQ);
            
            foreach (var q in subArr)
            {
                Thread.Sleep(7 * sec);
                backgroundWorker.ReportProgress(sec*(100/nrQ),q);
                sec++;
                
            }
        }

        private void Init()
        {
            try
            {
                var gotQuestions = dataManager.ReadQuestions();
                shuffledQuestions = ShuffleManager.Shuffle(gotQuestions);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ioi no'! Sigur ai pus fisieru' unde trăbă? Uită ni ce zice programu': {ex.Message}","Mesaj de baraj",MessageBoxButton.OK,MessageBoxImage.Exclamation);
            }
        }

        private void btnHit_Click(object sender, RoutedEventArgs e)
        {
            if (shuffledQuestions == null) 
            {
                MessageBox.Show("Apăi nicio întrebare nu s-o loadat! Verifică te rog amu' fișieru' cu întrebări!");
                return;
            }

            backgroundWorker.RunWorkerAsync();
        }

        private void Blink(Label label, int length, int times)
        {
            

            var doubleAnimation = new DoubleAnimation
            {
                From = 1.0,
                To = 0.0,
                Duration = new Duration(TimeSpan.FromMilliseconds(length)),
                AutoReverse = true,
                RepeatBehavior = new RepeatBehavior(times)
            };
            Storyboard storyBoard = new Storyboard();
            storyBoard.Children.Add(doubleAnimation);
            Storyboard.SetTarget(doubleAnimation, label);
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("Opacity"));
            storyBoard.Begin(label);
        }
    }
}
