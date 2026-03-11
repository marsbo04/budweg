using dimvetral.Models.Repo;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace dimvetral.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly ILoginAuthenticator _authenticator;
        private string _userId;
        private string _password;
        private string _errorMessage;

        public LoginViewModel(ILoginAuthenticator authenticator) // det er et interface? 
        {
   
        }


    }
}