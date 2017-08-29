using System.Collections.Generic;
using System.ComponentModel;

namespace Blake.UI
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            Items = new List<Person>
                {
                    new Person {LastName = "Иванов", FirstName = "Иван", Patronymic = "Иванович"},
                    new Person {LastName = "Петров", FirstName = "Петр", Patronymic = "Петрович"},
                    new Person {LastName = "Сидоров", FirstName = "Валерий", Patronymic = "Валентинович"},
                    new Person {LastName = "Скотников", FirstName = "Олег", Patronymic = "Владимирович"}
                };

            var bgWorker = new BackgroundWorker {WorkerReportsProgress = true};
            bgWorker.DoWork += bgWorker_DoWork;
            bgWorker.RunWorkerCompleted += bgWorker_RunWorkerCompleted;
            bgWorker.RunWorkerAsync();
        }

        void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var s = sender as BackgroundWorker;
            if (s == null) return;
            
            while (!s.CancellationPending)
            {
                System.Threading.Thread.Sleep(100);
                if (ProgressBarValue == 100)
                    ProgressBarValue = 0;
                else
                {
                    ProgressBarValue++;
                }
            }
        }

        private IList<Person> _items;
        private int _progressBarValue;

        public IList<Person> Items
        {
            get { return _items; }
            set { _items = value; OnPropertyChanged("Items"); }
        }

        public int ProgressBarValue
        {
            get { return _progressBarValue; }
            set { _progressBarValue = value; OnPropertyChanged("ProgressBarValue"); }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        private void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
