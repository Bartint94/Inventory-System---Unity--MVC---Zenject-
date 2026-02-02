using UnityEngine;
[CreateAssetMenu(menuName = "SlotsConfig")]
public class SlotsConfig : ScriptableObject
{
    [SerializeField] int rows;
    [SerializeField] int columns;
    [SerializeField] SlotColorPalette colorPalette;
    public int Rows => rows;
    public int Columns => columns;
    public SlotColorPalette ColorPalette => colorPalette;
}
