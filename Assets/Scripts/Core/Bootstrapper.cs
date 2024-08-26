using System;
using MechingCards.Common;
using MechingCards.InputService;
using UnityEngine;

namespace MechingCards.Core {
    public class Bootstrapper : MonoBehaviour {
        void Awake() {
            InitializeServices();
        }

        void InitializeServices() {
            var saveService = new SaveSystemService.SaveSystemService();

            IInputService inputService;


            switch (Application.platform) {
                case RuntimePlatform.Android:
                case RuntimePlatform.IPhonePlayer:
                    inputService = new MobileInputService();
                    break;
                case RuntimePlatform.WindowsEditor:
                case RuntimePlatform.WindowsPlayer:
                case RuntimePlatform.OSXEditor:
                case RuntimePlatform.OSXPlayer:
                    inputService = new PCInputService();
                    break;
                default:
                    Debug.LogError($"{nameof(Bootstrapper)} -> {nameof(InitializeServices)} :" +
                                   $"The {Application.platform} platform input system is not suported. Stop execution.");
                    return;
            }

            var gameService = new GameplayService.GameplayService(inputService);
            var menuService = new MenuService.MenuService(saveService, gameService);
            
            
            Action onSaveServiceInitialized = () => {
                menuService.Initialize();
            };
            saveService.Initialize(onSaveServiceInitialized);
        }
    }
}
