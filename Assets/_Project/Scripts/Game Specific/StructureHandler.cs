using UnityEngine;

public class StructureHandler : MonoBehaviour
{
    public Material [] usedMaterials;
    public StructurePartHandler[] houseParts;
    public Color transparentColor;
    public Color solidColor;
    public float totalParts = 0;
    public float partsBuild = 0;

    private void Start()
    {
        houseParts = this.GetComponentsInChildren<StructurePartHandler>();
        totalParts = houseParts.Length;
        //MaterialsTransparentStatus(true);

    }

    public void MaterialsTransparentStatus(bool _val) {

        //if (_val)
        //{
        //    for (int i = 0; i < usedMaterials.Length; i++)
        //    {
        //        ChangeRenderMode(usedMaterials[i], _val);
        //        //usedMaterials[i].SetFloat("_Mode", 3f);
        //        //usedMaterials[i].color = transparentColor;
        //    }
        //}
        //else {
        //    for (int i = 0; i < usedMaterials.Length; i++)
        //    {
        //        ChangeRenderMode(usedMaterials[i], _val);
        //        //usedMaterials[i].SetFloat("_Mode", 0);
        //        //usedMaterials[i].color = solidColor;
        //    }
        //}

        for (int i = 0; i < usedMaterials.Length; i++)
        {
            ChangeRenderMode(usedMaterials[i], _val);
        }
    }

    public void HousePartComplete() {

        partsBuild++;
        float progress = partsBuild / totalParts;
        Toolbox.HUDListner.SetProgressBarFill(progress);

        if (partsBuild >= totalParts) {

            MaterialsTransparentStatus(false);
        }
    }

    public void ChangeRenderMode(Material standardShaderMaterial, bool _transparent)
    {
        //Debug.LogError("IN VAL = " + _transparent);

        switch (_transparent)
        {
            case false:
                standardShaderMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                standardShaderMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                standardShaderMaterial.SetInt("_ZWrite", 1);
                standardShaderMaterial.DisableKeyword("_ALPHATEST_ON");
                standardShaderMaterial.DisableKeyword("_ALPHABLEND_ON");
                standardShaderMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                standardShaderMaterial.renderQueue = -1;
                standardShaderMaterial.color = solidColor;

                break;

            case true:
                //standardShaderMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                //standardShaderMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                //standardShaderMaterial.SetInt("_ZWrite", 0);
                //standardShaderMaterial.DisableKeyword("_ALPHATEST_ON");
                //standardShaderMaterial.DisableKeyword("_ALPHABLEND_ON");
                //standardShaderMaterial.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                //standardShaderMaterial.renderQueue = 3000;
                //standardShaderMaterial.color = transparentColor;

                standardShaderMaterial.SetFloat("_Mode", 3f);
                standardShaderMaterial.color = transparentColor;
                break;
        }

    }
}
