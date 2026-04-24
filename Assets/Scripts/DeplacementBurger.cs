using UnityEngine;

public class DeplacementBurger : MonoBehaviour
{
    public Transform anchor; // Glisse l'objet "Anchor" ici
    private GameObject burgerTenu;

    void Update()
    {
        // Exemple : Appuyer sur la touche "E" pour prendre ou lâcher
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (burgerTenu == null)
            {
                EssayerDePrendre();
            }
            else
            {
                Lacher();
            }
        }
    }

    void EssayerDePrendre()
    {
        // On cherche un objet avec le tag "Burger" dans un rayon de 2 mètres
        Collider[] objetsProches = Physics.OverlapSphere(transform.position, 2f);
        
        foreach (var col in objetsProches)
        {
            if (col.CompareTag("Burger"))
            {
                burgerTenu = col.gameObject;

                // --- LA MAGIE DU PARENTAGE ---
                // 1. On le lie à la mallette (via l'anchor)
                burgerTenu.transform.SetParent(anchor);

                // 2. On le réinitialise pour qu'il soit bien centré
                burgerTenu.transform.localPosition = Vector3.zero;
                burgerTenu.transform.localRotation = Quaternion.identity;

                // 3. IMPORTANT : On fige la physique pour qu'il ne tombe plus
                Rigidbody rb = burgerTenu.GetComponent<Rigidbody>();
                rb.isKinematic = true; 
                
                break;
            }
        }
    }

    void Lacher()
    {
        // 1. On casse le lien de parenté
        burgerTenu.transform.SetParent(null);

        // 2. On réactive la physique pour qu'il tombe normalement
        Rigidbody rb = burgerTenu.GetComponent<Rigidbody>();
        rb.isKinematic = false;

        burgerTenu = null;
    }
}
