using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace HoloDrone
{
    interface IAppState {
        void EnterState();
        void ExitState();
    }

    //TODO: Generate based on "AppState*.cs" classes defined in namespace HoloDrone
    // [GenerateEnumForClasses("AppState")]
    enum AppStates{
        Adjust,
        Explode,
        Info
    }


    class AppStateManager : ITickable, IFixedTickable, IInitializable
    {

        Dictionary<AppStates,IAppState> _states;
        IAppState _currentStateHandler;

        [Inject]
        public void InjectState(IAppState state)
        {
            Debug.Log("InjectState:");
        }


        public void FixedTick() {}
        public void Tick() {}
        public void Initialize() {}
    }
}