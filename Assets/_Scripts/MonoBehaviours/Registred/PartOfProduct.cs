using UnityEngine;
using System;
using Zenject;
using HoloDrone;
using HoloDrone.MicroAnimations;
using Microsoft.MixedReality.Toolkit.UI;

namespace HoloDrone {

    public class R_PartOfProduct: MonoRegistry<PartOfProduct>{}

    [AddComponentMenu("Holo Drone/Part Of Product")]
    public class PartOfProduct : RegistredMonoBehaviour<PartOfProduct,R_PartOfProduct>, IInitializable
    {
        public string label = "";
        
        [Tooltip("Explosion would be relative to this target")]
        public PartOfProduct mergeTo;

        [Tooltip("Object would not innclude it's own offfset")]
        public bool solidMerge;

        public Transform refTransform;

        public void Initialize () {
            // this.
            // refTransform = Instantiate<Transform>(null,this.transform);
        }
    }
}