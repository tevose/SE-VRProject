using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateAvatarLoader : MonoBehaviour
{
    public Material[] material;
    Renderer render;
    private string selectedAvatar = MenuManager.creatingAvitar;
    public int colorIndex;

    public void setCurrentAvatar(){
        

        if (selectedAvatar == "Avitar 1"){
            colorIndex = 0;
        } else if (selectedAvatar == "Avitar 2"){
            colorIndex = 1;
        }else{
            colorIndex = 2;
        }
    }


    void Start ()
    {
        setCurrentAvatar();
        render = GetComponent<Renderer>();
        // render.sharedMaterial = material[colorIndex];
        var currentMaterial = render.materials;
        currentMaterial[0] = material[colorIndex];
        render.materials = currentMaterial;
    }

}


