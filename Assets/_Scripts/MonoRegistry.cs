using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


namespace HoloDrone {

    public class MonoRegister<T> where T: MonoBehaviour{

        public  Action<T> componentAdded;
        public Action<T> componentRemoved;

        readonly List<T> _components = new List<T>();

        public IEnumerable<T> components => _components;
        
        public void AddComponent(T component) {
            _components.Add(component);
            if(componentAdded != null) componentAdded(component);
        } 

        public void RemoveComponent(T component) {
            _components.Remove(component);
            if(componentRemoved != null) componentRemoved(component);
        } 

        public override string ToString() {
            return base.ToString();
        }
    }

    public abstract class RegistredMonoBehaviour<T,MonoRegister>: MonoBehaviour where T: RegistredMonoBehaviour<T,MonoRegister> where MonoRegister: MonoRegister<T>{

        Action onDestroy;

        [Inject]
        void Register(MonoRegister c) {
            c.AddComponent((T)this);
            onDestroy += () => c.RemoveComponent((T)this);
        }

        protected virtual void OnDestroy() => onDestroy();
    }

}