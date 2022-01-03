using System.Windows.Controls;
using System.Windows.Input;
using Zadanie.Helper;
using Zadanie.Views;
using System.Data.SqlClient;
using System.Collections.ObjectModel;

namespace Zadanie.ViewModels
{
    struct Package
    {
        public string Lokalizacja { get; set; }
        public int Wartość { get; set; }
    }
    class MainWindowViewModel : BaseViewModel
    {
        #region Fields
        private string _Login;
        private bool _IsEnabled;
        private ObservableCollection<Package> _ItemsList;
        private SqlConnection cnn;
        #endregion
        public MainWindowViewModel()
        {
            _ItemsList = new ObservableCollection<Package>();
            IsEnabled = false;
        }
        #region Commands
        public ObservableCollection<Package> ItemsList
        {
            get
            {
                return _ItemsList;
            }
            set
            {
                if(value!=null)
                {
                    _ItemsList = value;
                }
            }
        }
        public bool IsEnabled
        {
            get
            {
                return _IsEnabled;
            }
            set
            {
                    _IsEnabled = value;
            }
        }
        public string Login
        {
            get
            {
                return _Login;
            }
            set
            {
                if(value!=null)
                {
                    _Login = value;
                }
            }
        }
        #endregion
        #region Commands
        private BaseCommand _LoginCommand;
        public ICommand LoginCommand
        {
            get
            {
                if (_LoginCommand == null)
                {
                    _LoginCommand = new BaseCommand(param=>LoginClick(param));
                }
                    
                return _LoginCommand;
            }
        }
        private BaseCommand _LoadCommand;
        public ICommand LoadCommand
        {
            get
            {
                if (_LoadCommand == null)
                {
                    _LoadCommand = new BaseCommand(LoadClick, () => IsEnabled);
                }
                return _LoadCommand;
            }
        }
        #endregion
        #region Private Helpers
        private void LoginClick(object parameter)
        {
            var passwordBox = (PasswordBox)parameter;
            if (!string.IsNullOrEmpty(_Login) && !string.IsNullOrEmpty(passwordBox.Password))
            {
                try
                {
                    string connetionString;
                    connetionString = @"Data Source=localhost;Initial Catalog=DevData;User ID=" + Login + ";Password=" + passwordBox.Password + ";";
                    cnn = new SqlConnection(connetionString);
                    cnn.Open();

                    _= new Popup("UDANE LOGOWANIE", "POMYŚLNIE POŁĄCZONO Z BAZĄ DANYCH");
                    IsEnabled = true;

                }
                catch (SqlException e)
                {
                    _ = new Popup("BŁĄD SQL", "WYSTĄPIŁ BŁĄD : " + e.Number);

                }

            }

            else
            {
                _ = new Popup("BŁĄD LOGOWANIA", "LOGIN/HASŁO NIE MOGĄ BYĆ PUSTE");
            }

        }
        private void LoadClick()
        {
            if (ItemsList.Count>0) ItemsList.Clear();

            SqlCommand cmd = new SqlCommand
            {
                Connection = cnn,
                CommandType = System.Data.CommandType.Text,
                CommandText = "exec SearchAllTables"
            };
            SqlDataReader sdr = cmd.ExecuteReader();

            while(sdr.Read())
            {
                Package p = new Package
                {
                    Lokalizacja = sdr.GetString(0),
                    Wartość = int.Parse(sdr.GetString(1))
                };
                ItemsList.Add(p);
            }

            sdr.Close();
            cnn.Close();
            IsEnabled = false;
        }
        #endregion
    }
}

