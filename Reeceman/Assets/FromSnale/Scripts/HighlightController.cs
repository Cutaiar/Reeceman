using UnityEngine;

public class HighlightController : MonoBehaviour {

    private Renderer ren;

    void Awake () {
        ren = GetComponent<Renderer>();
	}

    private void Start()
    {
        UnHighlight();
    }

    public void Highlight()
    {
        ren.material.SetFloat("_OutlineWidth", 0.02f);
    }

    public void UnHighlight()
    {
        ren.material.SetFloat("_OutlineWidth", 0);
    }
}
