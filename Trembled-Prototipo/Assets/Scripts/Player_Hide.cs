using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Hide : MonoBehaviour
{
    [SerializeField] private float scaleMore = 1.1f;

    private Renderer[] playerRenderers;
    private Transform currentTable = null;
    private Vector3 originalScale;
    private bool isHidden = false;

    private PlayerScript moveScript;

    void Start()
    {
        playerRenderers = GetComponentsInChildren<Renderer>();

        moveScript = GetComponent<PlayerScript>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Table"))
        {
            currentTable = other.transform;

            // Guardamos el scale original del MeshRenderer real (la parte visual)
            MeshRenderer mesh = currentTable.GetComponentInChildren<MeshRenderer>();
            if (mesh != null)
            {
                originalScale = mesh.transform.localScale;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Table"))
        {
            if (isHidden)
                ShowPlayer(); // Si se va estando escondido, lo forzamos a salir.

            currentTable = null;
        }
    }

    void Update()
    {
        if (currentTable == null) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isHidden)
                HidePlayer();
            else
                ShowPlayer();
        }
    }

    private void HidePlayer()
    {
        // Escalamos SOLO la parte visual
        MeshRenderer visual = currentTable.GetComponentInChildren<MeshRenderer>();
        if (visual != null)
            visual.transform.localScale = originalScale * scaleMore;

        foreach (Renderer r in playerRenderers)
            r.enabled = false;

        if (moveScript != null)
            moveScript.enabled = false;

        isHidden = true;
    }

    private void ShowPlayer()
    {
        // Restaurar escala de la mesa
        MeshRenderer visual = currentTable.GetComponentInChildren<MeshRenderer>();
        if (visual != null)
            visual.transform.localScale = originalScale;

        foreach (Renderer r in playerRenderers)
            r.enabled = true;

        if(moveScript != null)
            moveScript.enabled = true;

        isHidden = false;
    }
}