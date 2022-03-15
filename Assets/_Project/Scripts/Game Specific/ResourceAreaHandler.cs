using UnityEngine;

public class ResourceAreaHandler : MonoBehaviour
{
    public ResourceType type;
    public TextMesh amountTxt;

    private int resourcesValue = 0;

    public void AddResources(int _val) {

        resourcesValue += _val;
        amountTxt.text = resourcesValue.ToString();
    }
}
