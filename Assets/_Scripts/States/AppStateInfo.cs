using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using TMPro;

namespace HoloDrone {
    // IDEA: Tool to adjust tooltips in prefab preview
    public class AppStateInfo : AppStateBase
    {
        [Inject]
        readonly Settings settings = null;

        public override bool dissableWaves => true;

        //TODO: Move to parent class for more generic solution
        private bool _active = false; 

        R_PartOfProduct _partsOfProduct;

        List<ToolTip> tooltipsPool = new List<ToolTip>();

        //TODO Make proper container for this dictionaries
        Dictionary<string,ToolTip> tooltipsByLabel = new Dictionary<string,ToolTip>();

        Dictionary<string,int> tooltipSetupSelected = new Dictionary<string,int>();

        Dictionary<string,List<PartOfProduct>> partsByLabel = new Dictionary<string, List<PartOfProduct>>();

        Dictionary<string,List<PartOfProduct.PartTooltipSetup>> partsSetupByLabel = new Dictionary<string, List<PartOfProduct.PartTooltipSetup>>();

        //In case of changeing product/model 
        bool rebuildOnEnter = false;

        [Inject]
        void AddSelfToManager(AppStateManager manager) => stateMananger.AddState(this);

        [Inject]
        void GetPartRegistry(R_PartOfProduct partsOfProduct) {
            this._partsOfProduct = partsOfProduct;

            partsOfProduct.componentAdded += OnPartAdded;
            partsOfProduct.componentRemoved += OnPartRemoved;
        }

        private void OnPartAdded (PartOfProduct part) {
            rebuildOnEnter = true;

            part.onFocusEnter += OnPartFocused;

            if(!partsByLabel.ContainsKey(part.label)) {
                partsByLabel[part.label] = new List<PartOfProduct>();
                partsSetupByLabel[part.label] = new List<PartOfProduct.PartTooltipSetup>();
            }

            partsByLabel[part.label].Add(part);

            if(part.productTooltipSetups == null) part.productTooltipSetups = new List<PartOfProduct.PartTooltipSetup>();
            if(part.productTooltipSetups.Count == 0) part.productTooltipSetups.Add(new PartOfProduct.PartTooltipSetup());

            foreach(var setup in part.productTooltipSetups) {
                if(setup.tooltipAnchor == null) {
                    setup.tooltipAnchor = new GameObject("tooltip_anchor").transform;

                    setup.tooltipAnchor.position = (part.useBoundingBoxCenter ? part.transform.TransformPoint(part.localBoundCenter) : part.transform.position);
                }

                if(setup.tooltipPivot == null) {
                    setup.tooltipPivot = new GameObject("tooltip_pivot").transform;

                    setup.tooltipPivot.position = setup.tooltipAnchor.position + (part.transform.up * settings.autoFillEmptyPivotsDistance);
                }
                //TODO: parent to drone prefab root instead
                setup.tooltipAnchor.SetParent(stateMananger.appStateAdjust.boundingBox.transform);
                setup.tooltipPivot.SetParent(stateMananger.appStateAdjust.boundingBox.transform);
                setup.tooltipAnchor.hideFlags = HideFlags.HideInHierarchy;
                setup.tooltipPivot.hideFlags = HideFlags.HideInHierarchy;
            }
            partsSetupByLabel[part.label].AddRange(part.productTooltipSetups);
        }
        private void OnPartRemoved (PartOfProduct part) {
            rebuildOnEnter = true;

            part.onFocusEnter -= OnPartFocused;
            partsByLabel[part.label].Remove(part);
        }

        private void OnPartFocused (PartOfProduct part, FocusEventData eventData) {
            if(!_active) return;

            //TODO: On part of product with same name focused, move tooltip
        }

        public override void EnterState() {
            _active = true;
            if(rebuildOnEnter) {
                rebuildOnEnter = false;
                RebuildInfo();
            }

            foreach(ToolTip tooltip in tooltipsByLabel.Values) tooltip.gameObject.SetActive(true);
        }

        public override void ExitState() {
            _active = false;

            foreach(ToolTip tooltip in tooltipsByLabel.Values) tooltip.gameObject.SetActive(false);
        }
        
        //TODO: Best setup rebuild calculation should be done after Last Part is added on anothe thread to avoid heavy objects long calculation pause
        void RebuildInfo () {

            var partsByLabelValus = partsByLabel.Values;
            var labelIndex = 0;

            foreach(var labelSetups in partsSetupByLabel) {

                if(tooltipsPool.Count == labelIndex) {
                    tooltipsPool.Add(ToolTip.Instantiate<GameObject>(settings.toolTipPrefab).GetComponent<ToolTip>());
                    tooltipsPool[labelIndex].transform.SetParent(stateMananger.appStateAdjust.boundingBox.transform);
                }

                tooltipsPool[labelIndex].ToolTipText = labelSetups.Key;

                PartOfProduct.PartTooltipSetup farthestSetup = labelSetups.Value[0];
                float farthestTooltipDistance = 0f;

                float tempDistance = 0f;
                float tempNearestDistance = Mathf.Infinity;
                PartOfProduct.PartTooltipSetup tempSetup = farthestSetup;
                PartOfProduct.PartTooltipSetup tempNearestSetup = farthestSetup;

                //Avoid 1 setup cases and calculate 1-st case setup distance if there is more than 1 setup
                if(labelSetups.Value.Count>1) {
                    for(int setupIndex = 0; setupIndex < labelSetups.Value.Count; setupIndex++) {
                        tempNearestDistance = Mathf.Infinity;
                        for(int i = 0; i < labelIndex; i++) {
                            if((tempDistance = Vector3.Distance(tooltipsPool[i].Anchor.transform.position, (tempSetup = labelSetups.Value[setupIndex]).tooltipAnchor.position)) < tempNearestDistance) {
                                tempNearestDistance = tempDistance;
                                tempNearestSetup = tempSetup;
                            }
                        }
                        if(tempNearestDistance > farthestTooltipDistance) {
                            farthestTooltipDistance = tempNearestDistance;
                            farthestSetup = tempNearestSetup;
                        }
                    }
                }

                tooltipsPool[labelIndex].Anchor.transform.position = farthestSetup.tooltipAnchor.transform.position;
                tooltipsPool[labelIndex].Pivot.transform.position = farthestSetup.tooltipPivot.transform.position;

                tooltipsByLabel[labelSetups.Key] = tooltipsPool[labelIndex];

                labelIndex++;
            }

        }

        [Serializable]
        public enum DisplayFormat {
            OneOfGroup,
            Merged,
            NoGrouping,
        }

        [Serializable]
        public class Settings {
            public GameObject toolTipPrefab;
            public float autoFillEmptyPivotsDistance = 1;
            public bool faceUser;
        }
    }
}