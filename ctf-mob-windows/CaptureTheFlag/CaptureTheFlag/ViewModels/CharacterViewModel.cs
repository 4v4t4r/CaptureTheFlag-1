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
    public class CharacterViewModel : Screen
    {
        private readonly INavigationService navigationService;
        private readonly IEventAggregator eventAggregator;
        private readonly ICommunicationService communicationService;

        public CharacterViewModel(INavigationService navigationService, IEventAggregator eventAggregator, ICommunicationService communicationService)
        {
            DebugLogger.WriteLine("", this.GetType(), MethodBase.GetCurrentMethod());
            this.navigationService = navigationService;
            this.eventAggregator = eventAggregator;
            this.communicationService = communicationService;

            IsFormAccessible = true;

            Character = new Character();
#warning Temporary variables
            Character.url = "http://78.133.154.39:8888/api/characters/17/";

            DisplayName = "Character";

            UrlTextBlock = "Character:";
            UserTextBlock = "User:";
            CharacterTypeTextBlock = "Character type:";
            TotalTimeTextBlock = "Total time:";
            TotalScoreTextBlock = "Total score:";
            HealthTextBlock = "Health:";
            LevelTextBlock = "Level:";
            IsActiveTextBlock = "Is active:";

            ReadButton = "Read";
        }

        #region ViewModel States
        protected override void OnActivate()
        {
            DebugLogger.WriteLine("", this.GetType(), MethodBase.GetCurrentMethod());
            base.OnActivate();
            eventAggregator.Subscribe(this);
            //Character.url = CharacterModelKey;
            //Character = IoC.Get<GlobalStorageService>().Current.Characters[CharacterModelKey];
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
        public void ReadAction()
        {
            DebugLogger.WriteLine("", this.GetType(), MethodBase.GetCurrentMethod());
            communicationService.ReadCharacter(Character, Token,
                responseData =>
                {
                    DebugLogger.WriteLine("Successful create callback", this.GetType(), MethodBase.GetCurrentMethod());
                    MessageBox.Show("OK", "read", MessageBoxButton.OK);
                    Character = responseData;
                },
                serverErrorMessage =>
                {
                    DebugLogger.WriteLine("Failed create callback", this.GetType(), MethodBase.GetCurrentMethod());
                    MessageBox.Show(serverErrorMessage.Code.ToString(), serverErrorMessage.Message, MessageBoxButton.OK);
                }
            );
        }
        #endregion

        #region Properties

        #region Model Properties
        private Character character;
        public Character Character
        {
            get { return character; }
            set
            {
                if (character != value)
                {
                    character = value;
                    NotifyOfPropertyChange(() => Character);
                }
            }
        }

        private string characterModelKey;
        public string CharacterModelKey
        {
            get { return characterModelKey; }
            set
            {
                if (characterModelKey != value)
                {
                    characterModelKey = value;
                    NotifyOfPropertyChange(() => CharacterModelKey);
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

        private string userTextBlock;
        public string UserTextBlock
        {
            get { return userTextBlock; }
            set
            {
                if (userTextBlock != value)
                {
                    userTextBlock = value;
                    NotifyOfPropertyChange(() => UserTextBlock);
                }
            }
        }

        private string characterTypeTextBlock;
        public string CharacterTypeTextBlock
        {
            get { return characterTypeTextBlock; }
            set
            {
                if (characterTypeTextBlock != value)
                {
                    characterTypeTextBlock = value;
                    NotifyOfPropertyChange(() => CharacterTypeTextBlock);
                }
            }
        }

        private string totalTimeTextBlock;
        public string TotalTimeTextBlock
        {
            get { return totalTimeTextBlock; }
            set
            {
                if (totalTimeTextBlock != value)
                {
                    totalTimeTextBlock = value;
                    NotifyOfPropertyChange(() => TotalTimeTextBlock);
                }
            }
        }

        private string totalScoreTextBlock;
        public string TotalScoreTextBlock
        {
            get { return totalScoreTextBlock; }
            set
            {
                if (totalScoreTextBlock != value)
                {
                    totalScoreTextBlock = value;
                    NotifyOfPropertyChange(() => TotalScoreTextBlock);
                }
            }
        }

        private string healthTextBlock;
        public string HealthTextBlock
        {
            get { return healthTextBlock; }
            set
            {
                if (healthTextBlock != value)
                {
                    healthTextBlock = value;
                    NotifyOfPropertyChange(() => HealthTextBlock);
                }
            }
        }

        private string levelTextBlock;
        public string LevelTextBlock
        {
            get { return levelTextBlock; }
            set
            {
                if (levelTextBlock != value)
                {
                    levelTextBlock = value;
                    NotifyOfPropertyChange(() => LevelTextBlock);
                }
            }
        }

        private string isActiveTextBlock;
        public string IsActiveTextBlock
        {
            get { return isActiveTextBlock; }
            set
            {
                if (isActiveTextBlock != value)
                {
                    isActiveTextBlock = value;
                    NotifyOfPropertyChange(() => IsActiveTextBlock);
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
