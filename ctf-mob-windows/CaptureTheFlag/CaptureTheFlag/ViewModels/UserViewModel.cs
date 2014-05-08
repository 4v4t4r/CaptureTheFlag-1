using Caliburn.Micro;
using CaptureTheFlag.Models;
using CaptureTheFlag.Services;
using RestSharp;
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
        private readonly ICommunicationService communicationService;
        private readonly IGlobalStorageService globalStorageService;
        private RestRequestAsyncHandle requestHandle;// TODO: implement abort

        public UserViewModel(INavigationService navigationService, ICommunicationService communicationService, IGlobalStorageService globalStorageService)
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            this.navigationService = navigationService;
            this.communicationService = communicationService;
            this.globalStorageService = globalStorageService;

            DisplayName = "Account";

            IsFormAccessible = true;

            User = new UserD();
            Characters = new BindableCollection<Character>();

            UsernameTextBlock = "Username:";
            FirstNameTextBlock = "First name:";
            LastNameTextBlock = "Last name";
            EmailTextBlock = "E-mail:";
            PasswordTextBlock = "Password:";
            NickTextBlock = "Nick:";
            DeviceTypeTextBlock = "Device type:";
            LatTextBlock = "Latitude:";
            LonTextBlock = "Longitude:";

            UpdateUserIcon = new Uri("/Images/upload.png", UriKind.Relative);
            ReadCharacterIcon = new Uri("/Images/like.png", UriKind.Relative);

            ReadCharacterAppBarItemText = "character";
            UpdateUserAppBarItemText = "update";
            DeleteAppBarMenuItemText = "delete";
        }

        #region ViewModel States
        protected override void OnActivate()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            base.OnActivate();
            Authenticator = globalStorageService.Current.Authenticator;
            if (globalStorageService.Current.Users != null && globalStorageService.Current.Users.ContainsKey(Authenticator.user))
            {
                User = globalStorageService.Current.DUsers[Authenticator.user];
            }
            else
            {
                User.url = Authenticator.user;
                ReadAction();
            }
        }
        #endregion

        #region Actions
        public void ReadCharacterAction()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            //navigationService.UriFor<CharacterViewModel>()
            //    .WithParam(param => param.CharacterModelKey, "http://78.133.154.39:8888/api/characters/15/")
            //    .WithParam(param => param.Token, Token)
            //    .Navigate();
            navigationService.UriFor<CharacterViewModel>()
                .WithParam(param => param.CharacterModelKey, User.active_character)
                .Navigate();
        }

        //public void SelectCharacterAction()
        //{
        //    DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
        //    IsFormAccessible = false;
        //    Character chara = new Character(); //TODO: replace with actual caracter
        //    if (Authenticator.IsValid(Authenticator))
        //    {
        //        communicationService.SelectCharacter(chara, Authenticator.token,
        //            responseData =>
        //            {
        //                DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Successful create callback");
        //                IsFormAccessible = true;
        //                MessageBox.Show("OK", "read", MessageBoxButton.OK);
        //                //User = responseData;
        //            },
        //            serverErrorMessage =>
        //            {
        //                DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Failed create callback");
        //                IsFormAccessible = true;
        //                MessageBox.Show(serverErrorMessage.Code.ToString(), serverErrorMessage.Message, MessageBoxButton.OK);
        //            }
        //        );
        //    }
        //}

        public void ReadCharactersAction()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            IsFormAccessible = false;
            if (Authenticator.IsValid(Authenticator))
            {
                foreach(string characterString in User.characters)
                {
                    Character ch = new Character();
                    ch.url = characterString;
                    communicationService.ReadCharacter(ch, Authenticator.token, responseData =>
                        {
                            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Successful create callback");
                            Characters.Add(responseData);
                            IsFormAccessible = true;
                            //MessageBox.Show("OK", "read", MessageBoxButton.OK);
                        },
                        serverErrorMessage =>
                        {
                            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Failed create callback");
                            IsFormAccessible = true;
                            //MessageBox.Show(serverErrorMessage.Code.ToString(), serverErrorMessage.Message, MessageBoxButton.OK);
                        }
                    );
                }
            }
        }

        public void ReadAction()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            IsFormAccessible = false;
            if (Authenticator.IsValid(Authenticator))
            {
                communicationService.ReadUser(User, Authenticator.token,
                    responseData =>
                    {
                        DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Successful create callback");
                        User = responseData;
                        ReadCharactersAction();
                        IsFormAccessible = true;
                        MessageBox.Show("OK", "read", MessageBoxButton.OK);
                    },
                    serverErrorMessage =>
                    {
                        DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Failed create callback");
                        IsFormAccessible = true;
                        MessageBox.Show(serverErrorMessage.Code.ToString(), serverErrorMessage.Message, MessageBoxButton.OK);
                    }
                );
            }
        }

        public void DeleteAction()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            IsFormAccessible = false;
            if (Authenticator.IsValid(Authenticator))
            {
                communicationService.DeleteUser(User, Authenticator.token,
                    responseUserMap =>
                    {
                        DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Successful create callback");
                        MessageBox.Show("OK", "deleted", MessageBoxButton.OK);
                        IsFormAccessible = true;
                    },
                    serverErrorMessage =>
                    {
                        DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Failed create callback");
                        MessageBox.Show(serverErrorMessage.Code.ToString(), serverErrorMessage.Message, MessageBoxButton.OK);
                        IsFormAccessible = true;
                    }
                );
            }
        }

        public void UpdateAction()
        {
            DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod());
            IsFormAccessible = false;
            if (Authenticator.IsValid(Authenticator))
            {
                communicationService.UpdateUser(User, Authenticator.token,
                    responseUserMap =>
                    {
                        DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Successful create callback");
                        User = responseUserMap;
                        IsFormAccessible = true;
                    },
                    serverErrorMessage =>
                    {
                        DebugLogger.WriteLine(this.GetType(), MethodBase.GetCurrentMethod(), "Failed create callback");
                        MessageBox.Show(serverErrorMessage.Code.ToString(), serverErrorMessage.Message, MessageBoxButton.OK);
                        IsFormAccessible = true;
                    }
                );
            }
        }
        #endregion

        #region Properties

        #region Model Properties
        private Authenticator authenticator;
        public Authenticator Authenticator
        {
            get { return authenticator; }
            set
            {
                if (authenticator != value)
                {
                    authenticator = value;
                    NotifyOfPropertyChange(() => Authenticator);
                }
            }
        }

        private BindableCollection<Character> characters;
        public BindableCollection<Character> Characters
        {
            get { return characters; }
            set
            {
                if (characters != value)
                {
                    characters = value;
                    NotifyOfPropertyChange(() => Characters);
                }
            }
        }

        private UserD user;
        public UserD User
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

        private string selectedCharacter;
        public string SelectedCharacter
        {
            get { return selectedCharacter; }
            set
            {
                if (selectedCharacter != value)
                {
                    selectedCharacter = value;
                    NotifyOfPropertyChange(() => SelectedCharacter);
                }
            }
        }
        #endregion

        #region UI Properties

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

        private Uri eeadCharacterIcon;
        public Uri ReadCharacterIcon
        {
            get { return eeadCharacterIcon; }
            set
            {
                if (eeadCharacterIcon != value)
                {
                    eeadCharacterIcon = value;
                    NotifyOfPropertyChange(() => ReadCharacterIcon);
                }
            }
        }

        private Uri updateUserIcon;
        public Uri UpdateUserIcon
        {
            get { return updateUserIcon; }
            set
            {
                if (updateUserIcon != value)
                {
                    updateUserIcon = value;
                    NotifyOfPropertyChange(() => UpdateUserIcon);
                }
            }
        }


        private string readCharacterAppBarItemText;
        public string ReadCharacterAppBarItemText
        {
            get { return readCharacterAppBarItemText; }
            set
            {
                if (readCharacterAppBarItemText != value)
                {
                    readCharacterAppBarItemText = value;
                    NotifyOfPropertyChange(() => ReadCharacterAppBarItemText);
                }
            }
        }


        private string updateUserAppBarItemText;
        public string UpdateUserAppBarItemText
        {
            get { return updateUserAppBarItemText; }
            set
            {
                if (updateUserAppBarItemText != value)
                {
                    updateUserAppBarItemText = value;
                    NotifyOfPropertyChange(() => UpdateUserAppBarItemText);
                }
            }
        }

        private string deleteAppBarMenuItemText;
        public string DeleteAppBarMenuItemText
        {
            get { return deleteAppBarMenuItemText; }
            set
            {
                if (deleteAppBarMenuItemText != value)
                {
                    deleteAppBarMenuItemText = value;
                    NotifyOfPropertyChange(() => DeleteAppBarMenuItemText);
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
                }
            }
        }
        #endregion
        #endregion
    }
}
