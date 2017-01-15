using SimpleTorrent.Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTorrent.viewmodels
{
    class SettingsVM : INotifyPropertyChanged
    {
        public String path { get; set; }
        public bool in_app_player
        {
            get
            {
                return Settings.getInAppPlayerStatus();
            }
            set
            {
                Settings.setInAppPlayer(value);
                NotifyPropertyChanged("in_app_player");
            }
        }

        public SettingsVM()
        {
            this.path = Settings.getDownloadsPath();
            this.in_app_player = Settings.getInAppPlayerStatus();
        }

        public void setPlayerStatus(bool value)
        {
            Settings.setInAppPlayer(value);
            this.in_app_player = value;
            NotifyPropertyChanged("in_app_player");
        }

        public void setPath(String path)
        {
            Settings.setDownloadsPath(path);
            this.path = path;
            NotifyPropertyChanged("path");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
