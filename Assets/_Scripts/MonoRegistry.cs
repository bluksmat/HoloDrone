using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


namespace HoloDrone {

    public class MonoRegistry<T> where T: MonoBehaviour{

        readonly Action<T> componentAdded;
        readonly Action<T> componentRemoved;

        readonly List<T> _components;

        public IEnumerable<T> components => _components;
        
        public void AddComponent(T component) {
            _components.Add(component);
            componentAdded(component);
        } 

        public void RemoveComponent(T component) {
            _components.Remove(component);
            componentRemoved(component);
        } 

        public override string ToString() {
            return base.ToString();
        }
    }

    public abstract class RegistredMonoBehaviour<T1,T2>: MonoBehaviour where T1: RegistredMonoBehaviour<T1,T2> where T2: MonoRegistry<T1>{

        Action onDestroy;

        [Inject]
        void Register(T2 c) {
            c.AddComponent((T1)this);
            onDestroy += () => c.RemoveComponent((T1)this);
        }

        protected virtual void OnDestroy() => onDestroy();
    }
}