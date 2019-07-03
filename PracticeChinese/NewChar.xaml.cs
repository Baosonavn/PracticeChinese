using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace PracticeChinese
{
    /// <summary>
    /// Interaction logic for NewChar.xaml
    /// </summary>
    public partial class NewChar : Window, INotifyPropertyChanged
    {
        private ChineseCharacter _ChineseCharacter;
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

        public ChineseCharacter ChineseCharacter
        {
            get { return _ChineseCharacter; }
            set
            {
                if (value != null || value != _ChineseCharacter) _ChineseCharacter = value;
                OnPropertyChanged();
            }
        }

        public event EventHandler<ChineseCharacter> CharacterAdded;

        public NewChar()
        {
            InitializeComponent();
            this.DataContext = this;
            this.ChineseCharacter = new ChineseCharacter();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ChineseCharacter chineseCharacter = new ChineseCharacter()
            {
                UChar = this.ChineseCharacter.UChar,
                Pinyin = this.ChineseCharacter.Pinyin,
                Definition = this.ChineseCharacter.Definition,
                Level = this.ChineseCharacter.Level
            };
            this.CharacterAdded?.Invoke(this, chineseCharacter);
            this.ChineseCharacter = new ChineseCharacter();
        }
    }
}
