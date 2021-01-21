using System;
using System.Linq;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Microsoft.MixedReality.Toolkit.UI;

namespace HoloDrone
{

    public abstract class AppStateBase
    {
        [Inject] 
        public AppStateManager stateMananger;

        public Interactable myButton;

        public virtual bool dissableSelfRotations => false;
        public virtual bool dissableWaves => false;

        public abstract void EnterState();

        public abstract void ExitState();

        // [Inject]
        // public abstract void AddSelfToManager(AppStateManager manager);
    }

    public class AppStateTypeComparer: IComparer<Type> {
        
        public int Compare(Type x, Type y) => x.FullName.CompareTo(y.Name);

    }



    public class AppStateManager : ITickable, IFixedTickable, IInitializable
    {
        //
        SortedDictionary<Type,AppStateBase> _states = new SortedDictionary<Type, AppStateBase>(new AppStateTypeComparer());

        public AppStateBase _currentStateHandler {private set;get;}

        // public MenuContext menuContext;


        // [Inject] 
        // public void WERTYU (AppStateAdjust registrator) {
        //     Debug.Log("AppStateAdjust at Manager");
        // }

        //TODO: Hope to remove this ugly activation
        [Inject] AppStateAdjust appStateAdjust;
        [Inject] AppStateExplode appStateExplode;
        [Inject] AppStateInfo appStateInfo;

        [Inject] 
        public void MenuRegistryBinding (R_MenuSlotBinder registrator) {
            Debug.Log("R_MenuSlotBinder at Manager");

            void defoultActivationFunction(MenuSlotBinder menuSlotBinder) 
            => ActivateSlot(menuSlotBinder.GetComponent<Interactable>(),menuSlotBinder.transform.GetSiblingIndex());

            foreach(var slot in registrator.components) defoultActivationFunction(slot);

            registrator.componentAdded += defoultActivationFunction;
        }

        public void ExitCurrentState () => _currentStateHandler?.ExitState();

        //AddStates on {AppStateBase.Initialize()}, after Injections

        
        public void AddState<T>(T state) where T: AppStateBase
        {
            Debug.Log(typeof(T).Name);
            _states[typeof(T)] = state;
        }

        //Triggered when Injecting {MenuSlotBinder.BindMeToManager()}
        public void ActivateSlot(Interactable button, int slotIndex) {

            var stateForSlot = _states.ElementAt(slotIndex).Value;

            stateForSlot.myButton = button;

            button.OnClick.AddListener(()=> {
                SwitchState(stateForSlot);
            });

        }

        //IDEA: Rebuild to proper async and whait for Exit action to be Done before Entering next state
        public void SwitchState<T>(T nextState = null) where T: AppStateBase {
            if(nextState == null) nextState = GetState<T>();
            //TODO: Exit State when Button Deselected
            if(_currentStateHandler == nextState) {
                ExitCurrentState();
                _currentStateHandler.myButton.IsToggled = false;
                _currentStateHandler = null;
            }else{
                if(_currentStateHandler != null) _currentStateHandler.myButton.IsToggled = false;
                ExitCurrentState();

                nextState.EnterState();
                nextState.myButton.IsToggled = true;

                _currentStateHandler = nextState;
            }
        }

        public T GetState<T>() where T: AppStateBase => (T)_states[typeof(T)];

        public void FixedTick() {}
        public void Tick() {}

        public void Initialize()
        {
        }
    }
}