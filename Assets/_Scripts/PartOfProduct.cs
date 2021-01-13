using UnityEngine;
using System;
using Zenject;
using HoloDrone;
using HoloDrone.MicroAnimations;
using Microsoft.MixedReality.Toolkit.UI;

namespace HoloDrone {

    [AddComponentMenu("Holo Drone/Part Of Product")]
    public class PartOfProduct : MonoBehaviour, IInitializable
    {
        public string label = "";
        
        [Tooltip("Explosion would be relative to this target")]
        public PartOfProduct mergeTo;

        [Tooltip("Object would not innclude it's own offfset")]
        public bool solidMerge;

        public Transform refTransform;
        public void Initialize () {
            refTransform = Instantiate<Transform>(null,this.transform);
        }
        // [Inject] void _ (AppStateExplode c) => c.AddPart(this);
        // [Inject] void _ (AppStateInfo c) => string.IsNullOrEmpty(label) ? ()=>{} : c.AddPart(this);
    }
}