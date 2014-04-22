using Caliburn.Micro;
using CaptureTheFlag.Models;
using CaptureTheFlag.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CaptureTheFlag.ViewModels
{
    public class UserViewModel : Screen
    {
        private readonly INavigationService navigationService;
        private readonly IEventAggregator eventAggregator;
        private readonly ICommunicationService communicationService;

        public UserViewModel(INavigationService navigationService, IEventAggregator eventAggregator, ICommunicationService communicationService)
        {
            DebugLogger.WriteLine("", this.GetType(), MethodBase.GetCurrentMethod());
            this.navigationService = navigationService;
            this.eventAggregator = eventAggregator;
            this.communicationService = communicationService;

            IsFormAccessible = true;

            User = new User();
#warning Temporary variables
            User.url = "http://78.133.154.39:8888/api/users/4/";

            DisplayName = "User";
            UrlTextBlock = "Url:";
            UsernameTextBlock = "Username:";
            FirstNameTextBlock = "First name:";
            LastNameTextBlock = "Last name";
            EmailTextBlock = "E-mail:";
            PasswordTextBlock = "Password:";
            NickTextBlock = "Nick:";
            DeviceTypeTextBlock = "Device type:";
            DeviceIdTextBlock = "Device id:";
            LatTextBlock = "Latitude:";
            LonTextBlock = "Longitude:";

            ReadCharacterButton = "View character";
            ReadButton = "Read";
            UpdateButton = "Update";
            UpdateSelectiveButton = "Patch";
            DeleteButton = "Delete";
        }

        #region ViewModel States
        protected override void OnActivate()
        {
            DebugLogger.WriteLine("", this.GetType(), MethodBase.GetCurrentMethod());
            base.OnActivate();
            eventAggregator.Subscribe(this);
            //User.url = UserModelKey;
            //Game = IoC.Get<GlobalStorageService>().Current.Games[GameModelKey];
        }

        protected override void OnDeactivate(bool close)
        {
            DebugLogger.WriteLine("", this.GetType(), MethodBase.GetCurrentMethod());
            if(close)
            {
                eventAggregator.Unsubscribe(this);
            }
            base.OnDeactivate(close);
        }
        #endregion

        #region Actions
        public void ReadCharacterAction()
        {
            DebugLogger.WriteLine("", this.GetType(), MethodBase.GetCurrentMethod());
            navigationService.UriFor<CharacterViewModel>()
                .WithParam(param => param.CharacterModelKey, "http://78.133.154.39:8888/api/characters/15/")
                .WithParam(param => param.Token, Token)
                .Navigate();
        }

        public void SelectCharacterAction()
        {
            DebugLogger.WriteLine("", this.GetType(), MethodBase.GetCurrentMethod());
            Character chara = new Character();
            chara.url = "http://78.133.154.39:8888/api/characters/15/";
            communicationService.SelectCharacter(chara, Token,
                responseData =>
                {
                    DebugLogger.WriteLine("Successful create callback", this.GetType(), MethodBase.GetCurrentMethod());
                    MessageBox.Show("OK", "read", MessageBoxButton.OK);
                    //User = responseData;
                },
                serverErrorMessage =>
                {
                    DebugLogger.WriteLine("Failed create callback", this.GetType(), MethodBase.GetCurrentMethod());
                    MessageBox.Show(serverErrorMessage.Code.ToString(), serverErrorMessage.Message, MessageBoxButton.OK);
                }
            );
        }

        public void ReadAction()
        {
            DebugLogger.WriteLine("", this.GetType(), MethodBase.GetCurrentMethod());
            communicationService.ReadUser(User, Token,
                responseData =>
                {
                    DebugLogger.WriteLine("Successful create callback", this.GetType(), MethodBase.GetCurrentMethod());
                    MessageBox.Show("OK", "read", MessageBoxButton.OK);
                    User = responseData;
                },
                serverErrorMessage =>
                {
                    DebugLogger.WriteLine("Failed create callback", this.GetType(), MethodBase.GetCurrentMethod());
                    MessageBox.Show(serverErrorMessage.Code.ToString(), serverErrorMessage.Message, MessageBoxButton.OK);
                }
            );
        }

        public void DeleteAction()
        {
            communicationService.DeleteUser(User, Token,
            responseUserMap =>
            {
                DebugLogger.WriteLine("Successful create callback", this.GetType(), MethodBase.GetCurrentMethod());
                MessageBox.Show("OK", "deleted", MessageBoxButton.OK);
                IsFormAccessible = true;
            },
            serverErrorMessage =>
            {
                DebugLogger.WriteLine("Failed create callback", this.GetType(), MethodBase.GetCurrentMethod());
                MessageBox.Show(serverErrorMessage.Code.ToString(), serverErrorMessage.Message, MessageBoxButton.OK);
                IsFormAccessible = true;
            }
        );
        }

        public void UpdateAction()
        {
            DebugLogger.WriteLine("", this.GetType(), MethodBase.GetCurrentMethod());
            communicationService.UpdateUser(User, Token,
                responseUserMap =>
                {
                    DebugLogger.WriteLine("Successful create callback", this.GetType(), MethodBase.GetCurrentMethod());
                    User = responseUserMap;
                    eventAggregator.Publish(User);
                    IsFormAccessible = true;
                },
                serverErrorMessage =>
                {
                    DebugLogger.WriteLine("Failed create callback", this.GetType(), MethodBase.GetCurrentMethod());
                    MessageBox.Show(serverErrorMessage.Code.ToString(), serverErrorMessage.Message, MessageBoxButton.OK);
                    IsFormAccessible = true;
                }
            );
        }

        public void UpdateSelectiveAction()
        {
            DebugLogger.WriteLine("", this.GetType(), MethodBase.GetCurrentMethod());
            User selectedFields = User;
            communicationService.UpdateUserFields(User, Token,
                responseUserMap =>
                {
                    DebugLogger.WriteLine("Successful create callback", this.GetType(), MethodBase.GetCurrentMethod());
                    User = responseUserMap;
                    eventAggregator.Publish(User);
                    IsFormAccessible = true;
                },
                serverErrorMessage =>
                {
                    DebugLogger.WriteLine("Failed create callback", this.GetType(), MethodBase.GetCurrentMethod());
                    MessageBox.Show(serverErrorMessage.Code.ToString(), serverErrorMessage.Message, MessageBoxButton.OK);
                    IsFormAccessible = true;
                }
            );
        }
        #endregion

        #region Properties

        #region Model Properties
        private User user;
        public User User
        {
            get { return user; }
            set
            {
                if (user != value)
                {
                    user = value;
                    NotifyOfPropertyChange(() => User);
                }
            }
        }

        private string userModelKey;
        public string UserModelKey
        {
            get { return userModelKey; }
            set
            {
                if (userModelKey != value)
                {
                    userModelKey = value;
                    NotifyOfPropertyChange(() => UserModelKey);
                }
            }
        }
        #endregion

        #region UI Properties
        private string urlTextBlock;
        public string UrlTextBlock
        {
            get { return urlTextBlock; }
            set
            {
                if (urlTextBlock != value)
                {
                    urlTextBlock = value;
                    NotifyOfPropertyChange(() => UrlTextBlock);
                }
            }
        }

        private string usernameTextBlock;
        public string UsernameTextBlock
        {
            get { return usernameTextBlock; }
            set
            {
                if (usernameTextBlock != value)
                {
                    usernameTextBlock = value;
                    NotifyOfPropertyChange(() => UsernameTextBlock);
                }
            }
        }

        private string firstNameTextBlock;
        public string FirstNameTextBlock
        {
            get { return firstNameTextBlock; }
            set
            {
                if (firstNameTextBlock != value)
                {
                    firstNameTextBlock = value;
                    NotifyOfPropertyChange(() => FirstNameTextBlock);
                }
            }
        }

        private string lastNameTextBlock;
        public string LastNameTextBlock
        {
            get { return lastNameTextBlock; }
            set
            {
                if (lastNameTextBlock != value)
                {
                    lastNameTextBlock = value;
                    NotifyOfPropertyChange(() => LastNameTextBlock);
                }
            }
        }

        private string emailTextBlock;
        public string EmailTextBlock
        {
            get { return emailTextBlock; }
            set
            {
                if (emailTextBlock != value)
                {
                    emailTextBlock = value;
                    NotifyOfPropertyChange(() => EmailTextBlock);
                }
            }
        }

        private string passwordTextBlock;
        public string PasswordTextBlock
        {
            get { return passwordTextBlock; }
            set
            {
                if (passwordTextBlock != value)
                {
                    passwordTextBlock = value;
                    NotifyOfPropertyChange(() => PasswordTextBlock);
                }
            }
        }

        private string nickTextBlock;
        public string NickTextBlock
        {
            get { return nickTextBlock; }
            set
            {
                if (nickTextBlock != value)
                {
                    nickTextBlock = value;
                    NotifyOfPropertyChange(() => NickTextBlock);
                }
            }
        }

        private string deviceTypeTextBlock;
        public string DeviceTypeTextBlock
        {
            get { return deviceTypeTextBlock; }
            set
            {
                if (deviceTypeTextBlock != value)
                {
                    deviceTypeTextBlock = value;
                    NotifyOfPropertyChange(() => DeviceTypeTextBlock);
                }
            }
        }

        private string deviceIdTextBlock;
        public string DeviceIdTextBlock
        {
            get { return deviceIdTextBlock; }
            set
            {
                if (deviceIdTextBlock != value)
                {
                    deviceIdTextBlock = value;
                    NotifyOfPropertyChange(() => DeviceIdTextBlock);
                }
            }
        }

        private string latTextBlock;
        public string LatTextBlock
        {
            get { return latTextBlock; }
            set
            {
                if (latTextBlock != value)
                {
                    latTextBlock = value;
                    NotifyOfPropertyChange(() => LatTextBlock);
                }
            }
        }

        private string lonTextBlock;
        public string LonTextBlock
        {
            get { return lonTextBlock; }
            set
            {
                if (lonTextBlock != value)
                {
                    lonTextBlock = value;
                    NotifyOfPropertyChange(() => LonTextBlock);
                }
            }
        }

        private string characterListTextBlock;
        public string CharacterListTextBlock
        {
            get { return characterListTextBlock; }
            set
            {
                if (characterListTextBlock != value)
                {
                    characterListTextBlock = value;
                    NotifyOfPropertyChange(() => CharacterListTextBlock);
                }
            }
        }

        private string readButton;
        public string ReadButton
        {
            get { return readButton; }
            set
            {
                if (readButton != value)
                {
                    readButton = value;
                    NotifyOfPropertyChange(() => ReadButton);
                }
            }
        }

        private string readCharacterButton;
        public string ReadCharacterButton
        {
            get { return readCharacterButton; }
            set
            {
                if (readCharacterButton != value)
                {
                    readCharacterButton = value;
                    NotifyOfPropertyChange(() => ReadCharacterButton);
                }
            }
        }


        private string updateButton;
        public string UpdateButton
        {
            get { return updateButton; }
            set
            {
                if (updateButton != value)
                {
                    updateButton = value;
                    NotifyOfPropertyChange(() => UpdateButton);
                }
            }
        }

        private string updateSelectiveButton;
        public string UpdateSelectiveButton
        {
            get { return updateSelectiveButton; }
            set
            {
                if (updateSelectiveButton != value)
                {
                    updateSelectiveButton = value;
                    NotifyOfPropertyChange(() => UpdateSelectiveButton);
                }
            }
        }

        private string deleteButton;
        public string DeleteButton
        {
            get { return deleteButton; }
            set
            {
                if (deleteButton != value)
                {
                    deleteButton = value;
                    NotifyOfPropertyChange(() => DeleteButton);
                }
            }
        }

        private bool isFormAccessible;
        public bool IsFormAccessible
        {
            get { return isFormAccessible; }
            set
            {
                if (isFormAccessible != value)
                {
                    isFormAccessible = value;
                    NotifyOfPropertyChange(() => IsFormAccessible);
                    eventAggregator.Publish(isFormAccessible);
                }
            }
        }

        private string token;
        public string Token
        {
            get { return token; }
            set
            {
                if (token != value)
                {
                    token = value;
                    NotifyOfPropertyChange(() => Token);
                }
            }
        }
        #endregion
        #endregion
    }
}
