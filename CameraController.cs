using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class CameraController : MonoBehaviour {
    
        public GameObject camRef, leftButton, rightButton, downButton;
        GameObject objectRef;
        bool leftRotate, rightRotate;
    
        void Start()
        {
                objectRef = camRef;
                leftRotate = false;
                rightRotate = false;
            }
    
        void Update()
        {
        
                if ( Input.GetMouseButtonDown (0) && !EventSystem.current.IsPointerOverGameObject()){
                  RaycastHit hit;
                  Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                 if ( Physics.Raycast (ray,out hit,100.0f))
                    {
                            if(hit.transform.tag == "ZoomIn")
                            {
                                    print(hit.transform.name);
                                    print(hit.transform.GetChild(0).transform.name);
                                    objectRef = hit.transform.GetChild(0).transform.gameObject;
                                }
                    }
        }
        
        
                if (transform.position != objectRef.transform.position)
                {
                    transform.position = Vector3.Lerp(transform.position, objectRef.transform.position, Time.deltaTime*5);
                    transform.rotation = Quaternion.Lerp(transform.rotation, objectRef.transform.rotation, Time.deltaTime * 5);
            
                    //transform.position = Vector3.MoveTowards(transform.position, objectRef.transform.position, Time.deltaTime*10);
                    //transform.rotation = Quaternion.RotateTowards(transform.rotation, objectRef.transform.rotation, Time.deltaTime * 10);
                    }
        
                if (leftRotate)
                {
                        transform.position = objectRef.transform.position;
                        transform.Rotate(Vector3.down * Time.deltaTime * 70,Space.World);
                    }
                if (rightRotate)
                {
                        transform.position = objectRef.transform.position;
                        transform.Rotate(Vector3.up * Time.deltaTime * 70,Space.World);
                    }
            }
    
        public void LeftRotate()
        {
                leftRotate = true;
            }
        public void RightRotate()
        {
                rightRotate = true;
            }
        public void StopRotate()
        {
                transform.position = objectRef.transform.position;
                leftRotate = false;
                rightRotate = false;
            }
        public void ReturnToStart()
        {
                GameObject cameraRef= camRef;
                cameraRef.transform.eulerAngles = new Vector3(camRef.transform.eulerAngles.x,transform.eulerAngles.y,camRef.transform.eulerAngles.z);
                print(cameraRef.transform.rotation.y);
                print(cameraRef.transform.localRotation.y);
        
                objectRef = cameraRef;
                //objectRef.transform.position = camRef.transform.position;
        
                //objectRef.transform.rotation = Quaternion.Euler(camRef.transform.rotation.x,camRef.transform.rotation.eulerAngles.y,camRef.transform.rotation.x);
        
                //float cameraY = transform.rotation.y;
                //objectRef = camRef;
                //Quaternion tempu = Quaternion.Euler(camRef.transform.rotation.x,cameraY,camRef.transform.rotation.z);
                //objectRef.transform.rotation = tempu;
        
                /*Transform temp = camRef.transform;
                         temp.rotation.y = transform.rotation.y;
                 
                         objectRef = camRef;
                         Quaternion temp2 = objectRef.transform.rotation;
                         float currentY = this.transform.rotation.y;
                         objectRef.transform.rotation = Quaternion.Euler(temp2.x,currentY,temp2.z);
                         */
            }
        void OnTriggerStay(Collider hit)
        {
                if (hit.tag == "CameraBoundary")
                {
                        leftButton.SetActive(true);
                        rightButton.SetActive(true);
                        downButton.SetActive(false);
                    }
            }
        void OnTriggerExit(Collider hit)
        {
                if (hit.tag == "CameraBoundary")
                {
                        leftButton.SetActive(false);
                        rightButton.SetActive(false);
                        downButton.SetActive(true);
                    }
            }
}

