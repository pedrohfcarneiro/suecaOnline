using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayButton : NetworkBehaviour
{
    private Button playButton;

    private void Awake()
    {
        playButton = GetComponent<Button>();
        playButton.onClick.AddListener(() =>
        {
            Debug.Log("play pressed");
            if (IsHost)
            {
                SceneManager.LoadScene("Gameplay");
                clientLoadGameplayClientRpc();
            }
        });
    }

    [ClientRpc]
    public void clientLoadGameplayClientRpc()
    {
        if(IsClient)
        {
            SceneManager.LoadScene("Gameplay");
        }
    }
}
