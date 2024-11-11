using System.Collections.Generic;
using UnityEngine;

public class SladyGracza : MonoBehaviour
{
    public List<Vector3> slady = new List<Vector3>();  // Lista pozycji gracza
    public float odstepMiedzySladami = 0.5f;           // Czas (w sekundach) mi�dzy zapisami pozycji

    private float czasOdOstatniegoSladu;

    private void Update()
    {
        czasOdOstatniegoSladu += Time.deltaTime;

        if (czasOdOstatniegoSladu >= odstepMiedzySladami)
        {
            ZapiszSlad();
            czasOdOstatniegoSladu = 0;
        }
    }

    private void ZapiszSlad()
    {
        // Dodaje aktualn� pozycj� gracza do listy �lad�w
        slady.Add(transform.position);
    }
}
