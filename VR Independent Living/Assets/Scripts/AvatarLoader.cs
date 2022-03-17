using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarLoader : MonoBehaviour
{
    public Material[] material;
    Renderer render1;
    Renderer render2;
    


    public GameObject joiningAvatar;
    public GameObject creatingAvatar;
    public GameObject cameraPosition;


    private string selectedCAColor = MenuManager.creatingAvitar;
    private string selectedJAColor = MenuManager.joiningAvitar;
    private string userType = MenuManager.userType;

    public int creatingAvatarIndex;
    public int joiningAvatarIndex;
    public string room = MenuManager.roomID;

    public void setCreatingAvatarColor(){
        

        if (selectedCAColor == "Avitar 1"){
            creatingAvatarIndex = 0;
        } else if (selectedCAColor == "Avitar 2"){
            creatingAvatarIndex = 1;
        }else{
            creatingAvatarIndex = 2;
        }
    }

    public void setJoiningAvatarColor(){
        

        if (selectedJAColor == "Avitar 1"){
            joiningAvatarIndex = 0;
        } else if (selectedJAColor == "Avitar 2"){
            joiningAvatarIndex = 1;
        }else{
            joiningAvatarIndex = 2;
        }
    }


    void Start ()
    {
        setCreatingAvatarColor();
        setJoiningAvatarColor();

        if (userType == "joining") {
            render1 = joiningAvatar.GetComponent<Renderer>();
            var currentMaterial = render1.materials;
            currentMaterial[0] = material[joiningAvatarIndex];
            render1.materials = currentMaterial;
            

            // Set Camera postion based on room
            if (room == "Home") {
                cameraPosition.transform.position = new Vector3(4, 2, -1);
                cameraPosition.transform.rotation = Quaternion.Euler(3.2f, 0.4f, -0.01f);
            }

            else if (room == "Cafe") {
                cameraPosition.transform.position = new Vector3(-12.039f, 24.82f, 47.67f);
                cameraPosition.transform.rotation = Quaternion.Euler(4.67f, -179.379f, 0f);
            }

            else if (room == "Park") {
                cameraPosition.transform.position = new Vector3(836.4f, 0.935f, 218.11f);
                cameraPosition.transform.rotation = Quaternion.Euler(6.395f, 14.099f, 0f);
            }

            // Since I'm a joining user render material for the user who created the room
            // Obtain this from shared database
            render2 = creatingAvatar.GetComponent<Renderer>();
            var currentMaterial2 = render2.materials;
            currentMaterial2[0] = material[creatingAvatarIndex];
            render2.materials = currentMaterial2;

            
        }

        else {
            
            // Since I'm a creating user I will render my own material from the main menu
            render1 = creatingAvatar.GetComponent<Renderer>();
            var currentMaterial = render1.materials;
            currentMaterial[0] = material[creatingAvatarIndex];
            render1.materials = currentMaterial;

            // Obtain joining users materil from a shared database
            render2 = joiningAvatar.GetComponent<Renderer>();
            var currentMaterial2 = render2.materials;
            currentMaterial2[0] = material[joiningAvatarIndex];
            render2.materials = currentMaterial2;
        }

        
    }


    
}
