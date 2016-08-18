using UnityEngine;
using System.Collections;

public class TileScript : MonoBehaviour {

    public GameManager manager;
    public Renderer tileFace;
    //public Material tileFaceMaterial;
    public Renderer tileShell;
    public Material tileShellHighlightMaterial;

    private Material    tileShellDefaultMaterial;
    private bool        faceDown = true;

	// Use this for initialization
	void Start () {
        tileShellDefaultMaterial = tileShell.material;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetMaterial (Material newMaterial)
    {
        tileFace.material = newMaterial;
    }

    public IEnumerator Disappear (float disappearTime)
    {
        Go.to(transform, disappearTime, new GoTweenConfig().scale(Vector3.zero).setEaseType(GoEaseType.BackIn));
        yield return new WaitForSeconds(disappearTime);
        Destroy(gameObject);
    }

    public IEnumerator FlipBackOver (float delay, float flipTime)
    {
        yield return new WaitForSeconds(delay);
        Go.to(transform, flipTime, new GoTweenConfig().eulerAngles(Vector3.forward * -180f, true));
        yield return new WaitForSeconds(flipTime);
        faceDown = true;
    }

    void OnMouseEnter()
    {
        tileShell.material = tileShellHighlightMaterial;
    }

    void OnMouseExit()
    {
        tileShell.material = tileShellDefaultMaterial;
    }

    IEnumerator OnMouseUpAsButton()
    {
        if (faceDown)
        {
            faceDown = false;
            Go.to(transform, 0.2f, new GoTweenConfig().eulerAngles(Vector3.forward * 180f, true));
            yield return new WaitForSeconds(0.2f);
            manager.TileSelected(transform);
        }
    }
}
