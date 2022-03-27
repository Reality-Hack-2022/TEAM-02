using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityTimer;

public class PointerManager : MonoBehaviour
{
    public GameObject UIController;

    public GameObject pulsingAnim;

    public GameObject description_intestine;

    public GameObject digestive_system;
    
    private Timer timer;

    private GameObject anim;


    // Start is called before the first frame update
    void Start()
    {
        anim = transform.parent.GetChild(0).GetChild(0).gameObject;
        anim.SetActive(false);
        pulsingAnim.SetActive(false);
        description_intestine.SetActive(false);
        digestive_system.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "collider")
        {
            //find sibling
            //translate up
            GameObject sib = collision.transform.parent.GetChild(1).gameObject;
            sib.SetActive(true);
            //sib.transform.DOMoveZ(sib.transform.position.z + 0.02f, 0.2f);
            collision.gameObject.tag = "highlighted";

            //show description
            description_intestine.SetActive(true);
            description_intestine.transform.DOScale(0.001f, 0.2f);
            digestive_system.SetActive(true);

            //1s timer
            timer = Timer.Register(1f, () => Locked(collision));
            anim.SetActive(true);
        }

        if (collision.gameObject.tag == "locked")
        {
            timer = Timer.Register(2f, () => CompleteMassage(collision));
        }



    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "highlighted")
        {
            //collision.transform.DOMoveZ(collision.transform.position.z - 0.02f, 0.2f);
            collision.gameObject.tag = "collider";
            //turn off surface
            collision.transform.parent.GetChild(1).gameObject.SetActive(false);

            //hide description
            description_intestine.transform.DOScale(0.0001f, 0.2f);
            description_intestine.SetActive(false);
            digestive_system.SetActive(false);

            //1s timer
            timer.Cancel();
            anim.SetActive(false);
        }

        if (collision.gameObject.tag == "locked")
        {
            timer.Cancel();
            //locked state surface
        }
    }

    private void Locked(Collider c)
    {
        print("Locked");
        anim.SetActive(false);
        //UIController.GetComponent<UIController>().PublicMassageHint();
        pulsingAnim.SetActive(true);
        c.gameObject.tag = "locked";
        c.transform.parent.GetChild(1).GetComponent<MeshRenderer>().material.color = Color.red;
    }

    private void CompleteMassage(Collider c)
    {
        UIController.GetComponent<UIController>().PublicPressComplete();
        c.transform.parent.GetChild(1).GetComponent<MeshRenderer>().material.color = Color.green;
    }
}
