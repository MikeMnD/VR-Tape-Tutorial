namespace VRTK {
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Customized_Pointer : VRTK_Pointer
    {
        private Transform headset;
        private GameObject _tooltip;

        public override void OnDestinationMarkerEnter(DestinationMarkerEventArgs e)
        {
            base.OnDestinationMarkerEnter(e);
            Transform _btn = e.target;
            _tooltip = _btn.GetChild(0).gameObject;
            _tooltip.SetActive(true);
        }

        public override void OnDestinationMarkerExit(DestinationMarkerEventArgs e)
        {
            base.OnDestinationMarkerHover(e);
            if(_tooltip != null) {
                _tooltip.SetActive(false);
            }
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
        }
    }
}