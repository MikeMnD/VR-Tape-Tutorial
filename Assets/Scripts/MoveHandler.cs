using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using Obi;

public class MoveHandler : MonoBehaviour {

    private GameObject rightController;
    private GameObject rightHandler;
    VRTK_InteractableObject interactHandler; 
    private ObiCloth obiCloth;

	// Use this for initialization
	void Start () {
        rightController = VRTK_DeviceFinder.GetControllerRightHand();
        rightHandler = GameObject.Find("clothPart/right_hand");
        interactHandler = rightHandler.GetComponent<VRTK_InteractableObject>();
        obiCloth = GameObject.Find("clothPart").GetComponent<ObiCloth>();
	}
	
	// Update is called once per frame
	void Update () {

        //IEnumerable<ObiConstraintBatch> ts = obiCloth.PinConstraints.batches;
        List<ObiPinConstraintBatch> ts = obiCloth.PinConstraints.GetPinBatches();

        //Debug.Log(ts.Count);        // 1
        Debug.Log("index :" + ts[0].pinIndices[0]);
        Debug.Log("index :" + ts[0].pinIndices[1]);
        Debug.Log("index :" + ts[0].pinIndices[2]);
        Debug.Log("index :" + ts[0].pinIndices[3]);
        /*
        foreach (ObiPinConstraintBatch ocb in ts)
        {
            // !! todo print ocb.pinindice[0:4]
            //Debug.Log(ocb.ConstraintCount);             // 4
            Debug.Log("length: " + ocb.pinIndices.Count + ", index = " + ocb.pinIndices[0]);    // the index of the pin constraints
            // solverIndices[i] = constraints.Actor.particleIndices[pinIndices[i]];
            //           List<int> ocbList = ocb.GetConstraintsInvolvingParticle(0);
            //           Debug.Log(ocbList.Count);
        }
        */

        ObiActor actor = obiCloth;
        ObiSolver solver = actor.Solver;
        //Debug.Log("particle number : " + actor.particleIndices.Length);     // 33 * 4 = 132

        
        //Debug.Log(actor.GetParticlePosition(0));
     // solver index of the first particle in the actor.
        //ObiSolver solver = actor.Solver;
        //int particleSolverIndex = actor.particleIndices[0];



        if (interactHandler.IsUsing() && interactHandler.GetUsingObject().name == rightController.name)
        {
            /*
            for (int i = 0; i < actor.particleIndices.Length; ++i)
            {
                Vector3 pos = solver.renderablePositions[actor.particleIndices[i]];
                Debug.Log(pos);
            }
            */


            // remove the right handler pin constraints -- ObiPinConstraints.AddBatches();
            // get the current pin constraints

            // get the current position of the right handler
            // add the right handler pin constraints

            Debug.Log("using cloth");
            //            obiCloth.PinConstraints
            IEnumerable<ObiConstraintBatch> ocb_batch = obiCloth.PinConstraints.GetBatches();
            foreach(ObiConstraintBatch ocb in ocb_batch)
            {
                Debug.Log(ocb.ToString());
            }

        }
    }
}
