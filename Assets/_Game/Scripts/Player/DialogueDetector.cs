using UnityEngine;

public class DialogueDetector : MonoBehaviour
{
    public Dialogue currentNPC;

    private void OnTriggerStay2D(Collider2D collision)
    {
        Dialogue npc = collision.GetComponent<Dialogue>();
        if (npc != null)
        {
            // Solo cambiar si no hay uno activo o este está más cerca
            if (currentNPC == null ||
                Vector2.Distance(transform.position, npc.transform.position) <
                Vector2.Distance(transform.position, currentNPC.transform.position))
            {
                currentNPC = npc;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Dialogue npc = collision.GetComponent<Dialogue>();
        if (npc != null && npc == currentNPC)
        {
            currentNPC = null;
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
