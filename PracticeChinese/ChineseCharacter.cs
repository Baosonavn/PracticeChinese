using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PracticeChinese
{
    public class ChineseCharacter: INotifyPropertyChanged
    {
        private string _Character;
        private string _Pinyin;
        private string _Definition;
        private int _Level;
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


        public string UChar
        {
            get { return _Character; }
            set
            {
                if (value != null || value != _Character) _Character = value;
                OnPropertyChanged();
            }
        }

        public string Pinyin
        {
            get { return _Pinyin; }
            set
            {
                if (value != null || value != _Pinyin) _Pinyin = value;
                OnPropertyChanged();
            }
        }

        public string Definition
        {
            get { return _Definition; }
            set
            {
                if (value != null || value != _Definition) _Definition = value;
                OnPropertyChanged();
            }
        }

        public int Level
        {
            get { return _Level; }
            set
            {
                if (value != _Level) _Level = value;
                OnPropertyChanged();
            }
        }

        public ChineseCharacter()
        {
            Level = 0;
        }
    }

}
