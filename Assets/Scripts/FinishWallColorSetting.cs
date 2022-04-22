using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FinishWallColorSetting : MonoBehaviour
{
    [SerializeField] private FinishWallColor wallColor;
    [SerializeField] private bool isRandomColor;
    [SerializeField] private List<Material> colorsMaterials;
    [SerializeField] private MeshRenderer wallRenderer;
    [SerializeField] private List<MeshRenderer> wallPartsRenderers;

    private void Start()
    {
        if (isRandomColor)
        {
            var colorMaterial = colorsMaterials[Random.Range(0, colorsMaterials.Count - 1)];
            SetWallColor(colorMaterial);
        }
        else
            SetWallColor(colorsMaterials[(int)wallColor]);
    }

    private void SetWallColor(Material colorMaterial)
    {
        wallRenderer.material = colorMaterial;
        foreach (var partRenderer in wallPartsRenderers)
            partRenderer.material = colorMaterial;
    }
}

public enum FinishWallColor
{
    Purple,
    Blue,
    Cyan,
    Green,
    Yellow,
    Orange,
    Red
}
