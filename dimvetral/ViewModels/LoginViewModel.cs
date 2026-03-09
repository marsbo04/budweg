using dimvetral.Models.Repo;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace dimvetral.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly ILoginAuthenticator _authenticator;
        private string _userId;
        private string _password;
        private string _errorMessage;

        public event PropertyChangedEventHandler? PropertyChanged;

        public LoginViewModel(ILoginAuthenticator authenticator)
        {
   
        }


    }
}