using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollectableItem : MonoBehaviour
{
    public TextMeshProUGUI itemCountText;
    public TextMeshProUGUI pickupPromptText;

    private int itemCount = 0; // Counter for the number of collected items

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Cast a ray from the center of the screen
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;

            // Check if the ray hits an object
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the hit object is a collectible item
                if (hit.collider.CompareTag("CollectableItem"))
                {
                    // Destroy the collected item GameObject
                    Destroy(hit.collider.gameObject);
                    
                    // Increment the item count
                    itemCount++;

                    // Update the HUD with the new item count
                    UpdateHUD();
                    
                    // Hide the pickup prompt
                    UpdatePickupPrompt(false);
                }
            }
        }

        // Check if the player is looking 
        RaycastHit raycastHit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)), out raycastHit))
        {
            if (raycastHit.collider.CompareTag("CollectableItem"))
            {
                // Display the pickup prompt
                UpdatePickupPrompt(true);
            }
            else
            {
                // Hide the pickup prompt
                UpdatePickupPrompt(false);
            }
        }
        else
        {
            UpdatePickupPrompt(false);
        }
    }

    private void UpdateHUD()
{
    if (itemCount == 3)
    {
        itemCountText.text = "Congratulations! You've found all of the Golden Goblets and won the game!";
    }
    else
    {
        itemCountText.text = "Find all of the Golden Goblets to Win: You've found " + itemCount + " of 3!";
    }
}


    private void UpdatePickupPrompt(bool isActive)
    {
        if (pickupPromptText != null)
        {
            pickupPromptText.gameObject.SetActive(isActive);
        }
        else
        {
            Debug.LogWarning("pickupPromptText is null!");
        }
    }
}
