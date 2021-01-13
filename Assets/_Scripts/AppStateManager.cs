using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Microsoft.MixedReality.Toolkit.UI;

namespace HoloDrone
{

    public abstract class AppStateBase: IInitializable
    {
        [Inject] 
        public AppStateManager stateMananger;

        public Interactable myButton;

        public virtual bool allowMicroAnimations => false;

        public abstract void EnterState();

        public abstract void ExitState();

        public abstract void AddSelfToManager();

        public void Initialize() {
            AddSelfToManager();
        }
    }

    public class AppStateManager : ITickable, IFixedTickable
    {
        Dictionary<Type,AppStateBase> _states = null;
        public AppStateBase _currentStateHandler {private set;get;}

        Interactable[] _menuButtons;

        public AppStateManager(int statesInitialCapacity) {
            _states = new Dictionary<Type,AppStateBase>(statesInitialCapacity);
            _menuButtons = new Interactable[statesInitialCapacity];
        }

        public void ExitCurrentState () => _currentStateHandler?.ExitState();

        //AddStates on {AppStateBase.Initialize()}, after Injections
        public void AddStateToSlot<T>(T state,int index) where T: AppStateBase
        {
            Debug.Log("add state");
            _states[typeof(T)] = state;
            state.myButton = _menuButtons[index];

            state.myButton.OnClick.RemoveAllListeners();
            state.myButton.OnClick.AddListener(()=> SwitchState<T>());

            state.myButton.gameObject.SetActive(true);
            state.myButton.IsToggled = false;
        }

        //Triggered when Injecting {MenuSlotBinder.BindMeToManager()}
        public void ActivateSlot(Interactable button, int slotIndex) {
            Debug.Log("activateSlot");
            _menuButtons[slotIndex] = button;
            button.enabled = true;
        }

        //IDEA: Rebuild to proper async and whait for Exit action to be Done before Entering next state
        public void SwitchState<T>() where T: AppStateBase {
            var nextState = GetState<T>();
            //TODO: Exit State when Button Deselected
            if(_currentStateHandler == nextState) return;

            // _currentStateHandler!.myButton.IsToggled = false;
            if(_currentStateHandler != null) _currentStateHandler.myButton.IsToggled = false;
            ExitCurrentState();

            nextState.EnterState();
            nextState.myButton.IsToggled = true;

            _currentStateHandler = nextState;
        }

        public T GetState<T>() where T: AppStateBase => (T)_states[typeof(T)];

        public void FixedTick() {}
        public void Tick() {}
    }
}