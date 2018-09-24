using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tube : MonoBehaviour
{

    private ProceduralShit _Shit;
    [SerializeField]private List<Transform> _Disks;
    // Use this for initialization
    void Start()
    {
        _Shit = GameObject.Find("Manager").GetComponent<ProceduralShit>();
        foreach (Transform child in this.transform)
        {
            _Disks.Add(child);
        }

        int rng = Random.Range(0, 3);
       
        switch (rng)
        {
            case 0:
                _Disks[0].gameObject.SetActive(true);  
                _Disks[1].gameObject.SetActive(false);
                _Disks[2].gameObject.SetActive(false);
             
              
        
                break;
            case 1:

                _Disks[0].gameObject.SetActive(false);
                _Disks[1].gameObject.SetActive(true);            
                _Disks[2].gameObject.SetActive(false);      
                break;

            case 2:
                _Disks[0].gameObject.SetActive(false);
                _Disks[1].gameObject.SetActive(false);
                _Disks[2].gameObject.SetActive(true);
             
                break;
        

          

        }

        for (int i = 0; i < _Disks.Count -2; i++)
        {
            _Disks[i].transform.rotation = Quaternion.Euler(_Disks[i].transform.rotation.x, _Disks[i].transform.rotation.y, Random.Range(0, 361));
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(_Shit._Speed* transform.forward * Time.deltaTime);
    }

    private void Update()
    {
        if(transform.position.z >= 550)
        {
            int rng = Random.Range(0, 3);
            _Disks[0].gameObject.SetActive(false);
            _Disks[1].gameObject.SetActive(false);
            _Disks[2].gameObject.SetActive(false);
            switch (rng)
            {
                case 0:
                    _Disks[0].gameObject.SetActive(true);
                    _Disks[1].gameObject.SetActive(false);
                    _Disks[2].gameObject.SetActive(false);
                   
               
                    
                    break;
                case 1:

                    _Disks[0].gameObject.SetActive(false);
                    _Disks[1].gameObject.SetActive(true);
                    _Disks[2].gameObject.SetActive(false);
                  
                 
                   
                    break;

                case 2:
                    _Disks[0].gameObject.SetActive(false);
                    _Disks[1].gameObject.SetActive(false);
                    _Disks[2].gameObject.SetActive(true);
            
                   
                    break;
           

              
               

            }
            for (int i = 0; i < _Disks.Count -2; i++)
            {
                _Disks[i].transform.rotation = Quaternion.Euler(_Disks[i].transform.rotation.x, _Disks[i].transform.rotation.y, Random.Range(0, 361));
            }
            transform.position = new Vector3(transform.position.x, transform.position.y, -1907.5f);
        }
    }


}
