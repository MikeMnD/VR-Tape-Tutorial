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
            Debug.Log("go to next scene");
            /* change scene */
            //SceneManager.LoadScene(this.name);
            SceneManager.LoadScene("User");
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
