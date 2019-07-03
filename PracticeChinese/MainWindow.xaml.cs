using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using Newtonsoft.Json;

namespace PracticeChinese
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private ObservableCollection<ChineseCharacter> _Characters;
        private ChineseCharacter _CurrentChar;
        private string _Pinyin_1;
        private string _Pinyin_2;
        private string _Pinyin_3;
        private string _Pinyin_4;
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Helper method
        /// </summary>
        private void OnPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion


        public ObservableCollection<ChineseCharacter> Characters
        {
            get { return _Characters; }
            set
            {
                if (value != null || value != _Characters) _Characters = value;
                OnPropertyChanged();
            }
        }

        public Random Random { get; private set; }



        public string Pinyin_1
        {
            get { return _Pinyin_1; }
            set
            {
                if (value != null || value != _Pinyin_1) _Pinyin_1 = value;
                OnPropertyChanged();
            }
        }

        public string Pinyin_2
        {
            get { return _Pinyin_2; }
            set
            {
                if (value != null || value != _Pinyin_2) _Pinyin_2 = value;
                OnPropertyChanged();
            }
        }

        public string Pinyin_3
        {
            get { return _Pinyin_3; }
            set
            {
                if (value != null || value != _Pinyin_3) _Pinyin_3 = value;
                OnPropertyChanged();
            }
        }

        public string Pinyin_4
        {
            get { return _Pinyin_4; }
            set
            {
                if (value != null || value != _Pinyin_4) _Pinyin_4 = value;
                OnPropertyChanged();
            }
        }

        public ChineseCharacter CurrentChar
        {
            get { return _CurrentChar; }
            set
            {
                if (value != null || value != _CurrentChar) _CurrentChar = value;
                OnPropertyChanged();
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            this.Closing += this.MainWindow_Closing;
            this.Characters = new ObservableCollection<ChineseCharacter>();
            this.LoadData();
        }

        private void LoadData()
        {
            try
            {
                using (System.IO.StreamReader reader = new System.IO.StreamReader("data.json"))
                {
                    string json = reader.ReadToEnd();
                    this.Characters = JsonConvert.DeserializeObject<ObservableCollection<ChineseCharacter>>(json);
                    foreach (ChineseCharacter item in Characters)
                    {
                        item.Level = 50;
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.WriteJson();
        }

        /// <summary>
        /// Write data 
        /// </summary>
        private void WriteJson()
        {
            string json = JsonConvert.SerializeObject(this.Characters);
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter("data.json"))
            {
                writer.Write(json);
                writer.Close();
            }
        }

        private void AddNewChar_Click(object sender, RoutedEventArgs e)
        {
            this.AddNewChar();
        }

        private void AddNewChar()
        {
            NewChar newChar = new NewChar();
            newChar.CharacterAdded += this.NewChar_CharacterAdded;
            newChar.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Hide();
            var dialogResult = newChar.ShowDialog();
            this.Show();
        }

        private void NewChar_CharacterAdded(object sender, ChineseCharacter newChar)
        {
            if (this.Characters.Any(c => c.UChar == newChar.UChar))
            {
                MessageBox.Show("Character existed!");
                return;
            }
            this.Characters.Add(newChar);
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            this.Random = new Random();
            this.LoadNextChar();
        }

        private void LoadNextChar()
        {
            this.Button_A.IsEnabled = true;
            this.Button_B.IsEnabled = true;
            this.Button_C.IsEnabled = true;
            this.Button_D.IsEnabled = true;

            this.CurrentChar = this.Characters[this.Random.Next(0, this.Characters.Count - 1)];
            this.Pinyin_1 = this.Characters[this.Random.Next(0, this.Characters.Count - 1)].Pinyin;
            this.Pinyin_2 = this.Characters[this.Random.Next(0, this.Characters.Count - 1)].Pinyin;
            this.Pinyin_3 = this.Characters[this.Random.Next(0, this.Characters.Count - 1)].Pinyin;
            this.Pinyin_4 = this.Characters[this.Random.Next(0, this.Characters.Count - 1)].Pinyin;
            int i = this.Random.Next(0, 4);
            switch (i)
            {
                case 0:
                    this.Pinyin_1 = this.CurrentChar.Pinyin;
                    break;
                case 1:
                    this.Pinyin_2 = this.CurrentChar.Pinyin;
                    break;
                case 2:
                    this.Pinyin_3 = this.CurrentChar.Pinyin;
                    break;
                case 3:
                    this.Pinyin_4 = this.CurrentChar.Pinyin;
                    break;
                default:
                    break;
            }
        }

        private void Button_Ans_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string pinyin = button.Content.ToString();
            if (pinyin == this.CurrentChar.Pinyin)
            {
                this.CurrentChar.Level = Math.Min(this.CurrentChar.Level+1, 100);
                this.LoadNextChar();
            } else
            {
                button.IsEnabled = false;
            }
        }
    }
}