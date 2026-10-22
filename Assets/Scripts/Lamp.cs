using UnityEngine;

public class Lamp : MonoBehaviour {
    private Light LightComponent;
    private Material HeadMaterial;
    public Material TurnedOffMaterial;
    private bool TurnedOn = true;
    void Start() {
        LightComponent = this.GetComponentInChildren<Light>();
        if (TurnedOffMaterial != null) {
            HeadMaterial = this.transform.Find("Head").GetComponent<MeshRenderer>().material;
        }
    }

    public void Toggle() {
        LightComponent.enabled = !LightComponent.enabled;
        if (TurnedOffMaterial != null) {
            if (!TurnedOn) {
                this.transform.Find("Head").GetComponent<MeshRenderer>().material = HeadMaterial;
            }
            else {
                this.transform.Find("Head").GetComponent<MeshRenderer>().material = TurnedOffMaterial;
            }
        }
        TurnedOn = !TurnedOn;
    }
}
