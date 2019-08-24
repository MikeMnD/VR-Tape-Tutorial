namespace VRTK
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class Btn_muscle_behavior : VRTK_InteractableObject
    {
        public override void StartUsing(VRTK_InteractUse currentUsingObject = null)
        {
            base.StartUsing(currentUsingObject);
            StaticData.setTargetTapeLength(this.name.ToLower());
            Debug.Log("go to next scene");
            /* change scene */
            StaticData.resetAll();
            SceneManager.LoadScene("ecum 1");
            // SceneManager.LoadScene(StaticData.getTargetNumName());
            StopUsing();
        }

        protected void Start()
        {

        }

        protected override void Update()
        {
            base.Update();
        }
    }
}
