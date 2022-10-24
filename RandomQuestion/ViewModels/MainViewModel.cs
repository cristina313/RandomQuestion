using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using RandomQuestion.Data;
using RandomQuestion.Models;
using RandomQuestion.Utils.Providers;
using RandomQuestion.Utils.Shuffling;
using System;
using System.Collections;
using System.ComponentModel;
using System.Threading;

namespace RandomQuestion.ViewModels
{
    public partial class MainWindowViewModel: ObservableValidator
    {
        private IDataService dataService;
        private QuestionsList questions;
        private int languageIndex = 0;
        private ArrayList shuffledQuestions;
        private int currentQ;
        private string[] languages;

        private static BackgroundWorker backgroundWorker;
        public MainWindowViewModel()
        {
            languages = new string[] { "ro", "en", "de" };
            dataService = Ioc.Default.GetService<IDataService>();
            
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

        #region Init
        private void Init()
        {
            var dirPath = $"{AppDomain.CurrentDomain.BaseDirectory}\\Assets\\Files\\";
            var qProvider = new QuestionsFileProvider(dataService, new QuestionsProviderOptions { DirectoryPath = dirPath });
            questions = qProvider.GetQuestions(languages[languageIndex]);
            ArrayList gotQ = GetQuestionsArray();
            shuffledQuestions = ShuffleManager.Shuffle(gotQ);
        }

        private ArrayList GetQuestionsArray()
        {
            var gotQ = new ArrayList();
            if (questions != null && questions.Questions != null)
            {
                foreach (var question in questions.Questions)
                {
                    gotQ.Add(question.Text);
                }
            }

            return gotQ;
        }
        #endregion

        #region BackgroundWorker
        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            OnStartBlinking();
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            MainText = e.UserState.ToString();
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            int nrQ = 20;
            int sec = 1;
            var subArr = ShuffleManager.PickUpSubArray(shuffledQuestions, nrQ);

            foreach (var q in subArr)
            {
                Thread.Sleep(7 * sec);
                backgroundWorker.ReportProgress(sec * (100 / nrQ), q);
                sec++;

            }
        }
        #endregion

        #region Properties
        [ObservableProperty]
        private string mainText = "...";

        [ObservableProperty]
        private Visibility qestionsVisible = Visibility.Collapsed;
        #endregion

        #region Commands
        [ICommand]
        public void Start()
        {
            QestionsVisible = Visibility.Visible;
            backgroundWorker.RunWorkerAsync();
        }
        #endregion

        #region Delegates
        public delegate void BlinkCallback();
        public event BlinkCallback StartBlinking;
        public void OnStartBlinking()
        {
            if (StartBlinking != null)
                StartBlinking.Invoke();

            if (currentQ < shuffledQuestions.Count)
            {
                MainText = shuffledQuestions[currentQ].ToString();
                currentQ++;
            }
            else
            {
                currentQ = 0;
                Init();
            }
        }
        #endregion
    }
}
