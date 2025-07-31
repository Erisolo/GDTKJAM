using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plateLowererController : MonoBehaviour
{
    bool itemCollided;
    HashSet<dragObject> weights = new HashSet<dragObject>();
    int actTotalWeight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void addWeight(dragObject d)
    {
        if (!weights.Contains(d))
        {
            d.gameObject.transform.parent = this.transform;
            weights.Add(d);
            actTotalWeight += d.weight;
            levelerManager.Instance.weightChanged(this);
            Debug.Log("peso registrado");
        }
        
    }

    void removeWeight(dragObject d)
    {
        if (weights.Contains(d))
        {
            d.gameObject.transform.parent = null;
            weights.Remove(d);
            actTotalWeight -= d.weight;
            levelerManager.Instance.weightChanged(this);
            Debug.Log("peso deregistrado");
        }
    }

    public int getTotalWeight()
    {
        return actTotalWeight;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<dragObject>() != null)
        {
            if (!Input.GetMouseButton(0))   //if the object has been let go)
            {
                addWeight(collision.gameObject.GetComponent<dragObject>());
                
            }
            else
            {
                itemCollided = true;
            }

        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(itemCollided)
        {
            if (!Input.GetMouseButton(0) && collision.gameObject.GetComponent<dragObject>() != null)   //if the object has been let go)
            {
                addWeight(collision.gameObject.GetComponent<dragObject>());
                itemCollided = false;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (gameObject.activeSelf && collision.gameObject.GetComponent<dragObject>() != null)
        {
            removeWeight(collision.gameObject.GetComponent<dragObject>());
           
        }
        if (itemCollided)
            itemCollided = false;
    }

    
}
