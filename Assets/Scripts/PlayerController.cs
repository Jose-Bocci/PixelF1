using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Animator animator;
    int posicion = 1;
    public void UpdatePosicion(int posicion)
    {
        this.posicion = posicion;
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        
    }

    // Update is called once per frame
    void Update()
    {
        if (posicion == 1)
        {
            if (Input.GetKeyDown("up"))
            {
                UpdateState("CarrilArriba");
                UpdatePosicion(2);
            }else if (Input.GetKeyDown("down"))
            {
                UpdateState("CarrilAbajo");
                UpdatePosicion(0);
            }
        }
        if (posicion == 2)
        {
            if (Input.GetKeyDown("up"))
            {
            }
            else if (Input.GetKeyDown("down"))
            {
                UpdateState("CarrilMedioArriba");
                UpdatePosicion(1);
            }
        }
        if (posicion == 0)
        {
            if (Input.GetKeyDown("up"))
            {
                UpdateState("CarrilMedioAbajo");
                UpdatePosicion(1);
            }
            else if (Input.GetKeyDown("down"))
            {
            }
        }
    }

 

    public void UpdateState(string state = null)
    {
        if (state != null)
        {
            animator.Play(state);
        }
    }

}
